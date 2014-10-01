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
using Ivi.Driver;
using Ivi.Scope;
using IVI.C.NET.Adapter.IviCInterop;

namespace IVI.C.NET.Adapter
{
    class IviScopeAdapter : DriverAdapterBase<IviCInterop.IviScope>, IIviScope
    {
        #region Enum Mapping

        private static IviEnumCMapping<VerticalCoupling, int> ScopeVerticalCoupling = IviEnumCMapping<VerticalCoupling, int>.Instance
            .Map(VerticalCoupling.AC, IviScopeAttribute.IVISCOPE_VAL_AC)
            .Map(VerticalCoupling.DC, IviScopeAttribute.IVISCOPE_VAL_DC)
            .Map(VerticalCoupling.Ground, IviScopeAttribute.IVISCOPE_VAL_GND);

        private static IviEnumCMapping<MeasurementFunction, int> ScopeMeasurementFunction = IviEnumCMapping<MeasurementFunction, int>.Instance
            .Map(MeasurementFunction.RiseTime, IviScopeAttribute.IVISCOPE_VAL_RISE_TIME)
            .Map(MeasurementFunction.FallTime, IviScopeAttribute.IVISCOPE_VAL_FALL_TIME)
            .Map(MeasurementFunction.Frequency, IviScopeAttribute.IVISCOPE_VAL_FREQUENCY)
            .Map(MeasurementFunction.Period, IviScopeAttribute.IVISCOPE_VAL_PERIOD)
            .Map(MeasurementFunction.VoltageRms, IviScopeAttribute.IVISCOPE_VAL_VOLTAGE_RMS)
            .Map(MeasurementFunction.VoltageCycleRms, IviScopeAttribute.IVISCOPE_VAL_VOLTAGE_CYCLE_RMS)
            .Map(MeasurementFunction.VoltageMax, IviScopeAttribute.IVISCOPE_VAL_VOLTAGE_MAX)
            .Map(MeasurementFunction.VoltageMin, IviScopeAttribute.IVISCOPE_VAL_VOLTAGE_MIN)
            .Map(MeasurementFunction.VoltagePeakToPeak, IviScopeAttribute.IVISCOPE_VAL_VOLTAGE_PEAK_TO_PEAK)
            .Map(MeasurementFunction.VoltageHigh, IviScopeAttribute.IVISCOPE_VAL_VOLTAGE_HIGH)
            .Map(MeasurementFunction.VoltageLow, IviScopeAttribute.IVISCOPE_VAL_VOLTAGE_LOW)
            .Map(MeasurementFunction.VoltageAverage, IviScopeAttribute.IVISCOPE_VAL_VOLTAGE_AVERAGE)
            .Map(MeasurementFunction.VoltageCycleAverage, IviScopeAttribute.IVISCOPE_VAL_VOLTAGE_CYCLE_AVERAGE)
            .Map(MeasurementFunction.WidthNegative, IviScopeAttribute.IVISCOPE_VAL_WIDTH_NEG)
            .Map(MeasurementFunction.WidthPositive, IviScopeAttribute.IVISCOPE_VAL_WIDTH_POS)
            .Map(MeasurementFunction.DutyCycleNegative, IviScopeAttribute.IVISCOPE_VAL_DUTY_CYCLE_NEG)
            .Map(MeasurementFunction.DutyCyclePositive, IviScopeAttribute.IVISCOPE_VAL_DUTY_CYCLE_POS)
            .Map(MeasurementFunction.Amplitude, IviScopeAttribute.IVISCOPE_VAL_AMPLITUDE)
            .Map(MeasurementFunction.Overshoot, IviScopeAttribute.IVISCOPE_VAL_OVERSHOOT)
            .Map(MeasurementFunction.Preshoot, IviScopeAttribute.IVISCOPE_VAL_PRESHOOT);

        private static IviEnumCMapping<TimeMeasurementFunction, int> ScopeTimeMeasurementFunction = IviEnumCMapping<TimeMeasurementFunction, int>.Instance
            .Map(TimeMeasurementFunction.RiseTime, IviScopeAttribute.IVISCOPE_VAL_RISE_TIME)
            .Map(TimeMeasurementFunction.FallTime, IviScopeAttribute.IVISCOPE_VAL_FALL_TIME)
            .Map(TimeMeasurementFunction.Period, IviScopeAttribute.IVISCOPE_VAL_PERIOD)
            .Map(TimeMeasurementFunction.WidthNegative, IviScopeAttribute.IVISCOPE_VAL_WIDTH_NEG)
            .Map(TimeMeasurementFunction.WidthPos, IviScopeAttribute.IVISCOPE_VAL_WIDTH_POS);

        private static IviEnumCMapping<Interpolation, int> ScopeInterpolation = IviEnumCMapping<Interpolation, int>.Instance
            .Map(Interpolation.None, IviScopeAttribute.IVISCOPE_VAL_NO_INTERPOLATION)
            .Map(Interpolation.SineXOverX, IviScopeAttribute.IVISCOPE_VAL_SINE_X)
            .Map(Interpolation.Linear, IviScopeAttribute.IVISCOPE_VAL_LINEAR);

        private static IviEnumCMapping<SampleMode, int> ScopeSampleMode = IviEnumCMapping<SampleMode, int>.Instance
            .Map(SampleMode.RealTime, IviScopeAttribute.IVISCOPE_VAL_REAL_TIME)
            .Map(SampleMode.EquivalentTime, IviScopeAttribute.IVISCOPE_VAL_EQUIVALENT_TIME);

        private static IviEnumCMapping<AcquisitionType, int> ScopeAcquisitionType = IviEnumCMapping<AcquisitionType, int>.Instance
            .Map(AcquisitionType.Normal, IviScopeAttribute.IVISCOPE_VAL_NORMAL)
            .Map(AcquisitionType.HighResolution, IviScopeAttribute.IVISCOPE_VAL_HI_RES)
            .Map(AcquisitionType.Average, IviScopeAttribute.IVISCOPE_VAL_AVERAGE)
            .Map(AcquisitionType.PeakDetect, IviScopeAttribute.IVISCOPE_VAL_PEAK_DETECT)
            .Map(AcquisitionType.Envelope, IviScopeAttribute.IVISCOPE_VAL_ENVELOPE);

        private static IviEnumCMapping<AcquisitionStatus, int> ScopeAcquisitionStatus = IviEnumCMapping<AcquisitionStatus, int>.Instance
            .Map(AcquisitionStatus.Complete, IviScopeAttribute.IVISCOPE_VAL_ACQ_COMPLETE)
            .Map(AcquisitionStatus.InProgress, IviScopeAttribute.IVISCOPE_VAL_ACQ_IN_PROGRESS)
            .Map(AcquisitionStatus.Unknown, IviScopeAttribute.IVISCOPE_VAL_ACQ_STATUS_UNKNOWN);

