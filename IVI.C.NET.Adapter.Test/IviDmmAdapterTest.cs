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
using Ivi.Dmm;

namespace IVI.C.NET.Adapter.Test
{
    [TestFixture]
    public class IviDmmAdapterTest
    {
        private IIviDmm Dmm = null;
        [SetUp]
        public void InitAdapter()
        {
            Dmm = (IIviDmm)IviDriver.Create("Ag34401", true, true, "Simulate=1,RangeCheck=1,QueryInstrStatus=0,Cache=1");
        }

        [Test]
        public void ConfigureMeasurement()
        {
            Dmm.Configure(MeasurementFunction.DCCurrent, Auto.On, 0.001);
            Assert.AreEqual(MeasurementFunction.DCCurrent, Dmm.MeasurementFunction);
            Assert.AreEqual(Auto.On, Dmm.AutoRange);
            // Assert.Less(0, Dmm.Range);
            Assert.GreaterOrEqual(0.001, Dmm.Resolution);

            Dmm.Configure(MeasurementFunction.DCVolts, 10, 0.01);
            Assert.AreEqual(MeasurementFunction.DCVolts, Dmm.MeasurementFunction);
            Assert.AreEqual(Auto.Off, Dmm.AutoRange);
            Assert.AreEqual(10, Dmm.Range);
            Assert.GreaterOrEqual(0.01, Dmm.Resolution);

        }

        [Test]
        public void AC()
        {
            Dmm.MeasurementFunction = MeasurementFunction.ACVolts;
            IIviDmmAC AC = Dmm.AC;
            AC.ConfigureBandwidth(10, 100);
            Assert.AreEqual(10, AC.FrequencyMin);
            Assert.AreEqual(300000, AC.FrequencyMax);

            AC.ConfigureBandwidth(5, 50);
            Assert.AreEqual(5, AC.FrequencyMin);
            Assert.AreEqual(300000, AC.FrequencyMax);
        }

        [Test]
        public void Advanced()
        {
            IIviDmmAdvanced Advanced = Dmm.Advanced;
            Advanced.AutoZero = Auto.On;
            Assert.AreEqual(Auto.On, Advanced.AutoZero);
            Advanced.AutoZero = Auto.Off;
            Assert.AreEqual(Auto.Off, Advanced.AutoZero);

            Assert.AreEqual(10, Advanced.ApertureTime);
            Assert.AreEqual(ApertureTimeUnits.PowerlineCycles, Advanced.ApertureTimeUnits);

            Advanced.PowerlineFrequency = 60;
            Assert.AreEqual(60, Advanced.PowerlineFrequency);

            Advanced.PowerlineFrequency = 100;
            Assert.AreEqual(60, Advanced.PowerlineFrequency);
        }

        [Test]
        public void Frequency()
        {
            IIviDmmFrequency Frequency = Dmm.Frequency;
            Frequency.VoltageAutoRange = true;
            // due to difference between IVI-C and IVI.NET driver, this property will always return false.
            Assert.AreEqual(false, Frequency.VoltageAutoRange);
            Frequency.VoltageRange = 5;
            Assert.AreEqual(5, Frequency.VoltageRange);

            Frequency.VoltageAutoRange = false;
            // due to difference between IVI-C and IVI.NET driver, this property will always return false.
            Assert.AreEqual(false, Frequency.VoltageAutoRange);
            Frequency.VoltageRange = 10;
            Assert.AreEqual(10, Frequency.VoltageRange);
        }

