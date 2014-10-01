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

namespace IVI.C.NET.Adapter.IviCInterop
{
    public class IviDCPwrAttribute
    {

        /*- Defined values for rangeType parameter of function -*/
        /*- IviDCPwr_ConfigureOutputRange -*/
        public const int IVIDCPWR_VAL_RANGE_CURRENT = (0);
        public const int IVIDCPWR_VAL_RANGE_VOLTAGE = (1);

        public const int IVIDCPWR_VAL_RANGE_TYPE_CLASS_EXT_BASE = (500);
        public const int IVIDCPWR_VAL_RANGE_TYPE_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for outputState parameter of function -*/
        /*- IviDCPwr_QueryOutputState -*/
        public const int IVIDCPWR_VAL_OUTPUT_CONSTANT_VOLTAGE = (0);
        public const int IVIDCPWR_VAL_OUTPUT_CONSTANT_CURRENT = (1);
        public const int IVIDCPWR_VAL_OUTPUT_OVER_VOLTAGE = (2);
        public const int IVIDCPWR_VAL_OUTPUT_OVER_CURRENT = (3);
        public const int IVIDCPWR_VAL_OUTPUT_UNREGULATED = (4);

        public const int IVIDCPWR_VAL_OUTPUT_STATE_CLASS_EXT_BASE = (500);
        public const int IVIDCPWR_VAL_OUTPUT_STATE_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for measurementType parameter of function -*/
        /*- IviDCPwr_Measure -*/
        public const int IVIDCPWR_VAL_MEASURE_CURRENT = (0);
        public const int IVIDCPWR_VAL_MEASURE_VOLTAGE = (1);

        public const int IVIDCPWR_VAL_MEASURE_CLASS_EXT_BASE = (500);
        public const int IVIDCPWR_VAL_MEASURE_SPECIFIC_EXT_BASE = (1000);

        /*- IviDCPwr Fundamental Attributes -*/
        public const int IVIDCPWR_ATTR_CHANNEL_COUNT = IviDriverAttribute.IVI_ATTR_CHANNEL_COUNT;                /* ViInt32,  read-only */
        public const int IVIDCPWR_ATTR_VOLTAGE_LEVEL = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 1);    /* ViReal64,  multi-channel */
        public const int IVIDCPWR_ATTR_OVP_ENABLED = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 2);    /* ViBoolean, multi-channel */
        public const int IVIDCPWR_ATTR_OVP_LIMIT = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 3);    /* ViReal64,  multi-channel */
        public const int IVIDCPWR_ATTR_CURRENT_LIMIT_BEHAVIOR = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 4);     /* ViInt32,   multi-channel */
        public const int IVIDCPWR_ATTR_CURRENT_LIMIT = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 5);    /* ViReal64,  multi-channel */
        public const int IVIDCPWR_ATTR_OUTPUT_ENABLED = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 6);     /* ViBoolean, multi-channel */

        /*- IviDCPwr Extended Attributes -*/
        /*- Trigger Attributes -*/
        public const int IVIDCPWR_ATTR_TRIGGER_SOURCE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 101);    /* ViInt32,   multi-channel */
        public const int IVIDCPWR_ATTR_TRIGGERED_CURRENT_LIMIT = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 102);    /* ViReal64,  multi-channel */
        public const int IVIDCPWR_ATTR_TRIGGERED_VOLTAGE_LEVEL = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 103);   /* ViReal64,  multi-channel */

        /****************************************************************************
         *----------------- IviDCPwr Class Attribute Value Defines -----------------*
         ****************************************************************************/

        /*- Defined values for attribute IVIDCPWR_ATTR_CURRENT_LIMIT_BEHAVIOR -*/
        public const int IVIDCPWR_VAL_CURRENT_REGULATE = (0);
        public const int IVIDCPWR_VAL_CURRENT_TRIP = (1);

        public const int IVIDCPWR_VAL_CURRENT_LIMIT_BEHAVIOR_CLASS_EXT_BASE = (500);
        public const int IVIDCPWR_VAL_CURRENT_LIMIT_BEHAVIOR_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for attribute IVIDCPWR_ATTR_TRIGGER_SOURCE -*/
        public const int IVIDCPWR_VAL_TRIG_IMMEDIATE = (0);
        public const int IVIDCPWR_VAL_TRIG_EXTERNAL = (1);
        public const int IVIDCPWR_VAL_SOFTWARE_TRIG = (2);
        public const int IVIDCPWR_VAL_TRIG_TTL0 = (3);
        public const int IVIDCPWR_VAL_TRIG_TTL1 = (4);
        public const int IVIDCPWR_VAL_TRIG_TTL2 = (5);
        public const int IVIDCPWR_VAL_TRIG_TTL3 = (6);
        public const int IVIDCPWR_VAL_TRIG_TTL4 = (7);
        public const int IVIDCPWR_VAL_TRIG_TTL5 = (8);
        public const int IVIDCPWR_VAL_TRIG_TTL6 = (9);
        public const int IVIDCPWR_VAL_TRIG_TTL7 = (10);
        public const int IVIDCPWR_VAL_TRIG_ECL0 = (11);
        public const int IVIDCPWR_VAL_TRIG_ECL1 = (12);
        public const int IVIDCPWR_VAL_TRIG_PXI_STAR = (13);
        public const int IVIDCPWR_VAL_TRIG_RTSI_0 = (14);
        public const int IVIDCPWR_VAL_TRIG_RTSI_1 = (15);
        public const int IVIDCPWR_VAL_TRIG_RTSI_2 = (16);
        public const int IVIDCPWR_VAL_TRIG_RTSI_3 = (17);
        public const int IVIDCPWR_VAL_TRIG_RTSI_4 = (18);
        public const int IVIDCPWR_VAL_TRIG_RTSI_5 = (19);
        public const int IVIDCPWR_VAL_TRIG_RTSI_6 = (20);

        public const int IVIDCPWR_VAL_TRIG_SRC_CLASS_EXT_BASE = (500);
        public const int IVIDCPWR_VAL_TRIG_SRC_SPECIFIC_EXT_BASE = (1000);

    }
}
