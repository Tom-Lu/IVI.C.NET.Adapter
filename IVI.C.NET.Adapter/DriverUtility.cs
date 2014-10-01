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
using IVI.C.NET.Adapter.IviCInterop;

namespace IVI.C.NET.Adapter
{
    internal class DriverUtility : IIviDriverUtility
    {
        private IDriverAdapterBase Adapter;
        public DriverUtility(IDriverAdapterBase Adapter)
        {
            this.Adapter = Adapter;
        }

        public void Disable()
        {
            Adapter.Interop.Disable(Adapter.Session);
        }

        public ErrorQueryResult ErrorQuery()
        {
            int error_code = 0;
            StringBuilder error_message = new StringBuilder(256);
            Adapter.ViSessionStatusCheck(Adapter.Interop.error_query(Adapter.Session, ref error_code, error_message));
            return new ErrorQueryResult(error_code, error_message.ToString());
        }

        public IIviDriverLock Lock(PrecisionTimeSpan maxTime)
        {
            throw new NotImplementedException();
        }

        public IIviDriverLock Lock()
        {
            return new IviDriverLock(Adapter);
        }

        public void Reset()
        {
            Adapter.ViSessionStatusCheck(Adapter.Interop.reset(Adapter.Session));
        }

        public void ResetWithDefaults()
        {
            Adapter.ViSessionStatusCheck(Adapter.Interop.ResetWithDefaults(Adapter.Session));
        }

        public SelfTestResult SelfTest()
        {
            short selfTestResult = 0;
            StringBuilder selfTestMessage = new StringBuilder(1024);
            Adapter.ViSessionStatusCheck(Adapter.Interop.self_test(Adapter.Session, ref selfTestResult, selfTestMessage));
            return new SelfTestResult(selfTestResult, selfTestMessage.ToString());
        }

        public class IviDriverLock : IIviDriverLock
        {
            private bool HasLock = false;
            private IDriverAdapterBase Adapter;
            public IviDriverLock(IDriverAdapterBase Adapter)
            {
                this.Adapter = Adapter;
                Adapter.ViSessionStatusCheck(Adapter.Interop.LockSession(Adapter.Session, ref HasLock));
            }

            public void Unlock()
            {
                Adapter.ViSessionStatusCheck(Adapter.Interop.UnlockSession(Adapter.Session, ref HasLock));
            }

            public void Dispose()
            {
            }
        }
    }
}