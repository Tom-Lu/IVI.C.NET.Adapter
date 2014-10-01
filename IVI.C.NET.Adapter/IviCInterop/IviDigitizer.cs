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
    public interface IviDigitizer : IviDriver
    {
        /*- IviDigitizer Fundamental Capabilities -*/
        ViStatus Abort(ViSession vi);

        ViStatus ConfigureAcquisition(ViSession vi,
                                      ViInt64 NumRecords,
                                      ViInt64 RecordSize,
                                      ViReal64 SampleRate);

        ViStatus ConfigureActiveTriggerSource(ViSession vi,
                                              ViString TriggerSource);


        ViStatus ConfigureChannel(ViSession vi,
                                  ViString ChannelName,
                                  ViReal64 Range,
                                  ViReal64 Offset,
                                  ViInt32 Coupling,
                                  ViBoolean Enabled);

        ViStatus ConfigureEdgeTriggerSource(ViSession vi,
                                            ViString Source,
                                            ViReal64 Level,
                                            ViInt32 Slope);

        ViStatus FetchWaveformInt8(ViSession vi,
                                   ViString ChannelName,
                                   ViInt64 WaveformArraySize,
                                   IntPtr WaveformArray,
                                   ref ViInt64 ActualPoints,
                                   ref ViInt64 FirstValidPoint,
                                   ref ViReal64 InitialXOffset,
                                   ref ViReal64 InitialXTimeSeconds,
                                   ref ViReal64 InitialXTimeFraction,
                                   ref ViReal64 XIncrement,
                                   ref ViReal64 ScaleFactor,
                                   ref ViReal64 ScaleOffset);

        ViStatus FetchWaveformInt16(ViSession vi,
                                    ViString ChannelName,
                                    ViInt64 WaveformArraySize,
                                    IntPtr WaveformArray,
                                    ref ViInt64 ActualPoints,
                                    ref ViInt64 FirstValidPoint,
                                    ref ViReal64 InitialXOffset,
                                    ref ViReal64 InitialXTimeSeconds,
                                    ref ViReal64 InitialXTimeFraction,
                                    ref ViReal64 XIncrement,
                                    ref ViReal64 ScaleFactor,
                                    ref ViReal64 ScaleOffset);

        ViStatus FetchWaveformInt32(ViSession vi,
                                    ViString ChannelName,
                                    ViInt64 WaveformArraySize,
                                    IntPtr WaveformArray,
                                    ref ViInt64 ActualPoints,
                                    ref ViInt64 FirstValidPoint,
                                    ref ViReal64 InitialXOffset,
                                    ref ViReal64 InitialXTimeSeconds,
                                    ref ViReal64 InitialXTimeFraction,
                                    ref ViReal64 XIncrement,
                                    ref ViReal64 ScaleFactor,
                                    ref ViReal64 ScaleOffset);

        ViStatus FetchWaveformReal64(ViSession vi,
                                     ViString ChannelName,
                                     ViInt64 WaveformArraySize,
                                     IntPtr WaveformArray,
                                     ref ViInt64 ActualPoints,
                                     ref ViInt64 FirstValidPoint,
                                     ref ViReal64 InitialXOffset,
                                     ref ViReal64 InitialXTimeSeconds,
                                     ref ViReal64 InitialXTimeFraction,
                                     ref ViReal64 XIncrement);

        ViStatus GetChannelName(ViSession vi,
                                ViInt32 ChannelIndex,
                                ViInt32 ChannelNameBufferSize,
                                StringBuilder ChannelName);

        ViStatus GetTriggerSourceName(ViSession vi,
                                      ViInt32 SourceIndex,
                                      ViInt32 SourceNameBufferSize,
                                      StringBuilder SourceName);

        ViStatus InitiateAcquisition(ViSession vi);

        ViStatus IsIdle(ViSession vi,
                        ref ViInt32 Status);

        ViStatus IsMeasuring(ViSession vi,
                             ref ViInt32 Status);

        ViStatus IsWaitingForArm(ViSession vi,
                                 ref ViInt32 Status);

        ViStatus IsWaitingForTrigger(ViSession vi,
                                     ref ViInt32 Status);

        ViStatus QueryMinWaveformMemory(ViSession vi,
                                        ViInt32 DataWidth,
                                        ViInt64 NumRecords,
                                        ViInt64 OffsetWithinRecord,
                                        ViInt64 NumPointsPerRecord,
                                        ref ViInt64 NumSamples);

        ViStatus ReadWaveformInt8(ViSession vi,
                                  ViString ChannelName,
                                  ViInt32 MaxTimeMilliseconds,
                                  ViInt64 WaveformArraySize,
                                  IntPtr WaveformArray,
                                  ref ViInt64 ActualPoints,
                                  ref ViInt64 FirstValidPoint,
                                  ref ViReal64 InitialXOffset,
                                  ref ViReal64 InitialXTimeSeconds,
                                  ref ViReal64 InitialXTimeFraction,
                                  ref ViReal64 XIncrement,
                                  ref ViReal64 ScaleFactor,
                                  ref ViReal64 ScaleOffset);

        ViStatus ReadWaveformInt16(ViSession vi,
                                   ViString ChannelName,
                                   ViInt32 MaxTimeMilliseconds,
                                   ViInt64 WaveformArraySize,
                                   IntPtr WaveformArray,
                                   ref ViInt64 ActualPoints,
                                   ref ViInt64 FirstValidPoint,
                                   ref ViReal64 InitialXOffset,
                                   ref ViReal64 InitialXTimeSeconds,
                                   ref ViReal64 InitialXTimeFraction,
                                   ref ViReal64 XIncrement,
                                   ref ViReal64 ScaleFactor,
                                   ref ViReal64 ScaleOffset);

        ViStatus ReadWaveformInt32(ViSession vi,
                                   ViString ChannelName,
                                   ViInt32 MaxTimeMilliseconds,
                                   ViInt64 WaveformArraySize,
                                   IntPtr WaveformArray,
                                   ref ViInt64 ActualPoints,
                                   ref ViInt64 FirstValidPoint,
                                   ref ViReal64 InitialXOffset,
                                   ref ViReal64 InitialXTimeSeconds,
                                   ref ViReal64 InitialXTimeFraction,
                                   ref ViReal64 XIncrement,
                                   ref ViReal64 ScaleFactor,
                                   ref ViReal64 ScaleOffset);

        ViStatus ReadWaveformReal64(ViSession vi,
                                    ViString ChannelName,
                                    ViInt32 MaxTimeMilliseconds,
                                    ViInt64 WaveformArraySize,
                                    IntPtr WaveformArray,
                                    ref ViInt64 ActualPoints,
                                    ref ViInt64 FirstValidPoint,
                                    ref ViReal64 InitialXOffset,
                                    ref ViReal64 InitialXTimeSeconds,
                                    ref ViReal64 InitialXTimeFraction,
                                    ref ViReal64 XIncrement);

        ViStatus WaitForAcquisitionComplete(ViSession vi,
                                            ViInt32 MaxTimeMilliseconds);

        /*- IviDigitizerMultiRecordAcquisition Extension Group -*/
        ViStatus FetchMultiRecordWaveformInt8(ViSession vi,
                                              ViString ChannelName,
                                              ViInt64 FirstRecord,
                                              ViInt64 NumRecords,
                                              ViInt64 OffsetWithinRecord,
                                              ViInt64 NumPointsPerRecord,
                                              ViInt64 WaveformArraySize,
                                              IntPtr WaveformArray,
                                              ref ViInt64 ActualRecords,
                                              IntPtr ActualPoints,
                                              IntPtr FirstValidPoint,
                                              IntPtr InitialXOffset,
                                              IntPtr InitialXTimeSeconds,
                                              IntPtr InitialXTimeFraction,
                                              ref ViReal64 XIncrement,
                                              ref ViReal64 ScaleFactor,
                                              ref ViReal64 ScaleOffset);

        ViStatus FetchMultiRecordWaveformInt16(ViSession vi,
                                               ViString ChannelName,
                                               ViInt64 FirstRecord,
                                               ViInt64 NumRecords,
                                               ViInt64 OffsetWithinRecord,
                                               ViInt64 NumPointsPerRecord,
                                               ViInt64 WaveformArraySize,
                                               IntPtr WaveformArray,
                                               ref ViInt64 ActualRecords,
                                               IntPtr ActualPoints,
                                               IntPtr FirstValidPoint,
                                               IntPtr InitialXOffset,
                                               IntPtr InitialXTimeSeconds,
                                               IntPtr InitialXTimeFraction,
                                               ref ViReal64 XIncrement,
                                               ref ViReal64 ScaleFactor,
                                               ref ViReal64 ScaleOffset);

        ViStatus FetchMultiRecordWaveformInt32(ViSession vi,
                                               ViString ChannelName,
                                               ViInt64 FirstRecord,
                                               ViInt64 NumRecords,
                                               ViInt64 OffsetWithinRecord,
                                               ViInt64 NumPointsPerRecord,
                                               ViInt64 WaveformArraySize,
                                               IntPtr WaveformArray,
                                               ref ViInt64 ActualRecords,
                                               IntPtr ActualPoints,
                                               IntPtr FirstValidPoint,
                                               IntPtr InitialXOffset,
                                               IntPtr InitialXTimeSeconds,
                                               IntPtr InitialXTimeFraction,
                                               ref ViReal64 XIncrement,
                                               ref ViReal64 ScaleFactor,
                                               ref ViReal64 ScaleOffset);

        ViStatus FetchMultiRecordWaveformReal64(ViSession vi,
                                                ViString ChannelName,
                                                ViInt64 FirstRecord,
                                                ViInt64 NumRecords,
                                                ViInt64 OffsetWithinRecord,
                                                ViInt64 NumPointsPerRecord,
                                                ViInt64 WaveformArraySize,
                                                IntPtr WaveformArray,
                                                ref ViInt64 ActualRecords,
                                                IntPtr ActualPoints,
                                                IntPtr FirstValidPoint,
                                                IntPtr InitialXOffset,
                                                IntPtr InitialXTimeSeconds,
                                                IntPtr InitialXTimeFraction,
                                                ref ViReal64 XIncrement);

        /*- IviDigitizerBoardTemperature Extension Group -*/
        ViStatus ConfigureTemperatureUnits(ViSession vi,
                                           ViInt32 Units);

        ViStatus QueryBoardTemperature(ViSession vi,
                                       ref ViReal64 Temperature);

        /*- IviDigitizerChannelFilter Extension Group -*/
        ViStatus ConfigureInputFilter(ViSession vi,
                                      ViString ChannelName,
                                      ViReal64 MinFrequency,
                                      ViReal64 MaxFrequency);

        /*- IviDigitizerChannelTemperature Extension Group -*/
        ViStatus QueryChannelTemperature(ViSession vi,
                                         ViString ChannelName,
                                         ref ViReal64 Temperature);

        /*- IviDigitizerTimeInterleavedChannels Extension Group -*/
        ViStatus ConfigureTimeInterleavedChannelList(ViSession vi,
                                                     ViString ChannelName,
                                                     ViString ChannelList);

        /*- IviDigitizerDataInterleavedChannels Extension Group -*/
        ViStatus ConfigureDataInterleavedChannelList(ViSession vi,
                                                     ViString ChannelName,
                                                     ViString ChannelList);

        /*- IviDigitizerReferenceOscillator Extension Group -*/
        ViStatus ConfigureReferenceOscillator(ViSession vi,
                                              ViInt32 Source,
                                              ViReal64 Frequency);

        ViStatus ConfigureReferenceOscillatorOutputEnabled(ViSession vi,
                                                           ViBoolean Enabled);

        /*- IviDigitizerSampleClock Extension Group -*/
        ViStatus ConfigureSampleClock(ViSession vi,
                                      ViInt32 Source,
                                      ViReal64 Frequency,
                                      ViReal64 Divider);

        ViStatus ConfigureSampleClockOutputEnabled(ViSession vi,
                                                   ViBoolean Enabled);

        /*- IviDigitizerSampleMode Extension Group -*/
        ViStatus ConfigureSampleMode(ViSession vi,
                                     ViInt32 SampleMode);

        /*- IviDigitizerSelfCalibration Extension Group -*/
        ViStatus SelfCalibrate(ViSession vi);

        /*- IviDigitizerDownconversion Extension Group -*/
        ViStatus ConfigureDownconversion(ViSession vi,
                                         ViString ChannelName,
                                         ViBoolean Enabled,
                                         ViReal64 CenterFrequency);

        /*- IviDigitizerArm Extension Group -*/
        ViStatus ConfigureEdgeArmSource(ViSession vi,
                                        ViString Source,
                                        ViReal64 Level,
                                        ViInt32 Slope);

        ViStatus GetArmSourceName(ViSession vi,
                                  ViInt32 SourceIndex,
                                  ViInt32 SourceNameBufferSize,
                                  StringBuilder SourceName);

        /*- IviDigitizerMultiArm Extension Group -*/
        ViStatus ConfigureMultiArm(ViSession vi,
                                   ViString SourceList,
                                   ViInt32 Operator);

        /*- IviDigitizerGlitchArm Extension Group -*/
        ViStatus ConfigureGlitchArmSource(ViSession vi,
                                          ViString Source,
                                          ViReal64 Level,
                                          ViReal64 Width,
                                          ViInt32 Polarity,
                                          ViInt32 Condition);

        /*- IviDigitizerRuntArm Extension Group -*/
        ViStatus ConfigureRuntArmSource(ViSession vi,
                                        ViString Source,
                                        ViReal64 ThresholdLow,
                                        ViReal64 ThresholdHigh,
                                        ViInt32 Polarity);

        /*- IviDigitizerSoftwareArm Extension Group -*/
        ViStatus SendSoftwareArm(ViSession vi);

        /*- IviDigitizerTVArm Extension -*/
        ViStatus ConfigureTVArmSource(ViSession vi,
                                      ViString Source,
                                      ViInt32 SignalFormat,
                                      ViInt32 Event,
                                      ViInt32 Polarity);

        /*- IviDigitizerWidthArm Extension Group -*/
        ViStatus ConfigureWidthArmSource(ViSession vi,
                                         ViString Source,
                                         ViReal64 Level,
                                         ViReal64 ThresholdLow,
                                         ViReal64 ThresholdHigh,
                                         ViInt32 Polarity,
                                         ViInt32 Condition);

        /*- IviDigitizerWindowArm Extension Group -*/
        ViStatus ConfigureWindowArmSource(ViSession vi,
                                          ViString Source,
                                          ViReal64 ThresholdLow,
                                          ViReal64 ThresholdHigh,
                                          ViInt32 Condition);

        /*- IviDigitizerTriggerModifier Extension Group -*/
        ViStatus ConfigureTriggerModifier(ViSession vi,
                                          ViInt32 TriggerModifier);

        /*- IviDigitizerMultiTrigger Extension Group -*/
        ViStatus ConfigureMultiTrigger(ViSession vi,
                                       ViString SourceList,
                                       ViInt32 Operator);

        /*- IviDigitizerPretriggerSamples Extension Group -*/
        ViStatus ConfigurePretriggerSamples(ViSession vi,
                                            ViInt64 PretriggerSamples);

        /*- IviDigitizerTriggerHoldoff Extension Group -*/
        ViStatus ConfigureTriggerHoldoff(ViSession vi,
                                         ViReal64 TriggerHoldoff);

        /*- IIviDigitizerGlitchTrigger Extension Group -*/
        ViStatus ConfigureGlitchTriggerSource(ViSession vi,
                                              ViString Source,
                                              ViReal64 Level,
                                              ViReal64 Width,
                                              ViInt32 Polarity,
                                              ViInt32 Condition);

        /*- IviDigitizerRuntTrigger Extension Group -*/
        ViStatus ConfigureRuntTriggerSource(ViSession vi,
                                            ViString Source,
                                            ViReal64 ThresholdLow,
                                            ViReal64 ThresholdHigh,
                                            ViInt32 Polarity);

        /*- IviDigitizerSoftwareTrigger Extension Group -*/
        ViStatus SendSoftwareTrigger(ViSession vi);

        /*- IviDigitizerTVTrigger Extension Group -*/
        ViStatus ConfigureTVTriggerSource(ViSession vi,
                                          ViString Source,
                                          ViInt32 SignalFormat,
                                          ViInt32 Event,
                                          ViInt32 Polarity);

        /*- IviDigitizerWidthTrigger Extension Group -*/
        ViStatus ConfigureWidthTriggerSource(ViSession vi,
                                             ViString Source,
                                             ViReal64 Level,
                                             ViReal64 ThresholdLow,
                                             ViReal64 ThresholdHigh,
                                             ViInt32 Polarity,
                                             ViInt32 Condition);

        /*- IviDigitizerWindowTrigger Extension Group -*/
        ViStatus ConfigureWindowTriggerSource(ViSession vi,
                                              ViString Source,
                                              ViReal64 ThresholdLow,
                                              ViReal64 ThresholdHigh,
                                              ViInt32 Condition);
    }
}
