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
using NUnit.Framework;
using Ivi.Driver;
using Ivi.Scope;

namespace IVI.C.NET.Adapter.Test
{
    [TestFixture]
    public class IviScopeAdapterTest
    {
        private IIviScope Scope = null;
        [SetUp]
        public void InitAdapter()
        {
            Scope = (IIviScope)IviDriver.Create("ag1000ni", true, true, "Simulate=1,RangeCheck=1,QueryInstrStatus=0,Cache=1");
        }

        [Test]
        public void Acquisition()
        {
            IIviScopeAcquisition Acquisition = Scope.Acquisition;

            Assert.IsNotNull(Acquisition);
            Acquisition.ConfigureRecord(new PrecisionTimeSpan((decimal)10), 100, new PrecisionTimeSpan((decimal)5));

            Assert.AreEqual(10, Acquisition.TimePerRecord.TotalSeconds);
            Assert.AreEqual(100, Acquisition.RecordLength);
            Assert.AreEqual(5, Acquisition.StartTime.TotalSeconds);

            Acquisition.Type = AcquisitionType.Average;
            Assert.AreEqual(AcquisitionType.Average, Acquisition.Type);

            Acquisition.Type = AcquisitionType.Normal;
            Assert.AreEqual(AcquisitionType.Normal, Acquisition.Type);

        }

        [Test]
        public void Channel()
        {
            foreach (IIviScopeChannel Channel in Scope.Channels)
            {
                Channel.Configure(10, 0, VerticalCoupling.DC, 10, true);
                Assert.AreEqual(10, Channel.Range);
                Assert.AreEqual(0, Channel.Offset);
                Assert.AreEqual(VerticalCoupling.DC, Channel.Coupling);
                Assert.AreEqual(10, Channel.ProbeAttenuation);
                Assert.AreEqual(true, Channel.Enabled);

                Channel.Configure(9, 1, VerticalCoupling.AC, 100, false);
                Assert.AreEqual(9, Channel.Range);
                Assert.AreEqual(1, Channel.Offset);
                Assert.AreEqual(VerticalCoupling.AC, Channel.Coupling);
                Assert.AreEqual(100, Channel.ProbeAttenuation);
                Assert.AreEqual(false, Channel.Enabled);

                Channel.Enabled = false;
                Assert.AreEqual(false, Channel.Enabled);

                Channel.Enabled = true;
                Assert.AreEqual(true, Channel.Enabled);

                Channel.InputImpedance = 1E6;
                Assert.AreEqual(1E6, Channel.InputImpedance);

                Channel.InputFrequencyMaximum = 10E6;
                Assert.AreEqual(10E6, Channel.InputFrequencyMaximum);
                Channel.InputFrequencyMaximum = 100E6;
                Assert.AreEqual(100E6, Channel.InputFrequencyMaximum);


                Scope.Trigger.Configure(TriggerType.Edge, new PrecisionTimeSpan((decimal)1));
                IWaveform<double> waveformDouble = Scope.Measurement.CreateWaveformDouble(100);
                waveformDouble = Channel.Measurement.ReadWaveform(new PrecisionTimeSpan((decimal) 10), waveformDouble);

                Assert.AreEqual(100, waveformDouble.ValidPointCount);

                IWaveform<byte> waveformByte = Scope.Measurement.CreateWaveformByte(100);
                waveformByte = Channel.Measurement.ReadWaveform(new PrecisionTimeSpan((decimal)10), waveformByte);

                Assert.AreEqual(100, waveformByte.ValidPointCount);

            }
        }

        [Test]
        public void Measurement()
        {
            IIviScopeMeasurement Measurement = Scope.Measurement;
            Assert.IsNotNull(Measurement);

            Assert.AreEqual(AcquisitionStatus.Complete, Measurement.Status());
            Measurement.AutoSetup();
            Measurement.Initiate();
            Assert.AreEqual(AcquisitionStatus.Complete, Measurement.Status());
            Measurement.Abort();

            IWaveform<byte> waveformByte = Measurement.CreateWaveformByte(100);
            IWaveform<short> waveformInt16 = Measurement.CreateWaveformInt16(100);
            IWaveform<int> waveformInt32 = Measurement.CreateWaveformInt32(100);
            IWaveform<double> waveformDouble = Measurement.CreateWaveformDouble(100);

            Assert.AreEqual(100, waveformByte.Capacity);
            Assert.AreEqual(100, waveformInt16.Capacity);
            Assert.AreEqual(100, waveformInt32.Capacity);
            Assert.AreEqual(100, waveformDouble.Capacity);

        }

        [Test]
        public void ReferenceLevel()
        {
            IIviScopeReferenceLevel ReferenceLevel = Scope.ReferenceLevel;
            Assert.IsNotNull(ReferenceLevel);

            ReferenceLevel.Configure(0.7, 1.5, 3.0);
            Assert.AreEqual(0.7, ReferenceLevel.Low);
            Assert.AreEqual(1.5, ReferenceLevel.Mid);
            Assert.AreEqual(3.0, ReferenceLevel.High);
        }

        [Test]
        public void Trigger()
        {
            IIviScopeTrigger Trigger = Scope.Trigger;
            Assert.IsNotNull(Trigger);

            Trigger.Configure(TriggerType.Edge, new PrecisionTimeSpan((decimal)1));
            Assert.AreEqual(TriggerType.Edge, Trigger.Type);
            Assert.AreEqual(1, Trigger.Holdoff.TotalSeconds);

            Trigger.Source = "External";
            Assert.AreEqual("External", Trigger.Source);

            Trigger.Source = "TTL0";
            Assert.AreEqual("TTL0", Trigger.Source);

            Trigger.Coupling = TriggerCoupling.DC;
            Assert.AreEqual(TriggerCoupling.DC, Trigger.Coupling);

            Trigger.Coupling = TriggerCoupling.AC;
            Assert.AreEqual(TriggerCoupling.AC, Trigger.Coupling);

            Trigger.Continuous = false;
            Assert.AreEqual(false, Trigger.Continuous);

            Trigger.Continuous = true;
            Assert.AreEqual(true, Trigger.Continuous);

            // Trigger.ACLine.Slope = ACLineSlope.Positive;
            // Assert.AreEqual(ACLineSlope.Positive, Trigger.ACLine.Slope);

            // Trigger.ACLine.Slope = ACLineSlope.Negative;
            // Assert.AreEqual(ACLineSlope.Negative, Trigger.ACLine.Slope);

            Trigger.Edge.Slope = Slope.Positive;
            Assert.AreEqual(Slope.Positive, Trigger.Edge.Slope);
            Trigger.Edge.Slope = Slope.Negative;
            Assert.AreEqual(Slope.Negative, Trigger.Edge.Slope);

        }


    }
}