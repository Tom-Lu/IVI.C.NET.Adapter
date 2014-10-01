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
    public interface IviRFSigGen : IviDriver
    {
        /*- IviRFSigGen Fundamental Capabilities -*/
        ViStatus ConfigureRF(ViSession vi,
                              ViReal64 Frequency,
                              ViReal64 PowerLevel);

        ViStatus ConfigureALCEnabled(ViSession vi,
                                      ViBoolean ALCEnabled);

        ViStatus ConfigureOutputEnabled(ViSession vi,
                                         ViBoolean OutputEnabled);

        ViStatus DisableAllModulation(ViSession vi);

        ViStatus IsSettled(ViSession vi,
                            ref ViBoolean Done);

        ViStatus WaitUntilSettled(ViSession vi,
                                   ViInt32 MaxTimeMilliseconds);


        /*- IviRFSigGenModulateAM Extension Group -*/
        ViStatus ConfigureAMEnabled(ViSession vi,
                                     ViBoolean Enabled);

        ViStatus ConfigureAMExternalCoupling(ViSession vi,
                                              ViInt32 Coupling);

        ViStatus ConfigureAM(ViSession vi,
                              ViString Source,
                              ViInt32 Scaling,
                              ViReal64 Depth);


        /*- IviRFSigGenModulateFM Extension Group -*/
        ViStatus ConfigureFMEnabled(ViSession vi,
                                     ViBoolean Enabled);

        ViStatus ConfigureFMExternalCoupling(ViSession vi,
                                              ViInt32 Coupling);

        ViStatus ConfigureFM(ViSession vi,
                              ViString Source,
                              ViReal64 Deviation);


        /*- IviRFSigGenModulatePM Extension Group -*/
        ViStatus ConfigurePMEnabled(ViSession vi,
                                     ViBoolean Enabled);

        ViStatus ConfigurePMExternalCoupling(ViSession vi,
                                              ViInt32 Coupling);

        ViStatus ConfigurePM(ViSession vi,
                              ViString Source,
                              ViReal64 Deviation);


        /*- IviRFSigGenAnalogModulationSource Extension Group -*/
        ViStatus GetAnalogModulationSourceName(ViSession vi,
                                                ViInt32 Index,
                                                ViInt32 NameBufferSize,
                                                StringBuilder Name);


        /*- IviRFSigGenModulatePulse Extension Group -*/
        ViStatus ConfigurePulseModulationEnabled(ViSession vi,
                                                  ViBoolean Enabled);

        ViStatus ConfigurePulseModulationSource(ViSession vi,
                                                 ViInt32 Source);

        ViStatus ConfigurePulseModulationExternalPolarity(ViSession vi,
                                                           ViInt32 Polarity);


        /*- IviRFSigGenLFGenerator Extension Group -*/
        ViStatus GetLFGeneratorName(ViSession vi,
                                     ViInt32 Index,
                                     ViInt32 NameBufferSize,
                                     StringBuilder Name);

        ViStatus SetActiveLFGenerator(ViSession vi,
                                       ViString ActiveLFGenerator);

        ViStatus ConfigureLFGenerator(ViSession vi,
                                       ViReal64 Frequency,
                                       ViInt32 Waveform);


        /*- IviRFSigGenLFGeneratorOutput Extension Group -*/
        ViStatus ConfigureLFGeneratorOutput(ViSession vi,
                                             ViReal64 Amplitude,
                                             ViBoolean Enabled);


        /*- IviRFSigGenPulseGenerator Extension Group -*/
        ViStatus ConfigurePulseExternalTrigger(ViSession vi,
                                                ViInt32 Slope,
                                                ViReal64 Delay);

        ViStatus ConfigurePulseInternalTrigger(ViSession vi,
                                                ViReal64 Period);

        ViStatus ConfigurePulse(ViSession vi,
                                 ViInt32 PulseTriggerSource,
                                 ViReal64 PulseWidth,
                                 ViBoolean GatingEnabled);


        /*- IviRFSigGenPulseDoubleGenerator Extension Group -*/
        ViStatus ConfigurePulseDouble(ViSession vi,
                                       ViBoolean Enabled,
                                       ViReal64 Delay);


        /*- IviRFSigGenPulseGeneratorOutput Extension Group -*/
        ViStatus ConfigurePulseOutput(ViSession vi,
                                       ViInt32 Polarity,
                                       ViBoolean Enabled);


        /*- IviRFSigGenSweep Extension Group -*/
        ViStatus ConfigureSweep(ViSession vi,
                                 ViInt32 Mode,
                                 ViInt32 TriggerSource);


        /*- IviRFSigGenFrequencySweep Extension Group -*/
        ViStatus ConfigureFrequencySweepStartStop(ViSession vi,
                                                   ViReal64 Start,
                                                   ViReal64 Stop);

        ViStatus ConfigureFrequencySweepCenterSpan(ViSession vi,
                                                    ViReal64 Center,
                                                    ViReal64 Span);

        ViStatus ConfigureFrequencySweepTime(ViSession vi,
                                              ViReal64 Time);


        /*- IviRFSigGenPowerSweep Extension Group -*/
        ViStatus ConfigurePowerSweepStartStop(ViSession vi,
                                               ViReal64 Start,
                                               ViReal64 Stop);

        ViStatus ConfigurePowerSweepTime(ViSession vi,
                                          ViReal64 Time);


        /*- IviRFSigGenFrequencyStep Extension Group -*/
        ViStatus ConfigureFrequencyStepStartStop(ViSession vi,
                                                  ViReal64 Start,
                                                  ViReal64 Stop,
                                                  ViInt32 Scaling,
                                                  ViReal64 StepSize);

        ViStatus ConfigureFrequencyStepDwell(ViSession vi,
                                              ViBoolean SingleStepEnabled,
                                              ViReal64 Dwell);

        ViStatus ResetFrequencyStep(ViSession vi);


        /*- IviRFSigGenPowerStep Extension Group -*/
        ViStatus ConfigurePowerStepStartStop(ViSession vi,
                                              ViReal64 Start,
                                              ViReal64 Stop,
                                              ViReal64 StepSize);

        ViStatus ConfigurePowerStepDwell(ViSession vi,
                                          ViBoolean SingleStepEnabled,
                                          ViReal64 Dwell);

        ViStatus ResetPowerStep(ViSession vi);


        /*- IviRFSigGenList Extension Group -*/
        ViStatus CreateFrequencyList(ViSession vi,
                                      ViString Name,
                                      ViInt32 Length,
                                      ViReal64[] Frequency);

        ViStatus CreatePowerList(ViSession vi,
                                  ViString Name,
                                  ViInt32 Length,
                                  ViReal64[] Power);

        ViStatus CreateFrequencyPowerList(ViSession vi,
                                           ViString Name,
                                           ViInt32 Length,
                                           ViReal64[] Frequency,
                                           ViReal64[] Power);

        ViStatus SelectList(ViSession vi,
                             ViString Name);

        ViStatus ClearAllLists(ViSession vi);

        ViStatus ConfigureListDwell(ViSession vi,
                                     ViBoolean SingleStepEnabled,
                                     ViReal64 Dwell);

        ViStatus ResetList(ViSession vi);


        /*- IviRFSigGenALC Extension Group -*/
        ViStatus ConfigureALC(ViSession vi,
                               ViInt32 Source,
                               ViReal64 Bandwidth);


        /*- IviRFSigGenReferenceOscillator Extension Group -*/
        ViStatus ConfigureReferenceOscillator(ViSession vi,
                                               ViInt32 Source,
                                               ViReal64 Frequency);


        /*- IviRFSigGenSoftwareTrigger Extension Group -*/
        ViStatus SendSoftwareTrigger(ViSession vi);


        /*- IviRFSigGenModulateIQ Extension Group -*/
        ViStatus ConfigureIQEnabled(ViSession vi,
                                     ViBoolean Enabled);

        ViStatus ConfigureIQ(ViSession vi,
                              ViInt32 Source,
                              ViBoolean SwapEnabled);

        ViStatus CalibrateIQ(ViSession vi);


        /*- IviRFSigGenIQImpairment Extension Group -*/
        ViStatus ConfigureIQImpairmentEnabled(ViSession vi,
                                               ViBoolean Enabled);

        ViStatus ConfigureIQImpairment(ViSession vi,
                                        ViReal64 IOffset,
                                        ViReal64 QOffset,
                                        ViReal64 Ratio,
                                        ViReal64 Skew);


        /*- IviRFSigGenArbGenerator Extension Group -*/
        ViStatus ConfigureArb(ViSession vi,
                               ViReal64 ClockFrequency,
                               ViReal64 FilterFrequency);

        ViStatus WriteArbWaveform(ViSession vi,
                                   ViString Name,
                                   ViInt32 NumberOfSamples,
                                   ViReal64[] IData,
                                   ViReal64[] QData,
                                   ViBoolean MoreDataPending);

        ViStatus SelectArbWaveform(ViSession vi,
                                    ViString Name);

        ViStatus ClearAllArbWaveforms(ViSession vi);

        ViStatus QueryArbWaveformCapabilities(ViSession vi,
                                               ref ViInt32 MaxNumberWaveforms,
                                               ref ViInt32 WaveformQuantum,
                                               ref ViInt32 MinWaveformSize,
                                               ref ViInt32 MaxWaveformSize);

        ViStatus ConfigureArbTriggerSource(ViSession vi,
                                            ViInt32 Source);

        ViStatus ConfigureArbExternalTriggerSlope(ViSession vi,
                                                   ViInt32 Slope);


        /*- IviRFSigGenDigitalModulationBase Extension Group -*/
        ViStatus GetDigitalModulationBaseStandardName(ViSession vi,
                                                       ViInt32 Index,
                                                       ViInt32 NameBufferSize,
                                                       StringBuilder Name);

        ViStatus SelectDigitalModulationBaseStandard(ViSession vi,
                                                      ViString Name);

        ViStatus ConfigureDigitalModulationBaseClockSource(ViSession vi,
                                                            ViInt32 Source,
                                                            ViInt32 Type);

        ViStatus ConfigureDigitalModulationBaseDataSource(ViSession vi,
                                                           ViInt32 Source);

        ViStatus ConfigureDigitalModulationBasePRBSType(ViSession vi,
                                                         ViInt32 Type);

        ViStatus WriteDigitalModulationBaseBitSequence(ViSession vi,
                                                        ViString Name,
                                                        ViInt32 BitCount,
                                                        ViInt8[] Sequence,
                                                        ViBoolean MoreDataPending);

        ViStatus SelectDigitalModulationBaseBitSequence(ViSession vi,
                                                         ViString Name);

        ViStatus ClearAllDigitalModulationBaseBitSequences(ViSession vi);


        /*- IviRFSigGenCDMABase Extension Group -*/
        ViStatus GetCDMAStandardName(ViSession vi,
                                      ViInt32 Index,
                                      ViInt32 NameBufferSize,
                                      StringBuilder Name);

        ViStatus SelectCDMAStandard(ViSession vi,
                                     ViString Name);

        ViStatus ConfigureCDMAClockSource(ViSession vi,
                                           ViInt32 Source);

        ViStatus ConfigureCDMATriggerSource(ViSession vi,
                                             ViInt32 Source);

        ViStatus ConfigureCDMAExternalTriggerSlope(ViSession vi,
                                                    ViInt32 Slope);

        ViStatus GetCDMATestModelName(ViSession vi,
                                       ViInt32 Index,
                                       ViInt32 NameBufferSize,
                                       StringBuilder Name);

        ViStatus SelectCDMATestModel(ViSession vi,
                                      ViString Name);


        /*- IviRFSigGenTDMABase Extension Group -*/
        ViStatus GetTDMAStandardName(ViSession vi,
                                      ViInt32 Index,
                                      ViInt32 NameBufferSize,
                                      StringBuilder Name);

        ViStatus SelectTDMAStandard(ViSession vi,
                                     ViString Name);

        ViStatus ConfigureTDMAClockSource(ViSession vi,
                                           ViInt32 Source,
                                           ViInt32 Type);

        ViStatus ConfigureTDMATriggerSource(ViSession vi,
                                             ViInt32 Source);

        ViStatus ConfigureTDMAExternalTriggerSlope(ViSession vi,
                                                    ViInt32 Slope);

        ViStatus GetTDMAFrameName(ViSession vi,
                                   ViInt32 Index,
                                   ViInt32 NameBufferSize,
                                   StringBuilder Name);

        ViStatus SelectTDMAFrame(ViSession vi,
                                  ViString Name);
    }
}