        private static IviEnumCMapping<TriggerType, int> ScopeTriggerType = IviEnumCMapping<TriggerType, int>.Instance
            .Map(TriggerType.Edge, IviScopeAttribute.IVISCOPE_VAL_EDGE_TRIGGER)
            .Map(TriggerType.TV, IviScopeAttribute.IVISCOPE_VAL_TV_TRIGGER)
            .Map(TriggerType.Runt, IviScopeAttribute.IVISCOPE_VAL_RUNT_TRIGGER)
            .Map(TriggerType.Glitch, IviScopeAttribute.IVISCOPE_VAL_GLITCH_TRIGGER)
            .Map(TriggerType.Width, IviScopeAttribute.IVISCOPE_VAL_WIDTH_TRIGGER)
            .Map(TriggerType.Immediate, IviScopeAttribute.IVISCOPE_VAL_IMMEDIATE_TRIGGER)
            .Map(TriggerType.ACLine, IviScopeAttribute.IVISCOPE_VAL_AC_LINE_TRIGGER);

        private static IviEnumCMapping<TriggerCoupling, int> ScopeTriggerCoupling = IviEnumCMapping<TriggerCoupling, int>.Instance
            .Map(TriggerCoupling.AC, IviScopeAttribute.IVISCOPE_VAL_AC)
            .Map(TriggerCoupling.DC, IviScopeAttribute.IVISCOPE_VAL_DC)
            .Map(TriggerCoupling.LowFrequencyReject, IviScopeAttribute.IVISCOPE_VAL_LF_REJECT)
            .Map(TriggerCoupling.HighFrequencyReject, IviScopeAttribute.IVISCOPE_VAL_HF_REJECT)
            .Map(TriggerCoupling.NoiseReject, IviScopeAttribute.IVISCOPE_VAL_NOISE_REJECT);

        private static IviEnumCMapping<TriggerModifier, int> ScopeTriggerModifier = IviEnumCMapping<TriggerModifier, int>.Instance
            .Map(TriggerModifier.None, IviScopeAttribute.IVISCOPE_VAL_NO_TRIGGER_MOD)
            .Map(TriggerModifier.Auto, IviScopeAttribute.IVISCOPE_VAL_AUTO)
            .Map(TriggerModifier.AutoLevel, IviScopeAttribute.IVISCOPE_VAL_AUTO_LEVEL);

        private static IviEnumCMapping<ACLineSlope, int> ScopeACLineSlope = IviEnumCMapping<ACLineSlope, int>.Instance
            .Map(ACLineSlope.Positive, IviScopeAttribute.IVISCOPE_VAL_AC_LINE_POSITIVE)
            .Map(ACLineSlope.Negative, IviScopeAttribute.IVISCOPE_VAL_AC_LINE_NEGATIVE)
            .Map(ACLineSlope.Either, IviScopeAttribute.IVISCOPE_VAL_AC_LINE_EITHER);

        private static IviEnumCMapping<Slope, int> ScopeSlope = IviEnumCMapping<Slope, int>.Instance
            .Map(Slope.Negative, IviScopeAttribute.IVISCOPE_VAL_NEGATIVE)
            .Map(Slope.Positive, IviScopeAttribute.IVISCOPE_VAL_POSITIVE);

        private static IviEnumCMapping<GlitchCondition, int> ScopeGlitchCondition = IviEnumCMapping<GlitchCondition, int>.Instance
            .Map(GlitchCondition.GreaterThan, IviScopeAttribute.IVISCOPE_VAL_GLITCH_GREATER_THAN)
            .Map(GlitchCondition.LessThan, IviScopeAttribute.IVISCOPE_VAL_GLITCH_LESS_THAN);

        private static IviEnumCMapping<TVSignalFormat, int> ScopeTVSignalFormat = IviEnumCMapping<TVSignalFormat, int>.Instance
            .Map(TVSignalFormat.Ntsc, IviScopeAttribute.IVISCOPE_VAL_NTSC)
            .Map(TVSignalFormat.Pal, IviScopeAttribute.IVISCOPE_VAL_PAL)
            .Map(TVSignalFormat.Secam, IviScopeAttribute.IVISCOPE_VAL_SECAM);

        private static IviEnumCMapping<TVTriggerEvent, int> ScopeTVTriggerEvent = IviEnumCMapping<TVTriggerEvent, int>.Instance
            .Map(TVTriggerEvent.Field1, IviScopeAttribute.IVISCOPE_VAL_TV_EVENT_FIELD1)
            .Map(TVTriggerEvent.Field2, IviScopeAttribute.IVISCOPE_VAL_TV_EVENT_FIELD2)
            .Map(TVTriggerEvent.AnyField, IviScopeAttribute.IVISCOPE_VAL_TV_EVENT_ANY_FIELD)
            .Map(TVTriggerEvent.AnyLine, IviScopeAttribute.IVISCOPE_VAL_TV_EVENT_ANY_LINE)
            .Map(TVTriggerEvent.LineNumber, IviScopeAttribute.IVISCOPE_VAL_TV_EVENT_LINE_NUMBER);

        private static IviEnumCMapping<TVTriggerPolarity, int> ScopeTVTriggerPolarity = IviEnumCMapping<TVTriggerPolarity, int>.Instance
            .Map(TVTriggerPolarity.Positive, IviScopeAttribute.IVISCOPE_VAL_TV_POSITIVE)
            .Map(TVTriggerPolarity.Negative, IviScopeAttribute.IVISCOPE_VAL_TV_NEGATIVE);

        private static IviEnumCMapping<WidthCondition, int> ScopeWidthCondition = IviEnumCMapping<WidthCondition, int>.Instance
            .Map(WidthCondition.Within, IviScopeAttribute.IVISCOPE_VAL_WIDTH_WITHIN)
            .Map(WidthCondition.Outside, IviScopeAttribute.IVISCOPE_VAL_WIDTH_OUTSIDE);

        private static IviEnumCMapping<Polarity, int> ScopeRuntTriggerPolarity = IviEnumCMapping<Polarity, int>.Instance
            .Map(Polarity.Positive, IviScopeAttribute.IVISCOPE_VAL_RUNT_POSITIVE)
            .Map(Polarity.Negative, IviScopeAttribute.IVISCOPE_VAL_RUNT_NEGATIVE)
            .Map(Polarity.Either, IviScopeAttribute.IVISCOPE_VAL_RUNT_EITHER);

        private static IviEnumCMapping<Polarity, int> ScopeGlitchTriggerPolarity = IviEnumCMapping<Polarity, int>.Instance
            .Map(Polarity.Positive, IviScopeAttribute.IVISCOPE_VAL_GLITCH_POSITIVE)
            .Map(Polarity.Negative, IviScopeAttribute.IVISCOPE_VAL_GLITCH_NEGATIVE)
            .Map(Polarity.Either, IviScopeAttribute.IVISCOPE_VAL_GLITCH_EITHER);

        private static IviEnumCMapping<Polarity, int> ScopeWidthTriggerPolarity = IviEnumCMapping<Polarity, int>.Instance
            .Map(Polarity.Positive, IviScopeAttribute.IVISCOPE_VAL_WIDTH_POSITIVE)
            .Map(Polarity.Negative, IviScopeAttribute.IVISCOPE_VAL_WIDTH_NEGATIVE)
            .Map(Polarity.Either, IviScopeAttribute.IVISCOPE_VAL_WIDTH_EITHER);

