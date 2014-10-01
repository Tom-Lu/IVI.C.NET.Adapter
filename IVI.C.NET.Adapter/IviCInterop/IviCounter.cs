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
    public interface IviCounter : IviDriver
    {
        /*- IviCounterBase Functions -*/
        ViStatus GetChannelName(ViSession vi, ViInt32 ChannelIndex, ViInt32 ChannelNameBufferSize, StringBuilder ChannelName);
        ViStatus ConfigureChannel(ViSession vi, ViString Channel, ViReal64 Impedance, ViInt32 Coupling, ViReal64 Attenuation);
        ViStatus ConfigureChannelLevel(ViSession vi, ViString Channel, ViReal64 TriggerLevel, ViReal64 Hysteresis);
        ViStatus ConfigureChannelSlope(ViSession vi, ViString Channel, ViInt32 Slope);
        ViStatus ConfigureChannelFilterEnabled(ViSession vi, ViString Channel, ViBoolean FilterEnabled);
        ViStatus ConfigureFrequency(ViSession vi, ViString Channel);
        ViStatus ConfigureFrequencyManual(ViSession vi, ViString Channel, ViReal64 Estimate, ViReal64 Resolution);
        ViStatus ConfigureFrequencyWithApertureTime(ViSession vi, ViString Channel, ViReal64 ApertureTime);
        ViStatus ConfigurePeriod(ViSession vi, ViString Channel, ViReal64 Estimate, ViReal64 Resolution);
        ViStatus ConfigurePeriodWithApertureTime(ViSession vi, ViString Channel, ViReal64 ApertureTime);
        ViStatus ConfigurePulseWidth(ViSession vi, ViString Channel, ViReal64 Estimate, ViReal64 Resolution);
        ViStatus ConfigureDutyCycle(ViSession vi, ViString Channel, ViReal64 FrequencyEstimate, ViReal64 Resolution);
        ViStatus ConfigureEdgeTime(ViSession vi, ViString Channel, ViReal64 Estimate, ViReal64 Resolution);
        ViStatus ConfigureEdgeTimeReferenceLevels(ViSession vi, ViString Channel, ViInt32 ReferenceType, ViReal64 Estimate, ViReal64 Resolution, ViReal64 HighReference, ViReal64 LowReference);
        ViStatus ConfigureFrequencyRatio(ViSession vi, ViString NumeratorChannel, ViString DenominatorChannel, ViReal64 NumeratorFrequencyEstimate, ViReal64 Estimate, ViReal64 Resolution);
        ViStatus ConfigureTimeInterval(ViSession vi, ViString StartChannel, ViString StopChannel, ViReal64 Estimate, ViReal64 Resolution);
        ViStatus ConfigurePhase(ViSession vi, ViString MeasurementChannel, ViString ReferenceChannel, ViReal64 FrequencyEstimate, ViReal64 Resolution);
        ViStatus ConfigureContinuousTotalize(ViSession vi, ViString Channel);
        ViStatus ConfigureGatedTotalize(ViSession vi, ViString Channel, ViString GateSource, ViInt32 GateSlope);
        ViStatus ConfigureTimedTotalize(ViSession vi, ViString Channel, ViReal64 GateTime);
        ViStatus ConfigureStartArm(ViSession vi, ViInt32 Type);
        ViStatus ConfigureExternalStartArm(ViSession vi, ViString Source, ViReal64 Level, ViInt32 Slope, ViReal64 Delay);
        ViStatus ConfigureStopArm(ViSession vi, ViInt32 Type);
        ViStatus ConfigureExternalStopArm(ViSession vi, ViString Source, ViReal64 Level, ViInt32 Slope, ViReal64 Delay);
        ViStatus ConfigureFilter(ViSession vi, ViString Channel, ViReal64 MinimumFrequency, ViReal64 MaximumFrequency);
        ViStatus ConfigureTimeIntervalStopHoldoff(ViSession vi, ViReal64 Time);
        ViStatus ConfigureVoltage(ViSession vi, ViString Channel, ViInt32 Function, ViReal64 Estimate, ViReal64 Resolution);
        ViStatus StartContinuousTotalize(ViSession vi);
        ViStatus StopContinuousTotalize(ViSession vi);
        ViStatus FetchContinuousTotalizeCount(ViSession vi, ref ViInt32 Measurement);
        ViStatus Read(ViSession vi, ViInt32 MaximumTime, ref ViReal64 Measurement);
        ViStatus Initiate(ViSession vi);
        ViStatus Abort(ViSession vi);
        ViStatus Fetch(ViSession vi, ref ViReal64 Measurement);
        ViStatus IsMeasurementComplete(ViSession vi, ref ViInt32 MeasurementStatus);
    }
}
