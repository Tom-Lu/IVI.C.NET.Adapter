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
using Ivi.DCPwr;
using Ivi.ConfigServer.Interop;
using IVI.C.NET.Adapter.IviCInterop;


namespace IVI.C.NET.Adapter
{
    public class IviDCPwrAdapter : DriverAdapterBase<IviCInterop.IviDCPwr>, IIviDCPwr
    {
        #region Enum Mapping

        private static IviEnumCMapping<CurrentLimitBehavior, int> DCPwrCurrentLimitBehavior = IviEnumCMapping<CurrentLimitBehavior, int>.Instance
            .Map(CurrentLimitBehavior.Trip, IviDCPwrAttribute.IVIDCPWR_VAL_CURRENT_TRIP)
            .Map(CurrentLimitBehavior.Regulate, IviDCPwrAttribute.IVIDCPWR_VAL_CURRENT_REGULATE);

        private static IviEnumCMapping<RangeType, int> DCPwrRangeType = IviEnumCMapping<RangeType, int>.Instance
            .Map(RangeType.Current, IviDCPwrAttribute.IVIDCPWR_VAL_RANGE_CURRENT)
            .Map(RangeType.Voltage, IviDCPwrAttribute.IVIDCPWR_VAL_RANGE_VOLTAGE);

        private static IviEnumCMapping<MeasurementType, int> DCPwrMeasurementType = IviEnumCMapping<MeasurementType, int>.Instance
            .Map(MeasurementType.Current, IviDCPwrAttribute.IVIDCPWR_VAL_MEASURE_CURRENT)
            .Map(MeasurementType.Voltage, IviDCPwrAttribute.IVIDCPWR_VAL_MEASURE_VOLTAGE);

        private static IviEnumCMapping<OutputState, int> DCPwrOutputState = IviEnumCMapping<OutputState, int>.Instance
            .Map(OutputState.ConstantVoltage, IviDCPwrAttribute.IVIDCPWR_VAL_OUTPUT_CONSTANT_VOLTAGE)
            .Map(OutputState.ConstantCurrent, IviDCPwrAttribute.IVIDCPWR_VAL_OUTPUT_CONSTANT_CURRENT)
            .Map(OutputState.OverVoltage, IviDCPwrAttribute.IVIDCPWR_VAL_OUTPUT_OVER_VOLTAGE)
            .Map(OutputState.OverCurrent, IviDCPwrAttribute.IVIDCPWR_VAL_OUTPUT_OVER_CURRENT)
            .Map(OutputState.Unregulated, IviDCPwrAttribute.IVIDCPWR_VAL_OUTPUT_UNREGULATED);

        private static IviEnumCMapping<string, int> DCPwrTriggerSource = IviEnumCMapping<string, int>.Instance
            .Map("Immediate", IviDCPwrAttribute.IVIDCPWR_VAL_TRIG_IMMEDIATE)
            .Map("External", IviDCPwrAttribute.IVIDCPWR_VAL_TRIG_EXTERNAL)
            .Map("Software", IviDCPwrAttribute.IVIDCPWR_VAL_SOFTWARE_TRIG)
            .Map("TTL0", IviDCPwrAttribute.IVIDCPWR_VAL_TRIG_TTL0)
            .Map("TTL1", IviDCPwrAttribute.IVIDCPWR_VAL_TRIG_TTL1)
            .Map("TTL2", IviDCPwrAttribute.IVIDCPWR_VAL_TRIG_TTL2)
            .Map("TTL3", IviDCPwrAttribute.IVIDCPWR_VAL_TRIG_TTL3)
            .Map("TTL4", IviDCPwrAttribute.IVIDCPWR_VAL_TRIG_TTL4)
            .Map("TTL5", IviDCPwrAttribute.IVIDCPWR_VAL_TRIG_TTL5)
            .Map("TTL6", IviDCPwrAttribute.IVIDCPWR_VAL_TRIG_TTL6)
            .Map("TTL7", IviDCPwrAttribute.IVIDCPWR_VAL_TRIG_TTL7)
            .Map("ECL0", IviDCPwrAttribute.IVIDCPWR_VAL_TRIG_ECL0)
            .Map("ECL1", IviDCPwrAttribute.IVIDCPWR_VAL_TRIG_ECL1)
            .Map("PXI_STAR", IviDCPwrAttribute.IVIDCPWR_VAL_TRIG_PXI_STAR)
            .Map("RTSI0", IviDCPwrAttribute.IVIDCPWR_VAL_TRIG_RTSI_0)
            .Map("RTSI1", IviDCPwrAttribute.IVIDCPWR_VAL_TRIG_RTSI_1)
            .Map("RTSI2", IviDCPwrAttribute.IVIDCPWR_VAL_TRIG_RTSI_2)
            .Map("RTSI3", IviDCPwrAttribute.IVIDCPWR_VAL_TRIG_RTSI_3)
            .Map("RTSI4", IviDCPwrAttribute.IVIDCPWR_VAL_TRIG_RTSI_4)
            .Map("RTSI5", IviDCPwrAttribute.IVIDCPWR_VAL_TRIG_RTSI_5)
            .Map("RTSI6", IviDCPwrAttribute.IVIDCPWR_VAL_TRIG_RTSI_6);

