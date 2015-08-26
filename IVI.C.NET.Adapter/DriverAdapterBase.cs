//--------------------------------------------------------------------------------------------------
//
//   Copyright Tom Lu <luqiang1983@gmail.com>
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
//
//--------------------------------------------------------------------------------------------------

using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using Microsoft.Win32;
using Ivi.ConfigServer.Interop;
using Ivi.Driver;
using IVI.C.NET.Adapter.Win32Interop;

// Ivi type mapping
using ViStatus = System.Int32;
using ViSession = System.IntPtr;
using ViString = System.String;
using ViBoolean = System.Boolean;
using ViInt8 = System.Byte;
using ViInt16 = System.Int16;
using ViInt32 = System.Int32;
using ViAttr = System.UInt32;
using ViInt64 = System.Int64;
using ViReal64 = System.Double;
using IVI.C.NET.Adapter.IviCInterop;
using System.Text;
using System.Diagnostics;

namespace IVI.C.NET.Adapter
{
    public class DriverAdapterBase<T> : IDriverAdapterBase, IIviDriver
    {
        private static AssemblyBuilder assemblyBuilder = null;
        private ModuleBuilder moduleBuilder = null;
        private TypeBuilder typeBuilder = null;
        private Type interopClassType = null;
        private IntPtr hModule = IntPtr.Zero;

        private string TargetDriverName = null;
        private string TargetDriverPrefix = null;
        private string TargetLibraryFileName = null;

        private Dictionary<string, string> DriverOptions = null;
        private IntPtr ViSession = IntPtr.Zero;
        private Adapter.IviCInterop.IviDriver DriverInterop = null;

        private IIviDriverOperation driverOperation = null;
        private IIviDriverIdentity identity = null;
        private IIviDriverUtility utility = null;

        public DriverAdapterBase(string name, bool idQuery, bool reset, string options)
        {
            DriverOptions = new Dictionary<string, string>();
            IIviConfigStore ConfigStore = LoadConfigStore();
            IviDriverSession CurrentDriver = ConfigStore.GetDriverSession(name);

            // Parse Options from Ivi driver setup
            ParseDriverOptions(DriverOptions, CurrentDriver.DriverSetup);
            // Parse Options from parameter
            ParseDriverOptions(DriverOptions, options);

            if (DriverOptions.ContainsKey("TargetName") && !DriverOptions["TargetName"].Equals(string.Empty))
            {
                CurrentDriver = ConfigStore.GetDriverSession(DriverOptions["TargetName"]);
            }

            if (CurrentDriver.SoftwareModule == null)
            {
                new Exception(string.Format("Software Module not configured for driver session {0}", DriverOptions["TargetName"]));
            }

            TargetDriverName = CurrentDriver.Name;
            TargetDriverPrefix = CurrentDriver.SoftwareModule.Prefix;
            TargetLibraryFileName = CurrentDriver.SoftwareModule.ModulePath;
            string TargetSoftwareModuleName = CurrentDriver.SoftwareModuleName;

            string DriverSetup = CurrentDriver.DriverSetup;

            if (!Path.IsPathRooted(TargetLibraryFileName))
            {
                string IviPath = (string)Registry.LocalMachine.OpenSubKey("SOFTWARE\\IVI").GetValue("IviStandardRootDir");
                TargetLibraryFileName = Path.Combine(IviPath, "Bin\\" + TargetLibraryFileName);
            }

            LoadLibrary();
            InitDynamicAssemblyBuilder();
            DriverFunctionInterop();
            interopClassType = typeBuilder.CreateType();

            DriverInterop = (Adapter.IviCInterop.IviDriver)CreateInstanse();
            ViSessionStatusCheck(DriverInterop.init(TargetDriverName, idQuery, reset, ref ViSession));

            driverOperation = (IIviDriverOperation)new DriverOperation(this);
            identity = (IIviDriverIdentity)new DriverIdentity(this, TargetSoftwareModuleName);
            utility = (IIviDriverUtility)new DriverUtility(this);

            Marshal.ReleaseComObject(CurrentDriver);
            Marshal.ReleaseComObject(ConfigStore);
        }

        ~DriverAdapterBase()
        {
            UnloadLibrary();
        }

        public IIviDriverOperation DriverOperation
        {
            get { return driverOperation; }
        }

        public IIviDriverIdentity Identity
        {
            get { return identity; }
        }

        public IIviDriverUtility Utility
        {
            get { return utility; }
        }

