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
using Ivi.Digitizer;

namespace IVI.C.NET.Adapter.Test
{
    [TestFixture]
    public class IviDigitizerAdapterTest
    {
        private IIviDigitizer Digitizer = null;

        [SetUp]
        public void InitAdapter()
        {
            Digitizer = (IIviDigitizer)IviDriver.Create("agl453xdni", true, true, "Simulate=1,RangeCheck=1,QueryInstrStatus=0,Cache=1");
        }

        [Test]
        public void Acquisition()
        {
            IIviDigitizerAcquisition Acquisition = Digitizer.Acquisition;
            Assert.IsNotNull(Acquisition);

            Acquisition.ConfigureAcquisition(10, 100, 1E6);
            Assert.AreEqual(10, Acquisition.NumberOfRecordsToAcquire);
            Assert.AreEqual(100, Acquisition.RecordSize);
            Assert.AreEqual(1E6, Acquisition.SampleRate);

            Acquisition.SampleMode = SampleMode.RealTime;
            Assert.AreEqual(SampleMode.RealTime, Acquisition.SampleMode);

            Acquisition.Initiate();
            Acquisition.WaitForAcquisitionComplete(new PrecisionTimeSpan((decimal)1));
            Acquisition.Abort();
        }

        [Test]
        public void Channels()
        {
            foreach (IIviDigitizerChannel Channel in Digitizer.Channels)
            {
                Channel.Configure(10, 0, VerticalCoupling.DC, true);
                Assert.AreEqual(16, Channel.Range);
                Assert.AreEqual(0, Channel.Offset);
                Assert.AreEqual(VerticalCoupling.DC, Channel.Coupling);
                Assert.IsTrue(Channel.Enabled);

                Channel.Configure(20, 0, VerticalCoupling.DC, true);
                Assert.AreEqual(32, Channel.Range);
                Assert.AreEqual(0, Channel.Offset);
                Assert.AreEqual(VerticalCoupling.DC, Channel.Coupling);
                Assert.IsTrue(Channel.Enabled);

                Digitizer.Acquisition.ConfigureAcquisition(1, 1000, 1E6);

                IWaveform<double> waveformDouble = Digitizer.Acquisition.CreateWaveformDouble(1000);
                waveformDouble = Channel.Measurement.ReadWaveform(new PrecisionTimeSpan((decimal)10), waveformDouble);
                Assert.AreEqual(1000, waveformDouble.ValidPointCount);

                Digitizer.Acquisition.ConfigureAcquisition(10, 1000, 1E6);
                Digitizer.Acquisition.Initiate();
                Digitizer.Acquisition.WaitForAcquisitionComplete(new PrecisionTimeSpan((decimal) 20));
                IWaveformCollection<double> waveforms = Channel.MultiRecordMeasurement.FetchMultiRecordWaveform(0, 10, 0, 1000, (IWaveformCollection<double>)null);

                Assert.AreEqual(10, waveforms.ValidWaveformCount);
                Assert.AreEqual(1000, waveforms[0].ValidPointCount);

            }
        }
    }
}
