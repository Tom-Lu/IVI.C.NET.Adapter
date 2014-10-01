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

// Ivi type mapping
using ViStatus = System.Int32;
using ViSession = System.IntPtr;
using ViString = System.String;
using ViBoolean = System.Boolean;
using ViInt8 = System.Byte;
using ViInt16 = System.Int16;
using ViInt32 = System.Int32;
using ViAttr = System.UInt32;
using ViInt64 = System.Int64;
using ViReal64 = System.Double;

namespace IVI.C.NET.Adapter.IviCInterop
{
    public interface IviSwtch : IviDriver
    {
        /*- IviSwtchBase Capability Group Functions -*/
        ViStatus CanConnect(ViSession vi, ViString channel1, ViString channel2, ref ViInt32 pathCapability);

        ViStatus Connect(ViSession vi, ViString channel1, ViString channel2);

        ViStatus Disconnect(ViSession vi, ViString channel1, ViString channel2);

        ViStatus DisconnectAll(ViSession vi);

        ViStatus GetChannelName(ViSession vi, ViInt32 index, ViInt32 bufferSize, StringBuilder name);

        ViStatus GetPath(ViSession vi, ViString channel1, ViString channel2, ViInt32 bufferSize, StringBuilder pathList);

        ViStatus IsDebounced(ViSession vi, ref ViBoolean isDebounced);

        ViStatus SetPath(ViSession vi, ViString pathList);

        ViStatus WaitForDebounce(ViSession vi, ViInt32 maxTime);

        /*- IviSwtchScanner Extension Group Functions -*/
        ViStatus AbortScan(ViSession vi);

        ViStatus ConfigureScanList(ViSession vi, ViString scanList, ViInt32 scanMode);

        ViStatus ConfigureScanTrigger(ViSession vi, ViReal64 scanDelay, ViInt32 triggerInput, ViInt32 scanAdvancedOutput);

        ViStatus InitiateScan(ViSession vi);

        ViStatus IsScanning(ViSession vi, ref ViBoolean isScanning);

        ViStatus SetContinuousScan(ViSession vi, ViBoolean status);

        ViStatus WaitForScanComplete(ViSession vi, ViInt32 maxTime);

        /*- IviSwtchSoftwareTrigger Extension Group Functions -*/
        ViStatus SendSoftwareTrigger(ViSession vi);
    }
}
