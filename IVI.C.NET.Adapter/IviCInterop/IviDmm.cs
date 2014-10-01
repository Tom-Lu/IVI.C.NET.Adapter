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
    public interface IviDmm : IviDriver
    {
        /*- IviDmmBase Functions -*/
        ViStatus ConfigureMeasurement(ViSession vi, ViInt32 function, ViReal64 range, ViReal64 resolution);
        ViStatus ConfigureTrigger(ViSession vi, ViInt32 triggerSource, ViReal64 triggerDelay);

        ViStatus Read(ViSession vi, ViInt32 MaxTimeMilliseconds, ref ViReal64 reading);
        ViStatus Fetch(ViSession vi, ViInt32 MaxTimeMilliseconds, ref ViReal64 reading);
        ViStatus Abort(ViSession vi);
        ViStatus Initiate(ViSession vi);

        ViStatus IsOverRange(ViSession vi, ViReal64 measurementValue, ref ViBoolean isOverRange);

        /*- IviDmmAcMeasurement Functions -*/
        ViStatus ConfigureACBandwidth(ViSession vi, ViReal64 minFreq, ViReal64 maxFreq);

        /*- IviDmmFrequencyMeasurement Functions -*/
        ViStatus ConfigureFrequencyVoltageRange(ViSession vi, ViReal64 frequencyVoltageRange);

        /*- IviDmmTemperatureMeasurement Functions -*/
        ViStatus ConfigureTransducerType(ViSession vi, ViInt32 transducerType);

        /*- IviDmmThermocouple Functions -*/
        ViStatus ConfigureFixedRefJunction(ViSession vi, ViReal64 fixedRefJunction);
        ViStatus ConfigureThermocouple(ViSession vi, ViInt32 thermocoupleType, ViInt32 refJunctionType);

        /*- IviDmmRTD Functions -*/
        ViStatus ConfigureRTD(ViSession vi, ViReal64 alpha, ViReal64 resistance);

        /*- IviDmmThermistor Functions -*/
        ViStatus ConfigureThermistor(ViSession vi, ViReal64 resistance);

        /*- IviDmmMultiPoint Functions -*/
        ViStatus ConfigureMeasCompleteDest(ViSession vi, ViInt32 measCompleteDest);
        ViStatus ConfigureMultiPoint(ViSession vi, ViInt32 triggerCount, ViInt32 sampleCount, ViInt32 sampleTrigger, ViReal64 sampleInterval);
        ViStatus ReadMultiPoint(ViSession vi, ViInt32 maxTime, ViInt32 arraySize, IntPtr readingArray, ref ViInt32 actualPts);
        ViStatus FetchMultiPoint(ViSession vi, ViInt32 maxTime, ViInt32 arraySize, IntPtr readingArray, ref ViInt32 actualPts);

        /*- IviDmmTriggerSlope Functions -*/
        ViStatus ConfigureTriggerSlope(ViSession vi, ViInt32 polarity);
        /*- IviDmmSoftwareTrigger Functions -*/
        ViStatus SendSoftwareTrigger(ViSession vi);
        /*- IviDmmDeviceInfo Functions -*/
        ViStatus GetApertureTimeInfo(ViSession vi, ref ViReal64 apertureTime, ref ViInt32 apertureTimeUnits);
        /*- IviDmmAutoRangeValue Functions -*/
        ViStatus GetAutoRangeValue(ViSession vi, ref ViReal64 autoRangeValue);
        /*- IviDmmAutoZero Functions -*/
        ViStatus ConfigureAutoZeroMode(ViSession vi, ViInt32 autoZeroMode);
        /*- IviDmmPowerLineFrequency Functions -*/
        ViStatus ConfigurePowerLineFrequency(ViSession vi, ViReal64 powerLineFreq);
    }
}
