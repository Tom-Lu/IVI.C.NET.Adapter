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
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Ivi.Driver;
using Ivi.Digitizer;
using IVI.C.NET.Adapter.IviCInterop;

namespace IVI.C.NET.Adapter
{
    public class IviDigitizerAdapter : DriverAdapterBase<IviCInterop.IviDigitizer>, IIviDigitizer
    {
        #region Enum Mapping

        private static IviEnumCMapping<SampleMode, int> DigitizerSampleMode = IviEnumCMapping<SampleMode, int>.Instance
            .Map(SampleMode.RealTime, IviDigitizerAttribute.IVIDIGITIZER_VAL_SAMPLE_MODE_REAL_TIME)
            .Map(SampleMode.EquivalentTime, IviDigitizerAttribute.IVIDIGITIZER_VAL_SAMPLE_MODE_EQUIVALENT_TIME);

        private static IviEnumCMapping<AcquisitionStatusResult, int> DigitizerAcquisitionStatusResult = IviEnumCMapping<AcquisitionStatusResult, int>.Instance
            .Map(AcquisitionStatusResult.True, IviDigitizerAttribute.IVIDIGITIZER_VAL_ACQUISITION_STATUS_RESULT_TRUE)
            .Map(AcquisitionStatusResult.False, IviDigitizerAttribute.IVIDIGITIZER_VAL_ACQUISITION_STATUS_RESULT_FALSE)
            .Map(AcquisitionStatusResult.Unknown, IviDigitizerAttribute.IVIDIGITIZER_VAL_ACQUISITION_STATUS_RESULT_UNKNOWN);

        private static IviEnumCMapping<ArmSourceOperator, int> DigitizerArmSourceOperator = IviEnumCMapping<ArmSourceOperator, int>.Instance
            .Map(ArmSourceOperator.And, IviDigitizerAttribute.IVIDIGITIZER_VAL_ARM_SOURCE_OPERATOR_AND)
            .Map(ArmSourceOperator.Or, IviDigitizerAttribute.IVIDIGITIZER_VAL_ARM_SOURCE_OPERATOR_OR)
            .Map(ArmSourceOperator.None, IviDigitizerAttribute.IVIDIGITIZER_VAL_ARM_SOURCE_OPERATOR_NONE);

        private static IviEnumCMapping<TriggerCoupling, int> DigitizerTriggerCoupling = IviEnumCMapping<TriggerCoupling, int>.Instance
            .Map(TriggerCoupling.AC, IviDigitizerAttribute.IVIDIGITIZER_VAL_TRIGGER_COUPLING_AC)
            .Map(TriggerCoupling.DC, IviDigitizerAttribute.IVIDIGITIZER_VAL_TRIGGER_COUPLING_DC)
            .Map(TriggerCoupling.HFReject, IviDigitizerAttribute.IVIDIGITIZER_VAL_TRIGGER_COUPLING_HF_REJECT)
            .Map(TriggerCoupling.LFReject, IviDigitizerAttribute.IVIDIGITIZER_VAL_TRIGGER_COUPLING_LF_REJECT)
            .Map(TriggerCoupling.NoiseReject, IviDigitizerAttribute.IVIDIGITIZER_VAL_TRIGGER_COUPLING_NOISE_REJECT);

        private static IviEnumCMapping<ArmType, int> DigitizerArmType = IviEnumCMapping<ArmType, int>.Instance
            .Map(ArmType.Edge, IviDigitizerAttribute.IVIDIGITIZER_VAL_EDGE_ARM)
            .Map(ArmType.Width, IviDigitizerAttribute.IVIDIGITIZER_VAL_WIDTH_ARM)
            .Map(ArmType.Runt, IviDigitizerAttribute.IVIDIGITIZER_VAL_RUNT_ARM)
            .Map(ArmType.Glitch, IviDigitizerAttribute.IVIDIGITIZER_VAL_GLITCH_ARM)
            .Map(ArmType.TV, IviDigitizerAttribute.IVIDIGITIZER_VAL_TV_ARM)
            .Map(ArmType.Window, IviDigitizerAttribute.IVIDIGITIZER_VAL_WINDOW_ARM);

        private static IviEnumCMapping<Slope, int> DigitizerSlope = IviEnumCMapping<Slope, int>.Instance
            .Map(Slope.Negative, IviDigitizerAttribute.IVIDIGITIZER_VAL_TRIGGER_SLOPE_NEGATIVE)
            .Map(Slope.Positive, IviDigitizerAttribute.IVIDIGITIZER_VAL_TRIGGER_SLOPE_POSITIVE);

        private static IviEnumCMapping<GlitchCondition, int> DigitizerGlitchCondition = IviEnumCMapping<GlitchCondition, int>.Instance
            .Map(GlitchCondition.LessThan, IviDigitizerAttribute.IVIDIGITIZER_VAL_GLITCH_LESS_THAN)
            .Map(GlitchCondition.GreaterThan, IviDigitizerAttribute.IVIDIGITIZER_VAL_GLITCH_GREATER_THAN);

        private static IviEnumCMapping<GlitchPolarity, int> DigitizerGlitchPolarity = IviEnumCMapping<GlitchPolarity, int>.Instance
            .Map(GlitchPolarity.Positive, IviDigitizerAttribute.IVIDIGITIZER_VAL_GLITCH_POSITIVE)
            .Map(GlitchPolarity.Negative, IviDigitizerAttribute.IVIDIGITIZER_VAL_GLITCH_NEGATIVE)
            .Map(GlitchPolarity.Either, IviDigitizerAttribute.IVIDIGITIZER_VAL_GLITCH_EITHER);

        private static IviEnumCMapping<RuntPolarity, int> DigitizerRuntPolarity = IviEnumCMapping<RuntPolarity, int>.Instance
            .Map(RuntPolarity.Positive, IviDigitizerAttribute.IVIDIGITIZER_VAL_RUNT_POSITIVE)
            .Map(RuntPolarity.Negative, IviDigitizerAttribute.IVIDIGITIZER_VAL_RUNT_NEGATIVE)
            .Map(RuntPolarity.Either, IviDigitizerAttribute.IVIDIGITIZER_VAL_RUNT_EITHER);

        private static IviEnumCMapping<TVTriggerPolarity, int> DigitizerTVTriggerPolarity = IviEnumCMapping<TVTriggerPolarity, int>.Instance
            .Map(TVTriggerPolarity.Positive, IviDigitizerAttribute.IVIDIGITIZER_VAL_TV_POSITIVE)
            .Map(TVTriggerPolarity.Negative, IviDigitizerAttribute.IVIDIGITIZER_VAL_TV_NEGATIVE);

        private static IviEnumCMapping<TVSignalFormat, int> DigitizerTVSignalFormat = IviEnumCMapping<TVSignalFormat, int>.Instance
            .Map(TVSignalFormat.Ntsc, IviDigitizerAttribute.IVIDIGITIZER_VAL_NTSC)
            .Map(TVSignalFormat.Pal, IviDigitizerAttribute.IVIDIGITIZER_VAL_PAL)
            .Map(TVSignalFormat.Secam, IviDigitizerAttribute.IVIDIGITIZER_VAL_SECAM);

        private static IviEnumCMapping<TVTriggerEvent, int> DigitizerTVTriggerEvent = IviEnumCMapping<TVTriggerEvent, int>.Instance
            .Map(TVTriggerEvent.Field1, IviDigitizerAttribute.IVIDIGITIZER_VAL_TV_EVENT_FIELD1)
            .Map(TVTriggerEvent.Field2, IviDigitizerAttribute.IVIDIGITIZER_VAL_TV_EVENT_FIELD2)
            .Map(TVTriggerEvent.AnyField, IviDigitizerAttribute.IVIDIGITIZER_VAL_TV_EVENT_ANY_FIELD)
            .Map(TVTriggerEvent.AnyLine, IviDigitizerAttribute.IVIDIGITIZER_VAL_TV_EVENT_ANY_LINE)
            .Map(TVTriggerEvent.LineNumber, IviDigitizerAttribute.IVIDIGITIZER_VAL_TV_EVENT_LINE_NUMBER);

        private static IviEnumCMapping<WidthCondition, int> DigitizerWidthCondition = IviEnumCMapping<WidthCondition, int>.Instance
            .Map(WidthCondition.Within, IviDigitizerAttribute.IVIDIGITIZER_VAL_WIDTH_WITHIN)
            .Map(WidthCondition.Outside, IviDigitizerAttribute.IVIDIGITIZER_VAL_WIDTH_OUTSIDE);

        private static IviEnumCMapping<WidthPolarity, int> DigitizerWidthPolarity = IviEnumCMapping<WidthPolarity, int>.Instance
            .Map(WidthPolarity.Positive, IviDigitizerAttribute.IVIDIGITIZER_VAL_WIDTH_POSITIVE)
            .Map(WidthPolarity.Negative, IviDigitizerAttribute.IVIDIGITIZER_VAL_WIDTH_NEGATIVE)
            .Map(WidthPolarity.Either, IviDigitizerAttribute.IVIDIGITIZER_VAL_WIDTH_EITHER);

        private static IviEnumCMapping<WindowCondition, int> DigitizerWindowCondition = IviEnumCMapping<WindowCondition, int>.Instance
            .Map(WindowCondition.Entering, IviDigitizerAttribute.IVIDIGITIZER_VAL_WINDOW_CONDITION_ENTERING)
            .Map(WindowCondition.Leaving, IviDigitizerAttribute.IVIDIGITIZER_VAL_WINDOW_CONDITION_LEAVING);

        private static IviEnumCMapping<VerticalCoupling, int> DigitizerVerticalCoupling = IviEnumCMapping<VerticalCoupling, int>.Instance
            .Map(VerticalCoupling.AC, IviDigitizerAttribute.IVIDIGITIZER_VAL_VERTICAL_COUPLING_AC)
            .Map(VerticalCoupling.DC, IviDigitizerAttribute.IVIDIGITIZER_VAL_VERTICAL_COUPLING_DC)
            .Map(VerticalCoupling.Ground, IviDigitizerAttribute.IVIDIGITIZER_VAL_VERTICAL_COUPLING_GND);

        private static IviEnumCMapping<ReferenceOscillatorSource, int> DigitizerReferenceOscillatorSource = IviEnumCMapping<ReferenceOscillatorSource, int>.Instance
            .Map(ReferenceOscillatorSource.Internal, IviDigitizerAttribute.IVIDIGITIZER_VAL_REFERENCE_OSCILLATOR_SOURCE_INTERNAL)
            .Map(ReferenceOscillatorSource.External, IviDigitizerAttribute.IVIDIGITIZER_VAL_REFERENCE_OSCILLATOR_SOURCE_EXTERNAL)
            .Map(ReferenceOscillatorSource.PxiClk10, IviDigitizerAttribute.IVIDIGITIZER_VAL_REFERENCE_OSCILLATOR_SOURCE_PXI_CLK10)
            .Map(ReferenceOscillatorSource.PxiExpressClk100, IviDigitizerAttribute.IVIDIGITIZER_VAL_REFERENCE_OSCILLATOR_SOURCE_PXIE_CLK100);

        private static IviEnumCMapping<SampleClockSource, int> DigitizerSampleClockSource = IviEnumCMapping<SampleClockSource, int>.Instance
            .Map(SampleClockSource.Internal, IviDigitizerAttribute.IVIDIGITIZER_VAL_SAMPLE_CLOCK_SOURCE_INTERNAL)
            .Map(SampleClockSource.External, IviDigitizerAttribute.IVIDIGITIZER_VAL_SAMPLE_CLOCK_SOURCE_EXTERNAL);

        private static IviEnumCMapping<TemperatureUnits, int> DigitizerTemperatureUnits = IviEnumCMapping<TemperatureUnits, int>.Instance
            .Map(TemperatureUnits.Celsius, IviDigitizerAttribute.IVIDIGITIZER_VAL_CELSIUS)
            .Map(TemperatureUnits.Fahrenheit, IviDigitizerAttribute.IVIDIGITIZER_VAL_FAHRENHEIT)
            .Map(TemperatureUnits.Kelvin, IviDigitizerAttribute.IVIDIGITIZER_VAL_KELVIN);

        private static IviEnumCMapping<TriggerModifier, int> DigitizerTriggerModifier = IviEnumCMapping<TriggerModifier, int>.Instance
            .Map(TriggerModifier.None, IviDigitizerAttribute.IVIDIGITIZER_VAL_TRIGGER_MODIFIER_NONE)
            .Map(TriggerModifier.Auto, IviDigitizerAttribute.IVIDIGITIZER_VAL_TRIGGER_MODIFIER_AUTO)
            .Map(TriggerModifier.AutoLevel, IviDigitizerAttribute.IVIDIGITIZER_VAL_TRIGGER_MODIFIER_AUTO_LEVEL);

        private static IviEnumCMapping<TriggerSourceOperator, int> DigitizerTriggerSourceOperator = IviEnumCMapping<TriggerSourceOperator, int>.Instance
            .Map(TriggerSourceOperator.And, IviDigitizerAttribute.IVIDIGITIZER_VAL_TRIGGER_SOURCE_OPERATOR_AND)
            .Map(TriggerSourceOperator.Or, IviDigitizerAttribute.IVIDIGITIZER_VAL_TRIGGER_SOURCE_OPERATOR_OR)
            .Map(TriggerSourceOperator.None, IviDigitizerAttribute.IVIDIGITIZER_VAL_TRIGGER_SOURCE_OPERATOR_NONE);

        private static IviEnumCMapping<TriggerType, int> DigitizerTriggerType = IviEnumCMapping<TriggerType, int>.Instance
            .Map(TriggerType.Edge, IviDigitizerAttribute.IVIDIGITIZER_VAL_EDGE_TRIGGER)
            .Map(TriggerType.Width, IviDigitizerAttribute.IVIDIGITIZER_VAL_WIDTH_TRIGGER)
            .Map(TriggerType.Runt, IviDigitizerAttribute.IVIDIGITIZER_VAL_RUNT_TRIGGER)
            .Map(TriggerType.Glitch, IviDigitizerAttribute.IVIDIGITIZER_VAL_GLITCH_TRIGGER)
            .Map(TriggerType.TV, IviDigitizerAttribute.IVIDIGITIZER_VAL_TV_TRIGGER)
            .Map(TriggerType.Window, IviDigitizerAttribute.IVIDIGITIZER_VAL_WINDOW_TRIGGER);

        #endregion

        private static PrecisionTimeSpan DefaultIntervalPerPoint = new PrecisionTimeSpan((decimal)1);
        private IIviDigitizerAcquisition DigitizerAcquisition = null;
        private IIviDigitizerArm DigitizerArm = null;
        private IIviDigitizerCalibration DigitizerCalibration = null;
        private IIviDigitizerChannelCollection DigitizerChannelCollection = null;
        private IIviDigitizerReferenceOscillator DigitizerReferenceOscillator = null;
        private IIviDigitizerSampleClock DigitizerSampleClock = null;
        private IIviDigitizerTemperature DigitizerTemperature = null;
        public IviDigitizerAdapter(string name, bool idQuery, bool reset, string options)
            : base(name, idQuery, reset, options)
        {
            DigitizerAcquisition = new IviDigitizerAcquisition(this);
            DigitizerArm = new IviDigitizerArm(this);
            DigitizerCalibration = new IviDigitizerCalibration(this);
            DigitizerChannelCollection = new IviDigitizerChannelCollection(this);
            DigitizerReferenceOscillator = new IviDigitizerReferenceOscillator(this);
            DigitizerSampleClock = new IviDigitizerSampleClock(this);
            DigitizerTemperature = new IviDigitizerTemperature(this);
        }

