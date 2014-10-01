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
using System.Text;
using Ivi.Driver;
using Ivi.Downconverter;
using IVI.C.NET.Adapter.IviCInterop;
using System.Runtime.InteropServices;

namespace IVI.C.NET.Adapter
{
    public class IviDownconverterAdapter : DriverAdapterBase<IviCInterop.IviDownconverter>, IIviDownconverter
    {
        #region Enum Mapping

        private static IviEnumCMapping<CalibratedStatus, int> DownconverterCalibratedStatus = IviEnumCMapping<CalibratedStatus, int>.Instance
            .Map(CalibratedStatus.Calibrated, IviDownconverterAttribute.IVIDOWNCONVERTER_VAL_CALIBRATED)
            .Map(CalibratedStatus.Uncalibrated, IviDownconverterAttribute.IVIDOWNCONVERTER_VAL_UNCALIBRATED)
            .Map(CalibratedStatus.Unknown, IviDownconverterAttribute.IVIDOWNCONVERTER_VAL_CALIBRATED_STATUS_UNKNOWN);

        private static IviEnumCMapping<CalibrationStatus, int> DownconverterCalibrationStatus = IviEnumCMapping<CalibrationStatus, int>.Instance
            .Map(CalibrationStatus.Complete, IviDownconverterAttribute.IVIDOWNCONVERTER_VAL_CALIBRATION_COMPLETE)
            .Map(CalibrationStatus.InProgress, IviDownconverterAttribute.IVIDOWNCONVERTER_VAL_CALIBRATION_IN_PROGRESS)
            .Map(CalibrationStatus.Unknown, IviDownconverterAttribute.IVIDOWNCONVERTER_VAL_CALIBRATION_STATUS_UNKNOWN)
            .Map(CalibrationStatus.Failed, IviDownconverterAttribute.IVIDOWNCONVERTER_VAL_CALIBRATION_FAILED);

        private static IviEnumCMapping<InputCoupling, int> DownconverterInputCoupling = IviEnumCMapping<InputCoupling, int>.Instance
            .Map(InputCoupling.AC, IviDownconverterAttribute.IVIDOWNCONVERTER_VAL_INPUT_COUPLING_AC)
            .Map(InputCoupling.DC, IviDownconverterAttribute.IVIDOWNCONVERTER_VAL_INPUT_COUPLING_DC);

        private static IviEnumCMapping<FrequencySweepMode, int> DownconverterFrequencySweepMode = IviEnumCMapping<FrequencySweepMode, int>.Instance
            .Map(FrequencySweepMode.None, IviDownconverterAttribute.IVIDOWNCONVERTER_VAL_FREQUENCY_SWEEP_MODE_NONE)
            .Map(FrequencySweepMode.Sweep, IviDownconverterAttribute.IVIDOWNCONVERTER_VAL_FREQUENCY_SWEEP_MODE_SWEEP)
            .Map(FrequencySweepMode.Step, IviDownconverterAttribute.IVIDOWNCONVERTER_VAL_FREQUENCY_SWEEP_MODE_STEP)
            .Map(FrequencySweepMode.List, IviDownconverterAttribute.IVIDOWNCONVERTER_VAL_FREQUENCY_SWEEP_MODE_LIST);

        private static IviEnumCMapping<FrequencyStepScaling, int> DownconverterFrequencyStepScaling = IviEnumCMapping<FrequencyStepScaling, int>.Instance
            .Map(FrequencyStepScaling.Linear, IviDownconverterAttribute.IVIDOWNCONVERTER_VAL_FREQUENCY_STEP_SCALING_LINEAR)
            .Map(FrequencyStepScaling.Logarithmic, IviDownconverterAttribute.IVIDOWNCONVERTER_VAL_FREQUENCY_STEP_SCALING_LOGARITHMIC);

        private static IviEnumCMapping<string, int> DownconverterReferenceOscillatorSource = IviEnumCMapping<string, int>.Instance
            .Map("Internal", IviDownconverterAttribute.IVIDOWNCONVERTER_VAL_REFERENCE_OSCILLATOR_SOURCE_INTERNAL)
            .Map("External", IviDownconverterAttribute.IVIDOWNCONVERTER_VAL_REFERENCE_OSCILLATOR_SOURCE_EXTERNAL);