        #endregion

        private IIviScopeAcquisition ScopeAcquisition = null;
        private IIviScopeMeasurement ScopeMeasurement = null;
        private IIviScopeReferenceLevel ScopeReferenceLevel = null;
        private IIviScopeTrigger ScopeTrigger = null;
        private IIviScopeChannelCollection ScopeChannels = null;
        public IviScopeAdapter(string name, bool idQuery, bool reset, string options)
            : base(name, idQuery, reset, options)
        {
            ScopeAcquisition = new IviScopeAcquisition(this);
            ScopeChannels = new IviScopeChannelCollection(this);
            ScopeMeasurement = new IviScopeMeasurement(this);
            ScopeReferenceLevel = new IviScopeReferenceLevel(this);
            ScopeTrigger = new IviScopeTrigger(this);
        }

        public IIviScopeAcquisition Acquisition
        {
            get { return ScopeAcquisition; }
        }

        public IIviScopeChannelCollection Channels
        {
            get { return ScopeChannels; }
        }

        public IIviScopeMeasurement Measurement
        {
            get { return ScopeMeasurement; }
        }

        public IIviScopeReferenceLevel ReferenceLevel
        {
            get { return ScopeReferenceLevel; }
        }

        public IIviScopeTrigger Trigger
        {
            get { return ScopeTrigger; }
        }

        internal class IviScopeChannelCollection : IIviScopeChannelCollection
        {
            private IDriverAdapterBase Adapter;
            private IList<IIviScopeChannel> Channels = null;
            private IList<string> ChannelNames = null;
            public IviScopeChannelCollection(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                int ChannelCount = Adapter.GetAttributeViInt32(IviScopeAttribute.IVISCOPE_ATTR_CHANNEL_COUNT);

                Channels = new List<IIviScopeChannel>();
                ChannelNames = new List<string>();
                for (int Index = 1; Index <= ChannelCount; Index++)
                {
                    IIviScopeChannel Channel = new IviScopeChannel(Adapter, Index);
                    Channels.Add(Channel);
                    ChannelNames.Add(Channel.Name);
                }
            }

            public int Count
            {
                get { return Channels.Count; }
            }

            public IIviScopeChannel this[string name]
            {
                get { return Channels[ChannelNames.IndexOf(name)]; }
            }

            public IEnumerator<IIviScopeChannel> GetEnumerator()
            {
                return Channels.GetEnumerator();
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return Channels.GetEnumerator();
            }
        }

        internal class IviScopeChannel : IIviScopeChannel
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviScope IviScopeInterop = null;
            private int Index;
            private string ChannelName;
            private IIviScopeChannelMeasurement ScopeChannelMeasurement = null;

            public IviScopeChannel(IDriverAdapterBase Adapter, int Index)
            {
                this.Adapter = Adapter;
                IviScopeInterop = (IviCInterop.IviScope)Adapter.Interop;
                this.Index = Index;

                StringBuilder NameValue = new StringBuilder(256);
                Adapter.ViSessionStatusCheck(IviScopeInterop.GetChannelName(Adapter.Session, Index, NameValue.Capacity, NameValue));
                ChannelName = NameValue.ToString();
                ScopeChannelMeasurement = new IviScopeChannelMeasurement(Adapter, ChannelName);
            }


            public void Configure(double range, double offset, VerticalCoupling coupling, bool probeAttenuationAuto, bool enabled)
            {
                Adapter.ViSessionStatusCheck(IviScopeInterop.ConfigureChannel(Adapter.Session, ChannelName, range, offset, ScopeVerticalCoupling.getC_Value(coupling), ProbeAttenuation, enabled));
                ProbeAttenuationAuto = probeAttenuationAuto;
            }

            public void Configure(double range, double offset, VerticalCoupling coupling, double probeAttenuation, bool enabled)
            {
                Adapter.ViSessionStatusCheck(IviScopeInterop.ConfigureChannel(Adapter.Session, ChannelName, range, offset, ScopeVerticalCoupling.getC_Value(coupling), probeAttenuation, enabled));
            }

            public void ConfigureCharacteristics(double inputImpedance, double inputFrequencyMaximum)
            {
                Adapter.ViSessionStatusCheck(IviScopeInterop.ConfigureChanCharacteristics(Adapter.Session, ChannelName, inputImpedance, inputFrequencyMaximum));
            }

            public VerticalCoupling Coupling
            {
                get
                {
                    return ScopeVerticalCoupling.getEnum(Adapter.GetAttributeViInt32(ChannelName, IviScopeAttribute.IVISCOPE_ATTR_VERTICAL_COUPLING));
                }
                set
                {
                    Adapter.SetAttributeViInt32(ChannelName, IviScopeAttribute.IVISCOPE_ATTR_VERTICAL_COUPLING, ScopeVerticalCoupling.getC_Value(value));
                }
            }

            public bool Enabled
            {
                get
                {
                    return Adapter.GetAttributeViBoolean(ChannelName, IviScopeAttribute.IVISCOPE_ATTR_CHANNEL_ENABLED);
                }
                set
                {
                    Adapter.SetAttributeViBoolean(ChannelName, IviScopeAttribute.IVISCOPE_ATTR_CHANNEL_ENABLED, value);
                }
            }

            public double InputFrequencyMaximum
            {
                get
                {
                    return Adapter.GetAttributeViReal64(ChannelName, IviScopeAttribute.IVISCOPE_ATTR_MAX_INPUT_FREQUENCY);
                }
                set
                {
                    Adapter.SetAttributeViReal64(ChannelName, IviScopeAttribute.IVISCOPE_ATTR_MAX_INPUT_FREQUENCY, value);
                }
            }

            public double InputImpedance
            {
                get
                {
                    return Adapter.GetAttributeViReal64(ChannelName, IviScopeAttribute.IVISCOPE_ATTR_INPUT_IMPEDANCE);
                }
                set
                {
                    Adapter.SetAttributeViReal64(ChannelName, IviScopeAttribute.IVISCOPE_ATTR_INPUT_IMPEDANCE, value);
                }
            }

            public IIviScopeChannelMeasurement Measurement
            {
                get { return ScopeChannelMeasurement; }
            }

            public double Offset
            {
                get
                {
                    return Adapter.GetAttributeViReal64(ChannelName, IviScopeAttribute.IVISCOPE_ATTR_VERTICAL_OFFSET);
                }
                set
                {
                    Adapter.SetAttributeViReal64(ChannelName, IviScopeAttribute.IVISCOPE_ATTR_VERTICAL_OFFSET, value);
                }
            }

            public double ProbeAttenuation
            {
                get
                {
                    return Adapter.GetAttributeViReal64(ChannelName, IviScopeAttribute.IVISCOPE_ATTR_PROBE_ATTENUATION);
                }
                set
                {
                    Adapter.SetAttributeViReal64(ChannelName, IviScopeAttribute.IVISCOPE_ATTR_PROBE_ATTENUATION, value);
                }
            }

