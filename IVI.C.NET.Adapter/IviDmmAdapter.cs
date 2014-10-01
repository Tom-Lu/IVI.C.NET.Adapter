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
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Ivi.Dmm;
using IVI.C.NET.Adapter.IviCInterop;

namespace IVI.C.NET.Adapter
{
    public class IviDmmAdapter : DriverAdapterBase<IviCInterop.IviDmm>, IIviDmm
    {
        #region Enum Mapping

        private static IviEnumCMapping<MeasurementFunction, int> DmmMeasurementFunction = IviEnumCMapping<MeasurementFunction, int>.Instance
            .Map(MeasurementFunction.DCVolts, IviDmmAttribute.IVIDMM_VAL_DC_VOLTS)
            .Map(MeasurementFunction.ACVolts, IviDmmAttribute.IVIDMM_VAL_AC_VOLTS)
            .Map(MeasurementFunction.DCCurrent, IviDmmAttribute.IVIDMM_VAL_DC_CURRENT)
            .Map(MeasurementFunction.ACCurrent, IviDmmAttribute.IVIDMM_VAL_AC_CURRENT)
            .Map(MeasurementFunction.TwoWireResistance, IviDmmAttribute.IVIDMM_VAL_2_WIRE_RES)
            .Map(MeasurementFunction.FourWireResistance, IviDmmAttribute.IVIDMM_VAL_4_WIRE_RES)
            .Map(MeasurementFunction.ACPlusDCVolts, IviDmmAttribute.IVIDMM_VAL_AC_PLUS_DC_VOLTS)
            .Map(MeasurementFunction.ACPlusDCCurrent, IviDmmAttribute.IVIDMM_VAL_AC_PLUS_DC_CURRENT)
            .Map(MeasurementFunction.Frequency, IviDmmAttribute.IVIDMM_VAL_FREQ)
            .Map(MeasurementFunction.Period, IviDmmAttribute.IVIDMM_VAL_PERIOD)
            .Map(MeasurementFunction.Temperature, IviDmmAttribute.IVIDMM_VAL_TEMPERATURE);

        private static IviEnumCMapping<Auto, double> DmmAutoRange = IviEnumCMapping<Auto, double>.Instance
            .Map(Auto.On, IviDmmAttribute.IVIDMM_VAL_AUTO_RANGE_ON)
            .Map(Auto.Once, IviDmmAttribute.IVIDMM_VAL_AUTO_RANGE_ONCE)
            .Map(Auto.Off, IviDmmAttribute.IVIDMM_VAL_AUTO_RANGE_OFF);

        private static IviEnumCMapping<Auto, int> DmmAutoZero = IviEnumCMapping<Auto, int>.Instance
            .Map(Auto.On, IviDmmAttribute.IVIDMM_VAL_AUTO_ZERO_ON)
            .Map(Auto.Once, IviDmmAttribute.IVIDMM_VAL_AUTO_ZERO_ONCE)
            .Map(Auto.Off, IviDmmAttribute.IVIDMM_VAL_AUTO_ZERO_OFF);

        private static IviEnumCMapping<ApertureTimeUnits, int> DmmApertureTimeUnits = IviEnumCMapping<ApertureTimeUnits, int>.Instance
            .Map(ApertureTimeUnits.Seconds, IviDmmAttribute.IVIDMM_VAL_SECONDS)
            .Map(ApertureTimeUnits.PowerlineCycles, IviDmmAttribute.IVIDMM_VAL_POWER_LINE_CYCLES);

        private static IviEnumCMapping<TransducerType, int> DmmTransducerType = IviEnumCMapping<TransducerType, int>.Instance
            .Map(TransducerType.Thermocouple, IviDmmAttribute.IVIDMM_VAL_THERMOCOUPLE)
            .Map(TransducerType.Thermistor, IviDmmAttribute.IVIDMM_VAL_THERMISTOR)
            .Map(TransducerType.TwoWireRtd, IviDmmAttribute.IVIDMM_VAL_2_WIRE_RTD)
            .Map(TransducerType.FourWireRtd, IviDmmAttribute.IVIDMM_VAL_4_WIRE_RTD);

