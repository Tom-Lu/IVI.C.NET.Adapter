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
using System.Collections;

namespace IVI.C.NET.Adapter
{
    internal class IviEnumCMapping<EnumValue, C_Value>
    {
        IDictionary<EnumValue, C_Value> forward;
        IDictionary<C_Value, EnumValue> backward;

        private IviEnumCMapping()
        {
            forward = new Dictionary<EnumValue, C_Value>();
            backward = new Dictionary<C_Value, EnumValue>();
        }

        public C_Value getC_Value(EnumValue Key)
        {
            return forward[Key];
        }

        public EnumValue getEnum(C_Value Value)
        {
            return backward[Value];
        }

        public IviEnumCMapping<EnumValue, C_Value> Map(EnumValue enumValue, C_Value cValue)
        {
            forward.Add(enumValue, cValue);
            backward.Add(cValue, enumValue);
            return this;
        }

        public static IviEnumCMapping<EnumValue, C_Value> Instance
        {
            get
            {
                return new IviEnumCMapping<EnumValue, C_Value>();
            }
        }
    }
}
