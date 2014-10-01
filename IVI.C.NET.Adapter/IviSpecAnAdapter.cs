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
using Ivi.SpecAn;
using IVI.C.NET.Adapter.IviCInterop;
using System.Runtime.InteropServices;

namespace IVI.C.NET.Adapter
{
    public class IviSpecAnAdapter : DriverAdapterBase<IviCInterop.IviSpecAn>, IIviSpecAn
    {
        #region Enum Mapping

        private static IviEnumCMapping<DetectorType, int> SpecAnDetectorType = IviEnumCMapping<DetectorType, int>.Instance
            .Map(DetectorType.AutoPeak, IviSpecAnAttribute.IVISPECAN_VAL_DETECTOR_TYPE_AUTO_PEAK)
            .Map(DetectorType.Average, IviSpecAnAttribute.IVISPECAN_VAL_DETECTOR_TYPE_AVERAGE)
            .Map(DetectorType.MaxPeak, IviSpecAnAttribute.IVISPECAN_VAL_DETECTOR_TYPE_MAX_PEAK)
            .Map(DetectorType.MinPeak, IviSpecAnAttribute.IVISPECAN_VAL_DETECTOR_TYPE_MIN_PEAK)
            .Map(DetectorType.Sample, IviSpecAnAttribute.IVISPECAN_VAL_DETECTOR_TYPE_SAMPLE)
            .Map(DetectorType.Rms, IviSpecAnAttribute.IVISPECAN_VAL_DETECTOR_TYPE_RMS);

        private static IviEnumCMapping<VerticalScale, int> SpecAnVerticalScale = IviEnumCMapping<VerticalScale, int>.Instance
            .Map(VerticalScale.Linear, IviSpecAnAttribute.IVISPECAN_VAL_VERTICAL_SCALE_LINEAR)
            .Map(VerticalScale.Logarithmic, IviSpecAnAttribute.IVISPECAN_VAL_VERTICAL_SCALE_LOGARITHMIC);

        private static IviEnumCMapping<AmplitudeUnits, int> SpecAnAmplitudeUnits = IviEnumCMapping<AmplitudeUnits, int>.Instance
            .Map(AmplitudeUnits.dBm, IviSpecAnAttribute.IVISPECAN_VAL_AMPLITUDE_UNITS_DBM)
            .Map(AmplitudeUnits.dBmV, IviSpecAnAttribute.IVISPECAN_VAL_AMPLITUDE_UNITS_DBMV)
            .Map(AmplitudeUnits.dBuV, IviSpecAnAttribute.IVISPECAN_VAL_AMPLITUDE_UNITS_DBUV)
            .Map(AmplitudeUnits.Volt, IviSpecAnAttribute.IVISPECAN_VAL_AMPLITUDE_UNITS_VOLT)
            .Map(AmplitudeUnits.Watt, IviSpecAnAttribute.IVISPECAN_VAL_AMPLITUDE_UNITS_WATT);

        private static IviEnumCMapping<MarkerSearch, int> SpecAnMarkerSearch = IviEnumCMapping<MarkerSearch, int>.Instance
            .Map(MarkerSearch.Highest, IviSpecAnAttribute.IVISPECAN_VAL_MARKER_SEARCH_HIGHEST)
            .Map(MarkerSearch.Minimum, IviSpecAnAttribute.IVISPECAN_VAL_MARKER_SEARCH_MINIMUM)
            .Map(MarkerSearch.NextPeak, IviSpecAnAttribute.IVISPECAN_VAL_MARKER_SEARCH_NEXT_PEAK )
            .Map(MarkerSearch.NextPeakLeft, IviSpecAnAttribute.IVISPECAN_VAL_MARKER_SEARCH_NEXT_PEAK_LEFT)
            .Map(MarkerSearch.NextPeakRight, IviSpecAnAttribute.IVISPECAN_VAL_MARKER_SEARCH_NEXT_PEAK_RIGHT );

        private static IviEnumCMapping<InstrumentSetting, int> SpecAnInstrumentSetting = IviEnumCMapping<InstrumentSetting, int>.Instance
            .Map(InstrumentSetting.FrequencyCenter, IviSpecAnAttribute.IVISPECAN_VAL_INSTRUMENT_SETTING_FREQUENCY_CENTER )
            .Map(InstrumentSetting.FrequencySpan, IviSpecAnAttribute.IVISPECAN_VAL_INSTRUMENT_SETTING_FREQUENCY_SPAN )
            .Map(InstrumentSetting.FrequencyStart, IviSpecAnAttribute.IVISPECAN_VAL_INSTRUMENT_SETTING_FREQUENCY_START )
            .Map(InstrumentSetting.FrequencyStop, IviSpecAnAttribute.IVISPECAN_VAL_INSTRUMENT_SETTING_FREQUENCY_STOP)
            .Map(InstrumentSetting.ReferenceLevel, IviSpecAnAttribute.IVISPECAN_VAL_INSTRUMENT_SETTING_REFERENCE_LEVEL );

        private static IviEnumCMapping<MarkerType, int> SpecAnMarkerType = IviEnumCMapping<MarkerType, int>.Instance
            .Map(MarkerType.Normal, IviSpecAnAttribute.IVISPECAN_VAL_MARKER_TYPE_NORMAL)
            .Map(MarkerType.Delta, IviSpecAnAttribute.IVISPECAN_VAL_MARKER_TYPE_DELTA);

        private static IviEnumCMapping<Slope, int> SpecAnExternalSlope = IviEnumCMapping<Slope, int>.Instance
            .Map(Slope.Positive, IviSpecAnAttribute.IVISPECAN_VAL_EXTERNAL_TRIGGER_SLOPE_POSITIVE)
            .Map(Slope.Negative, IviSpecAnAttribute.IVISPECAN_VAL_EXTERNAL_TRIGGER_SLOPE_NEGATIVE);

        private static IviEnumCMapping<Slope, int> SpecAnVideoSlope = IviEnumCMapping<Slope, int>.Instance
            .Map(Slope.Positive, IviSpecAnAttribute.IVISPECAN_VAL_VIDEO_TRIGGER_SLOPE_POSITIVE)
            .Map(Slope.Negative, IviSpecAnAttribute.IVISPECAN_VAL_VIDEO_TRIGGER_SLOPE_NEGATIVE);

