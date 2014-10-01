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
using Ivi.Swtch;
using Ivi.ConfigServer.Interop;
using IVI.C.NET.Adapter.IviCInterop;

namespace IVI.C.NET.Adapter
{
    public class IviSwtchAdapter : DriverAdapterBase<IviCInterop.IviSwtch>, IIviSwtch
    {
        #region Enum Mapping

        private static IviEnumCMapping<PathCapability, int> SwtchPathCapability = IviEnumCMapping<PathCapability, int>.Instance
            .Map(PathCapability.Available, IviSwtchAttribute.IVISWTCH_VAL_PATH_AVAILABLE)
            .Map(PathCapability.Exists, IviSwtchAttribute.IVISWTCH_VAL_PATH_EXISTS)
            .Map(PathCapability.Unsupported, IviSwtchAttribute.IVISWTCH_VAL_PATH_UNSUPPORTED)
            .Map(PathCapability.ResourceInUse, IviSwtchAttribute.IVISWTCH_VAL_RSRC_IN_USE)
            .Map(PathCapability.SourceConflict, IviSwtchAttribute.IVISWTCH_VAL_SOURCE_CONFLICT)
            .Map(PathCapability.ChannelNotAvailable, IviSwtchAttribute.IVISWTCH_VAL_CHANNEL_NOT_AVAILABLE);

        private static IviEnumCMapping<ScanMode, int> SwtchScanMode = IviEnumCMapping<ScanMode, int>.Instance
            .Map(ScanMode.None, IviSwtchAttribute.IVISWTCH_VAL_NONE)
            .Map(ScanMode.BreakBeforeMake, IviSwtchAttribute.IVISWTCH_VAL_BREAK_BEFORE_MAKE)
            .Map(ScanMode.BreakAfterMake, IviSwtchAttribute.IVISWTCH_VAL_BREAK_AFTER_MAKE);

        private static IviEnumCMapping<string, int> SwtchTriggerInput = IviEnumCMapping<string, int>.Instance
            .Map("None", IviSwtchAttribute.IVISWTCH_VAL_NONE)
            .Map("GPIB SRQ", IviSwtchAttribute.IVISWTCH_VAL_GPIB_SRQ)
            .Map("Immediate", IviSwtchAttribute.IVISWTCH_VAL_IMMEDIATE)
            .Map("Software", IviSwtchAttribute.IVISWTCH_VAL_SOFTWARE_TRIG)
            .Map("External", IviSwtchAttribute.IVISWTCH_VAL_EXTERNAL)
            .Map("TTL0", IviSwtchAttribute.IVISWTCH_VAL_TTL0)
            .Map("TTL1", IviSwtchAttribute.IVISWTCH_VAL_TTL1)
            .Map("TTL2", IviSwtchAttribute.IVISWTCH_VAL_TTL2)
            .Map("TTL3", IviSwtchAttribute.IVISWTCH_VAL_TTL3)
            .Map("TTL4", IviSwtchAttribute.IVISWTCH_VAL_TTL4)
            .Map("TTL5", IviSwtchAttribute.IVISWTCH_VAL_TTL5)
            .Map("TTL6", IviSwtchAttribute.IVISWTCH_VAL_TTL6)
            .Map("TTL7", IviSwtchAttribute.IVISWTCH_VAL_TTL7)
            .Map("ECL0", IviSwtchAttribute.IVISWTCH_VAL_ECL0)
            .Map("ECL1", IviSwtchAttribute.IVISWTCH_VAL_ECL1)
            .Map("PXI_STAR", IviSwtchAttribute.IVISWTCH_VAL_PXI_STAR)
            .Map("RTSI0", IviSwtchAttribute.IVISWTCH_VAL_RTSI_0)
            .Map("RTSI1", IviSwtchAttribute.IVISWTCH_VAL_RTSI_1)
            .Map("RTSI2", IviSwtchAttribute.IVISWTCH_VAL_RTSI_2)
            .Map("RTSI3", IviSwtchAttribute.IVISWTCH_VAL_RTSI_3)
            .Map("RTSI4", IviSwtchAttribute.IVISWTCH_VAL_RTSI_4)
            .Map("RTSI5", IviSwtchAttribute.IVISWTCH_VAL_RTSI_5)
            .Map("RTSI6", IviSwtchAttribute.IVISWTCH_VAL_RTSI_6);

        #endregion

        private IIviSwtchChannelCollection SwtchChannelCollection = null;
        private IIviSwtchPath SwtchPath = null;
        private IIviSwtchScan SwtchScan = null;
        public IviSwtchAdapter(string name, bool idQuery, bool reset, string options)
            : base(name, idQuery, reset, options)
        {
            SwtchChannelCollection = new IviSwtchChannelCollection(this);
            SwtchPath = new IviSwtchPath(this);
            SwtchScan = new IviSwtchScan(this);
        }