        private static IviEnumCMapping<ReferenceJunctionType, int> DmmReferenceJunctionType = IviEnumCMapping<ReferenceJunctionType, int>.Instance
            .Map(ReferenceJunctionType.Internal, IviDmmAttribute.IVIDMM_VAL_TEMP_REF_JUNC_INTERNAL)
            .Map(ReferenceJunctionType.Fixed, IviDmmAttribute.IVIDMM_VAL_TEMP_REF_JUNC_FIXED);

        private static IviEnumCMapping<ThermocoupleType, int> DmmThermocoupleType = IviEnumCMapping<ThermocoupleType, int>.Instance
            .Map(ThermocoupleType.B, IviDmmAttribute.IVIDMM_VAL_TEMP_TC_B)
            .Map(ThermocoupleType.C, IviDmmAttribute.IVIDMM_VAL_TEMP_TC_C)
            .Map(ThermocoupleType.D, IviDmmAttribute.IVIDMM_VAL_TEMP_TC_D)
            .Map(ThermocoupleType.E, IviDmmAttribute.IVIDMM_VAL_TEMP_TC_E)
            .Map(ThermocoupleType.G, IviDmmAttribute.IVIDMM_VAL_TEMP_TC_G)
            .Map(ThermocoupleType.J, IviDmmAttribute.IVIDMM_VAL_TEMP_TC_J)
            .Map(ThermocoupleType.K, IviDmmAttribute.IVIDMM_VAL_TEMP_TC_K)
            .Map(ThermocoupleType.N, IviDmmAttribute.IVIDMM_VAL_TEMP_TC_N)
            .Map(ThermocoupleType.R, IviDmmAttribute.IVIDMM_VAL_TEMP_TC_R)
            .Map(ThermocoupleType.S, IviDmmAttribute.IVIDMM_VAL_TEMP_TC_S)
            .Map(ThermocoupleType.T, IviDmmAttribute.IVIDMM_VAL_TEMP_TC_T)
            .Map(ThermocoupleType.U, IviDmmAttribute.IVIDMM_VAL_TEMP_TC_U)
            .Map(ThermocoupleType.V, IviDmmAttribute.IVIDMM_VAL_TEMP_TC_V);


        private static IviEnumCMapping<Slope, int> DmmSlope = IviEnumCMapping<Slope, int>.Instance
            .Map(Slope.Positive, IviDmmAttribute.IVIDMM_VAL_POSITIVE)
            .Map(Slope.Negative, IviDmmAttribute.IVIDMM_VAL_NEGATIVE);

        private static IviEnumCMapping<string, int> DmmTriggerSource = IviEnumCMapping<string, int>.Instance
            .Map("", IviDmmAttribute.IVIDMM_VAL_NONE)
            .Map("Immediate", IviDmmAttribute.IVIDMM_VAL_IMMEDIATE)
            .Map("External", IviDmmAttribute.IVIDMM_VAL_EXTERNAL)
            .Map("Software", IviDmmAttribute.IVIDMM_VAL_SOFTWARE_TRIG)
            .Map("TTL0", IviDmmAttribute.IVIDMM_VAL_TTL0)
            .Map("TTL1", IviDmmAttribute.IVIDMM_VAL_TTL1)
            .Map("TTL2", IviDmmAttribute.IVIDMM_VAL_TTL2)
            .Map("TTL3", IviDmmAttribute.IVIDMM_VAL_TTL3)
            .Map("TTL4", IviDmmAttribute.IVIDMM_VAL_TTL4)
            .Map("TTL5", IviDmmAttribute.IVIDMM_VAL_TTL5)
            .Map("TTL6", IviDmmAttribute.IVIDMM_VAL_TTL6)
            .Map("TTL7", IviDmmAttribute.IVIDMM_VAL_TTL7)
            .Map("ECL0", IviDmmAttribute.IVIDMM_VAL_ECL0)
            .Map("ECL1", IviDmmAttribute.IVIDMM_VAL_ECL1)
            .Map("PXI_STAR", IviDmmAttribute.IVIDMM_VAL_PXI_STAR)
            .Map("RTSI0", IviDmmAttribute.IVIDMM_VAL_RTSI_0)
            .Map("RTSI1", IviDmmAttribute.IVIDMM_VAL_RTSI_1)
            .Map("RTSI2", IviDmmAttribute.IVIDMM_VAL_RTSI_2)
            .Map("RTSI3", IviDmmAttribute.IVIDMM_VAL_RTSI_3)
            .Map("RTSI4", IviDmmAttribute.IVIDMM_VAL_RTSI_4)
            .Map("RTSI5", IviDmmAttribute.IVIDMM_VAL_RTSI_5)
            .Map("RTSI6", IviDmmAttribute.IVIDMM_VAL_RTSI_6);

