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
    public interface IviScope : IviDriver
    {
        /*- IviScope Fundamental Capabilities -*/
        ViStatus ConfigureChannel(ViSession vi, ViString channel, ViReal64 range, ViReal64 offset, ViInt32 coupling, ViReal64 probeAttenuation, ViBoolean enabled);
        ViStatus ConfigureChanCharacteristics(ViSession vi, ViString channel, ViReal64 inputImpedance, ViReal64 maxInputFrequency);
        ViStatus ConfigureAcquisitionType(ViSession vi, ViInt32 acquisitionType);
        ViStatus ConfigureAcquisitionRecord(ViSession vi, ViReal64 timePerRecord, ViInt32 minimumRecordLength, ViReal64 acqStartTime);
        ViStatus ActualRecordLength(ViSession vi, ref ViInt32 actualRecordLength);

        ViStatus SampleRate(ViSession vi, ref ViReal64 sampleRate);

        ViStatus ConfigureTrigger(ViSession vi, ViInt32 triggerType, ViReal64 holdoff);
        ViStatus ConfigureTriggerCoupling(ViSession vi, ViInt32 coupling);
        ViStatus ConfigureEdgeTriggerSource(ViSession vi, ViString source, ViReal64 level, ViInt32 slope);

        ViStatus ReadWaveform(ViSession vi, ViString channel, ViInt32 waveformSize, ViInt32 maxTime, IntPtr waveform, ref ViInt32 actualPoints, ref ViReal64 initialX, ref ViReal64 xIncrement);

        ViStatus Abort(ViSession vi);
        ViStatus InitiateAcquisition(ViSession vi);
        ViStatus AcquisitionStatus(ViSession vi, ref ViInt32 status);
        ViStatus FetchWaveform(ViSession vi, ViString channel, ViInt32 waveformSize, IntPtr waveform, ref ViInt32 actualPoints, ref ViReal64 initialX, ref ViReal64 xIncrement);

        ViStatus IsInvalidWfmElement(ViSession vi, ViReal64 elementValue, ref ViBoolean isInvalid);

        ViStatus GetChannelName(ViSession vi, ViInt32 index, ViInt32 bufferSize, StringBuilder name);

        /*- IviScopeTVTrigger Extension Group -*/
        ViStatus ConfigureTVTriggerSource(ViSession vi, ViString source, ViInt32 TVSignalFormat, ViInt32 TVEvent, ViInt32 TVPolarity);
        ViStatus ConfigureTVTriggerLineNumber(ViSession vi, ViInt32 lineNumber);

        /*- IviScopeRuntTrigger Extension Group -*/
        ViStatus ConfigureRuntTriggerSource(ViSession vi, ViString source, ViReal64 runtLowThreshold, ViReal64 runtHighThreshold, ViInt32 runtPolarity);

        /*- IviScopeGlitchTrigger Extension Group -*/
        ViStatus ConfigureGlitchTriggerSource(ViSession vi, ViString source, ViReal64 level, ViReal64 glitchWidth, ViInt32 glitchPolarity, ViInt32 glitchCondition);

        /*- IviScopeWidthTrigger Extension Group -*/
        ViStatus ConfigureWidthTriggerSource(ViSession vi, ViString source, ViReal64 level, ViReal64 widthLowThreshold, ViReal64 widthHighThreshold, ViInt32 widthPolarity, ViInt32 widthCondition);

        /*- IviScopeAcLineTrigger Extension Group -*/
        ViStatus ConfigureAcLineTriggerSlope(ViSession vi, ViInt32 slope);

        /*- IviScopeTriggerModifier Extension Group -*/
        ViStatus ConfigureTriggerModifier(ViSession vi, ViInt32 triggerModifier);

        /*- IviScopeMinMaxWaveform Extension Group -*/
        ViStatus ConfigureNumEnvelopes(ViSession vi, ViInt32 numberOfEnvelopes);

        ViStatus ReadMinMaxWaveform(ViSession vi, ViString channel, ViInt32 waveformSize, ViInt32 maxTime, IntPtr minWaveform, IntPtr maxWaveform, ref ViInt32 actualPoints, ref ViReal64 initialX, ref ViReal64 xIncrement);

        ViStatus FetchMinMaxWaveform(ViSession vi, ViString channel, ViInt32 waveformSize, IntPtr minWaveform, IntPtr maxWaveform, ref ViInt32 actualPoints, ref ViReal64 initialX, ref ViReal64 xIncrement);

        /*- IviScopeWaveformMeas Extension Group -*/
        ViStatus ConfigureRefLevels(ViSession vi, ViReal64 lowRef, ViReal64 midRef, ViReal64 highRef);

        ViStatus ReadWaveformMeasurement(ViSession vi, ViString channel, ViInt32 measurementFunction, ViInt32 maxTime, ref ViReal64 measurement);

        ViStatus FetchWaveformMeasurement(ViSession vi, ViString channel, ViInt32 measurementFunction, ref ViReal64 measurement);

        /*- IviScope Average Acquisition Extension Group -*/
        ViStatus ConfigureNumAverages(ViSession vi, ViInt32 numberOfAverages);

        /*- IviScope Continuous Acquisition Extension Group -*/
        ViStatus ConfigureInitiateContinuous(ViSession vi, ViBoolean continuousAcquisition);

        /*- IviScope Interpolation Extension Group -*/
        ViStatus ConfigureInterpolation(ViSession vi, ViInt32 interpolation);

        /*- IviScope Sample Mode Extension Group -*/
        ViStatus SampleMode(ViSession vi, ref ViInt32 sampleMode);

        /*- IviScope Probe Auto Sense Extension Group -*/
        ViStatus AutoProbeSenseValue(ViSession vi, ViString channel, ref ViReal64 autoProbeSenseValue);
        /*- IviScope Auto Setup Extension Group -*/
        ViStatus AutoSetup(ViSession vi);
    }
}
