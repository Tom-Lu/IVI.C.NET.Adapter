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
    public interface IviDCPwr : IviDriver
    {
        /*- IviDCPwrBase Functions -*/
        ViStatus ConfigureOutputEnabled(ViSession vi, ViString channelName, ViBoolean enabled);
        ViStatus ConfigureOutputRange(ViSession vi, ViString channelName, ViInt32 rangeType, ViReal64 range);
        ViStatus ConfigureCurrentLimit(ViSession vi, ViString channelName, ViInt32 behavior, ViReal64 limit);
        ViStatus ConfigureOVP(ViSession vi, ViString channelName, ViBoolean enabled, ViReal64 limit);
        ViStatus ConfigureVoltageLevel(ViSession vi, ViString channelName, ViReal64 level);
        ViStatus GetChannelName(ViSession vi, ViInt32 index, ViInt32 bufferSize, StringBuilder name);
        ViStatus QueryOutputState(ViSession vi, ViString channelName, ViInt32 outputState, ref ViBoolean inState);
        ViStatus QueryMaxCurrentLimit(ViSession vi, ViString channelName, ViReal64 voltageLevel, ref ViReal64 maxCurrentLimit);
        ViStatus QueryMaxVoltageLevel(ViSession vi, ViString channelName, ViReal64 currentLimit, ref ViReal64 maxVoltageLevel);
        ViStatus ResetOutputProtection(ViSession vi, ViString channelName);

        /*- IviDcPwrTrigger Functions -*/
        ViStatus ConfigureTriggerSource(ViSession vi, ViString channelName, ViInt32 source);
        ViStatus ConfigureTriggeredVoltageLevel(ViSession vi, ViString channelName, ViReal64 level);
        ViStatus ConfigureTriggeredCurrentLimit(ViSession vi, ViString channelName, ViReal64 limit);

        ViStatus Abort(ViSession vi);
        ViStatus Initiate(ViSession vi);

        /*- IviDCPwrSoftwareTrigger Functions -*/
        ViStatus SendSoftwareTrigger(ViSession vi);

        /*- IviDCPwrMeasure Functions -*/
        ViStatus Measure(ViSession vi, ViString channelName, ViInt32 measurementType, ref ViReal64 measurement);
    }
}
