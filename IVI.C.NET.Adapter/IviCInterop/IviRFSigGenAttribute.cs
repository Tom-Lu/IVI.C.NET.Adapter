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
    public class IviRFSigGenAttribute
    {
        /* Instrument Capabilities */
        public const int IVIRFSIGGEN_ATTR_CHANNEL_COUNT = IviDriverAttribute.IVI_ATTR_CHANNEL_COUNT;                 /* ViInt32,  read-only  */

        /*- IviRFSigGen Fundamental Attributes -*/
        public const int IVIRFSIGGEN_ATTR_FREQUENCY = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 1);    /* ViReal64 */
        public const int IVIRFSIGGEN_ATTR_POWER_LEVEL = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 2);    /* ViReal64 */
        public const int IVIRFSIGGEN_ATTR_ALC_ENABLED = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 3);    /* ViBoolean */
        public const int IVIRFSIGGEN_ATTR_OUTPUT_ENABLED = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 4);    /* ViBoolean */

        /*- IviRFSigGen Extended Attributes -*/
        /*- IviRFSigGenModulateAM Extension Group -*/
        public const int IVIRFSIGGEN_ATTR_AM_ENABLED = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 11);   /* ViBoolean */
        public const int IVIRFSIGGEN_ATTR_AM_SOURCE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 12);   /* ViString */
        public const int IVIRFSIGGEN_ATTR_AM_SCALING = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 13);   /* ViInt32 */
        public const int IVIRFSIGGEN_ATTR_AM_EXTERNAL_COUPLING = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 14);   /* ViInt32 */
        public const int IVIRFSIGGEN_ATTR_AM_NOMINAL_VOLTAGE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 15);   /* ViReal64 */
        public const int IVIRFSIGGEN_ATTR_AM_DEPTH = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 16);   /* ViReal64 */

        /*- IviRFSigGenModulateFM Extension Group -*/
        public const int IVIRFSIGGEN_ATTR_FM_ENABLED = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 21);   /* ViBoolean */
        public const int IVIRFSIGGEN_ATTR_FM_SOURCE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 22);   /* ViString */
        public const int IVIRFSIGGEN_ATTR_FM_EXTERNAL_COUPLING = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 23);   /* ViInt32 */
        public const int IVIRFSIGGEN_ATTR_FM_NOMINAL_VOLTAGE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 24);   /* ViReal64 */
        public const int IVIRFSIGGEN_ATTR_FM_DEVIATION = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 25);   /* ViReal64 */

        /*- IviRFSigGenModulatePM Extension Group -*/
        public const int IVIRFSIGGEN_ATTR_PM_ENABLED = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 31);   /* ViBoolean */
        public const int IVIRFSIGGEN_ATTR_PM_SOURCE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 32);   /* ViString */
        public const int IVIRFSIGGEN_ATTR_PM_EXTERNAL_COUPLING = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 33);   /* ViInt32 */
        public const int IVIRFSIGGEN_ATTR_PM_NOMINAL_VOLTAGE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 34);   /* ViReal64 */
        public const int IVIRFSIGGEN_ATTR_PM_DEVIATION = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 35);   /* ViReal64 */

        /*- IviRFSigGenAnalogModulationSource Extension Group -*/
        public const int IVIRFSIGGEN_ATTR_ANALOG_MODULATION_SOURCE_COUNT = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 41);   /* ViInt32 */

        /*- IviRFSigGenModulatePulse Extension Group -*/
        public const int IVIRFSIGGEN_ATTR_PULSE_MODULATION_ENABLED = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 51);   /* ViBoolean */
        public const int IVIRFSIGGEN_ATTR_PULSE_MODULATION_SOURCE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 52);   /* ViInt32 */
        public const int IVIRFSIGGEN_ATTR_PULSE_MODULATION_EXTERNAL_POLARITY = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 53);   /* ViInt32 */

        /*- IviRFSigGenLFGenerator Extension Group -*/
        public const int IVIRFSIGGEN_ATTR_ACTIVE_LF_GENERATOR = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 101);  /* ViString */
        public const int IVIRFSIGGEN_ATTR_LF_GENERATOR_COUNT = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 104);  /* ViInt32 */
        public const int IVIRFSIGGEN_ATTR_LF_GENERATOR_FREQUENCY = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 102);  /* ViReal64 */
        public const int IVIRFSIGGEN_ATTR_LF_GENERATOR_WAVEFORM = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 103);  /* ViInt32 */

        /*- IviRFSigGenLFGeneratorOutput Extension Group -*/
        public const int IVIRFSIGGEN_ATTR_LF_GENERATOR_OUTPUT_AMPLITUDE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 111);  /* ViReal64 */
        public const int IVIRFSIGGEN_ATTR_LF_GENERATOR_OUTPUT_ENABLED = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 112);  /* ViBoolean */

        /*- IviRFSigGenPulseGenerator Extension Group -*/
        public const int IVIRFSIGGEN_ATTR_PULSE_INTERNAL_TRIGGER_PERIOD = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 121);  /* ViReal64 */
        public const int IVIRFSIGGEN_ATTR_PULSE_WIDTH = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 122);  /* ViReal64 */
        public const int IVIRFSIGGEN_ATTR_PULSE_GATING_ENABLED = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 123);  /* ViBoolean */
        public const int IVIRFSIGGEN_ATTR_PULSE_TRIGGER_SOURCE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 124);  /* ViInt32 */
        public const int IVIRFSIGGEN_ATTR_PULSE_EXTERNAL_TRIGGER_SLOPE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 125);  /* ViInt32 */
        public const int IVIRFSIGGEN_ATTR_PULSE_EXTERNAL_TRIGGER_DELAY = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 126);  /* ViReal64 */

        /*- IviRFSigGenPulseDoubleGenerator Extension Group -*/
        public const int IVIRFSIGGEN_ATTR_PULSE_DOUBLE_ENABLED = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 131);  /* ViBoolean */
        public const int IVIRFSIGGEN_ATTR_PULSE_DOUBLE_DELAY = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 132);  /* ViReal64 */

        /*- IviRFSigGenPulseGeneratorOutput Extension Group -*/
        public const int IVIRFSIGGEN_ATTR_PULSE_OUTPUT_POLARITY = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 141);  /* ViInt32 */
        public const int IVIRFSIGGEN_ATTR_PULSE_OUTPUT_ENABLED = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 142);  /* ViBoolean */

        /*- IviRFSigGenSweep Extension Group -*/
        public const int IVIRFSIGGEN_ATTR_SWEEP_MODE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 201);  /* ViInt32 */
        public const int IVIRFSIGGEN_ATTR_SWEEP_TRIGGER_SOURCE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 202);  /* ViInt32 */

        /*- IviRFSigGenFrequencySweep Extension Group -*/
        public const int IVIRFSIGGEN_ATTR_FREQUENCY_SWEEP_START = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 211);  /* ViReal64 */
        public const int IVIRFSIGGEN_ATTR_FREQUENCY_SWEEP_STOP = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 212);  /* ViReal64 */
        public const int IVIRFSIGGEN_ATTR_FREQUENCY_SWEEP_TIME = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 213);  /* ViReal64 */

        /*- IviRFSigGenPowerSweep Extension Group -*/
        public const int IVIRFSIGGEN_ATTR_POWER_SWEEP_START = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 221);  /* ViReal64 */
        public const int IVIRFSIGGEN_ATTR_POWER_SWEEP_STOP = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 222);  /* ViReal64 */
        public const int IVIRFSIGGEN_ATTR_POWER_SWEEP_TIME = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 223);  /* ViReal64 */

        /*- IviRFSigGenFrequencyStep Extension Group -*/
        public const int IVIRFSIGGEN_ATTR_FREQUENCY_STEP_START = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 241);  /* ViReal64 */
        public const int IVIRFSIGGEN_ATTR_FREQUENCY_STEP_STOP = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 242);  /* ViReal64 */
        public const int IVIRFSIGGEN_ATTR_FREQUENCY_STEP_SCALING = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 243);  /* ViInt32 */
        public const int IVIRFSIGGEN_ATTR_FREQUENCY_STEP_SIZE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 244);  /* ViReal64 */
        public const int IVIRFSIGGEN_ATTR_FREQUENCY_STEP_SINGLE_STEP_ENABLED = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 245);  /* ViBoolean */
        public const int IVIRFSIGGEN_ATTR_FREQUENCY_STEP_DWELL = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 246);  /* ViReal64 */

        /*- IviRFSigGenPowerStep Extension Group -*/
        public const int IVIRFSIGGEN_ATTR_POWER_STEP_START = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 261);  /* ViReal64 */
        public const int IVIRFSIGGEN_ATTR_POWER_STEP_STOP = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 262);  /* ViReal64 */
        public const int IVIRFSIGGEN_ATTR_POWER_STEP_SIZE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 263);  /* ViReal64 */
        public const int IVIRFSIGGEN_ATTR_POWER_STEP_SINGLE_STEP_ENABLED = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 264);  /* ViBoolean */
        public const int IVIRFSIGGEN_ATTR_POWER_STEP_DWELL = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 265);  /* ViReal64 */

        /*- IviRFSigGenList Extension Group -*/
        public const int IVIRFSIGGEN_ATTR_LIST_SELECTED_NAME = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 281);  /* ViString */
        public const int IVIRFSIGGEN_ATTR_LIST_SINGLE_STEP_ENABLED = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 282);  /* ViBoolean */
        public const int IVIRFSIGGEN_ATTR_LIST_DWELL = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 283);  /* ViReal64 */

        /*- IviRFSigGenALC Extension Group -*/
        public const int IVIRFSIGGEN_ATTR_ALC_SOURCE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 301);  /* ViInt32 */
        public const int IVIRFSIGGEN_ATTR_ALC_BANDWIDTH = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 302);  /* ViReal64 */

        /*- IviRFSigGenReferenceOscillator Extension Group -*/
        public const int IVIRFSIGGEN_ATTR_REFERENCE_OSCILLATOR_SOURCE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 321);  /* ViInt32 */
        public const int IVIRFSIGGEN_ATTR_REFERENCE_OSCILLATOR_EXTERNAL_FREQUENCY = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 322);  /* ViReal64 */

        /*- IviRFSigGenModulateIQ Extension Group -*/
        public const int IVIRFSIGGEN_ATTR_IQ_ENABLED = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 401);  /* ViBoolean */
        public const int IVIRFSIGGEN_ATTR_IQ_NOMINAL_VOLTAGE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 402);  /* ViReal64 */
        public const int IVIRFSIGGEN_ATTR_IQ_SOURCE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 403);  /* ViInt32 */
        public const int IVIRFSIGGEN_ATTR_IQ_SWAP_ENABLED = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 404);  /* ViBoolean */

        /*- IviRFSigGenIQImpairment Extension Group -*/
        public const int IVIRFSIGGEN_ATTR_IQ_IMPAIRMENT_ENABLED = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 421);  /* ViBoolean */
        public const int IVIRFSIGGEN_ATTR_IQ_I_OFFSET = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 422);  /* ViReal64 */
        public const int IVIRFSIGGEN_ATTR_IQ_Q_OFFSET = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 423);  /* ViReal64 */
        public const int IVIRFSIGGEN_ATTR_IQ_RATIO = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 424);  /* ViReal64 */
        public const int IVIRFSIGGEN_ATTR_IQ_SKEW = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 425);  /* ViReal64 */

        /*- IviRFSigGenArbGenerator Extension Group -*/
        public const int IVIRFSIGGEN_ATTR_ARB_SELECTED_WAVEFORM = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 451);  /* ViString */
        public const int IVIRFSIGGEN_ATTR_ARB_CLOCK_FREQUENCY = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 452);  /* ViReal64 */
        public const int IVIRFSIGGEN_ATTR_ARB_FILTER_FREQUENCY = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 453);  /* ViReal64 */
        public const int IVIRFSIGGEN_ATTR_ARB_MAX_NUMBER_WAVEFORMS = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 454);  /* ViInt32 */
        public const int IVIRFSIGGEN_ATTR_ARB_WAVEFORM_QUANTUM = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 455);  /* ViInt32 */
        public const int IVIRFSIGGEN_ATTR_ARB_WAVEFORM_SIZE_MIN = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 456);  /* ViInt32 */
        public const int IVIRFSIGGEN_ATTR_ARB_WAVEFORM_SIZE_MAX = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 457);  /* ViInt32 */
        public const int IVIRFSIGGEN_ATTR_ARB_TRIGGER_SOURCE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 458);  /* ViInt32 */
        public const int IVIRFSIGGEN_ATTR_ARB_EXTERNAL_TRIGGER_SLOPE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 459);  /* ViInt32 */

        /*- IviRFSigGenDigitalModulationBase Extension Group -*/
        public const int IVIRFSIGGEN_ATTR_DIGITAL_MODULATION_BASE_STANDARD_COUNT = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 501);  /* ViInt32 */
        public const int IVIRFSIGGEN_ATTR_DIGITAL_MODULATION_BASE_SELECTED_STANDARD = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 502);  /* ViString */
        public const int IVIRFSIGGEN_ATTR_DIGITAL_MODULATION_BASE_DATA_SOURCE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 503);  /* ViInt32 */
        public const int IVIRFSIGGEN_ATTR_DIGITAL_MODULATION_BASE_PRBS_TYPE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 504);  /* ViInt32 */
        public const int IVIRFSIGGEN_ATTR_DIGITAL_MODULATION_BASE_SELECTED_BIT_SEQUENCE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 505);  /* ViString */
        public const int IVIRFSIGGEN_ATTR_DIGITAL_MODULATION_BASE_CLOCK_SOURCE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 506);  /* ViInt32 */
        public const int IVIRFSIGGEN_ATTR_DIGITAL_MODULATION_BASE_EXTERNAL_CLOCK_TYPE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 507);  /* ViInt32 */

        /*- IviRFSigGenCDMABase Extension Group -*/
        public const int IVIRFSIGGEN_ATTR_CDMA_STANDARD_COUNT = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 601);  /* ViInt32 */
        public const int IVIRFSIGGEN_ATTR_CDMA_SELECTED_STANDARD = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 602);  /* ViString */
        public const int IVIRFSIGGEN_ATTR_CDMA_TRIGGER_SOURCE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 603);  /* ViInt32 */
        public const int IVIRFSIGGEN_ATTR_CDMA_EXTERNAL_TRIGGER_SLOPE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 604);  /* ViInt32 */
        public const int IVIRFSIGGEN_ATTR_CDMA_TEST_MODEL_COUNT = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 605);  /* ViInt32 */
        public const int IVIRFSIGGEN_ATTR_CDMA_SELECTED_TEST_MODEL = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 606);  /* ViString */
        public const int IVIRFSIGGEN_ATTR_CDMA_CLOCK_SOURCE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 607);  /* ViInt32 */

        /*- IviRFSigGenTDMABase Extension Group -*/
        public const int IVIRFSIGGEN_ATTR_TDMA_STANDARD_COUNT = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 701);  /* ViInt32 */
        public const int IVIRFSIGGEN_ATTR_TDMA_SELECTED_STANDARD = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 702);  /* ViString */
        public const int IVIRFSIGGEN_ATTR_TDMA_TRIGGER_SOURCE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 703);  /* ViInt32 */
        public const int IVIRFSIGGEN_ATTR_TDMA_EXTERNAL_TRIGGER_SLOPE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 704);  /* ViInt32 */
        public const int IVIRFSIGGEN_ATTR_TDMA_FRAME_COUNT = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 705);  /* ViInt32 */
        public const int IVIRFSIGGEN_ATTR_TDMA_SELECTED_FRAME = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 706);  /* ViString */
        public const int IVIRFSIGGEN_ATTR_TDMA_CLOCK_SOURCE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 707);  /* ViInt32 */
        public const int IVIRFSIGGEN_ATTR_TDMA_EXTERNAL_CLOCK_TYPE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 708);  /* ViInt32 */


        /****************************************************************************
         *----------------- IviRFSigGen Class Attribute Value Defines -----------------*
         ****************************************************************************/
        /*- Defined values for attribute IVIRFSIGGEN_ATTR_AM_SCALING -*/
        public const int IVIRFSIGGEN_VAL_AM_SCALING_LINEAR = (0);
        public const int IVIRFSIGGEN_VAL_AM_SCALING_LOGARITHMIC = (1);
        public const int IVIRFSIGGEN_VAL_AM_SCALING_CLASS_EXT_BASE = (500);
        public const int IVIRFSIGGEN_VAL_AM_SCALING_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for attribute IVIRFSIGGEN_ATTR_AM_EXTERNAL_COUPLING -*/
        public const int IVIRFSIGGEN_VAL_AM_EXTERNAL_COUPLING_AC = (0);
        public const int IVIRFSIGGEN_VAL_AM_EXTERNAL_COUPLING_DC = (1);
        public const int IVIRFSIGGEN_VAL_AM_EXTERNAL_COUPLING_CLASS_EXT_BASE = (500);
        public const int IVIRFSIGGEN_VAL_AM_EXTERNAL_COUPLING_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for attribute IVIRFSIGGEN_ATTR_FM_EXTERNAL_COUPLING -*/
        public const int IVIRFSIGGEN_VAL_FM_EXTERNAL_COUPLING_AC = (0);
        public const int IVIRFSIGGEN_VAL_FM_EXTERNAL_COUPLING_DC = (1);
        public const int IVIRFSIGGEN_VAL_FM_EXTERNAL_COUPLING_CLASS_EXT_BASE = (500);
        public const int IVIRFSIGGEN_VAL_FM_EXTERNAL_COUPLING_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for attribute IVIRFSIGGEN_ATTR_PM_EXTERNAL_COUPLING -*/
        public const int IVIRFSIGGEN_VAL_PM_EXTERNAL_COUPLING_AC = (0);
        public const int IVIRFSIGGEN_VAL_PM_EXTERNAL_COUPLING_DC = (1);
        public const int IVIRFSIGGEN_VAL_PM_EXTERNAL_COUPLING_CLASS_EXT_BASE = (500);
        public const int IVIRFSIGGEN_VAL_PM_EXTERNAL_COUPLING_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for attribute IVIRFSIGGEN_ATTR_PULSE_MODULATION_SOURCE -*/
        public const int IVIRFSIGGEN_VAL_PULSE_MODULATION_SOURCE_INTERNAL = (0);
        public const int IVIRFSIGGEN_VAL_PULSE_MODULATION_SOURCE_EXTERNAL = (1);
        public const int IVIRFSIGGEN_VAL_PULSE_MODULATION_SOURCE_CLASS_EXT_BASE = (500);
        public const int IVIRFSIGGEN_VAL_PULSE_MODULATION_SOURCE_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for attribute IVIRFSIGGEN_ATTR_PULSE_MODULATION_EXTERNAL_POLARITY -*/
        public const int IVIRFSIGGEN_VAL_PULSE_MODULATION_EXTERNAL_POLARITY_NORMAL = (0);
        public const int IVIRFSIGGEN_VAL_PULSE_MODULATION_EXTERNAL_POLARITY_INVERSE = (1);
        public const int IVIRFSIGGEN_VAL_PULSE_MODULATION_EXTERNAL_POLARITY_CLASS_EXT_BASE = (500);
        public const int IVIRFSIGGEN_VAL_PULSE_MODULATION_EXTERNAL_POLARITY_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for attribute IVIRFSIGGEN_ATTR_LF_GENERATOR_WAVEFORM -*/
        public const int IVIRFSIGGEN_VAL_LF_GENERATOR_WAVEFORM_SINE = (0);
        public const int IVIRFSIGGEN_VAL_LF_GENERATOR_WAVEFORM_SQUARE = (1);
        public const int IVIRFSIGGEN_VAL_LF_GENERATOR_WAVEFORM_TRIANGLE = (2);
        public const int IVIRFSIGGEN_VAL_LF_GENERATOR_WAVEFORM_RAMP_UP = (3);
        public const int IVIRFSIGGEN_VAL_LF_GENERATOR_WAVEFORM_RAMP_DOWN = (4);
        public const int IVIRFSIGGEN_VAL_LF_GENERATOR_WAVEFORM_CLASS_EXT_BASE = (500);
        public const int IVIRFSIGGEN_VAL_LF_GENERATOR_WAVEFORM_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for attribute IVIRFSIGGEN_ATTR_PULSE_TRIGGER_SOURCE -*/
        public const int IVIRFSIGGEN_VAL_PULSE_TRIGGER_SOURCE_INTERNAL = (0);
        public const int IVIRFSIGGEN_VAL_PULSE_TRIGGER_SOURCE_EXTERNAL = (1);
        public const int IVIRFSIGGEN_VAL_PULSE_TRIGGER_SOURCE_CLASS_EXT_BASE = (500);
        public const int IVIRFSIGGEN_VAL_PULSE_TRIGGER_SOURCE_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for attribute IVIRFSIGGEN_ATTR_PULSE_EXTERNAL_TRIGGER_SLOPE -*/
        public const int IVIRFSIGGEN_VAL_PULSE_EXTERNAL_TRIGGER_SLOPE_POSITIVE = (0);
        public const int IVIRFSIGGEN_VAL_PULSE_EXTERNAL_TRIGGER_SLOPE_NEGATIVE = (1);
        public const int IVIRFSIGGEN_VAL_PULSE_EXTERNAL_TRIGGER_SLOPE_CLASS_EXT_BASE = (500);
        public const int IVIRFSIGGEN_VAL_PULSE_EXTERNAL_TRIGGER_SLOPE_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for attribute IVIRFSIGGEN_ATTR_PULSE_OUTPUT_POLARITY -*/
        public const int IVIRFSIGGEN_VAL_PULSE_OUTPUT_POLARITY_NORMAL = (0);
        public const int IVIRFSIGGEN_VAL_PULSE_OUTPUT_POLARITY_INVERSE = (1);
        public const int IVIRFSIGGEN_VAL_PULSE_OUTPUT_POLARITY_CLASS_EXT_BASE = (500);
        public const int IVIRFSIGGEN_VAL_PULSE_OUTPUT_POLARITY_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for attribute IVIRFSIGGEN_ATTR_SWEEP_MODE -*/
        public const int IVIRFSIGGEN_VAL_SWEEP_MODE_NONE = (0);
        public const int IVIRFSIGGEN_VAL_SWEEP_MODE_FREQUENCY_SWEEP = (1);
        public const int IVIRFSIGGEN_VAL_SWEEP_MODE_POWER_SWEEP = (2);
        public const int IVIRFSIGGEN_VAL_SWEEP_MODE_FREQUENCY_STEP = (3);
        public const int IVIRFSIGGEN_VAL_SWEEP_MODE_POWER_STEP = (4);
        public const int IVIRFSIGGEN_VAL_SWEEP_MODE_LIST = (5);
        public const int IVIRFSIGGEN_VAL_SWEEP_MODE_CLASS_EXT_BASE = (500);
        public const int IVIRFSIGGEN_VAL_SWEEP_MODE_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for attribute IVIRFSIGGEN_ATTR_SWEEP_TRIGGER_SOURCE -*/
        public const int IVIRFSIGGEN_VAL_SWEEP_TRIGGER_SOURCE_IMMEDIATE = (0);
        public const int IVIRFSIGGEN_VAL_SWEEP_TRIGGER_SOURCE_EXTERNAL = (1);
        public const int IVIRFSIGGEN_VAL_SWEEP_TRIGGER_SOURCE_SOFTWARE = (2);
        public const int IVIRFSIGGEN_VAL_SWEEP_TRIGGER_SOURCE_CLASS_EXT_BASE = (500);
        public const int IVIRFSIGGEN_VAL_SWEEP_TRIGGER_SOURCE_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for attribute IVIRFSIGGEN_ATTR_FREQUENCY_STEP_SCALING -*/
        public const int IVIRFSIGGEN_VAL_FREQUENCY_STEP_SCALING_LINEAR = (0);
        public const int IVIRFSIGGEN_VAL_FREQUENCY_STEP_SCALING_LOGARITHMIC = (1);
        public const int IVIRFSIGGEN_VAL_FREQUENCY_STEP_SCALING_CLASS_EXT_BASE = (500);
        public const int IVIRFSIGGEN_VAL_FREQUENCY_STEP_SCALING_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for attribute IVIRFSIGGEN_ATTR_ALC_SOURCE -*/
        public const int IVIRFSIGGEN_VAL_ALC_SOURCE_INTERNAL = (0);
        public const int IVIRFSIGGEN_VAL_ALC_SOURCE_EXTERNAL = (1);
        public const int IVIRFSIGGEN_VAL_ALC_SOURCE_CLASS_EXT_BASE = (500);
        public const int IVIRFSIGGEN_VAL_ALC_SOURCE_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for attribute IVIRFSIGGEN_ATTR_REFERENCE_OSCILLATOR_SOURCE -*/
        public const int IVIRFSIGGEN_VAL_REFERENCE_OSCILLATOR_SOURCE_INTERNAL = (0);
        public const int IVIRFSIGGEN_VAL_REFERENCE_OSCILLATOR_SOURCE_EXTERNAL = (1);
        public const int IVIRFSIGGEN_VAL_REFERENCE_OSCILLATOR_SOURCE_CLASS_EXT_BASE = (500);
        public const int IVIRFSIGGEN_VAL_REFERENCE_OSCILLATOR_SOURCE_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for attribute IVIRFSIGGEN_ATTR_IQ_SOURCE -*/
        public const int IVIRFSIGGEN_VAL_IQ_SOURCE_DIGITAL_MODULATION_BASE = (0);
        public const int IVIRFSIGGEN_VAL_IQ_SOURCE_CDMA_BASE = (1);
        public const int IVIRFSIGGEN_VAL_IQ_SOURCE_TDMA_BASE = (2);
        public const int IVIRFSIGGEN_VAL_IQ_SOURCE_ARB_GENERATOR = (3);
        public const int IVIRFSIGGEN_VAL_IQ_SOURCE_EXTERNAL = (4);
        public const int IVIRFSIGGEN_VAL_IQ_SOURCE_CLASS_EXT_BASE = (500);
        public const int IVIRFSIGGEN_VAL_IQ_SOURCE_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for attribute IVIRFSIGGEN_ATTR_ARB_TRIGGER_SOURCE -*/
        public const int IVIRFSIGGEN_VAL_ARB_TRIGGER_SOURCE_IMMEDIATE = (0);
        public const int IVIRFSIGGEN_VAL_ARB_TRIGGER_SOURCE_EXTERNAL = (1);
        public const int IVIRFSIGGEN_VAL_ARB_TRIGGER_SOURCE_SOFTWARE = (2);
        public const int IVIRFSIGGEN_VAL_ARB_TRIGGER_SOURCE_CLASS_EXT_BASE = (500);
        public const int IVIRFSIGGEN_VAL_ARB_TRIGGER_SOURCE_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for attribute IVIRFSIGGEN_ATTR_ARB_EXTERNAL_TRIGGER_SLOPE -*/
        public const int IVIRFSIGGEN_VAL_ARB_EXTERNAL_TRIGGER_SLOPE_POSITIVE = (0);
        public const int IVIRFSIGGEN_VAL_ARB_EXTERNAL_TRIGGER_SLOPE_NEGATIVE = (1);
        public const int IVIRFSIGGEN_VAL_ARB_EXTERNAL_TRIGGER_SLOPE_CLASS_EXT_BASE = (500);
        public const int IVIRFSIGGEN_VAL_ARB_EXTERNAL_TRIGGER_SLOPE_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for attribute IVIRFSIGGEN_ATTR_DIGITAL_MODULATION_BASE_DATA_SOURCE -*/
        public const int IVIRFSIGGEN_VAL_DIGITAL_MODULATION_BASE_DATA_SOURCE_EXTERNAL = (0);
        public const int IVIRFSIGGEN_VAL_DIGITAL_MODULATION_BASE_DATA_SOURCE_PRBS = (1);
        public const int IVIRFSIGGEN_VAL_DIGITAL_MODULATION_BASE_DATA_SOURCE_BIT_SEQUENCE = (2);
        public const int IVIRFSIGGEN_VAL_DIGITAL_MODULATION_BASE_DATA_SOURCE_CLASS_EXT_BASE = (500);
        public const int IVIRFSIGGEN_VAL_DIGITAL_MODULATION_BASE_DATA_SOURCE_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for attribute IVIRFSIGGEN_ATTR_DIGITAL_MODULATION_BASE_PRBS_TYPE -*/
        public const int IVIRFSIGGEN_VAL_DIGITAL_MODULATION_BASE_PRBS_TYPE_PRBS9 = (0);
        public const int IVIRFSIGGEN_VAL_DIGITAL_MODULATION_BASE_PRBS_TYPE_PRBS11 = (1);
        public const int IVIRFSIGGEN_VAL_DIGITAL_MODULATION_BASE_PRBS_TYPE_PRBS15 = (2);
        public const int IVIRFSIGGEN_VAL_DIGITAL_MODULATION_BASE_PRBS_TYPE_PRBS16 = (3);
        public const int IVIRFSIGGEN_VAL_DIGITAL_MODULATION_BASE_PRBS_TYPE_PRBS20 = (4);
        public const int IVIRFSIGGEN_VAL_DIGITAL_MODULATION_BASE_PRBS_TYPE_PRBS21 = (5);
        public const int IVIRFSIGGEN_VAL_DIGITAL_MODULATION_BASE_PRBS_TYPE_PRBS23 = (6);
        public const int IVIRFSIGGEN_VAL_DIGITAL_MODULATION_BASE_PRBS_TYPE_CLASS_EXT_BASE = (500);
        public const int IVIRFSIGGEN_VAL_DIGITAL_MODULATION_BASE_PRBS_TYPE_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for attribute IVIRFSIGGEN_ATTR_DIGITAL_MODULATION_BASE_CLOCK_SOURCE -*/
        public const int IVIRFSIGGEN_VAL_DIGITAL_MODULATION_BASE_CLOCK_SOURCE_INTERNAL = (0);
        public const int IVIRFSIGGEN_VAL_DIGITAL_MODULATION_BASE_CLOCK_SOURCE_EXTERNAL = (1);
        public const int IVIRFSIGGEN_VAL_DIGITAL_MODULATION_BASE_CLOCK_SOURCE_CLASS_EXT_BASE = (500);
        public const int IVIRFSIGGEN_VAL_DIGITAL_MODULATION_BASE_CLOCK_SOURCE_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for attribute IVIRFSIGGEN_ATTR_DIGITAL_MODULATION_BASE_EXTERNAL_CLOCK_TYPE -*/
        public const int IVIRFSIGGEN_VAL_DIGITAL_MODULATION_BASE_EXTERNAL_CLOCK_TYPE_BIT = (0);
        public const int IVIRFSIGGEN_VAL_DIGITAL_MODULATION_BASE_EXTERNAL_CLOCK_TYPE_SYMBOL = (1);
        public const int IVIRFSIGGEN_VAL_DIGITAL_MODULATION_BASE_EXTERNAL_CLOCK_TYPE_CLASS_EXT_BASE = (500);
        public const int IVIRFSIGGEN_VAL_DIGITAL_MODULATION_BASE_EXTERNAL_CLOCK_TYPE_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for attribute IVIRFSIGGEN_ATTR_CDMA_TRIGGER_SOURCE -*/
        public const int IVIRFSIGGEN_VAL_CDMA_TRIGGER_SOURCE_IMMEDIATE = (0);
        public const int IVIRFSIGGEN_VAL_CDMA_TRIGGER_SOURCE_EXTERNAL = (1);
        public const int IVIRFSIGGEN_VAL_CDMA_TRIGGER_SOURCE_SOFTWARE = (2);
        public const int IVIRFSIGGEN_VAL_CDMA_TRIGGER_SOURCE_CLASS_EXT_BASE = (500);
        public const int IVIRFSIGGEN_VAL_CDMA_TRIGGER_SOURCE_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for attribute IVIRFSIGGEN_ATTR_CDMA_EXTERNAL_TRIGGER_SLOPE -*/
        public const int IVIRFSIGGEN_VAL_CDMA_EXTERNAL_TRIGGER_SLOPE_POSITIVE = (0);
        public const int IVIRFSIGGEN_VAL_CDMA_EXTERNAL_TRIGGER_SLOPE_NEGATIVE = (1);
        public const int IVIRFSIGGEN_VAL_CDMA_EXTERNAL_TRIGGER_SLOPE_CLASS_EXT_BASE = (500);
        public const int IVIRFSIGGEN_VAL_CDMA_EXTERNAL_TRIGGER_SLOPE_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for attribute IVIRFSIGGEN_ATTR_CDMA_CLOCK_SOURCE -*/
        public const int IVIRFSIGGEN_VAL_CDMA_CLOCK_SOURCE_INTERNAL = (0);
        public const int IVIRFSIGGEN_VAL_CDMA_CLOCK_SOURCE_EXTERNAL = (1);
        public const int IVIRFSIGGEN_VAL_CDMA_CLOCK_SOURCE_CLASS_EXT_BASE = (500);
        public const int IVIRFSIGGEN_VAL_CDMA_CLOCK_SOURCE_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for attribute IVIRFSIGGEN_ATTR_TDMA_TRIGGER_SOURCE -*/
        public const int IVIRFSIGGEN_VAL_TDMA_TRIGGER_SOURCE_IMMEDIATE = (0);
        public const int IVIRFSIGGEN_VAL_TDMA_TRIGGER_SOURCE_EXTERNAL = (1);
        public const int IVIRFSIGGEN_VAL_TDMA_TRIGGER_SOURCE_SOFTWARE = (2);
        public const int IVIRFSIGGEN_VAL_TDMA_TRIGGER_SOURCE_CLASS_EXT_BASE = (500);
        public const int IVIRFSIGGEN_VAL_TDMA_TRIGGER_SOURCE_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for attribute IVIRFSIGGEN_ATTR_TDMA_EXTERNAL_TRIGGER_SLOPE -*/
        public const int IVIRFSIGGEN_VAL_TDMA_EXTERNAL_TRIGGER_SLOPE_POSITIVE = (0);
        public const int IVIRFSIGGEN_VAL_TDMA_EXTERNAL_TRIGGER_SLOPE_NEGATIVE = (1);
        public const int IVIRFSIGGEN_VAL_TDMA_EXTERNAL_TRIGGER_SLOPE_CLASS_EXT_BASE = (500);
        public const int IVIRFSIGGEN_VAL_TDMA_EXTERNAL_TRIGGER_SLOPE_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for attribute IVIRFSIGGEN_ATTR_TDMA_CLOCK_SOURCE -*/
        public const int IVIRFSIGGEN_VAL_TDMA_CLOCK_SOURCE_INTERNAL = (0);
        public const int IVIRFSIGGEN_VAL_TDMA_CLOCK_SOURCE_EXTERNAL = (1);
        public const int IVIRFSIGGEN_VAL_TDMA_CLOCK_SOURCE_CLASS_EXT_BASE = (500);
        public const int IVIRFSIGGEN_VAL_TDMA_CLOCK_SOURCE_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for attribute IVIRFSIGGEN_ATTR_TDMA_EXTERNAL_CLOCK_TYPE -*/
        public const int IVIRFSIGGEN_VAL_TDMA_EXTERNAL_CLOCK_TYPE_BIT = (0);
        public const int IVIRFSIGGEN_VAL_TDMA_EXTERNAL_CLOCK_TYPE_SYMBOL = (1);
        public const int IVIRFSIGGEN_VAL_TDMA_EXTERNAL_CLOCK_TYPE_CLASS_EXT_BASE = (500);
        public const int IVIRFSIGGEN_VAL_TDMA_EXTERNAL_CLOCK_TYPE_SPECIFIC_EXT_BASE = (1000);
    }
}
