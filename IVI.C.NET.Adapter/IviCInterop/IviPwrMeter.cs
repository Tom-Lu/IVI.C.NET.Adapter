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
    public interface IviPwrMeter : IviDriver
    {
        /*- IviPwrMeter Fundamental Capabilities -*/
        ViStatus Abort(ViSession vi);

        ViStatus ConfigureAveragingAutoEnabled(ViSession vi,
                                                ViString Channel,
                                                ViBoolean AveragingAutoEnabled);

        ViStatus ConfigureCorrectionFrequency(ViSession vi,
                                               ViString Channel,
                                               ViReal64 CorrectionFrequency);

        ViStatus ConfigureMeasurement(ViSession vi,
                                       ViInt32 Operator,
                                       ViString Operand1,
                                       ViString Operand2);

        ViStatus ConfigureOffset(ViSession vi,
                                  ViString Channel,
                                  ViReal64 Offset);

        ViStatus ConfigureRangeAutoEnabled(ViSession vi,
                                            ViString Channel,
                                            ViBoolean RangeAutoEnabled);

        ViStatus ConfigureUnits(ViSession vi,
                                 ViInt32 Units);

        ViStatus Fetch(ViSession vi,
                        ref ViReal64 Result);

        ViStatus GetChannelName(ViSession vi,
                                 ViInt32 ChannelIndex,
                                 ViInt32 ChannelNameBufferSize,
                                 StringBuilder ChannelName);

        ViStatus Initiate(ViSession vi);

        ViStatus IsMeasurementComplete(ViSession vi,
                                        ref ViInt32 MeasurementStatus);

        ViStatus QueryResultRangeType(ViSession vi,
                                       ViReal64 MeasurementValue,
                                       ref ViInt32 RangeType);

        ViStatus Read(ViSession vi,
                       ViInt32 MaxTime,
                       ref ViReal64 Result);


        /*- IviPwrMeterChannelAcquisition Extension Group -*/
        ViStatus ConfigureChannelEnabled(ViSession vi,
                                          ViString Channel,
                                          ViBoolean ChannelEnabled);

        ViStatus FetchChannel(ViSession vi,
                               ViString Channel,
                               ref ViReal64 Result);

        ViStatus ReadChannel(ViSession vi,
                              ViString Channel,
                              ViInt32 MaxTime,
                              ref ViReal64 Result);


        /*- IviPwrMeterManualRange Extension Group -*/
        ViStatus ConfigureRange(ViSession vi,
                                 ViString Channel,
                                 ViReal64 RangeLower,
                                 ViReal64 RangeUpper);


        /*- IviPwrMeterTriggerSource Extension Group -*/
        ViStatus ConfigureTriggerSource(ViSession vi,
                                         ViInt32 TriggerSource);


        /*- IviPwrMeterInternalTrigger Extension Group -*/
        ViStatus ConfigureInternalTrigger(ViSession vi,
                                           ViString EventSource,
                                           ViInt32 Slope);

        ViStatus ConfigureInternalTriggerLevel(ViSession vi,
                                                ViReal64 TriggerLevel);


        /*- IviPwrMeterSoftwareTrigger Extension Group -*/
        ViStatus SendSoftwareTrigger(ViSession vi);


        /*- IviPwrMeterAveragingCount Extension Group -*/
        ViStatus ConfigureAveragingCount(ViSession vi,
                                          ViString Channel,
                                          ViInt32 Count);


        /*- IviPwrMeterZeroCorrection Extension Group -*/
        ViStatus IsZeroComplete(ViSession vi,
                                 ref ViInt32 ZeroStatus);

        ViStatus Zero(ViSession vi,
                       ViString Channel);

        ViStatus ZeroAllChannels(ViSession vi);


        /*- IviPwrMeterDutyCycleCorrection Extension Group -*/
        ViStatus ConfigureDutyCycleCorrection(ViSession vi,
                                               ViString Channel,
                                               ViBoolean CorrectionEnabled,
                                               ViReal64 Correction);


        /*- IviPwrMeterCalibration Extension Group -*/
        ViStatus Calibrate(ViSession vi,
                            ViString Channel);

        ViStatus IsCalibrationComplete(ViSession vi,
                                        ref ViInt32 CalibrationStatus);


        /*- IviPwrMeterReferenceOscillator Extension Group -*/
        ViStatus ConfigureRefOscillator(ViSession vi,
                                         ViReal64 Frequency,
                                         ViReal64 Level);

        ViStatus ConfigureRefOscillatorEnabled(ViSession vi,
                                                ViBoolean RefOscillatorEnabled);
    }
}
