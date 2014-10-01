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
using Ivi.Upconverter;
using IVI.C.NET.Adapter.IviCInterop;

namespace IVI.C.NET.Adapter
{
    public class IviUpconverterAdapter : DriverAdapterBase<IviCInterop.IviUpconverter>, IIviUpconverter
    {
        #region Enum Mapping

        private static IviEnumCMapping<AlcSource, int> UpconverterAlcSource = IviEnumCMapping<AlcSource, int>.Instance
            .Map(AlcSource.Internal, IviUpconverterAttribute.IVIUPCONVERTER_VAL_ALC_SOURCE_INTERNAL)
            .Map(AlcSource.External, IviUpconverterAttribute.IVIUPCONVERTER_VAL_ALC_SOURCE_EXTERNAL);

        private static IviEnumCMapping<ExternalCoupling, int> UpconverterAMExternalCoupling = IviEnumCMapping<ExternalCoupling, int>.Instance
            .Map(ExternalCoupling.AC, IviUpconverterAttribute.IVIUPCONVERTER_VAL_AM_EXTERNAL_COUPLING_AC)
            .Map(ExternalCoupling.DC, IviUpconverterAttribute.IVIUPCONVERTER_VAL_AM_EXTERNAL_COUPLING_DC);

        private static IviEnumCMapping<Scaling, int> UpconverterAMScaling = IviEnumCMapping<Scaling, int>.Instance
            .Map(Scaling.Linear, IviUpconverterAttribute.IVIUPCONVERTER_VAL_AM_SCALING_LINEAR)
            .Map(Scaling.Logarithmic, IviUpconverterAttribute.IVIUPCONVERTER_VAL_AM_SCALING_LOGARITHMIC);

        private static IviEnumCMapping<ExternalCoupling, int> UpconverterFMExternalCoupling = IviEnumCMapping<ExternalCoupling, int>.Instance
            .Map(ExternalCoupling.AC, IviUpconverterAttribute.IVIUPCONVERTER_VAL_FM_EXTERNAL_COUPLING_AC)
            .Map(ExternalCoupling.DC, IviUpconverterAttribute.IVIUPCONVERTER_VAL_FM_EXTERNAL_COUPLING_DC);

        private static IviEnumCMapping<ExternalCoupling, int> UpconverterPMExternalCoupling = IviEnumCMapping<ExternalCoupling, int>.Instance
            .Map(ExternalCoupling.AC, IviUpconverterAttribute.IVIUPCONVERTER_VAL_PM_EXTERNAL_COUPLING_AC)
            .Map(ExternalCoupling.DC, IviUpconverterAttribute.IVIUPCONVERTER_VAL_PM_EXTERNAL_COUPLING_DC);

        private static IviEnumCMapping<ExternalCoupling, int> UpconverterIFInputExternalCoupling = IviEnumCMapping<ExternalCoupling, int>.Instance
            .Map(ExternalCoupling.AC, IviUpconverterAttribute.IVIUPCONVERTER_VAL_IF_INPUT_COUPLING_AC)
            .Map(ExternalCoupling.DC, IviUpconverterAttribute.IVIUPCONVERTER_VAL_IF_INPUT_COUPLING_DC);

        private static IviEnumCMapping<PulseModulationExternalPolarity, int> UpconverterPulseModulationExternalPolarity = IviEnumCMapping<PulseModulationExternalPolarity, int>.Instance
            .Map(PulseModulationExternalPolarity.Normal, IviUpconverterAttribute.IVIUPCONVERTER_VAL_PULSE_MODULATION_EXTERNAL_POLARITY_NORMAL )
            .Map(PulseModulationExternalPolarity.Inverse, IviUpconverterAttribute.IVIUPCONVERTER_VAL_PULSE_MODULATION_EXTERNAL_POLARITY_INVERSE );

        private static IviEnumCMapping<CalibrationStatus, int> UpconverterCalibrationStatus = IviEnumCMapping<CalibrationStatus, int>.Instance
            .Map(CalibrationStatus.Complete, IviUpconverterAttribute.IVIUPCONVERTER_VAL_CALIBRATION_COMPLETE)
            .Map(CalibrationStatus.InProgress, IviUpconverterAttribute.IVIUPCONVERTER_VAL_CALIBRATION_IN_PROGRESS)
            .Map(CalibrationStatus.StatusUnknown, IviUpconverterAttribute.IVIUPCONVERTER_VAL_CALIBRATION_STATUS_UNKNOWN)
            .Map(CalibrationStatus.Failed, IviUpconverterAttribute.IVIUPCONVERTER_VAL_CALIBRATION_FAILED);

        private static IviEnumCMapping<ReferenceOscillatorSource, int> UpconverterReferenceOscillatorSource = IviEnumCMapping<ReferenceOscillatorSource, int>.Instance
            .Map(ReferenceOscillatorSource.Internal, IviUpconverterAttribute.IVIUPCONVERTER_VAL_REFERENCE_OSCILLATOR_SOURCE_INTERNAL)
            .Map(ReferenceOscillatorSource.External, IviUpconverterAttribute.IVIUPCONVERTER_VAL_REFERENCE_OSCILLATOR_SOURCE_EXTERNAL);

        private static IviEnumCMapping<SweepMode, int> UpconverterSweepMode = IviEnumCMapping<SweepMode, int>.Instance
            .Map(SweepMode.None, IviUpconverterAttribute.IVIUPCONVERTER_VAL_SWEEP_MODE_NONE)
            .Map(SweepMode.FrequencySweep, IviUpconverterAttribute.IVIUPCONVERTER_VAL_SWEEP_MODE_FREQUENCY_SWEEP)
            .Map(SweepMode.PowerSweep, IviUpconverterAttribute.IVIUPCONVERTER_VAL_SWEEP_MODE_POWER_SWEEP)
            .Map(SweepMode.GainSweep, IviUpconverterAttribute.IVIUPCONVERTER_VAL_SWEEP_MODE_GAIN_SWEEP)
            .Map(SweepMode.FrequencyStep, IviUpconverterAttribute.IVIUPCONVERTER_VAL_SWEEP_MODE_FREQUENCY_STEP)
            .Map(SweepMode.PowerStep, IviUpconverterAttribute.IVIUPCONVERTER_VAL_SWEEP_MODE_POWER_STEP)
            .Map(SweepMode.GainStep, IviUpconverterAttribute.IVIUPCONVERTER_VAL_SWEEP_MODE_GAIN_STEP)
            .Map(SweepMode.List, IviUpconverterAttribute.IVIUPCONVERTER_VAL_SWEEP_MODE_LIST);

