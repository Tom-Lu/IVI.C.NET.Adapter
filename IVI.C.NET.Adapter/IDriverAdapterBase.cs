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
using System.Text;

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

namespace IVI.C.NET.Adapter
{
    internal interface IDriverAdapterBase
    {
        IntPtr Session { get; }
        Adapter.IviCInterop.IviDriver Interop { get; }

        void ViSessionStatusCheck(ViStatus Status);

        /*- Set, Get, and Check Attribute Functions -*/
        ViInt32 GetAttributeViInt32(ViAttr attributeId);
        ViInt32 GetAttributeViInt32(ViString channelName, ViAttr attributeId);
        void SetAttributeViInt32(ViAttr attributeId, ViInt32 value);
        void SetAttributeViInt32(ViString channelName, ViAttr attributeId, ViInt32 value);


        ViInt64 GetAttributeViInt64(ViAttr attributeId);
        ViInt64 GetAttributeViInt64(ViString channelName, ViAttr attributeId);
        void SetAttributeViInt64(ViAttr attributeId, ViInt64 value);
        void SetAttributeViInt64(ViString channelName, ViAttr attributeId, ViInt64 value);

        ViReal64 GetAttributeViReal64(ViAttr attributeId);
        ViReal64 GetAttributeViReal64(ViString channelName, ViAttr attributeId);
        void SetAttributeViReal64(ViAttr attributeId, ViReal64 value);
        void SetAttributeViReal64(ViString channelName, ViAttr attributeId, ViReal64 value);

        string GetAttributeViString(ViAttr attributeId);
        string GetAttributeViString(ViString channelName, ViAttr attributeId);
        void SetAttributeViString(ViAttr attributeId, ViString value);
        void SetAttributeViString(ViString channelName, ViAttr attributeId, ViString value);

        ViBoolean GetAttributeViBoolean(ViAttr attributeId);
        ViBoolean GetAttributeViBoolean(ViString channelName, ViAttr attributeId);
        void SetAttributeViBoolean(ViAttr attributeId, ViBoolean value);
        void SetAttributeViBoolean(ViString channelName, ViAttr attributeId, ViBoolean value);

        ViSession GetAttributeViSession(ViAttr attributeId);
        ViSession GetAttributeViSession(ViString channelName, ViAttr attributeId);
        void SetAttributeViSession(ViAttr attributeId, ViSession value);
        void SetAttributeViSession(ViString channelName, ViAttr attributeId, ViSession value);
    }
}
