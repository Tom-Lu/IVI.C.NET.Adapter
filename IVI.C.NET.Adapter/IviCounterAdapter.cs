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
using System.Collections.Generic;
using Ivi.Driver;
using Ivi.Counter;
using IVI.C.NET.Adapter.IviCInterop;
using System.Text;

namespace IVI.C.NET.Adapter
{
    public class IviCounterAdapter : DriverAdapterBase<IviCInterop.IviCounter>, IIviCounter
    {

        #region Enum Mapping

        private static IviEnumCMapping<Coupling, int> CounterCoupling = IviEnumCMapping<Coupling, int>.Instance
            .Map(Coupling.DC, IviCounterAttribute.IVICOUNTER_VAL_DC)
            .Map(Coupling.AC, IviCounterAttribute.IVICOUNTER_VAL_AC);

        private static IviEnumCMapping<ArmType, int> CounterArmType = IviEnumCMapping<ArmType, int>.Instance
            .Map(ArmType.Immediate, IviCounterAttribute.IVICOUNTER_VAL_IMMEDIATE_ARM_TYPE)
            .Map(ArmType.External, IviCounterAttribute.IVICOUNTER_VAL_EXTERNAL_ARM_TYPE);

        private static IviEnumCMapping<Slope, int> CounterSlop = IviEnumCMapping<Slope, int>.Instance
            .Map(Slope.Positive, IviCounterAttribute.IVICOUNTER_VAL_POSITIVE)
            .Map(Slope.Negative, IviCounterAttribute.IVICOUNTER_VAL_NEGATIVE);

        private static IviEnumCMapping<ReferenceType, int> CounterReferenceType = IviEnumCMapping<ReferenceType, int>.Instance
            .Map(ReferenceType.Voltage, IviCounterAttribute.IVICOUNTER_VAL_VOLTAGE_REFERENCE_TYPE)
            .Map(ReferenceType.Percent, IviCounterAttribute.IVICOUNTER_VAL_PERCENT_REFERENCE_TYPE);

        private static IviEnumCMapping<MeasurementStatus, int> CounterMeasurementStatus = IviEnumCMapping<MeasurementStatus, int>.Instance
            .Map(MeasurementStatus.Complete, IviCounterAttribute.IVICOUNTER_VAL_MEASUREMENT_COMPLETE)
            .Map(MeasurementStatus.InProgress, IviCounterAttribute.IVICOUNTER_VAL_MEASUREMENT_IN_PROGRESS)
            .Map(MeasurementStatus.Unknown, IviCounterAttribute.IVICOUNTER_VAL_MEASUREMENT_STATUS_UNKNOWN);

        private static IviEnumCMapping<MeasurementFunction, int> CounterMeasurementFunction = IviEnumCMapping<MeasurementFunction, int>.Instance
            .Map(MeasurementFunction.Frequency, IviCounterAttribute.IVICOUNTER_VAL_FREQUENCY)
            .Map(MeasurementFunction.FrequencyWithAperture, IviCounterAttribute.IVICOUNTER_VAL_FREQUENCY_WITH_APERTURE)
            .Map(MeasurementFunction.Period, IviCounterAttribute.IVICOUNTER_VAL_PERIOD)
            .Map(MeasurementFunction.PeriodWithAperture, IviCounterAttribute.IVICOUNTER_VAL_PERIOD_WITH_APERTURE)
            .Map(MeasurementFunction.PulseWidth, IviCounterAttribute.IVICOUNTER_VAL_PULSE_WIDTH)
            .Map(MeasurementFunction.DutyCycle, IviCounterAttribute.IVICOUNTER_VAL_DUTY_CYCLE)
            .Map(MeasurementFunction.EdgeTime, IviCounterAttribute.IVICOUNTER_VAL_EDGE_TIME)
            .Map(MeasurementFunction.FrequencyRatio, IviCounterAttribute.IVICOUNTER_VAL_FREQUENCY_RATIO)
            .Map(MeasurementFunction.TimeInterval, IviCounterAttribute.IVICOUNTER_VAL_TIME_INTERVAL)
            .Map(MeasurementFunction.Phase, IviCounterAttribute.IVICOUNTER_VAL_PHASE)
            .Map(MeasurementFunction.ContinuousTotalize, IviCounterAttribute.IVICOUNTER_VAL_CONTINUOUS_TOTALIZE)
            .Map(MeasurementFunction.GatedTotalize, IviCounterAttribute.IVICOUNTER_VAL_GATED_TOTALIZE)
            .Map(MeasurementFunction.TimedTotalize, IviCounterAttribute.IVICOUNTER_VAL_TIMED_TOTALIZE)
            .Map(MeasurementFunction.DCVoltage, IviCounterAttribute.IVICOUNTER_VAL_DC_VOLTAGE)
            .Map(MeasurementFunction.MaximumVoltage, IviCounterAttribute.IVICOUNTER_VAL_MAXIMUM_VOLTAGE)
            .Map(MeasurementFunction.MinimumVoltage, IviCounterAttribute.IVICOUNTER_VAL_MINIMUM_VOLTAGE)
            .Map(MeasurementFunction.RmsVoltage, IviCounterAttribute.IVICOUNTER_VAL_RMS_VOLTAGE)
            .Map(MeasurementFunction.PeakToPeakVoltage, IviCounterAttribute.IVICOUNTER_VAL_PEAK_TO_PEAK_VOLTAGE);

        #endregion