        public object GetService(Type serviceType)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            DriverInterop = null;
            UnloadLibrary();
        }

        public ViSession Session
        {
            get { return ViSession; }
        }

        public IviCInterop.IviDriver Interop
        {
            get { return DriverInterop; }
        }

        #region Low Level DLL Interop

        private T CreateInstanse()
        {
            return (T)interopClassType.GetConstructor(Type.EmptyTypes).Invoke(Type.EmptyTypes);
        }

        private void LoadLibrary()
        {
            if (TargetLibraryFileName == null)
            {
                throw new ArgumentNullException("LibraryFileName");
            }

            hModule = Win32LibInterop.LoadLibrary(TargetLibraryFileName);

            if (hModule == IntPtr.Zero)
            {
                throw new Exception(string.Format("{0}:{1}", Win32LibInterop.GetLastErrorMessage(), TargetLibraryFileName));
            }

        }

        private void UnloadLibrary()
        {
            if (hModule != IntPtr.Zero)
            {
                Win32LibInterop.FreeLibrary(this.hModule);
                hModule = IntPtr.Zero;
            }
        }

        private void InitDynamicAssemblyBuilder()
        {
            string LibraryName = Path.GetFileNameWithoutExtension(TargetLibraryFileName);
            AssemblyName assemblyName = new AssemblyName(string.Format("{0}_DynamicInterop", LibraryName));
            assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
            //Create Module
            moduleBuilder = assemblyBuilder.DefineDynamicModule(assemblyName.Name);

            string typeName = typeof(T).Name;
            typeBuilder = moduleBuilder.DefineType(string.Format("{0}_{1}", TargetDriverPrefix, typeName), TypeAttributes.Public, typeof(object), new Type[] { typeof(T) });

            // Create Constructor
            //-------------------------------------------------------------------------------------------
            ConstructorInfo baseConstructorInfo = typeof(object).GetConstructor(new Type[0]);
            ConstructorBuilder constructorBuilder = typeBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, Type.EmptyTypes);

            ILGenerator ilGenerator = constructorBuilder.GetILGenerator();
            ilGenerator.Emit(OpCodes.Ldarg_0);                      // Load "this"
            ilGenerator.Emit(OpCodes.Call, baseConstructorInfo);    // Call the base constructor
            ilGenerator.Emit(OpCodes.Ret);                          // return

