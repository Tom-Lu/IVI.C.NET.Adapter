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
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace IVI.C.NET.Adapter.Win32Interop
{
    class Win32LibInterop
    {
        /// <summary> 
        /// Function: HMODULE LoadLibrary(LPCTSTR lpFileName); 
        /// </summary> 
        /// <param name="lpFileName"> The name of the module(DLL). </param> 
        /// <returns> If the function succeeds, the return value is a handle to the module. If the function fails, the return value is NULL. </returns> 
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr LoadLibrary(string lpFileName);

        /// <summary> 
        /// Function: FARPROC GetProcAddress(HMODULE hModule, LPCWSTR lpProcName); 
        /// </summary> 
        /// <param name="hModule"> A handle to the DLL module that contains the function or variable. </param> 
        /// <param name="lpProcName"> The function or variable name, or the function's ordinal value. If this parameter is an ordinal value, it must be in the low-order word; the high-order word must be zero. </param> 
        /// <returns> If the function succeeds, the return value is the address of the exported function or variable. If the function fails, the return value is NULL. </returns> 
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);

        /// <summary> 
        /// Function: BOOL FreeLibrary(HMODULE hModule); 
        /// </summary> 
        /// <param name="hModule"> A handle to the loaded library module. </param> 
        /// <returns> If the function succeeds, the return value is nonzero. If the function fails, the return value is zero.</returns> 
        [DllImport("kernel32", EntryPoint = "FreeLibrary", SetLastError = true)]
        public static extern bool FreeLibrary(IntPtr hModule);


        /// <summary> 
        /// Function: DWORD GetLastError(void); 
        /// </summary> 
        /// <returns> The return value is the calling thread's last-error code. </returns> 
        [DllImport("kernel32")]
        public static extern uint GetLastError();

        [DllImport("Kernel32.dll")]
        public extern static int FormatMessage(int flag, ref IntPtr source, int msgid, int langid, ref string buf, int size, ref IntPtr args);

        public static string GetLastErrorMessage()
        {
            int errCode = Marshal.GetLastWin32Error();
            IntPtr tempptr = IntPtr.Zero;
            string msg = null;
            FormatMessage(0x1300, ref tempptr, errCode, 0, ref msg, 255, ref tempptr);
            return msg;
        }

        /// <summary>     
        /// The function determines whether the current operating system is a      
        /// 64-bit operating system.     
        /// </summary>     
        /// <returns>     
        /// The function returns true if the operating system is 64-bit;      
        /// otherwise, it returns false.     
        /// </returns>    
        public static bool IsWin64BitOS(OperatingSystem os)
        {
            if (IntPtr.Size == 8)
                // 64-bit programs run only on Win64           
                return true;
            else// 32-bit programs run on both 32-bit and 64-bit Windows     
            {   // Detect whether the current process is a 32-bit process                
                // running on a 64-bit system.               
                return Is64BitProc(Process.GetCurrentProcess());
            }
        }

        /// <summary>  
        /// Checks if the process is 64 bit  
        /// </summary>  
        /// <param name="os"></param>  
        /// <returns>  
        /// The function returns true if the process is 64-bit;        
        /// otherwise, it returns false.  
        /// </returns>    
        public static bool Is64BitProc(System.Diagnostics.Process p)
        {
            // 32-bit programs run on both 32-bit and 64-bit Windows           
            // Detect whether the current process is a 32-bit process                
            // running on a 64-bit system.               
            bool result;
            return ((DoesWin32MethodExist("kernel32.dll", "IsWow64Process") && IsWow64Process(p.Handle, out result)) && result);
        }

        /// <summary>     
        /// The function determins whether a method exists in the export      
        /// table of a certain module.     
        /// </summary>     
        /// <param name="moduleName">The name of the module</param>     
        /// <param name="methodName">The name of the method</param>     
        /// <returns>     
        /// The function returns true if the method specified by methodName      
        /// exists in the export table of the module specified by moduleName.     
        /// </returns>       
        static bool DoesWin32MethodExist(string moduleName, string methodName)
        {
            IntPtr moduleHandle = GetModuleHandle(moduleName);
            if (moduleHandle == IntPtr.Zero)
                return false;
            return (GetProcAddress(moduleHandle, methodName) != IntPtr.Zero);
        }
        [DllImport("kernel32.dll")]
        static extern IntPtr GetCurrentProcess();

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr GetModuleHandle(string moduleName);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool IsWow64Process(IntPtr hProcess, out bool wow64Process);
    }
}
