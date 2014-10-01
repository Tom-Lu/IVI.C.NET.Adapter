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
    public class IviDigitizerAttribute
    {

        /*- IviDigitizer Fundamental Attributes -*/
        public const int IVIDIGITIZER_ATTR_CHANNEL_COUNT = IviDriverAttribute.IVI_ATTR_CHANNEL_COUNT;                /* ViInt32,  read-only */
        public const int IVIDIGITIZER_ATTR_ACTIVE_TRIGGER_SOURCE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 1);		/* ViString */
        public const int IVIDIGITIZER_ATTR_CHANNEL_ENABLED = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 2);		/* ViBoolean */
        public const int IVIDIGITIZER_ATTR_INPUT_CONNECTOR_SELECTION = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 3);		/* ViInt32 */
        public const int IVIDIGITIZER_ATTR_INPUT_IMPEDANCE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 4);		/* ViReal64 */
        public const int IVIDIGITIZER_ATTR_IS_IDLE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 5);		/* ViInt32, read-only */
        public const int IVIDIGITIZER_ATTR_IS_MEASURING = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 6);		/* ViInt32, read-only */
        public const int IVIDIGITIZER_ATTR_IS_WAITING_FOR_ARM = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 7);		/* ViInt32, read-only */
        public const int IVIDIGITIZER_ATTR_IS_WAITING_FOR_TRIGGER = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 8);		/* ViInt32, read-only */
        public const int IVIDIGITIZER_ATTR_MAX_FIRST_VALID_POINT_VAL = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 9);		/* ViInt64, read-only */
        public const int IVIDIGITIZER_ATTR_MAX_SAMPLES_PER_CHANNEL = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 10);		/* ViInt64, read-only */
        public const int IVIDIGITIZER_ATTR_MIN_RECORD_SIZE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 11);		/* ViInt64, read-only */
        public const int IVIDIGITIZER_ATTR_NUM_ACQUIRED_RECORDS = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 12);		/* ViInt64, read-only */
        public const int IVIDIGITIZER_ATTR_NUM_RECORDS_TO_ACQUIRE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 13);		/* ViInt64 */
        public const int IVIDIGITIZER_ATTR_RECORD_SIZE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 14);		/* ViInt64 */
        public const int IVIDIGITIZER_ATTR_SAMPLE_RATE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 15);		/* ViReal64 */
        public const int IVIDIGITIZER_ATTR_TRIGGER_COUPLING = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 16);		/* ViInt32 */
        public const int IVIDIGITIZER_ATTR_TRIGGER_DELAY = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 17);		/* ViReal64 */
        public const int IVIDIGITIZER_ATTR_TRIGGER_HYSTERESIS = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 18);		/* ViReal64 */
        public const int IVIDIGITIZER_ATTR_TRIGGER_LEVEL = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 19);		/* ViReal64 */
        public const int IVIDIGITIZER_ATTR_TRIGGER_OUTPUT_ENABLED = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 20);		/* ViBoolean */
        public const int IVIDIGITIZER_ATTR_TRIGGER_SLOPE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 21);		/* ViInt32 */
        public const int IVIDIGITIZER_ATTR_TRIGGER_SOURCE_COUNT = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 22);		/* ViInt32, read-only */
        public const int IVIDIGITIZER_ATTR_TRIGGER_TYPE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 23);		/* ViInt32 */
        public const int IVIDIGITIZER_ATTR_VERTICAL_COUPLING = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 24);		/* ViInt32 */
        public const int IVIDIGITIZER_ATTR_VERTICAL_OFFSET = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 25);		/* ViReal64 */
        public const int IVIDIGITIZER_ATTR_VERTICAL_RANGE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 26);		/* ViReal64 */

        /*- IviDigitizerBoardTemperature Extension Group -*/
        public const int IVIDIGITIZER_ATTR_BOARD_TEMPERATURE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 100);		/* ViReal64, read-only */
        public const int IVIDIGITIZER_ATTR_TEMPERATURE_UNITS = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 101);		/* ViInt32 */

        /*- IviDigitizerChannelFilter Extension Group -*/
        public const int IVIDIGITIZER_ATTR_INPUT_FILTER_BYPASS = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 200);		/* ViBoolean */
        public const int IVIDIGITIZER_ATTR_INPUT_FILTER_MAX_FREQUENCY = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 201);		/* ViReal64 */
        public const int IVIDIGITIZER_ATTR_INPUT_FILTER_MIN_FREQUENCY = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 202);		/* ViReal64 */

        /*- IviDigitizerChannelTemperature Extension Group -*/
        public const int IVIDIGITIZER_ATTR_CHANNEL_TEMPERATURE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 300);		/* ViReal64, read-only */

        /*- IviDigitizerTimeInterleavedChannels Extension Group -*/
        public const int IVIDIGITIZER_ATTR_TIME_INTERLEAVED_CHANNEL_LIST = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 400);		/* ViString */
        public const int IVIDIGITIZER_ATTR_TIME_INTERLEAVED_CHANNEL_LIST_AUTO = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 401);		/* ViBoolean */

        /*- IviDigitizerDataInterleavedChannels Extension Group -*/
        public const int IVIDIGITIZER_ATTR_DATA_INTERLEAVED_CHANNEL_LIST = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 500);		/* ViString */

        /*- IviDigitizerReferenceOscillator Extension Group -*/
        public const int IVIDIGITIZER_ATTR_REFERENCE_OSCILLATOR_EXTERNAL_FREQUENCY = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 600);		/* ViReal64 */
        public const int IVIDIGITIZER_ATTR_REFERENCE_OSCILLATOR_OUTPUT_ENABLED = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 601);		/* ViBoolean */
        public const int IVIDIGITIZER_ATTR_REFERENCE_OSCILLATOR_SOURCE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 602);		/* ViInt32 */

        /*- IviDigitizerSampleClock Extension Group -*/
        public const int IVIDIGITIZER_ATTR_SAMPLE_CLOCK_EXTERNAL_DIVIDER = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 700);		/* ViReal64 */
        public const int IVIDIGITIZER_ATTR_SAMPLE_CLOCK_EXTERNAL_FREQUENCY = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 701);		/* ViReal64 */
        public const int IVIDIGITIZER_ATTR_SAMPLE_CLOCK_OUTPUT_ENABLED = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 702);		/* ViBoolean */
        public const int IVIDIGITIZER_ATTR_SAMPLE_CLOCK_SOURCE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 703);		/* ViInt32 */

        /*- IviDigitizerSampleMode Extension Group -*/
        public const int IVIDIGITIZER_ATTR_SAMPLE_MODE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 800);		/* ViInt32 */

        /*- IviDigitizerDownconversion Extension Group -*/
        public const int IVIDIGITIZER_ATTR_DOWNCONVERSION_ENABLED = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 900);		/* ViBoolean */
        public const int IVIDIGITIZER_ATTR_DOWNCONVERSION_CENTER_FREQUENCY = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 901);		/* ViReal64 */
        public const int IVIDIGITIZER_ATTR_DOWNCONVERSION_IQ_INTERLEAVED = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 902);		/* ViBoolean */

        /*- IviDigitizerArm Extension Group -*/
        public const int IVIDIGITIZER_ATTR_ACTIVE_ARM_SOURCE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 1000);	/* ViString */
        public const int IVIDIGITIZER_ATTR_ARM_COUNT = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 1001);	/* ViInt32 */
        public const int IVIDIGITIZER_ATTR_ARM_COUPLING = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 1002);	/* ViInt32 */
        public const int IVIDIGITIZER_ATTR_ARM_DELAY = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 1003);	/* ViReal64 */
        public const int IVIDIGITIZER_ATTR_ARM_HYSTERESIS = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 1004);	/* ViReal64 */
        public const int IVIDIGITIZER_ATTR_ARM_LEVEL = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 1005);	/* ViReal64 */
        public const int IVIDIGITIZER_ATTR_ARM_OUTPUT_ENABLED = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 1006);	/* ViReal64 */
        public const int IVIDIGITIZER_ATTR_ARM_SLOPE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 1007);	/* ViInt32 */
        public const int IVIDIGITIZER_ATTR_ARM_SOURCE_COUNT = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 1008);	/* ViInt32, read-only */
        public const int IVIDIGITIZER_ATTR_ARM_TYPE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 1009);	/* ViInt32 */

        /*- IviDigitizerMultiArm Extension Group -*/
        public const int IVIDIGITIZER_ATTR_ARM_SOURCE_LIST = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 1100);	/* ViString */
        public const int IVIDIGITIZER_ATTR_ARM_SOURCE_OPERATOR = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 1101);	/* ViInt32 */

        /*- IviDigitizerGlitchArm Extension Group -*/
        public const int IVIDIGITIZER_ATTR_GLITCH_ARM_CONDITION = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 1200);	/* ViInt32 */
        public const int IVIDIGITIZER_ATTR_GLITCH_ARM_POLARITY = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 1201);	/* ViInt32 */
        public const int IVIDIGITIZER_ATTR_GLITCH_ARM_WIDTH = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 1202);	/* ViReal64 */

        /*- IviDigitizerRuntArm Extension Group -*/
        public const int IVIDIGITIZER_ATTR_RUNT_ARM_HIGH_THRESHOLD = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 1300);	/* ViReal64 */
        public const int IVIDIGITIZER_ATTR_RUNT_ARM_LOW_THRESHOLD = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 1301);	/* ViReal64 */
        public const int IVIDIGITIZER_ATTR_RUNT_ARM_POLARITY = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 1302);	/* ViInt32 */

        /*- IviDigitizerTVArm Extension -*/
        public const int IVIDIGITIZER_ATTR_TV_ARM_EVENT = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 1400);	/* ViInt32 */
        public const int IVIDIGITIZER_ATTR_TV_ARM_LINE_NUMBER = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 1401);	/* ViInt32 */
        public const int IVIDIGITIZER_ATTR_TV_ARM_POLARITY = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 1402);	/* ViInt32 */
        public const int IVIDIGITIZER_ATTR_TV_ARM_SIGNAL_FORMAT = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 1403);	/* ViInt32 */

        /*- IviDigitizerWidthArm Extension Group -*/
        public const int IVIDIGITIZER_ATTR_WIDTH_ARM_CONDITION = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 1500);	/* ViInt32 */
        public const int IVIDIGITIZER_ATTR_WIDTH_ARM_HIGH_THRESHOLD = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 1501);	/* ViReal64 */
        public const int IVIDIGITIZER_ATTR_WIDTH_ARM_LOW_THRESHOLD = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 1502);	/* ViReal64 */
        public const int IVIDIGITIZER_ATTR_WIDTH_ARM_POLARITY = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 1503);	/* ViInt32 */

        /*- IviDigitizerWindowArm Extension Group -*/
        public const int IVIDIGITIZER_ATTR_WINDOW_ARM_CONDITION = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 1600);	/* ViInt32 */
        public const int IVIDIGITIZER_ATTR_WINDOW_ARM_HIGH_THRESHOLD = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 1601);	/* ViReal64 */
        public const int IVIDIGITIZER_ATTR_WINDOW_ARM_LOW_THRESHOLD = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 1602);	/* ViReal64 */

        /*- IviDigitizerTriggerModifier Extension Group -*/
        public const int IVIDIGITIZER_ATTR_TRIGGER_MODIFIER = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 1700);	/* ViInt32 */

        /*- IviDigitizerMultiTrigger Extension Group -*/
        public const int IVIDIGITIZER_ATTR_TRIGGER_SOURCE_LIST = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 1800);	/* ViString */
        public const int IVIDIGITIZER_ATTR_TRIGGER_SOURCE_OPERATOR = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 1801);	/* ViInt32 */

        /*- IviDigitizerPretriggerSamples Extension Group -*/
        public const int IVIDIGITIZER_ATTR_PRETRIGGER_SAMPLES = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 1900);	/* ViInt64 */

        /*- IviDigitizerTriggerHoldoff Extension Group -*/
        public const int IVIDIGITIZER_ATTR_TRIGGER_HOLDOFF = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 2000);	/* ViReal64 */

        /*- IIviDigitizerGlitchTrigger Extension Group -*/
        public const int IVIDIGITIZER_ATTR_GLITCH_TRIGGER_CONDITION = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 2100);	/* ViInt32 */
        public const int IVIDIGITIZER_ATTR_GLITCH_TRIGGER_POLARITY = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 2101);	/* ViInt32 */
        public const int IVIDIGITIZER_ATTR_GLITCH_TRIGGER_WIDTH = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 2102);	/* ViReal64 */

        /*- IviDigitizerRuntTrigger Extension Group -*/
        public const int IVIDIGITIZER_ATTR_RUNT_TRIGGER_HIGH_THRESHOLD = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 2200);	/* ViReal64 */
        public const int IVIDIGITIZER_ATTR_RUNT_TRIGGER_LOW_THRESHOLD = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 2201);	/* ViReal64 */
        public const int IVIDIGITIZER_ATTR_RUNT_TRIGGER_POLARITY = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 2202);	/* ViInt32 */

        /*- IviDigitizerTVTrigger Extension Group -*/
        public const int IVIDIGITIZER_ATTR_TV_TRIGGER_EVENT = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 2300);	/* ViInt32 */
        public const int IVIDIGITIZER_ATTR_TV_TRIGGER_LINE_NUMBER = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 2301);	/* ViInt32 */
        public const int IVIDIGITIZER_ATTR_TV_TRIGGER_POLARITY = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 2302);	/* ViInt32 */
        public const int IVIDIGITIZER_ATTR_TV_TRIGGER_SIGNAL_FORMAT = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 2303);	/* ViInt32 */

        /*- IviDigitizerWidthTrigger Extension Group -*/
        public const int IVIDIGITIZER_ATTR_WIDTH_TRIGGER_CONDITION = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 2400);	/* ViInt32 */
        public const int IVIDIGITIZER_ATTR_WIDTH_TRIGGER_HIGH_THRESHOLD = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 2401);	/* ViReal64 */
        public const int IVIDIGITIZER_ATTR_WIDTH_TRIGGER_LOW_THRESHOLD = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 2402);	/* ViReal64 */
        public const int IVIDIGITIZER_ATTR_WIDTH_TRIGGER_POLARITY = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 2403);	/* ViInt32 */

        /*- IviDigitizerWindowTrigger Extension Group -*/
        public const int IVIDIGITIZER_ATTR_WINDOW_TRIGGER_CONDITION = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 2500);	/* ViInt32 */
        public const int IVIDIGITIZER_ATTR_WINDOW_TRIGGER_HIGH_THRESHOLD = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 2501);	/* ViReal64 */
        public const int IVIDIGITIZER_ATTR_WINDOW_TRIGGER_LOW_THRESHOLD = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 2502);	/* ViReal64 */

        /****************************************************************************
         *----------------- IviDigitizer Class Attribute Value Defines -------------*
         ****************************************************************************/
        /*- Defined values for attribute IVIDIGITIZER_ATTR_IS_IDLE -*/
        /*- Defined values for attribute IVIDIGITIZER_ATTR_IS_MEASURING -*/
        /*- Defined values for attribute IVIDIGITIZER_ATTR_IS_WAITING_FOR_ARM -*/
        /*- Defined values for attribute IVIDIGITIZER_ATTR_IS_WAITING_FOR_TRIGGER -*/
        public const int IVIDIGITIZER_VAL_ACQUISITION_STATUS_RESULT_TRUE = (1);
        public const int IVIDIGITIZER_VAL_ACQUISITION_STATUS_RESULT_FALSE = (2);
        public const int IVIDIGITIZER_VAL_ACQUISITION_STATUS_RESULT_UNKNOWN = (3);

        /*- Defined values for attribute IVIDIGITIZER_ATTR_TRIGGER_COUPLING -*/
        /*- Defined values for attribute IVIDIGITIZER_ATTR_ARM_COUPLING -*/
        public const int IVIDIGITIZER_VAL_TRIGGER_COUPLING_AC = (0);
        public const int IVIDIGITIZER_VAL_TRIGGER_COUPLING_DC = (1);
        public const int IVIDIGITIZER_VAL_TRIGGER_COUPLING_HF_REJECT = (2);
        public const int IVIDIGITIZER_VAL_TRIGGER_COUPLING_LF_REJECT = (3);
        public const int IVIDIGITIZER_VAL_TRIGGER_COUPLING_NOISE_REJECT = (4);

        /*- Defined values for attribute IVIDIGITIZER_ATTR_TRIGGER_SLOPE -*/
        /*- Defined values for attribute IVIDIGITIZER_ATTR_ARM_SLOPE -*/
        public const int IVIDIGITIZER_VAL_TRIGGER_SLOPE_NEGATIVE = (0);
        public const int IVIDIGITIZER_VAL_TRIGGER_SLOPE_POSITIVE = (1);

        /*- Defined values for attribute IVIDIGITIZER_ATTR_TRIGGER_TYPE -*/
        public const int IVIDIGITIZER_VAL_EDGE_TRIGGER = (1);
        public const int IVIDIGITIZER_VAL_WIDTH_TRIGGER = (2);
        public const int IVIDIGITIZER_VAL_RUNT_TRIGGER = (3);
        public const int IVIDIGITIZER_VAL_GLITCH_TRIGGER = (4);
        public const int IVIDIGITIZER_VAL_TV_TRIGGER = (5);
        public const int IVIDIGITIZER_VAL_WINDOW_TRIGGER = (6);

        /*- Defined values for attribute IVIDIGITIZER_ATTR_ARM_TYPE -*/
        public const int IVIDIGITIZER_VAL_EDGE_ARM = (1);
        public const int IVIDIGITIZER_VAL_WIDTH_ARM = (2);
        public const int IVIDIGITIZER_VAL_RUNT_ARM = (3);
        public const int IVIDIGITIZER_VAL_GLITCH_ARM = (4);
        public const int IVIDIGITIZER_VAL_TV_ARM = (5);
        public const int IVIDIGITIZER_VAL_WINDOW_ARM = (6);

        /*- Defined values for attribute IVIDIGITIZER_ATTR_VERTICAL_COUPLING -*/
        public const int IVIDIGITIZER_VAL_VERTICAL_COUPLING_AC = (0);
        public const int IVIDIGITIZER_VAL_VERTICAL_COUPLING_DC = (1);
        public const int IVIDIGITIZER_VAL_VERTICAL_COUPLING_GND = (2);

        /*- Defined values for attribute IVIDIGITIZER_ATTR_TEMPERATURE_UNITS -*/
        public const int IVIDIGITIZER_VAL_CELSIUS = (0);
        public const int IVIDIGITIZER_VAL_FAHRENHEIT = (1);
        public const int IVIDIGITIZER_VAL_KELVIN = (2);

        /*- Defined values for attribute IVIDIGITIZER_ATTR_REFERENCE_OSCILLATOR_SOURCE -*/
        public const int IVIDIGITIZER_VAL_REFERENCE_OSCILLATOR_SOURCE_INTERNAL = (0);
        public const int IVIDIGITIZER_VAL_REFERENCE_OSCILLATOR_SOURCE_EXTERNAL = (1);
        public const int IVIDIGITIZER_VAL_REFERENCE_OSCILLATOR_SOURCE_PXI_CLK10 = (2);
        public const int IVIDIGITIZER_VAL_REFERENCE_OSCILLATOR_SOURCE_PXIE_CLK100 = (3);

        /*- Defined values for attribute IVIDIGITIZER_ATTR_SAMPLE_CLOCK_SOURCE -*/
        public const int IVIDIGITIZER_VAL_SAMPLE_CLOCK_SOURCE_INTERNAL = (0);
        public const int IVIDIGITIZER_VAL_SAMPLE_CLOCK_SOURCE_EXTERNAL = (1);

        /*- Defined values for attribute IVIDIGITIZER_ATTR_SAMPLE_MODE -*/
        public const int IVIDIGITIZER_VAL_SAMPLE_MODE_REAL_TIME = (0);
        public const int IVIDIGITIZER_VAL_SAMPLE_MODE_EQUIVALENT_TIME = (1);

        /*- Defined values for attribute IVIDIGITIZER_ATTR_ARM_SOURCE_OPERATOR -*/
        public const int IVIDIGITIZER_VAL_ARM_SOURCE_OPERATOR_AND = (0);
        public const int IVIDIGITIZER_VAL_ARM_SOURCE_OPERATOR_OR = (1);
        public const int IVIDIGITIZER_VAL_ARM_SOURCE_OPERATOR_NONE = (2);

        /*- Defined values for attribute IVIDIGITIZER_ATTR_GLITCH_ARM_CONDITION -*/
        /*- Defined values for attribute IVIDIGITIZER_ATTR_GLITCH_TRIGGER_CONDITION -*/
        public const int IVIDIGITIZER_VAL_GLITCH_LESS_THAN = (1);
        public const int IVIDIGITIZER_VAL_GLITCH_GREATER_THAN = (2);

        /*- Defined values for attribute IVIDIGITIZER_ATTR_GLITCH_ARM_POLARITY -*/
        /*- Defined values for attribute IVIDIGITIZER_ATTR_GLITCH_TRIGGER_POLARITY -*/
        public const int IVIDIGITIZER_VAL_GLITCH_POSITIVE = (1);
        public const int IVIDIGITIZER_VAL_GLITCH_NEGATIVE = (2);
        public const int IVIDIGITIZER_VAL_GLITCH_EITHER = (3);

        /*- Defined values for attribute IVIDIGITIZER_ATTR_RUNT_ARM_POLARITY -*/
        /*- Defined values for attribute IVIDIGITIZER_ATTR_RUNT_TRIGGER_POLARITY -*/
        public const int IVIDIGITIZER_VAL_RUNT_POSITIVE = (1);
        public const int IVIDIGITIZER_VAL_RUNT_NEGATIVE = (2);
        public const int IVIDIGITIZER_VAL_RUNT_EITHER = (3);

        /*- Defined values for attribute IVIDIGITIZER_ATTR_TV_ARM_EVENT -*/
        /*- Defined values for attribute IVIDIGITIZER_ATTR_TV_TRIGGER_EVENT -*/
        public const int IVIDIGITIZER_VAL_TV_EVENT_FIELD1 = (1);
        public const int IVIDIGITIZER_VAL_TV_EVENT_FIELD2 = (2);
        public const int IVIDIGITIZER_VAL_TV_EVENT_ANY_FIELD = (3);
        public const int IVIDIGITIZER_VAL_TV_EVENT_ANY_LINE = (4);
        public const int IVIDIGITIZER_VAL_TV_EVENT_LINE_NUMBER = (5);

        /*- Defined values for attribute IVIDIGITIZER_ATTR_TV_ARM_POLARITY -*/
        /*- Defined values for attribute IVIDIGITIZER_ATTR_TV_TRIGGER_POLARITY -*/
        public const int IVIDIGITIZER_VAL_TV_POSITIVE = (1);
        public const int IVIDIGITIZER_VAL_TV_NEGATIVE = (2);

        /*- Defined values for attribute IVIDIGITIZER_ATTR_TV_ARM_SIGNAL_FORMAT -*/
        /*- Defined values for attribute IVIDIGITIZER_ATTR_TV_TRIGGER_SIGNAL_FORMAT -*/
        public const int IVIDIGITIZER_VAL_NTSC = (1);
        public const int IVIDIGITIZER_VAL_PAL = (2);
        public const int IVIDIGITIZER_VAL_SECAM = (3);

        /*- Defined values for attribute IVIDIGITIZER_ATTR_WIDTH_ARM_CONDITION -*/
        /*- Defined values for attribute IVIDIGITIZER_ATTR_WIDTH_TRIGGER_CONDITION -*/
        public const int IVIDIGITIZER_VAL_WIDTH_WITHIN = (1);
        public const int IVIDIGITIZER_VAL_WIDTH_OUTSIDE = (2);

        /*- Defined values for attribute IVIDIGITIZER_ATTR_WIDTH_ARM_POLARITY -*/
        /*- Defined values for attribute IVIDIGITIZER_ATTR_WIDTH_TRIGGER_POLARITY -*/
        public const int IVIDIGITIZER_VAL_WIDTH_POSITIVE = (1);
        public const int IVIDIGITIZER_VAL_WIDTH_NEGATIVE = (2);
        public const int IVIDIGITIZER_VAL_WIDTH_EITHER = (3);

        /*- Defined values for attribute IVIDIGITIZER_ATTR_WINDOW_ARM_CONDITION -*/
        /*- Defined values for attribute IVIDIGITIZER_ATTR_WINDOW_TRIGGER_CONDITION -*/
        public const int IVIDIGITIZER_VAL_WINDOW_CONDITION_ENTERING = (1);
        public const int IVIDIGITIZER_VAL_WINDOW_CONDITION_LEAVING = (2);

        /*- Defined values for attribute IVIDIGITIZER_ATTR_TRIGGER_MODIFIER -*/
        public const int IVIDIGITIZER_VAL_TRIGGER_MODIFIER_NONE = (1);
        public const int IVIDIGITIZER_VAL_TRIGGER_MODIFIER_AUTO = (2);
        public const int IVIDIGITIZER_VAL_TRIGGER_MODIFIER_AUTO_LEVEL = (3);

        public const int IVIDIGITIZER_VAL_TRIGGER_MOD_CLASS_EXT_BASE = (100);
        public const int IVIDIGITIZER_VAL_TRIGGER_MOD_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for attribute IVIDIGITIZER_ATTR_TRIGGER_SOURCE_OPERATOR -*/
        public const int IVIDIGITIZER_VAL_TRIGGER_SOURCE_OPERATOR_AND = (0);
        public const int IVIDIGITIZER_VAL_TRIGGER_SOURCE_OPERATOR_OR = (1);
        public const int IVIDIGITIZER_VAL_TRIGGER_SOURCE_OPERATOR_NONE = (2);

    }
}