        private static IviEnumCMapping<string, int> SpecAnTriggerSource = IviEnumCMapping<string, int>.Instance
            .Map("External", IviSpecAnAttribute.IVISPECAN_VAL_TRIGGER_SOURCE_EXTERNAL)
            .Map("Immediate", IviSpecAnAttribute.IVISPECAN_VAL_TRIGGER_SOURCE_IMMEDIATE)
            .Map("Software", IviSpecAnAttribute.IVISPECAN_VAL_TRIGGER_SOURCE_SOFTWARE)
            .Map("ACLine", IviSpecAnAttribute.IVISPECAN_VAL_TRIGGER_SOURCE_AC_LINE)
            .Map("Video", IviSpecAnAttribute.IVISPECAN_VAL_TRIGGER_SOURCE_VIDEO);

        private static IviEnumCMapping<AcquisitionStatus, int> SpecAnAcquisitionStatus = IviEnumCMapping<AcquisitionStatus, int>.Instance
            .Map(AcquisitionStatus.Complete, IviSpecAnAttribute.IVISPECAN_VAL_ACQUISITION_STATUS_COMPLETE)
            .Map(AcquisitionStatus.InProgress, IviSpecAnAttribute.IVISPECAN_VAL_ACQUISITION_STATUS_IN_PROGRESS )
            .Map(AcquisitionStatus.Unknown, IviSpecAnAttribute.IVISPECAN_VAL_ACQUISITION_STATUS_UNKNOWN);

        private static IviEnumCMapping<TraceType, int> SpecAnTraceType = IviEnumCMapping<TraceType, int>.Instance
            .Map(TraceType.ClearWrite, IviSpecAnAttribute.IVISPECAN_VAL_TRACE_TYPE_CLEAR_WRITE)
            .Map(TraceType.MaxHold, IviSpecAnAttribute.IVISPECAN_VAL_TRACE_TYPE_MAX_HOLD)
            .Map(TraceType.MinHold, IviSpecAnAttribute.IVISPECAN_VAL_TRACE_TYPE_MIN_HOLD)
            .Map(TraceType.VideoAverage, IviSpecAnAttribute.IVISPECAN_VAL_TRACE_TYPE_VIDEO_AVERAGE)
            .Map(TraceType.View, IviSpecAnAttribute.IVISPECAN_VAL_TRACE_TYPE_VIEW)
            .Map(TraceType.Store, IviSpecAnAttribute.IVISPECAN_VAL_TRACE_TYPE_STORE);

        #endregion

        private IIviSpecAnAcquisition SpecAnAcquisition = null;
        private IIviSpecAnDisplay SpecAnDisplay = null;
        private IIviSpecAnExternalMixer SpecAnExternalMixer = null;
        private IIviSpecAnFrequency SpecAnFrequency = null;
        private IIviSpecAnLevel SpecAnLevel = null;
        private IIviSpecAnMarker SpecAnMarker = null;
        private IIviSpecAnPreselector SpecAnPreselector = null;
        private IIviSpecAnSweepCoupling SpecAnSweepCoupling = null;
        private IIviSpecAnTrigger SpecAnTrigger = null;
        private IIviSpecAnTraceCollection SpecAnTraceCollection = null;
        public IviSpecAnAdapter(string name, bool idQuery, bool reset, string options)
            : base(name, idQuery, reset, options)
        {
            SpecAnAcquisition = new IviSpecAnAcquisition(this);
            SpecAnDisplay = new IviSpecAnDisplay(this);
            SpecAnExternalMixer = new IviSpecAnExternalMixer(this);
            SpecAnFrequency = new IviSpecAnFrequency(this);
            SpecAnLevel = new IviSpecAnLevel(this);
            SpecAnMarker = new IviSpecAnMarker(this);
            SpecAnPreselector = new IviSpecAnPreselector(this);
            SpecAnSweepCoupling = new IviSpecAnSweepCoupling(this);
            SpecAnTrigger = new IviSpecAnTrigger(this);
            SpecAnTraceCollection = new IviSpecAnTraceCollection(this);
        }

        public IIviSpecAnAcquisition Acquisition
        {
            get { return SpecAnAcquisition; }
        }

        public IIviSpecAnDisplay Display
        {
            get { return SpecAnDisplay; }
        }

        public IIviSpecAnExternalMixer ExternalMixer
        {
            get { return SpecAnExternalMixer; }
        }

        public IIviSpecAnFrequency Frequency
        {
            get { return SpecAnFrequency; }
        }

        public IIviSpecAnLevel Level
        {
            get { return SpecAnLevel; }
        }

        public IIviSpecAnMarker Marker
        {
            get { return SpecAnMarker; }
        }

        public IIviSpecAnPreselector Preselector
        {
            get { return SpecAnPreselector; }
        }

        public IIviSpecAnSweepCoupling SweepCoupling
        {
            get { return SpecAnSweepCoupling; }
        }

        public IIviSpecAnTraceCollection Traces
        {
            get { return SpecAnTraceCollection; }
        }

        public IIviSpecAnTrigger Trigger
        {
            get { return SpecAnTrigger; }
        }

        internal class IviSpecAnAcquisition : IIviSpecAnAcquisition
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviSpecAn IviSpecAnInterop = null;
            public IviSpecAnAcquisition(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviSpecAnInterop = (IviCInterop.IviSpecAn)Adapter.Interop;
            }

            public void Configure(bool sweepModeContinuous, int numberOfSweeps, DetectorType detectorType, VerticalScale verticalScale)
            {
                Adapter.ViSessionStatusCheck(IviSpecAnInterop.ConfigureAcquisition(Adapter.Session, sweepModeContinuous, numberOfSweeps, false, SpecAnDetectorType.getC_Value(detectorType), SpecAnVerticalScale.getC_Value(verticalScale)));
            }

            public void Configure(bool sweepModeContinuous, int numberOfSweeps, bool detectorTypeAuto, VerticalScale verticalScale)
            {
                Adapter.ViSessionStatusCheck(IviSpecAnInterop.ConfigureAcquisition(Adapter.Session, sweepModeContinuous, numberOfSweeps, detectorTypeAuto, SpecAnDetectorType.getC_Value(DetectorType), SpecAnVerticalScale.getC_Value(verticalScale)));
            }

            public DetectorType DetectorType
            {
                get
                {
                    return SpecAnDetectorType.getEnum(Adapter.GetAttributeViInt32(IviSpecAnAttribute.IVISPECAN_ATTR_DETECTOR_TYPE));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviSpecAnAttribute.IVISPECAN_ATTR_DETECTOR_TYPE, SpecAnDetectorType.getC_Value(value));
                }
            }