        public IIviDigitizerAcquisition Acquisition
        {
            get { return DigitizerAcquisition; }
        }

        public IIviDigitizerArm Arm
        {
            get { return DigitizerArm; }
        }

        public IIviDigitizerCalibration Calibration
        {
            get { return DigitizerCalibration; }
        }

        public IIviDigitizerChannelCollection Channels
        {
            get { return DigitizerChannelCollection; }
        }

        public IIviDigitizerReferenceOscillator ReferenceOscillator
        {
            get { return DigitizerReferenceOscillator; }
        }

        public IIviDigitizerSampleClock SampleClock
        {
            get { return DigitizerSampleClock; }
        }

        public IIviDigitizerTemperature Temperature
        {
            get { return DigitizerTemperature; }
        }

        public IIviDigitizerTrigger Trigger
        {
            get { throw new NotImplementedException(); }
        }

        internal class IviDigitizerAcquisition : IIviDigitizerAcquisition
        {
            private IDriverAdapterBase Adapter;
            private IviCInterop.IviDigitizer IviDigitizerInterop = null;
            private IIviDigitizerAcquisitionStatus DigitizerAcquisitionStatus = null;
            public IviDigitizerAcquisition(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviDigitizerInterop = (IviCInterop.IviDigitizer)Adapter.Interop;
                DigitizerAcquisitionStatus = new IviDigitizerAcquisitionStatus(Adapter);
            }

            public void Abort()
            {
                Adapter.ViSessionStatusCheck(IviDigitizerInterop.Abort(Adapter.Session));
            }

            public void ConfigureAcquisition(long numberOfRecordsToAcquire, long recordSize, double sampleRate)
            {
                Adapter.ViSessionStatusCheck(IviDigitizerInterop.ConfigureAcquisition(Adapter.Session, numberOfRecordsToAcquire, recordSize, sampleRate));
            }

            public IWaveformCollection<double> CreateWaveformCollectionDouble(long numberOfWaveforms, long sizePerWaveform)
            {
                return new WaveformCollection<double>(numberOfWaveforms, sizePerWaveform);
            }

            public IWaveformCollection<short> CreateWaveformCollectionInt16(long numberOfWaveforms, long sizePerWaveform)
            {
                return new WaveformCollection<short>(numberOfWaveforms, sizePerWaveform);
            }

            public IWaveformCollection<int> CreateWaveformCollectionInt32(long numberOfWaveforms, long sizePerWaveform)
            {
                return new WaveformCollection<int>(numberOfWaveforms, sizePerWaveform);
            }

            public IWaveformCollection<sbyte> CreateWaveformCollectionSByte(long numberOfWaveforms, long sizePerWaveform)
            {
                return new WaveformCollection<sbyte>(numberOfWaveforms, sizePerWaveform);
            }

            public IWaveform<double> CreateWaveformDouble(long size)
            {
                return new Waveform<double>(DefaultIntervalPerPoint, size);
            }

            public IWaveform<short> CreateWaveformInt16(long size)
            {
                return new Waveform<short>(DefaultIntervalPerPoint, size);
            }

            public IWaveform<int> CreateWaveformInt32(long size)
            {
                return new Waveform<int>(DefaultIntervalPerPoint, size);
            }

            public IWaveform<sbyte> CreateWaveformSByte(long size)
            {
                return new Waveform<sbyte>(DefaultIntervalPerPoint, size);
            }

            public void Initiate()
            {
                Adapter.ViSessionStatusCheck(IviDigitizerInterop.InitiateAcquisition(Adapter.Session));
            }

            public long MaxFirstValidPointValue
            {
                get { return Adapter.GetAttributeViInt64(IviDigitizerAttribute.IVIDIGITIZER_ATTR_MAX_FIRST_VALID_POINT_VAL); }
            }

            public long MaxSamplesPerChannel
            {
                get { return Adapter.GetAttributeViInt64(IviDigitizerAttribute.IVIDIGITIZER_ATTR_MAX_SAMPLES_PER_CHANNEL); }
            }

            public long MinRecordSize
            {
                get { return Adapter.GetAttributeViInt64(IviDigitizerAttribute.IVIDIGITIZER_ATTR_MIN_RECORD_SIZE); }
            }

            public long NumberOfAcquiredRecords
            {
                get { return Adapter.GetAttributeViInt64(IviDigitizerAttribute.IVIDIGITIZER_ATTR_NUM_ACQUIRED_RECORDS); }
            }

            public long NumberOfRecordsToAcquire
            {
                get { return Adapter.GetAttributeViInt64(IviDigitizerAttribute.IVIDIGITIZER_ATTR_NUM_RECORDS_TO_ACQUIRE); }
                set { Adapter.SetAttributeViInt64(IviDigitizerAttribute.IVIDIGITIZER_ATTR_NUM_RECORDS_TO_ACQUIRE, value); }
            }

            public long QueryMinWaveformMemory(int dataWidth, long numberOfRecords, long offsetWithinRecord, long numberOfPointsPerRecord)
            {
                long NumSamples = 0;
                Adapter.ViSessionStatusCheck(IviDigitizerInterop.QueryMinWaveformMemory(Adapter.Session, dataWidth, numberOfRecords, offsetWithinRecord, numberOfPointsPerRecord, ref NumSamples));
                return NumSamples;
            }

            public long RecordSize
            {
                get { return Adapter.GetAttributeViInt64(IviDigitizerAttribute.IVIDIGITIZER_ATTR_RECORD_SIZE); }
                set { Adapter.SetAttributeViInt64(IviDigitizerAttribute.IVIDIGITIZER_ATTR_RECORD_SIZE, value); }
            }

            public SampleMode SampleMode
            {
                get { return DigitizerSampleMode.getEnum(Adapter.GetAttributeViInt32(IviDigitizerAttribute.IVIDIGITIZER_ATTR_SAMPLE_MODE)); }
                set { Adapter.SetAttributeViInt32(IviDigitizerAttribute.IVIDIGITIZER_ATTR_SAMPLE_MODE, DigitizerSampleMode.getC_Value(value)); }
            }

            public double SampleRate
            {
                get { return Adapter.GetAttributeViReal64(IviDigitizerAttribute.IVIDIGITIZER_ATTR_SAMPLE_RATE); }
                set { Adapter.SetAttributeViReal64(IviDigitizerAttribute.IVIDIGITIZER_ATTR_SAMPLE_RATE, value); }
            }

            public IIviDigitizerAcquisitionStatus Status
            {
                get { return DigitizerAcquisitionStatus; }
            }

            public bool TimeInterleavedChannelListAuto
            {
                get { return Adapter.GetAttributeViBoolean(IviDigitizerAttribute.IVIDIGITIZER_ATTR_TIME_INTERLEAVED_CHANNEL_LIST_AUTO); }
                set { Adapter.SetAttributeViBoolean(IviDigitizerAttribute.IVIDIGITIZER_ATTR_SAMPLE_RATE, value); }
            }

            public void WaitForAcquisitionComplete(PrecisionTimeSpan maxTime)
            {
                Adapter.ViSessionStatusCheck(IviDigitizerInterop.WaitForAcquisitionComplete(Adapter.Session, (int)maxTime.TotalMilliseconds));
            }
        }

        internal class WaveformCollection<T> : IWaveformCollection<T> where T : struct
        {
            private ArrayList Waveforms = null; // IList<IWaveform<T>> Waveforms = null;

            public WaveformCollection(long numberOfWaveforms, long sizePerWaveform)
            {
                Waveforms = new ArrayList(); // (IList<IWaveform<T>>)new ArrayList();
                for (int i = 0; i < numberOfWaveforms; i++)
                {
                    Waveforms.Add(new Waveform<T>(DefaultIntervalPerPoint, sizePerWaveform));
                }
            }

            public long ValidWaveformCount
            {
                get { return Waveforms.Count; }
            }

            public IWaveform<T> this[long index]
            {
                get
                {
                    return (IWaveform<T>)Waveforms[(int)index];
                }
                set
                {
                    Waveforms[(int)index] = value;
                }
            }

            public System.Collections.Generic.IEnumerator<T> GetEnumerator()
            {
                return (IEnumerator<T>)Waveforms.GetEnumerator();
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return Waveforms.GetEnumerator();
            }
        }

        internal class IviDigitizerAcquisitionStatus : IIviDigitizerAcquisitionStatus
        {
            private IDriverAdapterBase Adapter;
            public IviDigitizerAcquisitionStatus(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
            }

            public AcquisitionStatusResult IsIdle
            {
                get { return DigitizerAcquisitionStatusResult.getEnum(Adapter.GetAttributeViInt32(IviDigitizerAttribute.IVIDIGITIZER_ATTR_IS_IDLE)); }
            }

            public AcquisitionStatusResult IsMeasuring
            {
                get { return DigitizerAcquisitionStatusResult.getEnum(Adapter.GetAttributeViInt32(IviDigitizerAttribute.IVIDIGITIZER_ATTR_IS_MEASURING)); }
            }

            public AcquisitionStatusResult IsWaitingForArm
            {
                get { return DigitizerAcquisitionStatusResult.getEnum(Adapter.GetAttributeViInt32(IviDigitizerAttribute.IVIDIGITIZER_ATTR_IS_WAITING_FOR_ARM)); }
            }

            public AcquisitionStatusResult IsWaitingForTrigger
            {
                get { return DigitizerAcquisitionStatusResult.getEnum(Adapter.GetAttributeViInt32(IviDigitizerAttribute.IVIDIGITIZER_ATTR_IS_WAITING_FOR_TRIGGER)); }
            }
        }

        #region Digitizer Arm

        internal class IviDigitizerArm : IIviDigitizerArm
        {
            private IDriverAdapterBase Adapter;
            private IviCInterop.IviDigitizer IviDigitizerInterop = null;
            private IIviDigitizerMultiArm DigitizerMultiArm = null;
            public IviDigitizerArm(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviDigitizerInterop = (IviCInterop.IviDigitizer)Adapter.Interop;
                DigitizerMultiArm = new IviDigitizerMultiArm(Adapter);
            }

            public string ActiveSource
            {
                get { return Adapter.GetAttributeViString(IviDigitizerAttribute.IVIDIGITIZER_ATTR_ACTIVE_ARM_SOURCE); }
                set { Adapter.SetAttributeViString(IviDigitizerAttribute.IVIDIGITIZER_ATTR_ACTIVE_ARM_SOURCE, value); }
            }

            public int Count
            {
                get { return Adapter.GetAttributeViInt32(IviDigitizerAttribute.IVIDIGITIZER_ATTR_ARM_COUNT); }
                set { Adapter.SetAttributeViInt32(IviDigitizerAttribute.IVIDIGITIZER_ATTR_ARM_COUNT, value); }
            }

