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
    public class IviCounterAttribute
    {
        /*- IviCounter Fundamental Attributes -*/
        public const int IVICOUNTER_ATTR_CHANNEL_COUNT = IviDriverAttribute.IVI_ATTR_CHANNEL_COUNT;				/* ViInt32,  read-only */
        public const int IVICOUNTER_ATTR_MEASUREMENT_FUNCTION = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 3);	/* ViInt32		*/
        public const int IVICOUNTER_ATTR_IMPEDANCE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 4);	/* ViInt32		*/
        public const int IVICOUNTER_ATTR_COUPLING = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 5);	/* ViInt32		*/
        public const int IVICOUNTER_ATTR_ATTENUATION = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 6);	/* ViReal64		*/
        public const int IVICOUNTER_ATTR_CHANNEL_LEVEL = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 7);	/* ViReal64		*/
        public const int IVICOUNTER_ATTR_CHANNEL_HYSTERESIS = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 8);	/* ViReal64		*/
        public const int IVICOUNTER_ATTR_CHANNEL_SLOPE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 9);	/* ViInt32		*/
        public const int IVICOUNTER_ATTR_FILTER_ENABLED = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 10);	/* ViBoolean	*/
        public const int IVICOUNTER_ATTR_FREQUENCY_CHANNEL = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 11);	/* ViString		*/
        public const int IVICOUNTER_ATTR_FREQUENCY_ESTIMATE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 12);	/* ViReal64		*/
        public const int IVICOUNTER_ATTR_FREQUENCY_RESOLUTION = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 13);	/* ViReal64		*/
        public const int IVICOUNTER_ATTR_FREQUENCY_APERTURE_TIME = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 14);	/* ViReal64		*/
        public const int IVICOUNTER_ATTR_FREQUENCY_ESTIMATE_AUTO = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 15);	/* ViBoolean	*/
        public const int IVICOUNTER_ATTR_FREQUENCY_RESOLUTION_AUTO = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 16);	/* ViBoolean	*/
        public const int IVICOUNTER_ATTR_PERIOD_CHANNEL = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 18);  /* ViString		*/
        public const int IVICOUNTER_ATTR_PERIOD_ESTIMATE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 19);	/* ViReal64		*/
        public const int IVICOUNTER_ATTR_PERIOD_RESOLUTION = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 20);	/* ViReal64		*/
        public const int IVICOUNTER_ATTR_PERIOD_APERTURE_TIME = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 21);	/* ViReal64		*/
        public const int IVICOUNTER_ATTR_PULSE_WIDTH_CHANNEL = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 22);  /* ViString		*/
        public const int IVICOUNTER_ATTR_PULSE_WIDTH_ESTIMATE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 23);	/* ViReal64		*/
        public const int IVICOUNTER_ATTR_PULSE_WIDTH_RESOLUTION = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 24);	/* ViReal64		*/
        public const int IVICOUNTER_ATTR_DUTY_CYCLE_CHANNEL = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 25);  /* ViString		*/
        public const int IVICOUNTER_ATTR_DUTY_CYCLE_FREQUENCY_ESTIMATE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 26);	/* ViReal64		*/
        public const int IVICOUNTER_ATTR_DUTY_CYCLE_RESOLUTION = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 27);	/* ViReal64		*/
        public const int IVICOUNTER_ATTR_EDGE_TIME_CHANNEL = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 28);  /* ViString		*/
        public const int IVICOUNTER_ATTR_EDGE_TIME_REFERENCE_TYPE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 29);  /* ViInt32		*/
        public const int IVICOUNTER_ATTR_EDGE_TIME_ESTIMATE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 30); 	/* ViReal64		*/
        public const int IVICOUNTER_ATTR_EDGE_TIME_RESOLUTION = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 31); 	/* ViReal64		*/
        public const int IVICOUNTER_ATTR_EDGE_TIME_HIGH_REFERENCE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 32);	/* ViReal64		*/
        public const int IVICOUNTER_ATTR_EDGE_TIME_LOW_REFERENCE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 33);	/* ViReal64		*/
        public const int IVICOUNTER_ATTR_FREQUENCY_RATIO_NUMERATOR_CHANNEL = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 34);   /*ViString   */
        public const int IVICOUNTER_ATTR_FREQUENCY_RATIO_DENOMINATOR_CHANNEL = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 35);	/* ViString		*/
        public const int IVICOUNTER_ATTR_FREQUENCY_RATIO_NUMERATOR_FREQUENCY_ESTIMATE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 36);	/* ViReal64		*/
        public const int IVICOUNTER_ATTR_FREQUENCY_RATIO_ESTIMATE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 37);	/* ViReal64		*/
        public const int IVICOUNTER_ATTR_FREQUENCY_RATIO_RESOLUTION = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 38);	/* ViReal64		*/
        public const int IVICOUNTER_ATTR_TIME_INTERVAL_START_CHANNEL = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 39);  /* ViString		*/
        public const int IVICOUNTER_ATTR_TIME_INTERVAL_STOP_CHANNEL = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 40);	/* ViString		*/
        public const int IVICOUNTER_ATTR_TIME_INTERVAL_ESTIMATE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 41);	/* ViReal64		*/
        public const int IVICOUNTER_ATTR_TIME_INTERVAL_RESOLUTION = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 42);	/* ViReal64		*/
        public const int IVICOUNTER_ATTR_PHASE_INPUT_CHANNEL = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 43);  /* ViString		*/
        public const int IVICOUNTER_ATTR_PHASE_REFERENCE_CHANNEL = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 44);	/* ViString		*/
        public const int IVICOUNTER_ATTR_PHASE_FREQUENCY_ESTIMATE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 45);	/* ViReal64		*/
        public const int IVICOUNTER_ATTR_PHASE_RESOLUTION = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 46);	/* ViReal64		*/
        public const int IVICOUNTER_ATTR_CONTINUOUS_TOTALIZE_CHANNEL = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 47);  /* ViString		*/
        public const int IVICOUNTER_ATTR_GATED_TOTALIZE_CHANNEL = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 48);  /* ViString		*/
        public const int IVICOUNTER_ATTR_GATED_TOTALIZE_GATE_SOURCE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 49);	/* ViString		*/
        public const int IVICOUNTER_ATTR_GATED_TOTALIZE_GATE_SLOPE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 50);	/* ViInt32		*/
        public const int IVICOUNTER_ATTR_TIMED_TOTALIZE_CHANNEL = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 51);  /* ViString		*/
        public const int IVICOUNTER_ATTR_TIMED_TOTALIZE_GATE_TIME = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 52);	/* ViReal64		*/
        public const int IVICOUNTER_ATTR_START_ARM_TYPE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 53); 	/* ViInt32		*/
        public const int IVICOUNTER_ATTR_EXTERNAL_START_ARM_SOURCE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 54);	/* ViString		*/
        public const int IVICOUNTER_ATTR_EXTERNAL_START_ARM_LEVEL = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 55);	/* ViReal64		*/
        public const int IVICOUNTER_ATTR_EXTERNAL_START_ARM_SLOPE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 56);	/* ViInt32		*/
        public const int IVICOUNTER_ATTR_EXTERNAL_START_ARM_DELAY = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 57);	/* ViReal64		*/
        public const int IVICOUNTER_ATTR_STOP_ARM_TYPE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 58);	/* ViInt32		*/
        public const int IVICOUNTER_ATTR_EXTERNAL_STOP_ARM_SOURCE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 59);	/* ViString		*/
        public const int IVICOUNTER_ATTR_EXTERNAL_STOP_ARM_LEVEL = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 60);	/* ViReal64		*/
        public const int IVICOUNTER_ATTR_EXTERNAL_STOP_ARM_SLOPE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 61);	/* ViInt32		*/
        public const int IVICOUNTER_ATTR_EXTERNAL_STOP_ARM_DELAY = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 62);	/* ViReal64		*/

        /*- IviCounter Filter Group Attributes ---------------*/
        public const int IVICOUNTER_ATTR_FILTER_MINIMUM_FREQUENCY = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 501);	/* ViReal64		*/
        public const int IVICOUNTER_ATTR_FILTER_MAXIMUM_FREQUENCY = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 502);	/* ViReal64		*/

        /*- IviCounter Time Interval Delay Group Attributes ---------------*/
        public const int IVICOUNTER_ATTR_TIME_INTERVAL_STOP_HOLDOFF = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 601);/* ViReal64		*/

        /*- IviCounter Voltage Measurement Group Attributes ---------------*/
        public const int IVICOUNTER_ATTR_VOLTAGE_CHANNEL = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 701); /* ViString		*/
        public const int IVICOUNTER_ATTR_VOLTAGE_ESTIMATE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 702);	/* ViReal64		*/
        public const int IVICOUNTER_ATTR_VOLTAGE_RESOLUTION = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 703);	/* ViReal64		*/

        /****************************************************************************
         *----------------- IviCounter Class Attribute Value Defines -----------------*
         ****************************************************************************/

        /*- Defined values for attribute IVICOUNTER_ATTR_COUPLING -*/
        public const int IVICOUNTER_VAL_AC = (1);
        public const int IVICOUNTER_VAL_DC = (2);

        public const int IVICOUNTER_VAL_COUPLING_CLASS_EXT_BASE = (500);
        public const int IVICOUNTER_VAL_COUPLING_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for attribute IVICOUNTER_ATTR_CHANNEL_SLOPE -*/
        /*- Defined values for attribute IVICOUNTER_ATTR_GATED_TOTALIZE_GATE_SLOPE -*/
        /*- Defined values for attribute IVICOUNTER_ATTR_EXTERNAL_START_ARM_SLOPE -*/
        /*- Defined values for attribute IVICOUNTER_ATTR_EXTERNAL_STOP_ARM_SLOPE -*/
        public const int IVICOUNTER_VAL_NEGATIVE = (0);
        public const int IVICOUNTER_VAL_POSITIVE = (1);

        public const int IVICOUNTER_VAL_SLOPE_CLASS_EXT_BASE = (500);
        public const int IVICOUNTER_VAL_SLOPE_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for attribute IVICOUNTER_ATTR_MEASUREMENT_FUNCTION -*/
        public const int IVICOUNTER_VAL_FREQUENCY = (1);
        public const int IVICOUNTER_VAL_FREQUENCY_WITH_APERTURE = (2);
        public const int IVICOUNTER_VAL_PERIOD = (3);
        public const int IVICOUNTER_VAL_PERIOD_WITH_APERTURE = (4);
        public const int IVICOUNTER_VAL_PULSE_WIDTH = (5);
        public const int IVICOUNTER_VAL_DUTY_CYCLE = (6);
        public const int IVICOUNTER_VAL_EDGE_TIME = (7);
        public const int IVICOUNTER_VAL_FREQUENCY_RATIO = (8);
        public const int IVICOUNTER_VAL_TIME_INTERVAL = (9);
        public const int IVICOUNTER_VAL_PHASE = (10);
        public const int IVICOUNTER_VAL_CONTINUOUS_TOTALIZE = (11);
        public const int IVICOUNTER_VAL_GATED_TOTALIZE = (12);
        public const int IVICOUNTER_VAL_TIMED_TOTALIZE = (13);
        public const int IVICOUNTER_VAL_DC_VOLTAGE = (14);
        public const int IVICOUNTER_VAL_MAXIMUM_VOLTAGE = (15);
        public const int IVICOUNTER_VAL_MINIMUM_VOLTAGE = (16);
        public const int IVICOUNTER_VAL_RMS_VOLTAGE = (17);
        public const int IVICOUNTER_VAL_PEAK_TO_PEAK_VOLTAGE = (18);

        public const int IVICOUNTER_VAL_MEASUREMENT_FUNCTION_CLASS_EXT_BASE = (500);
        public const int IVICOUNTER_VAL_MEASUREMENT_FUNCTION_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for attribute IVICOUNTER_ATTR_START_ARM_TYPE -*/
        /*- Defined values for attribute IVICOUNTER_ATTR_STOP_ARM_TYPE -*/
        public const int IVICOUNTER_VAL_IMMEDIATE_ARM_TYPE = (1);
        public const int IVICOUNTER_VAL_EXTERNAL_ARM_TYPE = (2);

        public const int IVICOUNTER_VAL_ARM_TYPE_CLASS_EXT_BASE = (500);
        public const int IVICOUNTER_VAL_ARM_TYPE_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for attribute IVICOUNTER_ATTR_EDGE_TIME_REFERENCE_TYPE -*/
        public const int IVICOUNTER_VAL_VOLTAGE_REFERENCE_TYPE = (1);
        public const int IVICOUNTER_VAL_PERCENT_REFERENCE_TYPE = (2);

        public const int IVICOUNTER_VAL_REFERENCE_TYPE_CLASS_EXT_BASE = (500);
        public const int IVICOUNTER_VAL_REFERENCE_TYPE_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for Function Parameters  -*/
        public const int IVICOUNTER_VAL_MAX_TIME_IMMEDIATE = (0);
        public const int IVICOUNTER_VAL_MAX_TIME_INFINITE = (-1);

        public const int IVICOUNTER_VAL_MEASUREMENT_COMPLETE = (1);
        public const int IVICOUNTER_VAL_MEASUREMENT_IN_PROGRESS = (0);
        public const int IVICOUNTER_VAL_MEASUREMENT_STATUS_UNKNOWN = (-1);

    }
}