        private IIviCounterChannelCollection CounterChannelCollection = null;
        private IIviCounterArm CounterArm = null;
        private IIviCounterDutyCycle CounterDutyCycle = null;
        private IIviCounterEdgeTime CounterEdgeTime = null;
        private IIviCounterFrequency CounterFrequency = null;
        private IIviCounterFrequencyRatio CounterFrequencyRatio = null;
        private IIviCounterMeasurement CounterMeasurement = null;
        private IIviCounterPeriod CounterPeriod = null;
        private IIviCounterPhase CounterPhase = null;
        private IIviCounterPulseWidth CounterPulseWidth = null;
        private IIviCounterTimeInterval CounterTimeInterval = null;
        private IIviCounterTotalizeContinuous CounterTotalizeContinuous = null;
        private IIviCounterTotalizeGated CounterTotalizeGated = null;
        private IIviCounterTotalizeTimed CounterTotalizeTimed = null;
        private IIviCounterVoltage CounterVoltage = null;
        public IviCounterAdapter(string name, bool idQuery, bool reset, string options)
            : base(name, idQuery, reset, options)
        {
            CounterChannelCollection = new IviCounterChannelCollection(this);
            CounterArm = new IviCounterArm(this);
            CounterDutyCycle = new IviCounterDutyCycle(this);
            CounterEdgeTime = new IviCounterEdgeTime(this);
            CounterFrequency = new IviCounterFrequency(this);
            CounterFrequencyRatio = new IviCounterFrequencyRatio(this);
            CounterMeasurement = new IviCounterMeasurement(this);
            CounterPeriod = new IviCounterPeriod(this);
            CounterPhase = new IviCounterPhase(this);
            CounterPulseWidth = new IviCounterPulseWidth(this);
            CounterTimeInterval = new IviCounterTimeInterval(this);
            CounterTotalizeContinuous = new IviCounterTotalizeContinuous(this);
            CounterTotalizeGated = new IviCounterTotalizeGated(this);
            CounterTotalizeTimed = new IviCounterTotalizeTimed(this);
            CounterVoltage = new IviCounterVoltage(this);
        }

        public IIviCounterArm Arm
        {
            get { return CounterArm; }
        }

        public IIviCounterChannelCollection Channels
        {
            get { return CounterChannelCollection; }
        }

        public IIviCounterDutyCycle DutyCycle
        {
            get { return CounterDutyCycle; }
        }

        public IIviCounterEdgeTime EdgeTime
        {
            get { return CounterEdgeTime; }
        }

        public IIviCounterFrequency Frequency
        {
            get { return CounterFrequency; }
        }

        public IIviCounterFrequencyRatio FrequencyRatio
        {
            get { return CounterFrequencyRatio; }
        }

        public IIviCounterMeasurement Measurement
        {
            get { return CounterMeasurement; }
        }

        public MeasurementFunction MeasurementFunction
        {
            get
            {
                return CounterMeasurementFunction.getEnum(GetAttributeViInt32(IviCounterAttribute.IVICOUNTER_ATTR_MEASUREMENT_FUNCTION));
            }
            set
            {
                SetAttributeViInt32(IviCounterAttribute.IVICOUNTER_ATTR_MEASUREMENT_FUNCTION, CounterMeasurementFunction.getC_Value(value));
            }
        }

        public IIviCounterPeriod Period
        {
            get { return CounterPeriod; }
        }

        public IIviCounterPhase Phase
        {
            get { return CounterPhase; }
        }

        public IIviCounterPulseWidth PulseWidth
        {
            get { return CounterPulseWidth; }
        }

        public IIviCounterTimeInterval TimeInterval
        {
            get { return CounterTimeInterval; }
        }

        public IIviCounterTotalizeContinuous TotalizeContinuous
        {
            get { return CounterTotalizeContinuous; }
        }

        public IIviCounterTotalizeGated TotalizeGated
        {
            get { return CounterTotalizeGated; }
        }

        public IIviCounterTotalizeTimed TotalizeTimed
        {
            get { return CounterTotalizeTimed; }
        }

        public IIviCounterVoltage Voltage
        {
            get { return CounterVoltage; }
        }

        internal class IviCounterChannelCollection : IIviCounterChannelCollection
        {
            private IDriverAdapterBase Adapter;
            private IviCInterop.IviCounter IviCounterInterop = null;
            private IList<IIviCounterChannel> Channels = null;
            private IList<string> ChannelNames = null;
            public IviCounterChannelCollection(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviCounterInterop = (IviCInterop.IviCounter)Adapter.Interop;
                int OutputCount = Adapter.GetAttributeViInt32(IviDCPwrAttribute.IVIDCPWR_ATTR_CHANNEL_COUNT);

                Channels = new List<IIviCounterChannel>();
                ChannelNames = new List<string>();
                for (int Index = 1; Index <= OutputCount; Index++)
                {
                    IIviCounterChannel Channel = new IviCounterChannel(Adapter, Index);
                    Channels.Add(Channel);
                    ChannelNames.Add(Channel.Name);
                }
            }

            public int Count
            {
                get
                {
                    return Channels.Count;
                }
            }

            public IIviCounterChannel this[string name]
            {
                get { return Channels[ChannelNames.IndexOf(name)]; }
            }

            public IEnumerator<IIviCounterChannel> GetEnumerator()
            {
                return Channels.GetEnumerator();
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return Channels.GetEnumerator();
            }
        }

        internal class IviCounterChannel : IIviCounterChannel
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviCounter IviCounterInterop = null;
            private int Index;
            private string ChannelName;

            public IviCounterChannel(IDriverAdapterBase Adapter, int Index)
            {
                this.Adapter = Adapter;
                IviCounterInterop = (IviCInterop.IviCounter)Adapter.Interop;
                this.Index = Index;

                try
                {
                    StringBuilder NameValue = new StringBuilder(256);
                    Adapter.ViSessionStatusCheck(IviCounterInterop.GetChannelName(Adapter.Session, Index, NameValue.Capacity, NameValue));
                    ChannelName = NameValue.ToString();
                }
                catch
                {
                    ChannelName = string.Empty;
                }
            }

