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
using Ivi.Fgen;
using IVI.C.NET.Adapter.IviCInterop;

namespace IVI.C.NET.Adapter
{
    public class IviFgenAdapter : DriverAdapterBase<IviCInterop.IviFgen>, IIviFgen
    {
        #region Enum Mapping

        private static IviEnumCMapping<StandardWaveform, int> FgenStandardWaveformMapping = IviEnumCMapping<StandardWaveform, int>.Instance
            .Map(Ivi.Fgen.StandardWaveform.Sine, IviFgenAttribute.IVIFGEN_VAL_WFM_SINE)
            .Map(Ivi.Fgen.StandardWaveform.Square, IviFgenAttribute.IVIFGEN_VAL_WFM_SQUARE)
            .Map(Ivi.Fgen.StandardWaveform.Triangle, IviFgenAttribute.IVIFGEN_VAL_WFM_TRIANGLE)
            .Map(Ivi.Fgen.StandardWaveform.RampUp, IviFgenAttribute.IVIFGEN_VAL_WFM_RAMP_UP)
            .Map(Ivi.Fgen.StandardWaveform.RampDown, IviFgenAttribute.IVIFGEN_VAL_WFM_RAMP_DOWN)
            .Map(Ivi.Fgen.StandardWaveform.DC, IviFgenAttribute.IVIFGEN_VAL_WFM_DC);

        private static IviEnumCMapping<AMSource, int> FgenAMSource = IviEnumCMapping<AMSource, int>.Instance
            .Map(AMSource.Internal, IviFgenAttribute.IVIFGEN_VAL_AM_INTERNAL)
            .Map(AMSource.External, IviFgenAttribute.IVIFGEN_VAL_AM_EXTERNAL);

        private static IviEnumCMapping<FMSource, int> FgenFMSource = IviEnumCMapping<FMSource, int>.Instance
            .Map(FMSource.Internal, IviFgenAttribute.IVIFGEN_VAL_FM_INTERNAL)
            .Map(FMSource.External, IviFgenAttribute.IVIFGEN_VAL_FM_EXTERNAL);

        private static IviEnumCMapping<BinaryAlignment, int> FgenBinaryAlignment = IviEnumCMapping<BinaryAlignment, int>.Instance
            .Map(BinaryAlignment.Left, IviFgenAttribute.IVIFGEN_VAL_BINARY_ALIGNMENT_LEFT)
            .Map(BinaryAlignment.Right, IviFgenAttribute.IVIFGEN_VAL_BINARY_ALIGNMENT_RIGHT);

        private static IviEnumCMapping<MarkerPolarity, int> FgenMarkerPolarity = IviEnumCMapping<MarkerPolarity, int>.Instance
            .Map(MarkerPolarity.ActiveHigh, IviFgenAttribute.IVIFGEN_VAL_MARKER_POLARITY_ACTIVE_HIGH)
            .Map(MarkerPolarity.ActiveLow, IviFgenAttribute.IVIFGEN_VAL_MARKER_POLARITY_ACTIVE_LOW);

        private static IviEnumCMapping<OperationMode, int> FgenOperationMode = IviEnumCMapping<OperationMode, int>.Instance
            .Map(OperationMode.Continuous, IviFgenAttribute.IVIFGEN_VAL_OPERATE_CONTINUOUS)
            .Map(OperationMode.Burst, IviFgenAttribute.IVIFGEN_VAL_OPERATE_BURST);

        private static IviEnumCMapping<TerminalConfiguration, int> FgenTerminalConfiguration = IviEnumCMapping<TerminalConfiguration, int>.Instance
            .Map(TerminalConfiguration.SingleEnded, IviFgenAttribute.IVIFGEN_VAL_TERMINAL_CONFIGURATION_SINGLE_ENDED)
            .Map(TerminalConfiguration.Differential, IviFgenAttribute.IVIFGEN_VAL_TERMINAL_CONFIGURATION_DIFFERENTIAL);

        private static IviEnumCMapping<OutputMode, int> FgenOutputMode = IviEnumCMapping<OutputMode, int>.Instance
            .Map(OutputMode.Function, IviFgenAttribute.IVIFGEN_VAL_OUTPUT_FUNC)
            .Map(OutputMode.Arbitrary, IviFgenAttribute.IVIFGEN_VAL_OUTPUT_ARB)
            .Map(OutputMode.Sequence, IviFgenAttribute.IVIFGEN_VAL_OUTPUT_SEQ);

        private static IviEnumCMapping<SampleClockSource, int> FgenSampleClockSource = IviEnumCMapping<SampleClockSource, int>.Instance
            .Map(SampleClockSource.Internal, IviFgenAttribute.IVIFGEN_VAL_SAMPLE_CLOCK_SOURCE_INTERNAL)
            .Map(SampleClockSource.External, IviFgenAttribute.IVIFGEN_VAL_SAMPLE_CLOCK_SOURCE_EXTERNAL);

        private static IviEnumCMapping<TriggerSlope, int> FgenTriggerSlope = IviEnumCMapping<TriggerSlope, int>.Instance
            .Map(TriggerSlope.Positive, IviFgenAttribute.IVIFGEN_VAL_TRIGGER_POSITIVE)
            .Map(TriggerSlope.Negative, IviFgenAttribute.IVIFGEN_VAL_TRIGGER_NEGATIVE)
            .Map(TriggerSlope.Either, IviFgenAttribute.IVIFGEN_VAL_TRIGGER_EITHER);


        #endregion

        private IIviFgenAM FgenAM = null;
        private IIviFgenFM FgenFM = null;
        private IIviFgenArbitrary FgenArbitrary = null;
        private IIviFgenDataMarkerCollection FgenDataMarkerCollection = null;
        private IIviFgenOutput FgenOutput = null;
        private IIviFgenSampleClock FgenSampleClock = null;
        private IIviFgenSparseMarkerCollection FgenSparseMarkerCollection = null;
        private IIviFgenStandardWaveform FgenStandardWaveform = null;
        private IIviFgenTrigger FgenTrigger = null;
        public IviFgenAdapter(string name, bool idQuery, bool reset, string options)
            : base(name, idQuery, reset, options)
        {
            FgenAM = new IviFgenAM(this);
            FgenFM = new IviFgenFM(this);
            FgenArbitrary = new IviFgenArbitrary(this);
            FgenDataMarkerCollection = new IviFgenDataMarkerCollection(this);
            FgenOutput = new IviFgenOutput(this);
            FgenSampleClock = new IviFgenSampleClock(this);
            FgenSparseMarkerCollection = new IviFgenSparseMarkerCollection(this);
            FgenStandardWaveform = new IviFgenStandardWaveform(this);
            FgenTrigger = new IviFgenTrigger(this);
        }

        public IIviFgenAM AM
        {
            get { return FgenAM; }
        }

        public void AbortGeneration()
        {
            ViSessionStatusCheck(((IviCInterop.IviFgen)Interop).AbortGeneration(Session));
        }

