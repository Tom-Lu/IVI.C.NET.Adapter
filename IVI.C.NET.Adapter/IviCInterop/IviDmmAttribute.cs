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
    public class IviDmmAttribute
    {

        /****************************************************************************
         *------------------------------ Useful Macros -----------------------------*
         ****************************************************************************/
        /*- Defined values for maxTime parameter to the Measure, Read -*/
        /*- and Fetch functions -*/
        //    public const int IVIDMM_VAL_MAX_TIME_INFINITE = IviDriverAttribute.IVI_VAL_MAX_TIME_INFINITE;
        //    public const int IVIDMM_VAL_MAX_TIME_IMMEDIATE = IviDriverAttribute.IVI_VAL_MAX_TIME_IMMEDIATE;

        /*- IviDmm Fundamental Attributes -*/
        public const int IVIDMM_ATTR_FUNCTION = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 1);    /* ViInt32  */
        public const int IVIDMM_ATTR_RANGE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 2);    /* ViReal64 */
        public const int IVIDMM_ATTR_RESOLUTION_ABSOLUTE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 8);    /* ViReal64 */
        public const int IVIDMM_ATTR_TRIGGER_SOURCE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 4);    /* ViInt32  */
        public const int IVIDMM_ATTR_TRIGGER_DELAY = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 5);    /* ViReal64 */

        /*- IviDmmAcMeasurement Attributes -*/
        public const int IVIDMM_ATTR_AC_MIN_FREQ = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 6);    /* ViReal64 */
        public const int IVIDMM_ATTR_AC_MAX_FREQ = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 7);    /* ViReal64 */

        /*- IviDmmFrequencyMeasurement Attributes -*/
        public const int IVIDMM_ATTR_FREQ_VOLTAGE_RANGE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 101);  /* ViReal64 */

        /*- IviDmmTemperatureMeasurement Attributes -*/
        public const int IVIDMM_ATTR_TEMP_TRANSDUCER_TYPE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 201);  /* ViInt32  */

        /*- IviDmmThermocouple Attributes -*/
        public const int IVIDMM_ATTR_TEMP_TC_TYPE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 231);  /* ViInt32  */
        public const int IVIDMM_ATTR_TEMP_TC_REF_JUNC_TYPE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 232);  /* ViInt32  */
        public const int IVIDMM_ATTR_TEMP_TC_FIXED_REF_JUNC = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 233);  /* ViReal64 */

        /*- IviDmmResistanceTemperatureDevice Attributes -*/
        public const int IVIDMM_ATTR_TEMP_RTD_ALPHA = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 241);  /* ViReal64 */
        public const int IVIDMM_ATTR_TEMP_RTD_RES = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 242);  /* ViReal64 */

        /*- IviDmmThermistor Attributes -*/
        public const int IVIDMM_ATTR_TEMP_THERMISTOR_RES = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 251);  /* ViReal64 */

        /*- IviDmmMultiPoint Attributes -*/
        public const int IVIDMM_ATTR_SAMPLE_COUNT = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 301);  /* ViInt32  */
        public const int IVIDMM_ATTR_SAMPLE_TRIGGER = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 302);  /* ViInt32  */
        public const int IVIDMM_ATTR_SAMPLE_INTERVAL = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 303);  /* ViReal64 */
        public const int IVIDMM_ATTR_TRIGGER_COUNT = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 304);  /* ViInt32  */
        public const int IVIDMM_ATTR_MEAS_COMPLETE_DEST = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 305);  /* ViInt32  */

        /*- IviDmmTriggerSlope Attributes -*/
        public const int IVIDMM_ATTR_TRIGGER_SLOPE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 334);  /* ViInt32  */

        /*- IviDmmDeviceInfo Attributes -*/
        public const int IVIDMM_ATTR_APERTURE_TIME = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 321);  /* ViReal64, read-only */
        public const int IVIDMM_ATTR_APERTURE_TIME_UNITS = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 322);  /* ViInt32,  read-only */

        /*- IviDmmAutoRangeValue Attributes -*/
        public const int IVIDMM_ATTR_AUTO_RANGE_VALUE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 331);  /* ViReal64, read-only */

        /*- IviDmmAutoZero Attributes -*/
        public const int IVIDMM_ATTR_AUTO_ZERO = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 332);  /* ViInt32  */

        /*- IviDmmPowerLineFrequency Attributes -*/
        public const int IVIDMM_ATTR_POWERLINE_FREQ = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 333);  /* ViReal64 */


        /****************************************************************************
         *----------------- IviDmm Class Attribute Value Defines -------------------*
         ****************************************************************************/

        /*- Defined values for attribute IVIDMM_ATTR_FUNCTION -*/
        public const int IVIDMM_VAL_DC_VOLTS = (1);
        public const int IVIDMM_VAL_AC_VOLTS = (2);
        public const int IVIDMM_VAL_DC_CURRENT = (3);
        public const int IVIDMM_VAL_AC_CURRENT = (4);
        public const int IVIDMM_VAL_2_WIRE_RES = (5);
        public const int IVIDMM_VAL_4_WIRE_RES = (101);
        public const int IVIDMM_VAL_AC_PLUS_DC_VOLTS = (106);
        public const int IVIDMM_VAL_AC_PLUS_DC_CURRENT = (107);
        public const int IVIDMM_VAL_FREQ = (104);
        public const int IVIDMM_VAL_PERIOD = (105);
        public const int IVIDMM_VAL_TEMPERATURE = (108);

        public const int IVIDMM_VAL_FUNC_CLASS_EXT_BASE = (500);
        public const int IVIDMM_VAL_FUNC_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for attribute IVIDMM_ATTR_RANGE -*/
        public const double IVIDMM_VAL_AUTO_RANGE_ON = (-1.0);
        public const double IVIDMM_VAL_AUTO_RANGE_OFF = (-2.0);
        public const double IVIDMM_VAL_AUTO_RANGE_ONCE = (-3.0);

        public const double IVIDMM_VAL_RANGE_CLASS_EXT_BASE = (-100.0);
        public const double IVIDMM_VAL_RANGE_SPECIFIC_EXT_BASE = (-1000.0);

        /*- Defined values for attribute IVIDMM_ATTR_FREQ_VOLTAGE_RANGE -*/
        /* public const int IVIDMM_VAL_AUTO_RANGE_ON                DEFINED ABOVE */
        /* public const int IVIDMM_VAL_AUTO_RANGE_OFF               DEFINED ABOVE */

        public const double IVIDMM_VAL_FREQ_VOLT_RANGE_CLASS_EXT_BASE = (-100.0);
        public const double IVIDMM_VAL_FREQ_VOLT_RANGE_SPECIFIC_EXT_BASE = (-1000.0);

        /*- Defined values for attribute IVIDMM_ATTR_TRIGGER_DELAY -*/
        public const double IVIDMM_VAL_AUTO_DELAY_ON = (-1.0);
        public const double IVIDMM_VAL_AUTO_DELAY_OFF = (-2.0);

        public const double IVIDMM_VAL_TRIGGER_DELAY_CLASS_EXT_BASE = (-100.0);
        public const double IVIDMM_VAL_TRIGGER_DELAY_SPECIFIC_EXT_BASE = (-1000.0);

        /*- Defined values for attribute IVIDMM_ATTR_TRIGGER_SOURCE -*/
        public const int IVIDMM_VAL_IMMEDIATE = (1);
        public const int IVIDMM_VAL_EXTERNAL = (2);
        public const int IVIDMM_VAL_SOFTWARE_TRIG = (3);
        public const int IVIDMM_VAL_TTL0 = (111);
        public const int IVIDMM_VAL_TTL1 = (112);
        public const int IVIDMM_VAL_TTL2 = (113);
        public const int IVIDMM_VAL_TTL3 = (114);
        public const int IVIDMM_VAL_TTL4 = (115);
        public const int IVIDMM_VAL_TTL5 = (116);
        public const int IVIDMM_VAL_TTL6 = (117);
        public const int IVIDMM_VAL_TTL7 = (118);
        public const int IVIDMM_VAL_ECL0 = (119);
        public const int IVIDMM_VAL_ECL1 = (120);
        public const int IVIDMM_VAL_PXI_STAR = (131);
        public const int IVIDMM_VAL_RTSI_0 = (140);
        public const int IVIDMM_VAL_RTSI_1 = (141);
        public const int IVIDMM_VAL_RTSI_2 = (142);
        public const int IVIDMM_VAL_RTSI_3 = (143);
        public const int IVIDMM_VAL_RTSI_4 = (144);
        public const int IVIDMM_VAL_RTSI_5 = (145);
        public const int IVIDMM_VAL_RTSI_6 = (146);

        public const int IVIDMM_VAL_TRIGGER_SOURCE_CLASS_EXT_BASE = (500);
        public const int IVIDMM_VAL_TRIGGER_SOURCE_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for attribute IVIDMM_ATTR_TEMP_TRANSDUCER_TYPE -*/
        public const int IVIDMM_VAL_THERMOCOUPLE = (1);
        public const int IVIDMM_VAL_THERMISTOR = (2);
        public const int IVIDMM_VAL_2_WIRE_RTD = (3);
        public const int IVIDMM_VAL_4_WIRE_RTD = (4);

        public const int IVIDMM_VAL_TRANSDUCER_CLASS_EXT_BASE = (100);
        public const int IVIDMM_VAL_TRANSDUCER_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for attribute IVIDMM_ATTR_TEMP_TC_REF_JUNC_TYPE -*/
        public const int IVIDMM_VAL_TEMP_REF_JUNC_INTERNAL = (1);
        public const int IVIDMM_VAL_TEMP_REF_JUNC_FIXED = (2);
        public const int IVIDMM_VAL_TEMP_REF_JUNC_CLASS_EXT_BASE = (100);
        public const int IVIDMM_VAL_TEMP_REF_JUNC_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for attribute IVIDMM_ATTR_TEMP_TC_TYPE -*/
        public const int IVIDMM_VAL_TEMP_TC_B = (1);
        public const int IVIDMM_VAL_TEMP_TC_C = (2);
        public const int IVIDMM_VAL_TEMP_TC_D = (3);
        public const int IVIDMM_VAL_TEMP_TC_E = (4);
        public const int IVIDMM_VAL_TEMP_TC_G = (5);
        public const int IVIDMM_VAL_TEMP_TC_J = (6);
        public const int IVIDMM_VAL_TEMP_TC_K = (7);
        public const int IVIDMM_VAL_TEMP_TC_N = (8);
        public const int IVIDMM_VAL_TEMP_TC_R = (9);
        public const int IVIDMM_VAL_TEMP_TC_S = (10);
        public const int IVIDMM_VAL_TEMP_TC_T = (11);
        public const int IVIDMM_VAL_TEMP_TC_U = (12);
        public const int IVIDMM_VAL_TEMP_TC_V = (13);
        public const int IVIDMM_VAL_TEMP_TC_TYPE_CLASS_EXT_BASE = (100);
        public const int IVIDMM_VAL_TEMP_TC_TYPE_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for attribute IVIDMM_ATTR_MEAS_COMPLETE_DEST -*/
        public const int IVIDMM_VAL_NONE = (-1);
        /* #define IVIDMM_VAL_EXTERNAL                  DEFINED ABOVE */
        /* #define IVIDMM_VAL_TTL0                      DEFINED ABOVE */
        /* #define IVIDMM_VAL_TTL1                      DEFINED ABOVE */
        /* #define IVIDMM_VAL_TTL2                      DEFINED ABOVE */
        /* #define IVIDMM_VAL_TTL3                      DEFINED ABOVE */
        /* #define IVIDMM_VAL_TTL4                      DEFINED ABOVE */
        /* #define IVIDMM_VAL_TTL5                      DEFINED ABOVE */
        /* #define IVIDMM_VAL_TTL6                      DEFINED ABOVE */
        /* #define IVIDMM_VAL_TTL7                      DEFINED ABOVE */
        /* #define IVIDMM_VAL_ECL0                      DEFINED ABOVE */
        /* #define IVIDMM_VAL_ECL1                      DEFINED ABOVE */
        /* #define IVIDMM_VAL_PXI_STAR                  DEFINED ABOVE */
        /* #define IVIDMM_VAL_RTSI_0                    DEFINED ABOVE */
        /* #define IVIDMM_VAL_RTSI_1                    DEFINED ABOVE */
        /* #define IVIDMM_VAL_RTSI_2                    DEFINED ABOVE */
        /* #define IVIDMM_VAL_RTSI_3                    DEFINED ABOVE */
        /* #define IVIDMM_VAL_RTSI_4                    DEFINED ABOVE */
        /* #define IVIDMM_VAL_RTSI_5                    DEFINED ABOVE */
        /* #define IVIDMM_VAL_RTSI_6                    DEFINED ABOVE */

        /* Defined values for attribute IVIDMM_ATTR_SAMPLE_TRIGGER -*/
        /* #define IVIDMM_VAL_IMMEDIATE                 DEFINED ABOVE */
        /* #define IVIDMM_VAL_EXTERNAL                  DEFINED ABOVE */
        /* #define IVIDMM_VAL_SOFTWARE_TRIG             DEFINED ABOVE */
        /* #define IVIDMM_VAL_TTL0                      DEFINED ABOVE */
        /* #define IVIDMM_VAL_TTL1                      DEFINED ABOVE */
        /* #define IVIDMM_VAL_TTL2                      DEFINED ABOVE */
        /* #define IVIDMM_VAL_TTL3                      DEFINED ABOVE */
        /* #define IVIDMM_VAL_TTL4                      DEFINED ABOVE */
        /* #define IVIDMM_VAL_TTL5                      DEFINED ABOVE */
        /* #define IVIDMM_VAL_TTL6                      DEFINED ABOVE */
        /* #define IVIDMM_VAL_TTL7                      DEFINED ABOVE */
        /* #define IVIDMM_VAL_ECL0                      DEFINED ABOVE */
        /* #define IVIDMM_VAL_ECL1                      DEFINED ABOVE */
        /* #define IVIDMM_VAL_PXI_STAR                  DEFINED ABOVE */
        /* #define IVIDMM_VAL_RTSI_0                    DEFINED ABOVE */
        /* #define IVIDMM_VAL_RTSI_1                    DEFINED ABOVE */
        /* #define IVIDMM_VAL_RTSI_2                    DEFINED ABOVE */
        /* #define IVIDMM_VAL_RTSI_3                    DEFINED ABOVE */
        /* #define IVIDMM_VAL_RTSI_4                    DEFINED ABOVE */
        /* #define IVIDMM_VAL_RTSI_5                    DEFINED ABOVE */
        /* #define IVIDMM_VAL_RTSI_6                    DEFINED ABOVE */


        public const int IVIDMM_VAL_INTERVAL = (10);

        /*- Defined values for attribute IVIDMM_ATTR_TRIGGER_SLOPE -*/
        public const int IVIDMM_VAL_POSITIVE = (0);
        public const int IVIDMM_VAL_NEGATIVE = (1);

        public const int IVIDMM_VAL_TRIGGER_SLOPE_CLASS_EXT_BASE = (100);
        public const int IVIDMM_VAL_TRIGGER_SLOPE_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for attribute IVIDMM_ATTR_APERTURE_TIME_UNITS -*/
        public const int IVIDMM_VAL_SECONDS = (0);
        public const int IVIDMM_VAL_POWER_LINE_CYCLES = (1);

        /*- Defined values for extended attribute IVIDMM_ATTR_AUTO_ZER0 -*/
        public const int IVIDMM_VAL_AUTO_ZERO_OFF = (0);
        public const int IVIDMM_VAL_AUTO_ZERO_ON = (1);
        public const int IVIDMM_VAL_AUTO_ZERO_ONCE = (2);

        public const int IVIDMM_VAL_AUTO_ZERO_CLASS_EXT_BASE = (100);
        public const int IVIDMM_VAL_AUTO_ZERO_SPECIFIC_EXT_BASE = (1000);

    }
}