            public double Attenuation
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviCounterAttribute.IVICOUNTER_ATTR_ATTENUATION);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviCounterAttribute.IVICOUNTER_ATTR_ATTENUATION, value);
                }
            }

            public void Configure(double impedance, Coupling coupling, double attenuation)
            {
                Adapter.ViSessionStatusCheck(IviCounterInterop.ConfigureChannel(Adapter.Session, ChannelName, impedance, CounterCoupling.getC_Value(coupling), attenuation));
            }

            public void ConfigureFilter(double minimumFrequency, double maximumFrequency)
            {
                Adapter.ViSessionStatusCheck(IviCounterInterop.ConfigureFilter(Adapter.Session, ChannelName, minimumFrequency, maximumFrequency));
            }

            public void ConfigureLevel(double triggerLevel, double hysteresis)
            {
                Adapter.ViSessionStatusCheck(IviCounterInterop.ConfigureChannelLevel(Adapter.Session, ChannelName, triggerLevel, hysteresis));
            }

            public Coupling Coupling
            {
                get
                {
                    return CounterCoupling.getEnum(Adapter.GetAttributeViInt32(IviCounterAttribute.IVICOUNTER_ATTR_COUPLING));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviCounterAttribute.IVICOUNTER_ATTR_COUPLING, CounterCoupling.getC_Value(value));
                }
            }

            public bool FilterEnabled
            {
                get
                {
                    return Adapter.GetAttributeViBoolean(IviCounterAttribute.IVICOUNTER_ATTR_FILTER_ENABLED);
                }
                set
                {
                    Adapter.SetAttributeViBoolean(IviCounterAttribute.IVICOUNTER_ATTR_FILTER_ENABLED, value);
                }
            }

            public double Hysteresis
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviCounterAttribute.IVICOUNTER_ATTR_CHANNEL_HYSTERESIS);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviCounterAttribute.IVICOUNTER_ATTR_CHANNEL_HYSTERESIS, value);
                }
            }

            public double Impedance
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviCounterAttribute.IVICOUNTER_ATTR_IMPEDANCE);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviCounterAttribute.IVICOUNTER_ATTR_IMPEDANCE, value);
                }
            }

            public double Level
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviCounterAttribute.IVICOUNTER_ATTR_CHANNEL_LEVEL);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviCounterAttribute.IVICOUNTER_ATTR_CHANNEL_LEVEL, value);
                }
            }

            public double MaximumFrequency
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviCounterAttribute.IVICOUNTER_ATTR_FILTER_MAXIMUM_FREQUENCY);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviCounterAttribute.IVICOUNTER_ATTR_FILTER_MAXIMUM_FREQUENCY, value);
                }
            }

            public double MinimumFrequency
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviCounterAttribute.IVICOUNTER_ATTR_FILTER_MINIMUM_FREQUENCY);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviCounterAttribute.IVICOUNTER_ATTR_FILTER_MINIMUM_FREQUENCY, value);
                }
            }

            public Slope Slope
            {
                get
                {
                    return CounterSlop.getEnum(Adapter.GetAttributeViInt32(IviCounterAttribute.IVICOUNTER_ATTR_CHANNEL_SLOPE));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviCounterAttribute.IVICOUNTER_ATTR_CHANNEL_SLOPE, CounterSlop.getC_Value(value));
                }
            }

            public string Name
            {
                get { return ChannelName; }
            }
        }


        #region CounterArm

        internal class IviCounterArm : IIviCounterArm
        {
            private IIviCounterArmStart CounterArmStart = null;
            private IIviCounterArmStop CounterArmStop = null;
            public IviCounterArm(IDriverAdapterBase Adapter)
            {
                CounterArmStart = new IviCounterArmStart(Adapter);
                CounterArmStop = new IviCounterArmStop(Adapter);
            }

            public IIviCounterArmStart Start
            {
                get { return CounterArmStart; }
            }

            public IIviCounterArmStop Stop
            {
                get { return CounterArmStop; }
            }
        }

        internal class IviCounterArmStart : IIviCounterArmStart
        {
            private IDriverAdapterBase Adapter;
            private IviCInterop.IviCounter IviCounterInterop = null;
            private IIviCounterArmStartExternal CounterArmStartExternal = null;
            public IviCounterArmStart(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviCounterInterop = (IviCInterop.IviCounter)Adapter.Interop;
                CounterArmStartExternal = new IviCounterArmStartExternal(Adapter);
            }

            public IIviCounterArmStartExternal External
            {
                get { return CounterArmStartExternal; }
            }

            public ArmType Type
            {
                get
                {
                    return CounterArmType.getEnum(Adapter.GetAttributeViInt32(IviCounterAttribute.IVICOUNTER_ATTR_START_ARM_TYPE));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviCounterAttribute.IVICOUNTER_ATTR_START_ARM_TYPE, CounterArmType.getC_Value(value));
                }
            }
        }

        internal class IviCounterArmStartExternal : IIviCounterArmStartExternal
        {
            private IDriverAdapterBase Adapter;
            private IviCInterop.IviCounter IviCounterInterop = null;
            public IviCounterArmStartExternal(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviCounterInterop = (IviCInterop.IviCounter)Adapter.Interop;
            }

            public void Configure(string source, double level, Slope slope, Ivi.Driver.PrecisionTimeSpan delay)
            {
                Adapter.ViSessionStatusCheck(IviCounterInterop.ConfigureExternalStartArm(Adapter.Session, source, level, CounterSlop.getC_Value(slope), delay.TotalSeconds));
            }

            public Ivi.Driver.PrecisionTimeSpan Delay
            {
                get
                {
                    return new Ivi.Driver.PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * Adapter.GetAttributeViReal64(IviCounterAttribute.IVICOUNTER_ATTR_EXTERNAL_START_ARM_DELAY))));
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviCounterAttribute.IVICOUNTER_ATTR_EXTERNAL_START_ARM_DELAY, Delay.TotalSeconds);
                }
            }

            public double Level
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviCounterAttribute.IVICOUNTER_ATTR_EXTERNAL_START_ARM_LEVEL);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviCounterAttribute.IVICOUNTER_ATTR_EXTERNAL_START_ARM_LEVEL, value);
                }
            }

            public Slope Slope
            {
                get
                {
                    return CounterSlop.getEnum(Adapter.GetAttributeViInt32(IviCounterAttribute.IVICOUNTER_ATTR_EXTERNAL_START_ARM_SLOPE));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviCounterAttribute.IVICOUNTER_ATTR_EXTERNAL_START_ARM_SLOPE, CounterSlop.getC_Value(value));
                }
            }

            public string Source
            {
                get
                {
                    return Adapter.GetAttributeViString(IviCounterAttribute.IVICOUNTER_ATTR_EXTERNAL_START_ARM_SOURCE);
                }
                set
                {
                    Adapter.SetAttributeViString(IviCounterAttribute.IVICOUNTER_ATTR_EXTERNAL_START_ARM_SOURCE, value);
                }
            }
        }

        internal class IviCounterArmStop : IIviCounterArmStop
        {
            private IDriverAdapterBase Adapter;
            private IviCInterop.IviCounter IviCounterInterop = null;
            private IIviCounterArmStopExternal CounterArmStopExternal = null;
            public IviCounterArmStop(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviCounterInterop = (IviCInterop.IviCounter)Adapter.Interop;
                CounterArmStopExternal = new IviCounterArmStopExternal(Adapter);
            }

            public IIviCounterArmStopExternal External
            {
                get { return CounterArmStopExternal; }
            }

            public ArmType Type
            {
                get
                {
                    return CounterArmType.getEnum(Adapter.GetAttributeViInt32(IviCounterAttribute.IVICOUNTER_ATTR_STOP_ARM_TYPE));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviCounterAttribute.IVICOUNTER_ATTR_STOP_ARM_TYPE, CounterArmType.getC_Value(value));
                }
            }
        }

        internal class IviCounterArmStopExternal : IIviCounterArmStopExternal
        {
            private IDriverAdapterBase Adapter;
            private IviCInterop.IviCounter IviCounterInterop = null;
            public IviCounterArmStopExternal(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviCounterInterop = (IviCInterop.IviCounter)Adapter.Interop;
            }

            public void Configure(string source, double level, Slope slope, Ivi.Driver.PrecisionTimeSpan delay)
            {
                Adapter.ViSessionStatusCheck(IviCounterInterop.ConfigureExternalStopArm(Adapter.Session, source, level, CounterSlop.getC_Value(slope), delay.TotalSeconds));
            }

            public Ivi.Driver.PrecisionTimeSpan Delay
            {
                get
                {
                    return new Ivi.Driver.PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * Adapter.GetAttributeViReal64(IviCounterAttribute.IVICOUNTER_ATTR_EXTERNAL_STOP_ARM_DELAY))));
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviCounterAttribute.IVICOUNTER_ATTR_EXTERNAL_STOP_ARM_DELAY, value.TotalSeconds);
                }
            }

            public double Level
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviCounterAttribute.IVICOUNTER_ATTR_EXTERNAL_STOP_ARM_LEVEL);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviCounterAttribute.IVICOUNTER_ATTR_EXTERNAL_STOP_ARM_LEVEL, value);
                }
            }

            public Slope Slope
            {
                get
                {
                    return CounterSlop.getEnum(Adapter.GetAttributeViInt32(IviCounterAttribute.IVICOUNTER_ATTR_EXTERNAL_STOP_ARM_SLOPE));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviCounterAttribute.IVICOUNTER_ATTR_EXTERNAL_STOP_ARM_SLOPE, CounterSlop.getC_Value(value));
                }
            }

            public string Source
            {
                get
                {
                    return Adapter.GetAttributeViString(IviCounterAttribute.IVICOUNTER_ATTR_EXTERNAL_STOP_ARM_SOURCE);
                }
                set
                {
                    Adapter.SetAttributeViString(IviCounterAttribute.IVICOUNTER_ATTR_EXTERNAL_STOP_ARM_SOURCE, value);
                }
            }
        }

        #endregion

        internal class IviCounterDutyCycle : IIviCounterDutyCycle
        {
            private IDriverAdapterBase Adapter;
            private IviCInterop.IviCounter IviCounterInterop = null;
            public IviCounterDutyCycle(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviCounterInterop = (IviCInterop.IviCounter)Adapter.Interop;
            }

            public string Channel
            {
                get
                {
                    return Adapter.GetAttributeViString(IviCounterAttribute.IVICOUNTER_ATTR_DUTY_CYCLE_CHANNEL);
                }
                set
                {
                    Adapter.SetAttributeViString(IviCounterAttribute.IVICOUNTER_ATTR_DUTY_CYCLE_CHANNEL, value);
                }
            }

            public void Configure(string channel, double frequencyEstimate, double resolution)
            {
                Adapter.ViSessionStatusCheck(IviCounterInterop.ConfigureDutyCycle(Adapter.Session, channel, frequencyEstimate, resolution));
            }

            public double FrequencyEstimate
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviCounterAttribute.IVICOUNTER_ATTR_DUTY_CYCLE_FREQUENCY_ESTIMATE);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviCounterAttribute.IVICOUNTER_ATTR_DUTY_CYCLE_FREQUENCY_ESTIMATE, value);
                }
            }

            public double Resolution
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviCounterAttribute.IVICOUNTER_ATTR_DUTY_CYCLE_RESOLUTION);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviCounterAttribute.IVICOUNTER_ATTR_DUTY_CYCLE_RESOLUTION, value);
                }
            }
        }

        internal class IviCounterEdgeTime : IIviCounterEdgeTime
        {
            private IDriverAdapterBase Adapter;
            private IviCInterop.IviCounter IviCounterInterop = null;
            public IviCounterEdgeTime(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviCounterInterop = (IviCInterop.IviCounter)Adapter.Interop;
            }

            public string Channel
            {
                get
                {
                    return Adapter.GetAttributeViString(IviCounterAttribute.IVICOUNTER_ATTR_EDGE_TIME_CHANNEL);
                }
                set
                {
                    Adapter.SetAttributeViString(IviCounterAttribute.IVICOUNTER_ATTR_EDGE_TIME_CHANNEL, value);
                }
            }

            public void Configure(string channel, Ivi.Driver.PrecisionTimeSpan estimate, Ivi.Driver.PrecisionTimeSpan resolution)
            {
                Adapter.ViSessionStatusCheck(IviCounterInterop.ConfigureEdgeTime(Adapter.Session, channel, estimate.TotalSeconds, resolution.TotalSeconds));
            }

            public void ConfigureReferenceLevels(string channel, ReferenceType referenceType, Ivi.Driver.PrecisionTimeSpan estimate, Ivi.Driver.PrecisionTimeSpan resolution, double highReference, double lowReference)
            {
                Adapter.ViSessionStatusCheck(IviCounterInterop.ConfigureEdgeTimeReferenceLevels(Adapter.Session, channel, CounterReferenceType.getC_Value(referenceType), estimate.TotalSeconds, resolution.TotalSeconds, highReference, lowReference)); 
            }

            public Ivi.Driver.PrecisionTimeSpan Estimate
            {
                get
                {
                    return new Ivi.Driver.PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * Adapter.GetAttributeViReal64(IviCounterAttribute.IVICOUNTER_ATTR_EDGE_TIME_ESTIMATE))));
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviCounterAttribute.IVICOUNTER_ATTR_EXTERNAL_START_ARM_DELAY, value.TotalSeconds);
                }
            }

            public double HighReference
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviCounterAttribute.IVICOUNTER_ATTR_EDGE_TIME_HIGH_REFERENCE);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviCounterAttribute.IVICOUNTER_ATTR_EDGE_TIME_HIGH_REFERENCE, value);
                }
            }

            public double LowReference
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviCounterAttribute.IVICOUNTER_ATTR_EDGE_TIME_LOW_REFERENCE);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviCounterAttribute.IVICOUNTER_ATTR_EDGE_TIME_LOW_REFERENCE, value);
                }
            }

            public ReferenceType ReferenceType
            {
                get
                {
                    return CounterReferenceType.getEnum(Adapter.GetAttributeViInt32(IviCounterAttribute.IVICOUNTER_ATTR_EDGE_TIME_REFERENCE_TYPE));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviCounterAttribute.IVICOUNTER_ATTR_EDGE_TIME_REFERENCE_TYPE, CounterReferenceType.getC_Value(value));
                }
            }

            public Ivi.Driver.PrecisionTimeSpan Resolution
            {
                get
                {
                    return new Ivi.Driver.PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * Adapter.GetAttributeViReal64(IviCounterAttribute.IVICOUNTER_ATTR_EDGE_TIME_RESOLUTION))));
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviCounterAttribute.IVICOUNTER_ATTR_EDGE_TIME_RESOLUTION, value.TotalSeconds);
                }
            }
        }

        internal class IviCounterFrequency : IIviCounterFrequency
        {
            private IDriverAdapterBase Adapter;
            private IviCInterop.IviCounter IviCounterInterop = null;
            public IviCounterFrequency(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviCounterInterop = (IviCInterop.IviCounter)Adapter.Interop;
            }

            public PrecisionTimeSpan ApertureTime
            {
                get
                {
                    return new Ivi.Driver.PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * Adapter.GetAttributeViReal64(IviCounterAttribute.IVICOUNTER_ATTR_FREQUENCY_APERTURE_TIME))));
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviCounterAttribute.IVICOUNTER_ATTR_FREQUENCY_APERTURE_TIME, value.TotalSeconds);
                }
            }

            public string Channel
            {
                get
                {
                    return Adapter.GetAttributeViString(IviCounterAttribute.IVICOUNTER_ATTR_FREQUENCY_CHANNEL);
                }
                set
                {
                    Adapter.SetAttributeViString(IviCounterAttribute.IVICOUNTER_ATTR_FREQUENCY_CHANNEL, value);
                }
            }

            public void Configure(string channel)
            {
                Adapter.ViSessionStatusCheck(IviCounterInterop.ConfigureFrequency(Adapter.Session, channel));
            }

            public void ConfigureManual(string channel, bool estimateAuto, bool resolutionAuto)
            {
                Adapter.ViSessionStatusCheck(IviCounterInterop.ConfigureFrequencyManual(Adapter.Session, channel, Estimate, Resolution ));
                Adapter.SetAttributeViBoolean(IviCounterAttribute.IVICOUNTER_ATTR_FREQUENCY_ESTIMATE_AUTO, estimateAuto);
                Adapter.SetAttributeViBoolean(IviCounterAttribute.IVICOUNTER_ATTR_FREQUENCY_RESOLUTION_AUTO, resolutionAuto);
            }

            public void ConfigureManual(string channel, double estimate, bool resolutionAuto)
            {
                Adapter.ViSessionStatusCheck(IviCounterInterop.ConfigureFrequencyManual(Adapter.Session, channel, estimate, Resolution ));
                Adapter.SetAttributeViBoolean(IviCounterAttribute.IVICOUNTER_ATTR_FREQUENCY_RESOLUTION_AUTO, resolutionAuto);
            }

            public void ConfigureManual(string channel, bool estimateAuto, double resolution)
            {
                Adapter.ViSessionStatusCheck(IviCounterInterop.ConfigureFrequencyManual(Adapter.Session, channel, Estimate, resolution));
                Adapter.SetAttributeViBoolean(IviCounterAttribute.IVICOUNTER_ATTR_FREQUENCY_ESTIMATE_AUTO, estimateAuto);
            }

            public void ConfigureManual(string channel, double estimate, double resolution)
            {
                Adapter.ViSessionStatusCheck(IviCounterInterop.ConfigureFrequencyManual(Adapter.Session, channel, estimate, resolution));
            }

            public void ConfigureWithAperture(string channel, PrecisionTimeSpan apertureTime)
            {
                Adapter.ViSessionStatusCheck(IviCounterInterop.ConfigureFrequencyWithApertureTime(Adapter.Session, channel, apertureTime.TotalSeconds));
            }

            public double Estimate
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviCounterAttribute.IVICOUNTER_ATTR_FREQUENCY_ESTIMATE);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviCounterAttribute.IVICOUNTER_ATTR_FREQUENCY_ESTIMATE, value);
                }
            }

            public bool EstimateAuto
            {
                get
                {
                    return Adapter.GetAttributeViBoolean(IviCounterAttribute.IVICOUNTER_ATTR_FREQUENCY_ESTIMATE_AUTO);
                }
                set
                {
                    Adapter.SetAttributeViBoolean(IviCounterAttribute.IVICOUNTER_ATTR_FREQUENCY_ESTIMATE_AUTO, value);
                }
            }

            public double Resolution
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviCounterAttribute.IVICOUNTER_ATTR_FREQUENCY_RESOLUTION);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviCounterAttribute.IVICOUNTER_ATTR_FREQUENCY_RESOLUTION, value);
                }
            }

            public bool ResolutionAuto
            {
                get
                {
                    return Adapter.GetAttributeViBoolean(IviCounterAttribute.IVICOUNTER_ATTR_FREQUENCY_RESOLUTION_AUTO);
                }
                set
                {
                    Adapter.SetAttributeViBoolean(IviCounterAttribute.IVICOUNTER_ATTR_FREQUENCY_RESOLUTION_AUTO, value);
                }
            }
        }

        internal class IviCounterFrequencyRatio : IIviCounterFrequencyRatio
        {
            private IDriverAdapterBase Adapter;
            private IviCInterop.IviCounter IviCounterInterop = null;
            public IviCounterFrequencyRatio(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviCounterInterop = (IviCInterop.IviCounter)Adapter.Interop;
            }

            public void Configure(string numeratorChannel, string denominatorChannel, double numeratorFrequencyEstimate, double estimate, double resolution)
            {
                Adapter.ViSessionStatusCheck(IviCounterInterop.ConfigureFrequencyRatio(Adapter.Session, numeratorChannel, denominatorChannel, numeratorFrequencyEstimate, estimate, resolution));
            }

            public string DenominatorChannel
            {
                get
                {
                    return Adapter.GetAttributeViString(IviCounterAttribute.IVICOUNTER_ATTR_FREQUENCY_RATIO_DENOMINATOR_CHANNEL);
                }
                set
                {
                    Adapter.SetAttributeViString(IviCounterAttribute.IVICOUNTER_ATTR_FREQUENCY_RATIO_DENOMINATOR_CHANNEL, value);
                }
            }

            public double Estimate
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviCounterAttribute.IVICOUNTER_ATTR_FREQUENCY_RATIO_ESTIMATE);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviCounterAttribute.IVICOUNTER_ATTR_FREQUENCY_RATIO_ESTIMATE, value);
                }
            }

            public string NumeratorChannel
            {
                get
                {
                    return Adapter.GetAttributeViString(IviCounterAttribute.IVICOUNTER_ATTR_FREQUENCY_RATIO_NUMERATOR_CHANNEL);
                }
                set
                {
                    Adapter.SetAttributeViString(IviCounterAttribute.IVICOUNTER_ATTR_FREQUENCY_RATIO_NUMERATOR_CHANNEL, value);
                }
            }

            public double NumeratorFrequencyEstimate
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviCounterAttribute.IVICOUNTER_ATTR_FREQUENCY_RATIO_NUMERATOR_FREQUENCY_ESTIMATE);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviCounterAttribute.IVICOUNTER_ATTR_FREQUENCY_RATIO_NUMERATOR_FREQUENCY_ESTIMATE, value);
                }
            }

            public double Resolution
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviCounterAttribute.IVICOUNTER_ATTR_FREQUENCY_RATIO_RESOLUTION);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviCounterAttribute.IVICOUNTER_ATTR_FREQUENCY_RATIO_RESOLUTION, value);
                }
            }
        }

        internal class IviCounterMeasurement : IIviCounterMeasurement
        {
            private IDriverAdapterBase Adapter;
            private IviCInterop.IviCounter IviCounterInterop = null;
            public IviCounterMeasurement(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviCounterInterop = (IviCInterop.IviCounter)Adapter.Interop;
            }

            public void Abort()
            {
                Adapter.ViSessionStatusCheck(IviCounterInterop.Abort(Adapter.Session));
            }

            public double Fetch()
            {
                double measurement = double.NaN;
                Adapter.ViSessionStatusCheck(IviCounterInterop.Fetch(Adapter.Session, ref measurement));
                return measurement;
            }

            public MeasurementStatus GetMeasurementComplete()
            {
                int measurementStutus = 0;
                Adapter.ViSessionStatusCheck(IviCounterInterop.IsMeasurementComplete(Adapter.Session, ref measurementStutus));
                return CounterMeasurementStatus.getEnum(measurementStutus);
            }

            public void Initiate()
            {
                Adapter.ViSessionStatusCheck(IviCounterInterop.Initiate(Adapter.Session));
            }

            public double Read(PrecisionTimeSpan maxTime)
            {
                double read = double.NaN;
                Adapter.ViSessionStatusCheck(IviCounterInterop.Read(Adapter.Session, (int)maxTime.TotalMilliseconds, ref read));
                return read;
            }
        }

        internal class IviCounterPeriod : IIviCounterPeriod
        {
            private IDriverAdapterBase Adapter;
            private IviCInterop.IviCounter IviCounterInterop = null;
            public IviCounterPeriod(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviCounterInterop = (IviCInterop.IviCounter)Adapter.Interop;
            }

            public PrecisionTimeSpan ApertureTime
            {
                get
                {
                    return new Ivi.Driver.PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * Adapter.GetAttributeViReal64(IviCounterAttribute.IVICOUNTER_ATTR_PERIOD_APERTURE_TIME))));
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviCounterAttribute.IVICOUNTER_ATTR_PERIOD_APERTURE_TIME, value.TotalSeconds);
                }
            }

            public string Channel
            {
                get
                {
                    return Adapter.GetAttributeViString(IviCounterAttribute.IVICOUNTER_ATTR_PERIOD_CHANNEL);
                }
                set
                {
                    Adapter.SetAttributeViString(IviCounterAttribute.IVICOUNTER_ATTR_PERIOD_CHANNEL, value);
                }
            }

            public void Configure(string channel, PrecisionTimeSpan estimate, PrecisionTimeSpan resolution)
            {
                Adapter.ViSessionStatusCheck(IviCounterInterop.ConfigurePeriod(Adapter.Session, channel, estimate.TotalSeconds, resolution.TotalSeconds));
            }

            public void ConfigureWithAperture(string channel, PrecisionTimeSpan apertureTime)
            {
                Adapter.ViSessionStatusCheck(IviCounterInterop.ConfigurePeriodWithApertureTime(Adapter.Session, channel, apertureTime.TotalSeconds));
            }

            public PrecisionTimeSpan Estimate
            {
                get
                {
                    return new Ivi.Driver.PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * Adapter.GetAttributeViReal64(IviCounterAttribute.IVICOUNTER_ATTR_PERIOD_ESTIMATE))));
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviCounterAttribute.IVICOUNTER_ATTR_PERIOD_ESTIMATE, value.TotalSeconds);
                }
            }

            public PrecisionTimeSpan Resolution
            {
                get
                {
                    return new Ivi.Driver.PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * Adapter.GetAttributeViReal64(IviCounterAttribute.IVICOUNTER_ATTR_PERIOD_RESOLUTION))));
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviCounterAttribute.IVICOUNTER_ATTR_PERIOD_RESOLUTION, value.TotalSeconds);
                }
            }
        }

        internal class IviCounterPhase : IIviCounterPhase
        {
            private IDriverAdapterBase Adapter;
            private IviCInterop.IviCounter IviCounterInterop = null;
            public IviCounterPhase(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviCounterInterop = (IviCInterop.IviCounter)Adapter.Interop;
            }

            public void Configure(string inputChannel, string referenceChannel, double frequencyEstimate, double resolution)
            {
                Adapter.ViSessionStatusCheck(IviCounterInterop.ConfigurePhase(Adapter.Session, inputChannel, referenceChannel, frequencyEstimate, resolution));
            }

            public double FrequencyEstimate
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviCounterAttribute.IVICOUNTER_ATTR_PHASE_FREQUENCY_ESTIMATE);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviCounterAttribute.IVICOUNTER_ATTR_PHASE_FREQUENCY_ESTIMATE, value);
                }
            }

            public string InputChannel
            {
                get
                {
                    return Adapter.GetAttributeViString(IviCounterAttribute.IVICOUNTER_ATTR_PHASE_INPUT_CHANNEL);
                }
                set
                {
                    Adapter.SetAttributeViString(IviCounterAttribute.IVICOUNTER_ATTR_PHASE_INPUT_CHANNEL, value);
                }
            }

            public string ReferenceChannel
            {
                get
                {
                    return Adapter.GetAttributeViString(IviCounterAttribute.IVICOUNTER_ATTR_PHASE_REFERENCE_CHANNEL);
                }
                set
                {
                    Adapter.SetAttributeViString(IviCounterAttribute.IVICOUNTER_ATTR_PHASE_REFERENCE_CHANNEL, value);
                }
            }

            public double Resolution
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviCounterAttribute.IVICOUNTER_ATTR_PHASE_RESOLUTION);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviCounterAttribute.IVICOUNTER_ATTR_PHASE_RESOLUTION, value);
                }
            }
        }

        internal class IviCounterPulseWidth : IIviCounterPulseWidth
        {
            private IDriverAdapterBase Adapter;
            private IviCInterop.IviCounter IviCounterInterop = null;
            public IviCounterPulseWidth(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviCounterInterop = (IviCInterop.IviCounter)Adapter.Interop;
            }

            public string Channel
            {
                get
                {
                    return Adapter.GetAttributeViString(IviCounterAttribute.IVICOUNTER_ATTR_PULSE_WIDTH_CHANNEL);
                }
                set
                {
                    Adapter.SetAttributeViString(IviCounterAttribute.IVICOUNTER_ATTR_PULSE_WIDTH_CHANNEL, value);
                }
            }

            public void Configure(string channel, PrecisionTimeSpan estimate, PrecisionTimeSpan resolution)
            {
                Adapter.ViSessionStatusCheck(IviCounterInterop.ConfigurePulseWidth(Adapter.Session, channel, estimate.TotalSeconds, resolution.TotalSeconds));
            }

            public PrecisionTimeSpan Estimate
            {
                get
                {
                    return new Ivi.Driver.PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * Adapter.GetAttributeViReal64(IviCounterAttribute.IVICOUNTER_ATTR_PULSE_WIDTH_ESTIMATE))));
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviCounterAttribute.IVICOUNTER_ATTR_PULSE_WIDTH_ESTIMATE, value.TotalSeconds);
                }
            }

            public PrecisionTimeSpan Resolution
            {
                get
                {
                    return new Ivi.Driver.PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * Adapter.GetAttributeViReal64(IviCounterAttribute.IVICOUNTER_ATTR_PULSE_WIDTH_RESOLUTION))));
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviCounterAttribute.IVICOUNTER_ATTR_PULSE_WIDTH_RESOLUTION, value.TotalSeconds);
                }
            }
        }

        internal class IviCounterTimeInterval : IIviCounterTimeInterval
        {
            private IDriverAdapterBase Adapter;
            private IviCInterop.IviCounter IviCounterInterop = null;
            public IviCounterTimeInterval(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviCounterInterop = (IviCInterop.IviCounter)Adapter.Interop;
            }

            public void Configure(string startChannel, string stopChannel, PrecisionTimeSpan estimate, PrecisionTimeSpan resolution)
            {
                Adapter.ViSessionStatusCheck(IviCounterInterop.ConfigureTimeInterval(Adapter.Session, startChannel, stopChannel, estimate.TotalSeconds, resolution.TotalSeconds));
            }

            public PrecisionTimeSpan Estimate
            {
                get
                {
                    return new Ivi.Driver.PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * Adapter.GetAttributeViReal64(IviCounterAttribute.IVICOUNTER_ATTR_TIME_INTERVAL_ESTIMATE))));
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviCounterAttribute.IVICOUNTER_ATTR_TIME_INTERVAL_ESTIMATE, value.TotalSeconds);
                }
            }

            public PrecisionTimeSpan Resolution
            {
                get
                {
                    return new Ivi.Driver.PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * Adapter.GetAttributeViReal64(IviCounterAttribute.IVICOUNTER_ATTR_TIME_INTERVAL_RESOLUTION))));
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviCounterAttribute.IVICOUNTER_ATTR_TIME_INTERVAL_RESOLUTION, value.TotalSeconds);
                }
            }

            public string StartChannel
            {
                get
                {
                    return Adapter.GetAttributeViString(IviCounterAttribute.IVICOUNTER_ATTR_TIME_INTERVAL_START_CHANNEL);
                }
                set
                {
                    Adapter.SetAttributeViString(IviCounterAttribute.IVICOUNTER_ATTR_TIME_INTERVAL_START_CHANNEL, value);
                }
            }

            public string StopChannel
            {
                get
                {
                    return Adapter.GetAttributeViString(IviCounterAttribute.IVICOUNTER_ATTR_TIME_INTERVAL_STOP_CHANNEL);
                }
                set
                {
                    Adapter.SetAttributeViString(IviCounterAttribute.IVICOUNTER_ATTR_TIME_INTERVAL_STOP_CHANNEL, value);
                }
            }

            public PrecisionTimeSpan StopHoldoff
            {
                get
                {
                    return new Ivi.Driver.PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * Adapter.GetAttributeViReal64(IviCounterAttribute.IVICOUNTER_ATTR_TIME_INTERVAL_STOP_HOLDOFF))));
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviCounterAttribute.IVICOUNTER_ATTR_TIME_INTERVAL_STOP_HOLDOFF, value.TotalSeconds);
                }
            }
        }

        internal class IviCounterTotalizeContinuous : IIviCounterTotalizeContinuous
        {
            private IDriverAdapterBase Adapter;
            private IviCInterop.IviCounter IviCounterInterop = null;
            public IviCounterTotalizeContinuous(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviCounterInterop = (IviCInterop.IviCounter)Adapter.Interop;
            }

            public string Channel
            {
                get
                {
                    return Adapter.GetAttributeViString(IviCounterAttribute.IVICOUNTER_ATTR_CONTINUOUS_TOTALIZE_CHANNEL);
                }
                set
                {
                    Adapter.SetAttributeViString(IviCounterAttribute.IVICOUNTER_ATTR_CONTINUOUS_TOTALIZE_CHANNEL, value);
                }
            }

            public void Configure(string channel)
            {
                Adapter.ViSessionStatusCheck(IviCounterInterop.ConfigureContinuousTotalize(Adapter.Session, channel));
            }

            public int FetchCount()
            {
                int measurement = 0;
                Adapter.ViSessionStatusCheck(IviCounterInterop.FetchContinuousTotalizeCount(Adapter.Session, ref measurement));
                return measurement;
            }

            public void Start()
            {
                Adapter.ViSessionStatusCheck(IviCounterInterop.StartContinuousTotalize(Adapter.Session));
            }

            public void Stop()
            {
                Adapter.ViSessionStatusCheck(IviCounterInterop.StopContinuousTotalize(Adapter.Session));
            }
        }

        internal class IviCounterTotalizeGated : IIviCounterTotalizeGated
        {
            private IDriverAdapterBase Adapter;
            private IviCInterop.IviCounter IviCounterInterop = null;
            public IviCounterTotalizeGated(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviCounterInterop = (IviCInterop.IviCounter)Adapter.Interop;
            }

            public string Channel
            {
                get
                {
                    return Adapter.GetAttributeViString(IviCounterAttribute.IVICOUNTER_ATTR_GATED_TOTALIZE_CHANNEL);
                }
                set
                {
                    Adapter.SetAttributeViString(IviCounterAttribute.IVICOUNTER_ATTR_GATED_TOTALIZE_CHANNEL, value);
                }
            }

            public void Configure(string channel, string gateSource, Slope gateSlope)
            {
                Adapter.ViSessionStatusCheck(IviCounterInterop.ConfigureGatedTotalize(Adapter.Session, channel, gateSource, CounterSlop.getC_Value(gateSlope)));
            }

            public Slope GateSlope
            {
                get
                {
                    return CounterSlop.getEnum(Adapter.GetAttributeViInt32(IviCounterAttribute.IVICOUNTER_ATTR_GATED_TOTALIZE_GATE_SLOPE));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviCounterAttribute.IVICOUNTER_ATTR_GATED_TOTALIZE_GATE_SLOPE, CounterSlop.getC_Value(value));
                }
            }

            public string GateSource
            {
                get
                {
                    return Adapter.GetAttributeViString(IviCounterAttribute.IVICOUNTER_ATTR_GATED_TOTALIZE_GATE_SOURCE);
                }
                set
                {
                    Adapter.SetAttributeViString(IviCounterAttribute.IVICOUNTER_ATTR_GATED_TOTALIZE_GATE_SOURCE, value);
                }
            }
        }

        internal class IviCounterTotalizeTimed : IIviCounterTotalizeTimed
        {
            private IDriverAdapterBase Adapter;
            private IviCInterop.IviCounter IviCounterInterop = null;
            public IviCounterTotalizeTimed(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviCounterInterop = (IviCInterop.IviCounter)Adapter.Interop;
            }

            public string Channel
            {
                get
                {
                    return Adapter.GetAttributeViString(IviCounterAttribute.IVICOUNTER_ATTR_TIMED_TOTALIZE_CHANNEL);
                }
                set
                {
                    Adapter.SetAttributeViString(IviCounterAttribute.IVICOUNTER_ATTR_TIMED_TOTALIZE_CHANNEL, value);
                }
            }

            public void Configure(string channel, PrecisionTimeSpan gateTime)
            {
                Adapter.ViSessionStatusCheck(IviCounterInterop.ConfigureTimedTotalize(Adapter.Session, channel, gateTime.TotalSeconds));
            }

            public PrecisionTimeSpan GateTime
            {
                get
                {
                    return new Ivi.Driver.PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * Adapter.GetAttributeViReal64(IviCounterAttribute.IVICOUNTER_ATTR_TIMED_TOTALIZE_GATE_TIME))));
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviCounterAttribute.IVICOUNTER_ATTR_TIMED_TOTALIZE_GATE_TIME, value.TotalSeconds);
                }
            }
        }

        internal class IviCounterVoltage : IIviCounterVoltage
        {
            private IDriverAdapterBase Adapter;
            private IviCInterop.IviCounter IviCounterInterop = null;
            public IviCounterVoltage(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviCounterInterop = (IviCInterop.IviCounter)Adapter.Interop;
            }

            public string Channel
            {
                get
                {
                    return Adapter.GetAttributeViString(IviCounterAttribute.IVICOUNTER_ATTR_VOLTAGE_CHANNEL);
                }
                set
                {
                    Adapter.SetAttributeViString(IviCounterAttribute.IVICOUNTER_ATTR_VOLTAGE_CHANNEL, value);
                }
            }

            public void Configure(string channel, MeasurementFunction measurementFunction, double estimate, double resolution)
            {
                Adapter.ViSessionStatusCheck(IviCounterInterop.ConfigureVoltage(Adapter.Session, channel, CounterMeasurementFunction.getC_Value(measurementFunction), estimate, resolution ));
            }

            public double Estimate
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviCounterAttribute.IVICOUNTER_ATTR_VOLTAGE_ESTIMATE);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviCounterAttribute.IVICOUNTER_ATTR_VOLTAGE_ESTIMATE, value);
                }
            }

            public double Resolution
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviCounterAttribute.IVICOUNTER_ATTR_VOLTAGE_RESOLUTION);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviCounterAttribute.IVICOUNTER_ATTR_VOLTAGE_RESOLUTION, value);
                }
            }
        }
    
    }
}