        #endregion

        private IIviDmmAC DmmAC = null;
        private IIviDmmAdvanced DmmAdvanced = null;
        private IIviDmmFrequency DmmFrequency = null;
        private IIviDmmMeasurement DmmMeasurement = null;
        private IIviDmmTemperature DmmTemperature = null;
        private IIviDmmTrigger DmmTrigger = null;

        public IviDmmAdapter(string name, bool idQuery, bool reset, string options)
            : base(name, idQuery, reset, options)
        {
            DmmAC = new IviDmmAC(this);
            DmmAdvanced = new IviDmmAdvanced(this);
            DmmFrequency = new IviDmmFrequency(this);
            DmmMeasurement = new IviDmmMeasurement(this);
            DmmTemperature = new IviDmmTemperature(this);
            DmmTrigger = new IviDmmTrigger(this);
        }

        public IIviDmmAC AC
        {
            get { return DmmAC; }
        }

        public IIviDmmAdvanced Advanced
        {
            get { return DmmAdvanced; }
        }

        public Auto AutoRange
        {
            get
            {
                double range_value = GetAttributeViReal64(IviDmmAttribute.IVIDMM_ATTR_RANGE);
                if (range_value >= 0)
                    return Auto.Off;
                else
                    return DmmAutoRange.getEnum(range_value);
            }
            set
            {
                SetAttributeViReal64(IviDmmAttribute.IVIDMM_ATTR_RANGE, DmmAutoRange.getC_Value(value));
            }
        }

        public void Configure(MeasurementFunction measurementFunction, Auto autoRange, double resolution)
        {
            ViSessionStatusCheck(((IviCInterop.IviDmm)Interop).ConfigureMeasurement(Session, DmmMeasurementFunction.getC_Value(measurementFunction), DmmAutoRange.getC_Value(autoRange), resolution));
        }

        public void Configure(MeasurementFunction measurementFunction, double range, double resolution)
        {
            ViSessionStatusCheck(((IviCInterop.IviDmm)Interop).ConfigureMeasurement(Session, DmmMeasurementFunction.getC_Value(measurementFunction), range, resolution));
        }

        public IIviDmmFrequency Frequency
        {
            get { return DmmFrequency; }
        }

        public IIviDmmMeasurement Measurement
        {
            get { return DmmMeasurement; }
        }

        public MeasurementFunction MeasurementFunction
        {
            get
            {
                return DmmMeasurementFunction.getEnum(GetAttributeViInt32(IviDmmAttribute.IVIDMM_ATTR_FUNCTION));
            }
            set
            {
                SetAttributeViInt32(IviDmmAttribute.IVIDMM_ATTR_FUNCTION, DmmMeasurementFunction.getC_Value(value));
            }
        }

        public double Range
        {
            get
            {
                return GetAttributeViReal64(IviDmmAttribute.IVIDMM_ATTR_AUTO_RANGE_VALUE);
            }
            set
            {
                SetAttributeViReal64(IviDmmAttribute.IVIDMM_ATTR_RANGE, value);
            }
        }

        public double Resolution
        {
            get
            {
                return GetAttributeViReal64(IviDmmAttribute.IVIDMM_ATTR_RESOLUTION_ABSOLUTE);
            }
            set
            {
                SetAttributeViReal64(IviDmmAttribute.IVIDMM_ATTR_RESOLUTION_ABSOLUTE, value);
            }
        }

        public IIviDmmTemperature Temperature
        {
            get { return DmmTemperature; }
        }

        public IIviDmmTrigger Trigger
        {
            get { return DmmTrigger; }
        }

        internal class IviDmmAC : IIviDmmAC
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviDmm IviDmmInterop = null;
            public IviDmmAC(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviDmmInterop = (IviCInterop.IviDmm)Adapter.Interop;

            }