            public bool ProbeAttenuationAuto
            {
                get
                {
                    return Adapter.GetAttributeViReal64(ChannelName, IviScopeAttribute.IVISCOPE_ATTR_PROBE_ATTENUATION) == IviScopeAttribute.IVISCOPE_VAL_PROBE_SENSE_ON;
                }
                set
                {
                    // Seems not supported by all driver IVISCOPE_VAL_PROBE_SENSE_ON
                    Adapter.SetAttributeViReal64(ChannelName, IviScopeAttribute.IVISCOPE_ATTR_PROBE_ATTENUATION, value ? IviScopeAttribute.IVISCOPE_VAL_PROBE_SENSE_ON : ProbeAttenuation);
                }
            }

            public double Range
            {
                get
                {
                    return Adapter.GetAttributeViReal64(ChannelName, IviScopeAttribute.IVISCOPE_ATTR_VERTICAL_RANGE);
                }
                set
                {
                    Adapter.SetAttributeViReal64(ChannelName, IviScopeAttribute.IVISCOPE_ATTR_VERTICAL_RANGE, value);
                }
            }

            public string Name
            {
                get { return ChannelName; }
            }
        }

        internal class IviScopeChannelMeasurement : IIviScopeChannelMeasurement
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviScope IviScopeInterop = null;
            private string ChannelName = null;
            public IviScopeChannelMeasurement(IDriverAdapterBase Adapter, string ChannelName)
            {
                this.Adapter = Adapter;
                IviScopeInterop = (IviCInterop.IviScope)Adapter.Interop;
                this.ChannelName = ChannelName;
            }

            #region FetchWaveform

            public IWaveform<byte> FetchWaveform(IWaveform<byte> waveform)
            {
                return FetchWaveformInternal<byte>(waveform, new Converter<double, byte>(delegate(double data)
                {
                    return (byte)data;
                }));
            }

            public IWaveform<short> FetchWaveform(IWaveform<short> waveform)
            {
                return FetchWaveformInternal<short>(waveform, new Converter<double, short>(delegate(double data)
                {
                    return (short)data;
                }));
            }

            public IWaveform<int> FetchWaveform(IWaveform<int> waveform)
            {
                return FetchWaveformInternal<int>(waveform, new Converter<double, int>(delegate(double data)
                {
                    return (int)data;
                }));
            }

            public IWaveform<double> FetchWaveform(IWaveform<double> waveform)
            {
                return FetchWaveformInternal<double>(waveform, new Converter<double, double>(delegate(double data)
                {
                    return (double)data;
                }));
            }