        public IIviSwtchChannelCollection Channels
        {
            get { return SwtchChannelCollection; }
        }

        public IIviSwtchPath Path
        {
            get { return SwtchPath; }
        }

        public IIviSwtchScan Scan
        {
            get { return SwtchScan; }
        }

        internal class IviSwtchChannelCollection : IIviSwtchChannelCollection
        {
            private IDriverAdapterBase Adapter;
            private IList<IIviSwtchChannel> Channels = null;
            private IList<string> ChannelNames = null;
            public IviSwtchChannelCollection(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                int ChannelCount = Adapter.GetAttributeViInt32(IviSwtchAttribute.IVISWTCH_ATTR_CHANNEL_COUNT);

                Channels = new List<IIviSwtchChannel>();
                ChannelNames = new List<string>();
                for (int Index = 1; Index <= ChannelCount; Index++)
                {
                    IIviSwtchChannel Channel = new IviSwtchChannel(Adapter, Index);
                    Channels.Add(Channel);
                    ChannelNames.Add(Channel.Name);
                }
            }

            public int Count
            {
                get { return Channels.Count; }
            }

            public IIviSwtchChannel this[string name]
            {
                get { return Channels[ChannelNames.IndexOf(name)]; }
            }

            public IEnumerator<IIviSwtchChannel> GetEnumerator()
            {
                return Channels.GetEnumerator();
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return Channels.GetEnumerator();
            }
        }

        internal class IviSwtchCharacteristics : IIviSwtchCharacteristics
        {
            private IDriverAdapterBase Adapter = null;
            private string ChannelName = null;
            public IviSwtchCharacteristics(IDriverAdapterBase Adapter, string ChannelName)
            {
                this.Adapter = Adapter;
                this.ChannelName = ChannelName;
            }

            public double ACCurrentCarryMax
            {
                get { return Adapter.GetAttributeViReal64(ChannelName, IviSwtchAttribute.IVISWTCH_ATTR_MAX_CARRY_AC_CURRENT); }
            }

            public double ACCurrentSwitchingMax
            {
                get { return Adapter.GetAttributeViReal64(ChannelName, IviSwtchAttribute.IVISWTCH_ATTR_MAX_SWITCHING_AC_CURRENT); }
            }

            public double ACPowerCarryMax
            {
                get { return Adapter.GetAttributeViReal64(ChannelName, IviSwtchAttribute.IVISWTCH_ATTR_MAX_CARRY_AC_POWER); }
            }

            public double ACPowerSwitchingMax
            {
                get { return Adapter.GetAttributeViReal64(ChannelName, IviSwtchAttribute.IVISWTCH_ATTR_MAX_SWITCHING_AC_POWER); }
            }

            public double ACVoltageMax
            {
                get { return Adapter.GetAttributeViReal64(ChannelName, IviSwtchAttribute.IVISWTCH_ATTR_MAX_AC_VOLTAGE); }
            }

            public double Bandwidth
            {
                get { return Adapter.GetAttributeViReal64(ChannelName, IviSwtchAttribute.IVISWTCH_ATTR_BANDWIDTH); }
            }

            public double DCCurrentCarryMax
            {
                get { return Adapter.GetAttributeViReal64(ChannelName, IviSwtchAttribute.IVISWTCH_ATTR_MAX_CARRY_DC_CURRENT); }
            }

            public double DCCurrentSwitchingMax
            {
                get { return Adapter.GetAttributeViReal64(ChannelName, IviSwtchAttribute.IVISWTCH_ATTR_MAX_SWITCHING_DC_CURRENT); }
            }

            public double DCPowerCarryMax
            {
                get { return Adapter.GetAttributeViReal64(ChannelName, IviSwtchAttribute.IVISWTCH_ATTR_MAX_CARRY_DC_POWER); }
            }

            public double DCPowerSwitchingMax
            {
                get { return Adapter.GetAttributeViReal64(ChannelName, IviSwtchAttribute.IVISWTCH_ATTR_MAX_SWITCHING_DC_POWER); }
            }

            public double DCVoltageMax
            {
                get { return Adapter.GetAttributeViReal64(ChannelName, IviSwtchAttribute.IVISWTCH_ATTR_MAX_DC_VOLTAGE); }
            }

            public double Impedance
            {
                get { return Adapter.GetAttributeViReal64(ChannelName, IviSwtchAttribute.IVISWTCH_VAL_IMMEDIATE); }
            }

