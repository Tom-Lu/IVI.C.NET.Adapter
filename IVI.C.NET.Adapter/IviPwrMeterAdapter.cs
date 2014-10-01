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
using Ivi.PwrMeter;
using IVI.C.NET.Adapter.IviCInterop;

namespace IVI.C.NET.Adapter
{
    public class IviPwrMeterAdapter : DriverAdapterBase<IviCInterop.IviPwrMeter>, IIviPwrMeter
    {
        #region Enum Mapping

        private static IviEnumCMapping<OperationState, int> PwrMeterOperationState = IviEnumCMapping<OperationState, int>.Instance
            .Map(OperationState.Complete, IviPwrMeterAttribute.IVIPWRMETER_VAL_MEAS_COMPLETE)
            .Map(OperationState.InProgress, IviPwrMeterAttribute.IVIPWRMETER_VAL_MEAS_IN_PROGRESS)
            .Map(OperationState.Unknown, IviPwrMeterAttribute.IVIPWRMETER_VAL_MEAS_STATUS_UNKNOWN);

        private static IviEnumCMapping<Units, int> PwrMeterUnits = IviEnumCMapping<Units, int>.Instance
            .Map(Units.dBm, IviPwrMeterAttribute.IVIPWRMETER_VAL_DBM)
            .Map(Units.dBmV, IviPwrMeterAttribute.IVIPWRMETER_VAL_DBMV)
            .Map(Units.dBuV, IviPwrMeterAttribute.IVIPWRMETER_VAL_DBUV)
            .Map(Units.Watts, IviPwrMeterAttribute.IVIPWRMETER_VAL_WATTS);

        private static IviEnumCMapping<MeasurementOperator, int> PwrMeterMeasurementOperator = IviEnumCMapping<MeasurementOperator, int>.Instance
            .Map(MeasurementOperator.Difference, IviPwrMeterAttribute.IVIPWRMETER_VAL_DIFFERENCE)
            .Map(MeasurementOperator.Sum, IviPwrMeterAttribute.IVIPWRMETER_VAL_SUM)
            .Map(MeasurementOperator.Quotient, IviPwrMeterAttribute.IVIPWRMETER_VAL_QUOTIENT)
            .Map(MeasurementOperator.None, IviPwrMeterAttribute.IVIPWRMETER_VAL_NONE);

        private static IviEnumCMapping<Slope, int> PwrMeterSlope = IviEnumCMapping<Slope, int>.Instance
            .Map(Slope.Positive, IviPwrMeterAttribute.IVIPWRMETER_VAL_POSITIVE)
            .Map(Slope.Negative, IviPwrMeterAttribute.IVIPWRMETER_VAL_NEGATIVE);

        #endregion

        private IIviPwrMeterChannelCollection PwrMeterChannels = null;
        private IIviPwrMeterMeasurement PwrMeterMeasurement = null;
        private IIviPwrMeterReferenceOscillator PwrMeterReferenceOscillator = null;
        private IIviPwrMeterTrigger PwrMeterTrigger = null;
        public IviPwrMeterAdapter(string name, bool idQuery, bool reset, string options)
            : base(name, idQuery, reset, options)
        {
            PwrMeterChannels = new IviPwrMeterChannelCollection(this);
            PwrMeterMeasurement = new IviPwrMeterMeasurement(this);
            PwrMeterReferenceOscillator = new IviPwrMeterReferenceOscillator(this);
            PwrMeterTrigger = new IviPwrMeterTrigger(this);
        }

        public IIviPwrMeterChannelCollection Channels
        {
            get { return PwrMeterChannels; }
        }

        public IIviPwrMeterMeasurement Measurement
        {
            get { return PwrMeterMeasurement; }
        }

        public IIviPwrMeterReferenceOscillator ReferenceOscillator
        {
            get { return PwrMeterReferenceOscillator; }
        }

        public IIviPwrMeterTrigger Trigger
        {
            get { return PwrMeterTrigger; }
        }