        #endregion

        IIviDCPwrTrigger trigger = null;
        IIviDCPwrOutputCollection outputs = null;
        public IviDCPwrAdapter(string name, bool idQuery, bool reset, string options)
            : base(name, idQuery, reset, options)
        {
            trigger = new IviDCPwrTrigger(this);
            outputs = new IviDCPwrOutputCollection(this);
        }

        public IIviDCPwrOutputCollection Outputs
        {
            get { return outputs; }
        }

        public IIviDCPwrTrigger Trigger
        {
            get { return trigger; }
        }

        internal class IviDCPwrOutputCollection : IIviDCPwrOutputCollection
        {
            private IDriverAdapterBase Adapter;
            private IList<IIviDCPwrOutput> Outputs = null;
            private IList<string> OutputNames = null;
            public IviDCPwrOutputCollection(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                int OutputCount = Adapter.GetAttributeViInt32(IviDCPwrAttribute.IVIDCPWR_ATTR_CHANNEL_COUNT);

                Outputs = new List<IIviDCPwrOutput>();
                OutputNames = new List<string>();
                for (int Index = 1; Index <= OutputCount; Index++)
                {
                    IIviDCPwrOutput Output = new IviDCPwrOutput(Adapter, Index);
                    Outputs.Add(Output);
                    OutputNames.Add(Output.Name);
                }
            }

            public int Count
            {
                get
                {
                    return Outputs.Count;
                }
            }

            public IIviDCPwrOutput this[string name]
            {
                get { return Outputs[OutputNames.IndexOf(name)]; }
            }

            public IEnumerator<IIviDCPwrOutput> GetEnumerator()
            {
                return Outputs.GetEnumerator();
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return Outputs.GetEnumerator();
            }
        }

        internal class IviDCPwrTrigger : IIviDCPwrTrigger
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviDCPwr IviDCPwrInterop = null;

            public IviDCPwrTrigger(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviDCPwrInterop = (IviCInterop.IviDCPwr)Adapter.Interop;
            }
            public void Abort()
            {
                Adapter.ViSessionStatusCheck(IviDCPwrInterop.Abort(Adapter.Session));
            }

            public void Initiate()
            {
                Adapter.ViSessionStatusCheck(IviDCPwrInterop.Initiate(Adapter.Session));
            }

            public void SendSoftwareTrigger()
            {
                Adapter.ViSessionStatusCheck(IviDCPwrInterop.SendSoftwareTrigger(Adapter.Session));
            }
        }

        internal class IviDCPwrOutput : IIviDCPwrOutput
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviDCPwr IviDCPwrInterop = null;
            private int Index;
            private string ChannelName;

            public IviDCPwrOutput(IDriverAdapterBase Adapter, int Index)
            {
                this.Adapter = Adapter;
                IviDCPwrInterop = (IviCInterop.IviDCPwr)Adapter.Interop;
                this.Index = Index;

                try
                {
                    StringBuilder NameValue = new StringBuilder(256);
                    Adapter.ViSessionStatusCheck(IviDCPwrInterop.GetChannelName(Adapter.Session, Index, NameValue.Capacity, NameValue));
                    ChannelName = NameValue.ToString();
                }
                catch
                {
                    ChannelName = string.Empty;
                }
            }

            public void ConfigureCurrentLimit(CurrentLimitBehavior behavior, double limit)
            {
                Adapter.ViSessionStatusCheck(IviDCPwrInterop.ConfigureCurrentLimit(Adapter.Session, ChannelName, DCPwrCurrentLimitBehavior.getC_Value(behavior), limit));
            }

            public void ConfigureOvp(bool enabled, double limit)
            {
                Adapter.ViSessionStatusCheck(IviDCPwrInterop.ConfigureOVP(Adapter.Session, ChannelName, enabled, limit));
            }

            public void ConfigureRange(RangeType rangeType, double range)
            {
                Adapter.ViSessionStatusCheck(IviDCPwrInterop.ConfigureOutputRange(Adapter.Session, ChannelName, DCPwrRangeType.getC_Value(rangeType), range));
            }

            public double CurrentLimit
            {
                get
                {
                    return Adapter.GetAttributeViReal64(ChannelName, IviDCPwrAttribute.IVIDCPWR_ATTR_CURRENT_LIMIT);
                }
                set
                {
                    Adapter.SetAttributeViReal64(ChannelName, IviDCPwrAttribute.IVIDCPWR_ATTR_CURRENT_LIMIT, value);
                }
            }