            public IWaveform<T> FetchWaveformInternal<T>(IWaveform<T> waveform, Converter<double, T> converter)
            {
                int actualPoints = 0;
                double initialX = 0;
                double xIncrement = 0;

                IntPtr pMeasure = Marshal.AllocHGlobal((int)(waveform.Capacity * Marshal.SizeOf(typeof(T))));

                Adapter.ViSessionStatusCheck(IviScopeInterop.FetchWaveform(Adapter.Session, ChannelName, (int)waveform.Capacity, pMeasure, ref actualPoints, ref initialX, ref xIncrement));

                double[] readingBuffer = new double[actualPoints];
                Marshal.Copy(pMeasure, readingBuffer, 0, actualPoints);
                Marshal.FreeHGlobal(pMeasure);

                PrecisionTimeSpan StartTime = new PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * initialX)));
                PrecisionTimeSpan IntervalPerPoint = new PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * xIncrement)));
                waveform.Configure(StartTime, IntervalPerPoint, actualPoints);
                waveform.PutElements(0, Array.ConvertAll<double, T>(readingBuffer, converter));

                return waveform;
            }

            #endregion

            public PrecisionTimeSpan FetchWaveformMeasurement(TimeMeasurementFunction measurementFunction)
            {
                double measurement = double.NaN;
                Adapter.ViSessionStatusCheck(IviScopeInterop.FetchWaveformMeasurement(Adapter.Session, ChannelName, ScopeTimeMeasurementFunction.getC_Value(measurementFunction), ref measurement));
                return new PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * measurement)));
            }

            public double FetchWaveformMeasurement(MeasurementFunction measurementFunction)
            {
                double measurement = double.NaN;
                Adapter.ViSessionStatusCheck(IviScopeInterop.FetchWaveformMeasurement(Adapter.Session, ChannelName, ScopeMeasurementFunction.getC_Value(measurementFunction), ref measurement));
                return measurement;
            }

            #region FetchWaveformMinMax

            public MinMaxWaveform<byte> FetchWaveformMinMax(MinMaxWaveform<byte> waveform)
            {
                return FetchWaveformMinMaxInternal<byte>(waveform, new Converter<double, byte>(delegate(double data)
                {
                    return (byte)data;
                }));
            }

            public MinMaxWaveform<short> FetchWaveformMinMax(MinMaxWaveform<short> waveform)
            {
                return FetchWaveformMinMaxInternal<short>(waveform, new Converter<double, short>(delegate(double data)
                {
                    return (short)data;
                }));
            }

            public MinMaxWaveform<int> FetchWaveformMinMax(MinMaxWaveform<int> waveform)
            {
                return FetchWaveformMinMaxInternal<int>(waveform, new Converter<double, int>(delegate(double data)
                {
                    return (int)data;
                }));
            }

            public MinMaxWaveform<double> FetchWaveformMinMax(MinMaxWaveform<double> waveform)
            {
                return FetchWaveformMinMaxInternal<double>(waveform, new Converter<double, double>(delegate(double data)
                {
                    return (double)data;
                }));
            }

            public MinMaxWaveform<T> FetchWaveformMinMaxInternal<T>(MinMaxWaveform<T> waveform, Converter<double, T> converter)
            {
                int actualPoints = 0;
                double initialX = 0;
                double xIncrement = 0;

                IntPtr pMinWaveform = Marshal.AllocHGlobal((int)(waveform.MinWaveform.Capacity * Marshal.SizeOf(typeof(T))));
                IntPtr pMaxWaveform = Marshal.AllocHGlobal((int)(waveform.MinWaveform.Capacity * Marshal.SizeOf(typeof(T))));
                Adapter.ViSessionStatusCheck(IviScopeInterop.FetchMinMaxWaveform(Adapter.Session, ChannelName, (int)waveform.MinWaveform.Capacity, pMinWaveform, pMaxWaveform, ref actualPoints, ref initialX, ref xIncrement));

                double[] MinWaveform = new double[actualPoints];
                double[] MaxWaveform = new double[actualPoints];

                Marshal.Copy(pMinWaveform, MinWaveform, 0, actualPoints);
                Marshal.Copy(pMaxWaveform, MaxWaveform, 0, actualPoints);

                Marshal.FreeHGlobal(pMinWaveform);
                Marshal.FreeHGlobal(pMaxWaveform);

                PrecisionTimeSpan StartTime = new PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * initialX)));
                PrecisionTimeSpan IntervalPerPoint = new PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * xIncrement)));

                waveform.MinWaveform.Configure(StartTime, IntervalPerPoint, actualPoints);
                waveform.MaxWaveform.Configure(StartTime, IntervalPerPoint, actualPoints);

                waveform.MinWaveform.PutElements(0, Array.ConvertAll<double, T>(MinWaveform, converter));
                waveform.MaxWaveform.PutElements(0, Array.ConvertAll<double, T>(MaxWaveform, converter));

                return waveform;
            }

            #endregion

            #region ReadWaveform

            public IWaveform<byte> ReadWaveform(PrecisionTimeSpan maximumTime, IWaveform<byte> waveform)
            {
                return ReadWaveformInternal<byte>(waveform, maximumTime, new Converter<double, byte>(delegate(double data)
                {
                    return (byte)data;
                }));
            }

            public IWaveform<short> ReadWaveform(PrecisionTimeSpan maximumTime, IWaveform<short> waveform)
            {
                return ReadWaveformInternal<short>(waveform, maximumTime, new Converter<double, short>(delegate(double data)
                {
                    return (short)data;
                }));
            }

            public IWaveform<int> ReadWaveform(PrecisionTimeSpan maximumTime, IWaveform<int> waveform)
            {
                return ReadWaveformInternal<int>(waveform, maximumTime, new Converter<double, int>(delegate(double data)
                {
                    return (int)data;
                }));
            }

            public IWaveform<double> ReadWaveform(PrecisionTimeSpan maximumTime, IWaveform<double> waveform)
            {
                return ReadWaveformInternal<double>(waveform, maximumTime, new Converter<double, double>(delegate(double data)
                {
                    return (double)data;
                }));
            }

            public IWaveform<T> ReadWaveformInternal<T>(IWaveform<T> waveform, PrecisionTimeSpan maximumTime, Converter<double, T> converter)
            {
                int actualPoints = 0;
                double initialX = 0;
                double xIncrement = 0;

                IntPtr pMeasure = Marshal.AllocHGlobal((int)(waveform.Capacity * Marshal.SizeOf(typeof(T))));

                Adapter.ViSessionStatusCheck(IviScopeInterop.ReadWaveform(Adapter.Session, ChannelName, (int)waveform.Capacity, (int)maximumTime.TotalMilliseconds, pMeasure, ref actualPoints, ref initialX, ref xIncrement));

                double[] readingBuffer = new double[actualPoints];
                Marshal.Copy(pMeasure, readingBuffer, 0, actualPoints);
                Marshal.FreeHGlobal(pMeasure);

                PrecisionTimeSpan StartTime = new PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * initialX)));
                PrecisionTimeSpan IntervalPerPoint = new PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * xIncrement)));

                waveform.Configure(StartTime, IntervalPerPoint, actualPoints);
                waveform.PutElements(0, Array.ConvertAll<double, T>(readingBuffer, converter));

                return waveform;
            }

            #endregion

            public PrecisionTimeSpan ReadWaveformMeasurement(TimeMeasurementFunction measurementFunction, PrecisionTimeSpan maximumTime)
            {
                double measurement = double.NaN;
                Adapter.ViSessionStatusCheck(IviScopeInterop.ReadWaveformMeasurement(Adapter.Session, ChannelName, ScopeTimeMeasurementFunction.getC_Value(measurementFunction), (int)maximumTime.TotalMilliseconds, ref measurement));
                return new PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * measurement)));
            }

            public double ReadWaveformMeasurement(MeasurementFunction measurementFunction, PrecisionTimeSpan maximumTime)
            {
                double measurement = double.NaN;
                Adapter.ViSessionStatusCheck(IviScopeInterop.ReadWaveformMeasurement(Adapter.Session, ChannelName, ScopeMeasurementFunction.getC_Value(measurementFunction), (int)maximumTime.TotalMilliseconds, ref measurement));
                return measurement;
            }

            #region ReadWaveformMinMax

            public MinMaxWaveform<byte> ReadWaveformMinMax(PrecisionTimeSpan maximumTime, MinMaxWaveform<byte> waveform)
            {
                return ReadWaveformMinMaxInternal<byte>(waveform, maximumTime, new Converter<double, byte>(delegate(double data)
                {
                    return (byte)data;
                }));
            }

            public MinMaxWaveform<short> ReadWaveformMinMax(PrecisionTimeSpan maximumTime, MinMaxWaveform<short> waveform)
            {
                return ReadWaveformMinMaxInternal<short>(waveform, maximumTime, new Converter<double, short>(delegate(double data)
                {
                    return (short)data;
                }));
            }

            public MinMaxWaveform<int> ReadWaveformMinMax(PrecisionTimeSpan maximumTime, MinMaxWaveform<int> waveform)
            {
                return ReadWaveformMinMaxInternal<int>(waveform, maximumTime, new Converter<double, int>(delegate(double data)
                {
                    return (int)data;
                }));
            }

            public MinMaxWaveform<double> ReadWaveformMinMax(PrecisionTimeSpan maximumTime, MinMaxWaveform<double> waveform)
            {
                return ReadWaveformMinMaxInternal<double>(waveform, maximumTime, new Converter<double, double>(delegate(double data)
                {
                    return (double)data;
                }));
            }

            public MinMaxWaveform<T> ReadWaveformMinMaxInternal<T>(MinMaxWaveform<T> waveform, PrecisionTimeSpan maximumTime, Converter<double, T> converter)
            {
                int actualPoints = 0;
                double initialX = 0;
                double xIncrement = 0;

                IntPtr pMinWaveform = Marshal.AllocHGlobal((int)(waveform.MinWaveform.Capacity * Marshal.SizeOf(typeof(T))));
                IntPtr pMaxWaveform = Marshal.AllocHGlobal((int)(waveform.MinWaveform.Capacity * Marshal.SizeOf(typeof(T))));
                Adapter.ViSessionStatusCheck(IviScopeInterop.ReadMinMaxWaveform(Adapter.Session, ChannelName, (int)waveform.MinWaveform.Capacity, (int)maximumTime.TotalMilliseconds, pMinWaveform, pMaxWaveform, ref actualPoints, ref initialX, ref xIncrement));

                double[] MinWaveform = new double[actualPoints];
                double[] MaxWaveform = new double[actualPoints];

                Marshal.Copy(pMinWaveform, MinWaveform, 0, actualPoints);
                Marshal.Copy(pMaxWaveform, MaxWaveform, 0, actualPoints);

                Marshal.FreeHGlobal(pMinWaveform);
                Marshal.FreeHGlobal(pMaxWaveform);

                PrecisionTimeSpan StartTime = new PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * initialX)));
                PrecisionTimeSpan IntervalPerPoint = new PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * xIncrement)));

                waveform.MinWaveform.Configure(StartTime, IntervalPerPoint, actualPoints);
                waveform.MaxWaveform.Configure(StartTime, IntervalPerPoint, actualPoints);

                waveform.MinWaveform.PutElements(0, Array.ConvertAll<double, T>(MinWaveform, converter));
                waveform.MaxWaveform.PutElements(0, Array.ConvertAll<double, T>(MaxWaveform, converter));

                return waveform;
            }

            #endregion
        }

        internal class IviScopeAcquisition : IIviScopeAcquisition
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviScope IviScopeInterop = null;
            public IviScopeAcquisition(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviScopeInterop = (IviCInterop.IviScope)Adapter.Interop;
            }

            public void ConfigureRecord(Ivi.Driver.PrecisionTimeSpan timePerRecord, int minimumNumberPoints, Ivi.Driver.PrecisionTimeSpan acquisitionStartTime)
            {
                Adapter.ViSessionStatusCheck(IviScopeInterop.ConfigureAcquisitionRecord(Adapter.Session, timePerRecord.TotalSeconds, minimumNumberPoints, acquisitionStartTime.TotalSeconds));
            }

            public Interpolation Interpolation
            {
                get
                {
                    return ScopeInterpolation.getEnum(Adapter.GetAttributeViInt32(IviScopeAttribute.IVISCOPE_ATTR_INTERPOLATION));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviScopeAttribute.IVISCOPE_ATTR_INTERPOLATION, ScopeInterpolation.getC_Value(value));
                }
            }

            public int NumberOfAverages
            {
                get
                {
                    return Adapter.GetAttributeViInt32(IviScopeAttribute.IVISCOPE_ATTR_NUM_AVERAGES);
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviScopeAttribute.IVISCOPE_ATTR_NUM_AVERAGES, value);
                }
            }

            public int NumberOfEnvelopes
            {
                get
                {
                    return Adapter.GetAttributeViInt32(IviScopeAttribute.IVISCOPE_ATTR_NUM_ENVELOPES);
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviScopeAttribute.IVISCOPE_ATTR_NUM_ENVELOPES, value);
                }
            }

            public int NumberOfPointsMinimum
            {
                get
                {
                    return Adapter.GetAttributeViInt32(IviScopeAttribute.IVISCOPE_ATTR_HORZ_MIN_NUM_PTS);
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviScopeAttribute.IVISCOPE_ATTR_HORZ_MIN_NUM_PTS, value);
                }
            }

            public int RecordLength
            {
                get { return Adapter.GetAttributeViInt32(IviScopeAttribute.IVISCOPE_ATTR_HORZ_RECORD_LENGTH); }
            }

            public SampleMode SampleMode
            {
                get { return ScopeSampleMode.getEnum(Adapter.GetAttributeViInt32(IviScopeAttribute.IVISCOPE_ATTR_SAMPLE_MODE)); }
            }

            public double SampleRate
            {
                get { return Adapter.GetAttributeViReal64(IviScopeAttribute.IVISCOPE_ATTR_HORZ_SAMPLE_RATE); }
            }

            public Ivi.Driver.PrecisionTimeSpan StartTime
            {
                get
                {
                    return new PrecisionTimeSpan((decimal)Adapter.GetAttributeViReal64(IviScopeAttribute.IVISCOPE_ATTR_ACQUISITION_START_TIME));
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviScopeAttribute.IVISCOPE_ATTR_ACQUISITION_START_TIME, value.TotalSeconds);
                }
            }

            public Ivi.Driver.PrecisionTimeSpan TimePerRecord
            {
                get
                {
                    return new PrecisionTimeSpan((decimal)Adapter.GetAttributeViReal64(IviScopeAttribute.IVISCOPE_ATTR_HORZ_TIME_PER_RECORD));
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviScopeAttribute.IVISCOPE_ATTR_HORZ_TIME_PER_RECORD, value.TotalSeconds);
                }
            }

            public AcquisitionType Type
            {
                get
                {
                    return ScopeAcquisitionType.getEnum(Adapter.GetAttributeViInt32(IviScopeAttribute.IVISCOPE_ATTR_ACQUISITION_TYPE));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviScopeAttribute.IVISCOPE_ATTR_ACQUISITION_TYPE, ScopeAcquisitionType.getC_Value(value));
                }
            }
        }

        internal class IviScopeMeasurement : IIviScopeMeasurement
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviScope IviScopeInterop = null;
            public IviScopeMeasurement(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviScopeInterop = (IviCInterop.IviScope)Adapter.Interop;
            }

            public void Abort()
            {
                Adapter.ViSessionStatusCheck(IviScopeInterop.Abort(Adapter.Session));
            }

            public void AutoSetup()
            {
                Adapter.ViSessionStatusCheck(IviScopeInterop.AutoSetup(Adapter.Session));
            }

            public IWaveform<byte> CreateWaveformByte(int numberSamples)
            {
                double SampleRate = Adapter.GetAttributeViReal64(IviScopeAttribute.IVISCOPE_ATTR_HORZ_SAMPLE_RATE);
                return new Waveform<byte>(new PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond / SampleRate))), numberSamples);
            }

            public IWaveform<double> CreateWaveformDouble(int numberSamples)
            {
                double SampleRate = Adapter.GetAttributeViReal64(IviScopeAttribute.IVISCOPE_ATTR_HORZ_SAMPLE_RATE);
                return new Waveform<double>(new PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond / SampleRate))), numberSamples);
            }

            public IWaveform<short> CreateWaveformInt16(int numberSamples)
            {
                double SampleRate = Adapter.GetAttributeViReal64(IviScopeAttribute.IVISCOPE_ATTR_HORZ_SAMPLE_RATE);
                return new Waveform<short>(new PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond / SampleRate))), numberSamples);
            }

            public IWaveform<int> CreateWaveformInt32(int numberSamples)
            {
                double SampleRate = Adapter.GetAttributeViReal64(IviScopeAttribute.IVISCOPE_ATTR_HORZ_SAMPLE_RATE);
                return new Waveform<int>(new PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond / SampleRate))), numberSamples);
            }

            public void Initiate()
            {
                Adapter.ViSessionStatusCheck(IviScopeInterop.InitiateAcquisition(Adapter.Session));
            }

            public AcquisitionStatus Status()
            {
                int acquisitionStatus = 0;
                Adapter.ViSessionStatusCheck(IviScopeInterop.AcquisitionStatus(Adapter.Session, ref acquisitionStatus));
                return ScopeAcquisitionStatus.getEnum(acquisitionStatus);
            }
        }

        internal class IviScopeReferenceLevel : IIviScopeReferenceLevel
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviScope IviScopeInterop = null;
            public IviScopeReferenceLevel(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviScopeInterop = (IviCInterop.IviScope)Adapter.Interop;
            }

            public void Configure(double low, double mid, double high)
            {
                Adapter.ViSessionStatusCheck(IviScopeInterop.ConfigureRefLevels(Adapter.Session, low, mid, high));
            }

            public double High
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviScopeAttribute.IVISCOPE_ATTR_MEAS_HIGH_REF);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviScopeAttribute.IVISCOPE_ATTR_MEAS_HIGH_REF, value);
                }
            }

            public double Low
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviScopeAttribute.IVISCOPE_ATTR_MEAS_LOW_REF);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviScopeAttribute.IVISCOPE_ATTR_MEAS_LOW_REF, value);
                }
            }

            public double Mid
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviScopeAttribute.IVISCOPE_ATTR_MEAS_MID_REF);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviScopeAttribute.IVISCOPE_ATTR_MEAS_MID_REF, value);
                }
            }
        }

        internal class IviScopeTrigger : IIviScopeTrigger
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviScope IviScopeInterop = null;
            private IIviScopeTriggerACLine ScopeTriggerACLine = null;
            private IIviScopeTriggerEdge ScopeTriggerEdge = null;
            private IIviScopeTriggerGlitch ScopeTriggerGlitch = null;
            private IIviScopeTriggerRunt ScopeTriggerRunt = null;
            private IIviScopeTriggerTV ScopeTriggerTV = null;
            private IIviScopeTriggerWidth ScopeTriggerWidth = null;
            public IviScopeTrigger(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviScopeInterop = (IviCInterop.IviScope)Adapter.Interop;
                ScopeTriggerACLine = new IviScopeTriggerACLine(Adapter);
                ScopeTriggerEdge = new IviScopeTriggerEdge(Adapter);
                ScopeTriggerGlitch = new IviScopeTriggerGlitch(Adapter);
                ScopeTriggerRunt = new IviScopeTriggerRunt(Adapter);
                ScopeTriggerTV = new IviScopeTriggerTV(Adapter);
                ScopeTriggerWidth = new IviScopeTriggerWidth(Adapter);
            }

            public IIviScopeTriggerACLine ACLine
            {
                get { return ScopeTriggerACLine; }
            }

            public void Configure(TriggerType type, PrecisionTimeSpan holdoff)
            {
                Adapter.ViSessionStatusCheck(IviScopeInterop.ConfigureTrigger(Adapter.Session, ScopeTriggerType.getC_Value(type), holdoff.TotalSeconds));
            }

            public bool Continuous
            {
                get
                {
                    return Adapter.GetAttributeViBoolean(IviScopeAttribute.IVISCOPE_ATTR_INITIATE_CONTINUOUS);
                }
                set
                {
                    Adapter.SetAttributeViBoolean(IviScopeAttribute.IVISCOPE_ATTR_INITIATE_CONTINUOUS, value);
                }
            }

            public TriggerCoupling Coupling
            {
                get
                {
                    return ScopeTriggerCoupling.getEnum(Adapter.GetAttributeViInt32(IviScopeAttribute.IVISCOPE_ATTR_TRIGGER_COUPLING));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviScopeAttribute.IVISCOPE_ATTR_TRIGGER_COUPLING, ScopeTriggerCoupling.getC_Value(value));
                }
            }

            public IIviScopeTriggerEdge Edge
            {
                get { return ScopeTriggerEdge; }
            }

            public IIviScopeTriggerGlitch Glitch
            {
                get { return ScopeTriggerGlitch; }
            }

            public PrecisionTimeSpan Holdoff
            {
                get
                {
                    return new PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * Adapter.GetAttributeViReal64(IviScopeAttribute.IVISCOPE_ATTR_TRIGGER_HOLDOFF))));
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviScopeAttribute.IVISCOPE_ATTR_TRIGGER_HOLDOFF, value.TotalSeconds);
                }
            }

            public double Level
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviScopeAttribute.IVISCOPE_ATTR_TRIGGER_LEVEL);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviScopeAttribute.IVISCOPE_ATTR_TRIGGER_LEVEL, value);
                }
            }

            public TriggerModifier Modifier
            {
                get
                {
                    return ScopeTriggerModifier.getEnum(Adapter.GetAttributeViInt32(IviScopeAttribute.IVISCOPE_ATTR_TRIGGER_MODIFIER));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviScopeAttribute.IVISCOPE_ATTR_TRIGGER_MODIFIER, ScopeTriggerModifier.getC_Value(value));
                }
            }

            public IIviScopeTriggerRunt Runt
            {
                get { return ScopeTriggerRunt; }
            }

            public string Source
            {
                get
                {
                    return Adapter.GetAttributeViString(IviScopeAttribute.IVISCOPE_ATTR_TRIGGER_SOURCE);
                }
                set
                {
                    Adapter.SetAttributeViString(IviScopeAttribute.IVISCOPE_ATTR_TRIGGER_SOURCE, value);
                }
            }

            public IIviScopeTriggerTV TV
            {
                get { return ScopeTriggerTV; }
            }

            public TriggerType Type
            {
                get
                {
                    return ScopeTriggerType.getEnum(Adapter.GetAttributeViInt32(IviScopeAttribute.IVISCOPE_ATTR_TRIGGER_TYPE));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviScopeAttribute.IVISCOPE_ATTR_TRIGGER_TYPE, ScopeTriggerType.getC_Value(value));
                }
            }

            public IIviScopeTriggerWidth Width
            {
                get { return ScopeTriggerWidth; }
            }
        }

        internal class IviScopeTriggerACLine : IIviScopeTriggerACLine
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviScope IviScopeInterop = null;
            public IviScopeTriggerACLine(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviScopeInterop = (IviCInterop.IviScope)Adapter.Interop;
            }

            public ACLineSlope Slope
            {
                get
                {
                    return ScopeACLineSlope.getEnum(Adapter.GetAttributeViInt32(IviScopeAttribute.IVISCOPE_ATTR_AC_LINE_TRIGGER_SLOPE));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviScopeAttribute.IVISCOPE_ATTR_AC_LINE_TRIGGER_SLOPE, ScopeACLineSlope.getC_Value(value));
                }
            }
        }

        internal class IviScopeTriggerEdge : IIviScopeTriggerEdge
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviScope IviScopeInterop = null;
            public IviScopeTriggerEdge(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviScopeInterop = (IviCInterop.IviScope)Adapter.Interop;
            }

            public void Configure(string source, double level, Slope slope)
            {
                Adapter.ViSessionStatusCheck(IviScopeInterop.ConfigureEdgeTriggerSource(Adapter.Session, source, level, ScopeSlope.getC_Value(slope)));
            }

            public Slope Slope
            {
                get
                {
                    return ScopeSlope.getEnum(Adapter.GetAttributeViInt32(IviScopeAttribute.IVISCOPE_ATTR_TRIGGER_SLOPE));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviScopeAttribute.IVISCOPE_ATTR_TRIGGER_SLOPE, ScopeSlope.getC_Value(value));
                }
            }
        }

        internal class IviScopeTriggerGlitch : IIviScopeTriggerGlitch
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviScope IviScopeInterop = null;
            public IviScopeTriggerGlitch(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviScopeInterop = (IviCInterop.IviScope)Adapter.Interop;
            }

            public GlitchCondition Condition
            {
                get
                {
                    return ScopeGlitchCondition.getEnum(Adapter.GetAttributeViInt32(IviScopeAttribute.IVISCOPE_ATTR_GLITCH_CONDITION));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviScopeAttribute.IVISCOPE_ATTR_GLITCH_CONDITION, ScopeGlitchCondition.getC_Value(value));
                }
            }

            public void Configure(string source, double level, PrecisionTimeSpan width, Polarity polarity, GlitchCondition condition)
            {
                Adapter.ViSessionStatusCheck(IviScopeInterop.ConfigureGlitchTriggerSource(Adapter.Session, source, level, width.TotalSeconds, ScopeGlitchTriggerPolarity.getC_Value(polarity), ScopeGlitchCondition.getC_Value(condition)));
            }

            public Polarity Polarity
            {
                get
                {
                    return ScopeGlitchTriggerPolarity.getEnum(Adapter.GetAttributeViInt32(IviScopeAttribute.IVISCOPE_ATTR_GLITCH_POLARITY));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviScopeAttribute.IVISCOPE_ATTR_GLITCH_POLARITY, ScopeGlitchTriggerPolarity.getC_Value(value));
                }
            }

            public PrecisionTimeSpan Width
            {
                get
                {
                    return new PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * Adapter.GetAttributeViReal64(IviScopeAttribute.IVISCOPE_ATTR_GLITCH_WIDTH))));
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviScopeAttribute.IVISCOPE_ATTR_GLITCH_POLARITY, value.TotalSeconds);
                }
            }
        }

        internal class IviScopeTriggerRunt : IIviScopeTriggerRunt
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviScope IviScopeInterop = null;
            public IviScopeTriggerRunt(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviScopeInterop = (IviCInterop.IviScope)Adapter.Interop;
            }

            public void Configure(string source, double thresholdLow, double thresholdHigh, Polarity polarity)
            {
                Adapter.ViSessionStatusCheck(IviScopeInterop.ConfigureRuntTriggerSource(Adapter.Session, source, thresholdLow, thresholdHigh, ScopeRuntTriggerPolarity.getC_Value(polarity)));
            }

            public Polarity Polarity
            {
                get
                {
                    return ScopeRuntTriggerPolarity.getEnum(Adapter.GetAttributeViInt32(IviScopeAttribute.IVISCOPE_ATTR_RUNT_POLARITY));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviScopeAttribute.IVISCOPE_ATTR_RUNT_POLARITY, ScopeRuntTriggerPolarity.getC_Value(value));
                }
            }

            public double ThresholdHigh
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviScopeAttribute.IVISCOPE_ATTR_RUNT_HIGH_THRESHOLD);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviScopeAttribute.IVISCOPE_ATTR_RUNT_HIGH_THRESHOLD, value);
                }
            }

            public double ThresholdLow
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviScopeAttribute.IVISCOPE_ATTR_RUNT_LOW_THRESHOLD);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviScopeAttribute.IVISCOPE_ATTR_RUNT_LOW_THRESHOLD, value);
                }
            }
        }

        internal class IviScopeTriggerTV : IIviScopeTriggerTV
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviScope IviScopeInterop = null;
            public IviScopeTriggerTV(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviScopeInterop = (IviCInterop.IviScope)Adapter.Interop;
            }

            public void Configure(string source, TVSignalFormat signalFormat, TVTriggerEvent triggerEvent, TVTriggerPolarity polarity)
            {
                Adapter.ViSessionStatusCheck(IviScopeInterop.ConfigureTVTriggerSource(Adapter.Session, source, ScopeTVSignalFormat.getC_Value(signalFormat), ScopeTVTriggerEvent.getC_Value(triggerEvent), ScopeTVTriggerPolarity.getC_Value(polarity)));
            }

            public int LineNumber
            {
                get
                {
                    return Adapter.GetAttributeViInt32(IviScopeAttribute.IVISCOPE_ATTR_TV_TRIGGER_LINE_NUMBER);
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviScopeAttribute.IVISCOPE_ATTR_TV_TRIGGER_LINE_NUMBER, value);
                }
            }

            public TVTriggerPolarity Polarity
            {
                get
                {
                    return ScopeTVTriggerPolarity.getEnum(Adapter.GetAttributeViInt32(IviScopeAttribute.IVISCOPE_ATTR_TV_TRIGGER_POLARITY));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviScopeAttribute.IVISCOPE_ATTR_TV_TRIGGER_POLARITY, ScopeTVTriggerPolarity.getC_Value(value));
                }
            }

            public TVSignalFormat SignalFormat
            {
                get
                {
                    return ScopeTVSignalFormat.getEnum(Adapter.GetAttributeViInt32(IviScopeAttribute.IVISCOPE_ATTR_TV_TRIGGER_SIGNAL_FORMAT));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviScopeAttribute.IVISCOPE_ATTR_TV_TRIGGER_SIGNAL_FORMAT, ScopeTVSignalFormat.getC_Value(value));
                }
            }

            public TVTriggerEvent TriggerEvent
            {
                get
                {
                    return ScopeTVTriggerEvent.getEnum(Adapter.GetAttributeViInt32(IviScopeAttribute.IVISCOPE_ATTR_TV_TRIGGER_EVENT));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviScopeAttribute.IVISCOPE_ATTR_TV_TRIGGER_EVENT, ScopeTVTriggerEvent.getC_Value(value));
                }
            }
        }

        internal class IviScopeTriggerWidth : IIviScopeTriggerWidth
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviScope IviScopeInterop = null;
            public IviScopeTriggerWidth(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviScopeInterop = (IviCInterop.IviScope)Adapter.Interop;
            }

            public WidthCondition Condition
            {
                get
                {
                    return ScopeWidthCondition.getEnum(Adapter.GetAttributeViInt32(IviScopeAttribute.IVISCOPE_ATTR_WIDTH_CONDITION));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviScopeAttribute.IVISCOPE_ATTR_WIDTH_CONDITION, ScopeWidthCondition.getC_Value(value));
                }
            }

            public void Configure(string source, double level, PrecisionTimeSpan thresholdLow, PrecisionTimeSpan thresholdHigh, Polarity polarity, WidthCondition condition)
            {
                Adapter.ViSessionStatusCheck(IviScopeInterop.ConfigureWidthTriggerSource(Adapter.Session, source, level, thresholdLow.TotalSeconds, thresholdHigh.TotalSeconds, (int)polarity, ScopeWidthCondition.getC_Value(condition)));
            }

            public Polarity Polarity
            {
                get
                {
                    return ScopeWidthTriggerPolarity.getEnum(Adapter.GetAttributeViInt32(IviScopeAttribute.IVISCOPE_ATTR_WIDTH_POLARITY));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviScopeAttribute.IVISCOPE_ATTR_WIDTH_POLARITY, ScopeWidthTriggerPolarity.getC_Value(value));
                }
            }

            public PrecisionTimeSpan ThresholdHigh
            {
                get
                {
                    return new PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * Adapter.GetAttributeViReal64(IviScopeAttribute.IVISCOPE_ATTR_WIDTH_HIGH_THRESHOLD))));
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviScopeAttribute.IVISCOPE_ATTR_WIDTH_HIGH_THRESHOLD, value.TotalSeconds);
                }
            }

            public PrecisionTimeSpan ThresholdLow
            {
                get
                {
                    return new PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * Adapter.GetAttributeViReal64(IviScopeAttribute.IVISCOPE_ATTR_WIDTH_LOW_THRESHOLD))));
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviScopeAttribute.IVISCOPE_ATTR_WIDTH_LOW_THRESHOLD, (int)value.TotalSeconds);
                }
            }
        }
    }
}