        private static IviEnumCMapping<Scaling, int> UpconverterFrequencyStepScaling = IviEnumCMapping<Scaling, int>.Instance
            .Map(Scaling.Linear, IviUpconverterAttribute.IVIUPCONVERTER_VAL_FREQUENCY_STEP_SCALING_LINEAR)
            .Map(Scaling.Logarithmic, IviUpconverterAttribute.IVIUPCONVERTER_VAL_FREQUENCY_STEP_SCALING_LOGARITHMIC);

        //private static IviEnumCMapping<string, int>  UpconverterSweepTriggerSource = IviEnumCMapping<string, int>.Instance
        //    .Map("", IviUpconverterAttribute.IVIUPCONVERTER_VAL_TRIGGER_SOURCE_NONE)
        //    .Map("Immediate", IviUpconverterAttribute.IVIUPCONVERTER_VAL_TRIGGER_SOURCE_IMMEDIATE)
        //    .Map("External", IviUpconverterAttribute.IVIUPCONVERTER_VAL_TRIGGER_SOURCE_EXTERNAL)
        //    .Map("Internal", IviUpconverterAttribute.IVIUPCONVERTER_VAL_TRIGGER_SOURCE_INTERNAL)
        //    .Map("Software", IviUpconverterAttribute.IVIUPCONVERTER_VAL_TRIGGER_SOURCE_SOFTWARE)
        //    .Map("LAN0", IviUpconverterAttribute.IVIUPCONVERTER_VAL_TRIGGER_SOURCE_LAN0)
        //    .Map("LAN1", IviUpconverterAttribute.IVIUPCONVERTER_VAL_TRIGGER_SOURCE_LAN1)
        //    .Map("LAN2", IviUpconverterAttribute.IVIUPCONVERTER_VAL_TRIGGER_SOURCE_LAN2)
        //    .Map("LAN3", IviUpconverterAttribute.IVIUPCONVERTER_VAL_TRIGGER_SOURCE_LAN3)
        //    .Map("LAN4", IviUpconverterAttribute.IVIUPCONVERTER_VAL_TRIGGER_SOURCE_LAN4)
        //    .Map("LAN5", IviUpconverterAttribute.IVIUPCONVERTER_VAL_TRIGGER_SOURCE_LAN5)
        //    .Map("LAN6", IviUpconverterAttribute.IVIUPCONVERTER_VAL_TRIGGER_SOURCE_LAN6)
        //    .Map("LAN7", IviUpconverterAttribute.IVIUPCONVERTER_VAL_TRIGGER_SOURCE_LAN7)
        //    .Map("LXI0", IviUpconverterAttribute.IVIUPCONVERTER_VAL_TRIGGER_SOURCE_LXI0)
        //    .Map("LXI1", IviUpconverterAttribute.IVIUPCONVERTER_VAL_TRIGGER_SOURCE_LXI1)
        //    .Map("LXI2", IviUpconverterAttribute.IVIUPCONVERTER_VAL_TRIGGER_SOURCE_LXI2
        //    .Map("LXI3", IviUpconverterAttribute.IVIUPCONVERTER_VAL_TRIGGER_SOURCE_LXI3)
        //    .Map("LXI4", IviUpconverterAttribute.IVIUPCONVERTER_VAL_TRIGGER_SOURCE_LXI4)
        //    .Map("LXI5", IviUpconverterAttribute.IVIUPCONVERTER_VAL_TRIGGER_SOURCE_LXI5)
        //    .Map("LXI6", IviUpconverterAttribute.IVIUPCONVERTER_VAL_TRIGGER_SOURCE_LXI6)
        //    .Map("LXI7", IviUpconverterAttribute.IVIUPCONVERTER_VAL_TRIGGER_SOURCE_LXI7)
        //    .Map("TTL0", IviUpconverterAttribute.IVIUPCONVERTER_VAL_TRIGGER_SOURCE_TTL0 )
        //    .Map("TTL1", IviUpconverterAttribute.IVIUPCONVERTER_VAL_TRIGGER_SOURCE_TTL1 )
        //    .Map("TTL2", IviUpconverterAttribute.IVIUPCONVERTER_VAL_TRIGGER_SOURCE_TTL2 )
        //    .Map("TTL3", IviUpconverterAttribute.IVIUPCONVERTER_VAL_TRIGGER_SOURCE_TTL3 )
        //    .Map("TTL4", IviUpconverterAttribute.IVIUPCONVERTER_VAL_TRIGGER_SOURCE_TTL4 )
        //    .Map("TTL5", IviUpconverterAttribute.IVIUPCONVERTER_VAL_TRIGGER_SOURCE_TTL5 )
        //    .Map("TTL6", IviUpconverterAttribute.IVIUPCONVERTER_VAL_TRIGGER_SOURCE_TTL6 )
        //    .Map("TTL7", IviUpconverterAttribute.IVIUPCONVERTER_VAL_TRIGGER_SOURCE_TTL7 )
        //    .Map("PXI_STAR", IviUpconverterAttribute.IVIUPCONVERTER_VAL_TRIGGER_SOURCE_PXI_STAR)
        //    .Map("PXI_TRIG0", IviUpconverterAttribute.IVIUPCONVERTER_VAL_TRIGGER_SOURCE_PXI_TRIG0)
        //    .Map("PXI_TRIG1", IviUpconverterAttribute.IVIUPCONVERTER_VAL_TRIGGER_SOURCE_PXI_TRIG1)
        //    .Map("PXI_TRIG2", IviUpconverterAttribute.IVIUPCONVERTER_VAL_TRIGGER_SOURCE_PXI_TRIG2)
        //    .Map("PXI_TRIG3", IviUpconverterAttribute.IVIUPCONVERTER_VAL_TRIGGER_SOURCE_PXI_TRIG3)
        //    .Map("PXI_TRIG4", IviUpconverterAttribute.IVIUPCONVERTER_VAL_TRIGGER_SOURCE_PXI_TRIG4)
        //    .Map("PXI_TRIG5", IviUpconverterAttribute.IVIUPCONVERTER_VAL_TRIGGER_SOURCE_PXI_TRIG5)
        //    .Map("PXI_TRIG6", IviUpconverterAttribute.IVIUPCONVERTER_VAL_TRIGGER_SOURCE_PXI_TRIG6)
        //    .Map("PXI_TRIG7", IviUpconverterAttribute.IVIUPCONVERTER_VAL_TRIGGER_SOURCE_PXI_TRIG7)
        //    .Map("PXIe_DSTARA", IviUpconverterAttribute.IVIUPCONVERTER_VAL_TRIGGER_SOURCE_PXIE_DSTARA)
        //    .Map("PXIe_DSTARB", IviUpconverterAttribute.IVIUPCONVERTER_VAL_TRIGGER_SOURCE_PXIE_DSTARB)
        //    .Map("PXIe_DSTARC", IviUpconverterAttribute.IVIUPCONVERTER_VAL_TRIGGER_SOURCE_PXIE_DSTARC)
        //    .Map("RTSI0", IviUpconverterAttribute.IVIUPCONVERTER_VAL_TRIGGER_SOURCE_RTSI0)
        //    .Map("RTSI1", IviUpconverterAttribute.IVIUPCONVERTER_VAL_TRIGGER_SOURCE_RTSI1)
        //    .Map("RTSI2", IviUpconverterAttribute.IVIUPCONVERTER_VAL_TRIGGER_SOURCE_RTSI2)
        //    .Map("RTSI3", IviUpconverterAttribute.IVIUPCONVERTER_VAL_TRIGGER_SOURCE_RTSI3)
        //    .Map("RTSI4", IviUpconverterAttribute.IVIUPCONVERTER_VAL_TRIGGER_SOURCE_RTSI4)
        //    .Map("RTSI5", IviUpconverterAttribute.IVIUPCONVERTER_VAL_TRIGGER_SOURCE_RTSI5)
        //    .Map("RTSI6", IviUpconverterAttribute.IVIUPCONVERTER_VAL_TRIGGER_SOURCE_RTSI6);

