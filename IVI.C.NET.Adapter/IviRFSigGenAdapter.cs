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
using Ivi.RFSigGen;
using IVI.C.NET.Adapter.IviCInterop;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace IVI.C.NET.Adapter
{
    public class IviRFSigGenAdapter : DriverAdapterBase<IviCInterop.IviRFSigGen>, IIviRFSigGen
    {
        #region Enum Mapping

        private static IviEnumCMapping<string, int> RFSigGenAlcSource  = IviEnumCMapping<string, int>.Instance
            .Map("Internal", IviRFSigGenAttribute.IVIRFSIGGEN_VAL_ALC_SOURCE_INTERNAL)
            .Map("External", IviRFSigGenAttribute.IVIRFSIGGEN_VAL_ALC_SOURCE_EXTERNAL);

        private static IviEnumCMapping<AMScaling, int> RFSigGenAMScaling = IviEnumCMapping<AMScaling, int>.Instance
            .Map(AMScaling.Linear, IviRFSigGenAttribute.IVIRFSIGGEN_VAL_AM_SCALING_LINEAR)
            .Map(AMScaling.Logarithmic, IviRFSigGenAttribute.IVIRFSIGGEN_VAL_AM_SCALING_LOGARITHMIC);

        private static IviEnumCMapping<ExternalCoupling, int> RFSigGenAMExternalCoupling = IviEnumCMapping<ExternalCoupling, int>.Instance
            .Map(ExternalCoupling.AC, IviRFSigGenAttribute.IVIRFSIGGEN_VAL_AM_EXTERNAL_COUPLING_AC)
            .Map(ExternalCoupling.DC, IviRFSigGenAttribute.IVIRFSIGGEN_VAL_AM_EXTERNAL_COUPLING_DC);

        private static IviEnumCMapping<ExternalCoupling, int> RFSigGenFMExternalCoupling = IviEnumCMapping<ExternalCoupling, int>.Instance
            .Map(ExternalCoupling.AC, IviRFSigGenAttribute.IVIRFSIGGEN_VAL_FM_EXTERNAL_COUPLING_AC)
            .Map(ExternalCoupling.DC, IviRFSigGenAttribute.IVIRFSIGGEN_VAL_FM_EXTERNAL_COUPLING_DC);

        private static IviEnumCMapping<ExternalCoupling, int> RFSigGenPMExternalCoupling = IviEnumCMapping<ExternalCoupling, int>.Instance
            .Map(ExternalCoupling.AC, IviRFSigGenAttribute.IVIRFSIGGEN_VAL_PM_EXTERNAL_COUPLING_AC)
            .Map(ExternalCoupling.DC, IviRFSigGenAttribute.IVIRFSIGGEN_VAL_PM_EXTERNAL_COUPLING_DC);

        private static IviEnumCMapping<Slope, int> RFSigGenArbExternalTriggerSlope = IviEnumCMapping<Slope, int>.Instance
            .Map(Slope.Positive, IviRFSigGenAttribute.IVIRFSIGGEN_VAL_ARB_EXTERNAL_TRIGGER_SLOPE_POSITIVE)
            .Map(Slope.Negative, IviRFSigGenAttribute.IVIRFSIGGEN_VAL_ARB_EXTERNAL_TRIGGER_SLOPE_NEGATIVE);

        private static IviEnumCMapping<DigitalModulationBaseDataSource, int> RFSigGenDigitalModulationBaseDataSource = IviEnumCMapping<DigitalModulationBaseDataSource, int>.Instance
            .Map(DigitalModulationBaseDataSource.External, IviRFSigGenAttribute.IVIRFSIGGEN_VAL_DIGITAL_MODULATION_BASE_DATA_SOURCE_EXTERNAL)
            .Map(DigitalModulationBaseDataSource.Prbs, IviRFSigGenAttribute.IVIRFSIGGEN_VAL_DIGITAL_MODULATION_BASE_DATA_SOURCE_PRBS)
            .Map(DigitalModulationBaseDataSource.BitSequence, IviRFSigGenAttribute.IVIRFSIGGEN_VAL_DIGITAL_MODULATION_BASE_DATA_SOURCE_BIT_SEQUENCE);

        private static IviEnumCMapping<DigitalModulationBaseExternalClockType, int> RFSigGenDigitalModulationBaseExternalClockType = IviEnumCMapping<DigitalModulationBaseExternalClockType, int>.Instance
            .Map(DigitalModulationBaseExternalClockType.Bit, IviRFSigGenAttribute.IVIRFSIGGEN_VAL_DIGITAL_MODULATION_BASE_EXTERNAL_CLOCK_TYPE_BIT)
            .Map(DigitalModulationBaseExternalClockType.Symbol, IviRFSigGenAttribute.IVIRFSIGGEN_VAL_DIGITAL_MODULATION_BASE_EXTERNAL_CLOCK_TYPE_SYMBOL);

        private static IviEnumCMapping<DigitalModulationBasePrbsType, int> RFSigGenDigitalModulationBasePrbsType = IviEnumCMapping<DigitalModulationBasePrbsType, int>.Instance
            .Map(DigitalModulationBasePrbsType.Prbs9, IviRFSigGenAttribute.IVIRFSIGGEN_VAL_DIGITAL_MODULATION_BASE_PRBS_TYPE_PRBS9)
            .Map(DigitalModulationBasePrbsType.Prbs11, IviRFSigGenAttribute.IVIRFSIGGEN_VAL_DIGITAL_MODULATION_BASE_PRBS_TYPE_PRBS11)
            .Map(DigitalModulationBasePrbsType.Prbs15, IviRFSigGenAttribute.IVIRFSIGGEN_VAL_DIGITAL_MODULATION_BASE_PRBS_TYPE_PRBS15)
            .Map(DigitalModulationBasePrbsType.Prbs16, IviRFSigGenAttribute.IVIRFSIGGEN_VAL_DIGITAL_MODULATION_BASE_PRBS_TYPE_PRBS16)
            .Map(DigitalModulationBasePrbsType.Prbs20, IviRFSigGenAttribute.IVIRFSIGGEN_VAL_DIGITAL_MODULATION_BASE_PRBS_TYPE_PRBS20)
            .Map(DigitalModulationBasePrbsType.Prbs21, IviRFSigGenAttribute.IVIRFSIGGEN_VAL_DIGITAL_MODULATION_BASE_PRBS_TYPE_PRBS21)
            .Map(DigitalModulationBasePrbsType.Prbs23, IviRFSigGenAttribute.IVIRFSIGGEN_VAL_DIGITAL_MODULATION_BASE_PRBS_TYPE_PRBS23);

        private static IviEnumCMapping<string, int> RFSigGenBaseClockSource = IviEnumCMapping<string, int>.Instance
            .Map("Internal", IviRFSigGenAttribute.IVIRFSIGGEN_VAL_DIGITAL_MODULATION_BASE_CLOCK_SOURCE_INTERNAL)
            .Map("External", IviRFSigGenAttribute.IVIRFSIGGEN_VAL_DIGITAL_MODULATION_BASE_CLOCK_SOURCE_EXTERNAL);

        private static IviEnumCMapping<string, int> RFSigGenCdmaClockSource = IviEnumCMapping<string, int>.Instance
            .Map("Internal", IviRFSigGenAttribute.IVIRFSIGGEN_VAL_CDMA_CLOCK_SOURCE_INTERNAL)
            .Map("External", IviRFSigGenAttribute.IVIRFSIGGEN_VAL_CDMA_CLOCK_SOURCE_EXTERNAL);

        private static IviEnumCMapping<Slope, int> RFSigGenCdmaExternalTriggerSlope = IviEnumCMapping<Slope, int>.Instance
            .Map(Slope.Positive, IviRFSigGenAttribute.IVIRFSIGGEN_VAL_CDMA_EXTERNAL_TRIGGER_SLOPE_POSITIVE )
            .Map(Slope.Negative, IviRFSigGenAttribute.IVIRFSIGGEN_VAL_CDMA_EXTERNAL_TRIGGER_SLOPE_NEGATIVE );

        private static IviEnumCMapping<string, int> RFSigGenCdmaTriggerSource = IviEnumCMapping<string, int>.Instance
            .Map("Internal", IviRFSigGenAttribute.IVIRFSIGGEN_VAL_CDMA_TRIGGER_SOURCE_IMMEDIATE)
            .Map("External", IviRFSigGenAttribute.IVIRFSIGGEN_VAL_CDMA_TRIGGER_SOURCE_EXTERNAL)
            .Map("Software", IviRFSigGenAttribute.IVIRFSIGGEN_VAL_CDMA_TRIGGER_SOURCE_SOFTWARE);

        private static IviEnumCMapping<string, int> RFSigGenTdmaClockSource = IviEnumCMapping<string, int>.Instance
            .Map("Internal", IviRFSigGenAttribute.IVIRFSIGGEN_VAL_TDMA_CLOCK_SOURCE_INTERNAL)
            .Map("External", IviRFSigGenAttribute.IVIRFSIGGEN_VAL_TDMA_CLOCK_SOURCE_EXTERNAL);

        private static IviEnumCMapping<TdmaExternalClockType, int> RFSigGenTdmaExternalClockType = IviEnumCMapping<TdmaExternalClockType, int>.Instance
            .Map(TdmaExternalClockType.Bit, IviRFSigGenAttribute.IVIRFSIGGEN_VAL_TDMA_EXTERNAL_CLOCK_TYPE_BIT)
            .Map(TdmaExternalClockType.Symbol, IviRFSigGenAttribute.IVIRFSIGGEN_VAL_TDMA_EXTERNAL_CLOCK_TYPE_SYMBOL);

        private static IviEnumCMapping<Slope, int> RFSigGenTdmaExternalTriggerSlope = IviEnumCMapping<Slope, int>.Instance
            .Map(Slope.Positive, IviRFSigGenAttribute.IVIRFSIGGEN_VAL_TDMA_EXTERNAL_TRIGGER_SLOPE_POSITIVE)
            .Map(Slope.Negative, IviRFSigGenAttribute.IVIRFSIGGEN_VAL_TDMA_EXTERNAL_TRIGGER_SLOPE_NEGATIVE);

        private static IviEnumCMapping<string, int> RFSigGenTdmaTriggerSource = IviEnumCMapping<string, int>.Instance
            .Map("Internal", IviRFSigGenAttribute.IVIRFSIGGEN_VAL_TDMA_TRIGGER_SOURCE_IMMEDIATE)
            .Map("External", IviRFSigGenAttribute.IVIRFSIGGEN_VAL_TDMA_TRIGGER_SOURCE_EXTERNAL)
            .Map("Software", IviRFSigGenAttribute.IVIRFSIGGEN_VAL_TDMA_TRIGGER_SOURCE_SOFTWARE);

        private static IviEnumCMapping<IQSource, int> RFSigGenIQSource = IviEnumCMapping<IQSource, int>.Instance
            .Map(IQSource.DigitalModulationBase, IviRFSigGenAttribute.IVIRFSIGGEN_VAL_IQ_SOURCE_DIGITAL_MODULATION_BASE )
            .Map(IQSource.CdmaBase, IviRFSigGenAttribute.IVIRFSIGGEN_VAL_IQ_SOURCE_CDMA_BASE)
            .Map(IQSource.TdmaBase, IviRFSigGenAttribute.IVIRFSIGGEN_VAL_IQ_SOURCE_TDMA_BASE)
            .Map(IQSource.ArbGenerator, IviRFSigGenAttribute.IVIRFSIGGEN_VAL_IQ_SOURCE_ARB_GENERATOR)
            .Map(IQSource.External, IviRFSigGenAttribute.IVIRFSIGGEN_VAL_IQ_SOURCE_EXTERNAL);

        private static IviEnumCMapping<LFGeneratorWaveform, int> RFSigGenLFGeneratorWaveform = IviEnumCMapping<LFGeneratorWaveform, int>.Instance
            .Map(LFGeneratorWaveform.Sine, IviRFSigGenAttribute.IVIRFSIGGEN_VAL_LF_GENERATOR_WAVEFORM_SINE)
            .Map(LFGeneratorWaveform.Square, IviRFSigGenAttribute.IVIRFSIGGEN_VAL_LF_GENERATOR_WAVEFORM_SQUARE)
            .Map(LFGeneratorWaveform.Triangle, IviRFSigGenAttribute.IVIRFSIGGEN_VAL_LF_GENERATOR_WAVEFORM_TRIANGLE)
            .Map(LFGeneratorWaveform.RampUp, IviRFSigGenAttribute.IVIRFSIGGEN_VAL_LF_GENERATOR_WAVEFORM_RAMP_UP)
            .Map(LFGeneratorWaveform.RampDown, IviRFSigGenAttribute.IVIRFSIGGEN_VAL_LF_GENERATOR_WAVEFORM_RAMP_DOWN);

        private static IviEnumCMapping<PulseModulationExternalPolarity, int> RFSigGenPulseModulationExternalPolarity = IviEnumCMapping<PulseModulationExternalPolarity, int>.Instance
            .Map(PulseModulationExternalPolarity.Normal, IviRFSigGenAttribute.IVIRFSIGGEN_VAL_PULSE_MODULATION_EXTERNAL_POLARITY_NORMAL)
            .Map(PulseModulationExternalPolarity.Inverse, IviRFSigGenAttribute.IVIRFSIGGEN_VAL_PULSE_MODULATION_EXTERNAL_POLARITY_INVERSE);

        private static IviEnumCMapping<PulseModulationSource, int> RFSigGenPulseModulationSource = IviEnumCMapping<PulseModulationSource, int>.Instance
            .Map(PulseModulationSource.Internal, IviRFSigGenAttribute.IVIRFSIGGEN_VAL_PULSE_MODULATION_SOURCE_INTERNAL)
            .Map(PulseModulationSource.External, IviRFSigGenAttribute.IVIRFSIGGEN_VAL_PULSE_MODULATION_SOURCE_EXTERNAL);

        private static IviEnumCMapping<Slope, int> RFSigGenPulseGeneratorExternalTriggerSlope = IviEnumCMapping<Slope, int>.Instance
            .Map(Slope.Positive, IviRFSigGenAttribute.IVIRFSIGGEN_VAL_PULSE_EXTERNAL_TRIGGER_SLOPE_POSITIVE )
            .Map(Slope.Negative, IviRFSigGenAttribute.IVIRFSIGGEN_VAL_PULSE_EXTERNAL_TRIGGER_SLOPE_NEGATIVE );

        private static IviEnumCMapping<string, int> RFSigGenPulseGeneratorTriggerSource = IviEnumCMapping<string, int>.Instance
            .Map("Internal", IviRFSigGenAttribute.IVIRFSIGGEN_VAL_PULSE_TRIGGER_SOURCE_INTERNAL)
            .Map("External", IviRFSigGenAttribute.IVIRFSIGGEN_VAL_PULSE_TRIGGER_SOURCE_EXTERNAL);

        private static IviEnumCMapping<PulseOutputPolarity, int> RFSigGenPulseOutputPolarity = IviEnumCMapping<PulseOutputPolarity, int>.Instance
            .Map(PulseOutputPolarity.Normal, IviRFSigGenAttribute.IVIRFSIGGEN_VAL_PULSE_OUTPUT_POLARITY_NORMAL)
            .Map(PulseOutputPolarity.Inverse, IviRFSigGenAttribute.IVIRFSIGGEN_VAL_PULSE_OUTPUT_POLARITY_INVERSE);

        private static IviEnumCMapping<string, int> RFSigGenReferenceOscillatorSource = IviEnumCMapping<string, int>.Instance
            .Map("Internal", IviRFSigGenAttribute.IVIRFSIGGEN_VAL_REFERENCE_OSCILLATOR_SOURCE_INTERNAL)
            .Map("External", IviRFSigGenAttribute.IVIRFSIGGEN_VAL_REFERENCE_OSCILLATOR_SOURCE_EXTERNAL);

        private static IviEnumCMapping<SweepMode, int> RFSigGenSweepMode = IviEnumCMapping<SweepMode, int>.Instance
            .Map(SweepMode.None, IviRFSigGenAttribute.IVIRFSIGGEN_VAL_SWEEP_MODE_NONE)
            .Map(SweepMode.FrequencySweep, IviRFSigGenAttribute.IVIRFSIGGEN_VAL_SWEEP_MODE_FREQUENCY_SWEEP)
            .Map(SweepMode.PowerSweep, IviRFSigGenAttribute.IVIRFSIGGEN_VAL_SWEEP_MODE_POWER_SWEEP)
            .Map(SweepMode.FrequencyStep, IviRFSigGenAttribute.IVIRFSIGGEN_VAL_SWEEP_MODE_FREQUENCY_STEP)
            .Map(SweepMode.PowerStep, IviRFSigGenAttribute.IVIRFSIGGEN_VAL_SWEEP_MODE_POWER_STEP)
            .Map(SweepMode.List, IviRFSigGenAttribute.IVIRFSIGGEN_VAL_SWEEP_MODE_LIST);

        private static IviEnumCMapping<string, int> RFSigGenSweepTriggerSource = IviEnumCMapping<string, int>.Instance
            .Map("Immediate", IviRFSigGenAttribute.IVIRFSIGGEN_VAL_SWEEP_TRIGGER_SOURCE_IMMEDIATE)
            .Map("External", IviRFSigGenAttribute.IVIRFSIGGEN_VAL_SWEEP_TRIGGER_SOURCE_EXTERNAL)
            .Map("Software", IviRFSigGenAttribute.IVIRFSIGGEN_VAL_SWEEP_TRIGGER_SOURCE_SOFTWARE);

        private static IviEnumCMapping<FrequencyStepScaling, int> RFSigGenFrequencyStepScaling = IviEnumCMapping<FrequencyStepScaling, int>.Instance
            .Map(FrequencyStepScaling.Linear, IviRFSigGenAttribute.IVIRFSIGGEN_VAL_FREQUENCY_STEP_SCALING_LINEAR)
            .Map(FrequencyStepScaling.Logarithmic, IviRFSigGenAttribute.IVIRFSIGGEN_VAL_FREQUENCY_STEP_SCALING_LOGARITHMIC );

        #endregion

        private IIviRFSigGenAlc RFSigGenAlc = null;
        private IIviRFSigGenAnalogModulation RFSigGenAnalogModulation = null;
        private IIviRFSigGenDigitalModulation RFSigGenDigitalModulation = null;
        private IIviRFSigGenIQ RFSigGenIQ = null;
        private IIviRFSigGenLFGenerator RFSigGenLFGenerator = null;
        private IIviRFSigGenPulseModulation RFSigGenPulseModulation = null;
        private IIviRFSigGenPulseGenerator RFSigGenPulseGenerator = null;
        private IIviRFSigGenRF RFSigGenRF = null;
        private IIviRFSigGenReferenceOscillator RFSigGenReferenceOscillator = null;
        private IIviRFSigGenSweep RFSigGenSweep = null;
        public IviRFSigGenAdapter(string name, bool idQuery, bool reset, string options)
            : base(name, idQuery, reset, options)
        {
            RFSigGenAlc = new IviRFSigGenAlc(this);
            RFSigGenAnalogModulation = new IviRFSigGenAnalogModulation(this);
            RFSigGenDigitalModulation = new IviRFSigGenDigitalModulation(this);
            RFSigGenIQ = new IviRFSigGenIQ(this);
            RFSigGenLFGenerator = new IviRFSigGenLFGenerator(this);
            RFSigGenPulseModulation = new IviRFSigGenPulseModulation(this);
            RFSigGenPulseGenerator = new IviRFSigGenPulseGenerator(this);
            RFSigGenRF = new IviRFSigGenRF(this);
            RFSigGenReferenceOscillator = new IviRFSigGenReferenceOscillator(this);
            RFSigGenSweep = new IviRFSigGenSweep(this);
        }

        public IIviRFSigGenAlc Alc
        {
            get { return RFSigGenAlc; }
        }

        public IIviRFSigGenAnalogModulation AnalogModulation
        {
            get { return RFSigGenAnalogModulation; }
        }

        public IIviRFSigGenDigitalModulation DigitalModulation
        {
            get { return RFSigGenDigitalModulation; }
        }

        public IIviRFSigGenIQ IQ
        {
            get { return RFSigGenIQ; }
        }

        public IIviRFSigGenLFGenerator LFGenerator
        {
            get { return RFSigGenLFGenerator; }
        }

        public IIviRFSigGenPulseGenerator PulseGenerator
        {
            get { return RFSigGenPulseGenerator; }
        }

        public IIviRFSigGenPulseModulation PulseModulation
        {
            get { return RFSigGenPulseModulation; }
        }

        public IIviRFSigGenRF RF
        {
            get { return RFSigGenRF; }
        }

        public IIviRFSigGenReferenceOscillator ReferenceOscillator
        {
            get { return RFSigGenReferenceOscillator; }
        }

        public void SendSoftwareTrigger()
        {
            ViSessionStatusCheck(((IviCInterop.IviRFSigGen)Interop).SendSoftwareTrigger(Session));
        }

        public IIviRFSigGenSweep Sweep
        {
            get { return RFSigGenSweep; }
        }

        internal class IviRFSigGenAlc : IIviRFSigGenAlc
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviRFSigGen IviRFSigGenInterop = null;
            public IviRFSigGenAlc(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviRFSigGenInterop = (IviCInterop.IviRFSigGen)Adapter.Interop;
            }

            public double Bandwidth
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_ALC_BANDWIDTH);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_ALC_BANDWIDTH, value);
                }
            }

            public void Configure(string source, double bandwidth)
            {
                Adapter.ViSessionStatusCheck(IviRFSigGenInterop.ConfigureALC(Adapter.Session, RFSigGenAlcSource.getC_Value(source), bandwidth));
            }

            public bool Enabled
            {
                get
                {
                    return Adapter.GetAttributeViBoolean(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_ALC_ENABLED);
                }
                set
                {
                    Adapter.SetAttributeViBoolean(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_ALC_ENABLED, value);
                }
            }

            public string Source
            {
                get
                {
                    return RFSigGenAlcSource.getEnum(Adapter.GetAttributeViInt32(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_ALC_SOURCE));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_ALC_SOURCE, RFSigGenAlcSource.getC_Value(value));
                }
            }
        }

        #region Analog Modulation

        internal class IviRFSigGenAnalogModulation : IIviRFSigGenAnalogModulation
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviRFSigGen IviRFSigGenInterop = null;
            private IIviRFSigGenAM RFSigGenAM = null;
            private IIviRFSigGenFM RFSigGenFM = null;
            private IIviRFSigGenPM RFSigGenPM = null;
            private IIviRFSigGenAnalogModulationSource RFSigGenAnalogModulationSource = null;
            public IviRFSigGenAnalogModulation(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviRFSigGenInterop = (IviCInterop.IviRFSigGen)Adapter.Interop;
                RFSigGenAM = new IviRFSigGenAM(Adapter);
                RFSigGenFM = new IviRFSigGenFM(Adapter);
                RFSigGenPM = new IviRFSigGenPM(Adapter);
                RFSigGenAnalogModulationSource = new IviRFSigGenAnalogModulationSource(Adapter);
            }

            public IIviRFSigGenAM AM
            {
                get { return RFSigGenAM; }
            }

            public IIviRFSigGenFM FM
            {
                get { return RFSigGenFM; }
            }

            public IIviRFSigGenPM PM
            {
                get { return RFSigGenPM; }
            }

            public IIviRFSigGenAnalogModulationSource Source
            {
                get { return RFSigGenAnalogModulationSource; }
            }
        }

        internal class IviRFSigGenAM : IIviRFSigGenAM
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviRFSigGen IviRFSigGenInterop = null;
            public IviRFSigGenAM(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviRFSigGenInterop = (IviCInterop.IviRFSigGen)Adapter.Interop;
            }

            public void Configure(string source, AMScaling scaling, double depth)
            {
                Adapter.ViSessionStatusCheck(IviRFSigGenInterop.ConfigureAM(Adapter.Session, source, RFSigGenAMScaling.getC_Value(scaling), depth));
            }

            public double Depth
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_AM_DEPTH);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_AM_DEPTH, value);
                }
            }

            public bool Enabled
            {
                get
                {
                    return Adapter.GetAttributeViBoolean(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_AM_ENABLED);
                }
                set
                {
                    Adapter.SetAttributeViBoolean(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_AM_ENABLED, value);
                }
            }

            public ExternalCoupling ExternalCoupling
            {
                get
                {
                    return RFSigGenAMExternalCoupling.getEnum(Adapter.GetAttributeViInt32(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_AM_EXTERNAL_COUPLING));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_AM_EXTERNAL_COUPLING, RFSigGenAMExternalCoupling.getC_Value(value));
                }
            }

            public double NominalVoltage
            {
                get { return Adapter.GetAttributeViReal64(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_AM_NOMINAL_VOLTAGE); }
            }

            public AMScaling Scaling
            {
                get
                {
                    return RFSigGenAMScaling.getEnum(Adapter.GetAttributeViInt32(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_AM_SCALING));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_AM_SCALING, RFSigGenAMScaling.getC_Value(value));
                }
            }

            public string Source
            {
                get
                {
                    return Adapter.GetAttributeViString(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_AM_SOURCE);
                }
                set
                {
                    Adapter.SetAttributeViString(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_AM_SOURCE, value);
                }
            }
        }

        internal class IviRFSigGenFM : IIviRFSigGenFM
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviRFSigGen IviRFSigGenInterop = null;
            public IviRFSigGenFM(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviRFSigGenInterop = (IviCInterop.IviRFSigGen)Adapter.Interop;
            }

            public void Configure(string source, double deviation)
            {
                Adapter.ViSessionStatusCheck(IviRFSigGenInterop.ConfigureFM(Adapter.Session, source, deviation));
            }

            public double Deviation
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_FM_DEVIATION);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_FM_DEVIATION, value);
                }
            }

            public bool Enabled
            {
                get
                {
                    return Adapter.GetAttributeViBoolean(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_FM_ENABLED);
                }
                set
                {
                    Adapter.SetAttributeViBoolean(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_FM_ENABLED, value);
                }
            }

            public ExternalCoupling ExternalCoupling
            {
                get
                {
                    return RFSigGenFMExternalCoupling.getEnum(Adapter.GetAttributeViInt32(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_FM_EXTERNAL_COUPLING));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_FM_EXTERNAL_COUPLING, RFSigGenFMExternalCoupling.getC_Value(value));
                }
            }

            public double NominalVoltage
            {
                get { return Adapter.GetAttributeViReal64(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_FM_NOMINAL_VOLTAGE); }
            }

            public string Source
            {
                get
                {
                    return Adapter.GetAttributeViString(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_FM_SOURCE);
                }
                set
                {
                    Adapter.SetAttributeViString(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_FM_SOURCE, value);
                }
            }
        }

        internal class IviRFSigGenPM : IIviRFSigGenPM
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviRFSigGen IviRFSigGenInterop = null;
            public IviRFSigGenPM(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviRFSigGenInterop = (IviCInterop.IviRFSigGen)Adapter.Interop;
            }

            public void Configure(string source, double deviation)
            {
                Adapter.ViSessionStatusCheck(IviRFSigGenInterop.ConfigurePM(Adapter.Session, source, deviation));
            }

            public double Deviation
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_PM_DEVIATION);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_PM_DEVIATION, value);
                }
            }

            public bool Enabled
            {
                get
                {
                    return Adapter.GetAttributeViBoolean(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_PM_ENABLED);
                }
                set
                {
                    Adapter.SetAttributeViBoolean(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_PM_ENABLED, value);
                }
            }

            public ExternalCoupling ExternalCoupling
            {
                get
                {
                    return RFSigGenPMExternalCoupling.getEnum(Adapter.GetAttributeViInt32(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_PM_EXTERNAL_COUPLING));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_PM_EXTERNAL_COUPLING, RFSigGenPMExternalCoupling.getC_Value(value));
                }
            }

            public double NominalVoltage
            {
                get { return Adapter.GetAttributeViReal64(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_PM_NOMINAL_VOLTAGE); }
            }

            public string Source
            {
                get
                {
                    return Adapter.GetAttributeViString(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_PM_SOURCE);
                }
                set
                {
                    Adapter.SetAttributeViString(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_PM_SOURCE, value);
                }
            }
        }

        internal class IviRFSigGenAnalogModulationSource : IIviRFSigGenAnalogModulationSource
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviRFSigGen IviRFSigGenInterop = null;
            public IviRFSigGenAnalogModulationSource(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviRFSigGenInterop = (IviCInterop.IviRFSigGen)Adapter.Interop;
            }

            public int Count
            {
                get { return Adapter.GetAttributeViInt32(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_ANALOG_MODULATION_SOURCE_COUNT); }
            }

            public string GetName(int index)
            {
                StringBuilder NameValue = new StringBuilder(256);
                Adapter.ViSessionStatusCheck(IviRFSigGenInterop.GetAnalogModulationSourceName(Adapter.Session, index, NameValue.Capacity, NameValue));
                return NameValue.ToString();
            }
        }

        #endregion

        #region Digital Modulation

        internal class IviRFSigGenDigitalModulation : IIviRFSigGenDigitalModulation
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviRFSigGen IviRFSigGenInterop = null;
            private IIviRFSigGenArb RFSigGenArb = null;
            private IIviRFSigGenDigitalModulationBase RFSigGenDigitalModulationBase = null;
            private IIviRFSigGenCdma RFSigGenCdma = null;
            private IIviRFSigGenTdma RFSigGenTdma = null;
            public IviRFSigGenDigitalModulation(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviRFSigGenInterop = (IviCInterop.IviRFSigGen)Adapter.Interop;
                RFSigGenArb = new IviRFSigGenArb(Adapter);
                RFSigGenDigitalModulationBase = new IviRFSigGenDigitalModulationBase(Adapter);
                RFSigGenCdma = new IviRFSigGenCdma(Adapter);
                RFSigGenTdma = new IviRFSigGenTdma(Adapter);
            }

            public IIviRFSigGenArb Arb
            {
                get { return RFSigGenArb; }
            }

            public IIviRFSigGenDigitalModulationBase Base
            {
                get { return RFSigGenDigitalModulationBase; }
            }

            public IIviRFSigGenCdma Cdma
            {
                get { return RFSigGenCdma; }
            }

            public IIviRFSigGenTdma Tdma
            {
                get { return RFSigGenTdma; }
            }
        }

        internal class IviRFSigGenArb : IIviRFSigGenArb
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviRFSigGen IviRFSigGenInterop = null;
            public IviRFSigGenArb(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviRFSigGenInterop = (IviCInterop.IviRFSigGen)Adapter.Interop;
            }

            public void ClearAllWaveforms()
            {
                Adapter.ViSessionStatusCheck(IviRFSigGenInterop.ClearAllArbWaveforms(Adapter.Session));
            }

            public double ClockFrequency
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_ARB_CLOCK_FREQUENCY);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_ARB_CLOCK_FREQUENCY, value);
                }
            }

            public void Configure(double clockFrequency, double filterFrequency)
            {
                Adapter.ViSessionStatusCheck(IviRFSigGenInterop.ConfigureArb(Adapter.Session, clockFrequency, filterFrequency));
            }

            public Slope ExternalTriggerSlope
            {
                get
                {
                    return RFSigGenArbExternalTriggerSlope.getEnum(Adapter.GetAttributeViInt32(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_ARB_EXTERNAL_TRIGGER_SLOPE));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_ARB_EXTERNAL_TRIGGER_SLOPE, RFSigGenArbExternalTriggerSlope.getC_Value(value));
                }
            }

            public double FilterFrequency
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_ARB_FILTER_FREQUENCY);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_ARB_FILTER_FREQUENCY, value);
                }
            }

            public int MaxNumberWaveforms
            {
                get { return Adapter.GetAttributeViInt32(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_ARB_MAX_NUMBER_WAVEFORMS); }
            }

            public string SelectedWaveform
            {
                get
                {
                    return Adapter.GetAttributeViString(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_ARB_SELECTED_WAVEFORM);
                }
                set
                {
                    Adapter.SetAttributeViString(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_ARB_SELECTED_WAVEFORM, value);
                }
            }

            public string TriggerSource
            {
                get
                {
                    return Adapter.GetAttributeViString(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_ARB_TRIGGER_SOURCE);
                }
                set
                {
                    Adapter.SetAttributeViString(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_ARB_TRIGGER_SOURCE, value);
                }
            }

            public int WaveformQuantum
            {
                get { return Adapter.GetAttributeViInt32(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_ARB_WAVEFORM_QUANTUM); }
            }

            public int WaveformSizeMax
            {
                get { return Adapter.GetAttributeViInt32(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_ARB_WAVEFORM_SIZE_MAX); }
            }

            public int WaveformSizeMin
            {
                get { return Adapter.GetAttributeViInt32(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_ARB_WAVEFORM_SIZE_MIN); }
            }

            public void WriteWaveform(string name, double[] iData, double[] qData, bool moreDataPending)
            {
                Adapter.ViSessionStatusCheck(IviRFSigGenInterop.WriteArbWaveform(Adapter.Session, name, iData.Length, iData, qData, moreDataPending));
            }
        }

        internal class IviRFSigGenDigitalModulationBase : IIviRFSigGenDigitalModulationBase
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviRFSigGen IviRFSigGenInterop = null;
            public IviRFSigGenDigitalModulationBase(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviRFSigGenInterop = (IviCInterop.IviRFSigGen)Adapter.Interop;
            }

            public void ClearAllBitSequences()
            {
                Adapter.ViSessionStatusCheck(IviRFSigGenInterop.ClearAllDigitalModulationBaseBitSequences(Adapter.Session));
            }

            public string ClockSource
            {
                get
                {
                    return Adapter.GetAttributeViString(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_DIGITAL_MODULATION_BASE_CLOCK_SOURCE);
                }
                set
                {
                    Adapter.SetAttributeViString(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_DIGITAL_MODULATION_BASE_CLOCK_SOURCE, value);
                }
            }

            public void ConfigureClockSource(string source, DigitalModulationBaseExternalClockType type)
            {
                Adapter.ViSessionStatusCheck(IviRFSigGenInterop.ConfigureDigitalModulationBaseClockSource(Adapter.Session, RFSigGenBaseClockSource.getC_Value(source), RFSigGenDigitalModulationBaseExternalClockType.getC_Value(type)));
            }

            public void CreateBitSequence(string name, int bitCount, byte[] sequence, bool moreDataPending)
            {
                Adapter.ViSessionStatusCheck(IviRFSigGenInterop.WriteDigitalModulationBaseBitSequence(Adapter.Session, name, bitCount, sequence, moreDataPending));
            }

            public DigitalModulationBaseDataSource DataSource
            {
                get
                {
                    return RFSigGenDigitalModulationBaseDataSource.getEnum(Adapter.GetAttributeViInt32(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_DIGITAL_MODULATION_BASE_DATA_SOURCE));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_DIGITAL_MODULATION_BASE_DATA_SOURCE, RFSigGenDigitalModulationBaseDataSource.getC_Value(value));
                }
            }

            public DigitalModulationBaseExternalClockType ExternalClockType
            {
                get
                {
                    return RFSigGenDigitalModulationBaseExternalClockType.getEnum(Adapter.GetAttributeViInt32(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_DIGITAL_MODULATION_BASE_EXTERNAL_CLOCK_TYPE));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_DIGITAL_MODULATION_BASE_EXTERNAL_CLOCK_TYPE, RFSigGenDigitalModulationBaseExternalClockType.getC_Value(value));
                }
            }

            public DigitalModulationBasePrbsType PrbsType
            {
                get
                {
                    return RFSigGenDigitalModulationBasePrbsType.getEnum(Adapter.GetAttributeViInt32(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_DIGITAL_MODULATION_BASE_PRBS_TYPE));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_DIGITAL_MODULATION_BASE_PRBS_TYPE, RFSigGenDigitalModulationBasePrbsType.getC_Value(value));
                }
            }

            public string SelectedBitSequence
            {
                get
                {
                    return Adapter.GetAttributeViString(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_DIGITAL_MODULATION_BASE_SELECTED_BIT_SEQUENCE);
                }
                set
                {
                    Adapter.SetAttributeViString(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_DIGITAL_MODULATION_BASE_SELECTED_BIT_SEQUENCE, value);
                }
            }

            public string SelectedStandard
            {
                get
                {
                    return Adapter.GetAttributeViString(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_DIGITAL_MODULATION_BASE_SELECTED_STANDARD);
                }
                set
                {
                    Adapter.SetAttributeViString(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_DIGITAL_MODULATION_BASE_SELECTED_STANDARD, value);
                }
            }

            public System.Collections.ObjectModel.ReadOnlyCollection<string> StandardNames
            {
                get
                {
                    IList<string> StandardNames = (IList<string>)new ArrayList();
                    int StandardCount = Adapter.GetAttributeViInt32(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_DIGITAL_MODULATION_BASE_STANDARD_COUNT);
                    for(int i = 1; i <= StandardCount; i++)
                    {
                        StringBuilder Name = new StringBuilder(255);
                        Adapter.ViSessionStatusCheck(IviRFSigGenInterop.GetDigitalModulationBaseStandardName(Adapter.Session, i, Name.Capacity, Name));
                        StandardNames.Add(Name.ToString());
                    }

                    return new System.Collections.ObjectModel.ReadOnlyCollection<string>(StandardNames);
                }
            }
        }

        internal class IviRFSigGenCdma : IIviRFSigGenCdma
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviRFSigGen IviRFSigGenInterop = null;
            public IviRFSigGenCdma(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviRFSigGenInterop = (IviCInterop.IviRFSigGen)Adapter.Interop;
            }

            public string ClockSource
            {
                get
                {
                    return RFSigGenCdmaClockSource.getEnum(Adapter.GetAttributeViInt32(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_CDMA_CLOCK_SOURCE));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_DIGITAL_MODULATION_BASE_CLOCK_SOURCE, RFSigGenCdmaClockSource.getC_Value(value));
                }
            }

            public Slope ExternalTriggerSlope
            {
                get
                {
                    return RFSigGenCdmaExternalTriggerSlope.getEnum(Adapter.GetAttributeViInt32(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_CDMA_EXTERNAL_TRIGGER_SLOPE));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_CDMA_EXTERNAL_TRIGGER_SLOPE, RFSigGenCdmaExternalTriggerSlope.getC_Value(value));
                }
            }

            public string SelectedStandard
            {
                get
                {
                    return Adapter.GetAttributeViString(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_CDMA_SELECTED_STANDARD);
                }
                set
                {
                    Adapter.SetAttributeViString(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_CDMA_SELECTED_STANDARD, value);
                }
            }

            public string SelectedTestModel
            {
                get
                {
                    return Adapter.GetAttributeViString(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_CDMA_SELECTED_TEST_MODEL);
                }
                set
                {
                    Adapter.SetAttributeViString(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_CDMA_SELECTED_TEST_MODEL, value);
                }
            }

            public System.Collections.ObjectModel.ReadOnlyCollection<string> StandardNames
            {
                get
                {
                    IList<string> StandardNames = (IList<string>)new ArrayList();
                    int StandardCount = Adapter.GetAttributeViInt32(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_CDMA_STANDARD_COUNT);
                    for (int i = 1; i <= StandardCount; i++)
                    {
                        StringBuilder Name = new StringBuilder(255);
                        Adapter.ViSessionStatusCheck(IviRFSigGenInterop.GetCDMAStandardName(Adapter.Session, i, Name.Capacity, Name));
                        StandardNames.Add(Name.ToString());
                    }

                    return new System.Collections.ObjectModel.ReadOnlyCollection<string>(StandardNames);
                }
            }

            public System.Collections.ObjectModel.ReadOnlyCollection<string> TestModelNames
            {
                get
                {
                    IList<string> TestModelNames = (IList<string>)new ArrayList();
                    int TestModelCount = Adapter.GetAttributeViInt32(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_CDMA_TEST_MODEL_COUNT);
                    for (int i = 1; i <= TestModelCount; i++)
                    {
                        StringBuilder Name = new StringBuilder(255);
                        Adapter.ViSessionStatusCheck(IviRFSigGenInterop.GetCDMATestModelName(Adapter.Session, i, Name.Capacity, Name));
                        TestModelNames.Add(Name.ToString());
                    }

                    return new System.Collections.ObjectModel.ReadOnlyCollection<string>(TestModelNames);
                }
            }

            public string TriggerSource
            {
                get
                {
                    return RFSigGenCdmaTriggerSource.getEnum(Adapter.GetAttributeViInt32(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_DIGITAL_MODULATION_BASE_CLOCK_SOURCE));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_DIGITAL_MODULATION_BASE_CLOCK_SOURCE, RFSigGenCdmaTriggerSource.getC_Value(value));
                }
            }
        }

        internal class IviRFSigGenTdma : IIviRFSigGenTdma
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviRFSigGen IviRFSigGenInterop = null;
            public IviRFSigGenTdma(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviRFSigGenInterop = (IviCInterop.IviRFSigGen)Adapter.Interop;
            }

            public string ClockSource
            {
                get
                {
                    return RFSigGenTdmaClockSource.getEnum(Adapter.GetAttributeViInt32(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_TDMA_CLOCK_SOURCE));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_TDMA_CLOCK_SOURCE, RFSigGenTdmaClockSource.getC_Value(value));
                }
            }

            public void ConfigureClockSource(string source, TdmaExternalClockType type)
            {
                Adapter.ViSessionStatusCheck(IviRFSigGenInterop.ConfigureTDMAClockSource(Adapter.Session, RFSigGenTdmaClockSource.getC_Value(source), RFSigGenTdmaExternalClockType.getC_Value(type)));
            }

            public TdmaExternalClockType ExternalClockType
            {
                get
                {
                    return RFSigGenTdmaExternalClockType.getEnum(Adapter.GetAttributeViInt32(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_TDMA_EXTERNAL_CLOCK_TYPE));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_TDMA_EXTERNAL_CLOCK_TYPE, RFSigGenTdmaExternalClockType.getC_Value(value));
                }
            }

            public Slope ExternalTriggerSlope
            {
                get
                {
                    return RFSigGenTdmaExternalTriggerSlope.getEnum(Adapter.GetAttributeViInt32(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_TDMA_EXTERNAL_TRIGGER_SLOPE));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_TDMA_EXTERNAL_TRIGGER_SLOPE, RFSigGenTdmaExternalTriggerSlope.getC_Value(value));
                }
            }

            public System.Collections.ObjectModel.ReadOnlyCollection<string> FrameNames
            {
                get
                {
                    IList<string> FrameNames = (IList<string>)new ArrayList();
                    int FrameNameCount = Adapter.GetAttributeViInt32(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_TDMA_STANDARD_COUNT);
                    for (int i = 1; i <= FrameNameCount; i++)
                    {
                        StringBuilder Name = new StringBuilder(255);
                        Adapter.ViSessionStatusCheck(IviRFSigGenInterop.GetTDMAFrameName(Adapter.Session, i, Name.Capacity, Name));
                        FrameNames.Add(Name.ToString());
                    }

                    return new System.Collections.ObjectModel.ReadOnlyCollection<string>(FrameNames);
                }
            }

            public string SelectedFrame
            {
                get
                {
                    return Adapter.GetAttributeViString(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_TDMA_SELECTED_FRAME);
                }
                set
                {
                    Adapter.SetAttributeViString(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_TDMA_SELECTED_FRAME, value);
                }
            }

            public string SelectedStandard
            {
                get
                {
                    return Adapter.GetAttributeViString(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_TDMA_SELECTED_STANDARD);
                }
                set
                {
                    Adapter.SetAttributeViString(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_TDMA_SELECTED_STANDARD, value);
                }
            }

            public System.Collections.ObjectModel.ReadOnlyCollection<string> StandardNames
            {
                get
                {
                    IList<string> StandardNames = (IList<string>)new ArrayList();
                    int StandardCount = Adapter.GetAttributeViInt32(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_TDMA_STANDARD_COUNT);
                    for (int i = 1; i <= StandardCount; i++)
                    {
                        StringBuilder Name = new StringBuilder(255);
                        Adapter.ViSessionStatusCheck(IviRFSigGenInterop.GetTDMAStandardName(Adapter.Session, i, Name.Capacity, Name));
                        StandardNames.Add(Name.ToString());
                    }

                    return new System.Collections.ObjectModel.ReadOnlyCollection<string>(StandardNames);
                }
            }

            public string TriggerSource
            {
                get
                {
                    return RFSigGenTdmaTriggerSource.getEnum(Adapter.GetAttributeViInt32(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_TDMA_TRIGGER_SOURCE));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_TDMA_TRIGGER_SOURCE, RFSigGenTdmaTriggerSource.getC_Value(value));
                }
            }
        }

        #endregion

        internal class IviRFSigGenIQ : IIviRFSigGenIQ
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviRFSigGen IviRFSigGenInterop = null;
            private IIviRFSigGenIQImpairment RFSigGenIQImpairment = null;
            public IviRFSigGenIQ(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviRFSigGenInterop = (IviCInterop.IviRFSigGen)Adapter.Interop;
                RFSigGenIQImpairment = new IviRFSigGenIQImpairment(Adapter);
            }

            public void Calibrate()
            {
                Adapter.ViSessionStatusCheck(IviRFSigGenInterop.CalibrateIQ(Adapter.Session));
            }

            public void Configure(IQSource source, bool swapEnabled)
            {
                Adapter.ViSessionStatusCheck(IviRFSigGenInterop.ConfigureIQ(Adapter.Session, RFSigGenIQSource.getC_Value(source), swapEnabled));
            }

            public bool Enabled
            {
                get
                {
                    return Adapter.GetAttributeViBoolean(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_IQ_ENABLED);
                }
                set
                {
                    Adapter.SetAttributeViBoolean(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_IQ_ENABLED, value);
                }
            }

            public IIviRFSigGenIQImpairment Impairment
            {
                get { return RFSigGenIQImpairment; }
            }

            public double NominalVoltage
            {
                get { return Adapter.GetAttributeViReal64(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_IQ_NOMINAL_VOLTAGE); }
            }

            public IQSource Source
            {
                get
                {
                    return RFSigGenIQSource.getEnum(Adapter.GetAttributeViInt32(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_IQ_SOURCE));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_IQ_SOURCE, RFSigGenIQSource.getC_Value(value));
                }
            }

            public bool SwapEnabled
            {
                get
                {
                    return Adapter.GetAttributeViBoolean(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_IQ_SWAP_ENABLED);
                }
                set
                {
                    Adapter.SetAttributeViBoolean(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_IQ_SWAP_ENABLED, value);
                }
            }
        }

        internal class IviRFSigGenIQImpairment : IIviRFSigGenIQImpairment
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviRFSigGen IviRFSigGenInterop = null;
            public IviRFSigGenIQImpairment(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviRFSigGenInterop = (IviCInterop.IviRFSigGen)Adapter.Interop;
            }

            public void Configure(double iOffset, double qOffset, double ratio, double skew)
            {
                Adapter.ViSessionStatusCheck(IviRFSigGenInterop.ConfigureIQImpairment(Adapter.Session, iOffset, qOffset, ratio, skew));
            }

            public bool Enabled
            {
                get
                {
                    return Adapter.GetAttributeViBoolean(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_IQ_IMPAIRMENT_ENABLED);
                }
                set
                {
                    Adapter.SetAttributeViBoolean(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_IQ_IMPAIRMENT_ENABLED, value);
                }
            }

            public double IOffset
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_IQ_I_OFFSET);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_IQ_I_OFFSET, value);
                }
            }

            public double QOffset
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_IQ_Q_OFFSET);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_IQ_Q_OFFSET, value);
                }
            }

            public double Ratio
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_IQ_RATIO);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_IQ_RATIO, value);
                }
            }

            public double Skew
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_IQ_SKEW);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_IQ_SKEW, value);
                }
            }
        }

        internal class IviRFSigGenLFGenerator : IIviRFSigGenLFGenerator
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviRFSigGen IviRFSigGenInterop = null;
            private IIviRFSigGenLFGeneratorOutput RFSigGenLFGeneratorOutput = null;
            public IviRFSigGenLFGenerator(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviRFSigGenInterop = (IviCInterop.IviRFSigGen)Adapter.Interop;
                RFSigGenLFGeneratorOutput = new IviRFSigGenLFGeneratorOutput(Adapter);
            }

            public string ActiveLFGenerator
            {
                get
                {
                    return Adapter.GetAttributeViString(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_ACTIVE_LF_GENERATOR);
                }
                set
                {
                    Adapter.SetAttributeViString(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_ACTIVE_LF_GENERATOR, value);
                }
            }

            public void Configure(double frequency, LFGeneratorWaveform waveform)
            {
                Adapter.ViSessionStatusCheck(IviRFSigGenInterop.ConfigureLFGenerator(Adapter.Session, frequency, RFSigGenLFGeneratorWaveform.getC_Value(waveform)));
            }

            public int Count
            {
                get { return Adapter.GetAttributeViInt32(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_LF_GENERATOR_COUNT); }
            }

            public double Frequency
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_LF_GENERATOR_FREQUENCY);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_LF_GENERATOR_FREQUENCY, value);
                }
            }

            public string GetName(int index)
            {
                StringBuilder Name = new StringBuilder(255);
                Adapter.ViSessionStatusCheck(IviRFSigGenInterop.GetLFGeneratorName(Adapter.Session, index, Name.Capacity, Name));
                return Name.ToString();
            }

            public IIviRFSigGenLFGeneratorOutput Output
            {
                get { return RFSigGenLFGeneratorOutput; }
            }

            public LFGeneratorWaveform Waveform
            {
                get
                {
                    return RFSigGenLFGeneratorWaveform.getEnum(Adapter.GetAttributeViInt32(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_LF_GENERATOR_WAVEFORM));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_LF_GENERATOR_WAVEFORM, RFSigGenLFGeneratorWaveform.getC_Value(value));
                }
            }
        }

        internal class IviRFSigGenLFGeneratorOutput : IIviRFSigGenLFGeneratorOutput
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviRFSigGen IviRFSigGenInterop = null;
            public IviRFSigGenLFGeneratorOutput(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviRFSigGenInterop = (IviCInterop.IviRFSigGen)Adapter.Interop;
            }

            public double Amplitude
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_LF_GENERATOR_OUTPUT_AMPLITUDE);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_LF_GENERATOR_OUTPUT_AMPLITUDE, value);
                }
            }

            public void Configure(double amplitude, bool enabled)
            {
                Adapter.ViSessionStatusCheck(IviRFSigGenInterop.ConfigureLFGeneratorOutput(Adapter.Session, amplitude, enabled));
            }

            public bool Enabled
            {
                get
                {
                    return Adapter.GetAttributeViBoolean(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_LF_GENERATOR_OUTPUT_ENABLED);
                }
                set
                {
                    Adapter.SetAttributeViBoolean(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_LF_GENERATOR_OUTPUT_ENABLED, value);
                }
            }
        }

        internal class IviRFSigGenPulseGenerator : IIviRFSigGenPulseGenerator
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviRFSigGen IviRFSigGenInterop = null;
            private IIviRFSigGenPulseGeneratorDouble RFSigGenPulseGeneratorDouble = null;
            private IIviRFSigGenPulseGeneratorOutput RFSigGenPulseGeneratorOutput = null;
            public IviRFSigGenPulseGenerator(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviRFSigGenInterop = (IviCInterop.IviRFSigGen)Adapter.Interop;
                RFSigGenPulseGeneratorDouble = new IviRFSigGenPulseGeneratorDouble(Adapter);
                RFSigGenPulseGeneratorOutput = new IviRFSigGenPulseGeneratorOutput(Adapter);
            }

            public void Configure(string pulseTriggerSource, Ivi.Driver.PrecisionTimeSpan pulseWidth, bool gatingEnabled)
            {
                Adapter.ViSessionStatusCheck(IviRFSigGenInterop.ConfigurePulse(Adapter.Session, RFSigGenPulseGeneratorTriggerSource.getC_Value(pulseTriggerSource), pulseWidth.TotalSeconds, gatingEnabled));
            }

            public void ConfigureExternalTrigger(Slope slope, Ivi.Driver.PrecisionTimeSpan delay)
            {
                Adapter.ViSessionStatusCheck(IviRFSigGenInterop.ConfigurePulseExternalTrigger(Adapter.Session, RFSigGenPulseGeneratorExternalTriggerSlope.getC_Value(slope), delay.TotalSeconds));
            }

            public IIviRFSigGenPulseGeneratorDouble DoublePulse
            {
                get { return RFSigGenPulseGeneratorDouble; }
            }

            public Ivi.Driver.PrecisionTimeSpan ExternalTriggerDelay
            {
                get
                {
                    return new Ivi.Driver.PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * Adapter.GetAttributeViReal64(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_PULSE_EXTERNAL_TRIGGER_DELAY))));
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_PULSE_EXTERNAL_TRIGGER_DELAY, value.TotalSeconds);
                }
            }

            public Slope ExternalTriggerSlope
            {
                get
                {
                    return RFSigGenPulseGeneratorExternalTriggerSlope.getEnum(Adapter.GetAttributeViInt32(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_PULSE_EXTERNAL_TRIGGER_SLOPE));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_PULSE_EXTERNAL_TRIGGER_SLOPE, RFSigGenPulseGeneratorExternalTriggerSlope.getC_Value(value));
                }
            }

            public bool GatingEnabled
            {
                get
                {
                    return Adapter.GetAttributeViBoolean(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_PULSE_GATING_ENABLED);
                }
                set
                {
                    Adapter.SetAttributeViBoolean(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_PULSE_GATING_ENABLED, value);
                }
            }

            public Ivi.Driver.PrecisionTimeSpan InternalTriggerPeriod
            {
                get
                {
                    return new Ivi.Driver.PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * Adapter.GetAttributeViReal64(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_PULSE_INTERNAL_TRIGGER_PERIOD))));
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_PULSE_INTERNAL_TRIGGER_PERIOD, value.TotalSeconds);
                }
            }

            public IIviRFSigGenPulseGeneratorOutput Output
            {
                get { return RFSigGenPulseGeneratorOutput; }
            }

            public string TriggerSource
            {
                get
                {
                    return RFSigGenPulseGeneratorTriggerSource.getEnum(Adapter.GetAttributeViInt32(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_PULSE_TRIGGER_SOURCE));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_PULSE_TRIGGER_SOURCE, RFSigGenPulseGeneratorTriggerSource.getC_Value(value));
                }
            }

            public Ivi.Driver.PrecisionTimeSpan Width
            {
                get
                {
                    return new Ivi.Driver.PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * Adapter.GetAttributeViReal64(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_PULSE_WIDTH))));
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_PULSE_WIDTH, value.TotalSeconds);
                }
            }
        }

        internal class IviRFSigGenPulseGeneratorDouble : IIviRFSigGenPulseGeneratorDouble
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviRFSigGen IviRFSigGenInterop = null;
            public IviRFSigGenPulseGeneratorDouble(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviRFSigGenInterop = (IviCInterop.IviRFSigGen)Adapter.Interop;
            }

            public void Configure(bool enabled, Ivi.Driver.PrecisionTimeSpan delay)
            {
                Adapter.ViSessionStatusCheck(IviRFSigGenInterop.ConfigurePulseDouble(Adapter.Session, enabled, delay.TotalSeconds));
            }

            public Ivi.Driver.PrecisionTimeSpan Delay
            {
                get
                {
                    return new Ivi.Driver.PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * Adapter.GetAttributeViReal64(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_PULSE_DOUBLE_DELAY))));
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_PULSE_DOUBLE_DELAY, value.TotalSeconds);
                }
            }

            public bool Enabled
            {
                get
                {
                    return Adapter.GetAttributeViBoolean(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_PULSE_DOUBLE_ENABLED);
                }
                set
                {
                    Adapter.SetAttributeViBoolean(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_PULSE_DOUBLE_ENABLED, value);
                }
            }
        }

        internal class IviRFSigGenPulseGeneratorOutput : IIviRFSigGenPulseGeneratorOutput
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviRFSigGen IviRFSigGenInterop = null;
            public IviRFSigGenPulseGeneratorOutput(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviRFSigGenInterop = (IviCInterop.IviRFSigGen)Adapter.Interop;
            }

            public void Configure(PulseOutputPolarity polarity, bool enabled)
            {
                Adapter.ViSessionStatusCheck(IviRFSigGenInterop.ConfigurePulseOutput(Adapter.Session, RFSigGenPulseOutputPolarity.getC_Value(polarity), enabled));
            }

            public bool Enabled
            {
                get
                {
                    return Adapter.GetAttributeViBoolean(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_PULSE_OUTPUT_ENABLED);
                }
                set
                {
                    Adapter.SetAttributeViBoolean(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_PULSE_OUTPUT_ENABLED, value);
                }
            }

            public PulseOutputPolarity Polarity
            {
                get
                {
                    return RFSigGenPulseOutputPolarity.getEnum(Adapter.GetAttributeViInt32(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_PULSE_OUTPUT_POLARITY));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_PULSE_OUTPUT_POLARITY, RFSigGenPulseOutputPolarity.getC_Value(value));
                }
            }
        }

        internal class IviRFSigGenPulseModulation : IIviRFSigGenPulseModulation
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviRFSigGen IviRFSigGenInterop = null;
            public IviRFSigGenPulseModulation(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviRFSigGenInterop = (IviCInterop.IviRFSigGen)Adapter.Interop;
            }

            public bool Enabled
            {
                get
                {
                    return Adapter.GetAttributeViBoolean(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_PULSE_MODULATION_ENABLED);
                }
                set
                {
                    Adapter.SetAttributeViBoolean(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_PULSE_MODULATION_ENABLED, value);
                }
            }

            public PulseModulationExternalPolarity ExternalPolarity
            {
                get
                {
                    return RFSigGenPulseModulationExternalPolarity.getEnum(Adapter.GetAttributeViInt32(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_PULSE_MODULATION_EXTERNAL_POLARITY));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_PULSE_MODULATION_EXTERNAL_POLARITY, RFSigGenPulseModulationExternalPolarity.getC_Value(value));
                }
            }

            public PulseModulationSource Source
            {
                get
                {
                    return RFSigGenPulseModulationSource.getEnum(Adapter.GetAttributeViInt32(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_PULSE_MODULATION_SOURCE));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_PULSE_MODULATION_SOURCE, RFSigGenPulseModulationSource.getC_Value(value));
                }
            }
        }

        internal class IviRFSigGenRF : IIviRFSigGenRF
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviRFSigGen IviRFSigGenInterop = null;
            public IviRFSigGenRF(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviRFSigGenInterop = (IviCInterop.IviRFSigGen)Adapter.Interop;
            }

            public void Configure(double frequency, double powerLevel)
            {
                Adapter.ViSessionStatusCheck(IviRFSigGenInterop.ConfigureRF(Adapter.Session, frequency, powerLevel));
            }

            public void DisableAllModulation()
            {
                Adapter.ViSessionStatusCheck(IviRFSigGenInterop.DisableAllModulation(Adapter.Session));
            }

            public double Frequency
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_FREQUENCY);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_FREQUENCY, value);
                }
            }

            public bool IsSettled()
            {
                bool Done = false;
                Adapter.ViSessionStatusCheck(IviRFSigGenInterop.IsSettled(Adapter.Session, ref Done));
                return Done;
            }

            public double Level
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_POWER_LEVEL);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_POWER_LEVEL, value);
                }
            }

            public bool OutputEnabled
            {
                get
                {
                    return Adapter.GetAttributeViBoolean(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_OUTPUT_ENABLED);
                }
                set
                {
                    Adapter.SetAttributeViBoolean(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_OUTPUT_ENABLED, value);
                }
            }

            public void WaitUntilSettled(Ivi.Driver.PrecisionTimeSpan maximumTime)
            {
                Adapter.ViSessionStatusCheck(IviRFSigGenInterop.WaitUntilSettled(Adapter.Session, (int)maximumTime.TotalMilliseconds));
            }
        }

        internal class IviRFSigGenReferenceOscillator : IIviRFSigGenReferenceOscillator
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviRFSigGen IviRFSigGenInterop = null;
            public IviRFSigGenReferenceOscillator(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviRFSigGenInterop = (IviCInterop.IviRFSigGen)Adapter.Interop;
            }

            public void Configure(string source, double frequency)
            {
                Adapter.ViSessionStatusCheck(IviRFSigGenInterop.ConfigureReferenceOscillator(Adapter.Session, RFSigGenReferenceOscillatorSource.getC_Value(source), frequency));
            }

            public double ExternalFrequency
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_REFERENCE_OSCILLATOR_EXTERNAL_FREQUENCY);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_REFERENCE_OSCILLATOR_EXTERNAL_FREQUENCY, value);
                }
            }

            public string Source
            {
                get
                {
                    return RFSigGenReferenceOscillatorSource.getEnum(Adapter.GetAttributeViInt32(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_REFERENCE_OSCILLATOR_SOURCE));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_REFERENCE_OSCILLATOR_SOURCE, RFSigGenReferenceOscillatorSource.getC_Value(value));
                }
            }
        }

        internal class IviRFSigGenSweep : IIviRFSigGenSweep
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviRFSigGen IviRFSigGenInterop = null;
            private IIviRFSigGenFrequencyStep RFSigGenFrequencyStep = null;
            private IIviRFSigGenFrequencySweep RFSigGenFrequencySweep = null;
            private IIviRFSigGenList RFSigGenList = null;
            private IIviRFSigGenPowerStep RFSigGenPowerStep = null;
            private IIviRFSigGenPowerSweep RFSigGenPowerSweep = null;
            public IviRFSigGenSweep(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviRFSigGenInterop = (IviCInterop.IviRFSigGen)Adapter.Interop;
                RFSigGenFrequencyStep = new IviRFSigGenFrequencyStep(Adapter);
                RFSigGenFrequencySweep = new IviRFSigGenFrequencySweep(Adapter);
                RFSigGenList = new IviRFSigGenList(Adapter);
                RFSigGenPowerStep = new IviRFSigGenPowerStep(Adapter);
                RFSigGenPowerSweep = new IviRFSigGenPowerSweep(Adapter);
            }

            public void Configure(SweepMode mode, string triggerSource)
            {
                Adapter.ViSessionStatusCheck(IviRFSigGenInterop.ConfigureSweep(Adapter.Session, RFSigGenSweepMode.getC_Value(mode), RFSigGenSweepTriggerSource.getC_Value(triggerSource)));
            }

            public IIviRFSigGenFrequencyStep FrequencyStep
            {
                get { return RFSigGenFrequencyStep; }
            }

            public IIviRFSigGenFrequencySweep FrequencySweep
            {
                get { return RFSigGenFrequencySweep; }
            }

            public IIviRFSigGenList List
            {
                get { return RFSigGenList; }
            }

            public SweepMode Mode
            {
                get
                {
                    return RFSigGenSweepMode.getEnum(Adapter.GetAttributeViInt32(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_SWEEP_MODE));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_SWEEP_MODE, RFSigGenSweepMode.getC_Value(value));
                }
            }

            public IIviRFSigGenPowerStep PowerStep
            {
                get { return RFSigGenPowerStep; }
            }

            public IIviRFSigGenPowerSweep PowerSweep
            {
                get { return RFSigGenPowerSweep; }
            }

            public string TriggerSource
            {
                get
                {
                    return RFSigGenSweepTriggerSource.getEnum(Adapter.GetAttributeViInt32(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_SWEEP_TRIGGER_SOURCE));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_SWEEP_TRIGGER_SOURCE, RFSigGenSweepTriggerSource.getC_Value(value));
                }
            }
        }

        internal class IviRFSigGenFrequencyStep : IIviRFSigGenFrequencyStep
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviRFSigGen IviRFSigGenInterop = null;
            public IviRFSigGenFrequencyStep(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviRFSigGenInterop = (IviCInterop.IviRFSigGen)Adapter.Interop;
            }

            public void ConfigureDwell(bool singleStepEnabled, Ivi.Driver.PrecisionTimeSpan dwell)
            {
                Adapter.ViSessionStatusCheck(IviRFSigGenInterop.ConfigureFrequencyStepDwell(Adapter.Session, singleStepEnabled, dwell.TotalSeconds));
            }

            public void ConfigureStartStop(double start, double stop, FrequencyStepScaling scaling, double stepSize)
            {
                Adapter.ViSessionStatusCheck(IviRFSigGenInterop.ConfigureFrequencyStepStartStop(Adapter.Session, start, stop, RFSigGenFrequencyStepScaling.getC_Value(scaling), stepSize));
            }

            public Ivi.Driver.PrecisionTimeSpan Dwell
            {
                get
                {
                    return new Ivi.Driver.PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * Adapter.GetAttributeViReal64(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_FREQUENCY_STEP_DWELL))));
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_FREQUENCY_STEP_DWELL, value.TotalSeconds);
                }
            }

            public void Reset()
            {
                Adapter.ViSessionStatusCheck(IviRFSigGenInterop.ResetFrequencyStep(Adapter.Session));
            }

            public FrequencyStepScaling Scaling
            {
                get
                {
                    return RFSigGenFrequencyStepScaling.getEnum(Adapter.GetAttributeViInt32(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_FREQUENCY_STEP_SCALING));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_FREQUENCY_STEP_SCALING, RFSigGenFrequencyStepScaling.getC_Value(value));
                }
            }

            public bool SingleStepEnabled
            {
                get
                {
                    return Adapter.GetAttributeViBoolean(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_FREQUENCY_STEP_SINGLE_STEP_ENABLED);
                }
                set
                {
                    Adapter.SetAttributeViBoolean(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_FREQUENCY_STEP_SINGLE_STEP_ENABLED, value);
                }
            }

            public double Size
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_FREQUENCY_STEP_SIZE);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_FREQUENCY_STEP_SIZE, value);
                }
            }

            public double Start
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_FREQUENCY_STEP_START);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_FREQUENCY_STEP_START, value);
                }
            }

            public double Stop
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_FREQUENCY_STEP_STOP);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_FREQUENCY_STEP_STOP, value);
                }
            }
        }

        internal class IviRFSigGenFrequencySweep : IIviRFSigGenFrequencySweep
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviRFSigGen IviRFSigGenInterop = null;
            public IviRFSigGenFrequencySweep(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviRFSigGenInterop = (IviCInterop.IviRFSigGen)Adapter.Interop;
            }

            public void ConfigureCenterSpan(double center, double span)
            {
                Adapter.ViSessionStatusCheck(IviRFSigGenInterop.ConfigureFrequencySweepCenterSpan(Adapter.Session, center, span));
            }

            public void ConfigureStartStop(double start, double stop)
            {
                Adapter.ViSessionStatusCheck(IviRFSigGenInterop.ConfigureFrequencySweepStartStop(Adapter.Session, start, stop));
            }

            public double Start
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_FREQUENCY_SWEEP_START);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_FREQUENCY_SWEEP_START, value);
                }
            }

            public double Stop
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_FREQUENCY_SWEEP_STOP);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_FREQUENCY_SWEEP_STOP, value);
                }
            }

            public Ivi.Driver.PrecisionTimeSpan Time
            {
                get
                {
                    return new Ivi.Driver.PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * Adapter.GetAttributeViReal64(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_FREQUENCY_SWEEP_TIME))));
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_FREQUENCY_SWEEP_TIME, value.TotalSeconds);
                }
            }
        }

        internal class IviRFSigGenList : IIviRFSigGenList
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviRFSigGen IviRFSigGenInterop = null;
            public IviRFSigGenList(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviRFSigGenInterop = (IviCInterop.IviRFSigGen)Adapter.Interop;
            }

            public void ClearAll()
            {
                Adapter.ViSessionStatusCheck(IviRFSigGenInterop.ClearAllLists(Adapter.Session));
            }

            public void ConfigureDwell(bool singleStepEnabled, Ivi.Driver.PrecisionTimeSpan dwell)
            {
                Adapter.ViSessionStatusCheck(IviRFSigGenInterop.ConfigureListDwell(Adapter.Session, singleStepEnabled, dwell.TotalSeconds));
            }

            public void CreateFrequency(string name, double[] frequency)
            {
                Adapter.ViSessionStatusCheck(IviRFSigGenInterop.CreateFrequencyList(Adapter.Session, name, frequency.Length, frequency));
            }

            public void CreateFrequencyPower(string name, double[] frequency, double[] power)
            {
                Adapter.ViSessionStatusCheck(IviRFSigGenInterop.CreateFrequencyPowerList(Adapter.Session, name, frequency.Length, frequency, power));
            }

            public void CreatePower(string name, double[] power)
            {
                Adapter.ViSessionStatusCheck(IviRFSigGenInterop.CreatePowerList(Adapter.Session, name, power.Length, power));
            }

            public Ivi.Driver.PrecisionTimeSpan Dwell
            {
                get
                {
                    return new Ivi.Driver.PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * Adapter.GetAttributeViReal64(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_LIST_DWELL))));
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_LIST_DWELL, value.TotalSeconds);
                }
            }

            public void Reset()
            {
                Adapter.ViSessionStatusCheck(IviRFSigGenInterop.ResetList(Adapter.Session));
            }

            public string SelectedList
            {
                get
                {
                    return Adapter.GetAttributeViString(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_LIST_SELECTED_NAME);
                }
                set
                {
                    Adapter.SetAttributeViString(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_LIST_SELECTED_NAME, value);
                }
            }

            public bool SingleStepEnabled
            {
                get
                {
                    return Adapter.GetAttributeViBoolean(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_LIST_SINGLE_STEP_ENABLED);
                }
                set
                {
                    Adapter.SetAttributeViBoolean(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_LIST_SINGLE_STEP_ENABLED, value);
                }
            }
        }

        internal class IviRFSigGenPowerStep : IIviRFSigGenPowerStep
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviRFSigGen IviRFSigGenInterop = null;
            public IviRFSigGenPowerStep(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviRFSigGenInterop = (IviCInterop.IviRFSigGen)Adapter.Interop;
            }

            public void ConfigureDwell(bool singleStepEnabled, Ivi.Driver.PrecisionTimeSpan dwell)
            {
                Adapter.ViSessionStatusCheck(IviRFSigGenInterop.ConfigurePowerStepDwell(Adapter.Session, singleStepEnabled, dwell.TotalSeconds));
            }

            public void ConfigureStartStop(double start, double stop, double stepSize)
            {
                Adapter.ViSessionStatusCheck(IviRFSigGenInterop.ConfigurePowerStepStartStop(Adapter.Session, start, stop, stepSize));
            }

            public Ivi.Driver.PrecisionTimeSpan Dwell
            {
                get
                {
                    return new Ivi.Driver.PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * Adapter.GetAttributeViReal64(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_POWER_STEP_DWELL))));
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_POWER_STEP_DWELL, value.TotalSeconds);
                }
            }

            public void Reset()
            {
                Adapter.ViSessionStatusCheck(IviRFSigGenInterop.ResetPowerStep(Adapter.Session));
            }

            public bool SingleStepEnabled
            {
                get
                {
                    return Adapter.GetAttributeViBoolean(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_POWER_STEP_SINGLE_STEP_ENABLED);
                }
                set
                {
                    Adapter.SetAttributeViBoolean(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_POWER_STEP_SINGLE_STEP_ENABLED, value);
                }
            }

            public double Size
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_POWER_STEP_SIZE);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_POWER_STEP_SIZE, value);
                }
            }

            public double Start
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_POWER_STEP_START);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_POWER_STEP_START, value);
                }
            }

            public double Stop
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_POWER_STEP_STOP);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_POWER_STEP_STOP, value);
                }
            }
        }

        internal class IviRFSigGenPowerSweep : IIviRFSigGenPowerSweep
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviRFSigGen IviRFSigGenInterop = null;
            public IviRFSigGenPowerSweep(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviRFSigGenInterop = (IviCInterop.IviRFSigGen)Adapter.Interop;
            }

            public void ConfigureStartStop(double start, double stop)
            {
                Adapter.ViSessionStatusCheck(IviRFSigGenInterop.ConfigurePowerSweepStartStop(Adapter.Session, start, stop));
            }

            public double Start
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_POWER_SWEEP_START);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_POWER_SWEEP_START, value);
                }
            }

            public double Stop
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_POWER_SWEEP_STOP);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_POWER_SWEEP_STOP, value);
                }
            }

            public Ivi.Driver.PrecisionTimeSpan Time
            {
                get
                {
                    return new Ivi.Driver.PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * Adapter.GetAttributeViReal64(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_POWER_SWEEP_TIME))));
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviRFSigGenAttribute.IVIRFSIGGEN_ATTR_POWER_SWEEP_TIME, value.TotalSeconds);
                }
            }
        }
    }
}