        public IIviFgenArbitrary Arbitrary
        {
            get { return FgenArbitrary; }
        }

        public IIviFgenDataMarkerCollection DataMarkers
        {
            get { return FgenDataMarkerCollection; }
        }

        public IIviFgenFM FM
        {
            get { return FgenFM; }
        }

        public void InitiateGeneration()
        {
            ViSessionStatusCheck(((IviCInterop.IviFgen)Interop).InitiateGeneration(Session));
        }

        public IIviFgenOutput Output
        {
            get { return FgenOutput; }
        }

        public IIviFgenSampleClock SampleClock
        {
            get { return FgenSampleClock; }
        }

        public IIviFgenSparseMarkerCollection SparseMarkers
        {
            get { return FgenSparseMarkerCollection; }
        }

        public IIviFgenStandardWaveform StandardWaveform
        {
            get { return FgenStandardWaveform; }
        }

        public IIviFgenTrigger Trigger
        {
            get { return FgenTrigger; }
        }

        internal class IviFgenAM : IIviFgenAM
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviFgen IviFgenInterop = null;
            public IviFgenAM(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviFgenInterop = (IviCInterop.IviFgen)Adapter.Interop;
            }

            public void ConfigureInternal(double depth, StandardWaveform waveformFunction, double frequency)
            {
                Adapter.ViSessionStatusCheck(IviFgenInterop.ConfigureAMInternal(Adapter.Session, depth, FgenStandardWaveformMapping.getC_Value(waveformFunction), frequency));
            }

            public bool GetEnabled(string channelName)
            {
                return Adapter.GetAttributeViBoolean(channelName, IviFgenAttribute.IVIFGEN_ATTR_AM_ENABLED);
            }

            public AMSource GetSource(string channelName)
            {
                return FgenAMSource.getEnum(Adapter.GetAttributeViInt32(channelName, IviFgenAttribute.IVIFGEN_ATTR_AM_SOURCE));
            }