        #endregion

        private IIviUpconverterAlc UpconverterAlc = null;
        private IIviUpconverterAnalogModulation UpconverterAnalogModulation = null;
        private IIviUpconverterExternalLO UpconverterExternalLO = null;
        private IIviUpconverterIFInput UpconverterIFInput = null;
        private IIviUpconverterIQ UpconverterIQ = null;
        private IIviUpconverterPulseModulation UpconverterPulseModulation = null;
        private IIviUpconverterRFOutput UpconverterRFOutput = null;
        private IIviUpconverterReferenceOscillator UpconverterReferenceOscillator = null;
        private IIviUpconverterSweep UpconverterSweep = null;
        public IviUpconverterAdapter(string name, bool idQuery, bool reset, string options)
            : base(name, idQuery, reset, options)
        {
            UpconverterAlc = new IviUpconverterAlc(this);
            UpconverterAnalogModulation = new IviUpconverterAnalogModulation(this);
            UpconverterExternalLO = new IviUpconverterExternalLO(this);
            UpconverterIFInput = new IviUpconverterIFInput(this);
            UpconverterIQ = new IviUpconverterIQ(this);
            UpconverterPulseModulation = new IviUpconverterPulseModulation(this);
            UpconverterRFOutput = new IviUpconverterRFOutput(this);
            UpconverterReferenceOscillator = new IviUpconverterReferenceOscillator(this);
            UpconverterSweep = new IviUpconverterSweep(this);
        }

        public IIviUpconverterAlc Alc
        {
            get { return UpconverterAlc; }
        }

        public IIviUpconverterAnalogModulation AnalogModulation
        {
            get { return UpconverterAnalogModulation; }
        }

        public IIviUpconverterExternalLO ExternalLO
        {
            get { return UpconverterExternalLO; }
        }

        public IIviUpconverterIFInput IFInput
        {
            get { return UpconverterIFInput; }
        }

        public IIviUpconverterIQ IQ
        {
            get { return UpconverterIQ; }
        }

        public IIviUpconverterPulseModulation PulseModulation
        {
            get { return UpconverterPulseModulation; }
        }

        public IIviUpconverterRFOutput RFOutput
        {
            get { return UpconverterRFOutput; }
        }

        public IIviUpconverterReferenceOscillator ReferenceOscillator
        {
            get { return UpconverterReferenceOscillator; }
        }

        public void SendSoftwareTrigger()
        {
            ViSessionStatusCheck(((IviCInterop.IviUpconverter)Interop).SendSoftwareTrigger(Session));
        }

        public IIviUpconverterSweep Sweep
        {
            get { return UpconverterSweep; }
        }

        internal class IviUpconverterAlc : IIviUpconverterAlc
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviUpconverter IviUpconverterInterop = null;
            public IviUpconverterAlc(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviUpconverterInterop = (IviCInterop.IviUpconverter)Adapter.Interop;
            }