            public PrecisionTimeSpan Delay
            {
                get
                {
                    return new PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * Adapter.GetAttributeViReal64(IviDigitizerAttribute.IVIDIGITIZER_ATTR_ARM_DELAY))));
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviDigitizerAttribute.IVIDIGITIZER_ATTR_ARM_DELAY, value.TotalSeconds);
                }
            }

            public bool OutputEnabled
            {
                get { return Adapter.GetAttributeViBoolean(IviDigitizerAttribute.IVIDIGITIZER_ATTR_ARM_OUTPUT_ENABLED); }
                set { Adapter.SetAttributeViBoolean(IviDigitizerAttribute.IVIDIGITIZER_ATTR_ARM_OUTPUT_ENABLED, value); }
            }

            public void SendSoftwareArm()
            {
                Adapter.ViSessionStatusCheck(IviDigitizerInterop.SendSoftwareArm(Adapter.Session));
            }

            public IIviDigitizerMultiArm MultiArm
            {
                get { return DigitizerMultiArm; }
            }

            public IIviDigitizerArmSourceCollection Sources
            {
                get { throw new NotImplementedException(); }
            }
        }

        internal class IviDigitizerMultiArm : IIviDigitizerMultiArm
        {
            private IDriverAdapterBase Adapter;
            private IviCInterop.IviDigitizer IviDigitizerInterop = null;
            public IviDigitizerMultiArm(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviDigitizerInterop = (IviCInterop.IviDigitizer)Adapter.Interop;
            }

            public void Configure(string sourceList, ArmSourceOperator sourceOperator)
            {
                Adapter.ViSessionStatusCheck(IviDigitizerInterop.ConfigureMultiArm(Adapter.Session, sourceList, DigitizerArmSourceOperator.getC_Value(sourceOperator)));
            }

            public string SourceList
            {
                get { return Adapter.GetAttributeViString(IviDigitizerAttribute.IVIDIGITIZER_ATTR_ARM_SOURCE_LIST); }
                set { Adapter.SetAttributeViString(IviDigitizerAttribute.IVIDIGITIZER_ATTR_ARM_SOURCE_LIST, value); }
            }

            public ArmSourceOperator SourceOperator
            {
                get
                {
                    return DigitizerArmSourceOperator.getEnum(Adapter.GetAttributeViInt32(IviDigitizerAttribute.IVIDIGITIZER_ATTR_ARM_SOURCE_OPERATOR));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviDigitizerAttribute.IVIDIGITIZER_ATTR_ARM_SOURCE_OPERATOR, DigitizerArmSourceOperator.getC_Value(value));
                }
            }
        }

        internal class IviDigitizerArmSourceCollection : IIviDigitizerArmSourceCollection
        {
            private IDriverAdapterBase Adapter;
            private IviCInterop.IviDigitizer IviDigitizerInterop = null;
            private IList<IIviDigitizerArmSource> ArmSources = null;
            private IList<string> ArmSourceNames = null;
            public IviDigitizerArmSourceCollection(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviDigitizerInterop = (IviCInterop.IviDigitizer)Adapter.Interop;

                int OutputCount = Adapter.GetAttributeViInt32(IviDigitizerAttribute.IVIDIGITIZER_ATTR_ARM_SOURCE_COUNT);

                ArmSources = new List<IIviDigitizerArmSource>();
                ArmSourceNames = new List<string>();
                for (int Index = 1; Index <= OutputCount; Index++)
                {
                    IIviDigitizerArmSource ArmSource = new IviDigitizerArmSource(Adapter, Index);
                    ArmSources.Add(ArmSource);
                    ArmSourceNames.Add(ArmSource.Name);
                }
            }

            public int Count
            {
                get { return ArmSources.Count; }
            }

            public IIviDigitizerArmSource this[string name]
            {
                get { return ArmSources[ArmSourceNames.IndexOf(name)]; }
            }

            public IEnumerator<IIviDigitizerArmSource> GetEnumerator()
            {
                return ArmSources.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return ArmSources.GetEnumerator();
            }
        }

        internal class IviDigitizerArmSource : IIviDigitizerArmSource
        {
            private IDriverAdapterBase Adapter;
            private IviCInterop.IviDigitizer IviDigitizerInterop = null;
            private string ArmSourceName;
            private IIviDigitizerArmEdge DigitizerArmEdge = null;
            private IIviDigitizerArmGlitch DigitizerArmGlitch = null;
            private IIviDigitizerArmRunt DigitizerArmRunt = null;
            private IIviDigitizerArmTV DigitizerArmTV = null;
            private IIviDigitizerArmWidth DigitizerArmWidth = null;
            private IIviDigitizerArmWindow DigitizerArmWindow = null;
            public IviDigitizerArmSource(IDriverAdapterBase Adapter, int Index)
            {
                this.Adapter = Adapter;
                IviDigitizerInterop = (IviCInterop.IviDigitizer)Adapter.Interop;

                try
                {
                    StringBuilder NameValue = new StringBuilder(256);
                    Adapter.ViSessionStatusCheck(IviDigitizerInterop.GetArmSourceName(Adapter.Session, Index, NameValue.Capacity, NameValue));
                    ArmSourceName = NameValue.ToString();
                }
                catch
                {
                    ArmSourceName = string.Empty;
                }
                DigitizerArmEdge = new IviDigitizerArmEdge(Adapter, ArmSourceName);
                DigitizerArmGlitch = new IviDigitizerArmGlitch(Adapter, ArmSourceName);
                DigitizerArmRunt = new IviDigitizerArmRunt(Adapter, ArmSourceName);
                DigitizerArmTV = new IviDigitizerArmTV(Adapter, ArmSourceName);
                DigitizerArmWidth = new IviDigitizerArmWidth(Adapter, ArmSourceName);
                DigitizerArmWindow = new IviDigitizerArmWindow(Adapter, ArmSourceName);
            }

            public IIviDigitizerArmEdge Edge
            {
                get { return DigitizerArmEdge; }
            }

            public IIviDigitizerArmGlitch Glitch
            {
                get { return DigitizerArmGlitch; }
            }

            public IIviDigitizerArmRunt Runt
            {
                get { return DigitizerArmRunt; }
            }

            public IIviDigitizerArmTV TV
            {
                get { return DigitizerArmTV; }
            }

            public IIviDigitizerArmWidth Width
            {
                get { return DigitizerArmWidth; }
            }

            public IIviDigitizerArmWindow Window
            {
                get { return DigitizerArmWindow; }
            }

            public TriggerCoupling Coupling
            {
                get
                {
                    return DigitizerTriggerCoupling.getEnum(Adapter.GetAttributeViInt32(ArmSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_ARM_COUPLING));
                }
                set
                {
                    Adapter.SetAttributeViInt32(ArmSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_ARM_COUPLING, DigitizerTriggerCoupling.getC_Value(value));
                }
            }

            public double Hysteresis
            {
                get { return Adapter.GetAttributeViReal64(ArmSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_ARM_HYSTERESIS); }
                set { Adapter.SetAttributeViReal64(ArmSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_ARM_HYSTERESIS, value); }
            }

            public double Level
            {
                get { return Adapter.GetAttributeViReal64(ArmSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_ARM_LEVEL); }
                set { Adapter.SetAttributeViReal64(ArmSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_ARM_LEVEL, value); }
            }

            public ArmType Type
            {
                get
                {
                    return DigitizerArmType.getEnum(Adapter.GetAttributeViInt32(ArmSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_ARM_TYPE));
                }
                set
                {
                    Adapter.SetAttributeViInt32(ArmSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_ARM_TYPE, DigitizerArmType.getC_Value(value));
                }
            }

            public string Name
            {
                get { return ArmSourceName; }
            }
        }

        internal class IviDigitizerArmEdge : IIviDigitizerArmEdge
        {
            private IDriverAdapterBase Adapter;
            private IviCInterop.IviDigitizer IviDigitizerInterop = null;
            private string ArmSourceName;
            public IviDigitizerArmEdge(IDriverAdapterBase Adapter, string ArmSourceName)
            {
                this.Adapter = Adapter;
                IviDigitizerInterop = (IviCInterop.IviDigitizer)Adapter.Interop;
                this.ArmSourceName = ArmSourceName;
            }

            public void Configure(double level, Slope slope)
            {
                Adapter.ViSessionStatusCheck(IviDigitizerInterop.ConfigureEdgeArmSource(Adapter.Session, ArmSourceName, level, DigitizerSlope.getC_Value(slope)));
            }

            public Slope Slope
            {
                get
                {
                    return DigitizerSlope.getEnum(Adapter.GetAttributeViInt32(ArmSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_ARM_SLOPE));
                }
                set
                {
                    Adapter.SetAttributeViInt32(ArmSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_ARM_SLOPE, DigitizerSlope.getC_Value(value));
                }
            }
        }

        internal class IviDigitizerArmGlitch : IIviDigitizerArmGlitch
        {
            private IDriverAdapterBase Adapter;
            private IviCInterop.IviDigitizer IviDigitizerInterop = null;
            private string ArmSourceName;
            public IviDigitizerArmGlitch(IDriverAdapterBase Adapter, string ArmSourceName)
            {
                this.Adapter = Adapter;
                IviDigitizerInterop = (IviCInterop.IviDigitizer)Adapter.Interop;
                this.ArmSourceName = ArmSourceName;
            }

            public GlitchCondition Condition
            {
                get { return DigitizerGlitchCondition.getEnum(Adapter.GetAttributeViInt32(ArmSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_GLITCH_ARM_CONDITION)); }
                set { Adapter.SetAttributeViInt32(ArmSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_GLITCH_ARM_CONDITION, DigitizerGlitchCondition.getC_Value(value)); }
            }

            public void Configure(double level, PrecisionTimeSpan width, GlitchPolarity polarity, GlitchCondition condition)
            {
                Adapter.ViSessionStatusCheck(IviDigitizerInterop.ConfigureGlitchArmSource(Adapter.Session, ArmSourceName, level, width.TotalSeconds, DigitizerGlitchPolarity.getC_Value(polarity), DigitizerGlitchCondition.getC_Value(condition)));
            }

            public GlitchPolarity Polarity
            {
                get { return DigitizerGlitchPolarity.getEnum(Adapter.GetAttributeViInt32(ArmSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_GLITCH_ARM_POLARITY)); }
                set { Adapter.SetAttributeViInt32(ArmSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_GLITCH_ARM_POLARITY, DigitizerGlitchPolarity.getC_Value(value)); }
            }

            public PrecisionTimeSpan Width
            {
                get
                {
                    return new Ivi.Driver.PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * Adapter.GetAttributeViReal64(ArmSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_GLITCH_ARM_WIDTH))));
                }
                set
                {
                    Adapter.SetAttributeViReal64(ArmSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_GLITCH_ARM_WIDTH, value.TotalSeconds);
                }
            }
        }

        internal class IviDigitizerArmRunt : IIviDigitizerArmRunt
        {
            private IDriverAdapterBase Adapter;
            private IviCInterop.IviDigitizer IviDigitizerInterop = null;
            private string ArmSourceName;
            public IviDigitizerArmRunt(IDriverAdapterBase Adapter, string ArmSourceName)
            {
                this.Adapter = Adapter;
                IviDigitizerInterop = (IviCInterop.IviDigitizer)Adapter.Interop;
                this.ArmSourceName = ArmSourceName;
            }

            public void Configure(double thresholdLow, double thresholdHigh, RuntPolarity polarity)
            {
                Adapter.ViSessionStatusCheck(IviDigitizerInterop.ConfigureRuntArmSource(Adapter.Session, ArmSourceName, thresholdLow, thresholdHigh, DigitizerRuntPolarity.getC_Value(polarity)));
            }

            public RuntPolarity Polarity
            {
                get { return DigitizerRuntPolarity.getEnum(Adapter.GetAttributeViInt32(ArmSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_RUNT_ARM_POLARITY)); }
                set { Adapter.SetAttributeViInt32(ArmSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_RUNT_ARM_POLARITY, DigitizerRuntPolarity.getC_Value(value)); }
            }

            public double ThresholdHigh
            {
                get { return Adapter.GetAttributeViReal64(ArmSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_RUNT_ARM_HIGH_THRESHOLD); }
                set { Adapter.SetAttributeViReal64(ArmSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_RUNT_ARM_HIGH_THRESHOLD, value); }
            }

            public double ThresholdLow
            {
                get { return Adapter.GetAttributeViReal64(ArmSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_RUNT_ARM_LOW_THRESHOLD); }
                set { Adapter.SetAttributeViReal64(ArmSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_RUNT_ARM_LOW_THRESHOLD, value); }
            }
        }

        internal class IviDigitizerArmTV : IIviDigitizerArmTV
        {
            private IDriverAdapterBase Adapter;
            private IviCInterop.IviDigitizer IviDigitizerInterop = null;
            private string ArmSourceName;
            public IviDigitizerArmTV(IDriverAdapterBase Adapter, string ArmSourceName)
            {
                this.Adapter = Adapter;
                IviDigitizerInterop = (IviCInterop.IviDigitizer)Adapter.Interop;
                this.ArmSourceName = ArmSourceName;
            }

            public void Configure(TVSignalFormat signalFormat, TVTriggerEvent triggerEvent, TVTriggerPolarity polarity)
            {
                Adapter.ViSessionStatusCheck(IviDigitizerInterop.ConfigureTVArmSource(Adapter.Session, ArmSourceName, DigitizerTVSignalFormat.getC_Value(signalFormat), DigitizerTVTriggerEvent.getC_Value(triggerEvent), DigitizerTVTriggerPolarity.getC_Value(polarity)));
            }

            public int LineNumber
            {
                get { return Adapter.GetAttributeViInt32(ArmSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_TV_ARM_LINE_NUMBER); }
                set { Adapter.SetAttributeViInt32(ArmSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_TV_ARM_LINE_NUMBER, value); }
            }

            public TVTriggerPolarity Polarity
            {
                get { return DigitizerTVTriggerPolarity.getEnum(Adapter.GetAttributeViInt32(ArmSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_TV_ARM_POLARITY)); }
                set { Adapter.SetAttributeViInt32(ArmSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_TV_ARM_POLARITY, DigitizerTVTriggerPolarity.getC_Value(value)); }
            }

            public TVSignalFormat SignalFormat
            {
                get { return DigitizerTVSignalFormat.getEnum(Adapter.GetAttributeViInt32(ArmSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_TV_ARM_SIGNAL_FORMAT)); }
                set { Adapter.SetAttributeViInt32(ArmSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_TV_ARM_SIGNAL_FORMAT, DigitizerTVSignalFormat.getC_Value(value)); }
            }

            public TVTriggerEvent TriggerEvent
            {
                get { return DigitizerTVTriggerEvent.getEnum(Adapter.GetAttributeViInt32(ArmSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_TV_ARM_EVENT)); }
                set { Adapter.SetAttributeViInt32(ArmSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_TV_ARM_EVENT, DigitizerTVTriggerEvent.getC_Value(value)); }
            }
        }

        internal class IviDigitizerArmWidth : IIviDigitizerArmWidth
        {
            private IDriverAdapterBase Adapter;
            private IviCInterop.IviDigitizer IviDigitizerInterop = null;
            private string ArmSourceName;
            public IviDigitizerArmWidth(IDriverAdapterBase Adapter, string ArmSourceName)
            {
                this.Adapter = Adapter;
                IviDigitizerInterop = (IviCInterop.IviDigitizer)Adapter.Interop;
                this.ArmSourceName = ArmSourceName;
            }

            public WidthCondition Condition
            {
                get { return DigitizerWidthCondition.getEnum(Adapter.GetAttributeViInt32(ArmSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_WIDTH_ARM_CONDITION)); }
                set { Adapter.SetAttributeViInt32(ArmSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_WIDTH_ARM_CONDITION, DigitizerWidthCondition.getC_Value(value)); }
            }

            public void Configure(double level, PrecisionTimeSpan thresholdLow, PrecisionTimeSpan thresholdHigh, WidthPolarity polarity, WidthCondition condition)
            {
                Adapter.ViSessionStatusCheck(IviDigitizerInterop.ConfigureWidthArmSource(Adapter.Session, ArmSourceName, level, thresholdLow.TotalSeconds, thresholdHigh.TotalSeconds, DigitizerWidthPolarity.getC_Value(polarity), DigitizerWidthCondition.getC_Value(condition)));
            }

            public WidthPolarity Polarity
            {
                get { return DigitizerWidthPolarity.getEnum(Adapter.GetAttributeViInt32(ArmSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_WIDTH_ARM_POLARITY)); }
                set { Adapter.SetAttributeViInt32(ArmSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_WIDTH_ARM_POLARITY, DigitizerWidthPolarity.getC_Value(value)); }
            }

            public PrecisionTimeSpan ThresholdHigh
            {
                get
                {
                    return new Ivi.Driver.PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * Adapter.GetAttributeViReal64(ArmSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_WIDTH_ARM_HIGH_THRESHOLD))));
                }
                set
                {
                    Adapter.SetAttributeViReal64(ArmSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_WIDTH_ARM_HIGH_THRESHOLD, value.TotalSeconds);
                }
            }

            public PrecisionTimeSpan ThresholdLow
            {
                get
                {
                    return new Ivi.Driver.PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * Adapter.GetAttributeViReal64(ArmSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_WIDTH_ARM_LOW_THRESHOLD))));
                }
                set
                {
                    Adapter.SetAttributeViReal64(ArmSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_WIDTH_ARM_LOW_THRESHOLD, value.TotalSeconds);
                }
            }
        }

        internal class IviDigitizerArmWindow : IIviDigitizerArmWindow
        {
            private IDriverAdapterBase Adapter;
            private IviCInterop.IviDigitizer IviDigitizerInterop = null;
            private string ArmSourceName;
            public IviDigitizerArmWindow(IDriverAdapterBase Adapter, string ArmSourceName)
            {
                this.Adapter = Adapter;
                IviDigitizerInterop = (IviCInterop.IviDigitizer)Adapter.Interop;
                this.ArmSourceName = ArmSourceName;
            }

            public WindowCondition Condition
            {
                get { return DigitizerWindowCondition.getEnum(Adapter.GetAttributeViInt32(ArmSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_WINDOW_ARM_CONDITION)); }
                set { Adapter.SetAttributeViInt32(ArmSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_WINDOW_ARM_CONDITION, DigitizerWindowCondition.getC_Value(value)); }
            }

            public void Configure(double thresholdLow, double thresholdHigh, WindowCondition condition)
            {
                Adapter.ViSessionStatusCheck(IviDigitizerInterop.ConfigureWindowArmSource(Adapter.Session, ArmSourceName, thresholdLow, thresholdHigh, DigitizerWindowCondition.getC_Value(condition)));
            }

            public double ThresholdHigh
            {
                get { return Adapter.GetAttributeViReal64(ArmSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_WINDOW_ARM_HIGH_THRESHOLD); }
                set { Adapter.SetAttributeViReal64(ArmSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_WINDOW_ARM_HIGH_THRESHOLD, value); }
            }

            public double ThresholdLow
            {
                get { return Adapter.GetAttributeViReal64(ArmSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_WINDOW_ARM_LOW_THRESHOLD); }
                set { Adapter.SetAttributeViReal64(ArmSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_WINDOW_ARM_LOW_THRESHOLD, value); }
            }
        }

        #endregion

        internal class IviDigitizerCalibration : IIviDigitizerCalibration
        {
            private IDriverAdapterBase Adapter;
            private IviCInterop.IviDigitizer IviDigitizerInterop = null;
            public IviDigitizerCalibration(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviDigitizerInterop = (IviCInterop.IviDigitizer)Adapter.Interop;
            }

            public void SelfCalibrate()
            {
                Adapter.ViSessionStatusCheck(IviDigitizerInterop.SelfCalibrate(Adapter.Session));
            }
        }

        #region Digitizer Channel

        internal class IviDigitizerChannelCollection : IIviDigitizerChannelCollection
        {
            private IDriverAdapterBase Adapter;
            private IviCInterop.IviDigitizer IviDigitizerInterop = null;
            private IList<IIviDigitizerChannel> Channels = null;
            private IList<string> ChannelNames = null;
            public IviDigitizerChannelCollection(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviDigitizerInterop = (IviCInterop.IviDigitizer)Adapter.Interop;

                int OutputCount = Adapter.GetAttributeViInt32(IviDigitizerAttribute.IVIDIGITIZER_ATTR_CHANNEL_COUNT);

                Channels = new List<IIviDigitizerChannel>();
                ChannelNames = new List<string>();
                for (int Index = 1; Index <= OutputCount; Index++)
                {
                    IIviDigitizerChannel Channel = new IviDigitizerChannel(Adapter, Index);
                    Channels.Add(Channel);
                    ChannelNames.Add(Channel.Name);
                }
            }

            public int Count
            {
                get { return Channels.Count; }
            }

            public IIviDigitizerChannel this[string name]
            {
                get { return Channels[ChannelNames.IndexOf(name)]; }
            }

            public IEnumerator<IIviDigitizerChannel> GetEnumerator()
            {
                return Channels.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
               return Channels.GetEnumerator();
            }
        }

        internal class IviDigitizerChannel : IIviDigitizerChannel
        {
            private IDriverAdapterBase Adapter;
            private IviCInterop.IviDigitizer IviDigitizerInterop = null;
            private string ChannelName;
            private IIviDigitizerChannelDownconversion DigitizerChannelDownconversion = null;
            private IIviDigitizerChannelFilter DigitizerChannelFilter = null;
            private IIviDigitizerChannelMeasurement DigitizerChannelMeasurement = null;
            private IIviDigitizerChannelMultiRecordMeasurement DigitizerChannelMultiRecordMeasurement = null;
            public IviDigitizerChannel(IDriverAdapterBase Adapter, int Index)
            {
                this.Adapter = Adapter;
                IviDigitizerInterop = (IviCInterop.IviDigitizer)Adapter.Interop;

                try
                {
                    StringBuilder NameValue = new StringBuilder(256);
                    Adapter.ViSessionStatusCheck(IviDigitizerInterop.GetChannelName(Adapter.Session, Index, NameValue.Capacity, NameValue));
                    ChannelName = NameValue.ToString();
                }
                catch
                {
                    ChannelName = string.Empty;
                }

                DigitizerChannelDownconversion = new IviDigitizerChannelDownconversion(Adapter, ChannelName);
                DigitizerChannelFilter = new IviDigitizerChannelFilter(Adapter, ChannelName);
                DigitizerChannelMeasurement = new IviDigitizerChannelMeasurement(Adapter, ChannelName);
                DigitizerChannelMultiRecordMeasurement = new IviDigitizerChannelMultiRecordMeasurement(Adapter, ChannelName);
            }

            public void Configure(double range, double offset, VerticalCoupling coupling, bool enabled)
            {
                Adapter.ViSessionStatusCheck(IviDigitizerInterop.ConfigureChannel(Adapter.Session, ChannelName, range, offset, DigitizerVerticalCoupling.getC_Value(coupling), enabled));
            }

            public VerticalCoupling Coupling
            {
                get { return DigitizerVerticalCoupling.getEnum(Adapter.GetAttributeViInt32(ChannelName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_VERTICAL_COUPLING)); }
                set { Adapter.SetAttributeViInt32(ChannelName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_VERTICAL_COUPLING, DigitizerVerticalCoupling.getC_Value(value)); }
            }

            public string DataInterleavedChannelList
            {
                get { return Adapter.GetAttributeViString(ChannelName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_DATA_INTERLEAVED_CHANNEL_LIST); }
                set { Adapter.SetAttributeViString(ChannelName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_DATA_INTERLEAVED_CHANNEL_LIST, value); }
            }

            public IIviDigitizerChannelDownconversion Downconversion
            {
                get { return DigitizerChannelDownconversion; }
            }

            public bool Enabled
            {
                get { return Adapter.GetAttributeViBoolean(ChannelName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_CHANNEL_ENABLED); }
                set { Adapter.SetAttributeViBoolean(ChannelName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_CHANNEL_ENABLED, value); }
            }

            public IIviDigitizerChannelFilter Filter
            {
                get { return DigitizerChannelFilter; }
            }

            public int InputConnectorSelection
            {
                get { return Adapter.GetAttributeViInt32(ChannelName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_INPUT_CONNECTOR_SELECTION); }
                set { Adapter.SetAttributeViInt32(ChannelName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_INPUT_CONNECTOR_SELECTION, value); }
            }

            public double InputImpedance
            {
                get { return Adapter.GetAttributeViReal64(ChannelName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_INPUT_IMPEDANCE); }
                set { Adapter.SetAttributeViReal64(ChannelName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_INPUT_IMPEDANCE, value); }
            }

            public IIviDigitizerChannelMeasurement Measurement
            {
                get { return DigitizerChannelMeasurement; }
            }

            public IIviDigitizerChannelMultiRecordMeasurement MultiRecordMeasurement
            {
                get { return DigitizerChannelMultiRecordMeasurement; }
            }

            public double Offset
            {
                get { return Adapter.GetAttributeViReal64(ChannelName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_VERTICAL_OFFSET); }
                set { Adapter.SetAttributeViReal64(ChannelName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_VERTICAL_OFFSET, value); }
            }

            public double Range
            {
                get { return Adapter.GetAttributeViReal64(ChannelName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_VERTICAL_RANGE); }
                set { Adapter.SetAttributeViReal64(ChannelName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_VERTICAL_RANGE, value); }
            }

            public double Temperature
            {
                get { return Adapter.GetAttributeViReal64(ChannelName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_CHANNEL_TEMPERATURE); }
            }

            public string TimeInterleavedChannelList
            {
                get { return Adapter.GetAttributeViString(ChannelName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_TIME_INTERLEAVED_CHANNEL_LIST); }
                set { Adapter.SetAttributeViString(ChannelName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_TIME_INTERLEAVED_CHANNEL_LIST, value); }
            }

            public string Name
            {
                get { return ChannelName; }
            }
        }

        internal class IviDigitizerChannelDownconversion : IIviDigitizerChannelDownconversion
        {
            private IDriverAdapterBase Adapter;
            private IviCInterop.IviDigitizer IviDigitizerInterop = null;
            private string ChannelName;
            public IviDigitizerChannelDownconversion(IDriverAdapterBase Adapter, string ChannelName)
            {
                this.Adapter = Adapter;
                IviDigitizerInterop = (IviCInterop.IviDigitizer)Adapter.Interop;
                this.ChannelName = ChannelName;
            }

            public double CenterFrequency
            {
                get { return Adapter.GetAttributeViReal64(ChannelName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_DOWNCONVERSION_CENTER_FREQUENCY); }
                set { Adapter.SetAttributeViReal64(ChannelName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_DOWNCONVERSION_CENTER_FREQUENCY, value); }
            }

            public void Configure(bool enabled, double centerFrequency)
            {
                Adapter.ViSessionStatusCheck(IviDigitizerInterop.ConfigureDownconversion(Adapter.Session, ChannelName, enabled, centerFrequency));
            }

            public bool Enabled
            {
                get { return Adapter.GetAttributeViBoolean(ChannelName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_DOWNCONVERSION_ENABLED); }
                set { Adapter.SetAttributeViBoolean(ChannelName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_DOWNCONVERSION_ENABLED, value); }
            }

            public bool IQInterleaved
            {
                get { return Adapter.GetAttributeViBoolean(ChannelName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_DOWNCONVERSION_IQ_INTERLEAVED); }
                set { Adapter.SetAttributeViBoolean(ChannelName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_DOWNCONVERSION_IQ_INTERLEAVED, value); }
            }
        }

        internal class IviDigitizerChannelFilter : IIviDigitizerChannelFilter
        {
            private IDriverAdapterBase Adapter;
            private IviCInterop.IviDigitizer IviDigitizerInterop = null;
            private string ChannelName;
            public IviDigitizerChannelFilter(IDriverAdapterBase Adapter, string ChannelName)
            {
                this.Adapter = Adapter;
                IviDigitizerInterop = (IviCInterop.IviDigitizer)Adapter.Interop;
                this.ChannelName = ChannelName;
            }

            public bool Bypass
            {
                get { return Adapter.GetAttributeViBoolean(ChannelName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_INPUT_FILTER_BYPASS); }
                set { Adapter.SetAttributeViBoolean(ChannelName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_INPUT_FILTER_BYPASS, value); }
            }

            public void Configure(double minFrequency, double maxFrequency)
            {
                Adapter.ViSessionStatusCheck(IviDigitizerInterop.ConfigureInputFilter(Adapter.Session, ChannelName, minFrequency, maxFrequency));
            }

            public double MaxFrequency
            {
                get { return Adapter.GetAttributeViReal64(ChannelName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_INPUT_FILTER_MAX_FREQUENCY); }
                set { Adapter.SetAttributeViReal64(ChannelName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_INPUT_FILTER_MAX_FREQUENCY, value); }
            }

            public double MinFrequency
            {
                get { return Adapter.GetAttributeViReal64(ChannelName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_INPUT_FILTER_MIN_FREQUENCY); }
                set { Adapter.SetAttributeViReal64(ChannelName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_INPUT_FILTER_MIN_FREQUENCY, value); }
            }
        }

        internal class IviDigitizerChannelMeasurement : IIviDigitizerChannelMeasurement
        {
            private IDriverAdapterBase Adapter;
            private IviCInterop.IviDigitizer IviDigitizerInterop = null;
            private string ChannelName;
            public IviDigitizerChannelMeasurement(IDriverAdapterBase Adapter, string ChannelName)
            {
                this.Adapter = Adapter;
                IviDigitizerInterop = (IviCInterop.IviDigitizer)Adapter.Interop;
                this.ChannelName = ChannelName;
            }

            #region FetchWaveform

            public IWaveform<double> FetchWaveform(IWaveform<double> waveform)
            {
                PrecisionTimeSpan StartTime = PrecisionTimeSpan.Zero;
                PrecisionTimeSpan IntervalPerPoint = PrecisionTimeSpan.Zero;

                long ActualPoints = 0;
                long FirstValidPoint = 0;
                double InitialXOffset = double.NaN;
                double InitialXTimeSeconds = double.NaN;
                double InitialXTimeFraction = double.NaN;
                double XIncrement = double.NaN;

                IntPtr pMeasure = Marshal.AllocHGlobal((int)(waveform.Capacity * sizeof(double)));
                Adapter.ViSessionStatusCheck(IviDigitizerInterop.FetchWaveformReal64(Adapter.Session, ChannelName, waveform.Capacity, pMeasure, ref ActualPoints, ref FirstValidPoint, ref InitialXOffset, ref InitialXTimeSeconds, ref InitialXTimeFraction, ref XIncrement));

                double[] readingBuffer = new double[ActualPoints];
                Marshal.Copy(pMeasure, readingBuffer, (int)FirstValidPoint, (int)ActualPoints);
                Marshal.FreeHGlobal(pMeasure);

                StartTime = new PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * InitialXTimeSeconds)));
                IntervalPerPoint = new PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * XIncrement)));
                waveform.Configure(StartTime, IntervalPerPoint, ActualPoints);
                waveform.PutElements(0, readingBuffer);

                return waveform;
            }

            public IWaveform<int> FetchWaveform(IWaveform<int> waveform)
            {
                PrecisionTimeSpan StartTime = PrecisionTimeSpan.Zero;
                PrecisionTimeSpan IntervalPerPoint = PrecisionTimeSpan.Zero;

                long ActualPoints = 0;
                long FirstValidPoint = 0;
                double InitialXOffset = double.NaN;
                double InitialXTimeSeconds = double.NaN;
                double InitialXTimeFraction = double.NaN;
                double XIncrement = double.NaN;
                double ScaleFactor = double.NaN;
                double ScaleOffset = double.NaN;

                IntPtr pMeasure = Marshal.AllocHGlobal((int)(waveform.Capacity * sizeof(int)));
                Adapter.ViSessionStatusCheck(IviDigitizerInterop.FetchWaveformInt32(Adapter.Session, ChannelName, waveform.Capacity, pMeasure, ref ActualPoints, ref FirstValidPoint, ref InitialXOffset, ref InitialXTimeSeconds, ref InitialXTimeFraction, ref XIncrement, ref ScaleFactor, ref ScaleOffset));

                int[] readingBuffer = new int[ActualPoints];
                Marshal.Copy(pMeasure, readingBuffer, (int)FirstValidPoint, (int)ActualPoints);
                Marshal.FreeHGlobal(pMeasure);

                StartTime = new PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * InitialXTimeSeconds)));
                IntervalPerPoint = new PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * XIncrement)));
                waveform.Configure(StartTime, IntervalPerPoint, ActualPoints);
                waveform.Scale = ScaleFactor;
                waveform.Offset = ScaleOffset;
                waveform.PutElements(0, readingBuffer);

                return waveform;
            }

            public IWaveform<short> FetchWaveform(IWaveform<short> waveform)
            {
                PrecisionTimeSpan StartTime = PrecisionTimeSpan.Zero;
                PrecisionTimeSpan IntervalPerPoint = PrecisionTimeSpan.Zero;

                long ActualPoints = 0;
                long FirstValidPoint = 0;
                double InitialXOffset = double.NaN;
                double InitialXTimeSeconds = double.NaN;
                double InitialXTimeFraction = double.NaN;
                double XIncrement = double.NaN;
                double ScaleFactor = double.NaN;
                double ScaleOffset = double.NaN;

                IntPtr pMeasure = Marshal.AllocHGlobal((int)(waveform.Capacity * sizeof(short)));
                Adapter.ViSessionStatusCheck(IviDigitizerInterop.FetchWaveformInt16(Adapter.Session, ChannelName, waveform.Capacity, pMeasure, ref ActualPoints, ref FirstValidPoint, ref InitialXOffset, ref InitialXTimeSeconds, ref InitialXTimeFraction, ref XIncrement, ref ScaleFactor, ref ScaleOffset));

                short[] readingBuffer = new short[ActualPoints];
                Marshal.Copy(pMeasure, readingBuffer, (int)FirstValidPoint, (int)ActualPoints);
                Marshal.FreeHGlobal(pMeasure);

                StartTime = new PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * InitialXTimeSeconds)));
                IntervalPerPoint = new PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * XIncrement)));
                waveform.Configure(StartTime, IntervalPerPoint, ActualPoints);
                waveform.Scale = ScaleFactor;
                waveform.Offset = ScaleOffset;
                waveform.PutElements(0, readingBuffer);

                return waveform;
            }

            public IWaveform<sbyte> FetchWaveform(IWaveform<sbyte> waveform)
            {
                PrecisionTimeSpan StartTime = PrecisionTimeSpan.Zero;
                PrecisionTimeSpan IntervalPerPoint = PrecisionTimeSpan.Zero;

                long ActualPoints = 0;
                long FirstValidPoint = 0;
                double InitialXOffset = double.NaN;
                double InitialXTimeSeconds = double.NaN;
                double InitialXTimeFraction = double.NaN;
                double XIncrement = double.NaN;
                double ScaleFactor = double.NaN;
                double ScaleOffset = double.NaN;

                IntPtr pMeasure = Marshal.AllocHGlobal((int)(waveform.Capacity * sizeof(sbyte)));
                Adapter.ViSessionStatusCheck(IviDigitizerInterop.FetchWaveformInt8(Adapter.Session, ChannelName, waveform.Capacity, pMeasure, ref ActualPoints, ref FirstValidPoint, ref InitialXOffset, ref InitialXTimeSeconds, ref InitialXTimeFraction, ref XIncrement, ref ScaleFactor, ref ScaleOffset));

                byte[] readingBuffer = new byte[ActualPoints];
                Marshal.Copy(pMeasure, (byte[])readingBuffer, (int)FirstValidPoint, (int)ActualPoints);
                Marshal.FreeHGlobal(pMeasure);

                StartTime = new PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * InitialXTimeSeconds)));
                IntervalPerPoint = new PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * XIncrement)));
                waveform.Configure(StartTime, IntervalPerPoint, ActualPoints);
                waveform.Scale = ScaleFactor;
                waveform.Offset = ScaleOffset;
                waveform.PutElements(0, Array.ConvertAll<byte, sbyte>(readingBuffer, new Converter<byte,sbyte>(delegate(byte data)
                    {
                        return (sbyte)data;
                    })));

                return waveform;
            }

            #endregion

            #region ReadWaveform

            public IWaveform<double> ReadWaveform(PrecisionTimeSpan maximumTime, IWaveform<double> waveform)
            {
                PrecisionTimeSpan StartTime = PrecisionTimeSpan.Zero;
                PrecisionTimeSpan IntervalPerPoint = PrecisionTimeSpan.Zero;

                long ActualPoints = 0;
                long FirstValidPoint = 0;
                double InitialXOffset = double.NaN;
                double InitialXTimeSeconds = double.NaN;
                double InitialXTimeFraction = double.NaN;
                double XIncrement = double.NaN;

                IntPtr pMeasure = Marshal.AllocHGlobal((int)(waveform.Capacity * sizeof(double)));
                Adapter.ViSessionStatusCheck(IviDigitizerInterop.ReadWaveformReal64(Adapter.Session, ChannelName, (int)maximumTime.TotalMilliseconds, waveform.Capacity, pMeasure, ref ActualPoints, ref FirstValidPoint, ref InitialXOffset, ref InitialXTimeSeconds, ref InitialXTimeFraction, ref XIncrement));

                double[] readingBuffer = new double[ActualPoints];
                Marshal.Copy(pMeasure, readingBuffer, (int)FirstValidPoint, (int)ActualPoints);
                Marshal.FreeHGlobal(pMeasure);

                StartTime = new PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * InitialXTimeSeconds)));
                IntervalPerPoint = new PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * XIncrement)));
                waveform.Configure(StartTime, IntervalPerPoint, ActualPoints);
                waveform.PutElements(0, readingBuffer);

                return waveform;
            }

            public IWaveform<int> ReadWaveform(PrecisionTimeSpan maximumTime, IWaveform<int> waveform)
            {
                PrecisionTimeSpan StartTime = PrecisionTimeSpan.Zero;
                PrecisionTimeSpan IntervalPerPoint = PrecisionTimeSpan.Zero;

                long ActualPoints = 0;
                long FirstValidPoint = 0;
                double InitialXOffset = double.NaN;
                double InitialXTimeSeconds = double.NaN;
                double InitialXTimeFraction = double.NaN;
                double XIncrement = double.NaN;
                double ScaleFactor = double.NaN;
                double ScaleOffset = double.NaN;

                IntPtr pMeasure = Marshal.AllocHGlobal((int)(waveform.Capacity * sizeof(int)));
                Adapter.ViSessionStatusCheck(IviDigitizerInterop.ReadWaveformInt32(Adapter.Session, ChannelName, (int)maximumTime.TotalMilliseconds, waveform.Capacity, pMeasure, ref ActualPoints, ref FirstValidPoint, ref InitialXOffset, ref InitialXTimeSeconds, ref InitialXTimeFraction, ref XIncrement, ref ScaleFactor, ref ScaleOffset));

                int[] readingBuffer = new int[ActualPoints];
                Marshal.Copy(pMeasure, readingBuffer, (int)FirstValidPoint, (int)ActualPoints);
                Marshal.FreeHGlobal(pMeasure);

                StartTime = new PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * InitialXTimeSeconds)));
                IntervalPerPoint = new PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * XIncrement)));
                waveform.Configure(StartTime, IntervalPerPoint, ActualPoints);
                waveform.Scale = ScaleFactor;
                waveform.Offset = ScaleOffset;
                waveform.PutElements(0, readingBuffer);

                return waveform;
            }

            public IWaveform<short> ReadWaveform(PrecisionTimeSpan maximumTime, IWaveform<short> waveform)
            {
                PrecisionTimeSpan StartTime = PrecisionTimeSpan.Zero;
                PrecisionTimeSpan IntervalPerPoint = PrecisionTimeSpan.Zero;

                long ActualPoints = 0;
                long FirstValidPoint = 0;
                double InitialXOffset = double.NaN;
                double InitialXTimeSeconds = double.NaN;
                double InitialXTimeFraction = double.NaN;
                double XIncrement = double.NaN;
                double ScaleFactor = double.NaN;
                double ScaleOffset = double.NaN;

                IntPtr pMeasure = Marshal.AllocHGlobal((int)(waveform.Capacity * sizeof(short)));
                Adapter.ViSessionStatusCheck(IviDigitizerInterop.ReadWaveformInt16(Adapter.Session, ChannelName, (int)maximumTime.TotalMilliseconds, waveform.Capacity, pMeasure, ref ActualPoints, ref FirstValidPoint, ref InitialXOffset, ref InitialXTimeSeconds, ref InitialXTimeFraction, ref XIncrement, ref ScaleFactor, ref ScaleOffset));

                short[] readingBuffer = new short[ActualPoints];
                Marshal.Copy(pMeasure, readingBuffer, (int)FirstValidPoint, (int)ActualPoints);
                Marshal.FreeHGlobal(pMeasure);

                StartTime = new PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * InitialXTimeSeconds)));
                IntervalPerPoint = new PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * XIncrement)));
                waveform.Configure(StartTime, IntervalPerPoint, ActualPoints);
                waveform.Scale = ScaleFactor;
                waveform.Offset = ScaleOffset;
                waveform.PutElements(0, readingBuffer);

                return waveform;
            }

            public IWaveform<sbyte> ReadWaveform(PrecisionTimeSpan maximumTime, IWaveform<sbyte> waveform)
            {
                PrecisionTimeSpan StartTime = PrecisionTimeSpan.Zero;
                PrecisionTimeSpan IntervalPerPoint = PrecisionTimeSpan.Zero;

                long ActualPoints = 0;
                long FirstValidPoint = 0;
                double InitialXOffset = double.NaN;
                double InitialXTimeSeconds = double.NaN;
                double InitialXTimeFraction = double.NaN;
                double XIncrement = double.NaN;
                double ScaleFactor = double.NaN;
                double ScaleOffset = double.NaN;

                IntPtr pMeasure = Marshal.AllocHGlobal((int)(waveform.Capacity * sizeof(sbyte)));
                Adapter.ViSessionStatusCheck(IviDigitizerInterop.ReadWaveformInt8(Adapter.Session, ChannelName, (int)maximumTime.TotalMilliseconds, waveform.Capacity, pMeasure, ref ActualPoints, ref FirstValidPoint, ref InitialXOffset, ref InitialXTimeSeconds, ref InitialXTimeFraction, ref XIncrement, ref ScaleFactor, ref ScaleOffset));

                byte[] readingBuffer = new byte[ActualPoints];
                Marshal.Copy(pMeasure, (byte[])readingBuffer, (int)FirstValidPoint, (int)ActualPoints);
                Marshal.FreeHGlobal(pMeasure);

                StartTime = new PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * InitialXTimeSeconds)));
                IntervalPerPoint = new PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * XIncrement)));
                waveform.Configure(StartTime, IntervalPerPoint, ActualPoints);
                waveform.Scale = ScaleFactor;
                waveform.Offset = ScaleOffset;
                waveform.PutElements(0, Array.ConvertAll<byte, sbyte>(readingBuffer, new Converter<byte, sbyte>(delegate(byte data)
                {
                    return (sbyte)data;
                })));

                return waveform;
            }

            #endregion
        }

        internal class IviDigitizerChannelMultiRecordMeasurement : IIviDigitizerChannelMultiRecordMeasurement
        {
            private IDriverAdapterBase Adapter;
            private IviCInterop.IviDigitizer IviDigitizerInterop = null;
            private string ChannelName;
            public IviDigitizerChannelMultiRecordMeasurement(IDriverAdapterBase Adapter, string ChannelName)
            {
                this.Adapter = Adapter;
                IviDigitizerInterop = (IviCInterop.IviDigitizer)Adapter.Interop;
                this.ChannelName = ChannelName;
            }

            public IWaveformCollection<double> FetchMultiRecordWaveform(long firstRecord, long numberOfRecords, long offsetWithinRecord, long numberOfPointsPerRecord, IWaveformCollection<double> waveforms)
            {
                IWaveformCollection<double> ResultWaveforms = waveforms;

                if(ResultWaveforms == null)
                {
                    ResultWaveforms = new WaveformCollection<double>(numberOfRecords, numberOfPointsPerRecord);
                }

                long WaveformArraySize = numberOfPointsPerRecord * numberOfRecords * sizeof(double);
                // WaveformArray parameter is an array of values returned from the instrument that includes values for all of the waveforms being returned
                //  In .NET, these values are stored in the individual waveforms in the waveform collection.

                IntPtr pWaveformArray = Marshal.AllocHGlobal((int)WaveformArraySize);
                // ActualPoints parameter is an array of sizes, one per waveform being returned. 
 
                // ActualRecords parameter corresponds to the waveform collection ValidWaveformCount property.
                long ActualRecords = 0;               

                IntPtr pActualPoints = Marshal.AllocHGlobal((int)(numberOfRecords * sizeof(long)));
                // FirstValidPoint parameter is an array of first valid point values, one per waveform being returned.
                // In .NET, these values are represented by the FirstValidPoint property of each individual waveform. 
                
                IntPtr pFirstValidPoint = Marshal.AllocHGlobal((int)(numberOfRecords * sizeof(long)));
                // InitialXOffset parameter is an array of offsets between the trigger time and the time of the first point in a waveform, one per waveform being returned. 
                // In .NET, these values are calculated for each individual waveform by taking the difference between the waveform Start Time property and TriggerTime property.
                IntPtr pInitialXOffset = Marshal.AllocHGlobal((int)(numberOfRecords * sizeof(double)));
                
                // InitialXTimeSeconds and InitialXTimeFraction parameters are both arrays that, when corresponding values are combined, yield the start time of a wavefrom, one time (seconds + fractional seconds) per waveform being returned. 
                // In .NET, these values are represented by the StartTime property of each individual waveform.
                IntPtr pInitialXTimeSeconds = Marshal.AllocHGlobal((int)(numberOfRecords * sizeof(long)));
                IntPtr pInitialXTimeFraction = Marshal.AllocHGlobal((int)(numberOfRecords * sizeof(long)));

                // XIncrement parameter is the time span separating each measured point, and is the same for all of waveforms being returned. In .NET, this value is represented by the IntervalPerPoint property of each individual waveform. 
                // In .NET, all of the waveforms in a waveform collection shall have the same value for IntervalPerPoint. 
                double XIncrement = double.NaN;

                Adapter.ViSessionStatusCheck(IviDigitizerInterop.FetchMultiRecordWaveformReal64(Adapter.Session, ChannelName, firstRecord, numberOfRecords, offsetWithinRecord, numberOfPointsPerRecord, WaveformArraySize, pWaveformArray, ref ActualRecords, pActualPoints, pFirstValidPoint, pInitialXOffset, pInitialXTimeSeconds, pInitialXTimeFraction, ref XIncrement));

                long CurrentRecordWaveformArrayOffset = 0;
                for(int CurrentRecord = 0; CurrentRecord < ActualRecords; CurrentRecord++)
                {
                    long ActualPoints = Marshal.ReadInt64(pActualPoints, CurrentRecord * sizeof(long));
                    long FirstValidPoint = Marshal.ReadInt64(pFirstValidPoint, CurrentRecord * sizeof(long));
                    double InitialXOffset =  Convert.ToDouble(Marshal.ReadInt64(pInitialXOffset, CurrentRecord * sizeof(long)));
                    double InitialXTimeSeconds = Convert.ToDouble(Marshal.ReadInt64(pInitialXTimeSeconds, CurrentRecord * sizeof(double)));
                    double InitialXTimeFraction = Convert.ToDouble(Marshal.ReadInt64(pInitialXTimeFraction, CurrentRecord * sizeof(long)));

                    double[] CurrentRecordWaveformArray = new double[ActualPoints];
                    Marshal.Copy(pWaveformArray, CurrentRecordWaveformArray, (int)CurrentRecordWaveformArrayOffset, (int)ActualPoints);

                    IWaveform<double> CurrentWaveform = ResultWaveforms[CurrentRecord];

                    PrecisionDateTime TriggerTime = new PrecisionDateTime(DateTime.Now, InitialXOffset);
                    PrecisionTimeSpan StartTime = new PrecisionTimeSpan(InitialXTimeSeconds, InitialXTimeFraction); 
                    PrecisionTimeSpan IntervalPerPoint = new PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * XIncrement)));
                    
                    CurrentWaveform.FirstValidPoint = FirstValidPoint;
                    CurrentWaveform.Configure(StartTime, IntervalPerPoint, ActualPoints, TriggerTime);
                    CurrentWaveform.PutElements(0, CurrentRecordWaveformArray);

                    CurrentRecordWaveformArrayOffset += ActualPoints;
                }

                Marshal.FreeHGlobal(pWaveformArray);
                Marshal.FreeHGlobal(pActualPoints);
                Marshal.FreeHGlobal(pFirstValidPoint);
                Marshal.FreeHGlobal(pInitialXOffset);
                Marshal.FreeHGlobal(pInitialXTimeSeconds);
                Marshal.FreeHGlobal(pInitialXTimeFraction);

                return ResultWaveforms;
            }

            public IWaveformCollection<int> FetchMultiRecordWaveform(long firstRecord, long numberOfRecords, long offsetWithinRecord, long numberOfPointsPerRecord, IWaveformCollection<int> waveforms)
            {
                IWaveformCollection<int> ResultWaveforms = waveforms;

                if (ResultWaveforms == null)
                {
                    ResultWaveforms = new WaveformCollection<int>(numberOfRecords, numberOfPointsPerRecord);
                }

                long WaveformArraySize = numberOfPointsPerRecord * numberOfRecords * sizeof(int);
                // WaveformArray parameter is an array of values returned from the instrument that includes values for all of the waveforms being returned
                //  In .NET, these values are stored in the individual waveforms in the waveform collection.

                IntPtr pWaveformArray = Marshal.AllocHGlobal((int)WaveformArraySize);
                // ActualPoints parameter is an array of sizes, one per waveform being returned. 

                // ActualRecords parameter corresponds to the waveform collection ValidWaveformCount property.
                long ActualRecords = 0;

                IntPtr pActualPoints = Marshal.AllocHGlobal((int)(numberOfRecords * sizeof(long)));
                // FirstValidPoint parameter is an array of first valid point values, one per waveform being returned.
                // In .NET, these values are represented by the FirstValidPoint property of each individual waveform. 

                IntPtr pFirstValidPoint = Marshal.AllocHGlobal((int)(numberOfRecords * sizeof(long)));
                // InitialXOffset parameter is an array of offsets between the trigger time and the time of the first point in a waveform, one per waveform being returned. 
                // In .NET, these values are calculated for each individual waveform by taking the difference between the waveform Start Time property and TriggerTime property.
                IntPtr pInitialXOffset = Marshal.AllocHGlobal((int)(numberOfRecords * sizeof(double)));

                // InitialXTimeSeconds and InitialXTimeFraction parameters are both arrays that, when corresponding values are combined, yield the start time of a wavefrom, one time (seconds + fractional seconds) per waveform being returned. 
                // In .NET, these values are represented by the StartTime property of each individual waveform.
                IntPtr pInitialXTimeSeconds = Marshal.AllocHGlobal((int)(numberOfRecords * sizeof(long)));
                IntPtr pInitialXTimeFraction = Marshal.AllocHGlobal((int)(numberOfRecords * sizeof(long)));

                // XIncrement parameter is the time span separating each measured point, and is the same for all of waveforms being returned. In .NET, this value is represented by the IntervalPerPoint property of each individual waveform. 
                // In .NET, all of the waveforms in a waveform collection shall have the same value for IntervalPerPoint. 
                double XIncrement = double.NaN;
                double ScaleFactor = double.NaN;
                double ScaleOffset = double.NaN;

                Adapter.ViSessionStatusCheck(IviDigitizerInterop.FetchMultiRecordWaveformInt32(Adapter.Session, ChannelName, firstRecord, numberOfRecords, offsetWithinRecord, numberOfPointsPerRecord, WaveformArraySize, pWaveformArray, ref ActualRecords, pActualPoints, pFirstValidPoint, pInitialXOffset, pInitialXTimeSeconds, pInitialXTimeFraction, ref XIncrement, ref ScaleFactor, ref ScaleOffset));

                long CurrentRecordWaveformArrayOffset = 0;
                for (int CurrentRecord = 0; CurrentRecord < ActualRecords; CurrentRecord++)
                {
                    long ActualPoints = Marshal.ReadInt64(pActualPoints, CurrentRecord * sizeof(long));
                    long FirstValidPoint = Marshal.ReadInt64(pFirstValidPoint, CurrentRecord * sizeof(long));
                    double InitialXOffset = Convert.ToDouble(Marshal.ReadInt64(pInitialXOffset, CurrentRecord * sizeof(long)));
                    double InitialXTimeSeconds = Convert.ToDouble(Marshal.ReadInt64(pInitialXTimeSeconds, CurrentRecord * sizeof(double)));
                    double InitialXTimeFraction = Convert.ToDouble(Marshal.ReadInt64(pInitialXTimeFraction, CurrentRecord * sizeof(long)));

                    int[] CurrentRecordWaveformArray = new int[ActualPoints];
                    Marshal.Copy(pWaveformArray, CurrentRecordWaveformArray, (int)CurrentRecordWaveformArrayOffset, (int)ActualPoints);

                    IWaveform<int> CurrentWaveform = ResultWaveforms[CurrentRecord];

                    PrecisionDateTime TriggerTime = new PrecisionDateTime(DateTime.Now, InitialXOffset);
                    PrecisionTimeSpan StartTime = new PrecisionTimeSpan(InitialXTimeSeconds, InitialXTimeFraction);
                    PrecisionTimeSpan IntervalPerPoint = new PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * XIncrement)));

                    CurrentWaveform.FirstValidPoint = FirstValidPoint;
                    CurrentWaveform.Configure(StartTime, IntervalPerPoint, ActualPoints, TriggerTime);
                    CurrentWaveform.Scale = ScaleFactor;
                    CurrentWaveform.Offset = ScaleOffset;
                    CurrentWaveform.PutElements(0, CurrentRecordWaveformArray);

                    CurrentRecordWaveformArrayOffset += ActualPoints;
                }

                Marshal.FreeHGlobal(pWaveformArray);
                Marshal.FreeHGlobal(pActualPoints);
                Marshal.FreeHGlobal(pFirstValidPoint);
                Marshal.FreeHGlobal(pInitialXOffset);
                Marshal.FreeHGlobal(pInitialXTimeSeconds);
                Marshal.FreeHGlobal(pInitialXTimeFraction);

                return ResultWaveforms;
            }

            public IWaveformCollection<short> FetchMultiRecordWaveform(long firstRecord, long numberOfRecords, long offsetWithinRecord, long numberOfPointsPerRecord, IWaveformCollection<short> waveforms)
            {
                IWaveformCollection<short> ResultWaveforms = waveforms;

                if (ResultWaveforms == null)
                {
                    ResultWaveforms = new WaveformCollection<short>(numberOfRecords, numberOfPointsPerRecord);
                }

                long WaveformArraySize = numberOfPointsPerRecord * numberOfRecords * sizeof(short);
                // WaveformArray parameter is an array of values returned from the instrument that includes values for all of the waveforms being returned
                //  In .NET, these values are stored in the individual waveforms in the waveform collection.

                IntPtr pWaveformArray = Marshal.AllocHGlobal((int)WaveformArraySize);
                // ActualPoints parameter is an array of sizes, one per waveform being returned. 

                // ActualRecords parameter corresponds to the waveform collection ValidWaveformCount property.
                long ActualRecords = 0;

                IntPtr pActualPoints = Marshal.AllocHGlobal((int)(numberOfRecords * sizeof(long)));
                // FirstValidPoint parameter is an array of first valid point values, one per waveform being returned.
                // In .NET, these values are represented by the FirstValidPoint property of each individual waveform. 

                IntPtr pFirstValidPoint = Marshal.AllocHGlobal((int)(numberOfRecords * sizeof(long)));
                // InitialXOffset parameter is an array of offsets between the trigger time and the time of the first point in a waveform, one per waveform being returned. 
                // In .NET, these values are calculated for each individual waveform by taking the difference between the waveform Start Time property and TriggerTime property.
                IntPtr pInitialXOffset = Marshal.AllocHGlobal((int)(numberOfRecords * sizeof(double)));

                // InitialXTimeSeconds and InitialXTimeFraction parameters are both arrays that, when corresponding values are combined, yield the start time of a wavefrom, one time (seconds + fractional seconds) per waveform being returned. 
                // In .NET, these values are represented by the StartTime property of each individual waveform.
                IntPtr pInitialXTimeSeconds = Marshal.AllocHGlobal((int)(numberOfRecords * sizeof(long)));
                IntPtr pInitialXTimeFraction = Marshal.AllocHGlobal((int)(numberOfRecords * sizeof(long)));

                // XIncrement parameter is the time span separating each measured point, and is the same for all of waveforms being returned. In .NET, this value is represented by the IntervalPerPoint property of each individual waveform. 
                // In .NET, all of the waveforms in a waveform collection shall have the same value for IntervalPerPoint. 
                double XIncrement = double.NaN;
                double ScaleFactor = double.NaN;
                double ScaleOffset = double.NaN;

                Adapter.ViSessionStatusCheck(IviDigitizerInterop.FetchMultiRecordWaveformInt32(Adapter.Session, ChannelName, firstRecord, numberOfRecords, offsetWithinRecord, numberOfPointsPerRecord, WaveformArraySize, pWaveformArray, ref ActualRecords, pActualPoints, pFirstValidPoint, pInitialXOffset, pInitialXTimeSeconds, pInitialXTimeFraction, ref XIncrement, ref ScaleFactor, ref ScaleOffset));

                long CurrentRecordWaveformArrayOffset = 0;
                for (int CurrentRecord = 0; CurrentRecord < ActualRecords; CurrentRecord++)
                {
                    long ActualPoints = Marshal.ReadInt64(pActualPoints, CurrentRecord * sizeof(long));
                    long FirstValidPoint = Marshal.ReadInt64(pFirstValidPoint, CurrentRecord * sizeof(long));
                    double InitialXOffset = Convert.ToDouble(Marshal.ReadInt64(pInitialXOffset, CurrentRecord * sizeof(long)));
                    double InitialXTimeSeconds = Convert.ToDouble(Marshal.ReadInt64(pInitialXTimeSeconds, CurrentRecord * sizeof(double)));
                    double InitialXTimeFraction = Convert.ToDouble(Marshal.ReadInt64(pInitialXTimeFraction, CurrentRecord * sizeof(long)));

                    short[] CurrentRecordWaveformArray = new short[ActualPoints];
                    Marshal.Copy(pWaveformArray, CurrentRecordWaveformArray, (int)CurrentRecordWaveformArrayOffset, (int)ActualPoints);

                    IWaveform<short> CurrentWaveform = ResultWaveforms[CurrentRecord];

                    PrecisionDateTime TriggerTime = new PrecisionDateTime(DateTime.Now, InitialXOffset);
                    PrecisionTimeSpan StartTime = new PrecisionTimeSpan(InitialXTimeSeconds, InitialXTimeFraction);
                    PrecisionTimeSpan IntervalPerPoint = new PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * XIncrement)));

                    CurrentWaveform.FirstValidPoint = FirstValidPoint;
                    CurrentWaveform.Configure(StartTime, IntervalPerPoint, ActualPoints, TriggerTime);
                    CurrentWaveform.Scale = ScaleFactor;
                    CurrentWaveform.Offset = ScaleOffset;
                    CurrentWaveform.PutElements(0, CurrentRecordWaveformArray);

                    CurrentRecordWaveformArrayOffset += ActualPoints;
                }

                Marshal.FreeHGlobal(pWaveformArray);
                Marshal.FreeHGlobal(pActualPoints);
                Marshal.FreeHGlobal(pFirstValidPoint);
                Marshal.FreeHGlobal(pInitialXOffset);
                Marshal.FreeHGlobal(pInitialXTimeSeconds);
                Marshal.FreeHGlobal(pInitialXTimeFraction);

                return ResultWaveforms;
            }

            public IWaveformCollection<sbyte> FetchMultiRecordWaveform(long firstRecord, long numberOfRecords, long offsetWithinRecord, long numberOfPointsPerRecord, IWaveformCollection<sbyte> waveforms)
            {
                IWaveformCollection<sbyte> ResultWaveforms = waveforms;

                if (ResultWaveforms == null)
                {
                    ResultWaveforms = new WaveformCollection<sbyte>(numberOfRecords, numberOfPointsPerRecord);
                }

                long WaveformArraySize = numberOfPointsPerRecord * numberOfRecords * sizeof(sbyte);
                // WaveformArray parameter is an array of values returned from the instrument that includes values for all of the waveforms being returned
                //  In .NET, these values are stored in the individual waveforms in the waveform collection.

                IntPtr pWaveformArray = Marshal.AllocHGlobal((int)WaveformArraySize);
                // ActualPoints parameter is an array of sizes, one per waveform being returned. 

                // ActualRecords parameter corresponds to the waveform collection ValidWaveformCount property.
                long ActualRecords = 0;

                IntPtr pActualPoints = Marshal.AllocHGlobal((int)(numberOfRecords * sizeof(long)));
                // FirstValidPoint parameter is an array of first valid point values, one per waveform being returned.
                // In .NET, these values are represented by the FirstValidPoint property of each individual waveform. 

                IntPtr pFirstValidPoint = Marshal.AllocHGlobal((int)(numberOfRecords * sizeof(long)));
                // InitialXOffset parameter is an array of offsets between the trigger time and the time of the first point in a waveform, one per waveform being returned. 
                // In .NET, these values are calculated for each individual waveform by taking the difference between the waveform Start Time property and TriggerTime property.
                IntPtr pInitialXOffset = Marshal.AllocHGlobal((int)(numberOfRecords * sizeof(double)));

                // InitialXTimeSeconds and InitialXTimeFraction parameters are both arrays that, when corresponding values are combined, yield the start time of a wavefrom, one time (seconds + fractional seconds) per waveform being returned. 
                // In .NET, these values are represented by the StartTime property of each individual waveform.
                IntPtr pInitialXTimeSeconds = Marshal.AllocHGlobal((int)(numberOfRecords * sizeof(long)));
                IntPtr pInitialXTimeFraction = Marshal.AllocHGlobal((int)(numberOfRecords * sizeof(long)));

                // XIncrement parameter is the time span separating each measured point, and is the same for all of waveforms being returned. In .NET, this value is represented by the IntervalPerPoint property of each individual waveform. 
                // In .NET, all of the waveforms in a waveform collection shall have the same value for IntervalPerPoint. 
                double XIncrement = double.NaN;
                double ScaleFactor = double.NaN;
                double ScaleOffset = double.NaN;

                Adapter.ViSessionStatusCheck(IviDigitizerInterop.FetchMultiRecordWaveformInt32(Adapter.Session, ChannelName, firstRecord, numberOfRecords, offsetWithinRecord, numberOfPointsPerRecord, WaveformArraySize, pWaveformArray, ref ActualRecords, pActualPoints, pFirstValidPoint, pInitialXOffset, pInitialXTimeSeconds, pInitialXTimeFraction, ref XIncrement, ref ScaleFactor, ref ScaleOffset));

                long CurrentRecordWaveformArrayOffset = 0;
                for (int CurrentRecord = 0; CurrentRecord < ActualRecords; CurrentRecord++)
                {
                    long ActualPoints = Marshal.ReadInt64(pActualPoints, CurrentRecord * sizeof(long));
                    long FirstValidPoint = Marshal.ReadInt64(pFirstValidPoint, CurrentRecord * sizeof(long));
                    double InitialXOffset = Convert.ToDouble(Marshal.ReadInt64(pInitialXOffset, CurrentRecord * sizeof(long)));
                    double InitialXTimeSeconds = Convert.ToDouble(Marshal.ReadInt64(pInitialXTimeSeconds, CurrentRecord * sizeof(double)));
                    double InitialXTimeFraction = Convert.ToDouble(Marshal.ReadInt64(pInitialXTimeFraction, CurrentRecord * sizeof(long)));

                    byte[] CurrentRecordWaveformArray = new byte[ActualPoints];
                    Marshal.Copy(pWaveformArray, CurrentRecordWaveformArray, (int)CurrentRecordWaveformArrayOffset, (int)ActualPoints);

                    IWaveform<sbyte> CurrentWaveform = ResultWaveforms[CurrentRecord];

                    PrecisionDateTime TriggerTime = new PrecisionDateTime(DateTime.Now, InitialXOffset);
                    PrecisionTimeSpan StartTime = new PrecisionTimeSpan(InitialXTimeSeconds, InitialXTimeFraction);
                    PrecisionTimeSpan IntervalPerPoint = new PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * XIncrement)));

                    CurrentWaveform.FirstValidPoint = FirstValidPoint;
                    CurrentWaveform.Configure(StartTime, IntervalPerPoint, ActualPoints, TriggerTime);
                    CurrentWaveform.Scale = ScaleFactor;
                    CurrentWaveform.Offset = ScaleOffset;
                    CurrentWaveform.PutElements(0, Array.ConvertAll<byte, sbyte>(CurrentRecordWaveformArray, new Converter<byte, sbyte>(delegate(byte data)
                    {
                        return (sbyte)data;
                    })));

                    CurrentRecordWaveformArrayOffset += ActualPoints;
                }

                Marshal.FreeHGlobal(pWaveformArray);
                Marshal.FreeHGlobal(pActualPoints);
                Marshal.FreeHGlobal(pFirstValidPoint);
                Marshal.FreeHGlobal(pInitialXOffset);
                Marshal.FreeHGlobal(pInitialXTimeSeconds);
                Marshal.FreeHGlobal(pInitialXTimeFraction);

                return ResultWaveforms;
            }
        }

        #endregion

        internal class IviDigitizerReferenceOscillator : IIviDigitizerReferenceOscillator
        {
            private IDriverAdapterBase Adapter;
            private IviCInterop.IviDigitizer IviDigitizerInterop = null;
            public IviDigitizerReferenceOscillator(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviDigitizerInterop = (IviCInterop.IviDigitizer)Adapter.Interop;
            }

            public void Configure(ReferenceOscillatorSource source, double frequency)
            {
                Adapter.ViSessionStatusCheck(IviDigitizerInterop.ConfigureReferenceOscillator(Adapter.Session, DigitizerReferenceOscillatorSource.getC_Value(source), frequency));
            }

            public double ExternalFrequency
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviDigitizerAttribute.IVIDIGITIZER_ATTR_REFERENCE_OSCILLATOR_EXTERNAL_FREQUENCY);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviDigitizerAttribute.IVIDIGITIZER_ATTR_REFERENCE_OSCILLATOR_EXTERNAL_FREQUENCY, value);
                }
            }

            public bool OutputEnabled
            {
                get
                {
                    return Adapter.GetAttributeViBoolean(IviDigitizerAttribute.IVIDIGITIZER_ATTR_REFERENCE_OSCILLATOR_OUTPUT_ENABLED);
                }
                set
                {
                    Adapter.SetAttributeViBoolean(IviDigitizerAttribute.IVIDIGITIZER_ATTR_REFERENCE_OSCILLATOR_OUTPUT_ENABLED, value);
                }
            }

            public ReferenceOscillatorSource Source
            {
                get
                {
                    return DigitizerReferenceOscillatorSource.getEnum(Adapter.GetAttributeViInt32(IviDigitizerAttribute.IVIDIGITIZER_ATTR_REFERENCE_OSCILLATOR_SOURCE));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviDigitizerAttribute.IVIDIGITIZER_ATTR_REFERENCE_OSCILLATOR_SOURCE, DigitizerReferenceOscillatorSource.getC_Value(value));
                }
            }
        }

        internal class IviDigitizerSampleClock : IIviDigitizerSampleClock
        {
            private IDriverAdapterBase Adapter;
            private IviCInterop.IviDigitizer IviDigitizerInterop = null;
            public IviDigitizerSampleClock(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviDigitizerInterop = (IviCInterop.IviDigitizer)Adapter.Interop;
            }

            public void Configure(SampleClockSource source, double frequency, double divider)
            {
                Adapter.ViSessionStatusCheck(IviDigitizerInterop.ConfigureSampleClock(Adapter.Session, DigitizerSampleClockSource.getC_Value(source), frequency, divider));
            }

            public double ExternalDivider
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviDigitizerAttribute.IVIDIGITIZER_ATTR_SAMPLE_CLOCK_EXTERNAL_DIVIDER);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviDigitizerAttribute.IVIDIGITIZER_ATTR_SAMPLE_CLOCK_EXTERNAL_DIVIDER, value);
                }
            }

            public double ExternalFrequency
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviDigitizerAttribute.IVIDIGITIZER_ATTR_SAMPLE_CLOCK_EXTERNAL_FREQUENCY);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviDigitizerAttribute.IVIDIGITIZER_ATTR_SAMPLE_CLOCK_EXTERNAL_FREQUENCY, value);
                }
            }

            public bool OutputEnabled
            {
                get
                {
                    return Adapter.GetAttributeViBoolean(IviDigitizerAttribute.IVIDIGITIZER_ATTR_SAMPLE_CLOCK_OUTPUT_ENABLED);
                }
                set
                {
                    Adapter.SetAttributeViBoolean(IviDigitizerAttribute.IVIDIGITIZER_ATTR_SAMPLE_CLOCK_OUTPUT_ENABLED, value);
                }
            }

            public SampleClockSource Source
            {
                get
                {
                    return DigitizerSampleClockSource.getEnum(Adapter.GetAttributeViInt32(IviDigitizerAttribute.IVIDIGITIZER_ATTR_SAMPLE_CLOCK_SOURCE));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviDigitizerAttribute.IVIDIGITIZER_ATTR_SAMPLE_CLOCK_SOURCE, DigitizerSampleClockSource.getC_Value(value));
                }
            }
        }

        internal class IviDigitizerTemperature : IIviDigitizerTemperature
        {
            private IDriverAdapterBase Adapter;
            private IviCInterop.IviDigitizer IviDigitizerInterop = null;
            public IviDigitizerTemperature(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviDigitizerInterop = (IviCInterop.IviDigitizer)Adapter.Interop;
            }

            public double BoardTemperature
            {
                get { return Adapter.GetAttributeViReal64(IviDigitizerAttribute.IVIDIGITIZER_ATTR_BOARD_TEMPERATURE);}
            }

            public TemperatureUnits Units
            {
                get
                {
                    return DigitizerTemperatureUnits.getEnum(Adapter.GetAttributeViInt32(IviDigitizerAttribute.IVIDIGITIZER_ATTR_TEMPERATURE_UNITS));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviDigitizerAttribute.IVIDIGITIZER_ATTR_TEMPERATURE_UNITS, DigitizerTemperatureUnits.getC_Value(value));
                }
            }
        }

        #region Digitizer Trigger

        internal class IviDigitizerTrigger : IIviDigitizerTrigger
        {
            private IDriverAdapterBase Adapter;
            private IviCInterop.IviDigitizer IviDigitizerInterop = null;
            private IIviDigitizerMultiTrigger DigitizerMultiTrigger = null;
            public IviDigitizerTrigger(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviDigitizerInterop = (IviCInterop.IviDigitizer)Adapter.Interop;
                DigitizerMultiTrigger = new IviDigitizerMultiTrigger(Adapter);
            }

            public string ActiveSource
            {
                get
                {
                    return Adapter.GetAttributeViString(IviDigitizerAttribute.IVIDIGITIZER_ATTR_ACTIVE_TRIGGER_SOURCE);
                }
                set
                {
                    Adapter.SetAttributeViString(IviDigitizerAttribute.IVIDIGITIZER_ATTR_ACTIVE_TRIGGER_SOURCE, value);
                }
            }

            public PrecisionTimeSpan Delay
            {
                get
                {
                    return new PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * Adapter.GetAttributeViReal64(IviDigitizerAttribute.IVIDIGITIZER_ATTR_TRIGGER_DELAY))));
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviDigitizerAttribute.IVIDIGITIZER_ATTR_TRIGGER_DELAY, value.TotalSeconds);
                }
            }

            public PrecisionTimeSpan Holdoff
            {
                get
                {
                    return new PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * Adapter.GetAttributeViReal64(IviDigitizerAttribute.IVIDIGITIZER_ATTR_TRIGGER_HOLDOFF))));
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviDigitizerAttribute.IVIDIGITIZER_ATTR_TRIGGER_HOLDOFF, value.TotalSeconds);
                }
            }

            public TriggerModifier Modifier
            {
                get
                {
                    return DigitizerTriggerModifier.getEnum(Adapter.GetAttributeViInt32(IviDigitizerAttribute.IVIDIGITIZER_ATTR_TRIGGER_MODIFIER));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviDigitizerAttribute.IVIDIGITIZER_ATTR_TRIGGER_MODIFIER, DigitizerTriggerModifier.getC_Value(value));
                }
            }

            public bool OutputEnabled
            {
                get
                {
                    return Adapter.GetAttributeViBoolean(IviDigitizerAttribute.IVIDIGITIZER_ATTR_TRIGGER_OUTPUT_ENABLED);
                }
                set
                {
                    Adapter.SetAttributeViBoolean(IviDigitizerAttribute.IVIDIGITIZER_ATTR_TRIGGER_OUTPUT_ENABLED, value);
                }
            }

            public long PretriggerSamples
            {
                get
                {
                    return Adapter.GetAttributeViInt64(IviDigitizerAttribute.IVIDIGITIZER_ATTR_PRETRIGGER_SAMPLES);
                }
                set
                {
                    Adapter.SetAttributeViInt64(IviDigitizerAttribute.IVIDIGITIZER_ATTR_PRETRIGGER_SAMPLES, value);
                }
            }

            public void SendSoftwareTrigger()
            {
                Adapter.ViSessionStatusCheck(IviDigitizerInterop.SendSoftwareTrigger(Adapter.Session));
            }

            public IIviDigitizerMultiTrigger MultiTrigger
            {
                get { return DigitizerMultiTrigger; }
            }

            public IIviDigitizerTriggerSourceCollection Sources
            {
                get { throw new NotImplementedException(); }
            }
        }

        internal class IviDigitizerMultiTrigger : IIviDigitizerMultiTrigger
        {
            private IDriverAdapterBase Adapter;
            private IviCInterop.IviDigitizer IviDigitizerInterop = null;
            public IviDigitizerMultiTrigger(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviDigitizerInterop = (IviCInterop.IviDigitizer)Adapter.Interop;
            }

            public void Configure(string sourceList, TriggerSourceOperator sourceOperator)
            {
                Adapter.ViSessionStatusCheck(IviDigitizerInterop.ConfigureMultiTrigger(Adapter.Session, sourceList, DigitizerTriggerSourceOperator.getC_Value(sourceOperator)));
            }

            public string SourceList
            {
                get
                {
                    return Adapter.GetAttributeViString(IviDigitizerAttribute.IVIDIGITIZER_ATTR_TRIGGER_SOURCE_LIST);
                }
                set
                {
                    Adapter.SetAttributeViString(IviDigitizerAttribute.IVIDIGITIZER_ATTR_TRIGGER_SOURCE_LIST, value);
                }
            }

            public TriggerSourceOperator SourceOperator
            {
                get
                {
                    return DigitizerTriggerSourceOperator.getEnum(Adapter.GetAttributeViInt32(IviDigitizerAttribute.IVIDIGITIZER_ATTR_TRIGGER_SOURCE_OPERATOR));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviDigitizerAttribute.IVIDIGITIZER_ATTR_TRIGGER_SOURCE_OPERATOR, DigitizerTriggerSourceOperator.getC_Value(value));
                }
            }
        }

        internal class IviDigitizerTriggerSourceCollection : IIviDigitizerTriggerSourceCollection
        {
            private IDriverAdapterBase Adapter;
            private IviCInterop.IviDigitizer IviDigitizerInterop = null;
            private IList<IIviDigitizerTriggerSource> TriggerSources = null;
            private IList<string> TriggerSourceNames = null;
            public IviDigitizerTriggerSourceCollection(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviDigitizerInterop = (IviCInterop.IviDigitizer)Adapter.Interop;

                int OutputCount = Adapter.GetAttributeViInt32(IviDigitizerAttribute.IVIDIGITIZER_ATTR_CHANNEL_COUNT);

                TriggerSources = new List<IIviDigitizerTriggerSource>();
                TriggerSourceNames = new List<string>();
                for (int Index = 1; Index <= OutputCount; Index++)
                {
                    IIviDigitizerTriggerSource TriggerSource = new IviDigitizerTriggerSource(Adapter, Index);
                    TriggerSources.Add(TriggerSource);
                    TriggerSourceNames.Add(TriggerSource.Name);
                }
            }

            public int Count
            {
                get { return TriggerSources.Count; }
            }

            public IIviDigitizerTriggerSource this[string name]
            {
                get { return TriggerSources[TriggerSourceNames.IndexOf(name)]; }
            }

            public IEnumerator<IIviDigitizerTriggerSource> GetEnumerator()
            {
                return TriggerSources.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return TriggerSources.GetEnumerator();
            }
        }

        internal class IviDigitizerTriggerSource : IIviDigitizerTriggerSource
        {
            private IDriverAdapterBase Adapter;
            private IviCInterop.IviDigitizer IviDigitizerInterop = null;
            private string TriggerSourceName;
            private IIviDigitizerTriggerEdge DigitizerTriggerEdge = null;
            private IIviDigitizerTriggerGlitch DigitizerTriggerGlitch = null;
            private IIviDigitizerTriggerRunt DigitizerTriggerRunt = null;
            private IIviDigitizerTriggerTV DigitizerTriggerTV = null;
            private IIviDigitizerTriggerWidth DigitizerTriggerWidth = null;
            private IIviDigitizerTriggerWindow DigitizerTriggerWindow = null;
            public IviDigitizerTriggerSource(IDriverAdapterBase Adapter, int Index)
            {
                this.Adapter = Adapter;
                IviDigitizerInterop = (IviCInterop.IviDigitizer)Adapter.Interop;

                try
                {
                    StringBuilder NameValue = new StringBuilder(256);
                    Adapter.ViSessionStatusCheck(IviDigitizerInterop.GetTriggerSourceName(Adapter.Session, Index, NameValue.Capacity, NameValue));
                    TriggerSourceName = NameValue.ToString();
                }
                catch
                {
                    TriggerSourceName = string.Empty;
                }

                DigitizerTriggerEdge = new IviDigitizerTriggerEdge(Adapter, TriggerSourceName);
                DigitizerTriggerGlitch = new IviDigitizerTriggerGlitch(Adapter, TriggerSourceName);
                DigitizerTriggerRunt = new IviDigitizerTriggerRunt(Adapter, TriggerSourceName);
                DigitizerTriggerTV = new IviDigitizerTriggerTV(Adapter, TriggerSourceName);
                DigitizerTriggerWidth = new IviDigitizerTriggerWidth(Adapter, TriggerSourceName);
                DigitizerTriggerWindow = new IviDigitizerTriggerWindow(Adapter, TriggerSourceName);
            }

            public TriggerType Type
            {
                get
                {
                    return DigitizerTriggerType.getEnum(Adapter.GetAttributeViInt32(TriggerSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_TRIGGER_TYPE));
                }
                set
                {
                    Adapter.SetAttributeViInt32(TriggerSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_TRIGGER_TYPE, DigitizerTriggerType.getC_Value(value));
                }
            }

            public TriggerCoupling Coupling
            {
                get
                {
                    return DigitizerTriggerCoupling.getEnum(Adapter.GetAttributeViInt32(TriggerSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_TRIGGER_COUPLING));
                }
                set
                {
                    Adapter.SetAttributeViInt32(TriggerSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_TRIGGER_COUPLING, DigitizerTriggerCoupling.getC_Value(value));
                }
            }

            public double Hysteresis
            {
                get { return Adapter.GetAttributeViReal64(TriggerSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_TRIGGER_HYSTERESIS); }
                set { Adapter.SetAttributeViReal64(TriggerSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_TRIGGER_HYSTERESIS, value); }
            }

            public double Level
            {
                get { return Adapter.GetAttributeViReal64(TriggerSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_TRIGGER_LEVEL); }
                set { Adapter.SetAttributeViReal64(TriggerSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_TRIGGER_LEVEL, value); }
            }

            public IIviDigitizerTriggerEdge Edge
            {
                get { return DigitizerTriggerEdge; }
            }

            public IIviDigitizerTriggerGlitch Glitch
            {
                get { return DigitizerTriggerGlitch; }
            }

            public IIviDigitizerTriggerRunt Runt
            {
                get { return DigitizerTriggerRunt; }
            }

            public IIviDigitizerTriggerTV TV
            {
                get { return DigitizerTriggerTV; }
            }

            public IIviDigitizerTriggerWidth Width
            {
                get { return DigitizerTriggerWidth; }
            }

            public IIviDigitizerTriggerWindow Window
            {
                get { return DigitizerTriggerWindow; }
            }

            public string Name
            {
                get { return TriggerSourceName; }
            }
        }

        internal class IviDigitizerTriggerEdge : IIviDigitizerTriggerEdge
        {
            private IDriverAdapterBase Adapter;
            private IviCInterop.IviDigitizer IviDigitizerInterop = null;
            private string TriggerSourceName;
            public IviDigitizerTriggerEdge(IDriverAdapterBase Adapter, string TriggerSourceName)
            {
                this.Adapter = Adapter;
                IviDigitizerInterop = (IviCInterop.IviDigitizer)Adapter.Interop;
                this.TriggerSourceName = TriggerSourceName;
            }

            public void Configure(double level, Slope slope)
            {
                Adapter.ViSessionStatusCheck(IviDigitizerInterop.ConfigureEdgeTriggerSource(Adapter.Session, TriggerSourceName, level, DigitizerSlope.getC_Value(slope)));
            }

            public Slope Slope
            {
                get
                {
                    return DigitizerSlope.getEnum(Adapter.GetAttributeViInt32(TriggerSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_TRIGGER_SLOPE));
                }
                set
                {
                    Adapter.SetAttributeViInt32(TriggerSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_TRIGGER_SLOPE, DigitizerSlope.getC_Value(value));
                }
            }
        }

        internal class IviDigitizerTriggerGlitch : IIviDigitizerTriggerGlitch
        {
            private IDriverAdapterBase Adapter;
            private IviCInterop.IviDigitizer IviDigitizerInterop = null;
            private string TriggerSourceName;
            public IviDigitizerTriggerGlitch(IDriverAdapterBase Adapter, string TriggerSourceName)
            {
                this.Adapter = Adapter;
                IviDigitizerInterop = (IviCInterop.IviDigitizer)Adapter.Interop;
                this.TriggerSourceName = TriggerSourceName;
            }

            public void Configure(double level, PrecisionTimeSpan width, GlitchPolarity polarity, GlitchCondition condition)
            {
                Adapter.ViSessionStatusCheck(IviDigitizerInterop.ConfigureGlitchTriggerSource(Adapter.Session, TriggerSourceName, level, width.TotalSeconds, DigitizerGlitchPolarity.getC_Value(polarity), DigitizerGlitchCondition.getC_Value(condition)));
            }

            public GlitchCondition Condition
            {
                get { return DigitizerGlitchCondition.getEnum(Adapter.GetAttributeViInt32(TriggerSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_GLITCH_TRIGGER_CONDITION)); }
                set { Adapter.SetAttributeViInt32(TriggerSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_GLITCH_TRIGGER_CONDITION, DigitizerGlitchCondition.getC_Value(value)); }
            }

            public GlitchPolarity Polarity
            {
                get { return DigitizerGlitchPolarity.getEnum(Adapter.GetAttributeViInt32(TriggerSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_GLITCH_TRIGGER_POLARITY)); }
                set { Adapter.SetAttributeViInt32(TriggerSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_GLITCH_TRIGGER_POLARITY, DigitizerGlitchPolarity.getC_Value(value)); }
            }

            public PrecisionTimeSpan Width
            {
                get
                {
                    return new Ivi.Driver.PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * Adapter.GetAttributeViReal64(TriggerSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_GLITCH_TRIGGER_WIDTH))));
                }
                set
                {
                    Adapter.SetAttributeViReal64(TriggerSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_GLITCH_TRIGGER_WIDTH, value.TotalSeconds);
                }
            }
        }

        internal class IviDigitizerTriggerRunt : IIviDigitizerTriggerRunt
        {
            private IDriverAdapterBase Adapter;
            private IviCInterop.IviDigitizer IviDigitizerInterop = null;
            private string TriggerSourceName;
            public IviDigitizerTriggerRunt(IDriverAdapterBase Adapter, string TriggerSourceName)
            {
                this.Adapter = Adapter;
                IviDigitizerInterop = (IviCInterop.IviDigitizer)Adapter.Interop;
                this.TriggerSourceName = TriggerSourceName;
            }

            public void Configure(double thresholdLow, double thresholdHigh, RuntPolarity polarity)
            {
                Adapter.ViSessionStatusCheck(IviDigitizerInterop.ConfigureRuntTriggerSource(Adapter.Session, TriggerSourceName, thresholdLow, thresholdHigh, DigitizerRuntPolarity.getC_Value(polarity)));
            }

            public RuntPolarity Polarity
            {
                get { return DigitizerRuntPolarity.getEnum(Adapter.GetAttributeViInt32(TriggerSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_RUNT_TRIGGER_POLARITY)); }
                set { Adapter.SetAttributeViInt32(TriggerSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_RUNT_TRIGGER_POLARITY, DigitizerRuntPolarity.getC_Value(value)); }
            }

            public double ThresholdHigh
            {
                get { return Adapter.GetAttributeViReal64(TriggerSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_RUNT_TRIGGER_HIGH_THRESHOLD); }
                set { Adapter.SetAttributeViReal64(TriggerSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_RUNT_TRIGGER_HIGH_THRESHOLD, value); }
            }

            public double ThresholdLow
            {
                get { return Adapter.GetAttributeViReal64(TriggerSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_RUNT_TRIGGER_LOW_THRESHOLD); }
                set { Adapter.SetAttributeViReal64(TriggerSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_RUNT_TRIGGER_LOW_THRESHOLD, value); }
            }
        }

        internal class IviDigitizerTriggerTV : IIviDigitizerTriggerTV
        {
            private IDriverAdapterBase Adapter;
            private IviCInterop.IviDigitizer IviDigitizerInterop = null;
            private string TriggerSourceName;
            public IviDigitizerTriggerTV(IDriverAdapterBase Adapter, string TriggerSourceName)
            {
                this.Adapter = Adapter;
                IviDigitizerInterop = (IviCInterop.IviDigitizer)Adapter.Interop;
                this.TriggerSourceName = TriggerSourceName;
            }

            public void Configure(TVSignalFormat signalFormat, TVTriggerEvent triggerEvent, TVTriggerPolarity polarity)
            {
                Adapter.ViSessionStatusCheck(IviDigitizerInterop.ConfigureTVTriggerSource(Adapter.Session, TriggerSourceName, DigitizerTVSignalFormat.getC_Value(signalFormat), DigitizerTVTriggerEvent.getC_Value(triggerEvent), DigitizerTVTriggerPolarity.getC_Value(polarity)));
            }

            public int LineNumber
            {
                get { return Adapter.GetAttributeViInt32(TriggerSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_TV_TRIGGER_LINE_NUMBER); }
                set { Adapter.SetAttributeViInt32(TriggerSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_TV_TRIGGER_LINE_NUMBER, value); }
            }

            public TVTriggerPolarity Polarity
            {
                get { return DigitizerTVTriggerPolarity.getEnum(Adapter.GetAttributeViInt32(TriggerSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_TV_TRIGGER_POLARITY)); }
                set { Adapter.SetAttributeViInt32(TriggerSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_TV_TRIGGER_POLARITY, DigitizerTVTriggerPolarity.getC_Value(value)); }
            }

            public TVSignalFormat SignalFormat
            {
                get { return DigitizerTVSignalFormat.getEnum(Adapter.GetAttributeViInt32(TriggerSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_TV_TRIGGER_SIGNAL_FORMAT)); }
                set { Adapter.SetAttributeViInt32(TriggerSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_TV_TRIGGER_SIGNAL_FORMAT, DigitizerTVSignalFormat.getC_Value(value)); }
            }

            public TVTriggerEvent TriggerEvent
            {
                get { return DigitizerTVTriggerEvent.getEnum(Adapter.GetAttributeViInt32(TriggerSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_TV_TRIGGER_EVENT)); }
                set { Adapter.SetAttributeViInt32(TriggerSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_TV_TRIGGER_EVENT, DigitizerTVTriggerEvent.getC_Value(value)); }
            }
        }

        internal class IviDigitizerTriggerWidth : IIviDigitizerTriggerWidth
        {
            private IDriverAdapterBase Adapter;
            private IviCInterop.IviDigitizer IviDigitizerInterop = null;
            private string TriggerSourceName;
            public IviDigitizerTriggerWidth(IDriverAdapterBase Adapter, string TriggerSourceName)
            {
                this.Adapter = Adapter;
                IviDigitizerInterop = (IviCInterop.IviDigitizer)Adapter.Interop;
                this.TriggerSourceName = TriggerSourceName;
            }

            public void Configure(double level, PrecisionTimeSpan thresholdLow, PrecisionTimeSpan thresholdHigh, WidthPolarity polarity, WidthCondition condition)
            {
                Adapter.ViSessionStatusCheck(IviDigitizerInterop.ConfigureWidthTriggerSource(Adapter.Session, TriggerSourceName, level, thresholdLow.TotalSeconds, thresholdHigh.TotalSeconds, DigitizerWidthPolarity.getC_Value(polarity), DigitizerWidthCondition.getC_Value(condition)));
            }

            public WidthCondition Condition
            {
                get { return DigitizerWidthCondition.getEnum(Adapter.GetAttributeViInt32(TriggerSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_WIDTH_TRIGGER_CONDITION)); }
                set { Adapter.SetAttributeViInt32(TriggerSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_WIDTH_TRIGGER_CONDITION, DigitizerWidthCondition.getC_Value(value)); }
            }

            public WidthPolarity Polarity
            {
                get { return DigitizerWidthPolarity.getEnum(Adapter.GetAttributeViInt32(TriggerSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_WIDTH_TRIGGER_POLARITY)); }
                set { Adapter.SetAttributeViInt32(TriggerSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_WIDTH_TRIGGER_POLARITY, DigitizerWidthPolarity.getC_Value(value)); }
            }

            public PrecisionTimeSpan ThresholdHigh
            {
                get
                {
                    return new Ivi.Driver.PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * Adapter.GetAttributeViReal64(TriggerSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_WIDTH_TRIGGER_HIGH_THRESHOLD))));
                }
                set
                {
                    Adapter.SetAttributeViReal64(TriggerSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_WIDTH_TRIGGER_HIGH_THRESHOLD, value.TotalSeconds);
                }
            }

            public PrecisionTimeSpan ThresholdLow
            {
                get
                {
                    return new Ivi.Driver.PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * Adapter.GetAttributeViReal64(TriggerSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_WIDTH_TRIGGER_LOW_THRESHOLD))));
                }
                set
                {
                    Adapter.SetAttributeViReal64(TriggerSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_WIDTH_TRIGGER_LOW_THRESHOLD, value.TotalSeconds);
                }
            }
        }

        internal class IviDigitizerTriggerWindow : IIviDigitizerTriggerWindow
        {
            private IDriverAdapterBase Adapter;
            private IviCInterop.IviDigitizer IviDigitizerInterop = null;
            private string TriggerSourceName;
            public IviDigitizerTriggerWindow(IDriverAdapterBase Adapter, string TriggerSourceName)
            {
                this.Adapter = Adapter;
                IviDigitizerInterop = (IviCInterop.IviDigitizer)Adapter.Interop;
                this.TriggerSourceName = TriggerSourceName;
            }

            public void Configure(double thresholdLow, double thresholdHigh, WindowCondition condition)
            {
                throw new NotImplementedException();
            }

            public WindowCondition Condition
            {
                get { return DigitizerWindowCondition.getEnum(Adapter.GetAttributeViInt32(TriggerSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_WINDOW_TRIGGER_CONDITION)); }
                set { Adapter.SetAttributeViInt32(TriggerSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_WINDOW_TRIGGER_CONDITION, DigitizerWindowCondition.getC_Value(value)); }
            }

            public double ThresholdHigh
            {
                get { return Adapter.GetAttributeViReal64(TriggerSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_WINDOW_TRIGGER_HIGH_THRESHOLD); }
                set { Adapter.SetAttributeViReal64(TriggerSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_WINDOW_TRIGGER_HIGH_THRESHOLD, value); }
            }

            public double ThresholdLow
            {
                get { return Adapter.GetAttributeViReal64(TriggerSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_WINDOW_TRIGGER_LOW_THRESHOLD); }
                set { Adapter.SetAttributeViReal64(TriggerSourceName, IviDigitizerAttribute.IVIDIGITIZER_ATTR_WINDOW_TRIGGER_LOW_THRESHOLD, value); }
            }
        }

        #endregion
    }
}