        internal class IviPwrMeterChannelCollection : IIviPwrMeterChannelCollection
        {
            private IDriverAdapterBase Adapter;
            private IviCInterop.IviPwrMeter IviPwrMeterInterop = null;
            private IList<IIviPwrMeterChannel> Channels = null;
            private IList<string> ChannelNames = null;
            public IviPwrMeterChannelCollection(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviPwrMeterInterop = (IviCInterop.IviPwrMeter)Adapter.Interop;
                int ChannelCount = Adapter.GetAttributeViInt32(IviPwrMeterAttribute.IVIPWRMETER_ATTR_CHANNEL_COUNT);

                Channels = new List<IIviPwrMeterChannel>();
                ChannelNames = new List<string>();
                for (int Index = 1; Index <= ChannelCount; Index++)
                {
                    IIviPwrMeterChannel Channel = new IviPwrMeterChannel(Adapter, Index);
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

            public IIviPwrMeterChannel this[string name]
            {
                get { return Channels[ChannelNames.IndexOf(name)]; }
            }

            public IEnumerator<IIviPwrMeterChannel> GetEnumerator()
            {
                return Channels.GetEnumerator();
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return Channels.GetEnumerator();
            }

            public OperationState CalibrationState
            {
                get 
                { 
                    int CalibrationStatus = 0;
                    Adapter.ViSessionStatusCheck(IviPwrMeterInterop.IsCalibrationComplete(Adapter.Session, ref CalibrationStatus));
                    return PwrMeterOperationState.getEnum(CalibrationStatus);
                }
            }

            public Units Units
            {
                get
                {
                    return PwrMeterUnits.getEnum(Adapter.GetAttributeViInt32(IviPwrMeterAttribute.IVIPWRMETER_ATTR_UNITS));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviPwrMeterAttribute.IVIPWRMETER_ATTR_UNITS, PwrMeterUnits.getC_Value(value));
                }
            }

            public void Zero()
            {
                Adapter.ViSessionStatusCheck(IviPwrMeterInterop.ZeroAllChannels(Adapter.Session));
            }

            public OperationState ZeroState
            {
                get 
                {
                    int ZeroStatus = 0;
                    Adapter.ViSessionStatusCheck(IviPwrMeterInterop.IsZeroComplete(Adapter.Session, ref ZeroStatus));
                    return PwrMeterOperationState.getEnum(ZeroStatus);
                }
            }
        }

        internal class IviPwrMeterChannel : IIviPwrMeterChannel
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviPwrMeter IviPwrMeterInterop = null;
            private int Index;
            private string ChannelName;

            private IIviPwrMeterAveraging PwrMeterAveraging = null;
            private IIviPwrMeterDutyCycleCorrection PwrMeterDutyCycleCorrection = null;
            private IIviPwrMeterRange PwrMeterRange = null;
            public IviPwrMeterChannel(IDriverAdapterBase Adapter, int Index)
            {
                this.Adapter = Adapter;
                IviPwrMeterInterop = (IviCInterop.IviPwrMeter)Adapter.Interop;
                this.Index = Index;

                try
                {
                    StringBuilder NameValue = new StringBuilder(256);
                    Adapter.ViSessionStatusCheck(IviPwrMeterInterop.GetChannelName(Adapter.Session, Index, NameValue.Capacity, NameValue));
                    ChannelName = NameValue.ToString();
                }
                catch
                {
                    ChannelName = string.Empty;
                }

                PwrMeterAveraging = new IviPwrMeterAveraging(Adapter, ChannelName);
                PwrMeterDutyCycleCorrection = new IviPwrMeterDutyCycleCorrection(Adapter, ChannelName);
                PwrMeterRange = new IviPwrMeterRange(Adapter, ChannelName);
            }

            public IIviPwrMeterAveraging Averaging
            {
                get { return PwrMeterAveraging; }
            }

            public void Calibrate()
            {
                Adapter.ViSessionStatusCheck(IviPwrMeterInterop.Calibrate(Adapter.Session, ChannelName));
            }

            public double CorrectionFrequency
            {
                get
                {
                    return Adapter.GetAttributeViReal64(ChannelName, IviPwrMeterAttribute.IVIPWRMETER_ATTR_CORRECTION_FREQUENCY);
                }
                set
                {
                    Adapter.SetAttributeViReal64(ChannelName, IviPwrMeterAttribute.IVIPWRMETER_ATTR_CORRECTION_FREQUENCY, value);
                }
            }

            public IIviPwrMeterDutyCycleCorrection DutyCycle
            {
                get { return PwrMeterDutyCycleCorrection; }
            }

