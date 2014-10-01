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
using Ivi.Driver;
using IVI.C.NET.Adapter.IviCInterop;

namespace IVI.C.NET.Adapter
{
    internal class DriverIdentity : IIviDriverIdentity
    {
        private IDriverAdapterBase Adapter;
        private string TargetSoftwareModuleName;
        public DriverIdentity(IDriverAdapterBase Adapter, string TargetSoftwareModuleName)
        {
            this.Adapter = Adapter;
            this.TargetSoftwareModuleName = TargetSoftwareModuleName;
        }

        public string[] GetGroupCapabilities()
        {
            return Adapter.GetAttributeViString(IviDriverAttribute.IVI_ATTR_GROUP_CAPABILITIES).Split(',');
        }

        public string[] GetSupportedInstrumentModels()
        {
            return Adapter.GetAttributeViString(IviDriverAttribute.IVI_ATTR_SUPPORTED_INSTRUMENT_MODELS).Split(',');  
        }

        public string Identifier
        {
            //TODO: need implement this function
            get { return TargetSoftwareModuleName; }
        }

        public string InstrumentFirmwareRevision
        {
            get 
            {
                return Adapter.GetAttributeViString(IviDriverAttribute.IVI_ATTR_INSTRUMENT_FIRMWARE_REVISION);               
            }
        }

        public string InstrumentManufacturer
        {
            get
            {
                return Adapter.GetAttributeViString(IviDriverAttribute.IVI_ATTR_INSTRUMENT_MANUFACTURER);    
            }
        }

        public string InstrumentModel
        {
            get
            {
                return Adapter.GetAttributeViString(IviDriverAttribute.IVI_ATTR_INSTRUMENT_MODEL);
            }
        }

        public int SpecificationMajorVersion
        {
            get
            {
                return Adapter.GetAttributeViInt32(IviDriverAttribute.IVI_ATTR_SPECIFIC_DRIVER_CLASS_SPEC_MAJOR_VERSION);
            }
        }

        public int SpecificationMinorVersion
        {
            get
            {
                return Adapter.GetAttributeViInt32(IviDriverAttribute.IVI_ATTR_CLASS_DRIVER_CLASS_SPEC_MINOR_VERSION);
            }
        }

        public string Description
        {
            get
            {
                return Adapter.GetAttributeViString(IviDriverAttribute.IVI_ATTR_SPECIFIC_DRIVER_DESCRIPTION);  
            }
        }

        public string Revision
        {
            get
            {
                return Adapter.GetAttributeViString(IviDriverAttribute.IVI_ATTR_SPECIFIC_DRIVER_REVISION); 
            }
        }

        public string Vendor
        {
            get
            {
                return Adapter.GetAttributeViString(IviDriverAttribute.IVI_ATTR_SPECIFIC_DRIVER_VENDOR); 
            }
        }
    }
}