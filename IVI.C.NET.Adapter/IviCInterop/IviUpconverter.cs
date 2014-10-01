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
    public interface IviUpconverter : IviDriver
    {
        /*- IviUpconverterBase Functions -*/
        ViStatus ConfigureIFInputAttenuation(ViSession vi,
                                              ViReal64 attenuation);

        ViStatus ConfigureRFOutputEnabled(ViSession vi,
                                           ViBoolean enabled);

        ViStatus ConfigureRFOutputFrequency(ViSession vi,
                                             ViReal64 frequency);

        ViStatus GetIFInputName(ViSession vi,
                                 ViInt32 index,
                                 ViInt32 nameBufferSize,
                                 StringBuilder name);

        ViStatus GetRFOutputName(ViSession vi,
                                  ViInt32 index,
                                  ViInt32 nameBufferSize,
                                  StringBuilder name);

        ViStatus SetActiveIFInput(ViSession vi,
                                   ViString name);

        ViStatus SetActiveRFOutput(ViSession vi,
                                    ViString name);

        ViStatus WaitUntilReady(ViSession vi,
                                 ViInt32 maxTimeMilliseconds);

        /*- IviUpconverterOutputGain Functions -*/
        ViStatus ConfigureRFOutputGain(ViSession vi,
                                        ViReal64 gain);

        /*- IviUpconverterOutputPowerLevel Functions -*/
        ViStatus ConfigureRFOutputLevel(ViSession vi,
                                         ViReal64 level);

        /*- IviUpconverterModulateAM Functions -*/
        ViStatus ConfigureAM(ViSession vi,
                              ViString source,
                              ViInt32 scaling,
                              ViReal64 depth);

        ViStatus ConfigureAMEnabled(ViSession vi,
                                     ViBoolean enabled);

        ViStatus ConfigureAMExternalCoupling(ViSession vi,
                                              ViInt32 coupling);

        /*- IviUpconverterModulateFM Functions -*/
        ViStatus ConfigureFM(ViSession vi,
                              ViString source,
                              ViReal64 deviation);

        ViStatus ConfigureFMEnabled(ViSession vi,
                                     ViBoolean enabled);

        ViStatus ConfigureFMExternalCoupling(ViSession vi,
                                              ViInt32 coupling);

        /*- IviUpconverterModulatePM Functions -*/
        ViStatus ConfigurePM(ViSession vi,
                              ViString source,
                              ViReal64 deviation);

        ViStatus ConfigurePMEnabled(ViSession vi,
                                     ViBoolean enabled);

        ViStatus ConfigurePMExternalCoupling(ViSession vi,
                                              ViInt32 coupling);

        /*- IviUpconverterAnalogModulationSource Functions -*/
        ViStatus GetAnalogModulationSourceName(ViSession vi,
                                                ViInt32 index,
                                                ViInt32 nameBufferSize,
                                                StringBuilder name);

        /*- IviUpconverterModulatePulse Functions -*/
        ViStatus ConfigurePulseModulationEnabled(ViSession vi,
                                                  ViBoolean enabled);

        ViStatus ConfigurePulseModulationExternalPolarity(ViSession vi,
                                                           ViInt32 polarity);

        /*- IviUpconverterBypass Functions -*/
        ViStatus ConfigureBypass(ViSession vi,
                                  ViBoolean bypass);

        /*- IviUpconverterOutputReadyTrigger Functions -*/
        ViStatus ConfigureRFOutputReadyTrigger(ViSession vi,
                                                ViString outputTrigger);

        /*- IviUpconverterSweep Functions -*/
        ViStatus ConfigureSweep(ViSession vi,
                                 ViInt32 mode,
                                 ViString triggerSource);

        /*- IviUpconverterFrequencySweep Functions -*/
        ViStatus ConfigureFrequencySweepCenterSpan(ViSession vi,
                                                    ViReal64 center,
                                                    ViReal64 span);

        ViStatus ConfigureFrequencySweepStartStop(ViSession vi,
                                                   ViReal64 start,
                                                   ViReal64 stop);

        ViStatus ConfigureFrequencySweepTime(ViSession vi,
                                              ViReal64 sweepTime);

        /*- IviUpconverterPowerSweep Functions -*/
        ViStatus ConfigurePowerSweepStartStop(ViSession vi,
                                               ViReal64 start,
                                               ViReal64 stop);

        ViStatus ConfigurePowerSweepTime(ViSession vi,
                                          ViReal64 sweepTime);

        /*- IviUpconverterGainSweep Functions -*/
        ViStatus ConfigureGainSweepStartStop(ViSession vi,
                                              ViReal64 start,
                                              ViReal64 stop);

        ViStatus ConfigureGainSweepTime(ViSession vi,
                                         ViReal64 sweepTime);

        /*- IviUpconverterFrequencyStep Functions -*/
        ViStatus ConfigureFrequencyStepDwell(ViSession vi,
                                              ViBoolean singleStepEnabled,
                                              ViReal64 dwell);

        ViStatus ConfigureFrequencyStepStartStop(ViSession vi,
                                                  ViReal64 start,
                                                  ViReal64 stop,
                                                  ViInt32 scaling,
                                                  ViReal64 stepSize);

        ViStatus ResetFrequencyStep(ViSession vi);

        /*- IviUpconverterPowerStep Functions -*/
        ViStatus ConfigurePowerStepDwell(ViSession vi,
                                          ViBoolean singleStepEnabled,
                                          ViReal64 dwell);

        ViStatus ConfigurePowerStepStartStop(ViSession vi,
                                              ViReal64 start,
                                              ViReal64 stop,
                                              ViReal64 stepSize);

        ViStatus ResetPowerStep(ViSession vi);

        /*- IviUpconverterGainStep Functions -*/
        ViStatus ConfigureGainStepDwell(ViSession vi,
                                         ViBoolean singleStepEnabled,
                                         ViReal64 dwell);

        ViStatus ConfigureGainStepStartStop(ViSession vi,
                                             ViReal64 start,
                                             ViReal64 stop,
                                             ViReal64 stepSize);

        ViStatus ResetGainStep(ViSession vi);

        /*- IviUpconverterList Functions -*/
        ViStatus ClearAllLists(ViSession vi);

        ViStatus ConfigureListDwell(ViSession vi,
                                     ViBoolean singleStepEnabled,
                                     ViReal64 dwell);

        ViStatus CreateFrequencyList(ViSession vi,
                                      ViString name,
                                      ViInt32 frequencyBufferSize,
                                      ViReal64[] frequency);

        ViStatus CreateFrequencyPowerList(ViSession vi,
                                           ViString name,
                                           ViInt32 bufferSize,
                                           ViReal64[] frequency,
                                           ViReal64[] power);

        ViStatus CreateFrequencyGainList(ViSession vi,
                                          ViString name,
                                          ViInt32 bufferSize,
                                          ViReal64[] frequency,
                                          ViReal64[] gain);

        ViStatus CreatePowerList(ViSession vi,
                                  ViString name,
                                  ViInt32 powerBufferSize,
                                  ViReal64[] power);

        ViStatus CreateGainList(ViSession vi,
                                 ViString name,
                                 ViInt32 gainBufferSize,
                                 ViReal64[] gain);

        ViStatus ResetList(ViSession vi);

        /*- IviUpconverterALC Functions -*/
        ViStatus ConfigureALC(ViSession vi,
                               ViInt32 source,
                               ViReal64 bandwidth);

        /*- IviUpconverterCalibration Functions -*/
        ViStatus Calibrate(ViSession vi);

        ViStatus IsCalibrationComplete(ViSession vi,
                                        ref ViInt32 status);

        /*- IviUpconverterAttenuatorHold Functions -*/
        ViStatus ConfigureAttenuatorHoldEnabled(ViSession vi,
                                                 ViBoolean enabled);

        /*- IviUpconverterReferenceOscillator Functions -*/
        ViStatus ConfigureReferenceOscillator(ViSession vi,
                                               ViInt32 source,
                                               ViReal64 frequency);

        ViStatus ConfigureReferenceOscillatorOutputEnabled(ViSession vi,
                                                            ViBoolean enabled);

        /*- IviUpconverterSoftwareTrigger Functions -*/
        ViStatus SendSoftwareTrigger(ViSession vi);

        /*- IviUpconverterModulateIQ Functions -*/
        ViStatus CalibrateIQ(ViSession vi);

        ViStatus ConfigureIQEnabled(ViSession vi,
                                     ViBoolean enabled);

        /*- IviUpconverterIQImpairment Functions -*/
        ViStatus ConfigureIQImpairment(ViSession vi,
                                        ViReal64 IOffset,
                                        ViReal64 QOffset,
                                        ViReal64 ratio,
                                        ViReal64 skew);

        ViStatus ConfigureIQImpairmentEnabled(ViSession vi,
                                               ViBoolean enabled);
    }
}