            public PrecisionTimeSpan SettlingTime
            {
                get { return new PrecisionTimeSpan(new TimeSpan((long)(TimeSpan.TicksPerSecond * Adapter.GetAttributeViReal64(ChannelName, IviSwtchAttribute.IVISWTCH_ATTR_SETTLING_TIME)))); }
            }

            public int WireMode
            {
                get { return Adapter.GetAttributeViInt32(ChannelName, IviSwtchAttribute.IVISWTCH_ATTR_WIRE_MODE); }
            }
        }

        internal class IviSwtchChannel : IIviSwtchChannel
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviSwtch IviSwtchInterop = null;
            private int Index;
            private string ChannelName;
            private IIviSwtchCharacteristics SwtchCharacteristics = null;
            public IviSwtchChannel(IDriverAdapterBase Adapter, int Index)
            {
                this.Adapter = Adapter;
                IviSwtchInterop = (IviCInterop.IviSwtch)Adapter.Interop;
                this.Index = Index;

                StringBuilder NameValue = new StringBuilder(256);
                Adapter.ViSessionStatusCheck(IviSwtchInterop.GetChannelName(Adapter.Session, Index, NameValue.Capacity, NameValue));
                ChannelName = NameValue.ToString();

                SwtchCharacteristics = new IviSwtchCharacteristics(Adapter, ChannelName);
            }

            public IIviSwtchCharacteristics Characteristics
            {
                get { return SwtchCharacteristics; }
            }

            public bool IsConfigurationChannel
            {
                get
                {
                    return Adapter.GetAttributeViBoolean(ChannelName, IviSwtchAttribute.IVISWTCH_ATTR_IS_CONFIGURATION_CHANNEL);
                }
                set
                {
                    Adapter.SetAttributeViBoolean(ChannelName, IviSwtchAttribute.IVISWTCH_ATTR_IS_CONFIGURATION_CHANNEL, value);
                }
            }

            public bool IsSourceChannel
            {
                get
                {
                    return Adapter.GetAttributeViBoolean(ChannelName, IviSwtchAttribute.IVISWTCH_ATTR_IS_SOURCE_CHANNEL);
                }
                set
                {
                    Adapter.SetAttributeViBoolean(ChannelName, IviSwtchAttribute.IVISWTCH_ATTR_IS_SOURCE_CHANNEL, value);
                }
            }

            public string Name
            {
                get { return ChannelName; }
            }
        }

        internal class IviSwtchPath : IIviSwtchPath
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviSwtch IviSwtchInterop = null;
            public IviSwtchPath(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviSwtchInterop = (IviCInterop.IviSwtch)Adapter.Interop;
            }

            public PathCapability CanConnect(string channel1, string channel2)
            {
                int pathCapability = 0;
                Adapter.ViSessionStatusCheck(IviSwtchInterop.CanConnect(Adapter.Session, channel1, channel2, ref pathCapability));
                return SwtchPathCapability.getEnum(pathCapability);
            }

            public void Connect(string channel1, string channel2)
            {
                Adapter.ViSessionStatusCheck(IviSwtchInterop.Connect(Adapter.Session, channel1, channel2));
            }

            public void Disconnect(string channel1, string channel2)
            {
                Adapter.ViSessionStatusCheck(IviSwtchInterop.Disconnect(Adapter.Session, channel1, channel2));
            }

            public void DisconnectAll()
            {
                Adapter.ViSessionStatusCheck(IviSwtchInterop.DisconnectAll(Adapter.Session));
            }

            public string[] GetPath(string channel1, string channel2)
            {
                StringBuilder PathList = new StringBuilder(1024);
                Adapter.ViSessionStatusCheck(IviSwtchInterop.GetPath(Adapter.Session, channel1, channel2, PathList.Capacity, PathList));
                return PathList.ToString().Split(',');
            }

            public bool IsDebounced
            {
                get { return Adapter.GetAttributeViBoolean(IviSwtchAttribute.IVISWTCH_ATTR_IS_DEBOUNCED); }
            }

            public void SetPath(string[] pathList)
            {
                Adapter.ViSessionStatusCheck(IviSwtchInterop.SetPath(Adapter.Session, String.Join(",", pathList)));
            }