            //-------------------------------------------------------------------------------------------
        }

        private void DriverFunctionInterop()
        {
            ConstructorInfo notImpException = typeof(NotImplementedException).GetConstructor(Type.EmptyTypes);
            Type targetType = typeof(T);
            string typeName = targetType.Name;

            List<MethodInfo> methods = new List<MethodInfo>();

            foreach (Type inheritedType in targetType.GetInterfaces())
            {
                methods.AddRange(inheritedType.GetMethods());
            }

            methods.AddRange(targetType.GetMethods());
            foreach (MethodInfo methodInfo in methods)
            {
                string Win32_FunctionName = string.Format("{0}_{1}", TargetDriverPrefix, methodInfo.Name);
                IntPtr ptrfun = Win32LibInterop.GetProcAddress(this.hModule, Win32_FunctionName);

                //if (ptrfun == IntPtr.Zero)
                //{
                //    throw new Exception(string.Format("{0}:{1}", Win32LibInterop.GetLastErrorMessage(), Win32_FunctionName));
                //}

                Type returnType = methodInfo.ReturnType;
                ParameterInfo[] Parameters = methodInfo.GetParameters();
                Type[] paramTypes = new Type[Parameters.Length];
                for (int i = 0; i < Parameters.Length; i++)
                {
                    paramTypes[i] = Parameters[i].ParameterType;
                }


                // Define the method
                MethodBuilder methodBuilder = typeBuilder.DefineMethod(methodInfo.Name, MethodAttributes.Public | MethodAttributes.Virtual, returnType, paramTypes);

                // Create Method body, call the corresponding driver function
                //-------------------------------------------------------------------------------------------
                // The ILGenerator class is used to put op-codes (similar to assembly) into the
                // method
                ILGenerator genIL = methodBuilder.GetILGenerator();

                if (ptrfun == IntPtr.Zero)
                {
                    // If function is not exist in Ivi C driver dll, then throw not implemented exception. :(
                    genIL.Emit(OpCodes.Newobj, notImpException);
                    genIL.Emit(OpCodes.Throw);
                }
                else
                {
                    // Loads method argument onto the stack for coming DLL call.
                    // Satrt from 1 because 0 is index for 'this' which no use for our DLL call
                    for (int i = 1; i <= paramTypes.Length; i++)
                    {
                        //By-Ref given parameters.
                        genIL.Emit(OpCodes.Ldarg, i);
                    }

                    if (IntPtr.Size == 4)
                    {
                        // Check Processer Type 
                        genIL.Emit(OpCodes.Ldc_I4, ptrfun.ToInt32());
                    }
                    else if (IntPtr.Size == 8)
                    {
                        genIL.Emit(OpCodes.Ldc_I8, ptrfun.ToInt64());
                    }
                    else
                    {
                        throw new PlatformNotSupportedException();
                    }

                    genIL.EmitCalli(OpCodes.Calli, CallingConvention.StdCall, returnType, paramTypes);
                    genIL.Emit(OpCodes.Ret); // Return
                }
                //-------------------------------------------------------------------------------------------
            }
        }

        private void ParseDriverOptions(Dictionary<string, string> DriverOptions, string OptionsSource)
        {
            Regex reg = new Regex("((?<Key>\\w+?)\\s*=((\\s*\\\"(?<Value>.+?)\\s*\\\")|(\\s*(?<Value>\\S+)\\s*)))[,\\s]*");
            MatchCollection matchs = reg.Matches(OptionsSource);

            foreach (Match match in matchs)
            {
                string Key = match.Groups["Key"].Value;
                string Value = match.Groups["Value"].Value;

                if (DriverOptions.ContainsKey(Key))
                {
                    DriverOptions[Key] = Value;
                }
                else
                {
                    DriverOptions.Add(Key, Value);
                }
            }
        }

        #endregion

        private static IIviConfigStore LoadConfigStore()
        {
            IIviConfigStore iviConfigStore = new IviConfigStore();
            string text = iviConfigStore.MasterLocation;
            string processDefaultLocation = iviConfigStore.ProcessDefaultLocation;
            if (!string.IsNullOrEmpty(processDefaultLocation))
            {
                text = processDefaultLocation;
            }
            if (text.Length > 0 && File.Exists(text))
            {
                iviConfigStore.Deserialize(text);
                return iviConfigStore;
            }
            throw new ConfigurationStoreLoadException();
        }

        public void Close()
        {
            if (ViSession != IntPtr.Zero)
            {
                DriverInterop.close(ViSession);
            }
        }

        #region ViSession Attribute Access

        public void ViSessionStatusCheck(ViStatus Status)
        {
            ViSessionStatusCheck(Status, true);
        }

        private void ViSessionStatusCheck(ViStatus Status, bool Critical)
        {
            if (Status != IviDriverAttribute.VI_SUCCESS)
            {
                StringBuilder errorMessage = new StringBuilder(1024);
                DriverInterop.error_message(ViSession, Status, errorMessage);
                Console.WriteLine(errorMessage);
                if (Critical)
                    throw new InstrumentStatusException(errorMessage.ToString());
                // printCallStack();
            }
        }

        private void printCallStack()
        {
            StackTrace ss = new StackTrace(true);
            String flName = ss.GetFrame(1).GetFileName();
            int lineNo = ss.GetFrame(1).GetFileLineNumber();
            String methodName = ss.GetFrame(1).GetMethod().Name;
            Console.WriteLine(flName + "---" + lineNo + "---" + methodName);
        }

        public ViInt32 GetAttributeViInt32(ViAttr attributeId)
        {
            return GetAttributeViInt32(IviDriverAttribute.VI_NULL, attributeId);
        }

        public ViInt32 GetAttributeViInt32(ViString channelName, ViAttr attributeId)
        {
            ViInt32 Value = 0;
            ViSessionStatusCheck(DriverInterop.GetAttributeViInt32(ViSession, channelName, attributeId, ref Value));
            return Value;
        }

        public void SetAttributeViInt32(ViAttr attributeId, ViInt32 value)
        {
            SetAttributeViInt32(IviDriverAttribute.VI_NULL, attributeId, value);
        }

        public void SetAttributeViInt32(ViString channelName, ViAttr attributeId, ViInt32 value)
        {
            ViSessionStatusCheck(DriverInterop.SetAttributeViInt32(ViSession, channelName, attributeId, value));
        }

        public ViInt64 GetAttributeViInt64(ViAttr attributeId)
        {
            return GetAttributeViInt64(IviDriverAttribute.VI_NULL, attributeId);
        }

        public ViInt64 GetAttributeViInt64(ViString channelName, ViAttr attributeId)
        {
            ViInt64 Value = 0;
            ViSessionStatusCheck(DriverInterop.GetAttributeViInt64(ViSession, channelName, attributeId, ref Value));
            return Value;
        }

        public void SetAttributeViInt64(ViAttr attributeId, ViInt64 value)
        {
            SetAttributeViInt64(IviDriverAttribute.VI_NULL, attributeId, value);
        }

        public void SetAttributeViInt64(ViString channelName, ViAttr attributeId, ViInt64 value)
        {
            ViSessionStatusCheck(DriverInterop.SetAttributeViInt64(ViSession, channelName, attributeId, value));
        }

        public ViReal64 GetAttributeViReal64(ViAttr attributeId)
        {
            return GetAttributeViReal64(IviDriverAttribute.VI_NULL, attributeId);
        }

        public ViReal64 GetAttributeViReal64(ViString channelName, ViAttr attributeId)
        {
            ViReal64 Value = 0;
            ViSessionStatusCheck(DriverInterop.GetAttributeViReal64(ViSession, channelName, attributeId, ref Value));
            return Value;
        }

        public void SetAttributeViReal64(ViAttr attributeId, ViReal64 value)
        {
            SetAttributeViReal64(IviDriverAttribute.VI_NULL, attributeId, value);
        }

        public void SetAttributeViReal64(ViString channelName, ViAttr attributeId, ViReal64 value)
        {
            ViSessionStatusCheck(DriverInterop.SetAttributeViReal64(ViSession, channelName, attributeId, value));
        }

        public ViString GetAttributeViString(ViAttr attributeId)
        {
            return GetAttributeViString(IviDriverAttribute.VI_NULL, attributeId);
        }

        public ViString GetAttributeViString(ViString channelName, ViAttr attributeId)
        {
            StringBuilder Value = new StringBuilder(1024);
            ViSessionStatusCheck(DriverInterop.GetAttributeViString(ViSession, channelName, attributeId, Value.Capacity, Value));
            return Value.ToString();
        }

        public void SetAttributeViString(ViAttr attributeId, ViString value)
        {
            SetAttributeViString(IviDriverAttribute.VI_NULL, attributeId, value);
        }

        public void SetAttributeViString(ViString channelName, ViAttr attributeId, ViString value)
        {
            ViSessionStatusCheck(DriverInterop.SetAttributeViString(ViSession, channelName, attributeId, value));
        }

        public ViBoolean GetAttributeViBoolean(ViAttr attributeId)
        {
            return GetAttributeViBoolean(IviDriverAttribute.VI_NULL, attributeId);
        }

        public ViBoolean GetAttributeViBoolean(ViString channelName, ViAttr attributeId)
        {
            ViBoolean Value = false;
            ViSessionStatusCheck(DriverInterop.GetAttributeViBoolean(ViSession, channelName, attributeId, ref Value));
            return Value;
        }

        public void SetAttributeViBoolean(ViAttr attributeId, ViBoolean value)
        {
            SetAttributeViBoolean(IviDriverAttribute.VI_NULL, attributeId, value);
        }

        public void SetAttributeViBoolean(ViString channelName, ViAttr attributeId, ViBoolean value)
        {
            ViSessionStatusCheck(DriverInterop.SetAttributeViBoolean(ViSession, channelName, attributeId, value));
        }

        public ViSession GetAttributeViSession(ViAttr attributeId)
        {
            return GetAttributeViSession(IviDriverAttribute.VI_NULL, attributeId);
        }

        public ViSession GetAttributeViSession(ViString channelName, ViAttr attributeId)
        {
            ViSession Value = IntPtr.Zero;
            ViSessionStatusCheck(DriverInterop.GetAttributeViSession(ViSession, channelName, attributeId, ref Value));
            return Value;
        }

        public void SetAttributeViSession(ViAttr attributeId, ViSession value)
        {
            SetAttributeViSession(IviDriverAttribute.VI_NULL, attributeId, value);
        }

        public void SetAttributeViSession(ViString channelName, ViAttr attributeId, ViSession value)
        {
            ViSessionStatusCheck(DriverInterop.SetAttributeViSession(ViSession, channelName, attributeId, value));
        }

        #endregion

    }
}
