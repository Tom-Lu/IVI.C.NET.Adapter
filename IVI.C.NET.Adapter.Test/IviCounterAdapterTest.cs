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
using Ivi.Counter;

namespace IVI.C.NET.Adapter.Test
{
    [TestFixture]
    public class IviCounterAdapterTest
    {
        private IIviCounter Counter = null;

        [SetUp]
        public void InitAdapter()
        {
            Counter = (IIviCounter)IviDriver.Create("ag5313xni", true, true, "Simulate=1,RangeCheck=1,QueryInstrStatus=0,Cache=1");
        }

        [Test]
        public void Arm()
        {
            IIviCounterArm Arm = Counter.Arm;
            Assert.IsNotNull(Arm);

            Arm.Start.External.Configure("EXT", 5, Slope.Positive, new PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * 0.000001))));
            Arm.Stop.External.Configure("EXT", 3.3, Slope.Negative, new PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * 0.01))));

            Assert.AreEqual("EXT", Arm.Start.External.Source);
            Assert.AreEqual(5, Arm.Start.External.Level);
            Assert.AreEqual(Slope.Positive, Arm.Start.External.Slope);
            Assert.AreEqual(new PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * 0.000001))), Arm.Start.External.Delay);
            Assert.AreEqual("EXT", Arm.Stop.External.Source);
            Assert.AreEqual(3.3, Arm.Stop.External.Level);
            Assert.AreEqual(Slope.Negative, Arm.Stop.External.Slope);
            Assert.AreEqual(new PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * 0.01))), Arm.Stop.External.Delay);

            Arm.Start.Type = ArmType.Immediate;
            Arm.Stop.Type = ArmType.Immediate;
            Assert.AreEqual(ArmType.Immediate, Arm.Start.Type);
            Assert.AreEqual(ArmType.Immediate, Arm.Stop.Type);

            Arm.Start.Type = ArmType.External;
            Arm.Stop.Type = ArmType.External;
            Assert.AreEqual(ArmType.External, Arm.Start.Type);
            Assert.AreEqual(ArmType.External, Arm.Stop.Type);

        }

        [Test]
        public void DutyCycle()
        {
            IIviCounterDutyCycle DutyCycle = Counter.DutyCycle;
            Assert.IsNotNull(DutyCycle);

            DutyCycle.Configure("1", 1E7, 0.01);
            Assert.AreEqual("1", DutyCycle.Channel);
            Assert.AreEqual(1E7, DutyCycle.FrequencyEstimate);
            Assert.AreEqual(0.01, DutyCycle.Resolution);

            DutyCycle.Configure("1", 1E6, 0.1);
            Assert.AreEqual("1", DutyCycle.Channel);
            Assert.AreEqual(1E6, DutyCycle.FrequencyEstimate);
            Assert.AreEqual(0.1, DutyCycle.Resolution);

        }

        [Test]
        public void EdgeTime()
        {
            IIviCounterEdgeTime EdgeTime = Counter.EdgeTime;
            Assert.IsNotNull(EdgeTime);

            EdgeTime.Configure("1", new PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * 0.1))), new PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * 0.000001))));
            Assert.AreEqual("1", EdgeTime.Channel);
            Assert.AreEqual(new PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * 0.1))), EdgeTime.Estimate);
            Assert.AreEqual(new PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * 0.000001))), EdgeTime.Resolution);

            EdgeTime.Configure("1", new PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * 0.01))), new PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * 0.0000001))));
            Assert.AreEqual("1", EdgeTime.Channel);
            Assert.AreEqual(new PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * 0.01))), EdgeTime.Estimate);
            Assert.AreEqual(new PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * 0.0000001))), EdgeTime.Resolution);
        }

        [Test]
        public void Frequency()
        {
            IIviCounterFrequency Frequency = Counter.Frequency;
            Assert.IsNotNull(Frequency);

            Frequency.ConfigureManual("1", 1E7, 1E3);
            Assert.AreEqual("1", Frequency.Channel);
            Assert.AreEqual(1E7, Frequency.Estimate);
            Assert.GreaterOrEqual(1E3, Frequency.Resolution);
        }

    }
}