            public CurrentLimitBehavior CurrentLimitBehavior
            {
                get
                {
                    return DCPwrCurrentLimitBehavior.getEnum(Adapter.GetAttributeViInt32(ChannelName, IviDCPwrAttribute.IVIDCPWR_ATTR_CURRENT_LIMIT_BEHAVIOR));
                }
                set
                {
                    Adapter.SetAttributeViInt32(ChannelName, IviDCPwrAttribute.IVIDCPWR_ATTR_CURRENT_LIMIT_BEHAVIOR, DCPwrCurrentLimitBehavior.getC_Value(value));
                }
            }

            public bool Enabled
            {
                get
                {
                    return Adapter.GetAttributeViBoolean(ChannelName, IviDCPwrAttribute.IVIDCPWR_ATTR_OUTPUT_ENABLED);
                }
                set
                {
                    Adapter.SetAttributeViBoolean(ChannelName, IviDCPwrAttribute.IVIDCPWR_ATTR_OUTPUT_ENABLED, value);
                }
            }

            public double Measure(MeasurementType measurementType)
            {
                double measurement = double.NaN;
                Adapter.ViSessionStatusCheck(IviDCPwrInterop.Measure(Adapter.Session, ChannelName, DCPwrMeasurementType.getC_Value(measurementType), ref measurement));
                return measurement;
            }

            public bool OvpEnabled
            {
                get
                {
                    return Adapter.GetAttributeViBoolean(ChannelName, IviDCPwrAttribute.IVIDCPWR_ATTR_OVP_ENABLED);
                }
                set
                {
                    Adapter.SetAttributeViBoolean(ChannelName, IviDCPwrAttribute.IVIDCPWR_ATTR_OVP_ENABLED, value);
                }
            }

            public double OvpLimit
            {
                get
                {
                    return Adapter.GetAttributeViReal64(ChannelName, IviDCPwrAttribute.IVIDCPWR_ATTR_OVP_LIMIT);
                }
                set
                {
                    Adapter.SetAttributeViReal64(ChannelName, IviDCPwrAttribute.IVIDCPWR_ATTR_OVP_LIMIT, value);
                }
            }

            public double QueryCurrentLimitMax(double voltageLevel)
            {
                double CurrentLimitMax = double.NaN;
                Adapter.ViSessionStatusCheck(IviDCPwrInterop.QueryMaxCurrentLimit(Adapter.Session, ChannelName, voltageLevel, ref CurrentLimitMax));
                return CurrentLimitMax;
            }

            public bool QueryState(OutputState outputState)
            {
                bool inStatus = false;
                Adapter.ViSessionStatusCheck(IviDCPwrInterop.QueryOutputState(Adapter.Session, ChannelName, DCPwrOutputState.getC_Value(outputState), ref inStatus));
                return inStatus;
            }

            public double QueryVoltageLevelMax(double currentLimit)
            {
                double QueryVoltageLevelMax = double.NaN;
                Adapter.ViSessionStatusCheck(IviDCPwrInterop.QueryMaxVoltageLevel(Adapter.Session, ChannelName, currentLimit, ref QueryVoltageLevelMax));
                return QueryVoltageLevelMax;
            }

            public void ResetOutputProtection()
            {
                Adapter.ViSessionStatusCheck(IviDCPwrInterop.ResetOutputProtection(Adapter.Session, ChannelName));
            }

            public string TriggerSource
            {
                get
                {
                    return DCPwrTriggerSource.getEnum(Adapter.GetAttributeViInt32(ChannelName, IviDCPwrAttribute.IVIDCPWR_ATTR_TRIGGER_SOURCE));
                }
                set
                {
                    Adapter.SetAttributeViInt32(ChannelName, IviDCPwrAttribute.IVIDCPWR_ATTR_TRIGGER_SOURCE, DCPwrTriggerSource.getC_Value(value));
                }
            }

            public double TriggeredCurrentLimit
            {
                get
                {
                    return Adapter.GetAttributeViReal64(ChannelName, IviDCPwrAttribute.IVIDCPWR_ATTR_TRIGGERED_CURRENT_LIMIT);
                }
                set
                {
                    Adapter.SetAttributeViReal64(ChannelName, IviDCPwrAttribute.IVIDCPWR_ATTR_TRIGGERED_CURRENT_LIMIT, value);
                }
            }

            public double TriggeredVoltageLevel
            {
                get
                {
                    return Adapter.GetAttributeViReal64(ChannelName, IviDCPwrAttribute.IVIDCPWR_ATTR_TRIGGERED_VOLTAGE_LEVEL);
                }
                set
                {
                    Adapter.SetAttributeViReal64(ChannelName, IviDCPwrAttribute.IVIDCPWR_ATTR_TRIGGERED_VOLTAGE_LEVEL, value);
                }
            }

            public double VoltageLevel
            {
                get
                {
                    return Adapter.GetAttributeViReal64(ChannelName, IviDCPwrAttribute.IVIDCPWR_ATTR_VOLTAGE_LEVEL);
                }
                set
                {
                    Adapter.SetAttributeViReal64(ChannelName, IviDCPwrAttribute.IVIDCPWR_ATTR_VOLTAGE_LEVEL, value);
                }
            }

            public string Name
            {
                get { return ChannelName; }
            }
        }
    
    }
}