            public void ConfigureBandwidth(double minFreq, double maxFreq)
            {
                Adapter.ViSessionStatusCheck(IviDmmInterop.ConfigureACBandwidth(Adapter.Session, minFreq, maxFreq));
            }

            public double FrequencyMax
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviDmmAttribute.IVIDMM_ATTR_AC_MAX_FREQ);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviDmmAttribute.IVIDMM_ATTR_AC_MAX_FREQ, value);
                }
            }

            public double FrequencyMin
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviDmmAttribute.IVIDMM_ATTR_AC_MIN_FREQ);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviDmmAttribute.IVIDMM_ATTR_AC_MIN_FREQ, value);
                }
            }
        }

        internal class IviDmmAdvanced : IIviDmmAdvanced
        {
            private IDriverAdapterBase Adapter = null;
            public IviDmmAdvanced(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
            }

            public double ApertureTime
            {
                get { return Adapter.GetAttributeViReal64(IviDmmAttribute.IVIDMM_ATTR_APERTURE_TIME); }
            }

            public ApertureTimeUnits ApertureTimeUnits
            {
                get { return DmmApertureTimeUnits.getEnum(Adapter.GetAttributeViInt32(IviDmmAttribute.IVIDMM_ATTR_APERTURE_TIME_UNITS)); }
            }

            public Auto AutoZero
            {
                get
                {
                    return DmmAutoZero.getEnum(Adapter.GetAttributeViInt32(IviDmmAttribute.IVIDMM_ATTR_AUTO_ZERO));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviDmmAttribute.IVIDMM_ATTR_AUTO_ZERO, DmmAutoZero.getC_Value(value));
                }
            }

            public double PowerlineFrequency
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviDmmAttribute.IVIDMM_ATTR_POWERLINE_FREQ);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviDmmAttribute.IVIDMM_ATTR_POWERLINE_FREQ, value);
                }
            }
        }

        internal class IviDmmFrequency : IIviDmmFrequency
        {
            private IDriverAdapterBase Adapter = null;
            public IviDmmFrequency(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
            }

            public bool VoltageAutoRange
            {
                get
                {
                    // due to difference between IVI-C and IVI.NET driver, this property will always return false.
                    return Adapter.GetAttributeViReal64(IviDmmAttribute.IVIDMM_ATTR_FREQ_VOLTAGE_RANGE) == IviDmmAttribute.IVIDMM_VAL_AUTO_RANGE_ON;
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviDmmAttribute.IVIDMM_ATTR_FREQ_VOLTAGE_RANGE, value ? IviDmmAttribute.IVIDMM_VAL_AUTO_RANGE_ON : IviDmmAttribute.IVIDMM_VAL_AUTO_RANGE_OFF);
                }
            }

            public double VoltageRange
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviDmmAttribute.IVIDMM_ATTR_FREQ_VOLTAGE_RANGE);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviDmmAttribute.IVIDMM_ATTR_FREQ_VOLTAGE_RANGE, value);
                }
            }
        }

        internal class IviDmmMeasurement : IIviDmmMeasurement
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviDmm IviDmmInterop = null;
            public IviDmmMeasurement(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviDmmInterop = (IviCInterop.IviDmm)Adapter.Interop;
            }

            public void Abort()
            {
                Adapter.ViSessionStatusCheck(IviDmmInterop.Abort(Adapter.Session));
            }

            public double Fetch(Ivi.Driver.PrecisionTimeSpan maximumTime)
            {
                double reading = double.NaN;
                Adapter.ViSessionStatusCheck(IviDmmInterop.Fetch(Adapter.Session, (int)maximumTime.TotalMilliseconds, ref reading));
                return reading;
            }

            public double[] FetchMultiPoint(Ivi.Driver.PrecisionTimeSpan maximumTime, int numberOfMeasurements)
            {
                IntPtr pMeasure = Marshal.AllocHGlobal(numberOfMeasurements * sizeof(double));
                int actualPts = 0;
                Adapter.ViSessionStatusCheck(IviDmmInterop.FetchMultiPoint(Adapter.Session, (int)maximumTime.TotalMilliseconds, numberOfMeasurements, pMeasure, ref actualPts));
                double[] reading = new double[actualPts];
                Marshal.Copy(pMeasure, reading, 0, actualPts);
                Marshal.FreeHGlobal(pMeasure);
                return reading;
            }

            public double[] FetchMultiPoint(Ivi.Driver.PrecisionTimeSpan maximumTime)
            {
                int TriggerCount = Adapter.GetAttributeViInt32(IviDmmAttribute.IVIDMM_ATTR_TRIGGER_COUNT);
                int SampleCount = Adapter.GetAttributeViInt32(IviDmmAttribute.IVIDMM_ATTR_SAMPLE_COUNT);
                return FetchMultiPoint(maximumTime, TriggerCount * SampleCount);
            }

            public void Initiate()
            {
                Adapter.ViSessionStatusCheck(IviDmmInterop.Initiate(Adapter.Session));
            }

            public bool IsOutOfRange(double measurementValue)
            {
                return IsOverRange(measurementValue) || IsUnderRange(measurementValue);
            }

            public bool IsOverRange(double measurementValue)
            {
                bool isOverRange = false;
                Adapter.ViSessionStatusCheck(IviDmmInterop.IsOverRange(Adapter.Session, measurementValue, ref isOverRange));
                return isOverRange;
            }

            public bool IsUnderRange(double measurementValue)
            {
                bool isUnderRange = false;
                if (measurementValue < 0)
                {
                    Adapter.ViSessionStatusCheck(IviDmmInterop.IsOverRange(Adapter.Session, Math.Abs(measurementValue), ref isUnderRange));
                }
                return isUnderRange;
            }

            public double Read(Ivi.Driver.PrecisionTimeSpan maximumTime)
            {
                double reading = double.NaN;
                Adapter.ViSessionStatusCheck(IviDmmInterop.Read(Adapter.Session, (int)maximumTime.TotalMilliseconds, ref reading));
                return reading;
            }

            public double[] ReadMultiPoint(Ivi.Driver.PrecisionTimeSpan maximumTime, int numberOfMeasurements)
            {
                IntPtr pMeasure = Marshal.AllocHGlobal(numberOfMeasurements * sizeof(double));
                int actualPts = 0;
                Adapter.ViSessionStatusCheck(IviDmmInterop.ReadMultiPoint(Adapter.Session, (int)maximumTime.TotalMilliseconds, numberOfMeasurements, pMeasure, ref actualPts));
                double[] reading = new double[actualPts];
                Marshal.Copy(pMeasure, reading, 0, actualPts);
                Marshal.FreeHGlobal(pMeasure);
                return reading;
            }

            public double[] ReadMultiPoint(Ivi.Driver.PrecisionTimeSpan maximumTime)
            {
                int TriggerCount = Adapter.GetAttributeViInt32(IviDmmAttribute.IVIDMM_ATTR_TRIGGER_COUNT);
                int SampleCount = Adapter.GetAttributeViInt32(IviDmmAttribute.IVIDMM_ATTR_SAMPLE_COUNT);
                return ReadMultiPoint(maximumTime, TriggerCount * SampleCount);
            }

            public void SendSoftwareTrigger()
            {
                Adapter.ViSessionStatusCheck(IviDmmInterop.SendSoftwareTrigger(Adapter.Session));
            }
        }

        internal class IviDmmTemperature : IIviDmmTemperature
        {
            private IDriverAdapterBase Adapter = null;
            private IIviDmmRtd DmmRtd = null;
            private IIviDmmThermistor DmmThermistor = null;
            private IIviDmmThermocouple DmmThermocouple = null;
            public IviDmmTemperature(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                DmmRtd = new IviDmmRtd(Adapter);
                DmmThermistor = new IviDmmThermistor(Adapter);
                DmmThermocouple = new IviDmmThermocouple(Adapter);
            }

            public IIviDmmRtd Rtd
            {
                get { return DmmRtd; }
            }

            public IIviDmmThermistor Thermistor
            {
                get { return DmmThermistor; }
            }

            public IIviDmmThermocouple Thermocouple
            {
                get { return DmmThermocouple; }
            }

            public TransducerType TransducerType
            {
                get
                {
                    return DmmTransducerType.getEnum(Adapter.GetAttributeViInt32(IviDmmAttribute.IVIDMM_ATTR_TEMP_TRANSDUCER_TYPE));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviDmmAttribute.IVIDMM_ATTR_TEMP_TRANSDUCER_TYPE, DmmTransducerType.getC_Value(value));
                }
            }
        }

        internal class IviDmmRtd : IIviDmmRtd
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviDmm IviDmmInterop = null;
            public IviDmmRtd(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviDmmInterop = (IviCInterop.IviDmm)Adapter.Interop;
            }

            public double Alpha
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviDmmAttribute.IVIDMM_ATTR_TEMP_RTD_ALPHA);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviDmmAttribute.IVIDMM_ATTR_TEMP_RTD_ALPHA, value);
                }
            }

            public void Configure(double alpha, double resistance)
            {
                Adapter.ViSessionStatusCheck(IviDmmInterop.ConfigureRTD(Adapter.Session, alpha, resistance));
            }

            public double Resistance
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviDmmAttribute.IVIDMM_ATTR_TEMP_THERMISTOR_RES);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviDmmAttribute.IVIDMM_ATTR_TEMP_THERMISTOR_RES, value);
                }
            }
        }

        internal class IviDmmThermistor : IIviDmmThermistor
        {
            private IDriverAdapterBase Adapter = null;
            public IviDmmThermistor(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
            }

            public double Resistance
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviDmmAttribute.IVIDMM_ATTR_TEMP_THERMISTOR_RES);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviDmmAttribute.IVIDMM_ATTR_TEMP_THERMISTOR_RES, value);
                }
            }
        }

        internal class IviDmmThermocouple : IIviDmmThermocouple
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviDmm IviDmmInterop = null;
            public IviDmmThermocouple(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviDmmInterop = (IviCInterop.IviDmm)Adapter.Interop;
            }

            public void Configure(ThermocoupleType type, ReferenceJunctionType referenceJunctionType)
            {
                Adapter.ViSessionStatusCheck(IviDmmInterop.ConfigureThermocouple(Adapter.Session, DmmThermocoupleType.getC_Value(type), DmmReferenceJunctionType.getC_Value(referenceJunctionType)));
            }

            public double FixedReferenceJunction
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviDmmAttribute.IVIDMM_ATTR_TEMP_TC_FIXED_REF_JUNC);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviDmmAttribute.IVIDMM_ATTR_TEMP_TC_FIXED_REF_JUNC, value);
                }
            }

            public ReferenceJunctionType ReferenceJunctionType
            {
                get
                {
                    return DmmReferenceJunctionType.getEnum(Adapter.GetAttributeViInt32(IviDmmAttribute.IVIDMM_ATTR_TEMP_TC_REF_JUNC_TYPE));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviDmmAttribute.IVIDMM_ATTR_TEMP_TC_REF_JUNC_TYPE, DmmReferenceJunctionType.getC_Value(value));
                }
            }

            public ThermocoupleType Type
            {
                get
                {
                    return DmmThermocoupleType.getEnum(Adapter.GetAttributeViInt32(IviDmmAttribute.IVIDMM_ATTR_TEMP_TC_TYPE));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviDmmAttribute.IVIDMM_ATTR_TEMP_TC_TYPE, DmmThermocoupleType.getC_Value(value));
                }
            }
        }

        internal class IviDmmTrigger : IIviDmmTrigger
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviDmm IviDmmInterop = null;
            private IIviDmmMultiPoint DmmMultiPoint = null;

            public IviDmmTrigger(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviDmmInterop = (IviCInterop.IviDmm)Adapter.Interop;
                DmmMultiPoint = new IviDmmMultiPoint(Adapter);
            }

            public void Configure(string triggerSource, bool autoTriggerDelay)
            {
                Adapter.ViSessionStatusCheck(IviDmmInterop.ConfigureTrigger(Adapter.Session, DmmTriggerSource.getC_Value(triggerSource), autoTriggerDelay ? IviDmmAttribute.IVIDMM_VAL_AUTO_DELAY_ON : IviDmmAttribute.IVIDMM_VAL_AUTO_DELAY_OFF));
            }

            public void Configure(string triggerSource, Ivi.Driver.PrecisionTimeSpan triggerDelay)
            {
                Adapter.ViSessionStatusCheck(IviDmmInterop.ConfigureTrigger(Adapter.Session, DmmTriggerSource.getC_Value(triggerSource), triggerDelay.TotalSeconds));
            }

            public Ivi.Driver.PrecisionTimeSpan Delay
            {
                get
                {
                    return new Ivi.Driver.PrecisionTimeSpan((decimal)Adapter.GetAttributeViReal64(IviDmmAttribute.IVIDMM_ATTR_TRIGGER_DELAY));
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviDmmAttribute.IVIDMM_ATTR_TRIGGER_DELAY, value.TotalSeconds);
                }
            }

            public bool DelayAuto
            {
                get
                {
                    // due to difference between IVI-C and IVI.NET driver, this property will always return false.
                    return Adapter.GetAttributeViReal64(IviDmmAttribute.IVIDMM_ATTR_TRIGGER_DELAY) == IviDmmAttribute.IVIDMM_VAL_AUTO_DELAY_ON;
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviDmmAttribute.IVIDMM_ATTR_TRIGGER_DELAY, value ? IviDmmAttribute.IVIDMM_VAL_AUTO_DELAY_ON : IviDmmAttribute.IVIDMM_VAL_AUTO_DELAY_OFF);
                }
            }

            public string MeasurementCompleteDestination
            {
                get
                {
                    return DmmTriggerSource.getEnum(Adapter.GetAttributeViInt32(IviDmmAttribute.IVIDMM_ATTR_MEAS_COMPLETE_DEST));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviDmmAttribute.IVIDMM_ATTR_MEAS_COMPLETE_DEST, DmmTriggerSource.getC_Value(value));
                }
            }

            public IIviDmmMultiPoint MultiPoint
            {
                get { return DmmMultiPoint; }
            }

            public Slope Slope
            {
                get
                {
                    return DmmSlope.getEnum(Adapter.GetAttributeViInt32(IviDmmAttribute.IVIDMM_ATTR_TRIGGER_SLOPE));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviDmmAttribute.IVIDMM_ATTR_TRIGGER_SLOPE, DmmSlope.getC_Value(value));
                }
            }

            public string Source
            {
                get
                {
                    return DmmTriggerSource.getEnum(Adapter.GetAttributeViInt32(IviDmmAttribute.IVIDMM_ATTR_TRIGGER_SOURCE));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviDmmAttribute.IVIDMM_ATTR_TRIGGER_SOURCE, DmmTriggerSource.getC_Value(value));
                }
            }
        }

        internal class IviDmmMultiPoint : IIviDmmMultiPoint
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviDmm IviDmmInterop = null;
            public IviDmmMultiPoint(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviDmmInterop = (IviCInterop.IviDmm)Adapter.Interop;
            }

            public void Configure(int triggerCount, int sampleCount, string sampleTrigger, Ivi.Driver.PrecisionTimeSpan sampleInterval)
            {
                Adapter.ViSessionStatusCheck(IviDmmInterop.ConfigureMultiPoint(Adapter.Session, triggerCount, sampleCount, DmmTriggerSource.getC_Value(sampleTrigger), SampleInterval.TotalSeconds));
            }

            public int SampleCount
            {
                get
                {
                    return Adapter.GetAttributeViInt32(IviDmmAttribute.IVIDMM_ATTR_SAMPLE_COUNT);
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviDmmAttribute.IVIDMM_ATTR_SAMPLE_COUNT, value);
                }
            }

            public Ivi.Driver.PrecisionTimeSpan SampleInterval
            {
                get
                {
                    return new Ivi.Driver.PrecisionTimeSpan((decimal)Adapter.GetAttributeViReal64(IviDmmAttribute.IVIDMM_ATTR_SAMPLE_INTERVAL));
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviDmmAttribute.IVIDMM_ATTR_SAMPLE_INTERVAL, value.TotalSeconds);
                }
            }

            public string SampleTrigger
            {
                get
                {
                    return DmmTriggerSource.getEnum(Adapter.GetAttributeViInt32(IviDmmAttribute.IVIDMM_ATTR_SAMPLE_TRIGGER));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviDmmAttribute.IVIDMM_ATTR_SAMPLE_TRIGGER, DmmTriggerSource.getC_Value(value));
                }
            }

            public int TriggerCount
            {
                get
                {
                    return Adapter.GetAttributeViInt32(IviDmmAttribute.IVIDMM_ATTR_TRIGGER_COUNT);
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviDmmAttribute.IVIDMM_ATTR_TRIGGER_COUNT, value);
                }
            }
        }
    }
}
