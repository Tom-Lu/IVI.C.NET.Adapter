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
    public interface IviACPwr : IviDriver
    {
        /*- IviACPwrBase Functions -*/
        ViStatus ConfigureCurrentLimit(ViSession vi, ViString PhaseName, ViReal64 Limit);
        ViStatus ConfigureOutputEnabled(ViSession vi, ViString PhaseName, ViBoolean Enabled);
        ViStatus ConfigureVoltageLevel(ViSession vi, ViString PhaseName, ViReal64 VoltageLevel);
        ViStatus ConfigureVoltageRange(ViSession vi, ViString PhaseName, ViReal64 Range);
        ViStatus ConfigureFrequency(ViSession vi, ViReal64 Frequency);
        ViStatus ConfigureFrequencyRange(ViSession vi, ViReal64 Range);
        ViStatus ConfigureWaveform(ViSession vi, ViString PhaseName, ViString WaveformName);
        ViStatus QueryVoltageRangeCapabilities(ViSession vi, ViString PhaseName, ViInt32 Range, ViString WaveformName, ref ViReal64 MinVoltage, ref ViReal64 MaxVoltage);
        ViStatus QueryFrequencyRangeCapabilities(ViSession vi, ViInt32 Range, ref ViReal64 MinFrequency, ref ViReal64 MaxFrequency);
        ViStatus GetOutputPhaseName(ViSession vi, ViInt32 Index, ViInt32 NameBufferSize, ref ViString Name);

        /*- IviACPwrMeasurement Functions -*/
        ViStatus InitiateMeasurement(ViSession vi, ViInt32 Group);
        ViStatus FetchMeasurement(ViSession vi, ViString PhaseName, ViInt32 MeasurementType, ref ViReal64 Measurement);
        ViStatus FetchMeasurementArray(ViSession vi, ViString PhaseName, ViInt32 MeasurementType, ViInt32 MeasurementBufferSize, ref ViReal64[] Measurement, ref ViInt32 MeasurementActualSize);

        /*- IviACPwrPhase Functions -*/
        ViStatus ConfigurePhaseAngle(ViSession vi, ViString PhaseName, ViReal64 PhaseAngle);

        /*- IviACPwrExternalSync Functions -*/
        ViStatus ConfigureExternalSync(ViSession vi, ViBoolean Enabled, ViReal64 PhaseOffset);
        ViStatus QueryExternalSyncLocked(ViSession vi, ref ViBoolean Locked);

        /*- IviACPwrCurrentProtection Functions -*/
        ViStatus QueryCurrentProtectionTripped(ViSession vi, ViString PhaseName, ref ViBoolean Tripped);
        ViStatus ResetCurrentProtection(ViSession vi, ViString PhaseName);
        ViStatus ConfigureCurrentProtection(ViSession vi, ViString PhaseName, ViBoolean Enabled, ViReal64 Threshold, ViReal64 Delay);

        /*- IviACPwrVoltageProtection Functions -*/
        ViStatus QueryVoltageProtectionTripped(ViSession vi, ViString PhaseName, ref ViBoolean Tripped);
        ViStatus ResetVoltageProtection(ViSession vi, ViString PhaseName);
        ViStatus ConfigureVoltageProtection(ViSession vi, ViString PhaseName, ViBoolean UnderEnabled, ViBoolean OverEnabled, ViReal64 UnderLimit, ViReal64 OverLimit);

        /*- IviACPwrArbWaveform Functions -*/
        ViStatus ClearArbWaveform(ViSession vi, ViString WaveformName);
        ViStatus WriteArbWaveform(ViSession vi, ViString WaveformName, ViInt32 WaveformDataBufferSize, ref ViReal64[] WaveformData);
        ViStatus QueryArbWaveformCatalog(ViSession vi, ViInt32 CatalogType, ViInt32 CatalogBufferSize, ref ViString Catalog);

        /*- IviACPwrImpedance Functions -*/
        ViStatus ConfigureOutputImpedance(ViSession vi, ViString PhaseName, ViBoolean Enabled, ViReal64 ResistiveValue, ViReal64 InductiveValue);
        ViStatus QueryOutputImpedanceCapabilities(ViSession vi, ViString PhaseName, ref ViReal64 ResistiveMin, ref ViReal64 ResistiveMax, ref ViReal64 InductiveMin, ref ViReal64 InductiveMax);

        /*- IviACPwrDCGeneration Functions -*/
        ViStatus ConfigureDC(ViSession vi, ViString PhaseName, ViInt32 Mode, ViReal64 DCVoltageLevel);
        ViStatus ConfigureDCRange(ViSession vi, ViString PhaseName, ViReal64 Minimum, ViReal64 Maximum);
        ViStatus QueryDCCapabilities(ViSession vi, ViString PhaseName, ViInt32 Range, ref ViReal64 Minimum, ref ViReal64 Maximum);

        /*- IviACPwrVoltageRamp Functions -*/
        ViStatus RampVoltage(ViSession vi, ViString PhaseName, ViReal64 StartVoltage, ViReal64 EndVoltage, ViReal64 Duration);
        ViStatus QueryVoltageRampBusy(ViSession vi, ViString PhaseName, ref ViBoolean Busy);
        ViStatus AbortVoltageRamp(ViSession vi, ViString PhaseName);

        /*- IviACPwrCurrentRamp Functions -*/
        ViStatus RampCurrent(ViSession vi, ViString PhaseName, ViReal64 StartCurrent, ViReal64 EndCurrent, ViReal64 Duration);
        ViStatus QueryCurrentRampBusy(ViSession vi, ViString PhaseName, ref ViBoolean Busy);
        ViStatus AbortCurrentRamp(ViSession vi, ViString PhaseName);

        /*- IviACPwrFrequencyRamp Functions -*/
        ViStatus RampFrequency(ViSession vi, ViReal64 StartFrequency, ViReal64 EndFrequency, ViReal64 Duration);
        ViStatus QueryFrequencyRampBusy(ViSession vi, ref ViBoolean Busy);
        ViStatus AbortFrequencyRamp(ViSession vi);
    }
}