            public bool DetectorTypeAuto
            {
                get
                {
                    return Adapter.GetAttributeViBoolean(IviSpecAnAttribute.IVISPECAN_ATTR_DETECTOR_TYPE_AUTO);
                }
                set
                {
                    Adapter.SetAttributeViBoolean(IviSpecAnAttribute.IVISPECAN_ATTR_DETECTOR_TYPE_AUTO, value);
                }
            }

            public int NumberOfSweeps
            {
                get
                {
                    return Adapter.GetAttributeViInt32(IviSpecAnAttribute.IVISPECAN_ATTR_NUMBER_OF_SWEEPS);
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviSpecAnAttribute.IVISPECAN_ATTR_NUMBER_OF_SWEEPS, value);
                }
            }

            public bool SweepModeContinuous
            {
                get
                {
                    return Adapter.GetAttributeViBoolean(IviSpecAnAttribute.IVISPECAN_ATTR_SWEEP_MODE_CONTINUOUS);
                }
                set
                {
                    Adapter.SetAttributeViBoolean(IviSpecAnAttribute.IVISPECAN_ATTR_SWEEP_MODE_CONTINUOUS, value);
                }
            }

            public VerticalScale VerticalScale
            {
                get
                {
                    return SpecAnVerticalScale.getEnum(Adapter.GetAttributeViInt32(IviSpecAnAttribute.IVISPECAN_ATTR_VERTICAL_SCALE));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviSpecAnAttribute.IVISPECAN_ATTR_VERTICAL_SCALE, SpecAnVerticalScale.getC_Value(value));
                }
            }
        }

        internal class IviSpecAnDisplay : IIviSpecAnDisplay
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviSpecAn IviSpecAnInterop = null;
            public IviSpecAnDisplay(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviSpecAnInterop = (IviCInterop.IviSpecAn)Adapter.Interop;
            }

            public int NumberOfDivisions
            {
                get { return Adapter.GetAttributeViInt32(IviSpecAnAttribute.IVISPECAN_ATTR_NUMBER_OF_DIVISIONS); }
            }

            public double UnitsPerDivision
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviSpecAnAttribute.IVISPECAN_ATTR_UNITS_PER_DIVISION);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviSpecAnAttribute.IVISPECAN_ATTR_UNITS_PER_DIVISION, value);
                }
            }
        }

        internal class IviSpecAnExternalMixer : IIviSpecAnExternalMixer
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviSpecAn IviSpecAnInterop = null;
            private IIviSpecAnExternalMixerBias SpecAnExternalMixerBias = null;
            private IIviSpecAnExternalMixerConversionLossTable SpecAnExternalMixerConversionLossTable = null;
            public IviSpecAnExternalMixer(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviSpecAnInterop = (IviCInterop.IviSpecAn)Adapter.Interop;
                SpecAnExternalMixerBias = new IviSpecAnExternalMixerBias(Adapter);
                SpecAnExternalMixerConversionLossTable = new IviSpecAnExternalMixerConversionLossTable(Adapter);
            }

            public double AverageConversionLoss
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviSpecAnAttribute.IVISPECAN_ATTR_EXTERNAL_MIXER_AVERAGE_CONVERSION_LOSS);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviSpecAnAttribute.IVISPECAN_ATTR_EXTERNAL_MIXER_AVERAGE_CONVERSION_LOSS, value);
                }
            }

            public IIviSpecAnExternalMixerBias Bias
            {
                get { return SpecAnExternalMixerBias; }
            }

            public void Configure(int harmonic, double averageConversionLoss)
            {
                Adapter.ViSessionStatusCheck(IviSpecAnInterop.ConfigureExternalMixer(Adapter.Session, harmonic, averageConversionLoss));
            }

            public IIviSpecAnExternalMixerConversionLossTable ConversionLossTable
            {
                get { return SpecAnExternalMixerConversionLossTable; }
            }

            public bool Enabled
            {
                get
                {
                    return Adapter.GetAttributeViBoolean(IviSpecAnAttribute.IVISPECAN_ATTR_EXTERNAL_MIXER_ENABLED);
                }
                set
                {
                    Adapter.SetAttributeViBoolean(IviSpecAnAttribute.IVISPECAN_ATTR_EXTERNAL_MIXER_ENABLED, value);
                }
            }

            public int Harmonic
            {
                get
                {
                    return Adapter.GetAttributeViInt32(IviSpecAnAttribute.IVISPECAN_ATTR_EXTERNAL_MIXER_HARMONIC);
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviSpecAnAttribute.IVISPECAN_ATTR_EXTERNAL_MIXER_HARMONIC, value);
                }
            }

            public int NumberOfPorts
            {
                get
                {
                    return Adapter.GetAttributeViInt32(IviSpecAnAttribute.IVISPECAN_ATTR_EXTERNAL_MIXER_NUMBER_OF_PORTS);
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviSpecAnAttribute.IVISPECAN_ATTR_EXTERNAL_MIXER_NUMBER_OF_PORTS, value);
                }
            }
        }

        internal class IviSpecAnExternalMixerBias : IIviSpecAnExternalMixerBias
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviSpecAn IviSpecAnInterop = null;
            public IviSpecAnExternalMixerBias(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviSpecAnInterop = (IviCInterop.IviSpecAn)Adapter.Interop;
            }

            public void Configure(double bias, double biasLimit)
            {
                Adapter.ViSessionStatusCheck(IviSpecAnInterop.ConfigureExternalMixerBias(Adapter.Session, bias, biasLimit));
            }

            public bool Enabled
            {
                get
                {
                    return Adapter.GetAttributeViBoolean(IviSpecAnAttribute.IVISPECAN_ATTR_EXTERNAL_MIXER_BIAS_ENABLED);
                }
                set
                {
                    Adapter.SetAttributeViBoolean(IviSpecAnAttribute.IVISPECAN_ATTR_EXTERNAL_MIXER_BIAS_ENABLED, value);
                }
            }

            public double Level
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviSpecAnAttribute.IVISPECAN_ATTR_EXTERNAL_MIXER_BIAS);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviSpecAnAttribute.IVISPECAN_ATTR_EXTERNAL_MIXER_BIAS, value);
                }
            }

            public double Limit
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviSpecAnAttribute.IVISPECAN_ATTR_EXTERNAL_MIXER_BIAS_LIMIT);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviSpecAnAttribute.IVISPECAN_ATTR_EXTERNAL_MIXER_BIAS_LIMIT, value);
                }
            }
        }

        internal class IviSpecAnExternalMixerConversionLossTable : IIviSpecAnExternalMixerConversionLossTable
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviSpecAn IviSpecAnInterop = null;
            public IviSpecAnExternalMixerConversionLossTable(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviSpecAnInterop = (IviCInterop.IviSpecAn)Adapter.Interop;
            }

            public void Configure(double[] frequency, double[] conversionLoss)
            {
                Adapter.ViSessionStatusCheck(IviSpecAnInterop.ConfigureConversionLossTable(Adapter.Session, frequency.Length, frequency, conversionLoss));
            }

            public bool Enabled
            {
                get
                {
                    return Adapter.GetAttributeViBoolean(IviSpecAnAttribute.IVISPECAN_ATTR_EXTERNAL_MIXER_CONVERSION_LOSS_TABLE_ENABLED);
                }
                set
                {
                    Adapter.SetAttributeViBoolean(IviSpecAnAttribute.IVISPECAN_ATTR_EXTERNAL_MIXER_CONVERSION_LOSS_TABLE_ENABLED, value);
                }
            }
        }

        internal class IviSpecAnFrequency : IIviSpecAnFrequency
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviSpecAn IviSpecAnInterop = null;
            public IviSpecAnFrequency(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviSpecAnInterop = (IviCInterop.IviSpecAn)Adapter.Interop;
            }

            public void ConfigureCenterSpan(double centerFrequency, double span)
            {
                Adapter.ViSessionStatusCheck(IviSpecAnInterop.ConfigureFrequencyCenterSpan(Adapter.Session, centerFrequency, span));
            }

            public void ConfigureStartStop(double startFrequency, double stopFrequency)
            {
                Adapter.ViSessionStatusCheck(IviSpecAnInterop.ConfigureFrequencyStartStop(Adapter.Session, startFrequency, stopFrequency));
            }

            public double Offset
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviSpecAnAttribute.IVISPECAN_ATTR_FREQUENCY_OFFSET);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviSpecAnAttribute.IVISPECAN_ATTR_FREQUENCY_OFFSET, value);
                }
            }

            public double Start
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviSpecAnAttribute.IVISPECAN_ATTR_FREQUENCY_START);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviSpecAnAttribute.IVISPECAN_ATTR_FREQUENCY_START, value);
                }
            }

            public double Stop
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviSpecAnAttribute.IVISPECAN_ATTR_FREQUENCY_STOP);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviSpecAnAttribute.IVISPECAN_ATTR_FREQUENCY_STOP, value);
                }
            }
        }

        internal class IviSpecAnLevel : IIviSpecAnLevel
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviSpecAn IviSpecAnInterop = null;
            public IviSpecAnLevel(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviSpecAnInterop = (IviCInterop.IviSpecAn)Adapter.Interop;
            }

            public AmplitudeUnits AmplitudeUnits
            {
                get
                {
                    return SpecAnAmplitudeUnits.getEnum(Adapter.GetAttributeViInt32(IviSpecAnAttribute.IVISPECAN_ATTR_AMPLITUDE_UNITS));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviSpecAnAttribute.IVISPECAN_ATTR_AMPLITUDE_UNITS, SpecAnAmplitudeUnits.getC_Value(value));
                }
            }

            public double Attenuation
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviSpecAnAttribute.IVISPECAN_ATTR_ATTENUATION);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviSpecAnAttribute.IVISPECAN_ATTR_ATTENUATION, value);
                }
            }

            public bool AttenuationAuto
            {
                get
                {
                    return Adapter.GetAttributeViBoolean(IviSpecAnAttribute.IVISPECAN_ATTR_ATTENUATION_AUTO);
                }
                set
                {
                    Adapter.SetAttributeViBoolean(IviSpecAnAttribute.IVISPECAN_ATTR_ATTENUATION_AUTO, value);
                }
            }

            public void Configure(AmplitudeUnits amplitudeUnits, double inputImpedance, double referenceLevel, double referenceLevelOffset, double attenuation)
            {
                Adapter.ViSessionStatusCheck(IviSpecAnInterop.ConfigureLevel(Adapter.Session, SpecAnAmplitudeUnits.getC_Value(amplitudeUnits), inputImpedance, referenceLevel, referenceLevelOffset, false, attenuation));
            }

            public void Configure(AmplitudeUnits amplitudeUnits, double inputImpedance, double referenceLevel, double referenceLevelOffset, bool attenuationAuto)
            {
                Adapter.ViSessionStatusCheck(IviSpecAnInterop.ConfigureLevel(Adapter.Session, SpecAnAmplitudeUnits.getC_Value(amplitudeUnits), inputImpedance, referenceLevel, referenceLevelOffset, attenuationAuto, Attenuation));
            }

            public double InputImpedance
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviSpecAnAttribute.IVISPECAN_ATTR_INPUT_IMPEDANCE);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviSpecAnAttribute.IVISPECAN_ATTR_INPUT_IMPEDANCE, value);
                }
            }

            public double Reference
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviSpecAnAttribute.IVISPECAN_ATTR_REFERENCE_LEVEL);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviSpecAnAttribute.IVISPECAN_ATTR_REFERENCE_LEVEL, value);
                }
            }

            public double ReferenceOffset
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviSpecAnAttribute.IVISPECAN_ATTR_REFERENCE_LEVEL_OFFSET);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviSpecAnAttribute.IVISPECAN_ATTR_REFERENCE_LEVEL_OFFSET, value);
                }
            }
        }

        internal class IviSpecAnMarker : IIviSpecAnMarker
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviSpecAn IviSpecAnInterop = null;
            private IIviSpecAnMarkerFrequencyCounter SpecAnMarkerFrequencyCounter = null;
            public IviSpecAnMarker(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviSpecAnInterop = (IviCInterop.IviSpecAn)Adapter.Interop;
                SpecAnMarkerFrequencyCounter = new IviSpecAnMarkerFrequencyCounter(Adapter);
            }

            public string ActiveMarker
            {
                get
                {
                    return Adapter.GetAttributeViString(IviSpecAnAttribute.IVISPECAN_ATTR_ACTIVE_MARKER);
                }
                set
                {
                    Adapter.SetAttributeViString(IviSpecAnAttribute.IVISPECAN_ATTR_ACTIVE_MARKER, value);
                }
            }

            public double Amplitude
            {
                get { return Adapter.GetAttributeViReal64(IviSpecAnAttribute.IVISPECAN_ATTR_MARKER_AMPLITUDE); }
            }

            public void ConfigureEnabled(bool enabled, string markerTraceName)
            {
                Adapter.ViSessionStatusCheck(IviSpecAnInterop.ConfigureMarkerEnabled(Adapter.Session, enabled, markerTraceName));
            }

            public void ConfigureSearch(double peakExcursion, double markerThreshold)
            {
                Adapter.ViSessionStatusCheck(IviSpecAnInterop.ConfigureMarkerSearch(Adapter.Session, peakExcursion, markerThreshold));
            }

            public int Count
            {
                get { return Adapter.GetAttributeViInt32(IviSpecAnAttribute.IVISPECAN_ATTR_MARKER_COUNT); }
            }

            public void DisableAll()
            {
                Adapter.ViSessionStatusCheck(IviSpecAnInterop.DisableAllMarkers(Adapter.Session));
            }

            public bool Enabled
            {
                get
                {
                    return Adapter.GetAttributeViBoolean(IviSpecAnAttribute.IVISPECAN_ATTR_MARKER_ENABLED);
                }
                set
                {
                    Adapter.SetAttributeViBoolean(IviSpecAnAttribute.IVISPECAN_ATTR_MARKER_ENABLED, value);
                }
            }

            public IIviSpecAnMarkerFrequencyCounter FrequencyCounter
            {
                get { return SpecAnMarkerFrequencyCounter; }
            }

            public string GetName(int index)
            {
                StringBuilder Name = new StringBuilder(255);
                Adapter.ViSessionStatusCheck(IviSpecAnInterop.GetMarkerName(Adapter.Session, index, Name.Capacity, Name));
                return Name.ToString();
            }

            public void MakeDelta(bool deltaMarker)
            {
                Adapter.ViSessionStatusCheck(IviSpecAnInterop.MakeMarkerDelta(Adapter.Session, deltaMarker));
            }

            public double PeakExcursion
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviSpecAnAttribute.IVISPECAN_ATTR_PEAK_EXCURSION);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviSpecAnAttribute.IVISPECAN_ATTR_PEAK_EXCURSION, value);
                }
            }

            public double Position
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviSpecAnAttribute.IVISPECAN_ATTR_MARKER_POSITION);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviSpecAnAttribute.IVISPECAN_ATTR_MARKER_POSITION, value);
                }
            }

            public MarkerInfo Query()
            {
                double MarkerPosition = double.NaN;
                double MarkerAmplitude = double.NaN;
                Adapter.ViSessionStatusCheck(IviSpecAnInterop.QueryMarker(Adapter.Session, ref MarkerPosition, ref MarkerAmplitude));
                return new MarkerInfo(MarkerPosition, MarkerAmplitude);
            }

            public MarkerInfo QueryReference()
            {
                double MarkerPosition = double.NaN;
                double MarkerAmplitude = double.NaN;
                Adapter.ViSessionStatusCheck(IviSpecAnInterop.QueryReferenceMarker(Adapter.Session, ref MarkerPosition, ref MarkerAmplitude));
                return new MarkerInfo(MarkerPosition, MarkerAmplitude);
            }

            public double ReferenceAmplitude
            {
                get { return Adapter.GetAttributeViReal64(IviSpecAnAttribute.IVISPECAN_ATTR_REFERENCE_MARKER_AMPLITUDE); }
            }

            public double ReferencePosition
            {
                get { return Adapter.GetAttributeViReal64(IviSpecAnAttribute.IVISPECAN_ATTR_REFERENCE_MARKER_POSITION); }
            }

            public void Search(MarkerSearch searchType)
            {
                Adapter.ViSessionStatusCheck(IviSpecAnInterop.MarkerSearch(Adapter.Session, SpecAnMarkerSearch.getC_Value(searchType)));
            }

            public void SetInstrumentFromMarker(InstrumentSetting instrumentSetting)
            {
                Adapter.ViSessionStatusCheck(IviSpecAnInterop.SetInstrumentFromMarker(Adapter.Session, SpecAnInstrumentSetting.getC_Value(instrumentSetting)));
            }

            public bool SignalTrackEnabled
            {
                get
                {
                    return Adapter.GetAttributeViBoolean(IviSpecAnAttribute.IVISPECAN_ATTR_SIGNAL_TRACK_ENABLED);
                }
                set
                {
                    Adapter.SetAttributeViBoolean(IviSpecAnAttribute.IVISPECAN_ATTR_SIGNAL_TRACK_ENABLED, value);
                }
            }

            public double Threshold
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviSpecAnAttribute.IVISPECAN_ATTR_MARKER_THRESHOLD);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviSpecAnAttribute.IVISPECAN_ATTR_MARKER_THRESHOLD, value);
                }
            }

            public string Trace
            {
                get
                {
                    return Adapter.GetAttributeViString(IviSpecAnAttribute.IVISPECAN_ATTR_MARKER_TRACE);
                }
                set
                {
                    Adapter.SetAttributeViString(IviSpecAnAttribute.IVISPECAN_ATTR_MARKER_TRACE, value);
                }
            }

            public MarkerType Type
            {
                get { return SpecAnMarkerType.getEnum(Adapter.GetAttributeViInt32(IviSpecAnAttribute.IVISPECAN_ATTR_MARKER_TYPE)); }
            }
        }

        internal class IviSpecAnMarkerFrequencyCounter : IIviSpecAnMarkerFrequencyCounter
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviSpecAn IviSpecAnInterop = null;
            public IviSpecAnMarkerFrequencyCounter(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviSpecAnInterop = (IviCInterop.IviSpecAn)Adapter.Interop;
            }

            public void Configure(bool enabled, double resolution)
            {
                Adapter.ViSessionStatusCheck(IviSpecAnInterop.ConfigureMarkerFrequencyCounter(Adapter.Session, enabled, resolution));
            }

            public bool Enabled
            {
                get
                {
                    return Adapter.GetAttributeViBoolean(IviSpecAnAttribute.IVISPECAN_ATTR_MARKER_FREQUENCY_COUNTER_ENABLED);
                }
                set
                {
                    Adapter.SetAttributeViBoolean(IviSpecAnAttribute.IVISPECAN_ATTR_MARKER_FREQUENCY_COUNTER_ENABLED, value);
                }
            }

            public double Resolution
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviSpecAnAttribute.IVISPECAN_ATTR_MARKER_FREQUENCY_COUNTER_RESOLUTION);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviSpecAnAttribute.IVISPECAN_ATTR_MARKER_FREQUENCY_COUNTER_RESOLUTION, value);
                }
            }
        }

        internal class IviSpecAnPreselector : IIviSpecAnPreselector
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviSpecAn IviSpecAnInterop = null;
            public IviSpecAnPreselector(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviSpecAnInterop = (IviCInterop.IviSpecAn)Adapter.Interop;
            }

            public void Peak()
            {
                Adapter.ViSessionStatusCheck(IviSpecAnInterop.PeakPreselector(Adapter.Session));
            }
        }

        internal class IviSpecAnSweepCoupling : IIviSpecAnSweepCoupling
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviSpecAn IviSpecAnInterop = null;
            public IviSpecAnSweepCoupling(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviSpecAnInterop = (IviCInterop.IviSpecAn)Adapter.Interop;
            }

            public void Configure(double resolutionBandwidth, double videoBandwidth, Ivi.Driver.PrecisionTimeSpan sweepTime)
            {
                Adapter.ViSessionStatusCheck(IviSpecAnInterop.ConfigureSweepCoupling(Adapter.Session, false, resolutionBandwidth, false, videoBandwidth, false, sweepTime.TotalSeconds));
            }

            public void Configure(double resolutionBandwidth, double videoBandwidth, bool sweepTimeAuto)
            {
                Adapter.ViSessionStatusCheck(IviSpecAnInterop.ConfigureSweepCoupling(Adapter.Session, false, resolutionBandwidth, false, videoBandwidth, sweepTimeAuto, SweepTime.TotalSeconds));
            }

            public void Configure(double resolutionBandwidth, bool videoBandwidthAuto, Ivi.Driver.PrecisionTimeSpan sweepTime)
            {
                Adapter.ViSessionStatusCheck(IviSpecAnInterop.ConfigureSweepCoupling(Adapter.Session, false, resolutionBandwidth, videoBandwidthAuto, VideoBandwidth, false, sweepTime.TotalSeconds));
            }

            public void Configure(double resolutionBandwidth, bool videoBandwidthAuto, bool sweepTimeAuto)
            {
                Adapter.ViSessionStatusCheck(IviSpecAnInterop.ConfigureSweepCoupling(Adapter.Session, false, resolutionBandwidth, videoBandwidthAuto, VideoBandwidth, false, SweepTime.TotalSeconds));
            }

            public void Configure(bool resolutionBandwidthAuto, double videoBandwidth, Ivi.Driver.PrecisionTimeSpan sweepTime)
            {
                Adapter.ViSessionStatusCheck(IviSpecAnInterop.ConfigureSweepCoupling(Adapter.Session, resolutionBandwidthAuto, ResolutionBandwidth, false, videoBandwidth, false, sweepTime.TotalSeconds));
            }

            public void Configure(bool resolutionBandwidthAuto, double videoBandwidth, bool sweepTimeAuto)
            {
                Adapter.ViSessionStatusCheck(IviSpecAnInterop.ConfigureSweepCoupling(Adapter.Session, resolutionBandwidthAuto, ResolutionBandwidth, false, videoBandwidth, sweepTimeAuto, SweepTime.TotalSeconds));
            }

            public void Configure(bool resolutionBandwidthAuto, bool videoBandwidthAuto, Ivi.Driver.PrecisionTimeSpan sweepTime)
            {
                Adapter.ViSessionStatusCheck(IviSpecAnInterop.ConfigureSweepCoupling(Adapter.Session, resolutionBandwidthAuto, ResolutionBandwidth, videoBandwidthAuto, VideoBandwidth, false, sweepTime.TotalSeconds));
            }

            public void Configure(bool resolutionBandwidthAuto, bool videoBandwidthAuto, bool sweepTimeAuto)
            {
                Adapter.ViSessionStatusCheck(IviSpecAnInterop.ConfigureSweepCoupling(Adapter.Session, resolutionBandwidthAuto, ResolutionBandwidth, videoBandwidthAuto, VideoBandwidth, sweepTimeAuto, SweepTime.TotalSeconds));
            }

            public double ResolutionBandwidth
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviSpecAnAttribute.IVISPECAN_ATTR_RESOLUTION_BANDWIDTH);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviSpecAnAttribute.IVISPECAN_ATTR_RESOLUTION_BANDWIDTH, value);
                }
            }

            public bool ResolutionBandwidthAuto
            {
                get
                {
                    return Adapter.GetAttributeViBoolean(IviSpecAnAttribute.IVISPECAN_ATTR_RESOLUTION_BANDWIDTH_AUTO);
                }
                set
                {
                    Adapter.SetAttributeViBoolean(IviSpecAnAttribute.IVISPECAN_ATTR_RESOLUTION_BANDWIDTH_AUTO, value);
                }
            }

            public Ivi.Driver.PrecisionTimeSpan SweepTime
            {
                get
                {
                    return new Ivi.Driver.PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * Adapter.GetAttributeViReal64(IviSpecAnAttribute.IVISPECAN_ATTR_SWEEP_TIME))));
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviSpecAnAttribute.IVISPECAN_ATTR_SWEEP_TIME, value.TotalSeconds);
                }
            }

            public bool SweepTimeAuto
            {
                get
                {
                    return Adapter.GetAttributeViBoolean(IviSpecAnAttribute.IVISPECAN_ATTR_SWEEP_TIME_AUTO);
                }
                set
                {
                    Adapter.SetAttributeViBoolean(IviSpecAnAttribute.IVISPECAN_ATTR_SWEEP_TIME_AUTO, value);
                }
            }

            public double VideoBandwidth
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviSpecAnAttribute.IVISPECAN_ATTR_VIDEO_BANDWIDTH);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviSpecAnAttribute.IVISPECAN_ATTR_VIDEO_BANDWIDTH, value);
                }
            }

            public bool VideoBandwidthAuto
            {
                get
                {
                    return Adapter.GetAttributeViBoolean(IviSpecAnAttribute.IVISPECAN_ATTR_VIDEO_BANDWIDTH_AUTO);
                }
                set
                {
                    Adapter.SetAttributeViBoolean(IviSpecAnAttribute.IVISPECAN_ATTR_VIDEO_BANDWIDTH_AUTO, value);
                }
            }
        }

        internal class IviSpecAnTrigger : IIviSpecAnTrigger
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviSpecAn IviSpecAnInterop = null;
            private IIviSpecAnTriggerExternal SpecAnTriggerExternal = null;
            private IIviSpecAnTriggerVideo SpecAnTriggerVideo = null;
            public IviSpecAnTrigger(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviSpecAnInterop = (IviCInterop.IviSpecAn)Adapter.Interop;
                SpecAnTriggerExternal = new IviSpecAnTriggerExternal(Adapter);
                SpecAnTriggerVideo = new IviSpecAnTriggerVideo(Adapter);
            }

            public IIviSpecAnTriggerExternal External
            {
                get { return SpecAnTriggerExternal; }
            }

            public IIviSpecAnTriggerVideo Video
            {
                get { return SpecAnTriggerVideo; }
            }

            public string Source
            {
                get
                {
                    return SpecAnTriggerSource.getEnum(Adapter.GetAttributeViInt32(IviScopeAttribute.IVISCOPE_ATTR_TRIGGER_SOURCE));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviScopeAttribute.IVISCOPE_ATTR_TRIGGER_SOURCE, SpecAnTriggerSource.getC_Value(value));
                }
            }
        }

        internal class IviSpecAnTriggerExternal : IIviSpecAnTriggerExternal
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviSpecAn IviSpecAnInterop = null;
            public IviSpecAnTriggerExternal(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviSpecAnInterop = (IviCInterop.IviSpecAn)Adapter.Interop;
            }

            public void Configure(double externalTriggerLevel, Slope externalTriggerSlope)
            {
                Adapter.ViSessionStatusCheck(IviSpecAnInterop.ConfigureExternalTrigger(Adapter.Session, externalTriggerLevel, SpecAnExternalSlope.getC_Value(externalTriggerSlope)));
            }

            public double Level
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviSpecAnAttribute.IVISPECAN_ATTR_EXTERNAL_TRIGGER_LEVEL);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviSpecAnAttribute.IVISPECAN_ATTR_EXTERNAL_TRIGGER_LEVEL, value);
                }
            }

            public Slope Slope
            {
                get
                {
                    return SpecAnExternalSlope.getEnum(Adapter.GetAttributeViInt32(IviSpecAnAttribute.IVISPECAN_ATTR_EXTERNAL_TRIGGER_SLOPE));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviSpecAnAttribute.IVISPECAN_ATTR_EXTERNAL_TRIGGER_SLOPE, SpecAnExternalSlope.getC_Value(value));
                }
            }
        }

        internal class IviSpecAnTriggerVideo : IIviSpecAnTriggerVideo
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviSpecAn IviSpecAnInterop = null;
            public IviSpecAnTriggerVideo(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviSpecAnInterop = (IviCInterop.IviSpecAn)Adapter.Interop;
            }

            public void Configure(double videoTriggerLevel, Slope videoTriggerSlope)
            {
                Adapter.ViSessionStatusCheck(IviSpecAnInterop.ConfigureVideoTrigger(Adapter.Session, videoTriggerLevel, SpecAnVideoSlope.getC_Value(videoTriggerSlope)));
            }

            public double Level
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviSpecAnAttribute.IVISPECAN_ATTR_VIDEO_TRIGGER_LEVEL);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviSpecAnAttribute.IVISPECAN_ATTR_VIDEO_TRIGGER_LEVEL, value);
                }
            }

            public Slope Slope
            {
                get
                {
                    return SpecAnVideoSlope.getEnum(Adapter.GetAttributeViInt32(IviSpecAnAttribute.IVISPECAN_ATTR_VIDEO_TRIGGER_SLOPE));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviSpecAnAttribute.IVISPECAN_ATTR_VIDEO_TRIGGER_SLOPE, SpecAnVideoSlope.getC_Value(value));
                }
            }
        }

        internal class IviSpecAnTraceCollection : IIviSpecAnTraceCollection
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviSpecAn IviSpecAnInterop = null;
            private IList<IIviSpecAnTrace> Traces = null;
            private IList<string> TraceNames = null;
            private IIviSpecAnTracesMath SpecAnTracesMath = null;
            public IviSpecAnTraceCollection(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviSpecAnInterop = (IviCInterop.IviSpecAn)Adapter.Interop;

                int TraceCount = Adapter.GetAttributeViInt32(IviSpecAnAttribute.IVISPECAN_ATTR_TRACE_COUNT);

                Traces = new List<IIviSpecAnTrace>();
                TraceNames = new List<string>();

                for (int Index = 1; Index <= TraceCount; Index++)
                {
                    IIviSpecAnTrace Trace = new IviSpecAnTrace(Adapter, Index);
                    Traces.Add(Trace);
                    TraceNames.Add(Trace.Name);
                }

                SpecAnTracesMath = new IviSpecAnTracesMath(Adapter);
            }

            public int Count
            {
                get { return Traces.Count; }
            }

            public IIviSpecAnTrace this[string name]
            {
                get { return Traces[TraceNames.IndexOf(name)]; }
            }

            public System.Collections.Generic.IEnumerator<IIviSpecAnTrace> GetEnumerator()
            {
                return Traces.GetEnumerator();
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return Traces.GetEnumerator();
            }

            public void Abort()
            {
                Adapter.ViSessionStatusCheck(IviSpecAnInterop.Abort(Adapter.Session));
            }

            public AcquisitionStatus AcquisitionStatus()
            {
                int status = 0;
                Adapter.ViSessionStatusCheck(IviSpecAnInterop.AcquisitionStatus(Adapter.Session, ref status));
                return SpecAnAcquisitionStatus.getEnum(status);
            }

            public Ivi.Driver.ISpectrum<double> CreateSpectrum(int size)
            {
                double FrequenceyStart = Adapter.GetAttributeViReal64(IviSpecAnAttribute.IVISPECAN_ATTR_FREQUENCY_START);
                double FrequenceyStop = Adapter.GetAttributeViReal64(IviSpecAnAttribute.IVISPECAN_ATTR_FREQUENCY_STOP);
                return new Ivi.Driver.Spectrum<double>(FrequenceyStart, FrequenceyStop, (long)size); 
            }

            public void Initiate()
            {
                Adapter.ViSessionStatusCheck(IviSpecAnInterop.Initiate(Adapter.Session));
            }

            public IIviSpecAnTracesMath Math
            {
                get { return SpecAnTracesMath; }
            }

            public void SendSoftwareTrigger()
            {
                Adapter.ViSessionStatusCheck(IviSpecAnInterop.SendSoftwareTrigger(Adapter.Session));
            }
        }

        internal class IviSpecAnTracesMath : IIviSpecAnTracesMath
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviSpecAn IviSpecAnInterop = null;
            public IviSpecAnTracesMath(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviSpecAnInterop = (IviCInterop.IviSpecAn)Adapter.Interop;
            }

            public void Add(string destinationTrace, string trace1, string trace2)
            {
                Adapter.ViSessionStatusCheck(IviSpecAnInterop.AddTraces(Adapter.Session, destinationTrace, trace1, trace2));
            }

            public void Copy(string destinationTrace, string sourceTrace)
            {
                Adapter.ViSessionStatusCheck(IviSpecAnInterop.CopyTrace(Adapter.Session, destinationTrace, sourceTrace));
            }

            public void Exchange(string trace1, string trace2)
            {
                Adapter.ViSessionStatusCheck(IviSpecAnInterop.ExchangeTraces(Adapter.Session, trace1, trace2));
            }

            public void Subtract(string destinationTrace, string trace1, string trace2)
            {
                Adapter.ViSessionStatusCheck(IviSpecAnInterop.SubtractTraces(Adapter.Session, destinationTrace, trace1, trace2));
            }
        }

        internal class IviSpecAnTrace : IIviSpecAnTrace
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviSpecAn IviSpecAnInterop = null;
            private int Index;
            private string TraceName;
            public IviSpecAnTrace(IDriverAdapterBase Adapter, int Index)
            {
                this.Adapter = Adapter;
                IviSpecAnInterop = (IviCInterop.IviSpecAn)Adapter.Interop;
                this.Index = Index;

                try
                {
                    StringBuilder NameValue = new StringBuilder(256);
                    Adapter.ViSessionStatusCheck(IviSpecAnInterop.GetTraceName(Adapter.Session, Index, NameValue.Capacity, NameValue));
                    TraceName = NameValue.ToString();
                }
                catch
                {
                    TraceName = string.Empty;
                }
            }

            public Ivi.Driver.IWaveform<double> FetchY(Ivi.Driver.IWaveform<double> waveform)
            {
                IntPtr pAmplitude = Marshal.AllocHGlobal((int)(waveform.Capacity * sizeof(double)));
                int ActualPoints = 0;
                Adapter.ViSessionStatusCheck(IviSpecAnInterop.FetchYTrace(Adapter.Session, TraceName, (int)waveform.Capacity, ref ActualPoints, pAmplitude));
                double[] Amplitude = new double[ActualPoints];
                Marshal.Copy(pAmplitude, Amplitude, 0, ActualPoints);
                Marshal.FreeHGlobal(pAmplitude);

                waveform.ValidPointCount = ActualPoints;
                waveform.PutElements(0, Amplitude);
                return waveform;
            }

            public Ivi.Driver.ISpectrum<double> FetchY(Ivi.Driver.ISpectrum<double> spectrum)
            {
                IntPtr pAmplitude = Marshal.AllocHGlobal((int)(spectrum.Capacity * sizeof(double)));
                int ActualPoints = 0;
                Adapter.ViSessionStatusCheck(IviSpecAnInterop.FetchYTrace(Adapter.Session, TraceName, (int)spectrum.Capacity, ref ActualPoints, pAmplitude));
                double[] Amplitude = new double[ActualPoints];
                Marshal.Copy(pAmplitude, Amplitude, 0, ActualPoints);
                Marshal.FreeHGlobal(pAmplitude);

                double FrequenceyStart = Adapter.GetAttributeViReal64(IviSpecAnAttribute.IVISPECAN_ATTR_FREQUENCY_START);
                double FrequenceyStop = Adapter.GetAttributeViReal64(IviSpecAnAttribute.IVISPECAN_ATTR_FREQUENCY_STOP);
                spectrum.Configure(FrequenceyStart, FrequenceyStop, ActualPoints);
                spectrum.PutElements(0, Amplitude);
                return spectrum;
            }

            public Ivi.Driver.IWaveform<double> ReadY(Ivi.Driver.PrecisionTimeSpan maximumTime, Ivi.Driver.IWaveform<double> waveform)
            {
                IntPtr pAmplitude = Marshal.AllocHGlobal((int)(waveform.Capacity * sizeof(double)));
                int ActualPoints = 0;
                Adapter.ViSessionStatusCheck(IviSpecAnInterop.ReadYTrace(Adapter.Session, TraceName, (int)maximumTime.TotalMilliseconds, (int)waveform.Capacity, ref ActualPoints, pAmplitude));
                double[] Amplitude = new double[ActualPoints];
                Marshal.Copy(pAmplitude, Amplitude, 0, ActualPoints);
                Marshal.FreeHGlobal(pAmplitude);

                waveform.ValidPointCount = ActualPoints;
                waveform.PutElements(0, Amplitude);
                return waveform;
            }

            public Ivi.Driver.ISpectrum<double> ReadY(Ivi.Driver.PrecisionTimeSpan maximumTime, Ivi.Driver.ISpectrum<double> spectrum)
            {
                IntPtr pAmplitude = Marshal.AllocHGlobal((int)(spectrum.Capacity * sizeof(double)));
                int ActualPoints = 0;
                Adapter.ViSessionStatusCheck(IviSpecAnInterop.ReadYTrace(Adapter.Session, TraceName, (int)maximumTime.TotalMilliseconds, (int)spectrum.Capacity, ref ActualPoints, pAmplitude));
                double[] Amplitude = new double[ActualPoints];
                Marshal.Copy(pAmplitude, Amplitude, 0, ActualPoints);
                Marshal.FreeHGlobal(pAmplitude);

                double FrequenceyStart = Adapter.GetAttributeViReal64(IviSpecAnAttribute.IVISPECAN_ATTR_FREQUENCY_START);
                double FrequenceyStop = Adapter.GetAttributeViReal64(IviSpecAnAttribute.IVISPECAN_ATTR_FREQUENCY_STOP);
                spectrum.Configure(FrequenceyStart, FrequenceyStop, ActualPoints);
                spectrum.PutElements(0, Amplitude);
                return spectrum;
            }

            public TraceType Type
            {
                get
                {
                    return SpecAnTraceType.getEnum(Adapter.GetAttributeViInt32(TraceName, IviSpecAnAttribute.IVISPECAN_ATTR_TRACE_TYPE));
                }
                set
                {
                    Adapter.SetAttributeViInt32(TraceName, IviSpecAnAttribute.IVISPECAN_ATTR_TRACE_TYPE, SpecAnTraceType.getC_Value(value));
                }
            }

            public string Name
            {
                get { return TraceName; }
            }
        }
    }
}
