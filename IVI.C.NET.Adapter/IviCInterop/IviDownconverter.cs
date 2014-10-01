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
    public interface IviDownconverter : IviDriver
    {
        /*- IviDownconverterBase Functions -*/
        ViStatus ConfigureIFOutputEnabled(ViSession vi,
                                          ViBoolean enabled);

        ViStatus ConfigureIFOutputGain(ViSession vi,
                                       ViReal64 gain);

        ViStatus GetIFOutputName(ViSession vi,
                                 ViInt32 index,
                                 ViInt32 nameBufferSize,
                                 StringBuilder name);

        ViStatus SetActiveIFOutput(ViSession vi,
                                   ViString name);

        ViStatus ConfigureRFInputAttenuation(ViSession vi,
                                             ViReal64 attenuation);

        ViStatus ConfigureRFInputFrequency(ViSession vi,
                                           ViReal64 frequency);

        ViStatus GetRFInputName(ViSession vi,
                                ViInt32 index,
                                ViInt32 nameBufferSize,
                                StringBuilder name);

        ViStatus SetActiveRFInput(ViSession vi,
                                  ViString name);

        ViStatus WaitUntilSettled(ViSession vi,
                                  ViInt32 maxTimeMilliseconds);

        /*- IviDownconverterBypass Functions -*/
        ViStatus ConfigureBypass(ViSession vi,
                                 ViBoolean bypass);

        /*- IviDownconverterExternalMixer Functions -*/
        ViStatus ConfigureExternalMixerBias(ViSession vi,
                                            ViReal64 bias,
                                            ViReal64 biasLimit);

        /*- IviDownconverterFrequencyStep Functions -*/
        ViStatus ConfigureFrequencyStepDwell(ViSession vi,
                                             ViBoolean singleStepEnabled,
                                             ViReal64 dwell);

        ViStatus ConfigureFrequencyStepStartStop(ViSession vi,
                                                 ViReal64 start,
                                                 ViReal64 stop,
                                                 ViInt32 scaling,
                                                 ViReal64 stepSize);

        ViStatus ResetFrequencyStep(ViSession vi);

        /*- IviDownconverterFrequencySweep Functions -*/
        ViStatus ConfigureFrequencySweep(ViSession vi,
                                         ViInt32 mode,
                                         ViString triggerSource);

        ViStatus ConfigureFrequencySweepStartStop(ViSession vi,
                                                  ViReal64 start,
                                                  ViReal64 stop);

        ViStatus ConfigureFrequencySweepTime(ViSession vi,
                                             ViReal64 sweepTime);

        ViStatus WaitUntilFrequencySweepComplete(ViSession vi,
                                                 ViInt32 maxTimeMilliseconds);

        /*- IviDownconverterFrequencySweepList Functions -*/
        ViStatus ClearAllFrequencySweepLists(ViSession vi);

        ViStatus ConfigureFrequencySweepListDwell(ViSession vi,
                                                  ViBoolean singleStepEnabled,
                                                  ViReal64 dwell);

        ViStatus CreateFrequencySweepList(ViSession vi,
                                          ViString name,
                                          ViInt32 frequencyListBufferSize,
                                          ViReal64[] frequencyList);

        ViStatus ResetFrequencySweepList(ViSession vi);

        /*- IviDownconverterBandCrossingInformation Functions -*/
        ViStatus GetBandCrossingInfo(ViSession vi,
                                     ViInt32 bufferSize,
                                     IntPtr startFrequencies,
                                     IntPtr stopFrequencies,
                                     ref ViInt32 actualNumFrequencies);

        /*- IviDownconverterSoftwareTrigger Functions -*/
        ViStatus SendSoftwareTrigger(ViSession vi);

        /*- IviDownconverterIFFilter Functions -*/
        ViStatus ConfigureIFOutputFilterBandwidth(ViSession vi,
                                                  ViReal64 bandwidth);

        /*- IviDownconverterPreselector Functions -*/
        ViStatus ConfigurePreselectorEnabled(ViSession vi,
                                             ViBoolean enabled);

        /*- IviDownconverterVideoDetectorBandwidth Functions -*/
        ViStatus ConfigureIFOutputVideoDetectorBandwidth(ViSession vi,
                                                         ViReal64 bandwidth);

        /*- IviDownconverterCalibration Functions -*/
        ViStatus Calibrate(ViSession vi);

        ViStatus IsCalibrationComplete(ViSession vi,
                                       ref ViInt32 status);

        ViStatus IsCalibrated(ViSession vi,
                              ref ViInt32 status);

        /*- IviDownconverterReferenceOscillator Functions -*/
        ViStatus ConfigureReferenceOscillator(ViSession vi,
                                              ViInt32 source,
                                              ViReal64 frequency);

        ViStatus ConfigureReferenceOscillatorOutputEnabled(ViSession vi,
                                                           ViBoolean enabled);
    }
}

