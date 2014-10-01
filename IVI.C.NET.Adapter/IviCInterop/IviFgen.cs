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
    public interface IviFgen : IviDriver
    {
        /*- IviFgenBase Capability Group  -*/
        ViStatus ConfigureOutputMode(ViSession vi,
                                      ViInt32 outputMode);
        ViStatus ConfigureOperationMode(ViSession vi,
                                         ViString channelName,
                                         ViInt32 operationMode);

        ViStatus ConfigureRefClockSource(ViSession vi,
                                          ViInt32 refClockSource);

        ViStatus ConfigureOutputImpedance(ViSession vi,
                                           ViString channelName,
                                           ViReal64 outputImpedance);

        ViStatus ConfigureOutputEnabled(ViSession vi,
                                         ViString channelName,
                                         ViBoolean enabled);

        ViStatus GetChannelName(ViSession vi,
                                 ViInt32 index,
                                 ViInt32 bufferSize,
                                 StringBuilder name);

        ViStatus InitiateGeneration(ViSession vi);

        ViStatus AbortGeneration(ViSession vi);

        /*- IviFgenStdFunc Extension Group -*/
        ViStatus ConfigureStandardWaveform(ViSession vi,
                                            ViString channelName,
                                            ViInt32 waveform,
                                            ViReal64 amplitude,
                                            ViReal64 dcOffset,
                                            ViReal64 frequency,
                                            ViReal64 startPhase);

        /*- IviFgenArbWfm Extension Group -*/
        ViStatus QueryArbWfmCapabilities(ViSession vi,
                                          ref ViInt32 maxNumWfms,
                                          ref ViInt32 wfmQuantum,
                                          ref ViInt32 minWfmSize,
                                          ref ViInt32 maxWfmSize);

        ViStatus CreateArbWaveform(ViSession vi,
                                    ViInt32 wfmSize,
                                    ref ViReal64[] wfmData,
                                    ref ViInt32 wfmHandle);

        ViStatus ConfigureSampleRate(ViSession vi,
                                      ViReal64 sampleRate);

        ViStatus ConfigureArbWaveform(ViSession vi,
                                       ViString channelName,
                                       ViInt32 wfmHandle,
                                       ViReal64 arbGain,
                                       ViReal64 arbOffset);

        ViStatus ClearArbWaveform(ViSession vi,
                                   ViInt32 wfmHandle);

        /*- IviFgenArbFrequency Extension Group -*/
        ViStatus ConfigureArbFrequency(ViSession vi,
                                        ViString channelName,
                                        ViReal64 frequency);

        /*- IviFgenArbSeq Extension Group -*/
        ViStatus QueryArbSeqCapabilities(ViSession vi,
                                          ref ViInt32 maxNumSeqs,
                                          ref ViInt32 minSeqLength,
                                          ref ViInt32 maxSeqLength,
                                          ref ViInt32 maxLoopCount);

        ViStatus CreateArbSequence(ViSession vi,
                                    ViInt32 seqLength,
                                    ViInt32[] wfmHandle,
                                    ViInt32[] wfmLoopCount,
                                    ref ViInt32 seqHandle);

        ViStatus ConfigureArbSequence(ViSession vi,
                                       ViString channelName,
                                       ViInt32 seqHandle,
                                       ViReal64 arbGain,
                                       ViReal64 arbOffset);

        ViStatus ClearArbSequence(ViSession vi,
                                   ViInt32 seqHandle);

        ViStatus ClearArbMemory(ViSession vi);

        /*- IviFgenTrigger Extension Group -*/
        ViStatus ConfigureTriggerSource(ViSession vi,
                                         ViString channelName,
                                         ViInt32 trigSource);

        /*- IviFgenStartTrigger Extension Group -*/
        ViStatus ConfigureStartTrigger(ViSession vi,
                                        ViString channelName,
                                        ViString source,
                                        ViInt32 slope);

        /*- IviFgenStopTrigger Extension Group -*/
        ViStatus ConfigureStopTrigger(ViSession vi,
                                       ViString channelName,
                                       ViString source,
                                       ViInt32 slope);

        ViStatus SendSoftwareStopTrigger(ViSession vi);

        /*- IviFgenHoldTrigger Extension Group -*/
        ViStatus ConfigureHoldTrigger(ViSession vi,
                                       ViString channelName,
                                       ViString source,
                                       ViInt32 slope);

        ViStatus SendSoftwareHoldTrigger(ViSession vi);

        /*- IviFgenResumeTrigger Extension Group -*/
        ViStatus ConfigureResumeTrigger(ViSession vi,
                                         ViString channelName,
                                         ViString source,
                                         ViInt32 slope);

        ViStatus SendSoftwareResumeTrigger(ViSession vi);

        /*- IviFgenAdvanceTrigger Extension Group -*/
        ViStatus ConfigureAdvanceTrigger(ViSession vi,
                                          ViString channelName,
                                          ViString source,
                                          ViInt32 slope);

        ViStatus SendSoftwareAdvanceTrigger(ViSession vi);

        /*- IviFgenInternalTrigger Extension Group -*/
        ViStatus ConfigureInternalTriggerRate(ViSession vi,
                                               ViReal64 rate);

        /*- IviFgenSoftwareTrigger Extension Group -*/
        ViStatus SendSoftwareTrigger(ViSession vi);

        /*- IviFgenBurst Extension Group -*/
        ViStatus ConfigureBurstCount(ViSession vi,
                                      ViString channelName,
                                      ViInt32 count);

        /*- IviFgenModulateAM Extension Group -*/
        ViStatus ConfigureAMEnabled(ViSession vi,
                                     ViString channelName,
                                     ViBoolean enabled);

        ViStatus ConfigureAMSource(ViSession vi,
                                    ViString channelName,
                                    ViInt32 source);

        ViStatus ConfigureAMInternal(ViSession vi,
                                      ViReal64 amdepth,
                                      ViInt32 amWaveform,
                                      ViReal64 amFrequency);

        /*- IviFgenModulateFM Extension Group -*/
        ViStatus ConfigureFMEnabled(ViSession vi,
                                     ViString channelName,
                                     ViBoolean enabled);

        ViStatus ConfigureFMSource(ViSession vi,
                                    ViString channelName,
                                    ViInt32 source);

        ViStatus ConfigureFMInternal(ViSession vi,
                                      ViReal64 fmdeviation,
                                      ViInt32 fmWaveform,
                                      ViReal64 fmFrequency);

        /*- IviFgenSampleClock Extension Group -*/
        ViStatus ConfigureSampleClock(ViSession vi,
                                       ViInt32 source);

        ViStatus ConfigureSampleClockOutputEnabled(ViSession vi,
                                                    ViBoolean enabled);

        /*- IviFgenArbWfmSize64 Extension Group -*/
        ViStatus QueryArbWfmCapabilities64(ViSession vi,
                                            ref ViInt32 maxNumWfms,
                                            ref ViInt32 wfmQuantum,
                                            ref ViInt64 minWfmSize,
                                            ref ViInt64 maxWfmSize);

        /*- IviFgenArbChannelWfm Extension Group -*/
        ViStatus CreateChannelArbWaveform(ViSession vi,
                                           ViString channelName,
                                           ViInt64 wfmSize,
                                           ViReal64[] wfmData,
                                           ref ViInt32 wfmHandle);

        /*- IviFgenArbWfmBinary Extension Group -*/
        ViStatus CreateChannelArbWaveform16(ViSession vi,
                                             ViString channelName,
                                             ViInt64 wfmSize,
                                             ViInt16[] wfmData,
                                             ref ViInt32 wfmHandle);

        ViStatus CreateChannelArbWaveform32(ViSession vi,
                                             ViString channelName,
                                             ViInt64 wfmSize,
                                             ViInt32[] wfmData,
                                             ref ViInt32 wfmHandle);

        /*- IviFgenDataMarker Extension Group -*/
        ViStatus ConfigureDataMarker(ViSession vi,
                                      ViString name,
                                      ViString sourceChannel,
                                      ViInt32 bitPosition,
                                      ViString destination);

        ViStatus GetDataMarkerName(ViSession vi,
                                    ViInt32 index,
                                    ViInt32 nameBufferSize,
                                    StringBuilder name);

        ViStatus DisableAllDataMarkers(ViSession vi);

        /*- IviFgenSparseMarker Extension Group -*/
        ViStatus ConfigureSparseMarker(ViSession vi,
                                        ViString name,
                                        ViInt32 wfmHandle,
                                        ViInt64 numIndexes,
                                        ViInt64[] indexes,
                                        ViString destination);

        ViStatus GetSparseMarkerName(ViSession vi,
                                      ViInt32 index,
                                      ViInt32 nameBufferSize,
                                      StringBuilder name);

        ViStatus GetSparseMarkerIndexes(ViSession vi,
                                         ViString name,
                                         ViInt64 indexesArraySize,
                                         IntPtr indexes,
                                         ref ViInt64 indexesActualSize);

        ViStatus SetSparseMarkerIndexes(ViSession vi,
                                         ViString name,
                                         ViInt64 indexesArraySize,
                                         ViInt64[] indexes);

        ViStatus DisableAllSparseMarkers(ViSession vi);
    }
}
