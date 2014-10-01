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
using NUnit.Framework;
using Ivi.Driver;

namespace IVI.C.NET.Adapter.Test
{
    [TestFixture]
    public class IviDriverAdapterTest
    {
        private IIviDriver Driver = null;
        [SetUp]
        public void InitAdapter()
        {
            Driver = IviDriver.Create("Ag34401", true, true, "Simulate=1,RangeCheck=1,QueryInstrStatus=0,Cache=1");
        }

        [Test]
        public void DriverIdentity()
        {
            IIviDriverIdentity Identity = Driver.Identity;
            Assert.IsNotNull(Identity);
            Assert.AreEqual("IVI driver for Agilent Technologies 34401A Digital Multi Meter", Identity.Description);
            Assert.AreEqual("Ag34401", Identity.Identifier);
            Assert.AreEqual("1.2.5.0", Identity.Revision);
            Assert.AreEqual("Agilent Technologies", Identity.InstrumentManufacturer);
            Assert.AreEqual("34401A", Identity.InstrumentModel);
            Assert.AreEqual("Sim1.2.5.0", Identity.InstrumentFirmwareRevision);
            Assert.AreEqual(3, Identity.SpecificationMajorVersion);

            // Error with 34401 Driver
            // Assert.AreEqual(0, Identity.SpecificationMinorVersion);

            string SupportedInstrumentModels = "34401A";
            Assert.AreEqual(SupportedInstrumentModels.Split(','), Identity.GetSupportedInstrumentModels());
            string GetGroupCapabilities = "IviDmmBase,IviDmmACMeasurement,IviDmmFrequencyMeasurement,IviDmmMultiPoint,IviDmmTriggerSlope,IviDmmSoftwareTrigger,IviDmmDeviceInfo,IviDmmAutoRangeValue,IviDmmAutoZero";
            Assert.AreEqual(GetGroupCapabilities.Split(','), Identity.GetGroupCapabilities());
        }

        [Test]
        public void DriverOperation()
        {
            IIviDriverOperation DriverOperation = Driver.DriverOperation;
            Assert.IsNotNull(DriverOperation);
            Assert.AreEqual("Ag34401", DriverOperation.LogicalName);
            Assert.AreEqual("COM1", DriverOperation.IOResourceDescriptor);
            Assert.AreEqual("Ag34401", DriverOperation.DriverSetup);
            Assert.AreEqual(false, DriverOperation.Cache);
            Assert.AreEqual(false, DriverOperation.RangeCheck);
            Assert.AreEqual(false, DriverOperation.QueryInstrumentStatus);
            Assert.AreEqual(true, DriverOperation.Simulate);

            // Unsupported by 34401 driver
            // DriverOperation.ResetInterchangeCheck();
            DriverOperation.InvalidateAllAttributes();
        }

        [Test]
        public void DriverUtility()
        {
            IIviDriverUtility DriverUtility = Driver.Utility;
            DriverUtility.Reset();
            DriverUtility.ResetWithDefaults();
            ErrorQueryResult ErrorResult = DriverUtility.ErrorQuery();
            Assert.AreEqual(0, ErrorResult.Code);
            Assert.AreEqual("No error.", ErrorResult.Message);

            SelfTestResult SelfTestResult = DriverUtility.SelfTest();
            Assert.AreEqual(0, SelfTestResult.Code);
            Assert.AreEqual("Selftest passed", SelfTestResult.Message);

            IIviDriverLock DriverLock = DriverUtility.Lock();
            DriverLock.Unlock();
            DriverUtility.Disable();

        }

    }
}