            public void WaitForDebounce(PrecisionTimeSpan maximumTime)
            {
                Adapter.ViSessionStatusCheck(IviSwtchInterop.WaitForDebounce(Adapter.Session, (int)maximumTime.TotalMilliseconds));
            }
        }

        internal class IviSwtchScan : IIviSwtchScan
        {
            private IDriverAdapterBase Adapter = null;
            private IviCInterop.IviSwtch IviSwtchInterop = null;
            public IviSwtchScan(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                IviSwtchInterop = (IviCInterop.IviSwtch)Adapter.Interop;
            }

            public void Abort()
            {
                Adapter.ViSessionStatusCheck(IviSwtchInterop.AbortScan(Adapter.Session));
            }

            public void ConfigureList(string list, ScanMode mode)
            {
                Adapter.ViSessionStatusCheck(IviSwtchInterop.ConfigureScanList(Adapter.Session, list, SwtchScanMode.getC_Value(mode)));
            }

            public void ConfigureTrigger(PrecisionTimeSpan scanDelay, string triggerInput, string scannerAdvancedOutput)
            {
                Adapter.ViSessionStatusCheck(IviSwtchInterop.ConfigureScanTrigger(Adapter.Session, scanDelay.TotalSeconds, SwtchTriggerInput.getC_Value(triggerInput), SwtchTriggerInput.getC_Value(scannerAdvancedOutput)));
            }

            public bool Continuous
            {
                get
                {
                    return Adapter.GetAttributeViBoolean(IviSwtchAttribute.IVISWTCH_ATTR_CONTINUOUS_SCAN);
                }
                set
                {
                    Adapter.SetAttributeViBoolean(IviSwtchAttribute.IVISWTCH_ATTR_CONTINUOUS_SCAN, value);
                }
            }

            public PrecisionTimeSpan Delay
            {
                get
                {
                    return new PrecisionTimeSpan((decimal)(Adapter.GetAttributeViReal64(IviSwtchAttribute.IVISWTCH_ATTR_SCAN_DELAY)/1000.0));
                }
                set
                {
                    Adapter.SetAttributeViReal64(IviSwtchAttribute.IVISWTCH_ATTR_SCAN_DELAY, value.TotalMilliseconds);
                }
            }

            public void Initiate()
            {
                Adapter.ViSessionStatusCheck(IviSwtchInterop.InitiateScan(Adapter.Session));
            }

            public string Input
            {
                get
                {
                    return SwtchTriggerInput.getEnum(Adapter.GetAttributeViInt32(IviSwtchAttribute.IVISWTCH_ATTR_TRIGGER_INPUT));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviSwtchAttribute.IVISWTCH_ATTR_TRIGGER_INPUT, (SwtchTriggerInput.getC_Value(value)));
                }
            }

            public bool IsScanning
            {
                get { return Adapter.GetAttributeViBoolean(IviSwtchAttribute.IVISWTCH_ATTR_IS_SCANNING); }
            }

            public string List
            {
                get
                {
                    return Adapter.GetAttributeViString(IviSwtchAttribute.IVISWTCH_ATTR_SCAN_LIST);
                }
                set
                {
                    Adapter.SetAttributeViString(IviSwtchAttribute.IVISWTCH_ATTR_SCAN_LIST, value);
                }
            }

            public ScanMode Mode
            {
                get
                {
                    return SwtchScanMode.getEnum(Adapter.GetAttributeViInt32(IviSwtchAttribute.IVISWTCH_ATTR_SCAN_MODE));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviSwtchAttribute.IVISWTCH_ATTR_SCAN_MODE, SwtchScanMode.getC_Value(value));
                }
            }

            public int NumberOfColumns
            {
                get { return Adapter.GetAttributeViInt32(IviSwtchAttribute.IVISWTCH_ATTR_NUM_OF_COLUMNS); }
            }

            public int NumberOfRows
            {
                get { return Adapter.GetAttributeViInt32(IviSwtchAttribute.IVISWTCH_ATTR_NUM_OF_ROWS); }
            }

            public string ScannerAdvancedOutput
            {
                get
                {
                    return SwtchTriggerInput.getEnum(Adapter.GetAttributeViInt32(IviSwtchAttribute.IVISWTCH_ATTR_SCAN_ADVANCED_OUTPUT));
                }
                set
                {
                    Adapter.SetAttributeViInt32(IviSwtchAttribute.IVISWTCH_ATTR_SCAN_ADVANCED_OUTPUT, SwtchTriggerInput.getC_Value(value));
                }
            }

            public void SendSoftwareTrigger()
            {
                Adapter.ViSessionStatusCheck(IviSwtchInterop.SendSoftwareTrigger(Adapter.Session));
            }

            public void WaitForScanComplete(PrecisionTimeSpan maximumTime)
            {
                Adapter.ViSessionStatusCheck(IviSwtchInterop.WaitForScanComplete(Adapter.Session, (int)maximumTime.TotalMilliseconds));
            }
        }
    }
}
