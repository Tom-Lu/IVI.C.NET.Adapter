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
    internal class DriverOperation : IIviDriverOperation
    {
        private IDriverAdapterBase Adapter;
        public DriverOperation(IDriverAdapterBase Adapter)
        {
            this.Adapter = Adapter;
        }

        public bool Cache
        {
            get
            {
                return Adapter.GetAttributeViBoolean(IviDriverAttribute.IVI_ATTR_CACHE);
            }
            set
            {
                Adapter.SetAttributeViBoolean(IviDriverAttribute.IVI_ATTR_CACHE, value);
            }
        }

#pragma warning disable
        public event EventHandler<CoercionEventArgs> Coercion;
#pragma warning restore

        public string DriverSetup
        {
            get
            {
                return Adapter.GetAttributeViString(IviDriverAttribute.IVI_ATTR_DRIVER_SETUP);
            }
        }

        public string IOResourceDescriptor
        {
            get
            {
                return Adapter.GetAttributeViString(IviDriverAttribute.IVI_ATTR_IO_RESOURCE_DESCRIPTOR);
            }
        }

        #pragma warning disable
        public event EventHandler<InterchangeCheckWarningEventArgs> InterchangeCheckWarning;
        #pragma warning restore

        public void InvalidateAllAttributes()
        {
            Adapter.ViSessionStatusCheck(Adapter.Interop.InvalidateAllAttributes(Adapter.Session));
        }

        public string LogicalName
        {
            get
            {
                return Adapter.GetAttributeViString(IviDriverAttribute.IVI_ATTR_LOGICAL_NAME);
            }
        }

        public bool QueryInstrumentStatus
        {
            get
            {
                return Adapter.GetAttributeViBoolean(IviDriverAttribute.IVI_ATTR_QUERY_INSTRUMENT_STATUS);
            }
            set
            {
                Adapter.SetAttributeViBoolean(IviDriverAttribute.IVI_ATTR_QUERY_INSTRUMENT_STATUS, value);
            }
        }

        public bool RangeCheck
        {
            get
            {
                return Adapter.GetAttributeViBoolean(IviDriverAttribute.IVI_ATTR_RANGE_CHECK);
            }
            set
            {
                Adapter.SetAttributeViBoolean(IviDriverAttribute.IVI_ATTR_RANGE_CHECK, value);
            }
        }

        public void ResetInterchangeCheck()
        {
            Adapter.ViSessionStatusCheck(Adapter.Interop.ResetInterchangeCheck(Adapter.Session));
        }

        public bool Simulate
        {
            get
            {
                return Adapter.GetAttributeViBoolean(IviDriverAttribute.IVI_ATTR_SIMULATE);
            }
            set
            {
                Adapter.SetAttributeViBoolean(IviDriverAttribute.IVI_ATTR_SIMULATE, value);
            }
        }

        #pragma warning disable
        public event EventHandler<WarningEventArgs> Warning;
        #pragma warning restore
    }
}