        #endregion

        private IIviDownconverterCalibration DownconverterCalibration = null;
        private IIviDownconverterExternalLO DownconverterExternalLO = null;
        private IIviDownconverterExternalMixer DownconverterExternalMixer = null;
        private IIviDownconverterIFOutput DownconverterIFOutput = null;
        private IIviDownconverterRFInput DownconverterRFInput = null;
        private IIviDownconverterReferenceOscillator DownconverterReferenceOscillator = null;
        public IviDownconverterAdapter(string name, bool idQuery, bool reset, string options)
            : base(name, idQuery, reset, options)
        {
            DownconverterCalibration = new IviDownconverterCalibration(this);
            DownconverterExternalLO = new IviDownconverterExternalLO(this);
            DownconverterExternalMixer = new IviDownconverterExternalMixer(this);
            DownconverterIFOutput = new IviDownconverterIFOutput(this);
            DownconverterRFInput = new IviDownconverterRFInput(this);
            DownconverterReferenceOscillator = new IviDownconverterReferenceOscillator(this);
        }

        public IIviDownconverterCalibration Calibration
        {
            get { return DownconverterCalibration; }
        }

        public IIviDownconverterExternalLO ExternalLO
        {
            get { return DownconverterExternalLO; }
        }

        public IIviDownconverterExternalMixer ExternalMixer
        {
            get { return DownconverterExternalMixer; }
        }

        public IIviDownconverterIFOutput IFOutput
        {
            get { return DownconverterIFOutput; }
        }

        public IIviDownconverterRFInput RFInput
        {
            get { return DownconverterRFInput; }
        }

        public IIviDownconverterReferenceOscillator ReferenceOscillator
        {
            get { return DownconverterReferenceOscillator; }
        }

        internal class IviDownconverterCalibration : IIviDownconverterCalibration
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviDownconverter IviDownconverterInterop = null;
            public IviDownconverterCalibration(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviDownconverterInterop = (IviCInterop.IviDownconverter)Adapter.Interop;
            }

            public void Calibrate()
            {
                Adapter.ViSessionStatusCheck(IviDownconverterInterop.Calibrate(Adapter.Session));
            }

            public CalibratedStatus GetCalibratedStatus()
            {
                int status = 0;
                Adapter.ViSessionStatusCheck(IviDownconverterInterop.IsCalibrated(Adapter.Session, ref status));
                return DownconverterCalibratedStatus.getEnum(status);
            }

            public CalibrationStatus GetCalibrationStatus()
            {
                int status = 0;
                Adapter.ViSessionStatusCheck(IviDownconverterInterop.IsCalibrationComplete(Adapter.Session, ref status));
                return DownconverterCalibrationStatus.getEnum(status);
            }
        }

        internal class IviDownconverterExternalLO : IIviDownconverterExternalLO
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviDownconverter IviDownconverterInterop = null;
            public IviDownconverterExternalLO(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviDownconverterInterop = (IviCInterop.IviDownconverter)Adapter.Interop;
            }

            public bool Enabled
            {
                get
                {
                    return Adapter.GetAttributeViBoolean(IviDownconverterAttribute.IVIDOWNCONVERTER_ATTR_EXTERNAL_LO_ENABLED);
                }
                set
                {
                    Adapter.SetAttributeViBoolean(IviDownconverterAttribute.IVIDOWNCONVERTER_ATTR_EXTERNAL_LO_ENABLED, value);
                }
            }

