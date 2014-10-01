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
    public interface IviSpecAn : IviDriver
    {
        /*- IviSpecAn Fundamental Capabilities -*/
        ViStatus Abort(ViSession vi);

        ViStatus AcquisitionStatus(ViSession vi,
                                    ref ViInt32 status);

        ViStatus ConfigureAcquisition(ViSession vi,
                                       ViBoolean sweepModeContinuous,
                                       ViInt32 numberOfSweeps,
                                       ViBoolean detectorTypeAuto,
                                       ViInt32 detectorType,
                                       ViInt32 verticalScale);

        ViStatus ConfigureFrequencyCenterSpan(ViSession vi,
                                               ViReal64 CenterFrequency,
                                               ViReal64 Span);

        ViStatus ConfigureFrequencyOffset(ViSession vi,
                                           ViReal64 FrequencyOffset);

        ViStatus ConfigureFrequencyStartStop(ViSession vi,
                                              ViReal64 StartFrequency,
                                              ViReal64 StopFrequency);

        ViStatus ConfigureLevel(ViSession vi,
                                 ViInt32 AmplitudeUnits,
                                 ViReal64 InputImpedance,
                                 ViReal64 ReferenceLevel,
                                 ViReal64 ReferenceLevelOffset,
                                 ViBoolean AttenuationAuto,
                                 ViReal64 Attenuation);

        ViStatus ConfigureSweepCoupling(ViSession vi,
                                         ViBoolean ResolutionBandwidtAuto,
                                         ViReal64 ResolutionBandwidth,
                                         ViBoolean VideoBandwidthAuto,
                                         ViReal64 VideoBandwidth,
                                         ViBoolean SweepTimeAuto,
                                         ViReal64 SweepTime);

        ViStatus ConfigureTraceType(ViSession vi,
                                     ViString TraceName,
                                     ViInt32 TraceType);

        ViStatus FetchYTrace(ViSession vi,
                              ViString TraceName,
                              ViInt32 ArrayLength,
                              ref ViInt32 ActualPoints,
                              IntPtr Amplitude);

        ViStatus GetTraceName(ViSession vi,
                               ViInt32 Index,
                               ViInt32 NameBufferSize,
                               StringBuilder Name);

        ViStatus Initiate(ViSession vi);

        ViStatus QueryTraceSize(ViSession vi,
                                 ViString TraceName,
                                 ref ViInt32 TraceSize);

        ViStatus ReadYTrace(ViSession vi,
                             ViString TraceName,
                             ViInt32 MaxTime,
                             ViInt32 ArrayLength,
                             ref ViInt32 ActualPoints,
                             IntPtr Amplitude);


        /*- IviSpecAnMultitrace Extension Group -*/
        ViStatus AddTraces(ViSession vi,
                            ViString DestinationTrace,
                            ViString Trace1,
                            ViString Trace2);

        ViStatus CopyTrace(ViSession vi,
                            ViString DestinationTrace,
                            ViString SourceTrace);

        ViStatus ExchangeTraces(ViSession vi,
                                 ViString Trace1,
                                 ViString Trace2);

        ViStatus SubtractTraces(ViSession vi,
                                 ViString DestinationTrace,
                                 ViString Trace1,
                                 ViString Trace2);


        /*- IviSpecAnMarker Extension Group -*/
        ViStatus ConfigureMarkerEnabled(ViSession vi,
                                         ViBoolean MarkerEnabled,
                                         ViString MarkerTraceName);

        ViStatus ConfigureMarkerFrequencyCounter(ViSession vi,
                                                  ViBoolean Enabled,
                                                  ViReal64 Resolution);

        ViStatus ConfigureMarkerSearch(ViSession vi,
                                        ViReal64 PeakExcursion,
                                        ViReal64 MarkerThreshold);

        ViStatus ConfigureSignalTrackEnabled(ViSession vi,
                                              ViBoolean SignalTrackEnabled);

        ViStatus DisableAllMarkers(ViSession vi);

        ViStatus GetMarkerName(ViSession vi,
                                ViInt32 Index,
                                ViInt32 NameBufferSize,
                                StringBuilder Name);

        ViStatus MarkerSearch(ViSession vi,
                               ViInt32 SearchType);

        ViStatus MoveMarker(ViSession vi,
                             ViReal64 MarkerPosition);

        ViStatus QueryMarker(ViSession vi,
                              ref ViReal64 MarkerPosition,
                              ref ViReal64 MarkerAmplitude);

        ViStatus SetActiveMarker(ViSession vi,
                                  ViString ActiveMarker);

        ViStatus SetInstrumentFromMarker(ViSession vi,
                                          ViInt32 InstrumentSetting);


        /*- IviSpecAnTrigger Extension Group -*/
        ViStatus ConfigureTriggerSource(ViSession vi,
                                         ViInt32 TriggerSource);


        /*- IviSpecAnExternalTrigger Extension Group -*/
        ViStatus ConfigureExternalTrigger(ViSession vi,
                                           ViReal64 ExternalTriggerLevel,
                                           ViInt32 ExternalTriggerSlope);


        /*- IviSpecAnSoftwareTrigger Extension Group -*/
        ViStatus SendSoftwareTrigger(ViSession vi);


        /*- IviSpecAnVideoTrigger Extension Group -*/
        ViStatus ConfigureVideoTrigger(ViSession vi,
                                        ViReal64 VideoTriggerLevel,
                                        ViInt32 VideoTriggerSlope);


        /*- IviSpecAnMarkerType Extension Group -*/
        ViStatus QueryMarkerType(ViSession vi,
                                  ref ViInt32 MarkerType);


        /*- IviSpecAnDeltaMarker Extension Group -*/
        ViStatus MakeMarkerDelta(ViSession vi,
                                  ViBoolean DeltaMarker);

        ViStatus QueryReferenceMarker(ViSession vi,
                                       ref ViReal64 ReferenceMarkerAmplitude,
                                       ref ViReal64 ReferenceMarkerPosition);


        /*- IviSpecAnExternalMixer Extension Group -*/
        ViStatus ConfigureConversionLossTable(ViSession vi,
                                               ViInt32 Count,
                                               ViReal64[] Frequency,
                                               ViReal64[] ConversionLoss);

        ViStatus ConfigureConversionLossTableEnabled(ViSession vi,
                                                      ViBoolean ConversionLossTableEnabled);

        ViStatus ConfigureExternalMixer(ViSession vi,
                                         ViInt32 Harmonic,
                                         ViReal64 AverageConversionLoss);

        ViStatus ConfigureExternalMixerBias(ViSession vi,
                                             ViReal64 Bias,
                                             ViReal64 BiasLimit);

        ViStatus ConfigureExternalMixerBiasEnabled(ViSession vi,
                                                    ViBoolean BiasEnabled);

        ViStatus ConfigureExternalMixerEnabled(ViSession vi,
                                                ViBoolean ExternalMixerEnabled);

        ViStatus ConfigureExternalMixerNumberOfPorts(ViSession vi,
                                                      ViInt32 NumberOfPorts);


        /*- IviSpecAnPreselector Extension Group -*/
        ViStatus PeakPreselector(ViSession vi);

    }
}
