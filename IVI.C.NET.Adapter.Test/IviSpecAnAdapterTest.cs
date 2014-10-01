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
using Ivi.SpecAn;

namespace IVI.C.NET.Adapter.Test
{
    [TestFixture]
    public class IviSpecAnAdapterTest
    {
        private IIviSpecAn SpecAn = null;

        [SetUp]
        public void InitAdapter()
        {
            SpecAn = (IIviSpecAn)IviDriver.Create("agpsa", true, true, "Simulate=1,RangeCheck=1,QueryInstrStatus=0,Cache=1");
        }

        [Test]
        public void Acquisition()
        {
            IIviSpecAnAcquisition Acquisition = SpecAn.Acquisition;
            Assert.IsNotNull(Acquisition);
            Acquisition.Configure(true, 1, DetectorType.AutoPeak, VerticalScale.Linear);
            Assert.AreEqual(true, Acquisition.SweepModeContinuous);
            Assert.AreEqual(1, Acquisition.NumberOfSweeps);
            Assert.AreEqual(DetectorType.AutoPeak, Acquisition.DetectorType);
            Assert.AreEqual(VerticalScale.Linear, Acquisition.VerticalScale);
        }

        [Test]
        public void Trace()
        {
            SpecAn.Frequency.ConfigureStartStop(500.0e6, 600.0e6);
            SpecAn.SweepCoupling.Configure(true, true, true);
            SpecAn.Acquisition.Configure(false, 1, DetectorType.MaxPeak, VerticalScale.Logarithmic);
            SpecAn.Level.Configure(AmplitudeUnits.dBm, 50, 0, 0, true);
            // SpecAn.Trigger.Source = "Immediate";
            SpecAn.Marker.DisableAll();

            foreach (IIviSpecAnTrace Trace in SpecAn.Traces)
            {
                Trace.Type = TraceType.ClearWrite;
                IWaveform<double> waveform = new Ivi.Driver.Waveform<double>(new PrecisionTimeSpan((decimal)1), 100);
                 waveform = Trace.ReadY(new PrecisionTimeSpan((decimal)10), waveform);

            }
        }

    }
}