            public double InternalDepth
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviFgenAttribute.IVIFGEN_ATTR_AM_INTERNAL_DEPTH);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviFgenAttribute.IVIFGEN_ATTR_AM_INTERNAL_DEPTH, value);
                }
            }

            public double InternalFrequency
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviFgenAttribute.IVIFGEN_ATTR_AM_INTERNAL_FREQUENCY);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviFgenAttribute.IVIFGEN_ATTR_AM_INTERNAL_FREQUENCY, value);
                }
            }

            public StandardWaveform InternalWaveformFunction
            {
                get
                {
                    return FgenStandardWaveformMapping.getEnum(Adapter.GetAttributeViInt32(IviFgenAttribute.IVIFGEN_ATTR_AM_INTERNAL_WAVEFORM));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviFgenAttribute.IVIFGEN_ATTR_AM_INTERNAL_WAVEFORM, FgenStandardWaveformMapping.getC_Value(value));
                }
            }

            public void SetEnabled(string channelName, bool enabled)
            {
                Adapter.SetAttributeViBoolean(channelName, IviFgenAttribute.IVIFGEN_ATTR_AM_ENABLED, enabled);
            }

            public void SetSource(string channelName, AMSource source)
            {
                Adapter.SetAttributeViInt32(channelName, IviFgenAttribute.IVIFGEN_ATTR_AM_SOURCE, FgenAMSource.getC_Value(source));
            }
        }

        internal class IviFgenArbitrary : IIviFgenArbitrary
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviFgen IviFgenInterop = null;
            private IIviFgenArbitrarySequence FgenArbitrarySequence = null;
            private IIviFgenArbitraryWaveform FgenArbitraryWaveform = null;
            public IviFgenArbitrary(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviFgenInterop = (IviCInterop.IviFgen)Adapter.Interop;
                FgenArbitrarySequence = new IviFgenArbitrarySequence(Adapter);
                FgenArbitraryWaveform = new IviFgenArbitraryWaveform(Adapter);
            }

            public BinaryAlignment BinaryAlignment
            {
                get { return FgenBinaryAlignment.getEnum(Adapter.GetAttributeViInt32(IviFgenAttribute.IVIFGEN_ATTR_BINARY_ALIGNMENT)); }
            }

            public void ClearMemory()
            {
                Adapter.ViSessionStatusCheck(IviFgenInterop.ClearArbMemory(Adapter.Session));
            }

            public int DataMask
            {
                get
                {
                    return Adapter.GetAttributeViInt32(IviFgenAttribute.IVIFGEN_ATTR_OUTPUT_DATA_MASK);
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviFgenAttribute.IVIFGEN_ATTR_OUTPUT_DATA_MASK, value);
                }
            }

            public double GetGain(string channelName)
            {
                return Adapter.GetAttributeViReal64(channelName, IviFgenAttribute.IVIFGEN_ATTR_ARB_GAIN);
            }

            public double GetOffset(string channelName)
            {
                return Adapter.GetAttributeViReal64(channelName, IviFgenAttribute.IVIFGEN_ATTR_ARB_OFFSET);
            }

            public int SampleBitResolution
            {
                get { return Adapter.GetAttributeViInt32(IviFgenAttribute.IVIFGEN_ATTR_SAMPLE_BIT_RESOLUTION); }
            }

            public double SampleRate
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviFgenAttribute.IVIFGEN_ATTR_ARB_SAMPLE_RATE);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviFgenAttribute.IVIFGEN_ATTR_ARB_SAMPLE_RATE, value);
                }
            }

            public IIviFgenArbitrarySequence Sequence
            {
                get { return FgenArbitrarySequence; }
            }

            public void SetGain(string channelName, double gain)
            {
                Adapter.SetAttributeViReal64(channelName, IviFgenAttribute.IVIFGEN_ATTR_ARB_GAIN, gain);
            }

            public void SetOffset(string channelName, double offset)
            {
                Adapter.SetAttributeViReal64(channelName, IviFgenAttribute.IVIFGEN_ATTR_ARB_OFFSET, offset);
            }

            public IIviFgenArbitraryWaveform Waveform
            {
                get { return FgenArbitraryWaveform; }
            }
        }

        internal class IviFgenArbitrarySequence : IIviFgenArbitrarySequence
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviFgen IviFgenInterop = null;
            public IviFgenArbitrarySequence(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviFgenInterop = (IviCInterop.IviFgen)Adapter.Interop;
            }

            public void Clear(int handle)
            {
                Adapter.ViSessionStatusCheck(IviFgenInterop.ClearArbSequence(Adapter.Session, handle));
            }

            public void Configure(string channelName, int handle, double gain, double offset)
            {
                Adapter.ViSessionStatusCheck(IviFgenInterop.ConfigureArbSequence(Adapter.Session, channelName, handle, gain, offset));
            }

            public int Create(int[] waveformHandle, int[] loopCount)
            {
                int seqHandle = 0;
                Adapter.ViSessionStatusCheck(IviFgenInterop.CreateArbSequence(Adapter.Session, waveformHandle.Length, waveformHandle, loopCount, ref seqHandle));
                return seqHandle;
            }

            public int DepthMax
            {
                get { return Adapter.GetAttributeViInt32(IviFgenAttribute.IVIFGEN_ATTR_SEQUENCE_DEPTH_MAX); }
            }

            public int LengthMax
            {
                get { return Adapter.GetAttributeViInt32(IviFgenAttribute.IVIFGEN_ATTR_MAX_SEQUENCE_LENGTH); }
            }

            public int LengthMin
            {
                get { return Adapter.GetAttributeViInt32(IviFgenAttribute.IVIFGEN_ATTR_MIN_SEQUENCE_LENGTH); }
            }

            public int LoopCountMax
            {
                get { return Adapter.GetAttributeViInt32(IviFgenAttribute.IVIFGEN_ATTR_MAX_LOOP_COUNT); }
            }

            public int NumberSequencesMax
            {
                get { return Adapter.GetAttributeViInt32(IviFgenAttribute.IVIFGEN_ATTR_MAX_NUM_SEQUENCES); }
            }
        }

        internal class IviFgenArbitraryWaveform : IIviFgenArbitraryWaveform
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviFgen IviFgenInterop = null;
            public IviFgenArbitraryWaveform(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviFgenInterop = (IviCInterop.IviFgen)Adapter.Interop;
            }

            public void Clear(int handle)
            {
                Adapter.ViSessionStatusCheck(IviFgenInterop.ClearArbWaveform(Adapter.Session, handle));
            }

            public void Configure(string channelName, int handle, double gain, double offset)
            {
                Adapter.ViSessionStatusCheck(IviFgenInterop.ConfigureArbWaveform(Adapter.Session, channelName, handle, gain, offset));
            }

            public int CreateChannelWaveform(string channelName, Ivi.Driver.IWaveform<double> waveform)
            {
                int waveformHandle = 0;
                Adapter.ViSessionStatusCheck(IviFgenInterop.CreateChannelArbWaveform(Adapter.Session, channelName, waveform.ValidPointCount, waveform.GetAllElements(), ref waveformHandle));
                return waveformHandle;
            }

            public int CreateChannelWaveform(string channelName, Ivi.Driver.IWaveform<int> waveform)
            {
                int waveformHandle = 0;
                Adapter.ViSessionStatusCheck(IviFgenInterop.CreateChannelArbWaveform32(Adapter.Session, channelName, waveform.ValidPointCount, waveform.GetAllElements(), ref waveformHandle));
                return waveformHandle;
            }

            public int CreateChannelWaveform(string channelName, Ivi.Driver.IWaveform<short> waveform)
            {
                int waveformHandle = 0;
                Adapter.ViSessionStatusCheck(IviFgenInterop.CreateChannelArbWaveform16(Adapter.Session, channelName, waveform.ValidPointCount, waveform.GetAllElements(), ref waveformHandle));
                return waveformHandle;
            }

            public double GetFrequency(string channelName)
            {
                return Adapter.GetAttributeViReal64(channelName, IviFgenAttribute.IVIFGEN_ATTR_ARB_FREQUENCY);
            }

            public int NumberWaveformsMax
            {
                get { return Adapter.GetAttributeViInt32(IviFgenAttribute.IVIFGEN_ATTR_MAX_NUM_WAVEFORMS); }
            }

            public int Quantum
            {
                get { return Adapter.GetAttributeViInt32(IviFgenAttribute.IVIFGEN_ATTR_WAVEFORM_QUANTUM); }
            }

            public void SetFrequency(string channelName, double frequency)
            {
                Adapter.SetAttributeViReal64(channelName, IviFgenAttribute.IVIFGEN_ATTR_ARB_FREQUENCY, frequency);
            }

            public long SizeMax
            {
                get { return Adapter.GetAttributeViInt32(IviFgenAttribute.IVIFGEN_ATTR_MAX_WAVEFORM_SIZE); }
            }

            public long SizeMin
            {
                get { return Adapter.GetAttributeViInt32(IviFgenAttribute.IVIFGEN_ATTR_MIN_WAVEFORM_SIZE); }
            }
        }

        internal class IviFgenDataMarkerCollection : IIviFgenDataMarkerCollection
        {
            private IDriverAdapterBase Adapter;
            private IviCInterop.IviFgen IviFgenInterop = null;
            private IList<IIviFgenDataMarker> DataMarkers = null;
            private IList<string> DataMarkerNames = null;

            public IviFgenDataMarkerCollection(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviFgenInterop = (IviCInterop.IviFgen)Adapter.Interop;
                int DataMarkerCount = Adapter.GetAttributeViInt32(IviFgenAttribute.IVIFGEN_ATTR_DATAMARKER_COUNT);

                DataMarkers = new List<IIviFgenDataMarker>();
                DataMarkerNames = new List<string>();
                for (int Index = 1; Index <= DataMarkerCount; Index++)
                {
                    IIviFgenDataMarker DataMarker = new IviFgenDataMarker(Adapter, Index);
                    DataMarkers.Add(DataMarker);
                    DataMarkerNames.Add(DataMarker.Name);
                }
            }

            public void Clear()
            {
                Adapter.ViSessionStatusCheck(IviFgenInterop.DisableAllDataMarkers(Adapter.Session));
            }

            public int Count
            {
                get { return DataMarkers.Count; }
            }

            public IIviFgenDataMarker this[string name]
            {
                get { return DataMarkers[DataMarkerNames.IndexOf(name)]; }
            }

            public System.Collections.Generic.IEnumerator<IIviFgenDataMarker> GetEnumerator()
            {
                return DataMarkers.GetEnumerator();
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return DataMarkers.GetEnumerator();
            }
        }

        internal class IviFgenDataMarker : IIviFgenDataMarker
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviFgen IviFgenInterop = null;
            private int Index;
            private string DataMarkerName;

            public IviFgenDataMarker(IDriverAdapterBase Adapter, int Index)
            {
                this.Adapter = Adapter;
                IviFgenInterop = (IviCInterop.IviFgen)Adapter.Interop;
                this.Index = Index;

                StringBuilder NameValue = new StringBuilder(256);
                Adapter.ViSessionStatusCheck(IviFgenInterop.GetDataMarkerName(Adapter.Session, Index, NameValue.Capacity, NameValue));
                DataMarkerName = NameValue.ToString();
            }

            public double Amplitude
            {
                get
                {
                    return Adapter.GetAttributeViReal64(DataMarkerName, IviFgenAttribute.IVIFGEN_ATTR_DATAMARKER_AMPLITUDE);
                }
                set
                {
                    Adapter.SetAttributeViReal64(DataMarkerName, IviFgenAttribute.IVIFGEN_ATTR_DATAMARKER_AMPLITUDE, value);
                }
            }

            public int BitPosition
            {
                get
                {
                    return Adapter.GetAttributeViInt32(DataMarkerName, IviFgenAttribute.IVIFGEN_ATTR_DATAMARKER_BIT_POSITION);
                }
                set
                {
                    Adapter.SetAttributeViInt32(DataMarkerName, IviFgenAttribute.IVIFGEN_ATTR_DATAMARKER_BIT_POSITION, value);
                }
            }

            public void Configure(string sourceChannel, int bitPosition, string destination)
            {
                Adapter.ViSessionStatusCheck(IviFgenInterop.ConfigureDataMarker(Adapter.Session, DataMarkerName, sourceChannel, bitPosition, destination));
            }

            public Ivi.Driver.PrecisionTimeSpan Delay
            {
                get
                {
                    return new Ivi.Driver.PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * Adapter.GetAttributeViReal64(DataMarkerName, IviFgenAttribute.IVIFGEN_ATTR_DATAMARKER_DELAY))));
                }
                set
                {
                    Adapter.SetAttributeViReal64(DataMarkerName, IviFgenAttribute.IVIFGEN_ATTR_DATAMARKER_DELAY, value.TotalSeconds);
                }
            }

            public string Destination
            {
                get
                {
                    return Adapter.GetAttributeViString(DataMarkerName, IviFgenAttribute.IVIFGEN_ATTR_DATAMARKER_DESTINATION);
                }
                set
                {
                    Adapter.SetAttributeViString(DataMarkerName, IviFgenAttribute.IVIFGEN_ATTR_DATAMARKER_DESTINATION, value);
                }
            }

            public MarkerPolarity Polarity
            {
                get
                {
                    return FgenMarkerPolarity.getEnum(Adapter.GetAttributeViInt32(DataMarkerName, IviFgenAttribute.IVIFGEN_ATTR_DATAMARKER_POLARITY));
                }
                set
                {
                    Adapter.SetAttributeViInt32(DataMarkerName, IviFgenAttribute.IVIFGEN_ATTR_DATAMARKER_POLARITY, FgenMarkerPolarity.getC_Value(value));
                }
            }

            public string SourceChannel
            {
                get
                {
                    return Adapter.GetAttributeViString(DataMarkerName, IviFgenAttribute.IVIFGEN_ATTR_DATAMARKER_SOURCE_CHANNEL);
                }
                set
                {
                    Adapter.SetAttributeViString(DataMarkerName, IviFgenAttribute.IVIFGEN_ATTR_DATAMARKER_SOURCE_CHANNEL, value);
                }
            }

            public string Name
            {
                get { return DataMarkerName; }
            }
        }

        internal class IviFgenFM : IIviFgenFM
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviFgen IviFgenInterop = null;
            public IviFgenFM(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviFgenInterop = (IviCInterop.IviFgen)Adapter.Interop;
            }

            public void ConfigureInternal(double deviation, StandardWaveform waveformFunction, double frequency)
            {
                Adapter.ViSessionStatusCheck(IviFgenInterop.ConfigureFMInternal(Adapter.Session, deviation, FgenStandardWaveformMapping.getC_Value(waveformFunction), frequency));
            }

            public bool GetEnabled(string channelName)
            {
                return Adapter.GetAttributeViBoolean(channelName, IviFgenAttribute.IVIFGEN_ATTR_FM_ENABLED);
            }

            public FMSource GetSource(string channelName)
            {
                return FgenFMSource.getEnum(Adapter.GetAttributeViInt32(channelName, IviFgenAttribute.IVIFGEN_ATTR_FM_SOURCE));
            }

            public double InternalDeviation
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviFgenAttribute.IVIFGEN_ATTR_FM_INTERNAL_DEVIATION);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviFgenAttribute.IVIFGEN_ATTR_FM_INTERNAL_DEVIATION, value);
                }
            }

            public double InternalFrequency
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviFgenAttribute.IVIFGEN_ATTR_FM_INTERNAL_FREQUENCY);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviFgenAttribute.IVIFGEN_ATTR_FM_INTERNAL_FREQUENCY, value);
                }
            }

            public StandardWaveform InternalWaveformFunction
            {
                get
                {
                    return FgenStandardWaveformMapping.getEnum(Adapter.GetAttributeViInt32(IviFgenAttribute.IVIFGEN_ATTR_FM_INTERNAL_WAVEFORM));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviFgenAttribute.IVIFGEN_ATTR_FM_INTERNAL_WAVEFORM, FgenStandardWaveformMapping.getC_Value(value));
                }
            }

            public void SetEnabled(string channelName, bool enabled)
            {
                Adapter.SetAttributeViBoolean(channelName, IviFgenAttribute.IVIFGEN_ATTR_FM_ENABLED, enabled);
            }

            public void SetSource(string channelName, FMSource source)
            {
                Adapter.SetAttributeViInt32(channelName, IviFgenAttribute.IVIFGEN_ATTR_FM_SOURCE, FgenFMSource.getC_Value(source));
            }
        }

        internal class IviFgenOutput : IIviFgenOutput
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviFgen IviFgenInterop = null;
            public IviFgenOutput(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviFgenInterop = (IviCInterop.IviFgen)Adapter.Interop;
            }

            public int Count
            {
                get { return Adapter.GetAttributeViInt32(IviFgenAttribute.IVIFGEN_ATTR_CHANNEL_COUNT); }
            }

            public string GetChannelName(int index)
            {
                StringBuilder NameValue = new StringBuilder(256);
                Adapter.ViSessionStatusCheck(IviFgenInterop.GetChannelName(Adapter.Session, index, NameValue.Capacity, NameValue));
                return NameValue.ToString();
            }

            public bool GetEnabled(string channelName)
            {
                return Adapter.GetAttributeViBoolean(channelName, IviFgenAttribute.IVIFGEN_ATTR_OUTPUT_ENABLED);
            }

            public double GetImpedance(string channelName)
            {
                return Adapter.GetAttributeViReal64(channelName, IviFgenAttribute.IVIFGEN_ATTR_OUTPUT_IMPEDANCE);
            }

            public OperationMode GetOperationMode(string channelName)
            {
                return FgenOperationMode.getEnum(Adapter.GetAttributeViInt32(channelName, IviFgenAttribute.IVIFGEN_ATTR_OPERATION_MODE));
            }

            public TerminalConfiguration GetTerminalConfiguration(string channelName)
            {
                return FgenTerminalConfiguration.getEnum(Adapter.GetAttributeViInt32(channelName, IviFgenAttribute.IVIFGEN_ATTR_TERMINAL_CONFIGURATION));
            }

            public OutputMode OutputMode
            {
                get
                {
                    return FgenOutputMode.getEnum(Adapter.GetAttributeViInt32(IviFgenAttribute.IVIFGEN_ATTR_OUTPUT_MODE));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviFgenAttribute.IVIFGEN_ATTR_OUTPUT_MODE, FgenOutputMode.getC_Value(value));
                }
            }

            public string ReferenceClockSource
            {
                get
                {
                    return Adapter.GetAttributeViString(IviFgenAttribute.IVIFGEN_ATTR_REF_CLOCK_SOURCE);
                }
                set
                {
                    Adapter.SetAttributeViString(IviFgenAttribute.IVIFGEN_ATTR_REF_CLOCK_SOURCE, value);
                }
            }

            public void SetEnabled(string channelName, bool enabled)
            {
                Adapter.SetAttributeViBoolean(channelName, IviFgenAttribute.IVIFGEN_ATTR_OUTPUT_ENABLED, enabled);
            }

            public void SetImpedance(string channelName, double impedance)
            {
                Adapter.SetAttributeViReal64(channelName, IviFgenAttribute.IVIFGEN_ATTR_OUTPUT_IMPEDANCE, impedance);
            }

            public void SetOperationMode(string channelName, OperationMode mode)
            {
                Adapter.SetAttributeViInt32(channelName, IviFgenAttribute.IVIFGEN_ATTR_OPERATION_MODE, FgenOperationMode.getC_Value(mode));
            }

            public void SetTerminalConfiguration(string channelName, TerminalConfiguration configuration)
            {
                Adapter.SetAttributeViInt32(channelName, IviFgenAttribute.IVIFGEN_ATTR_TERMINAL_CONFIGURATION, FgenTerminalConfiguration.getC_Value(configuration));
            }
        }

        internal class IviFgenSampleClock : IIviFgenSampleClock
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviFgen IviFgenInterop = null;
            public IviFgenSampleClock(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviFgenInterop = (IviCInterop.IviFgen)Adapter.Interop;
            }

            public bool OutputEnabled
            {
                get
                {
                    return Adapter.GetAttributeViBoolean(IviFgenAttribute.IVIFGEN_ATTR_SAMPLE_CLOCK_OUTPUT_ENABLED);
                }
                set
                {
                    Adapter.SetAttributeViBoolean(IviFgenAttribute.IVIFGEN_ATTR_SAMPLE_CLOCK_OUTPUT_ENABLED, value);
                }
            }

            public SampleClockSource Source
            {
                get
                {
                    return FgenSampleClockSource.getEnum(Adapter.GetAttributeViInt32(IviFgenAttribute.IVIFGEN_ATTR_SAMPLE_CLOCK_SOURCE));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviFgenAttribute.IVIFGEN_ATTR_SAMPLE_CLOCK_SOURCE, FgenSampleClockSource.getC_Value(value));
                }
            }
        }

        internal class IviFgenSparseMarkerCollection : IIviFgenSparseMarkerCollection
        {
            private IDriverAdapterBase Adapter;
            private IviCInterop.IviFgen IviFgenInterop = null;
            private IList<IIviFgenSparseMarker> SparseMarkers = null;
            private IList<string> SparseMarkerNames = null;

            public IviFgenSparseMarkerCollection(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviFgenInterop = (IviCInterop.IviFgen)Adapter.Interop;
                int DataMarkerCount = Adapter.GetAttributeViInt32(IviFgenAttribute.IVIFGEN_ATTR_DATAMARKER_COUNT);

                SparseMarkers = new List<IIviFgenSparseMarker>();
                SparseMarkerNames = new List<string>();
                for (int Index = 1; Index <= DataMarkerCount; Index++)
                {
                    IIviFgenSparseMarker DataMarker = new IviFgenSparseMarker(Adapter, Index);
                    SparseMarkers.Add(DataMarker);
                    SparseMarkerNames.Add(DataMarker.Name);
                }
            }

            public void Clear()
            {
                Adapter.ViSessionStatusCheck(IviFgenInterop.DisableAllDataMarkers(Adapter.Session));
            }

            public int Count
            {
                get { return SparseMarkers.Count; }
            }

            public IIviFgenSparseMarker this[string name]
            {
                get { return SparseMarkers[SparseMarkerNames.IndexOf(name)]; }
            }

            public System.Collections.Generic.IEnumerator<IIviFgenSparseMarker> GetEnumerator()
            {
                return SparseMarkers.GetEnumerator();
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return SparseMarkers.GetEnumerator();
            }
        }

        internal class IviFgenSparseMarker : IIviFgenSparseMarker
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviFgen IviFgenInterop = null;
            private int Index;
            private string SparseMarkerName;

            public IviFgenSparseMarker(IDriverAdapterBase Adapter, int Index)
            {
                this.Adapter = Adapter;
                IviFgenInterop = (IviCInterop.IviFgen)Adapter.Interop;
                this.Index = Index;

                StringBuilder NameValue = new StringBuilder(256);
                Adapter.ViSessionStatusCheck(IviFgenInterop.GetSparseMarkerName(Adapter.Session, Index, NameValue.Capacity, NameValue));
                SparseMarkerName = NameValue.ToString();
            }

            public double Amplitude
            {
                get
                {
                    return Adapter.GetAttributeViReal64(SparseMarkerName, IviFgenAttribute.IVIFGEN_ATTR_SPARSEMARKER_AMPLITUDE);
                }
                set
                {
                    Adapter.SetAttributeViReal64(SparseMarkerName, IviFgenAttribute.IVIFGEN_ATTR_SPARSEMARKER_AMPLITUDE, value);
                }
            }

            public void Configure(int waveformHandle, long[] indexes, string destination)
            {
                Adapter.ViSessionStatusCheck(IviFgenInterop.ConfigureSparseMarker(Adapter.Session, SparseMarkerName, waveformHandle, indexes.Length, indexes, destination));
            }

            public Ivi.Driver.PrecisionTimeSpan Delay
            {
                get
                {
                    return new Ivi.Driver.PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * Adapter.GetAttributeViReal64(SparseMarkerName, IviFgenAttribute.IVIFGEN_ATTR_SPARSEMARKER_DELAY))));
                }
                set
                {
                    Adapter.SetAttributeViReal64(SparseMarkerName, IviFgenAttribute.IVIFGEN_ATTR_SPARSEMARKER_DELAY, value.TotalSeconds);
                }
            }

            public string Destination
            {
                get
                {
                    return Adapter.GetAttributeViString(SparseMarkerName, IviFgenAttribute.IVIFGEN_ATTR_SPARSEMARKER_DESTINATION);
                }
                set
                {
                    Adapter.SetAttributeViString(SparseMarkerName, IviFgenAttribute.IVIFGEN_ATTR_SPARSEMARKER_DESTINATION, value);
                }
            }

            public long[] GetIndexes()
            {
                long maxIndexSize = 4096;
                long indexesActualSize = 0;
                IntPtr pIndexes = Marshal.AllocHGlobal((int)(maxIndexSize * sizeof(long)));

                Adapter.ViSessionStatusCheck(IviFgenInterop.GetSparseMarkerIndexes(Adapter.Session, SparseMarkerName, maxIndexSize, pIndexes, ref indexesActualSize));

                long[] Indexes = new long[indexesActualSize];
                Marshal.Copy(pIndexes, Indexes, 0, (int)indexesActualSize);
                Marshal.FreeHGlobal(pIndexes);

                return Indexes;
            }

            public MarkerPolarity Polarity
            {
                get
                {
                    return FgenMarkerPolarity.getEnum(Adapter.GetAttributeViInt32(SparseMarkerName, IviFgenAttribute.IVIFGEN_ATTR_SPARSEMARKER_POLARITY));
                }
                set
                {
                    Adapter.SetAttributeViInt32(SparseMarkerName, IviFgenAttribute.IVIFGEN_ATTR_SPARSEMARKER_POLARITY, FgenMarkerPolarity.getC_Value(value));
                }
            }

            public void SetIndexes(long[] indexes)
            {
                Adapter.ViSessionStatusCheck(IviFgenInterop.SetSparseMarkerIndexes(Adapter.Session, SparseMarkerName, indexes.Length, indexes));
            }

            public int WaveformHandle
            {
                get
                {
                    return Adapter.GetAttributeViInt32(SparseMarkerName, IviFgenAttribute.IVIFGEN_ATTR_SPARSEMARKER_WFMHANDLE);
                }
                set
                {
                    Adapter.SetAttributeViInt32(SparseMarkerName, IviFgenAttribute.IVIFGEN_ATTR_SPARSEMARKER_WFMHANDLE, value);
                }
            }

            public string Name
            {
                get { return SparseMarkerName; }
            }
        }

        internal class IviFgenStandardWaveform : IIviFgenStandardWaveform
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviFgen IviFgenInterop = null;
            public IviFgenStandardWaveform(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviFgenInterop = (IviCInterop.IviFgen)Adapter.Interop;
            }

            public void Configure(string channelName, StandardWaveform waveformFunction, double amplitude, double dcOffset, double frequency, double startPhase)
            {
                Adapter.ViSessionStatusCheck(IviFgenInterop.ConfigureStandardWaveform(Adapter.Session, channelName, FgenStandardWaveformMapping.getC_Value(waveformFunction), amplitude, dcOffset, frequency, startPhase));
            }

            public double GetAmplitude(string channelName)
            {
                return Adapter.GetAttributeViReal64(channelName, IviFgenAttribute.IVIFGEN_ATTR_FUNC_AMPLITUDE);
            }

            public double GetDCOffset(string channelName)
            {
                return Adapter.GetAttributeViReal64(channelName, IviFgenAttribute.IVIFGEN_ATTR_FUNC_DC_OFFSET);
            }

            public double GetDutyCycleHigh(string channelName)
            {
                return Adapter.GetAttributeViReal64(channelName, IviFgenAttribute.IVIFGEN_ATTR_FUNC_DUTY_CYCLE_HIGH);
            }

            public double GetFrequency(string channelName)
            {
                return Adapter.GetAttributeViReal64(channelName, IviFgenAttribute.IVIFGEN_ATTR_FUNC_FREQUENCY);
            }

            public double GetStartPhase(string channelName)
            {
                return Adapter.GetAttributeViReal64(channelName, IviFgenAttribute.IVIFGEN_ATTR_FUNC_START_PHASE);
            }

            public StandardWaveform GetWaveformFunction(string channelName)
            {
                return FgenStandardWaveformMapping.getEnum(Adapter.GetAttributeViInt32(channelName, IviFgenAttribute.IVIFGEN_ATTR_FUNC_WAVEFORM));
            }

            public void SetAmplitude(string channelName, double amplitude)
            {
                Adapter.SetAttributeViReal64(channelName, IviFgenAttribute.IVIFGEN_ATTR_FUNC_AMPLITUDE, amplitude);
            }

            public void SetDCOffset(string channelName, double dcOffset)
            {
                Adapter.SetAttributeViReal64(channelName, IviFgenAttribute.IVIFGEN_ATTR_FUNC_DC_OFFSET, dcOffset);
            }

            public void SetDutyCycleHigh(string channelName, double dutyCycleHigh)
            {
                Adapter.SetAttributeViReal64(channelName, IviFgenAttribute.IVIFGEN_ATTR_FUNC_DUTY_CYCLE_HIGH, dutyCycleHigh);
            }

            public void SetFrequency(string channelName, double frequency)
            {
                Adapter.SetAttributeViReal64(channelName, IviFgenAttribute.IVIFGEN_ATTR_FUNC_FREQUENCY, frequency);
            }

            public void SetStartPhase(string channelName, double startPhase)
            {
                Adapter.SetAttributeViReal64(channelName, IviFgenAttribute.IVIFGEN_ATTR_FUNC_START_PHASE, startPhase);
            }

            public void SetWaveformFunction(string channelName, StandardWaveform waveformFunction)
            {
                Adapter.SetAttributeViInt32(channelName, IviFgenAttribute.IVIFGEN_ATTR_FUNC_WAVEFORM, FgenStandardWaveformMapping.getC_Value(waveformFunction));
            }
        }

        internal class IviFgenTrigger : IIviFgenTrigger
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviFgen IviFgenInterop = null;
            private IIviFgenTriggerAdvance FgenTriggerAdvance = null;
            private IIviFgenTriggerHold FgenTriggerHold = null;
            private IIviFgenTriggerResume FgenTriggerResume = null;
            private IIviFgenTriggerStart FgenTriggerStart = null;
            private IIviFgenTriggerStop FgenTriggerStop = null;
            public IviFgenTrigger(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviFgenInterop = (IviCInterop.IviFgen)Adapter.Interop;
                FgenTriggerAdvance = new IviFgenTriggerAdvance(Adapter);
                FgenTriggerHold = new IviFgenTriggerHold(Adapter);
                FgenTriggerResume = new IviFgenTriggerResume(Adapter);
                FgenTriggerStart = new IviFgenTriggerStart(Adapter);
                FgenTriggerStop = new IviFgenTriggerStop(Adapter);
            }

            public IIviFgenTriggerAdvance Advance
            {
                get { return FgenTriggerAdvance; }
            }

            public int GetBurstCount(string channelName)
            {
                return Adapter.GetAttributeViInt32(channelName, IviFgenAttribute.IVIFGEN_ATTR_BURST_COUNT);
            }

            public string GetSource(string channelName)
            {
                return Adapter.GetAttributeViString(channelName, IviFgenAttribute.IVIFGEN_ATTR_TRIGGER_SOURCE);
            }

            public IIviFgenTriggerHold Hold
            {
                get { return FgenTriggerHold; }
            }

            public double InternalRate
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviFgenAttribute.IVIFGEN_ATTR_INTERNAL_TRIGGER_RATE);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviFgenAttribute.IVIFGEN_ATTR_INTERNAL_TRIGGER_RATE, value);
                }
            }

            public IIviFgenTriggerResume Resume
            {
                get { return FgenTriggerResume; }
            }

            public void SendSoftwareTrigger()
            {
                Adapter.ViSessionStatusCheck(IviFgenInterop.SendSoftwareTrigger(Adapter.Session));
            }

            public void SetBurstCount(string channelName, int burstCount)
            {
                Adapter.SetAttributeViInt32(channelName, IviFgenAttribute.IVIFGEN_ATTR_BURST_COUNT, burstCount);
            }

            public void SetSource(string channelName, string source)
            {
                Adapter.SetAttributeViString(channelName, IviFgenAttribute.IVIFGEN_ATTR_TRIGGER_SOURCE, source);
            }

            public IIviFgenTriggerStart Start
            {
                get { return FgenTriggerStart; }
            }

            public IIviFgenTriggerStop Stop
            {
                get { return FgenTriggerStop; }
            }
        }

        internal class IviFgenTriggerAdvance : IIviFgenTriggerAdvance
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviFgen IviFgenInterop = null;
            public IviFgenTriggerAdvance(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviFgenInterop = (IviCInterop.IviFgen)Adapter.Interop;
            }

            public void Configure(string channelName, string source, TriggerSlope slope)
            {
                Adapter.ViSessionStatusCheck(IviFgenInterop.ConfigureAdvanceTrigger(Adapter.Session, channelName, source, FgenTriggerSlope.getC_Value(slope)));
            }

            public Ivi.Driver.PrecisionTimeSpan GetDelay(string channelName)
            {
                return new Ivi.Driver.PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * Adapter.GetAttributeViReal64(channelName, IviFgenAttribute.IVIFGEN_ATTR_ADVANCE_TRIGGER_DELAY))));
            }

            public TriggerSlope GetSlope(string channelName)
            {
                return FgenTriggerSlope.getEnum(Adapter.GetAttributeViInt32(channelName, IviFgenAttribute.IVIFGEN_ATTR_ADVANCE_TRIGGER_SLOPE));
            }

            public string GetSource(string channelName)
            {
                return Adapter.GetAttributeViString(channelName, IviFgenAttribute.IVIFGEN_ATTR_ADVANCE_TRIGGER_SOURCE);
            }

            public double GetThreshold(string channelName)
            {
                return Adapter.GetAttributeViReal64(channelName, IviFgenAttribute.IVIFGEN_ATTR_ADVANCE_TRIGGER_THRESHOLD);
            }

            public void SendSoftwareTrigger()
            {
                Adapter.ViSessionStatusCheck(IviFgenInterop.SendSoftwareAdvanceTrigger(Adapter.Session));
            }

            public void SetDelay(string channelName, Ivi.Driver.PrecisionTimeSpan delay)
            {
                Adapter.SetAttributeViReal64(channelName, IviFgenAttribute.IVIFGEN_ATTR_ADVANCE_TRIGGER_DELAY, delay.TotalSeconds);
            }

            public void SetSlope(string channelName, TriggerSlope slope)
            {
                Adapter.SetAttributeViInt32(channelName, IviFgenAttribute.IVIFGEN_ATTR_ADVANCE_TRIGGER_SLOPE, FgenTriggerSlope.getC_Value(slope));
            }

            public void SetSource(string channelName, string source)
            {
                Adapter.SetAttributeViString(channelName, IviFgenAttribute.IVIFGEN_ATTR_ADVANCE_TRIGGER_SOURCE, source);
            }

            public void SetThreshold(string channelName, double threshold)
            {
                Adapter.SetAttributeViReal64(channelName, IviFgenAttribute.IVIFGEN_ATTR_ADVANCE_TRIGGER_THRESHOLD, threshold);
            }
        }

        internal class IviFgenTriggerHold : IIviFgenTriggerHold
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviFgen IviFgenInterop = null;
            public IviFgenTriggerHold(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviFgenInterop = (IviCInterop.IviFgen)Adapter.Interop;
            }

            public void Configure(string channelName, string source, TriggerSlope slope)
            {
                Adapter.ViSessionStatusCheck(IviFgenInterop.ConfigureHoldTrigger(Adapter.Session, channelName, source, FgenTriggerSlope.getC_Value(slope)));
            }

            public Ivi.Driver.PrecisionTimeSpan GetDelay(string channelName)
            {
                return new Ivi.Driver.PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * Adapter.GetAttributeViReal64(channelName, IviFgenAttribute.IVIFGEN_ATTR_HOLD_TRIGGER_DELAY))));
            }

            public TriggerSlope GetSlope(string channelName)
            {
                return FgenTriggerSlope.getEnum(Adapter.GetAttributeViInt32(channelName, IviFgenAttribute.IVIFGEN_ATTR_HOLD_TRIGGER_SLOPE));
            }

            public string GetSource(string channelName)
            {
                return Adapter.GetAttributeViString(channelName, IviFgenAttribute.IVIFGEN_ATTR_HOLD_TRIGGER_SOURCE);
            }

            public double GetThreshold(string channelName)
            {
                return Adapter.GetAttributeViReal64(channelName, IviFgenAttribute.IVIFGEN_ATTR_HOLD_TRIGGER_THRESHOLD);
            }

            public void SendSoftwareTrigger()
            {
                Adapter.ViSessionStatusCheck(IviFgenInterop.SendSoftwareHoldTrigger(Adapter.Session));
            }

            public void SetDelay(string channelName, Ivi.Driver.PrecisionTimeSpan delay)
            {
                Adapter.SetAttributeViReal64(channelName, IviFgenAttribute.IVIFGEN_ATTR_HOLD_TRIGGER_DELAY, delay.TotalSeconds);
            }

            public void SetSlope(string channelName, TriggerSlope slope)
            {
                Adapter.SetAttributeViInt32(channelName, IviFgenAttribute.IVIFGEN_ATTR_HOLD_TRIGGER_SLOPE, FgenTriggerSlope.getC_Value(slope));
            }

            public void SetSource(string channelName, string source)
            {
                Adapter.SetAttributeViString(channelName, IviFgenAttribute.IVIFGEN_ATTR_HOLD_TRIGGER_SOURCE, source);
            }

            public void SetThreshold(string channelName, double threshold)
            {
                Adapter.SetAttributeViReal64(channelName, IviFgenAttribute.IVIFGEN_ATTR_HOLD_TRIGGER_THRESHOLD, threshold);
            }
        }

        internal class IviFgenTriggerResume : IIviFgenTriggerResume
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviFgen IviFgenInterop = null;
            public IviFgenTriggerResume(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviFgenInterop = (IviCInterop.IviFgen)Adapter.Interop;
            }

            public void Configure(string channelName, string source, TriggerSlope slope)
            {
                Adapter.ViSessionStatusCheck(IviFgenInterop.ConfigureResumeTrigger(Adapter.Session, channelName, source, FgenTriggerSlope.getC_Value(slope)));
            }

            public Ivi.Driver.PrecisionTimeSpan GetDelay(string channelName)
            {
                return new Ivi.Driver.PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * Adapter.GetAttributeViReal64(channelName, IviFgenAttribute.IVIFGEN_ATTR_RESUME_TRIGGER_DELAY))));
            }

            public TriggerSlope GetSlope(string channelName)
            {
                return FgenTriggerSlope.getEnum(Adapter.GetAttributeViInt32(channelName, IviFgenAttribute.IVIFGEN_ATTR_RESUME_TRIGGER_SLOPE));
            }

            public string GetSource(string channelName)
            {
                return Adapter.GetAttributeViString(channelName, IviFgenAttribute.IVIFGEN_ATTR_RESUME_TRIGGER_SOURCE);
            }

            public double GetThreshold(string channelName)
            {
                return Adapter.GetAttributeViReal64(channelName, IviFgenAttribute.IVIFGEN_ATTR_RESUME_TRIGGER_THRESHOLD);
            }

            public void SendSoftwareTrigger()
            {
                Adapter.ViSessionStatusCheck(IviFgenInterop.SendSoftwareResumeTrigger(Adapter.Session));
            }

            public void SetDelay(string channelName, Ivi.Driver.PrecisionTimeSpan delay)
            {
                Adapter.SetAttributeViReal64(channelName, IviFgenAttribute.IVIFGEN_ATTR_RESUME_TRIGGER_DELAY, delay.TotalSeconds);
            }

            public void SetSlope(string channelName, TriggerSlope slope)
            {
                Adapter.SetAttributeViInt32(channelName, IviFgenAttribute.IVIFGEN_ATTR_RESUME_TRIGGER_SLOPE, FgenTriggerSlope.getC_Value(slope));
            }

            public void SetSource(string channelName, string source)
            {
                Adapter.SetAttributeViString(channelName, IviFgenAttribute.IVIFGEN_ATTR_RESUME_TRIGGER_SOURCE, source);
            }

            public void SetThreshold(string channelName, double threshold)
            {
                Adapter.SetAttributeViReal64(channelName, IviFgenAttribute.IVIFGEN_ATTR_RESUME_TRIGGER_THRESHOLD, threshold);
            }
        }

        internal class IviFgenTriggerStart : IIviFgenTriggerStart
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviFgen IviFgenInterop = null;
            public IviFgenTriggerStart(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviFgenInterop = (IviCInterop.IviFgen)Adapter.Interop;
            }

            public void Configure(string channelName, string source, TriggerSlope slope)
            {
                Adapter.ViSessionStatusCheck(IviFgenInterop.ConfigureStartTrigger(Adapter.Session, channelName, source, FgenTriggerSlope.getC_Value(slope)));
            }

            public Ivi.Driver.PrecisionTimeSpan GetDelay(string channelName)
            {
                return new Ivi.Driver.PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * Adapter.GetAttributeViReal64(channelName, IviFgenAttribute.IVIFGEN_ATTR_START_TRIGGER_DELAY))));
            }

            public TriggerSlope GetSlope(string channelName)
            {
                return FgenTriggerSlope.getEnum(Adapter.GetAttributeViInt32(channelName, IviFgenAttribute.IVIFGEN_ATTR_START_TRIGGER_SLOPE));
            }

            public string GetSource(string channelName)
            {
                return Adapter.GetAttributeViString(channelName, IviFgenAttribute.IVIFGEN_ATTR_START_TRIGGER_SOURCE);
            }

            public double GetThreshold(string channelName)
            {
                return Adapter.GetAttributeViReal64(channelName, IviFgenAttribute.IVIFGEN_ATTR_START_TRIGGER_THRESHOLD);
            }

            public void SetDelay(string channelName, Ivi.Driver.PrecisionTimeSpan delay)
            {
                Adapter.SetAttributeViReal64(channelName, IviFgenAttribute.IVIFGEN_ATTR_START_TRIGGER_DELAY, delay.TotalSeconds);
            }

            public void SetSlope(string channelName, TriggerSlope slope)
            {
                Adapter.SetAttributeViInt32(channelName, IviFgenAttribute.IVIFGEN_ATTR_START_TRIGGER_SLOPE, FgenTriggerSlope.getC_Value(slope));
            }

            public void SetSource(string channelName, string source)
            {
                Adapter.SetAttributeViString(channelName, IviFgenAttribute.IVIFGEN_ATTR_START_TRIGGER_SOURCE, source);
            }

            public void SetThreshold(string channelName, double threshold)
            {
                Adapter.SetAttributeViReal64(channelName, IviFgenAttribute.IVIFGEN_ATTR_START_TRIGGER_THRESHOLD, threshold);
            }
        }

        internal class IviFgenTriggerStop : IIviFgenTriggerStop
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviFgen IviFgenInterop = null;
            public IviFgenTriggerStop(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviFgenInterop = (IviCInterop.IviFgen)Adapter.Interop;
            }

            public void Configure(string channelName, string source, TriggerSlope slope)
            {
                Adapter.ViSessionStatusCheck(IviFgenInterop.ConfigureStopTrigger(Adapter.Session, channelName, source, FgenTriggerSlope.getC_Value(slope)));
            }

            public Ivi.Driver.PrecisionTimeSpan GetDelay(string channelName)
            {
                return new Ivi.Driver.PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * Adapter.GetAttributeViReal64(channelName, IviFgenAttribute.IVIFGEN_ATTR_STOP_TRIGGER_DELAY))));
            }

            public TriggerSlope GetSlope(string channelName)
            {
                return FgenTriggerSlope.getEnum(Adapter.GetAttributeViInt32(channelName, IviFgenAttribute.IVIFGEN_ATTR_STOP_TRIGGER_SLOPE));
            }

            public string GetSource(string channelName)
            {
                return Adapter.GetAttributeViString(channelName, IviFgenAttribute.IVIFGEN_ATTR_STOP_TRIGGER_SOURCE);
            }

            public double GetThreshold(string channelName)
            {
                return Adapter.GetAttributeViReal64(channelName, IviFgenAttribute.IVIFGEN_ATTR_STOP_TRIGGER_THRESHOLD);
            }

            public void SendSoftwareTrigger()
            {
                Adapter.ViSessionStatusCheck(IviFgenInterop.SendSoftwareStopTrigger(Adapter.Session));
            }

            public void SetDelay(string channelName, Ivi.Driver.PrecisionTimeSpan delay)
            {
                Adapter.SetAttributeViReal64(channelName, IviFgenAttribute.IVIFGEN_ATTR_STOP_TRIGGER_DELAY, delay.TotalSeconds);
            }

            public void SetSlope(string channelName, TriggerSlope slope)
            {
                Adapter.SetAttributeViInt32(channelName, IviFgenAttribute.IVIFGEN_ATTR_STOP_TRIGGER_SLOPE, FgenTriggerSlope.getC_Value(slope));
            }

            public void SetSource(string channelName, string source)
            {
                Adapter.SetAttributeViString(channelName, IviFgenAttribute.IVIFGEN_ATTR_STOP_TRIGGER_SOURCE, source);
            }

            public void SetThreshold(string channelName, double threshold)
            {
                Adapter.SetAttributeViReal64(channelName, IviFgenAttribute.IVIFGEN_ATTR_STOP_TRIGGER_THRESHOLD, threshold);
            }
        }
    
    }
}
