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
using System.Text;
using System.Collections;
using System.Reflection;
using Ivi.ConfigServer.Interop;

namespace IVI.C.NET.Adapter.ConfigUtility
{
    class IVIHandler
    {
        private static IVIHandler instance = null;
        private IviConfigStore iviConfigStore = null;
        public static string[] RCNameList = new string[] { "Channel", "Marker", "OutputChannel", "LFGenerator", "AnalogModulationSource", 
                                                     "RFInput", "IFOutput", "IFInput", "RFOutput", "ArmSource", "TriggerSource", "OutputPhase" };

        public static IVIHandler Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new IVIHandler();
                }

                return instance;
            }
        }

        public IviConfigStore IviConfigStore
        {
            get { return iviConfigStore; }
        }

        private IVIHandler()
        {
            iviConfigStore = new IviConfigStore();
        }

        public static void Reset()
        {
            instance = null;
        }

        public void Save()
        {
            iviConfigStore.Serialize(iviConfigStore.MasterLocation);
        }

        public IviLogicalName CreateLogicalName()
        {
            IviLogicalName newLogicalName = new IviLogicalName();
            newLogicalName.Name = SuggestName(GetLogicalNames(), "NewLogicalName");
            iviConfigStore.LogicalNames.Add(newLogicalName);
            return newLogicalName;
        }

        public IviHardwareAsset CreateHardwareAsset()
        {
            IviHardwareAsset newHardwareAsset = new IviHardwareAsset();
            newHardwareAsset.Name = SuggestName(GetHardwareNames(), "NewHardwareAsset");
            iviConfigStore.HardwareAssets.Add(newHardwareAsset);
            return newHardwareAsset;
        }

        public IviDriverSession CreateDriverSession()
        {
            IviDriverSession newDriverSession = new IviDriverSession();
            newDriverSession.Name = SuggestName(GetDriverNames(), "NewDriver");
            iviConfigStore.DriverSessions.Add(newDriverSession);
            return newDriverSession;
        }

        public IviVirtualName CreateVirtualName(IviDriverSession DriverSession)
        {
            IviVirtualName newVirtualName = new IviVirtualName();
            newVirtualName.Name = SuggestName(GetVirtualNames(DriverSession), "NewName");
            ArrayList PhysicalNameList = GetPhysicalNameList(DriverSession.SoftwareModule);
            if (PhysicalNameList.Count > 0)
            {
                newVirtualName.MapTo = (string)PhysicalNameList[0];
            }
            else
            {
                newVirtualName.MapTo = "NewMap";
            }

            DriverSession.VirtualNames.Add(newVirtualName);
            return newVirtualName;
        }

        public IviSoftwareModule CreateSoftwareModule()
        {
            IviSoftwareModule newSoftwareModule = new IviSoftwareModule();
            newSoftwareModule.Name = SuggestName(GetSoftwareModuleNames(), "NewSoftwareModule");
            iviConfigStore.SoftwareModules.Add(newSoftwareModule);
            return newSoftwareModule;
        }

        public IviPhysicalName CreatePhysicalName(IviSoftwareModule SoftwareModule)
        {
            IviPhysicalName newPhysicalName = new IviPhysicalName();
            newPhysicalName.Name = SuggestName(GetPhysicalNameList(SoftwareModule), "NewName");
            newPhysicalName.RCName = RCNameList[0];
            SoftwareModule.PhysicalNames.Add(newPhysicalName);
            return newPhysicalName;
        }

        public IviPublishedAPI CreatePublishedAPI(IviSoftwareModule SoftwareModule)
        {
            IviPublishedAPI newPublishedAPI = GetUnusedPublishedAPI(SoftwareModule);
            if (newPublishedAPI == null)
            {
                newPublishedAPI = new IviPublishedAPI();
                newPublishedAPI.Name = SuggestName(GetGlobalPublishedAPINameList(), "IviDriver");
                newPublishedAPI.MajorVersion = 1;
                newPublishedAPI.MinorVersion = 0;
                newPublishedAPI.Type = "IVI-C";

                iviConfigStore.PublishedAPIs.Add(newPublishedAPI);
            }

            SoftwareModule.PublishedAPIs.Add(newPublishedAPI);
            return newPublishedAPI;
        }

        public IviPublishedAPI CreateGlobalPublishedAPI()
        {
            IviPublishedAPI newPublishedAPI = new IviPublishedAPI();
            newPublishedAPI.Name = SuggestName(GetGlobalPublishedAPINameList(), "IviDriverAPI");
            newPublishedAPI.MajorVersion = 1;
            newPublishedAPI.MinorVersion = 0;
            newPublishedAPI.Type = "IVI-C";
            iviConfigStore.PublishedAPIs.Add(newPublishedAPI);
            return newPublishedAPI;
        }

        private IviPublishedAPI GetUnusedPublishedAPI(IviSoftwareModule SoftwareModule)
        {
            foreach (IviPublishedAPI publishedAPI in iviConfigStore.PublishedAPIs)
            {
                if (!ContainsPublishedAPI(SoftwareModule.PublishedAPIs, publishedAPI))
                {
                    return publishedAPI;
                }
            }
            return null;
        }

        private bool ContainsPublishedAPI(IviPublishedAPICollection PublishedAPIs, IviPublishedAPI PublishedAPI)
        {
            foreach (IviPublishedAPI currentPublishedAPI in PublishedAPIs)
            {
                if (currentPublishedAPI.Equals(PublishedAPI))
                    return true;
            }
            return false;
        }

        public ArrayList GetLogicalNames()
        {
            IviLogicalNameCollection logicalNames = iviConfigStore.LogicalNames;
            ArrayList Names = new ArrayList();
            foreach (IviLogicalName logicalName in logicalNames)
            {
                Names.Add(logicalName.Name);
            }
            return Names;
        }

        public ArrayList GetHardwareNames()
        {
            IviHardwareAssetCollection hardwareAssets = iviConfigStore.HardwareAssets;
            ArrayList Names = new ArrayList();
            foreach (IviHardwareAsset hardwareAsset in hardwareAssets)
            {
                Names.Add(hardwareAsset.Name);
            }
            return Names;
        }

        public ArrayList GetDriverNames()
        {
            IviDriverSessionCollection driverSessions = iviConfigStore.DriverSessions;
            ArrayList Names = new ArrayList();
            foreach (IviDriverSession driverSession in driverSessions)
            {
                Names.Add(driverSession.Name);
            }
            return Names;
        }

        public ArrayList GetVirtualNames(IviDriverSession DriverSession)
        {
            IviVirtualNameCollection virtualNames = DriverSession.VirtualNames;
            ArrayList Names = new ArrayList();
            foreach (IviVirtualName virtualName in virtualNames)
            {
                Names.Add(virtualName.Name);
            }
            return Names;
        }

        public ArrayList GetPhysicalNameList(IviSoftwareModule SoftwareModule)
        {
            IviPhysicalNameCollection PhysicalNames = SoftwareModule.PhysicalNames;
            ArrayList Names = new ArrayList();
            foreach (IviPhysicalName PhysicalName in PhysicalNames)
            {
                Names.Add(PhysicalName.Name);
            }
            return Names;
        }

        public ArrayList GetPublishedAPIList(IviSoftwareModule SoftwareModule)
        {
            IviPublishedAPICollection publishedAPIs = SoftwareModule.PublishedAPIs;
            ArrayList Names = new ArrayList();
            foreach (IviPublishedAPI publishedAPI in publishedAPIs)
            {
                if (!Names.Contains(publishedAPI.Name))
                {
                    Names.Add(publishedAPI.Name);
                }
            }
            return Names;
        }

        public ArrayList GetGlobalPublishedAPINameList()
        {
            IviPublishedAPICollection publishedAPIs = iviConfigStore.PublishedAPIs;
            ArrayList Names = new ArrayList();
            foreach (IviPublishedAPI publishedAPI in publishedAPIs)
            {
                if (!Names.Contains(publishedAPI.Name))
                {
                    Names.Add(publishedAPI.Name);
                }
            }
            return Names;
        }

        public ArrayList GetGlobalPublishedAPIVersionList(string Name)
        {
            IviPublishedAPICollection publishedAPIs = iviConfigStore.PublishedAPIs;
            ArrayList VersionList = new ArrayList();
            foreach (IviPublishedAPI publishedAPI in publishedAPIs)
            {
                if (Name.Equals(publishedAPI.Name) && !VersionList.Contains(String.Format("{0}.{1}", publishedAPI.MajorVersion, publishedAPI.MinorVersion)))
                {
                    VersionList.Add(String.Format("{0}.{1}", publishedAPI.MajorVersion, publishedAPI.MinorVersion));
                }
            }
            return VersionList;
        }

        public ArrayList GetGlobalPublishedAPITypeList(string Name, int MajorVersion, int MinorVersion)
        {
            IviPublishedAPICollection publishedAPIs = iviConfigStore.PublishedAPIs;
            ArrayList TypeList = new ArrayList();
            foreach (IviPublishedAPI publishedAPI in publishedAPIs)
            {
                if (Name.Equals(publishedAPI.Name) && publishedAPI.MajorVersion == MajorVersion && publishedAPI.MinorVersion == MinorVersion && !TypeList.Contains(publishedAPI.Type))
                {
                    TypeList.Add(publishedAPI.Type);
                }
            }
            return TypeList;
        }

        private bool InList(IviPublishedAPICollection PublishedAPIList, IviPublishedAPI PublishedAPI)
        {
            foreach (IviPublishedAPI Item in PublishedAPIList)
            {
                if(PublishedAPI.Equals(Item))
                {
                    return true;
                }
            }
            return false;
        }

        public ArrayList GetUnusedGlobalPublishedAPINameList(IviSoftwareModule SoftwareModule, object CurrentName = null)
        {
            IviPublishedAPICollection GlobalPublishedAPIs = iviConfigStore.PublishedAPIs;
            ArrayList Names = new ArrayList();
            foreach (IviPublishedAPI publishedAPI in GlobalPublishedAPIs)
            {
                if (!InList(SoftwareModule.PublishedAPIs, publishedAPI) && !Names.Contains(publishedAPI.Name))
                {
                    Names.Add(publishedAPI.Name);
                }
            }

            if (CurrentName != null && !Names.Contains(CurrentName))
            {
                Names.Add(CurrentName);
            }

            Names.Sort();

            return Names;
        }

        public ArrayList GetUnusedGlobalPublishedAPIVersionList(IviSoftwareModule SoftwareModule, string Name, object CurrentVersion = null)
        {
            IviPublishedAPICollection publishedAPIs = iviConfigStore.PublishedAPIs;
            ArrayList VersionList = new ArrayList();
            foreach (IviPublishedAPI publishedAPI in publishedAPIs)
            {
                if (!InList(SoftwareModule.PublishedAPIs, publishedAPI) && Name.Equals(publishedAPI.Name) && !VersionList.Contains(String.Format("{0}.{1}", publishedAPI.MajorVersion, publishedAPI.MinorVersion)))
                {
                    VersionList.Add(String.Format("{0}.{1}", publishedAPI.MajorVersion, publishedAPI.MinorVersion));
                }
            }

            if (CurrentVersion != null && !VersionList.Contains(CurrentVersion))
            {
                VersionList.Add(CurrentVersion);
            }

            VersionList.Sort();

            return VersionList;
        }

        public ArrayList GetUnusedGlobalPublishedAPITypeList(IviSoftwareModule SoftwareModule, string Name, int MajorVersion, int MinorVersion, object CurrentType = null)
        {
            IviPublishedAPICollection publishedAPIs = iviConfigStore.PublishedAPIs;
            ArrayList TypeList = new ArrayList();
            foreach (IviPublishedAPI publishedAPI in publishedAPIs)
            {
                if (!InList(SoftwareModule.PublishedAPIs, publishedAPI) && Name.Equals(publishedAPI.Name) && publishedAPI.MajorVersion == MajorVersion && publishedAPI.MinorVersion == MinorVersion && !TypeList.Contains(publishedAPI.Type))
                {
                    TypeList.Add(publishedAPI.Type);
                }
            }

            if (CurrentType != null && !TypeList.Contains(CurrentType))
            {
                TypeList.Add(CurrentType);
            }

            TypeList.Sort();
            return TypeList;
        }

        public ArrayList GetSoftwareModuleNames()
        {
            IviSoftwareModuleCollection softwareModules = iviConfigStore.SoftwareModules;
            ArrayList Names = new ArrayList();
            foreach (IviSoftwareModule softwareModule in softwareModules)
            {
                Names.Add(softwareModule.Name);
            }
            return Names;
        }

        public IviLogicalName GetLogicalName(string Name)
        {
            foreach (IviLogicalName logicalName in iviConfigStore.LogicalNames)
            {
                if (logicalName.Name.Equals(Name))
                {
                    return logicalName;
                }
            }
            return null;
        }

        public IviHardwareAsset GetHarwareAsset(string Name)
        {
            foreach (IviHardwareAsset hardwareAsset in iviConfigStore.HardwareAssets)
            {
                if (hardwareAsset.Name.Equals(Name))
                {
                    return hardwareAsset;
                }
            }
            return null;
        }

        public IviDriverSession GetDriverSession(string Name)
        {
            foreach (IviDriverSession driverSession in iviConfigStore.DriverSessions)
            {
                if (driverSession.Name.Equals(Name))
                {
                    return driverSession;
                }
            }
            return null;
        }

        public IviVirtualName GetVirtualName(IviDriverSession DriverSession, string Name)
        {
            foreach (IviVirtualName virtualName in DriverSession.VirtualNames)
            {
                if (virtualName.Name.Equals(Name))
                {
                    return virtualName;
                }
            }
            return null;
        }

        public IviSoftwareModule GetSoftwareModule(string Name)
        {
            foreach (IviSoftwareModule softwareModule in iviConfigStore.SoftwareModules)
            {
                if (softwareModule.Name.Equals(Name))
                {
                    return softwareModule;
                }
            }
            return null;
        }

        public IviPhysicalName GetPhysicalName(IviSoftwareModule SoftwareModule, string Name)
        {
            foreach (IviPhysicalName PhysicalName in SoftwareModule.PhysicalNames)
            {
                if (PhysicalName.Name.Equals(Name))
                {
                    return PhysicalName;
                }
            }
            return null;
        }

        public IviPublishedAPI GetGlobalPublishedAPI(string Name)
        {
            foreach (IviPublishedAPI publishedAPI in iviConfigStore.PublishedAPIs)
            {
                if (publishedAPI.Name.Equals(Name))
                {
                    return publishedAPI;
                }
            }
            return null;
        }

        public IviPublishedAPI GetGlobalPublishedAPI(string Name, int MajorVersion, int MinorVersion, string Type)
        {
            foreach (IviPublishedAPI publishedAPI in iviConfigStore.PublishedAPIs)
            {
                if (publishedAPI.Name.Equals(Name) && publishedAPI.MajorVersion == MajorVersion && publishedAPI.MinorVersion == MinorVersion && publishedAPI.Type.Equals(Type))
                {
                    return publishedAPI;
                }
            }
            return null;
        }

        public IviPublishedAPI GetPublishedAPI(IviSoftwareModule SoftwareModule, string Name, int MajorVersion, int MinorVersion, string Type)
        {
            foreach (IviPublishedAPI publishedAPI in SoftwareModule.PublishedAPIs)
            {
                if (publishedAPI.Name.Equals(Name) && publishedAPI.MajorVersion == MajorVersion && publishedAPI.MinorVersion == MinorVersion && publishedAPI.Type.Equals(Type))
                {
                    return publishedAPI;
                }
            }
            return null;
        }

        private string SuggestName(ArrayList existNames, string targetName)
        {
            string resultName = targetName;
            int index = 1;
            while (existNames.Contains(resultName))
            {
                resultName = String.Format("{0}{1}", targetName, index++);
            }

            return resultName;
        }
    }
}