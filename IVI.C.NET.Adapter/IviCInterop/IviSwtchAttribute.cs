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
    public class IviSwtchAttribute
    {


        /*- IviSwtch Fundamental Attributes -*/
        public const int IVISWTCH_ATTR_CHANNEL_COUNT = IviDriverAttribute.IVI_ATTR_CHANNEL_COUNT;              /* ViInt32,  read-only */
        public const int IVISWTCH_ATTR_IS_SOURCE_CHANNEL = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 1);   /* ViBoolean, Channel-based */
        public const int IVISWTCH_ATTR_IS_DEBOUNCED = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 2);   /* ViBoolean, Read-only */
        public const int IVISWTCH_ATTR_IS_CONFIGURATION_CHANNEL = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 3);   /* ViBoolean, Channel-based */
        public const int IVISWTCH_ATTR_SETTLING_TIME = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 4);   /* ViReal64, Read-only, Channel-based */
        public const int IVISWTCH_ATTR_BANDWIDTH = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 5);   /* ViReal64, Read-only, Channel-based */
        public const int IVISWTCH_ATTR_MAX_DC_VOLTAGE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 6);   /* ViReal64, Read-only, Channel-based */
        public const int IVISWTCH_ATTR_MAX_AC_VOLTAGE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 7);   /* ViReal64, Read-only, Channel-based */
        public const int IVISWTCH_ATTR_MAX_SWITCHING_DC_CURRENT = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 8);   /* ViReal64, Read-only, Channel-based */
        public const int IVISWTCH_ATTR_MAX_SWITCHING_AC_CURRENT = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 9);   /* ViReal64, Read-only, Channel-based */
        public const int IVISWTCH_ATTR_MAX_CARRY_DC_CURRENT = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 10);  /* ViReal64, Read-only, Channel-based */
        public const int IVISWTCH_ATTR_MAX_CARRY_AC_CURRENT = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 11);  /* ViReal64, Read-only, Channel-based */
        public const int IVISWTCH_ATTR_MAX_SWITCHING_DC_POWER = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 12);  /* ViReal64, Read-only, Channel-based */
        public const int IVISWTCH_ATTR_MAX_SWITCHING_AC_POWER = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 13);  /* ViReal64, Read-only, Channel-based */
        public const int IVISWTCH_ATTR_MAX_CARRY_DC_POWER = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 14);  /* ViReal64, Read-only, Channel-based */
        public const int IVISWTCH_ATTR_MAX_CARRY_AC_POWER = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 15);  /* ViReal64, Read-only, Channel-based */
        public const int IVISWTCH_ATTR_CHARACTERISTIC_IMPEDANCE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 16);  /* ViReal64, Read-only, Channel-based */
        public const int IVISWTCH_ATTR_WIRE_MODE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 17);  /* ViInt32,  Read-only, Channel-based */

        /*- IviSwtch Extended Attributes -*/
        /*- IviSwitchScanner Extension Group -*/
        public const int IVISWTCH_ATTR_NUM_OF_ROWS = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 18);  /* ViInt32, Read-only */
        public const int IVISWTCH_ATTR_NUM_OF_COLUMNS = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 19);  /* ViInt32, Read-only */
        public const int IVISWTCH_ATTR_SCAN_LIST = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 20);  /* ViString */
        public const int IVISWTCH_ATTR_SCAN_MODE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 21);  /* ViInt32 */
        public const int IVISWTCH_ATTR_TRIGGER_INPUT = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 22);  /* ViInt32 */
        public const int IVISWTCH_ATTR_SCAN_ADVANCED_OUTPUT = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 23);  /* ViInt32 */
        public const int IVISWTCH_ATTR_IS_SCANNING = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 24);  /* ViBoolean, Read-only */
        public const int IVISWTCH_ATTR_SCAN_DELAY = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 25);  /* ViReal64 */
        public const int IVISWTCH_ATTR_CONTINUOUS_SCAN = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 26);  /* ViBoolean */

        /*****************************************************************************
         *---------------- IviSwtch Class Attribute Value Defines -------------------*
         *****************************************************************************/

        /*- Defined values for attribute IVISWTCH_ATTR_SCAN_MODE -*/
        public const int IVISWTCH_VAL_NONE = (0);
        public const int IVISWTCH_VAL_BREAK_BEFORE_MAKE = (1);
        public const int IVISWTCH_VAL_BREAK_AFTER_MAKE = (2);
        public const int IVISWTCH_VAL_SCAN_MODE_CLASS_EXT_BASE = (500);
        public const int IVISWTCH_VAL_SCAN_MODE_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for attribute IVISWTCH_ATTR_TRIGGER_INPUT -*/
        public const int IVISWTCH_VAL_IMMEDIATE = (1);
        public const int IVISWTCH_VAL_EXTERNAL = (2);
        public const int IVISWTCH_VAL_SOFTWARE_TRIG = (3);
        public const int IVISWTCH_VAL_TTL0 = (111);
        public const int IVISWTCH_VAL_TTL1 = (112);
        public const int IVISWTCH_VAL_TTL2 = (113);
        public const int IVISWTCH_VAL_TTL3 = (114);
        public const int IVISWTCH_VAL_TTL4 = (115);
        public const int IVISWTCH_VAL_TTL5 = (116);
        public const int IVISWTCH_VAL_TTL6 = (117);
        public const int IVISWTCH_VAL_TTL7 = (118);
        public const int IVISWTCH_VAL_ECL0 = (119);
        public const int IVISWTCH_VAL_ECL1 = (120);
        public const int IVISWTCH_VAL_PXI_STAR = (125);
        public const int IVISWTCH_VAL_RTSI_0 = (140);
        public const int IVISWTCH_VAL_RTSI_1 = (141);
        public const int IVISWTCH_VAL_RTSI_2 = (142);
        public const int IVISWTCH_VAL_RTSI_3 = (143);
        public const int IVISWTCH_VAL_RTSI_4 = (144);
        public const int IVISWTCH_VAL_RTSI_5 = (145);
        public const int IVISWTCH_VAL_RTSI_6 = (146);
        public const int IVISWTCH_VAL_TRIGGER_INPUT_CLASS_EXT_BASE = (500);
        public const int IVISWTCH_VAL_TRIGGER_INPUT_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for attribute IVISWTCH_ATTR_SCAN_ADVANCED_OUTPUT -*/
        public const int IVISWTCH_VAL_GPIB_SRQ = (5);

        public const int IVISWTCH_VAL_SCAN_ADVANCED_OUTPUT_CLASS_EXT_BASE = (500);
        public const int IVISWTCH_VAL_SCAN_ADVANCED_OUTPUT_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for IviSwtch_CanConnect path capability parameter -*/
        public const int IVISWTCH_VAL_PATH_AVAILABLE = (1);
        public const int IVISWTCH_VAL_PATH_EXISTS = (2);
        public const int IVISWTCH_VAL_PATH_UNSUPPORTED = (3);
        public const int IVISWTCH_VAL_RSRC_IN_USE = (4);
        public const int IVISWTCH_VAL_SOURCE_CONFLICT = (5);
        public const int IVISWTCH_VAL_CHANNEL_NOT_AVAILABLE = (6);
        public const int IVISWTCH_VAL_CAN_CONNECT_CLASS_EXT_BASE = (500);
        public const int IVISWTCH_VAL_CAN_CONNECT_SPECIFIC_EXT_BASE = (1000);

    }
}