            public double Bandwidth
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_ALC_BANDWIDTH);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_ALC_BANDWIDTH, value);
                }
            }

            public void Configure(AlcSource source, double bandwidth)
            {
                Adapter.ViSessionStatusCheck(IviUpconverterInterop.ConfigureALC(Adapter.Session, UpconverterAlcSource.getC_Value(source), bandwidth));
            }

            public bool Enabled
            {
                get
                {
                    return Adapter.GetAttributeViBoolean(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_ALC_ENABLED);
                }
                set
                {
                    Adapter.SetAttributeViBoolean(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_ALC_ENABLED, value);
                }
            }

            public AlcSource Source
            {
                get
                {
                    return UpconverterAlcSource.getEnum(Adapter.GetAttributeViInt32(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_ALC_SOURCE)); 
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_ALC_SOURCE, UpconverterAlcSource.getC_Value(value));
                }
            }
        }

        #region Upconverter Analog Modulation

        internal class IviUpconverterAnalogModulation : IIviUpconverterAnalogModulation
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviUpconverter IviUpconverterInterop = null;
            private IIviUpconverterAM UpconverterAM = null;
            private IIviUpconverterFM UpconverterFM = null;
            private IIviUpconverterPM UpconverterPM = null;
            private IIviUpconverterAnalogModulationSource UpconverterAnalogModulationSource = null;
            public IviUpconverterAnalogModulation(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviUpconverterInterop = (IviCInterop.IviUpconverter)Adapter.Interop;
                UpconverterAM = new IviUpconverterAM(Adapter);
                UpconverterFM = new IviUpconverterFM(Adapter);
                UpconverterPM = new IviUpconverterPM(Adapter);
                UpconverterAnalogModulationSource = new IviUpconverterAnalogModulationSource(Adapter);
            }

            public IIviUpconverterAM AM
            {
                get { return UpconverterAM; }
            }

            public IIviUpconverterFM FM
            {
                get { return UpconverterFM; }
            }

            public IIviUpconverterPM PM
            {
                get { return UpconverterPM; }
            }

            public IIviUpconverterAnalogModulationSource Source
            {
                get { return UpconverterAnalogModulationSource; }
            }
        }

        internal class IviUpconverterAM : IIviUpconverterAM
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviUpconverter IviUpconverterInterop = null;
            public IviUpconverterAM(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviUpconverterInterop = (IviCInterop.IviUpconverter)Adapter.Interop;
            }

            public void Configure(string source, Scaling scaling, double depth)
            {
                Adapter.ViSessionStatusCheck(IviUpconverterInterop.ConfigureAM(Adapter.Session, source, UpconverterAMScaling.getC_Value(scaling), depth));
            }

            public double Depth
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_AM_DEPTH);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_AM_DEPTH, value);
                }
            }

            public bool Enabled
            {
                get
                {
                    return Adapter.GetAttributeViBoolean(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_AM_ENABLED);
                }
                set
                {
                    Adapter.SetAttributeViBoolean(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_AM_ENABLED, value);
                }
            }

            public ExternalCoupling ExternalCoupling
            {
                get
                {
                    return UpconverterAMExternalCoupling.getEnum(Adapter.GetAttributeViInt32(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_AM_EXTERNAL_COUPLING));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_AM_EXTERNAL_COUPLING, UpconverterAMExternalCoupling.getC_Value(value));
                }
            }

            public double NominalVoltage
            {
                get { return Adapter.GetAttributeViReal64(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_AM_NOMINAL_VOLTAGE); }
            }

            public Scaling Scaling
            { 
                get
                {
                    return UpconverterAMScaling.getEnum(Adapter.GetAttributeViInt32(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_AM_SCALING));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_AM_SCALING, UpconverterAMScaling.getC_Value(value));
                }
            }

            public string Source
            {
                get
                {
                    return Adapter.GetAttributeViString(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_AM_SOURCE);
                }
                set
                {
                    Adapter.SetAttributeViString(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_AM_SOURCE, value);
                }
            }
        }

        internal class IviUpconverterFM : IIviUpconverterFM
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviUpconverter IviUpconverterInterop = null;
            public IviUpconverterFM(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviUpconverterInterop = (IviCInterop.IviUpconverter)Adapter.Interop;
            }

            public void Configure(string source, double deviation)
            {
                Adapter.ViSessionStatusCheck(IviUpconverterInterop.ConfigureFM(Adapter.Session, source, deviation));
            }

            public double Deviation
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_FM_DEVIATION);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_FM_DEVIATION, value);
                }
            }

            public bool Enabled
            {
                get
                {
                    return Adapter.GetAttributeViBoolean(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_FM_ENABLED);
                }
                set
                {
                    Adapter.SetAttributeViBoolean(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_FM_ENABLED, value);
                }
            }

            public ExternalCoupling ExternalCoupling
            {
                get
                {
                    return UpconverterFMExternalCoupling.getEnum(Adapter.GetAttributeViInt32(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_FM_EXTERNAL_COUPLING));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_FM_EXTERNAL_COUPLING, UpconverterFMExternalCoupling.getC_Value(value));
                }
            }

            public double NominalVoltage
            {
                get { return Adapter.GetAttributeViReal64(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_FM_NOMINAL_VOLTAGE); }
            }

            public string Source
            {
                get
                {
                    return Adapter.GetAttributeViString(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_FM_SOURCE);
                }
                set
                {
                    Adapter.SetAttributeViString(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_FM_SOURCE, value);
                }
            }
        }

        internal class IviUpconverterPM : IIviUpconverterPM
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviUpconverter IviUpconverterInterop = null;
            public IviUpconverterPM(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviUpconverterInterop = (IviCInterop.IviUpconverter)Adapter.Interop;
            }

            public void Configure(string source, double deviation)
            {
                Adapter.ViSessionStatusCheck(IviUpconverterInterop.ConfigureFM(Adapter.Session, source, deviation));
            }

            public double Deviation
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_PM_DEVIATION);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_PM_DEVIATION, value);
                }
            }

            public bool Enabled
            {
                get
                {
                    return Adapter.GetAttributeViBoolean(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_PM_ENABLED);
                }
                set
                {
                    Adapter.SetAttributeViBoolean(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_PM_ENABLED, value);
                }
            }

            public ExternalCoupling ExternalCoupling
            { 
                get
                {
                    return UpconverterPMExternalCoupling.getEnum(Adapter.GetAttributeViInt32(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_PM_EXTERNAL_COUPLING));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_PM_EXTERNAL_COUPLING, UpconverterPMExternalCoupling.getC_Value(value));
                }
            }

            public double NominalVoltage
            {
                get { return Adapter.GetAttributeViReal64(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_PM_NOMINAL_VOLTAGE); }
            }

            public string Source
            {
                get
                {
                    return Adapter.GetAttributeViString(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_PM_SOURCE);
                }
                set
                {
                    Adapter.SetAttributeViString(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_PM_SOURCE, value);
                }
            }
        }

        internal class IviUpconverterAnalogModulationSource : IIviUpconverterAnalogModulationSource
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviUpconverter IviUpconverterInterop = null;
            public IviUpconverterAnalogModulationSource(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviUpconverterInterop = (IviCInterop.IviUpconverter)Adapter.Interop;
            }

            public int Count
            {
                get { return Adapter.GetAttributeViInt32(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_ANALOG_MODULATION_SOURCE_COUNT); }
            }

            public string GetName(int index)
            {
                StringBuilder Name = new StringBuilder(255);
                Adapter.ViSessionStatusCheck(IviUpconverterInterop.GetAnalogModulationSourceName(Adapter.Session, index, Name.Capacity, Name));
                return Name.ToString();
            }
        }

        #endregion

        internal class IviUpconverterExternalLO : IIviUpconverterExternalLO
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviUpconverter IviUpconverterInterop = null;
            public IviUpconverterExternalLO(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviUpconverterInterop = (IviCInterop.IviUpconverter)Adapter.Interop;
            }

            public bool Enabled
            {
                get
                {
                    return Adapter.GetAttributeViBoolean(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_EXTERNAL_LO_ENABLED);
                }
                set
                {
                    Adapter.SetAttributeViBoolean(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_EXTERNAL_LO_ENABLED, value);
                }
            }

            public double Frequency
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_EXTERNAL_LO_FREQUENCY);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_EXTERNAL_LO_FREQUENCY, value);
                }
            }
        }

        internal class IviUpconverterIFInput : IIviUpconverterIFInput
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviUpconverter IviUpconverterInterop = null;
            public IviUpconverterIFInput(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviUpconverterInterop = (IviCInterop.IviUpconverter)Adapter.Interop;
            }

            public string ActiveIFInput
            {
                get
                {
                    return Adapter.GetAttributeViString(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_ACTIVE_IF_INPUT);
                }
                set
                {
                    Adapter.SetAttributeViString(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_ACTIVE_IF_INPUT, value);
                }
            }

            public double Attenuation
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_IF_INPUT_ATTENUATION);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_IF_INPUT_ATTENUATION, value);
                }
            }

            public bool AutoCorrectionsEnabled
            {
                get
                {
                    return Adapter.GetAttributeViBoolean(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_AUTO_CORRECTIONS_ENABLED);
                }
                set
                {
                    Adapter.SetAttributeViBoolean(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_AUTO_CORRECTIONS_ENABLED, value);
                }
            }

            public bool Bypass
            {
                get
                {
                    return Adapter.GetAttributeViBoolean(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_BYPASS);
                }
                set
                {
                    Adapter.SetAttributeViBoolean(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_BYPASS, value);
                }
            }

            public int Count
            {
                get { return Adapter.GetAttributeViInt32(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_IF_INPUT_COUNT); }
            }

            public ExternalCoupling Coupling
            {
                get
                {
                    return UpconverterIFInputExternalCoupling.getEnum(Adapter.GetAttributeViInt32(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_IF_INPUT_COUPLING));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_IF_INPUT_COUPLING, UpconverterIFInputExternalCoupling.getC_Value(value));
                }
            }

            public double Frequency
            {
                get { return Adapter.GetAttributeViReal64(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_IF_INPUT_FREQUENCY); }
            }

            public string GetName(int index)
            {
                StringBuilder Name = new StringBuilder(255);
                Adapter.ViSessionStatusCheck(IviUpconverterInterop.GetIFInputName(Adapter.Session, index, Name.Capacity, Name));
                return Name.ToString();
            }
        }

        internal class IviUpconverterIQ : IIviUpconverterIQ
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviUpconverter IviUpconverterInterop = null;
            private IIviUpconverterIQImpairment UpconverterIQImpairment = null;
            public IviUpconverterIQ(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviUpconverterInterop = (IviCInterop.IviUpconverter)Adapter.Interop;
                UpconverterIQImpairment = new IviUpconverterIQImpairment(Adapter);
            }

            public void Calibrate()
            {
                Adapter.ViSessionStatusCheck(IviUpconverterInterop.CalibrateIQ(Adapter.Session));
            }

            public bool Enabled
            {
                get
                {
                    return Adapter.GetAttributeViBoolean(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_IQ_ENABLED);
                }
                set
                {
                    Adapter.SetAttributeViBoolean(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_IQ_ENABLED, value);
                }
            }

            public IIviUpconverterIQImpairment Impairment
            {
                get { return UpconverterIQImpairment; }
            }

            public double NominalVoltage
            {
                get { return Adapter.GetAttributeViReal64(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_IQ_NOMINAL_VOLTAGE); }
            }

            public bool SwapEnabled
            {
                get
                {
                    return Adapter.GetAttributeViBoolean(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_IQ_SWAP_ENABLED);
                }
                set
                {
                    Adapter.SetAttributeViBoolean(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_IQ_SWAP_ENABLED, value);
                }
            }
        }

        internal class IviUpconverterIQImpairment : IIviUpconverterIQImpairment
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviUpconverter IviUpconverterInterop = null;
            public IviUpconverterIQImpairment(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviUpconverterInterop = (IviCInterop.IviUpconverter)Adapter.Interop;
            }

            public void Configure(double iOffset, double qOffset, double ratio, double skew)
            {
                Adapter.ViSessionStatusCheck(IviUpconverterInterop.ConfigureIQImpairment(Adapter.Session, iOffset, qOffset, ratio, skew));
            }

            public bool Enabled
            {
                get
                {
                    return Adapter.GetAttributeViBoolean(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_IQ_IMPAIRMENT_ENABLED);
                }
                set
                {
                    Adapter.SetAttributeViBoolean(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_IQ_IMPAIRMENT_ENABLED, value);
                }
            }

            public double IOffset
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_IQ_IMPAIRMENT_I_OFFSET);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_IQ_IMPAIRMENT_I_OFFSET, value);
                }
            }

            public double QOffset
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_IQ_IMPAIRMENT_Q_OFFSET);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_IQ_IMPAIRMENT_Q_OFFSET, value);
                }
            }

            public double Ratio
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_IQ_IMPAIRMENT_RATIO);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_IQ_IMPAIRMENT_RATIO, value);
                }
            }

            public double Skew
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_IQ_IMPAIRMENT_SKEW);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_IQ_IMPAIRMENT_SKEW, value);
                }
            }
        }

        internal class IviUpconverterPulseModulation : IIviUpconverterPulseModulation
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviUpconverter IviUpconverterInterop = null;
            public IviUpconverterPulseModulation(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviUpconverterInterop = (IviCInterop.IviUpconverter)Adapter.Interop;
            }

            public bool Enabled
            {
                get
                {
                    return Adapter.GetAttributeViBoolean(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_PULSE_MODULATION_ENABLED);
                }
                set
                {
                    Adapter.SetAttributeViBoolean(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_PULSE_MODULATION_ENABLED, value);
                }
            }

            public PulseModulationExternalPolarity ExternalPolarity
            {
                get 
                {
                    return UpconverterPulseModulationExternalPolarity.getEnum(Adapter.GetAttributeViInt32(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_PULSE_MODULATION_EXTERNAL_POLARITY));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_PULSE_MODULATION_EXTERNAL_POLARITY, UpconverterPulseModulationExternalPolarity.getC_Value(value));
                }
            }
        }

        internal class IviUpconverterRFOutput : IIviUpconverterRFOutput
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviUpconverter IviUpconverterInterop = null;
            public IviUpconverterRFOutput(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviUpconverterInterop = (IviCInterop.IviUpconverter)Adapter.Interop;
            }

            public string ActiveRFOutput
            {
                get
                {
                    return Adapter.GetAttributeViString(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_ACTIVE_RF_OUTPUT);
                }
                set
                {
                    Adapter.SetAttributeViString(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_ACTIVE_RF_OUTPUT, value);
                }
            }

            public bool AttenuatorHoldEnabled
            {
                get
                {
                    return Adapter.GetAttributeViBoolean(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_ATTENUATOR_HOLD_ENABLED);
                }
                set
                {
                    Adapter.SetAttributeViBoolean(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_ATTENUATOR_HOLD_ENABLED, value);
                }
            }

            public double Bandwidth
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_RF_OUTPUT_BANDWIDTH);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_RF_OUTPUT_BANDWIDTH, value);
                }
            }

            public void Calibrate()
            {
                Adapter.ViSessionStatusCheck(IviUpconverterInterop.Calibrate(Adapter.Session));
            }

            public int Count
            {
                get { return Adapter.GetAttributeViInt32(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_RF_OUTPUT_COUNT); }
            }

            public bool Enabled
            {
                get
                {
                    return Adapter.GetAttributeViBoolean(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_RF_OUTPUT_ENABLED);
                }
                set
                {
                    Adapter.SetAttributeViBoolean(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_RF_OUTPUT_ENABLED, value);
                }
            }

            public double Frequency
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_RF_OUTPUT_FREQUENCY);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_RF_OUTPUT_FREQUENCY, value);
                }
            }

            public double Gain
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_RF_OUTPUT_GAIN);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_RF_OUTPUT_GAIN, value);
                }
            }

            public string GetName(int index)
            {
                StringBuilder Name = new StringBuilder(255);
                Adapter.ViSessionStatusCheck(IviUpconverterInterop.GetRFOutputName(Adapter.Session, index, Name.Capacity, Name));
                return Name.ToString();
            }

            public CalibrationStatus IsCalibrationComplete()
            {
                int status = 0;
                Adapter.ViSessionStatusCheck(IviUpconverterInterop.IsCalibrationComplete(Adapter.Session, ref status));
                return UpconverterCalibrationStatus.getEnum(status);
            }

            public bool IsReady
            {
                get { return Adapter.GetAttributeViBoolean(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_IS_READY); }
            }

            public double Level
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_RF_OUTPUT_LEVEL);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_RF_OUTPUT_LEVEL, value);
                }
            }

            public string ReadyTrigger
            {
                get
                {
                    return Adapter.GetAttributeViString(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_RF_OUTPUT_READY_TRIGGER);
                }
                set
                {
                    Adapter.SetAttributeViString(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_RF_OUTPUT_READY_TRIGGER, value);
                }
            }

            public void WaitUntilReady(Ivi.Driver.PrecisionTimeSpan maximumTime)
            {
                Adapter.ViSessionStatusCheck(IviUpconverterInterop.WaitUntilReady(Adapter.Session, (int)maximumTime.TotalMilliseconds));
            }
        }

        internal class IviUpconverterReferenceOscillator : IIviUpconverterReferenceOscillator
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviUpconverter IviUpconverterInterop = null;
            public IviUpconverterReferenceOscillator(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviUpconverterInterop = (IviCInterop.IviUpconverter)Adapter.Interop;
            }

            public void Configure(ReferenceOscillatorSource source, double frequency)
            {
                Adapter.ViSessionStatusCheck(IviUpconverterInterop.ConfigureReferenceOscillator(Adapter.Session, UpconverterReferenceOscillatorSource.getC_Value(source), frequency));
            }

            public double ExternalFrequency
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_REFERENCE_OSCILLATOR_EXTERNAL_FREQUENCY);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_REFERENCE_OSCILLATOR_EXTERNAL_FREQUENCY, value);
                }
            }

            public bool OutputEnabled
            {
                get
                {
                    return Adapter.GetAttributeViBoolean(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_REFERENCE_OSCILLATOR_OUTPUT_ENABLED);
                }
                set
                {
                    Adapter.SetAttributeViBoolean(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_REFERENCE_OSCILLATOR_OUTPUT_ENABLED, value);
                }
            }

            public ReferenceOscillatorSource Source
            { 
                get 
                {
                    return UpconverterReferenceOscillatorSource.getEnum(Adapter.GetAttributeViInt32(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_REFERENCE_OSCILLATOR_SOURCE));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_REFERENCE_OSCILLATOR_SOURCE, UpconverterReferenceOscillatorSource.getC_Value(value));
                }
            }
        }

        #region Upconverter Sweep

        internal class IviUpconverterSweep : IIviUpconverterSweep
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviUpconverter IviUpconverterInterop = null;
            private IIviUpconverterFrequencyStep UpconverterFrequencyStep = null;
            private IIviUpconverterFrequencySweep UpconverterFrequencySweep = null;
            private IIviUpconverterGainStep UpconverterGainStep = null;
            private IIviUpconverterGainSweep UpconverterGainSweep = null;
            private IIviUpconverterList UpconverterList = null;
            private IIviUpconverterPowerStep UpconverterPowerStep = null;
            private IIviUpconverterPowerSweep UpconverterPowerSweep = null;
            public IviUpconverterSweep(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviUpconverterInterop = (IviCInterop.IviUpconverter)Adapter.Interop;
                UpconverterFrequencyStep = new IviUpconverterFrequencyStep(Adapter);
                UpconverterFrequencySweep = new IviUpconverterFrequencySweep(Adapter);
                UpconverterGainStep = new IviUpconverterGainStep(Adapter);
                UpconverterGainSweep = new IviUpconverterGainSweep(Adapter);
                UpconverterList = new IviUpconverterList(Adapter);
                UpconverterPowerStep = new IviUpconverterPowerStep(Adapter);
                UpconverterPowerSweep = new IviUpconverterPowerSweep(Adapter);
            }

            public void Configure(SweepMode mode, string triggerSource)
            {
                Adapter.ViSessionStatusCheck(IviUpconverterInterop.ConfigureSweep(Adapter.Session, UpconverterSweepMode.getC_Value(mode), triggerSource));
            }

            public IIviUpconverterFrequencyStep FrequencyStep
            {
                get { return UpconverterFrequencyStep; }
            }

            public IIviUpconverterFrequencySweep FrequencySweep
            {
                get { return UpconverterFrequencySweep; }
            }

            public IIviUpconverterGainStep GainStep
            {
                get { return UpconverterGainStep; }
            }

            public IIviUpconverterGainSweep GainSweep
            {
                get { return UpconverterGainSweep; }
            }

            public bool IsSweeping
            {
                get { return Adapter.GetAttributeViBoolean(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_IS_SWEEPING); }
            }

            public IIviUpconverterList List
            {
                get { return UpconverterList; }
            }

            public SweepMode Mode
            {
                get
                {
                    return UpconverterSweepMode.getEnum(Adapter.GetAttributeViInt32(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_SWEEP_MODE));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_SWEEP_MODE, UpconverterSweepMode.getC_Value(value));
                }
            }

            public IIviUpconverterPowerStep PowerStep
            {
                get { return UpconverterPowerStep; }
            }

            public IIviUpconverterPowerSweep PowerSweep
            {
                get { return UpconverterPowerSweep; }
            }

            public string TriggerSource
            {
                get
                {
                    return Adapter.GetAttributeViString(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_SWEEP_TRIGGER_SOURCE);
                }
                set
                {
                    Adapter.SetAttributeViString(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_SWEEP_TRIGGER_SOURCE, value);
                }
            }
        }

        internal class IviUpconverterFrequencyStep : IIviUpconverterFrequencyStep
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviUpconverter IviUpconverterInterop = null;
            public IviUpconverterFrequencyStep(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviUpconverterInterop = (IviCInterop.IviUpconverter)Adapter.Interop;
            }

            public void ConfigureDwell(bool singleStepEnabled, Ivi.Driver.PrecisionTimeSpan dwell)
            {
                Adapter.ViSessionStatusCheck(IviUpconverterInterop.ConfigureFrequencyStepDwell(Adapter.Session, singleStepEnabled, dwell.TotalSeconds));
            }

            public void ConfigureStartStop(double start, double stop, Scaling scaling, double stepSize)
            {
                Adapter.ViSessionStatusCheck(IviUpconverterInterop.ConfigureFrequencyStepStartStop(Adapter.Session, start, stop, UpconverterFrequencyStepScaling.getC_Value(scaling), stepSize));
            }

            public Ivi.Driver.PrecisionTimeSpan Dwell
            {
                get
                {
                    return new Ivi.Driver.PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * Adapter.GetAttributeViReal64(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_FREQUENCY_STEP_DWELL))));
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_FREQUENCY_STEP_DWELL, value.TotalSeconds);
                }
            }

            public void Reset()
            {
                Adapter.ViSessionStatusCheck(IviUpconverterInterop.ResetFrequencyStep(Adapter.Session));
            }

            public Scaling Scaling
            {
                get
                {
                    return UpconverterFrequencyStepScaling.getEnum(Adapter.GetAttributeViInt32(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_FREQUENCY_STEP_SCALING));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_FREQUENCY_STEP_SCALING, UpconverterFrequencyStepScaling.getC_Value(value));
                }
            }

            public bool SingleStepEnabled
            {
                get
                {
                    return Adapter.GetAttributeViBoolean(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_FREQUENCY_STEP_SINGLE_STEP_ENABLED);
                }
                set
                {
                    Adapter.SetAttributeViBoolean(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_FREQUENCY_STEP_SINGLE_STEP_ENABLED, value);
                }
            }

            public double Size
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_FREQUENCY_STEP_SIZE);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_FREQUENCY_STEP_SIZE, value);
                }
            }

            public double Start
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_FREQUENCY_STEP_START);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_FREQUENCY_STEP_START, value);
                }
            }

            public double Stop
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_FREQUENCY_STEP_STOP);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_FREQUENCY_STEP_STOP, value);
                }
            }
        }

        internal class IviUpconverterFrequencySweep : IIviUpconverterFrequencySweep
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviUpconverter IviUpconverterInterop = null;
            public IviUpconverterFrequencySweep(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviUpconverterInterop = (IviCInterop.IviUpconverter)Adapter.Interop;
            }

            public void ConfigureCenterSpan(double center, double span)
            {
                Adapter.ViSessionStatusCheck(IviUpconverterInterop.ConfigureFrequencySweepCenterSpan(Adapter.Session, center, span));
            }

            public void ConfigureStartStop(double start, double stop)
            {
                Adapter.ViSessionStatusCheck(IviUpconverterInterop.ConfigureFrequencySweepStartStop(Adapter.Session, start, stop));
            }

            public double Start
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_FREQUENCY_SWEEP_START);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_FREQUENCY_SWEEP_START, value);
                }
            }

            public double Stop
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_FREQUENCY_SWEEP_STOP);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_FREQUENCY_SWEEP_STOP, value);
                }
            }

            public Ivi.Driver.PrecisionTimeSpan Time
            {
                get
                {
                    return new Ivi.Driver.PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * Adapter.GetAttributeViReal64(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_FREQUENCY_SWEEP_TIME))));
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_FREQUENCY_SWEEP_TIME, value.TotalSeconds);
                }
            }
        }

        internal class IviUpconverterGainStep : IIviUpconverterGainStep
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviUpconverter IviUpconverterInterop = null;
            public IviUpconverterGainStep(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviUpconverterInterop = (IviCInterop.IviUpconverter)Adapter.Interop;
            }

            public void ConfigureDwell(bool singleStepEnabled, Ivi.Driver.PrecisionTimeSpan dwell)
            {
                Adapter.ViSessionStatusCheck(IviUpconverterInterop.ConfigureGainStepDwell(Adapter.Session, singleStepEnabled, dwell.TotalSeconds));
            }

            public void ConfigureStartStop(double start, double stop, double stepSize)
            {
                Adapter.ViSessionStatusCheck(IviUpconverterInterop.ConfigureGainStepStartStop(Adapter.Session, start, stop, stepSize));
            }

            public Ivi.Driver.PrecisionTimeSpan Dwell
            {
                get
                {
                    return new Ivi.Driver.PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * Adapter.GetAttributeViReal64(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_GAIN_STEP_DWELL))));
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_GAIN_STEP_DWELL, value.TotalSeconds);
                }
            }

            public void Reset()
            {
                Adapter.ViSessionStatusCheck(IviUpconverterInterop.ResetGainStep(Adapter.Session));
            }

            public bool SingleStepEnabled
            {
                get
                {
                    return Adapter.GetAttributeViBoolean(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_GAIN_STEP_SINGLE_STEP_ENABLED);
                }
                set
                {
                    Adapter.SetAttributeViBoolean(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_GAIN_STEP_SINGLE_STEP_ENABLED, value);
                }
            }

            public double Size
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_GAIN_STEP_SIZE);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_GAIN_STEP_SIZE, value);
                }
            }

            public double Start
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_GAIN_STEP_START);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_GAIN_STEP_START, value);
                }
            }

            public double Stop
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_GAIN_STEP_STOP);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_GAIN_STEP_STOP, value);
                }
            }
        }

        internal class IviUpconverterGainSweep : IIviUpconverterGainSweep
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviUpconverter IviUpconverterInterop = null;
            public IviUpconverterGainSweep(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviUpconverterInterop = (IviCInterop.IviUpconverter)Adapter.Interop;
            }

            public void ConfigureStartStop(double start, double stop)
            {
                Adapter.ViSessionStatusCheck(IviUpconverterInterop.ConfigureGainSweepStartStop(Adapter.Session, start, stop));
            }

            public double Start
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_GAIN_SWEEP_START);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_GAIN_SWEEP_START, value);
                }
            }

            public double Stop
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_GAIN_SWEEP_STOP);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_GAIN_SWEEP_STOP, value);
                }
            }

            public Ivi.Driver.PrecisionTimeSpan Time
            {
                get
                {
                    return new Ivi.Driver.PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * Adapter.GetAttributeViReal64(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_GAIN_SWEEP_TIME))));
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_GAIN_SWEEP_TIME, value.TotalSeconds);
                }
            }
        }

        internal class IviUpconverterList : IIviUpconverterList
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviUpconverter IviUpconverterInterop = null;
            public IviUpconverterList(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviUpconverterInterop = (IviCInterop.IviUpconverter)Adapter.Interop;
            }

            public void ClearAll()
            {
                Adapter.ViSessionStatusCheck(IviUpconverterInterop.ClearAllLists(Adapter.Session));
            }

            public void ConfigureDwell(bool singleStepEnabled, Ivi.Driver.PrecisionTimeSpan dwell)
            {
                Adapter.ViSessionStatusCheck(IviUpconverterInterop.ConfigureListDwell(Adapter.Session, singleStepEnabled, dwell.TotalSeconds));
            }

            public void CreateFrequency(string name, double[] frequencyList)
            {
                Adapter.ViSessionStatusCheck(IviUpconverterInterop.CreateFrequencyList(Adapter.Session, name, frequencyList.Length, frequencyList));
            }

            public void CreateFrequencyGain(string name, FrequencyGain[] frequencyGainList)
            {
                 double[] Frequency = new double[frequencyGainList.Length];
                 double[] Gain = new double[frequencyGainList.Length];
                 
                for(int i =0; i<= frequencyGainList.Length; i++)
                {
                    Frequency[i]= frequencyGainList[i].Frequency;
                    Gain[i]= frequencyGainList[i].Gain;
                }

                Adapter.ViSessionStatusCheck(IviUpconverterInterop.CreateFrequencyGainList(Adapter.Session, name, frequencyGainList.Length, Frequency, Gain));
            }

            public void CreateFrequencyPower(string name, FrequencyPower[] frequencyPowerList)
            {
                double[] Frequency = new double[frequencyPowerList.Length];
                double[] Power = new double[frequencyPowerList.Length];

                for (int i = 0; i <= frequencyPowerList.Length; i++)
                {
                    Frequency[i] = frequencyPowerList[i].Frequency;
                    Power[i] = frequencyPowerList[i].Power;
                }

                Adapter.ViSessionStatusCheck(IviUpconverterInterop.CreateFrequencyPowerList(Adapter.Session, name, frequencyPowerList.Length, Frequency, Power));
            }

            public void CreateGain(string name, double[] gainList)
            {
                Adapter.ViSessionStatusCheck(IviUpconverterInterop.CreateGainList(Adapter.Session, name, gainList.Length, gainList));
            }

            public void CreatePower(string name, double[] powerList)
            {
                Adapter.ViSessionStatusCheck(IviUpconverterInterop.CreatePowerList(Adapter.Session, name, powerList.Length, powerList));
            }

            public Ivi.Driver.PrecisionTimeSpan Dwell
            {
                get
                {
                    return new Ivi.Driver.PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * Adapter.GetAttributeViReal64(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_LIST_DWELL))));
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_LIST_DWELL, value.TotalSeconds);
                }
            }

            public void Reset()
            {
                Adapter.ViSessionStatusCheck(IviUpconverterInterop.ResetList(Adapter.Session));
            }

            public string SelectedName
            {
                get
                {
                    return Adapter.GetAttributeViString(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_LIST_SELECTED_NAME);
                }
                set
                {
                    Adapter.SetAttributeViString(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_LIST_SELECTED_NAME, value);
                }
            }

            public bool SingleStepEnabled
            {
                get
                {
                    return Adapter.GetAttributeViBoolean(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_LIST_SINGLE_STEP_ENABLED);
                }
                set
                {
                    Adapter.SetAttributeViBoolean(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_LIST_SINGLE_STEP_ENABLED, value);
                }
            }
        }

        internal class IviUpconverterPowerStep : IIviUpconverterPowerStep
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviUpconverter IviUpconverterInterop = null;
            public IviUpconverterPowerStep(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviUpconverterInterop = (IviCInterop.IviUpconverter)Adapter.Interop;
            }

            public void ConfigureDwell(bool singleStepEnabled, Ivi.Driver.PrecisionTimeSpan dwell)
            {
                Adapter.ViSessionStatusCheck(IviUpconverterInterop.ConfigurePowerStepDwell(Adapter.Session, singleStepEnabled, dwell.TotalSeconds));
            }

            public void ConfigureStartStop(double start, double stop, double stepSize)
            {
                Adapter.ViSessionStatusCheck(IviUpconverterInterop.ConfigurePowerStepStartStop(Adapter.Session, start, stop, stepSize));
            }

            public Ivi.Driver.PrecisionTimeSpan Dwell
            {
                get
                {
                    return new Ivi.Driver.PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * Adapter.GetAttributeViReal64(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_POWER_STEP_DWELL))));
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_POWER_STEP_DWELL, value.TotalSeconds);
                }
            }

            public void Reset()
            {
                Adapter.ViSessionStatusCheck(IviUpconverterInterop.ResetPowerStep(Adapter.Session));
            }

            public bool SingleStepEnabled
            {
                get
                {
                    return Adapter.GetAttributeViBoolean(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_POWER_STEP_SINGLE_STEP_ENABLED);
                }
                set
                {
                    Adapter.SetAttributeViBoolean(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_POWER_STEP_SINGLE_STEP_ENABLED, value);
                }
            }

            public double Size
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_POWER_STEP_SIZE);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_POWER_STEP_SIZE, value);
                }
            }

            public double Start
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_POWER_STEP_START);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_POWER_STEP_START, value);
                }
            }

            public double Stop
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_POWER_STEP_STOP);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_POWER_STEP_STOP, value);
                }
            }
        }

        internal class IviUpconverterPowerSweep : IIviUpconverterPowerSweep
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviUpconverter IviUpconverterInterop = null;
            public IviUpconverterPowerSweep(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviUpconverterInterop = (IviCInterop.IviUpconverter)Adapter.Interop;
            }

            public void ConfigureStartStop(double start, double stop)
            {
                Adapter.ViSessionStatusCheck(IviUpconverterInterop.ConfigurePowerSweepStartStop(Adapter.Session, start, stop));
            }

            public double Start
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_POWER_SWEEP_START);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_POWER_SWEEP_START, value);
                }
            }

            public double Stop
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_POWER_SWEEP_STOP);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_POWER_SWEEP_STOP, value);
                }
            }

            public Ivi.Driver.PrecisionTimeSpan Time
            {
                get
                {
                    return new Ivi.Driver.PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * Adapter.GetAttributeViReal64(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_POWER_SWEEP_TIME))));
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviUpconverterAttribute.IVIUPCONVERTER_ATTR_POWER_SWEEP_TIME, value.TotalSeconds);
                }
            }
        }

        #endregion
    }
}

