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
    public class IviScopeAttribute
    {
        /*- IviScope Fundamental Attributes -*/
        /*- Channel Subsystem -*/
        public const int IVISCOPE_ATTR_CHANNEL_COUNT = IviDriverAttribute.IVI_ATTR_CHANNEL_COUNT;             /* ViInt32,  read-only */
        public const int IVISCOPE_ATTR_VERTICAL_RANGE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 1);   /* ViReal64,  Multi-Channel */
        public const int IVISCOPE_ATTR_VERTICAL_OFFSET = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 2);   /* ViReal64,  Multi-Channel */
        public const int IVISCOPE_ATTR_VERTICAL_COUPLING = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 3);   /* ViInt32,   Multi-Channel */
        public const int IVISCOPE_ATTR_PROBE_ATTENUATION = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 4);   /* ViReal64,  Multi-Channel */
        public const int IVISCOPE_ATTR_CHANNEL_ENABLED = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 5);   /* ViBoolean, Multi-Channel */
        public const int IVISCOPE_ATTR_MAX_INPUT_FREQUENCY = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 6);   /* ViReal64,  Multi-Channel */
        public const int IVISCOPE_ATTR_INPUT_IMPEDANCE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 103); /* ViReal64,  Multi-Channel */

        /*- Acquisition Subsystem -*/
        public const int IVISCOPE_ATTR_ACQUISITION_TYPE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 101); /* ViInt32    */
        public const int IVISCOPE_ATTR_ACQUISITION_START_TIME = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 109); /* ViReal64  */
        public const int IVISCOPE_ATTR_HORZ_TIME_PER_RECORD = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 7);   /* ViReal64  */
        public const int IVISCOPE_ATTR_HORZ_RECORD_LENGTH = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 8);   /* ViInt32, Read-only  */
        public const int IVISCOPE_ATTR_HORZ_MIN_NUM_PTS = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 9);   /* ViInt32   */
        public const int IVISCOPE_ATTR_HORZ_SAMPLE_RATE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 10);  /* ViReal64, Read-only */

        /*- Triggering Subsystem -*/
        public const int IVISCOPE_ATTR_TRIGGER_TYPE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 12);  /* ViInt32   */
        public const int IVISCOPE_ATTR_TRIGGER_SOURCE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 13);  /* ViString  */
        public const int IVISCOPE_ATTR_TRIGGER_COUPLING = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 14);  /* ViInt32   */
        public const int IVISCOPE_ATTR_TRIGGER_HOLDOFF = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 16);  /* ViReal64  */

        /*- Edge Triggering Attributes -*/
        public const int IVISCOPE_ATTR_TRIGGER_LEVEL = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 17);  /* ViReal64  */
        public const int IVISCOPE_ATTR_TRIGGER_SLOPE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 18);  /* ViInt32   */

        /*- IviScope Extended Attributes -*/
        /*- IviScopeTVTrigger Extension Group -*/
        public const int IVISCOPE_ATTR_TV_TRIGGER_SIGNAL_FORMAT = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 201); /* ViInt32   */
        public const int IVISCOPE_ATTR_TV_TRIGGER_EVENT = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 205); /* ViInt32   */
        public const int IVISCOPE_ATTR_TV_TRIGGER_LINE_NUMBER = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 206); /* ViInt32   */
        public const int IVISCOPE_ATTR_TV_TRIGGER_POLARITY = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 204); /* ViInt32   */

        /*- IviScopeRuntTrigger Extension Group -*/
        public const int IVISCOPE_ATTR_RUNT_HIGH_THRESHOLD = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 301); /* ViReal64  */
        public const int IVISCOPE_ATTR_RUNT_LOW_THRESHOLD = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 302); /* ViReal64  */
        public const int IVISCOPE_ATTR_RUNT_POLARITY = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 303); /* ViInt32   */

        /*- IviScopeGlitchTrigger Extension Group -*/
        public const int IVISCOPE_ATTR_GLITCH_WIDTH = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 401); /* ViReal64  */
        public const int IVISCOPE_ATTR_GLITCH_POLARITY = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 402); /* ViInt32   */
        public const int IVISCOPE_ATTR_GLITCH_CONDITION = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 403); /* ViInt32   */

        /*- IviScopeWidthTrigger Extension Group -*/
        public const int IVISCOPE_ATTR_WIDTH_LOW_THRESHOLD = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 501); /* ViReal64  */
        public const int IVISCOPE_ATTR_WIDTH_HIGH_THRESHOLD = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 502); /* ViReal64  */
        public const int IVISCOPE_ATTR_WIDTH_POLARITY = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 503); /* ViInt32   */
        public const int IVISCOPE_ATTR_WIDTH_CONDITION = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 504); /* ViInt32   */

        /*- IviScopeAcLineTrigger Extension Group -*/
        public const int IVISCOPE_ATTR_AC_LINE_TRIGGER_SLOPE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 701); /* ViInt32   */

        /*- IviScopeMinMaxWaveform Extension Group -*/
        public const int IVISCOPE_ATTR_NUM_ENVELOPES = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 105); /* ViInt32   */

        /*- IviScopeWaveformMeas Extension Group -*/
        public const int IVISCOPE_ATTR_MEAS_HIGH_REF = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 607); /* ViReal64, Percentage */
        public const int IVISCOPE_ATTR_MEAS_LOW_REF = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 608); /* ViReal64, Percentage */
        public const int IVISCOPE_ATTR_MEAS_MID_REF = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 609); /* ViReal64, Percentage */

        /*- IviScope Trigger Modifier Extension Group -*/
        public const int IVISCOPE_ATTR_TRIGGER_MODIFIER = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 102); /* ViInt32   */

        /*- IviScope Average Acquisition Extension Group -*/
        public const int IVISCOPE_ATTR_NUM_AVERAGES = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 104); /* ViInt32   */

        /*- IviScope Sample Mode Extension Group -*/
        public const int IVISCOPE_ATTR_SAMPLE_MODE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 106); /* ViInt32, R/O  */

        /*- IviScope Continuous Acquisition Extension Group -*/
        public const int IVISCOPE_ATTR_INITIATE_CONTINUOUS = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 107); /* ViBoolean */

        /*- IviScope Probe Auto Sense Extension Group -*/
        public const int IVISCOPE_ATTR_PROBE_SENSE_VALUE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 108); /* ViReal64, R/O */

        /*- IviScope Interpolation Extension Group -*/
        public const int IVISCOPE_ATTR_INTERPOLATION = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 19); /* ViInt32   */

        /*****************************************************************************
         *------ IviScope Class Function Parameter and Attribute Value Defines ------*
         *****************************************************************************/

        /*- Defined values for maxTime parameter to the waveform acquisition and measurement functions -*/
        // public const int IVISCOPE_VAL_MAX_TIME_INFINITE     =     IviDriverAttribute.IVI_VAL_MAX_TIME_INFINITE;
        // public const int IVISCOPE_VAL_MAX_TIME_IMMEDIATE    =     IviDriverAttribute.IVI_VAL_MAX_TIME_IMMEDIATE;

        /*- Defined values for the status parameter of the IviScope_AcquisitionStatus function -*/
        public const int IVISCOPE_VAL_ACQ_COMPLETE = (1);
        public const int IVISCOPE_VAL_ACQ_IN_PROGRESS = (0);
        public const int IVISCOPE_VAL_ACQ_STATUS_UNKNOWN = (-1);

        /*- Defined values for the measurementFunction parameter of the IviScope_ReadWaveformMeasurment function -*/
        public const int IVISCOPE_VAL_RISE_TIME = (0);
        public const int IVISCOPE_VAL_FALL_TIME = (1);
        public const int IVISCOPE_VAL_FREQUENCY = (2);
        public const int IVISCOPE_VAL_PERIOD = (3);
        public const int IVISCOPE_VAL_VOLTAGE_RMS = (4);
        public const int IVISCOPE_VAL_VOLTAGE_PEAK_TO_PEAK = (5);
        public const int IVISCOPE_VAL_VOLTAGE_MAX = (6);
        public const int IVISCOPE_VAL_VOLTAGE_MIN = (7);
        public const int IVISCOPE_VAL_VOLTAGE_HIGH = (8);
        public const int IVISCOPE_VAL_VOLTAGE_LOW = (9);
        public const int IVISCOPE_VAL_VOLTAGE_AVERAGE = (10);
        public const int IVISCOPE_VAL_WIDTH_NEG = (11);
        public const int IVISCOPE_VAL_WIDTH_POS = (12);
        public const int IVISCOPE_VAL_DUTY_CYCLE_NEG = (13);
        public const int IVISCOPE_VAL_DUTY_CYCLE_POS = (14);
        public const int IVISCOPE_VAL_AMPLITUDE = (15);
        public const int IVISCOPE_VAL_VOLTAGE_CYCLE_RMS = (16);
        public const int IVISCOPE_VAL_VOLTAGE_CYCLE_AVERAGE = (17);
        public const int IVISCOPE_VAL_OVERSHOOT = (18);
        public const int IVISCOPE_VAL_PRESHOOT = (19);

        public const int IVISCOPE_VAL_MEASUREMENT_FUNCTION_CLASS_EXT_BASE = (100);
        public const int IVISCOPE_VAL_MEASUREMENT_FUNCTION_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for IVISCOPE_ATTR_VERTICAL_COUPLING -*/
        public const int IVISCOPE_VAL_AC = (0);
        public const int IVISCOPE_VAL_DC = (1);
        public const int IVISCOPE_VAL_GND = (2);
        public const int IVISCOPE_VAL_VERTICAL_COUPLING_CLASS_EXT_BASE = (100);
        public const int IVISCOPE_VAL_VERTICAL_COUPLING_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for IVISCOPE_ATTR_TRIGGER_TYPE -*/
        public const int IVISCOPE_VAL_EDGE_TRIGGER = (1);
        public const int IVISCOPE_VAL_WIDTH_TRIGGER = (2);
        public const int IVISCOPE_VAL_RUNT_TRIGGER = (3);
        public const int IVISCOPE_VAL_GLITCH_TRIGGER = (4);
        public const int IVISCOPE_VAL_TV_TRIGGER = (5);
        public const int IVISCOPE_VAL_IMMEDIATE_TRIGGER = (6);
        public const int IVISCOPE_VAL_AC_LINE_TRIGGER = (7);
        public const int IVISCOPE_VAL_TRIGGER_TYPE_CLASS_EXT_BASE = (200);
        public const int IVISCOPE_VAL_TRIGGER_TYPE_SPECIFIC_EXT_BASE = (1000);


        /*- Defined values for IVISCOPE_ATTR_TRIGGER_SLOPE -*/
        public const int IVISCOPE_VAL_POSITIVE = (1);
        public const int IVISCOPE_VAL_NEGATIVE = (0);

        /*- Defined values for IVISCOPE_ATTR_TRIGGER_SOURCE -*/
        public const string IVISCOPE_VAL_EXTERNAL = "VAL_EXTERNAL";
        public const string IVISCOPE_VAL_TTL0 = "VAL_TTL0";
        public const string IVISCOPE_VAL_TTL1 = "VAL_TTL1";
        public const string IVISCOPE_VAL_TTL2 = "VAL_TTL2";
        public const string IVISCOPE_VAL_TTL3 = "VAL_TTL3";
        public const string IVISCOPE_VAL_TTL4 = "VAL_TTL4";
        public const string IVISCOPE_VAL_TTL5 = "VAL_TTL5";
        public const string IVISCOPE_VAL_TTL6 = "VAL_TTL6";
        public const string IVISCOPE_VAL_TTL7 = "VAL_TTL7";
        public const string IVISCOPE_VAL_ECL0 = "VAL_ECL0";
        public const string IVISCOPE_VAL_ECL1 = "VAL_ECL1";
        public const string IVISCOPE_VAL_PXI_STAR = "VAL_PXI_STAR";
        public const string IVISCOPE_VAL_RTSI_0 = "VAL_RTSI_0";
        public const string IVISCOPE_VAL_RTSI_1 = "VAL_RTSI_1";
        public const string IVISCOPE_VAL_RTSI_2 = "VAL_RTSI_2";
        public const string IVISCOPE_VAL_RTSI_3 = "VAL_RTSI_3";
        public const string IVISCOPE_VAL_RTSI_4 = "VAL_RTSI_4";
        public const string IVISCOPE_VAL_RTSI_5 = "VAL_RTSI_5";
        public const string IVISCOPE_VAL_RTSI_6 = "VAL_RTSI_6";
        /*  
            In addition to the above defines, 
            IVISCOPE_ATTR_TRIGGER_SOURCE accpets any defined 
            channel name or string representation of a channel number
        */

        /*- Defined extended values for IVISCOPE_ATTR_PROBE_ATTENUATION -*/
        public const int IVISCOPE_VAL_PROBE_SENSE_ON = (-1);

        public const int IVISCOPE_VAL_PROBE_ATTENUATION_CLASS_EXT_BASE = (-100);
        public const int IVISCOPE_VAL_PROBE_ATTENUATION_SPECIFIC_EXT_BASE = (-1000);

        /*- Defined values for IVISCOPE_ATTR_TRIGGER_COUPLING -*/
        /* public const int IVISCOPE_VAL_AC                      DEFINED ABOVE */
        /* public const int IVISCOPE_VAL_DC                      DEFINED ABOVE */
        public const int IVISCOPE_VAL_HF_REJECT = (3);
        public const int IVISCOPE_VAL_LF_REJECT = (4);
        public const int IVISCOPE_VAL_NOISE_REJECT = (5);

        public const int IVISCOPE_VAL_TRIGGER_COUPLING_CLASS_EXT_BASE = (100);
        public const int IVISCOPE_VAL_TRIGGER_COUPLING_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for IVISCOPE_ATTR_INTERPOLATION -*/
        public const int IVISCOPE_VAL_NO_INTERPOLATION = (1);
        public const int IVISCOPE_VAL_SINE_X = (2);
        public const int IVISCOPE_VAL_LINEAR = (3);
        public const int IVISCOPE_VAL_INTERPOLATION_CLASS_EXT_BASE = (100);
        public const int IVISCOPE_VAL_INTERPOLATION_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for IVISCOPE_ATTR_TV_TRIGGER_SIGNAL_FORMAT -*/
        public const int IVISCOPE_VAL_NTSC = (1);
        public const int IVISCOPE_VAL_PAL = (2);
        public const int IVISCOPE_VAL_SECAM = (3);
        public const int IVISCOPE_VAL_TV_SIGNAL_FORMAT_CLASS_EXT_BASE = (100);
        public const int IVISCOPE_VAL_TV_SIGNAL_FORMAT_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for IVISCOPE_ATTR_TV_TRIGGER_EVENT -*/
        public const int IVISCOPE_VAL_TV_EVENT_FIELD1 = (1);
        public const int IVISCOPE_VAL_TV_EVENT_FIELD2 = (2);
        public const int IVISCOPE_VAL_TV_EVENT_ANY_FIELD = (3);
        public const int IVISCOPE_VAL_TV_EVENT_ANY_LINE = (4);
        public const int IVISCOPE_VAL_TV_EVENT_LINE_NUMBER = (5);
        public const int IVISCOPE_VAL_TV_TRIGGER_EVENT_CLASS_EXT_BASE = (100);
        public const int IVISCOPE_VAL_TV_TRIGGER_EVENT_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for IVISCOPE_ATTR_TV_TRIGGER_POLARITY -*/
        public const int IVISCOPE_VAL_TV_POSITIVE = (1);
        public const int IVISCOPE_VAL_TV_NEGATIVE = (2);
        public const int IVISCOPE_VAL_TV_TRIGGER_POLARITY_CLASS_EXT_BASE = (100);
        public const int IVISCOPE_VAL_TV_TRIGGER_POLARITY_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for IVISCOPE_ATTR_RUNT_POLARITY -*/
        public const int IVISCOPE_VAL_RUNT_POSITIVE = (1);
        public const int IVISCOPE_VAL_RUNT_NEGATIVE = (2);
        public const int IVISCOPE_VAL_RUNT_EITHER = (3);

        /*- Defined values for IVISCOPE_ATTR_GLITCH_POLARITY -*/
        public const int IVISCOPE_VAL_GLITCH_POSITIVE = (1);
        public const int IVISCOPE_VAL_GLITCH_NEGATIVE = (2);
        public const int IVISCOPE_VAL_GLITCH_EITHER = (3);

        /*- Defined values for IVISCOPE_ATTR_GLITCH_CONDITION -*/
        public const int IVISCOPE_VAL_GLITCH_LESS_THAN = (1);
        public const int IVISCOPE_VAL_GLITCH_GREATER_THAN = (2);

        /*- Defined values for IVISCOPE_ATTR_WIDTH_POLARITY -*/
        public const int IVISCOPE_VAL_WIDTH_POSITIVE = (1);
        public const int IVISCOPE_VAL_WIDTH_NEGATIVE = (2);
        public const int IVISCOPE_VAL_WIDTH_EITHER = (3);

        /*- Defined values for IVISCOPE_ATTR_WIDTH_CONDITION -*/
        public const int IVISCOPE_VAL_WIDTH_WITHIN = (1);
        public const int IVISCOPE_VAL_WIDTH_OUTSIDE = (2);

        /*- Defined values for IVISCOPE_ATTR_AC_LINE_TRIGGER_SLOPE -*/
        public const int IVISCOPE_VAL_AC_LINE_POSITIVE = (1);
        public const int IVISCOPE_VAL_AC_LINE_NEGATIVE = (2);
        public const int IVISCOPE_VAL_AC_LINE_EITHER = (3);

        /*- Defined values for IVISCOPE_ATTR_ACQUISITION_TYPE -*/
        public const int IVISCOPE_VAL_NORMAL = (0);
        public const int IVISCOPE_VAL_PEAK_DETECT = (1);
        public const int IVISCOPE_VAL_HI_RES = (2);
        public const int IVISCOPE_VAL_ENVELOPE = (3);
        public const int IVISCOPE_VAL_AVERAGE = (4);

        public const int IVISCOPE_VAL_ACQUISITION_TYPE_CLASS_EXT_BASE = (100);
        public const int IVISCOPE_VAL_ACQUISITION_TYPE_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for IVISCOPE_ATTR_TRIGGER_MODIFIER -*/
        public const int IVISCOPE_VAL_NO_TRIGGER_MOD = (1);
        public const int IVISCOPE_VAL_AUTO = (2);
        public const int IVISCOPE_VAL_AUTO_LEVEL = (3);

        public const int IVISCOPE_VAL_TRIGGER_MOD_CLASS_EXT_BASE = (100);
        public const int IVISCOPE_VAL_TRIGGER_MOD_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for IVISCOPE_ATTR_SAMPLE_MODE  */
        public const int IVISCOPE_VAL_REAL_TIME = (0);
        public const int IVISCOPE_VAL_EQUIVALENT_TIME = (1);
    }
}