            public bool Enabled
            {
                get
                {
                    return Adapter.GetAttributeViBoolean(ChannelName, IviPwrMeterAttribute.IVIPWRMETER_ATTR_CHANNEL_ENABLED);
                }
                set
                {
                    Adapter.SetAttributeViBoolean(ChannelName, IviPwrMeterAttribute.IVIPWRMETER_ATTR_CHANNEL_ENABLED, value);
                }
            }

            public OperationState MeasurementState
            {
                get
                {
                    int measurementStatus = 0;
                    Adapter.ViSessionStatusCheck(IviPwrMeterInterop.IsMeasurementComplete(Adapter.Session, ref measurementStatus));
                    return PwrMeterOperationState.getEnum(measurementStatus);
                }
            }

            public double Offset
            {
                get
                {
                    return Adapter.GetAttributeViReal64(ChannelName, IviPwrMeterAttribute.IVIPWRMETER_ATTR_OFFSET);
                }
                set
                {
                    Adapter.SetAttributeViReal64(ChannelName, IviPwrMeterAttribute.IVIPWRMETER_ATTR_OFFSET, value);
                }
            }

            public IIviPwrMeterRange Range
            {
                get { return PwrMeterRange; }
            }

            public void Zero()
            {
                Adapter.ViSessionStatusCheck(IviPwrMeterInterop.Zero(Adapter.Session, ChannelName));
            }

            public string Name
            {
                get { return ChannelName; }
            }
        }

        internal class IviPwrMeterAveraging : IIviPwrMeterAveraging
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviPwrMeter IviPwrMeterInterop = null;
            private string ChannelName;

            public IviPwrMeterAveraging(IDriverAdapterBase Adapter, string ChannelName)
            {
                this.Adapter = Adapter;
                IviPwrMeterInterop = (IviCInterop.IviPwrMeter)Adapter.Interop;
                this.ChannelName = ChannelName;
            }

            public int Count
            {
                get
                {
                    return Adapter.GetAttributeViInt32(ChannelName, IviPwrMeterAttribute.IVIPWRMETER_ATTR_AVERAGING_COUNT);
                }
                set
                {
                    Adapter.SetAttributeViInt32(ChannelName, IviPwrMeterAttribute.IVIPWRMETER_ATTR_AVERAGING_COUNT, value);
                }
            }

            public bool CountAuto
            {
                get
                {
                    return Adapter.GetAttributeViBoolean(ChannelName, IviPwrMeterAttribute.IVIPWRMETER_ATTR_AVERAGING_AUTO_ENABLED);
                }
                set
                {
                    Adapter.SetAttributeViBoolean(ChannelName, IviPwrMeterAttribute.IVIPWRMETER_ATTR_AVERAGING_AUTO_ENABLED, value);
                }
            }
        }

        internal class IviPwrMeterDutyCycleCorrection : IIviPwrMeterDutyCycleCorrection
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviPwrMeter IviPwrMeterInterop = null;
            private string ChannelName;

            public IviPwrMeterDutyCycleCorrection(IDriverAdapterBase Adapter, string ChannelName)
            {
                this.Adapter = Adapter;
                IviPwrMeterInterop = (IviCInterop.IviPwrMeter)Adapter.Interop;
                this.ChannelName = ChannelName;
            }

            public void Configure(bool enabled, double correction)
            {
                Adapter.ViSessionStatusCheck(IviPwrMeterInterop.ConfigureDutyCycleCorrection(Adapter.Session, ChannelName, enabled, correction));
            }

            public bool Enabled
            {
                get
                {
                    return Adapter.GetAttributeViBoolean(ChannelName, IviPwrMeterAttribute.IVIPWRMETER_ATTR_DUTY_CYCLE_CORRECTION_ENABLED);
                }
                set
                {
                    Adapter.SetAttributeViBoolean(ChannelName, IviPwrMeterAttribute.IVIPWRMETER_ATTR_DUTY_CYCLE_CORRECTION_ENABLED, value);
                }
            }

