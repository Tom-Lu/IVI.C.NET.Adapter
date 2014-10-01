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

namespace IVI.C.NET.Adapter.IviCInterop
{
    public interface IviDriver
    {
        ViStatus init(ViString logicalName, ViBoolean idQuery, ViBoolean resetDevice, ref ViSession vi);
        ViStatus close(ViSession vi);
        ViStatus reset(ViSession vi);

        ViStatus self_test(ViSession vi, ref ViInt16 selfTestResult, StringBuilder selfTestMessage);
        ViStatus error_query(ViSession vi, ref ViInt32 errorCode, StringBuilder errorMessage);
        ViStatus error_message(ViSession vi, ViStatus statusCode, StringBuilder message);
        ViStatus revision_query(ViSession vi, StringBuilder driverRev, StringBuilder instrRev);

        /*- Utility Functions -*/
        ViStatus InvalidateAllAttributes(ViSession vi);
        ViStatus ResetWithDefaults(ViSession vi);
        ViStatus Disable(ViSession vi);

        /*- Required IVI Functions -*/
        ViStatus InitWithOptions(ViString logicalName, ViBoolean IDQuery, ViBoolean resetDevice, ViString optionString, ref ViSession vi);

        /*- Set, Get, and Check Attribute Functions -*/
        ViStatus GetAttributeViInt32(ViSession vi, ViString channelName, ViAttr attributeId, ref ViInt32 value);
        ViStatus SetAttributeViInt32(ViSession vi, ViString channelName, ViAttr attributeId, ViInt32 value);
        ViStatus CheckAttributeViInt32(ViSession vi, ViString channelName, ViAttr attributeId, ViInt32 value);

        ViStatus GetAttributeViInt64(ViSession vi, ViString channelName, ViAttr attributeId, ref ViInt64 value);
        ViStatus SetAttributeViInt64(ViSession vi, ViString channelName, ViAttr attributeId, ViInt64 value);
        ViStatus CheckAttributeViInt64(ViSession vi, ViString channelName, ViAttr attributeId, ViInt64 value);

        ViStatus GetAttributeViReal64(ViSession vi, ViString channelName, ViAttr attributeId, ref ViReal64 value);
        ViStatus SetAttributeViReal64(ViSession vi, ViString channelName, ViAttr attributeId, ViReal64 value);
        ViStatus CheckAttributeViReal64(ViSession vi, ViString channelName, ViAttr attributeId, ViReal64 value);

        ViStatus GetAttributeViString(ViSession vi, ViString channelName, ViAttr attributeId, ViInt32 bufferSize, StringBuilder value);
        // ViStatus GetAttributeViString(ViSession vi, ViString channelName, ViAttr attributeId, ViInt32 bufferSize, ref ViString value);
        ViStatus SetAttributeViString(ViSession vi, ViString channelName, ViAttr attributeId, ViString value);
        ViStatus CheckAttributeViString(ViSession vi, ViString channelName, ViAttr attributeId, ViString value);

        ViStatus GetAttributeViBoolean(ViSession vi, ViString channelName, ViAttr attributeId, ref ViBoolean value);
        ViStatus SetAttributeViBoolean(ViSession vi, ViString channelName, ViAttr attributeId, ViBoolean value);
        ViStatus CheckAttributeViBoolean(ViSession vi, ViString channelName, ViAttr attributeId, ViBoolean value);

        ViStatus GetAttributeViSession(ViSession vi, ViString channelName, ViAttr attributeId, ref ViSession value);
        ViStatus SetAttributeViSession(ViSession vi, ViString channelName, ViAttr attributeId, ViSession value);
        ViStatus CheckAttributeViSession(ViSession vi, ViString channelName, ViAttr attributeId, ViSession value);

        /*- Lock and Unlock Functions -*/
        ViStatus LockSession(ViSession vi, ref ViBoolean callerHasLock);
        ViStatus UnlockSession(ViSession vi, ref ViBoolean callerHasLock);
        /*- Error Information Functions -*/
        ViStatus GetError(ViSession vi, ref ViStatus errorCode, ViInt32 bufferSize, ref ViString description);
        ViStatus ClearError(ViSession vi);

        /*- Interchangeability Checking Functions -*/
        ViStatus GetNextInterchangeWarning(ViSession vi, ViInt32 bufferSize, ref ViString warning);
        ViStatus ClearInterchangeWarnings(ViSession vi);
        ViStatus ResetInterchangeCheck(ViSession vi);
        ViStatus GetNextCoercionRecord(ViSession vi, ViInt32 bufferSize, ref ViInt8[] record);
        ViStatus GetSpecificDriverCHandle(ViSession vi, ref ViSession specificDriverCHandle);
        ViStatus GetSpecificDriverIUnknownPtr(ViSession vi, ref object specificDriverIUnknownPtr);
    }
}