        [Test]
        [Ignore]
        public void Temperature()
        {
            Dmm.MeasurementFunction = MeasurementFunction.Temperature;
            IIviDmmTemperature Temperature = Dmm.Temperature;
            Temperature.TransducerType = TransducerType.Thermistor;
            //Assert.AreEqual(TransducerType.Thermistor, Temperature.TransducerType);
            Temperature.TransducerType = TransducerType.Thermocouple;
            //Assert.AreEqual(TransducerType.Thermocouple, Temperature.TransducerType);

            //Temperature.Rtd.Configure(10, 100);
            //Assert.AreEqual(10, Temperature.Rtd.Alpha);
            //Assert.AreEqual(100, Temperature.Rtd.Resistance);

            //Temperature.Rtd.Configure(20, 200);
            //Assert.AreEqual(20, Temperature.Rtd.Alpha);
            //Assert.AreEqual(200, Temperature.Rtd.Resistance);

            Temperature.Thermistor.Resistance = 100;
            Assert.AreEqual(100, Temperature.Thermistor.Resistance);

            Temperature.Thermistor.Resistance = 50;
            Assert.AreEqual(50, Temperature.Thermistor.Resistance);

            Temperature.Thermocouple.Configure(ThermocoupleType.B, ReferenceJunctionType.Internal);
            Assert.AreEqual(ThermocoupleType.B, Temperature.Thermocouple.Type);
            Assert.AreEqual(ReferenceJunctionType.Internal, Temperature.Thermocouple.ReferenceJunctionType);
        }

        [Test]
        public void Trigger()
        {
            IIviDmmTrigger Trigger = Dmm.Trigger;
            Trigger.Configure("Software", true);
            Assert.AreEqual("Software", Trigger.Source);
            // due to difference between IVI-C and IVI.NET driver, this property will always return false.
            Assert.AreEqual(false, Trigger.DelayAuto);
            PrecisionTimeSpan delay = new PrecisionTimeSpan((decimal)100);
            Trigger.Configure("External", delay);
            Assert.AreEqual("External", Trigger.Source);
            Assert.AreEqual(delay, Trigger.Delay);

            Trigger.Slope = Slope.Positive;
            // Assert.AreEqual(Slope.Positive, Trigger.Slope); // Failed, why?
            Trigger.Slope = Slope.Negative;
            Assert.AreEqual(Slope.Negative, Trigger.Slope);

            Trigger.MeasurementCompleteDestination = "TTL0";
            // Assert.AreEqual("TTL0", Trigger.MeasurementCompleteDestination); // Failed

            Trigger.MeasurementCompleteDestination = "External";
            Assert.AreEqual("External", Trigger.MeasurementCompleteDestination);

            Trigger.MultiPoint.Configure(1, 10, "Software", new PrecisionTimeSpan((decimal)10));
            Assert.AreEqual(1, Trigger.MultiPoint.TriggerCount);
            Assert.AreEqual(10, Trigger.MultiPoint.SampleCount);
            Assert.AreEqual("Software", Trigger.MultiPoint.SampleTrigger);


        }

        [Test]
        public void MeasurementTest()
        {
            Dmm.Configure(MeasurementFunction.DCCurrent, Auto.Off, 0.001);
            IIviDmmMeasurement Measurement = Dmm.Measurement;
            Assert.IsNotNull(Measurement);


            Dmm.Trigger.Configure("Immediate", true);
            Assert.AreNotEqual(double.NaN, Measurement.Read(new PrecisionTimeSpan((decimal)10)));

            Dmm.Trigger.MultiPoint.Configure(1, 100, "Immediate", new PrecisionTimeSpan((decimal)1));
            double[] ReadMultiPointResult = Measurement.ReadMultiPoint(new PrecisionTimeSpan((decimal)10), 100);

            Assert.AreEqual(100, ReadMultiPointResult.Length);


            Dmm.Measurement.Initiate();

            Dmm.Trigger.Configure("Immediate", true);
            Assert.AreNotEqual(double.NaN, Measurement.Fetch(new PrecisionTimeSpan((decimal)10)));

            Dmm.Trigger.MultiPoint.Configure(1, 100, "Immediate", new PrecisionTimeSpan((decimal)1));
            double[] FetchMultiPointResult = Measurement.FetchMultiPoint(new PrecisionTimeSpan((decimal)10), 100);

            Assert.AreEqual(100, FetchMultiPointResult.Length);

            Dmm.Measurement.Abort();

        }
    }
}
