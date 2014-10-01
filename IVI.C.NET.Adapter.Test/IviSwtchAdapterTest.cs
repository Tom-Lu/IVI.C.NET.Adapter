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
using Ivi.Swtch;

namespace IVI.C.NET.Adapter.Test
{
    [TestFixture]
    public class IviSwtchAdapterTest
    {
        private IIviSwtch Swtch = null;

        [SetUp]
        public void InitAdapter()
        {
            Swtch = (IIviSwtch)IviDriver.Create("age1442a", true, true, "Simulate=1,RangeCheck=1,QueryInstrStatus=0,Cache=1");
        }

        [Test]
        public void Channels()
        {
            foreach (IIviSwtchChannel Channel in Swtch.Channels)
            {
                Assert.IsNotNullOrEmpty(Channel.Name);
                Assert.IsFalse(Channel.IsSourceChannel);
                Assert.IsFalse(Channel.IsConfigurationChannel);

                IIviSwtchCharacteristics Characteristics = Channel.Characteristics;
                Assert.AreEqual(1, Characteristics.WireMode);
            }
        }

        [Test]
        [Ignore("Unsupported by age1442a driver")]
        public void Scan()
        {
            IIviSwtchScan Scan = Swtch.Scan;
            Assert.IsNotNull(Scan);

            string ScanList = "00C->00NC & 01C->01NC & 02C->02NC";
            Scan.ConfigureList(ScanList, ScanMode.BreakAfterMake);

            Assert.AreEqual(ScanList, Scan.List);
            Assert.AreEqual(ScanMode.BreakAfterMake, Scan.Mode);
        }

        [Test]
        public void Path()
        {
            IIviSwtchPath Path = Swtch.Path;
            Assert.IsNotNull(Path);

            string ch1 = "00C";
            string ch2 = "00NC";
            string ch3 = "00NO";

            Assert.AreEqual(PathCapability.Available, Path.CanConnect(ch1, ch2));
            Assert.AreEqual(PathCapability.Available, Path.CanConnect(ch1, ch3));
            Assert.AreEqual(PathCapability.Unsupported, Path.CanConnect(ch2, ch3));
            Path.Connect(ch1, ch2);
            Assert.AreEqual(PathCapability.Exists, Path.CanConnect(ch1, ch2));
            Assert.AreEqual(PathCapability.ResourceInUse, Path.CanConnect(ch1, ch3));
            Assert.AreEqual(PathCapability.Unsupported, Path.CanConnect(ch2, ch3));
            try
            {
                Path.Disconnect(ch1, ch2);
            }
            catch (Exception e)
            {
                Assert.IsInstanceOf(typeof(InstrumentStatusException), e);
                Assert.AreEqual("Some connections remain after disconnecting.", ((InstrumentStatusException)e).Message);
            }


            Assert.AreEqual(PathCapability.Available, Path.CanConnect(ch1, ch2));
            Assert.AreEqual(PathCapability.Available, Path.CanConnect(ch1, ch3));
            Assert.AreEqual(PathCapability.Unsupported, Path.CanConnect(ch2, ch3));
            Path.Connect(ch1, ch3);
            Assert.AreEqual(PathCapability.ResourceInUse, Path.CanConnect(ch1, ch2));
            Assert.AreEqual(PathCapability.Exists, Path.CanConnect(ch1, ch3));
            Assert.AreEqual(PathCapability.Unsupported, Path.CanConnect(ch2, ch3));
            try
            {
                Path.Disconnect(ch1, ch3);
            }
            catch (Exception e)
            {
                Assert.IsInstanceOf(typeof(InstrumentStatusException), e);
                Assert.AreEqual("Some connections remain after disconnecting.", ((InstrumentStatusException)e).Message);
            }

        }
    }
}
