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
using Ivi.DCPwr;

namespace IVI.C.NET.Adapter.Test
{
    [TestFixture]
    public class IviDCPwrAdapterTest
    {
        private IIviDCPwr DCPwr = null;
        [SetUp]
        public void InitAdapter()
        {
            DCPwr = (IIviDCPwr)IviDriver.Create("AgE36xx", true, true, "Simulate=1,RangeCheck=1,QueryInstrStatus=0,Cache=1");
        }

        [Test]
        public void OutputTest()
        {
            Assert.AreEqual(1, DCPwr.Outputs.Count);

            foreach (IIviDCPwrOutput Output in DCPwr.Outputs)
            {
                Output.ConfigureCurrentLimit(CurrentLimitBehavior.Regulate, 1);
                Output.ConfigureOvp(true, 10);
                Output.ConfigureRange(RangeType.Voltage, 50);
                Output.VoltageLevel = 8;
                Output.TriggerSource = "Immediate";
                Output.TriggeredCurrentLimit = 0.5;
                Output.TriggeredVoltageLevel = 7;
                Output.Enabled = true;
                
                Assert.AreEqual(CurrentLimitBehavior.Regulate, Output.CurrentLimitBehavior);
                Assert.AreEqual(1, Output.CurrentLimit);
                Assert.AreEqual(true, Output.OvpEnabled);
                Assert.AreEqual(10, Output.OvpLimit);
                Assert.AreEqual(8, Output.VoltageLevel);
                Assert.AreEqual("Immediate", Output.TriggerSource);
                Assert.AreEqual(0.5, Output.TriggeredCurrentLimit);
                Assert.AreEqual(7, Output.TriggeredVoltageLevel);
                Assert.AreEqual(true, Output.Enabled);
                Assert.IsTrue(Math.Abs((Output.Measure(MeasurementType.Voltage) - Output.VoltageLevel)) < 0.5);


                Output.ConfigureCurrentLimit(CurrentLimitBehavior.Trip, 5);
                Output.ConfigureOvp(false, 8);
                //Output.ConfigureRange(RangeType.Current, 10);
                Output.VoltageLevel = 5;
                Output.TriggerSource = "Software";
                Output.TriggeredCurrentLimit = 2;
                Output.TriggeredVoltageLevel = 5.1;
                Output.Enabled = false;

                Assert.AreEqual(CurrentLimitBehavior.Trip, Output.CurrentLimitBehavior);
                Assert.AreEqual(5, Output.CurrentLimit);
                Assert.AreEqual(false, Output.OvpEnabled);
                Assert.AreEqual(8, Output.OvpLimit);
                Assert.AreEqual(5, Output.VoltageLevel);
                Assert.AreEqual("Software", Output.TriggerSource);
                Assert.AreEqual(2, Output.TriggeredCurrentLimit);
                Assert.AreEqual(5.1, Output.TriggeredVoltageLevel);
                Assert.AreEqual(false, Output.Enabled);
                Assert.IsTrue(Output.Measure(MeasurementType.Current) < 0.001);

            }
        }

        [Test]
        public void TriggerTest()
        {
            DCPwr.Trigger.Initiate();
            DCPwr.Trigger.SendSoftwareTrigger();
            DCPwr.Trigger.Abort();
        }
    }
}