            public double Frequency
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviDownconverterAttribute.IVIDOWNCONVERTER_ATTR_EXTERNAL_LO_FREQUENCY);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviDownconverterAttribute.IVIDOWNCONVERTER_ATTR_EXTERNAL_LO_FREQUENCY, value);
                }
            }
        }

        internal class IviDownconverterExternalMixer : IIviDownconverterExternalMixer
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviDownconverter IviDownconverterInterop = null;
            private IIviDownconverterExternalMixerBias DownconverterExternalMixerBias = null;
            public IviDownconverterExternalMixer(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviDownconverterInterop = (IviCInterop.IviDownconverter)Adapter.Interop;
                DownconverterExternalMixerBias = new IviDownconverterExternalMixerBias(Adapter);
            }

            public IIviDownconverterExternalMixerBias Bias
            {
                get { return DownconverterExternalMixerBias; }
            }

            public bool Enabled
            {
                get
                {
                    return Adapter.GetAttributeViBoolean(IviDownconverterAttribute.IVIDOWNCONVERTER_ATTR_EXTERNAL_MIXER_ENABLED);
                }
                set
                {
                    Adapter.SetAttributeViBoolean(IviDownconverterAttribute.IVIDOWNCONVERTER_ATTR_EXTERNAL_MIXER_ENABLED, value);
                }
            }

            public int Harmonic
            {
                get
                {
                    return Adapter.GetAttributeViInt32(IviDownconverterAttribute.IVIDOWNCONVERTER_ATTR_EXTERNAL_MIXER_HARMONIC);
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviDownconverterAttribute.IVIDOWNCONVERTER_ATTR_EXTERNAL_MIXER_HARMONIC, value);
                }
            }

            public int NumberOfPorts
            {
                get
                {
                    return Adapter.GetAttributeViInt32(IviDownconverterAttribute.IVIDOWNCONVERTER_ATTR_EXTERNAL_MIXER_NUMBER_OF_PORTS);
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviDownconverterAttribute.IVIDOWNCONVERTER_ATTR_EXTERNAL_MIXER_NUMBER_OF_PORTS, value);
                }
            }
        }

        internal class IviDownconverterExternalMixerBias : IIviDownconverterExternalMixerBias
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviDownconverter IviDownconverterInterop = null;
            public IviDownconverterExternalMixerBias(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviDownconverterInterop = (IviCInterop.IviDownconverter)Adapter.Interop;
            }

            public void Configure(double bias, double biasLimit)
            {
                Adapter.ViSessionStatusCheck(IviDownconverterInterop.ConfigureExternalMixerBias(Adapter.Session, bias, biasLimit));
            }

            public bool Enabled
            {
                get
                {
                    return Adapter.GetAttributeViBoolean(IviDownconverterAttribute.IVIDOWNCONVERTER_ATTR_EXTERNAL_MIXER_BIAS_ENABLED);
                }
                set
                {
                    Adapter.SetAttributeViBoolean(IviDownconverterAttribute.IVIDOWNCONVERTER_ATTR_EXTERNAL_MIXER_BIAS_ENABLED, value);
                }
            }

            public double Level
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviDownconverterAttribute.IVIDOWNCONVERTER_ATTR_EXTERNAL_MIXER_BIAS_LEVEL);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviDownconverterAttribute.IVIDOWNCONVERTER_ATTR_EXTERNAL_MIXER_BIAS_LEVEL, value);
                }
            }

            public double Limit
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviDownconverterAttribute.IVIDOWNCONVERTER_ATTR_EXTERNAL_MIXER_BIAS_LIMIT);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviDownconverterAttribute.IVIDOWNCONVERTER_ATTR_EXTERNAL_MIXER_BIAS_LIMIT, value);
                }
            }
        }

        internal class IviDownconverterIFOutput : IIviDownconverterIFOutput
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviDownconverter IviDownconverterInterop = null;
            public IviDownconverterIFOutput(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviDownconverterInterop = (IviCInterop.IviDownconverter)Adapter.Interop;
            }

            public string ActiveIFOutput
            {
                get
                {
                    return Adapter.GetAttributeViString(IviDownconverterAttribute.IVIDOWNCONVERTER_ATTR_ACTIVE_IF_OUTPUT);
                }
                set
                {
                    Adapter.SetAttributeViString(IviDownconverterAttribute.IVIDOWNCONVERTER_ATTR_ACTIVE_IF_OUTPUT, value);
                }
            }

            public int Count
            {
                get { return Adapter.GetAttributeViInt32(IviDownconverterAttribute.IVIDOWNCONVERTER_ATTR_IF_OUTPUT_COUNT); }
            }

            public bool Enabled
            {
                get
                {
                    return Adapter.GetAttributeViBoolean(IviDownconverterAttribute.IVIDOWNCONVERTER_ATTR_IF_OUTPUT_ENABLED);
                }
                set
                {
                    Adapter.SetAttributeViBoolean(IviDownconverterAttribute.IVIDOWNCONVERTER_ATTR_IF_OUTPUT_ENABLED, value);
                }
            }

            public double FilterBandwidth
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviDownconverterAttribute.IVIDOWNCONVERTER_ATTR_IF_OUTPUT_FILTER_BANDWIDTH);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviDownconverterAttribute.IVIDOWNCONVERTER_ATTR_IF_OUTPUT_FILTER_BANDWIDTH, value);
                }
            }

            public double Frequency
            {
                get { return Adapter.GetAttributeViReal64(IviDownconverterAttribute.IVIDOWNCONVERTER_ATTR_IF_OUTPUT_FREQUENCY); }
            }

            public double Gain
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviDownconverterAttribute.IVIDOWNCONVERTER_ATTR_IF_OUTPUT_GAIN);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviDownconverterAttribute.IVIDOWNCONVERTER_ATTR_IF_OUTPUT_GAIN, value);
                }
            }

            public string GetName(int index)
            {
                StringBuilder Name = new StringBuilder(255);
                Adapter.ViSessionStatusCheck(IviDownconverterInterop.GetIFOutputName(Adapter.Session, index, Name.Capacity, Name));
                return Name.ToString();
            }

            public bool IsSettled
            {
                get { return Adapter.GetAttributeViBoolean(IviDownconverterAttribute.IVIDOWNCONVERTER_ATTR_IS_SETTLED); }
            }

            public double VideoDetectorBandwidth
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviDownconverterAttribute.IVIDOWNCONVERTER_ATTR_IF_OUTPUT_VIDEO_DETECTOR_BANDWIDTH);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviDownconverterAttribute.IVIDOWNCONVERTER_ATTR_IF_OUTPUT_VIDEO_DETECTOR_BANDWIDTH, value);
                }
            }

            public void WaitUntilSettled(Ivi.Driver.PrecisionTimeSpan maxTime)
            {
                Adapter.ViSessionStatusCheck(IviDownconverterInterop.WaitUntilSettled(Adapter.Session, (int)maxTime.TotalMilliseconds));
            }
        }

        internal class IviDownconverterRFInput : IIviDownconverterRFInput
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviDownconverter IviDownconverterInterop = null;
            private IIviDownconverterFrequencySweep DownconverterFrequencySweep = null;
            public IviDownconverterRFInput(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviDownconverterInterop = (IviCInterop.IviDownconverter)Adapter.Interop;
                DownconverterFrequencySweep = new IviDownconverterFrequencySweep(Adapter);
            }

            public string ActiveRFInput
            {
                get
                {
                    return Adapter.GetAttributeViString(IviDownconverterAttribute.IVIDOWNCONVERTER_ATTR_ACTIVE_RF_INPUT);
                }
                set
                {
                    Adapter.SetAttributeViString(IviDownconverterAttribute.IVIDOWNCONVERTER_ATTR_ACTIVE_RF_INPUT, value);
                }
            }

            public double Attenuation
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviDownconverterAttribute.IVIDOWNCONVERTER_ATTR_RF_INPUT_ATTENUATION);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviDownconverterAttribute.IVIDOWNCONVERTER_ATTR_RF_INPUT_ATTENUATION, value);
                }
            }

            public bool Bypass
            {
                get
                {
                    return Adapter.GetAttributeViBoolean(IviDownconverterAttribute.IVIDOWNCONVERTER_ATTR_BYPASS);
                }
                set
                {
                    Adapter.SetAttributeViBoolean(IviDownconverterAttribute.IVIDOWNCONVERTER_ATTR_BYPASS, value);
                }
            }

            public bool CorrectionsEnabled
            {
                get
                {
                    return Adapter.GetAttributeViBoolean(IviDownconverterAttribute.IVIDOWNCONVERTER_ATTR_RF_INPUT_CORRECTIONS_ENABLED);
                }
                set
                {
                    Adapter.SetAttributeViBoolean(IviDownconverterAttribute.IVIDOWNCONVERTER_ATTR_RF_INPUT_CORRECTIONS_ENABLED, value);
                }
            }

            public int Count
            {
                get { return Adapter.GetAttributeViInt32(IviDownconverterAttribute.IVIDOWNCONVERTER_ATTR_RF_INPUT_COUNT); }
            }

            public InputCoupling Coupling
            {
                get
                {
                    return DownconverterInputCoupling.getEnum(Adapter.GetAttributeViInt32(IviDownconverterAttribute.IVIDOWNCONVERTER_ATTR_RF_INPUT_COUPLING));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviDownconverterAttribute.IVIDOWNCONVERTER_ATTR_RF_INPUT_COUPLING, DownconverterInputCoupling.getC_Value(value));
                }
            }

            public double Frequency
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviDownconverterAttribute.IVIDOWNCONVERTER_ATTR_RF_INPUT_FREQUENCY);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviDownconverterAttribute.IVIDOWNCONVERTER_ATTR_RF_INPUT_FREQUENCY, value);
                }
            }

            public IIviDownconverterFrequencySweep FrequencySweep
            {
                get { return DownconverterFrequencySweep; }
            }

            public string GetName(int index)
            {
                StringBuilder Name = new StringBuilder(255);
                Adapter.ViSessionStatusCheck(IviDownconverterInterop.GetRFInputName(Adapter.Session, index, Name.Capacity, Name));
                return Name.ToString();
            }

            public bool PreselectorEnabled
            {
                get
                {
                    return Adapter.GetAttributeViBoolean(IviDownconverterAttribute.IVIDOWNCONVERTER_ATTR_PRESELECTOR_ENABLED);
                }
                set
                {
                    Adapter.SetAttributeViBoolean(IviDownconverterAttribute.IVIDOWNCONVERTER_ATTR_PRESELECTOR_ENABLED, value);
                }
            }

            public void SendSoftwareTrigger()
            {
                Adapter.ViSessionStatusCheck(IviDownconverterInterop.SendSoftwareTrigger(Adapter.Session));
            }
        }

        internal class IviDownconverterFrequencySweep : IIviDownconverterFrequencySweep
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviDownconverter IviDownconverterInterop = null;
            private IIviDownconverterFrequencySweepAnalog DownconverterFrequencySweepAnalog = null;
            private IIviDownconverterFrequencyStep DownconverterFrequencyStep = null;
            private IIviDownconverterFrequencySweepList DownconverterFrequencySweepList = null;
            public IviDownconverterFrequencySweep(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviDownconverterInterop = (IviCInterop.IviDownconverter)Adapter.Interop;
                DownconverterFrequencySweepAnalog = new IviDownconverterFrequencySweepAnalog(Adapter);
                DownconverterFrequencyStep = new IviDownconverterFrequencyStep(Adapter);
                DownconverterFrequencySweepList = new IviDownconverterFrequencySweepList(Adapter);
            }

            public void Configure(FrequencySweepMode mode, string triggerSource)
            {
                Adapter.ViSessionStatusCheck(IviDownconverterInterop.ConfigureFrequencySweep(Adapter.Session, DownconverterFrequencySweepMode.getC_Value(mode), triggerSource));
            }

            public Band[] GetBandInformation()
            {
                int ActualNumFrequencies = 0;
                int BandCount = NumberOfBands;
                IntPtr pStartFrequency = Marshal.AllocHGlobal((int)(BandCount * sizeof(double)));
                IntPtr pStopFrequency = Marshal.AllocHGlobal((int)(BandCount * sizeof(double)));

                Adapter.ViSessionStatusCheck(IviDownconverterInterop.GetBandCrossingInfo(Adapter.Session, BandCount, pStartFrequency, pStopFrequency, ref ActualNumFrequencies));

                double[] StartFrequency = new double[ActualNumFrequencies];
                double[] StopFrequency = new double[ActualNumFrequencies];
                Marshal.Copy(pStartFrequency, StartFrequency, 0, ActualNumFrequencies);
                Marshal.Copy(pStopFrequency, StopFrequency, 0, ActualNumFrequencies);
                Marshal.FreeHGlobal(pStartFrequency);
                Marshal.FreeHGlobal(pStopFrequency);

                Band[] Bands = new Band[ActualNumFrequencies];
                for (int i = 0; i < ActualNumFrequencies; i++)
                {
                    Bands[i] = new Band(StartFrequency[i], StopFrequency[i]);
                }

                return Bands;
            }

            public bool IsSweeping
            {
                get { return Adapter.GetAttributeViBoolean(IviDownconverterAttribute.IVIDOWNCONVERTER_ATTR_IS_SWEEPING); }
            }


            public FrequencySweepMode Mode
            {
                get
                {
                    return DownconverterFrequencySweepMode.getEnum(Adapter.GetAttributeViInt32(IviDownconverterAttribute.IVIDOWNCONVERTER_ATTR_FREQUENCY_SWEEP_MODE));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviDownconverterAttribute.IVIDOWNCONVERTER_ATTR_FREQUENCY_SWEEP_MODE, DownconverterFrequencySweepMode.getC_Value(value));
                }
            }

            public int NumberOfBands
            {
                get { return Adapter.GetAttributeViInt32(IviDownconverterAttribute.IVIDOWNCONVERTER_ATTR_NUM_BANDS); }
            }

            public string TriggerSource
            {
                get
                {
                    return Adapter.GetAttributeViString(IviDownconverterAttribute.IVIDOWNCONVERTER_ATTR_FREQUENCY_SWEEP_TRIGGER_SOURCE);
                }
                set
                {
                    Adapter.SetAttributeViString(IviDownconverterAttribute.IVIDOWNCONVERTER_ATTR_FREQUENCY_SWEEP_TRIGGER_SOURCE, value);
                }
            }

            public void WaitUntilComplete(Ivi.Driver.PrecisionTimeSpan maxTime)
            {
                Adapter.ViSessionStatusCheck(IviDownconverterInterop.WaitUntilFrequencySweepComplete(Adapter.Session, (int)maxTime.TotalMilliseconds));
            }

            public IIviDownconverterFrequencySweepAnalog Analog
            {
                get { return DownconverterFrequencySweepAnalog; }
            }

            public IIviDownconverterFrequencyStep Step
            {
                get { return DownconverterFrequencyStep; }
            }

            public IIviDownconverterFrequencySweepList List
            {
                get { return DownconverterFrequencySweepList; }
            }

        }

        internal class IviDownconverterFrequencySweepAnalog : IIviDownconverterFrequencySweepAnalog
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviDownconverter IviDownconverterInterop = null;
            public IviDownconverterFrequencySweepAnalog(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviDownconverterInterop = (IviCInterop.IviDownconverter)Adapter.Interop;
            }

            public void ConfigureStartStop(double start, double stop)
            {
                Adapter.ViSessionStatusCheck(IviDownconverterInterop.ConfigureFrequencySweepStartStop(Adapter.Session, start, stop));
            }

            public double Start
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviDownconverterAttribute.IVIDOWNCONVERTER_ATTR_FREQUENCY_SWEEP_START);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviDownconverterAttribute.IVIDOWNCONVERTER_ATTR_FREQUENCY_SWEEP_START, value);
                }
            }

            public double Stop
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviDownconverterAttribute.IVIDOWNCONVERTER_ATTR_FREQUENCY_SWEEP_STOP);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviDownconverterAttribute.IVIDOWNCONVERTER_ATTR_FREQUENCY_SWEEP_STOP, value);
                }
            }

            public PrecisionTimeSpan Time
            {
                get
                {
                    return new PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * Adapter.GetAttributeViReal64(IviDownconverterAttribute.IVIDOWNCONVERTER_ATTR_FREQUENCY_SWEEP_TIME))));
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviDownconverterAttribute.IVIDOWNCONVERTER_ATTR_FREQUENCY_SWEEP_TIME, value.TotalSeconds);
                }
            }
        }

        internal class IviDownconverterFrequencyStep : IIviDownconverterFrequencyStep
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviDownconverter IviDownconverterInterop = null;
            public IviDownconverterFrequencyStep(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviDownconverterInterop = (IviCInterop.IviDownconverter)Adapter.Interop;
            }

            public void ConfigureDwell(bool singleStepEnabled, PrecisionTimeSpan dwell)
            {
                Adapter.ViSessionStatusCheck(IviDownconverterInterop.ConfigureFrequencyStepDwell(Adapter.Session, singleStepEnabled, dwell.TotalSeconds));
            }

            public void ConfigureStartStop(double start, double stop, FrequencyStepScaling scaling, double stepSize)
            {
                Adapter.ViSessionStatusCheck(IviDownconverterInterop.ConfigureFrequencyStepStartStop(Adapter.Session, start, stop, DownconverterFrequencyStepScaling.getC_Value(scaling), stepSize));
            }

            public PrecisionTimeSpan Dwell
            {
                get
                {
                    return new Ivi.Driver.PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * Adapter.GetAttributeViReal64(IviDownconverterAttribute.IVIDOWNCONVERTER_ATTR_FREQUENCY_STEP_DWELL))));
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviDownconverterAttribute.IVIDOWNCONVERTER_ATTR_FREQUENCY_STEP_DWELL, value.TotalSeconds);
                }
            }

            public void Reset()
            {
                Adapter.ViSessionStatusCheck(IviDownconverterInterop.ResetFrequencyStep(Adapter.Session));
            }

            public FrequencyStepScaling Scaling
            {
                get
                {
                    return DownconverterFrequencyStepScaling.getEnum(Adapter.GetAttributeViInt32(IviDownconverterAttribute.IVIDOWNCONVERTER_ATTR_FREQUENCY_STEP_SCALING));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviDownconverterAttribute.IVIDOWNCONVERTER_ATTR_FREQUENCY_STEP_SCALING, DownconverterFrequencyStepScaling.getC_Value(value));
                }
            }

            public bool SingleStepEnabled
            {
                get
                {
                    return Adapter.GetAttributeViBoolean(IviDownconverterAttribute.IVIDOWNCONVERTER_ATTR_FREQUENCY_STEP_SINGLE_STEP_ENABLED);
                }
                set
                {
                    Adapter.SetAttributeViBoolean(IviDownconverterAttribute.IVIDOWNCONVERTER_ATTR_FREQUENCY_STEP_SINGLE_STEP_ENABLED, value);
                }
            }

            public double Size
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviDownconverterAttribute.IVIDOWNCONVERTER_ATTR_FREQUENCY_STEP_SIZE);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviDownconverterAttribute.IVIDOWNCONVERTER_ATTR_FREQUENCY_STEP_SIZE, value);
                }
            }

            public double Start
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviDownconverterAttribute.IVIDOWNCONVERTER_ATTR_FREQUENCY_STEP_START);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviDownconverterAttribute.IVIDOWNCONVERTER_ATTR_FREQUENCY_STEP_START, value);
                }
            }

            public double Stop
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviDownconverterAttribute.IVIDOWNCONVERTER_ATTR_FREQUENCY_STEP_STOP);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviDownconverterAttribute.IVIDOWNCONVERTER_ATTR_FREQUENCY_STEP_STOP, value);
                }
            }
        }

        internal class IviDownconverterFrequencySweepList : IIviDownconverterFrequencySweepList
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviDownconverter IviDownconverterInterop = null;
            public IviDownconverterFrequencySweepList(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviDownconverterInterop = (IviCInterop.IviDownconverter)Adapter.Interop;
            }

            public void ClearAll()
            {
                Adapter.ViSessionStatusCheck(IviDownconverterInterop.ClearAllFrequencySweepLists(Adapter.Session));
            }

            public void ConfigureDwell(bool singleStepEnabled, PrecisionTimeSpan dwell)
            {
                Adapter.ViSessionStatusCheck(IviDownconverterInterop.ConfigureFrequencySweepListDwell(Adapter.Session, singleStepEnabled, dwell.TotalSeconds));
            }

            public void CreateList(string name, double[] frequencyList)
            {
                Adapter.ViSessionStatusCheck(IviDownconverterInterop.CreateFrequencySweepList(Adapter.Session, name, frequencyList.Length, frequencyList));
            }

            public PrecisionTimeSpan Dwell
            {
                get
                {
                    return new Ivi.Driver.PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * Adapter.GetAttributeViReal64(IviDownconverterAttribute.IVIDOWNCONVERTER_ATTR_FREQUENCY_SWEEP_LIST_DWELL))));
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviDownconverterAttribute.IVIDOWNCONVERTER_ATTR_FREQUENCY_SWEEP_LIST_DWELL, value.TotalSeconds);
                }
            }

            public void Reset()
            {
                Adapter.ViSessionStatusCheck(IviDownconverterInterop.ResetFrequencySweepList(Adapter.Session));
            }

            public string SelectedName
            {
                get
                {
                    return Adapter.GetAttributeViString(IviDownconverterAttribute.IVIDOWNCONVERTER_ATTR_FREQUENCY_SWEEP_LIST_SELECTED_NAME);
                }
                set
                {
                    Adapter.SetAttributeViString(IviDownconverterAttribute.IVIDOWNCONVERTER_ATTR_FREQUENCY_SWEEP_LIST_SELECTED_NAME, value);
                }
            }

            public bool SingleStepEnabled
            {
                get
                {
                    return Adapter.GetAttributeViBoolean(IviDownconverterAttribute.IVIDOWNCONVERTER_ATTR_FREQUENCY_SWEEP_LIST_SINGLE_STEP_ENABLED);
                }
                set
                {
                    Adapter.SetAttributeViBoolean(IviDownconverterAttribute.IVIDOWNCONVERTER_ATTR_FREQUENCY_SWEEP_LIST_SINGLE_STEP_ENABLED, value);
                }
            }
        }

        internal class IviDownconverterReferenceOscillator : IIviDownconverterReferenceOscillator
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviDownconverter IviDownconverterInterop = null;
            public IviDownconverterReferenceOscillator(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviDownconverterInterop = (IviCInterop.IviDownconverter)Adapter.Interop;
            }

            public void Configure(string source, double frequency)
            {
                Adapter.ViSessionStatusCheck(IviDownconverterInterop.ConfigureReferenceOscillator(Adapter.Session, DownconverterReferenceOscillatorSource.getC_Value(source), frequency));
            }

            public double ExternalFrequency
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviDownconverterAttribute.IVIDOWNCONVERTER_ATTR_REFERENCE_OSCILLATOR_EXTERNAL_FREQUENCY);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviDownconverterAttribute.IVIDOWNCONVERTER_ATTR_REFERENCE_OSCILLATOR_EXTERNAL_FREQUENCY, value);
                }
            }

            public bool OutputEnabled
            {
                get
                {
                    return Adapter.GetAttributeViBoolean(IviDownconverterAttribute.IVIDOWNCONVERTER_ATTR_REFERENCE_OSCILLATOR_OUTPUT_ENABLED);
                }
                set
                {
                    Adapter.SetAttributeViBoolean(IviDownconverterAttribute.IVIDOWNCONVERTER_ATTR_REFERENCE_OSCILLATOR_OUTPUT_ENABLED, value);
                }
            }

            public string Source
            {
                get
                {
                    return DownconverterReferenceOscillatorSource.getEnum(Adapter.GetAttributeViInt32(IviDownconverterAttribute.IVIDOWNCONVERTER_ATTR_REFERENCE_OSCILLATOR_SOURCE));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviDownconverterAttribute.IVIDOWNCONVERTER_ATTR_REFERENCE_OSCILLATOR_SOURCE, DownconverterReferenceOscillatorSource.getC_Value(value));
                }
            }
        }

    }
}