            public double Value
            {
                get
                {
                    return Adapter.GetAttributeViReal64(ChannelName, IviPwrMeterAttribute.IVIPWRMETER_ATTR_DUTY_CYCLE_CORRECTION);
                }
                set
                {
                    Adapter.SetAttributeViReal64(ChannelName, IviPwrMeterAttribute.IVIPWRMETER_ATTR_DUTY_CYCLE_CORRECTION, value);
                }
            }
        }

        internal class IviPwrMeterRange : IIviPwrMeterRange
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviPwrMeter IviPwrMeterInterop = null;
            private string ChannelName;

            public IviPwrMeterRange(IDriverAdapterBase Adapter, string ChannelName)
            {
                this.Adapter = Adapter;
                IviPwrMeterInterop = (IviCInterop.IviPwrMeter)Adapter.Interop;
                this.ChannelName = ChannelName;
            }

            public bool Auto
            {
                get
                {
                    return Adapter.GetAttributeViBoolean(ChannelName, IviPwrMeterAttribute.IVIPWRMETER_ATTR_RANGE_AUTO_ENABLED);
                }
                set
                {
                    Adapter.SetAttributeViBoolean(ChannelName, IviPwrMeterAttribute.IVIPWRMETER_ATTR_RANGE_AUTO_ENABLED, value);
                }
            }

            public void Configure(bool rangeAuto)
            {
                Adapter.ViSessionStatusCheck(IviPwrMeterInterop.ConfigureRangeAutoEnabled(Adapter.Session, ChannelName, rangeAuto));
            }

            public void Configure(double lower, double upper)
            {
                Adapter.ViSessionStatusCheck(IviPwrMeterInterop.ConfigureRange(Adapter.Session, ChannelName, lower, upper));
            }

            public double Lower
            {
                get
                {
                    return Adapter.GetAttributeViReal64(ChannelName, IviPwrMeterAttribute.IVIPWRMETER_ATTR_RANGE_LOWER);
                }
                set
                {
                    Adapter.SetAttributeViReal64(ChannelName, IviPwrMeterAttribute.IVIPWRMETER_ATTR_RANGE_LOWER, value);
                }
            }

            public double Upper
            {
                get
                {
                    return Adapter.GetAttributeViReal64(ChannelName, IviPwrMeterAttribute.IVIPWRMETER_ATTR_RANGE_UPPER);
                }
                set
                {
                    Adapter.SetAttributeViReal64(ChannelName, IviPwrMeterAttribute.IVIPWRMETER_ATTR_RANGE_UPPER, value);
                }
            }
        }

        internal class IviPwrMeterMeasurement : IIviPwrMeterMeasurement
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviPwrMeter IviPwrMeterInterop = null;
            public IviPwrMeterMeasurement(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviPwrMeterInterop = (IviCInterop.IviPwrMeter)Adapter.Interop;
            }

            public void Abort()
            {
                Adapter.ViSessionStatusCheck(IviPwrMeterInterop.Abort(Adapter.Session));
            }

            public void Configure(MeasurementOperator measurementOperator, string operand1, string operand2)
            {
                Adapter.ViSessionStatusCheck(IviPwrMeterInterop.ConfigureMeasurement(Adapter.Session, PwrMeterMeasurementOperator.getC_Value(measurementOperator), operand1, operand2));
            }

            public double Fetch()
            {
                double result = double.NaN;
                Adapter.ViSessionStatusCheck(IviPwrMeterInterop.Fetch(Adapter.Session, ref result));
                return result;
            }

            public double FetchChannel(string channelName)
            {
                double result = double.NaN;
                Adapter.ViSessionStatusCheck(IviPwrMeterInterop.FetchChannel(Adapter.Session, channelName, ref result));
                return result;
            }

            public OperationState GetMeasurementComplete()
            {
                int measurementStatus = 0;
                Adapter.ViSessionStatusCheck(IviPwrMeterInterop.IsMeasurementComplete(Adapter.Session, ref measurementStatus));
                return PwrMeterOperationState.getEnum(measurementStatus);
            }

            public void Initiate()
            {
                Adapter.ViSessionStatusCheck(IviPwrMeterInterop.Initiate(Adapter.Session));
            }

            public double Read(Ivi.Driver.PrecisionTimeSpan maximumTime)
            {
                double result = double.NaN;
                Adapter.ViSessionStatusCheck(IviPwrMeterInterop.Read(Adapter.Session, (int)maximumTime.TotalMilliseconds, ref result));
                return result;
            }

            public double ReadChannel(string channelName, Ivi.Driver.PrecisionTimeSpan maxTime)
            {
                double result = double.NaN;
                Adapter.ViSessionStatusCheck(IviPwrMeterInterop.ReadChannel(Adapter.Session, channelName, (int)maxTime.TotalMilliseconds, ref result));
                return result;
            }

            public void SendSoftwareTrigger()
            {
                Adapter.ViSessionStatusCheck(IviPwrMeterInterop.SendSoftwareTrigger(Adapter.Session));
            }
        }

        internal class IviPwrMeterReferenceOscillator : IIviPwrMeterReferenceOscillator
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviPwrMeter IviPwrMeterInterop = null;
            public IviPwrMeterReferenceOscillator(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviPwrMeterInterop = (IviCInterop.IviPwrMeter)Adapter.Interop;
            }

            public void Configure(double frequency, double level)
            {
                Adapter.ViSessionStatusCheck(IviPwrMeterInterop.ConfigureRefOscillator(Adapter.Session, frequency, level));
            }

            public bool Enabled
            {
                get
                {
                    return Adapter.GetAttributeViBoolean(IviPwrMeterAttribute.IVIPWRMETER_ATTR_REF_OSCILLATOR_ENABLED);
                }
                set
                {
                    Adapter.SetAttributeViBoolean(IviPwrMeterAttribute.IVIPWRMETER_ATTR_REF_OSCILLATOR_ENABLED, value);
                }
            }

            public double Frequency
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviPwrMeterAttribute.IVIPWRMETER_ATTR_REF_OSCILLATOR_FREQUENCY);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviPwrMeterAttribute.IVIPWRMETER_ATTR_REF_OSCILLATOR_FREQUENCY, value);
                }
            }

            public double Level
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviPwrMeterAttribute.IVIPWRMETER_ATTR_REF_OSCILLATOR_LEVEL);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviPwrMeterAttribute.IVIPWRMETER_ATTR_REF_OSCILLATOR_LEVEL, value);
                }
            }
        }

        internal class IviPwrMeterTrigger : IIviPwrMeterTrigger
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviPwrMeter IviPwrMeterInterop = null;
            private IIviPwrMeterInternalTrigger PwrMeterInternalTrigger = null;
            public IviPwrMeterTrigger(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviPwrMeterInterop = (IviCInterop.IviPwrMeter)Adapter.Interop;
                PwrMeterInternalTrigger = new IviPwrMeterInternalTrigger(Adapter);
            }

            public IIviPwrMeterInternalTrigger Internal
            {
                get { throw new NotImplementedException(); }
            }

            public string Source
            {
                get
                {
                    return Adapter.GetAttributeViString(IviPwrMeterAttribute.IVIPWRMETER_ATTR_TRIGGER_SOURCE);
                }
                set
                {
                    Adapter.SetAttributeViString(IviPwrMeterAttribute.IVIPWRMETER_ATTR_TRIGGER_SOURCE, value);
                }
            }
        }

        internal class IviPwrMeterInternalTrigger : IIviPwrMeterInternalTrigger
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviPwrMeter IviPwrMeterInterop = null;
            public IviPwrMeterInternalTrigger(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviPwrMeterInterop = (IviCInterop.IviPwrMeter)Adapter.Interop;
            }

            public void Configure(string eventSource, Slope slope)
            {
                Adapter.ViSessionStatusCheck(IviPwrMeterInterop.ConfigureInternalTrigger(Adapter.Session, eventSource, PwrMeterSlope.getC_Value(slope)));
            }

            public string EventSource
            {
                get
                {
                    return Adapter.GetAttributeViString(IviPwrMeterAttribute.IVIPWRMETER_ATTR_INTERNAL_TRIGGER_EVENT_SOURCE);
                }
                set
                {
                    Adapter.SetAttributeViString(IviPwrMeterAttribute.IVIPWRMETER_ATTR_INTERNAL_TRIGGER_EVENT_SOURCE, value);
                }
            }

            public double Level
            {
                get
                {
                    return Adapter.GetAttributeViReal64(IviPwrMeterAttribute.IVIPWRMETER_ATTR_INTERNAL_TRIGGER_LEVEL);
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviPwrMeterAttribute.IVIPWRMETER_ATTR_INTERNAL_TRIGGER_LEVEL, value);
                }
            }

            public Slope Slope
            {
                get
                {
                    return PwrMeterSlope.getEnum(Adapter.GetAttributeViInt32(IviPwrMeterAttribute.IVIPWRMETER_ATTR_INTERNAL_TRIGGER_SLOPE));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviPwrMeterAttribute.IVIPWRMETER_ATTR_INTERNAL_TRIGGER_SLOPE, PwrMeterSlope.getC_Value(value));
                }
            }
        }
    }
}
