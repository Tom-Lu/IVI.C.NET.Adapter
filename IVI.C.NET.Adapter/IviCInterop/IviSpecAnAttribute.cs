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
    public class IviSpecAnAttribute
    {
        /*- IviSpecAn Fundamental Attributes -*/
        public const int IVISPECAN_ATTR_AMPLITUDE_UNITS = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 1);    /* ViInt32 */
        public const int IVISPECAN_ATTR_ATTENUATION = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 2);    /* ViReal64 */
        public const int IVISPECAN_ATTR_ATTENUATION_AUTO = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 3);    /* ViBoolean */
        public const int IVISPECAN_ATTR_DETECTOR_TYPE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 4);    /* ViInt32 */
        public const int IVISPECAN_ATTR_DETECTOR_TYPE_AUTO = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 5);    /* ViBoolean */
        public const int IVISPECAN_ATTR_FREQUENCY_START = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 6);    /* ViReal64 */
        public const int IVISPECAN_ATTR_FREQUENCY_STOP = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 7);    /* ViReal64 */
        public const int IVISPECAN_ATTR_FREQUENCY_OFFSET = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 8);    /* ViReal64 */
        public const int IVISPECAN_ATTR_INPUT_IMPEDANCE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 9);    /* ViReal64 */
        public const int IVISPECAN_ATTR_NUMBER_OF_SWEEPS = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 10);   /* ViInt32 */
        public const int IVISPECAN_ATTR_REFERENCE_LEVEL = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 11);   /* ViReal64 */
        public const int IVISPECAN_ATTR_REFERENCE_LEVEL_OFFSET = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 12);   /* ViReal64 */
        public const int IVISPECAN_ATTR_RESOLUTION_BANDWIDTH = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 13);   /* ViReal64 */
        public const int IVISPECAN_ATTR_RESOLUTION_BANDWIDTH_AUTO = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 14);   /* ViBoolean */
        public const int IVISPECAN_ATTR_SWEEP_MODE_CONTINUOUS = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 15);   /* ViBoolean */
        public const int IVISPECAN_ATTR_SWEEP_TIME = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 16);   /* ViReal64 */
        public const int IVISPECAN_ATTR_SWEEP_TIME_AUTO = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 17);   /* ViBoolean */
        public const int IVISPECAN_ATTR_TRACE_COUNT = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 18);   /* ViInt32 */
        public const int IVISPECAN_ATTR_TRACE_SIZE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 19);   /* ViInt32 */
        public const int IVISPECAN_ATTR_TRACE_TYPE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 20);   /* ViInt32 */
        public const int IVISPECAN_ATTR_VERTICAL_SCALE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 21);   /* ViInt32 */
        public const int IVISPECAN_ATTR_VIDEO_BANDWIDTH = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 22);   /* ViReal64 */
        public const int IVISPECAN_ATTR_VIDEO_BANDWIDTH_AUTO = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 23);   /* ViBoolean */

        /*- IviSpecAn Extended Attributes -*/
        /*- IviSpecAnMarker Extension Group -*/
        public const int IVISPECAN_ATTR_ACTIVE_MARKER = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 201);  /* ViString */
        public const int IVISPECAN_ATTR_MARKER_AMPLITUDE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 202);  /* ViReal64 */
        public const int IVISPECAN_ATTR_MARKER_COUNT = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 203);  /* ViInt32 */
        public const int IVISPECAN_ATTR_MARKER_ENABLED = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 204);  /* ViBoolean */
        public const int IVISPECAN_ATTR_MARKER_FREQUENCY_COUNTER_ENABLED = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 205);  /* ViBoolean */
        public const int IVISPECAN_ATTR_MARKER_FREQUENCY_COUNTER_RESOLUTION = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 206);  /* ViReal64 */
        public const int IVISPECAN_ATTR_MARKER_POSITION = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 207);  /* ViReal64 */
        public const int IVISPECAN_ATTR_MARKER_THRESHOLD = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 208);  /* ViReal64 */
        public const int IVISPECAN_ATTR_MARKER_TRACE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 209);  /* ViString */
        public const int IVISPECAN_ATTR_PEAK_EXCURSION = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 210);  /* ViReal64 */
        public const int IVISPECAN_ATTR_SIGNAL_TRACK_ENABLED = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 211);  /* ViBoolean */

        /*- IviSpecAnTrigger Extension Group -*/
        public const int IVISPECAN_ATTR_TRIGGER_SOURCE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 301);  /* ViInt32 */

        /*- IviSpecAnExternalTrigger Extension Group -*/
        public const int IVISPECAN_ATTR_EXTERNAL_TRIGGER_LEVEL = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 401);  /* ViReal64 */
        public const int IVISPECAN_ATTR_EXTERNAL_TRIGGER_SLOPE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 402);  /* ViInt32 */

        /*- IviSpecAnVideoTrigger Extension Group -*/
        public const int IVISPECAN_ATTR_VIDEO_TRIGGER_LEVEL = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 501);  /* ViReal64 */
        public const int IVISPECAN_ATTR_VIDEO_TRIGGER_SLOPE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 502);  /* ViInt32 */

        /*- IviSpecAnDisplay Extension Group -*/
        public const int IVISPECAN_ATTR_NUMBER_OF_DIVISIONS = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 602);  /* ViInt32 */
        public const int IVISPECAN_ATTR_UNITS_PER_DIVISION = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 601);  /* ViReal64 */

        /*- IviSpecAnMarkerType Extension Group -*/
        public const int IVISPECAN_ATTR_MARKER_TYPE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 701);  /* ViInt32 */

        /*- IviSpecAnDeltaMarker Extension Group -*/
        public const int IVISPECAN_ATTR_REFERENCE_MARKER_AMPLITUDE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 801);  /* ViReal64 */
        public const int IVISPECAN_ATTR_REFERENCE_MARKER_POSITION = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 802);  /* ViReal64 */

        /*- IviSpecAnExternalMixer Extension Group -*/
        public const int IVISPECAN_ATTR_EXTERNAL_MIXER_AVERAGE_CONVERSION_LOSS = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 901);  /* ViReal64 */
        public const int IVISPECAN_ATTR_EXTERNAL_MIXER_BIAS = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 902);  /* ViReal64 */
        public const int IVISPECAN_ATTR_EXTERNAL_MIXER_BIAS_ENABLED = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 903);  /* ViBoolean */
        public const int IVISPECAN_ATTR_EXTERNAL_MIXER_BIAS_LIMIT = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 904);  /* ViReal64 */
        public const int IVISPECAN_ATTR_EXTERNAL_MIXER_CONVERSION_LOSS_TABLE_ENABLED = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 905);  /* ViBoolean */
        public const int IVISPECAN_ATTR_EXTERNAL_MIXER_ENABLED = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 906);  /* ViBoolean */
        public const int IVISPECAN_ATTR_EXTERNAL_MIXER_HARMONIC = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 907);  /* ViInt32 */
        public const int IVISPECAN_ATTR_EXTERNAL_MIXER_NUMBER_OF_PORTS = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 908);  /* ViInt32 */


        /****************************************************************************
         *----------------- IviSpecAn Class Attribute Value Defines -----------------*
         ****************************************************************************/
        /*- Defined values for attribute IVISPECAN_ATTR_AMPLITUDE_UNITS -*/
        public const int IVISPECAN_VAL_AMPLITUDE_UNITS_DBM = (1);
        public const int IVISPECAN_VAL_AMPLITUDE_UNITS_DBMV = (2);
        public const int IVISPECAN_VAL_AMPLITUDE_UNITS_DBUV = (3);
        public const int IVISPECAN_VAL_AMPLITUDE_UNITS_VOLT = (4);
        public const int IVISPECAN_VAL_AMPLITUDE_UNITS_WATT = (5);
        public const int IVISPECAN_VAL_AMPLITUDE_UNITS_CLASS_EXT_BASE = (500);
        public const int IVISPECAN_VAL_AMPLITUDE_UNITS_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for attribute IVISPECAN_ATTR_DETECTOR_TYPE -*/
        public const int IVISPECAN_VAL_DETECTOR_TYPE_AUTO_PEAK = (1);
        public const int IVISPECAN_VAL_DETECTOR_TYPE_AVERAGE = (2);
        public const int IVISPECAN_VAL_DETECTOR_TYPE_MAX_PEAK = (3);
        public const int IVISPECAN_VAL_DETECTOR_TYPE_MIN_PEAK = (4);
        public const int IVISPECAN_VAL_DETECTOR_TYPE_SAMPLE = (5);
        public const int IVISPECAN_VAL_DETECTOR_TYPE_RMS = (6);
        public const int IVISPECAN_VAL_DETECTOR_TYPE_CLASS_EXT_BASE = (500);
        public const int IVISPECAN_VAL_DETECTOR_TYPE_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for attribute IVISPECAN_ATTR_TRACE_TYPE -*/
        public const int IVISPECAN_VAL_TRACE_TYPE_CLEAR_WRITE = (1);
        public const int IVISPECAN_VAL_TRACE_TYPE_MAX_HOLD = (2);
        public const int IVISPECAN_VAL_TRACE_TYPE_MIN_HOLD = (3);
        public const int IVISPECAN_VAL_TRACE_TYPE_VIDEO_AVERAGE = (4);
        public const int IVISPECAN_VAL_TRACE_TYPE_VIEW = (5);
        public const int IVISPECAN_VAL_TRACE_TYPE_STORE = (6);
        public const int IVISPECAN_VAL_TRACE_TYPE_CLASS_EXT_BASE = (500);
        public const int IVISPECAN_VAL_TRACE_TYPE_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for attribute IVISPECAN_ATTR_VERTICAL_SCALE -*/
        public const int IVISPECAN_VAL_VERTICAL_SCALE_LINEAR = (1);
        public const int IVISPECAN_VAL_VERTICAL_SCALE_LOGARITHMIC = (2);
        public const int IVISPECAN_VAL_VERTICAL_SCALE_CLASS_EXT_BASE = (500);
        public const int IVISPECAN_VAL_VERTICAL_SCALE_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for attribute IVISPECAN_ATTR_TRIGGER_SOURCE -*/
        public const int IVISPECAN_VAL_TRIGGER_SOURCE_EXTERNAL = (1);
        public const int IVISPECAN_VAL_TRIGGER_SOURCE_IMMEDIATE = (2);
        public const int IVISPECAN_VAL_TRIGGER_SOURCE_SOFTWARE = (3);
        public const int IVISPECAN_VAL_TRIGGER_SOURCE_AC_LINE = (4);
        public const int IVISPECAN_VAL_TRIGGER_SOURCE_VIDEO = (5);
        public const int IVISPECAN_VAL_TRIGGER_SOURCE_CLASS_EXT_BASE = (500);
        public const int IVISPECAN_VAL_TRIGGER_SOURCE_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for attribute IVISPECAN_ATTR_EXTERNAL_TRIGGER_SLOPE -*/
        public const int IVISPECAN_VAL_EXTERNAL_TRIGGER_SLOPE_POSITIVE = (1);
        public const int IVISPECAN_VAL_EXTERNAL_TRIGGER_SLOPE_NEGATIVE = (2);
        public const int IVISPECAN_VAL_EXTERNAL_TRIGGER_SLOPE_CLASS_EXT_BASE = (500);
        public const int IVISPECAN_VAL_EXTERNAL_TRIGGER_SLOPE_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for attribute IVISPECAN_ATTR_VIDEO_TRIGGER_SLOPE -*/
        public const int IVISPECAN_VAL_VIDEO_TRIGGER_SLOPE_POSITIVE = (1);
        public const int IVISPECAN_VAL_VIDEO_TRIGGER_SLOPE_NEGATIVE = (2);
        public const int IVISPECAN_VAL_VIDEO_TRIGGER_SLOPE_CLASS_EXT_BASE = (500);
        public const int IVISPECAN_VAL_VIDEO_TRIGGER_SLOPE_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for attribute IVISPECAN_ATTR_MARKER_TYPE -*/
        public const int IVISPECAN_VAL_MARKER_TYPE_NORMAL = (1);
        public const int IVISPECAN_VAL_MARKER_TYPE_DELTA = (2);
        public const int IVISPECAN_VAL_MARKER_TYPE_CLASS_EXT_BASE = (500);
        public const int IVISPECAN_VAL_MARKER_TYPE_SPECIFIC_EXT_BASE = (1000);



        /****************************************************************************
         *------------- IviSpecAn Function Parameter Value Definitions -------------*
         ****************************************************************************/
        /*- Defined values for status parameter of function -*/
        /*- IviSpecAn_AcquisitionStatus -*/
        public const int IVISPECAN_VAL_ACQUISITION_STATUS_COMPLETE = (1);
        public const int IVISPECAN_VAL_ACQUISITION_STATUS_IN_PROGRESS = (0);
        public const int IVISPECAN_VAL_ACQUISITION_STATUS_UNKNOWN = (-1);

        /*- Defined values for MaxTime parameter of function -*/
        /*- IviSpecAn_ReadYTrace -*/
        public const int IVISPECAN_VAL_MAX_TIME_IMMEDIATE = (0x0);
        public const uint IVISPECAN_VAL_MAX_TIME_INFINITE = (0xFFFFFFFFU);

        /*- Defined values for SearchType parameter of function -*/
        /*- IviSpecAn_MarkerSearch -*/
        public const int IVISPECAN_VAL_MARKER_SEARCH_HIGHEST = (1);
        public const int IVISPECAN_VAL_MARKER_SEARCH_MINIMUM = (2);
        public const int IVISPECAN_VAL_MARKER_SEARCH_NEXT_PEAK = (3);
        public const int IVISPECAN_VAL_MARKER_SEARCH_NEXT_PEAK_LEFT = (4);
        public const int IVISPECAN_VAL_MARKER_SEARCH_NEXT_PEAK_RIGHT = (5);
        public const int IVISPECAN_VAL_MARKER_SEARCH_CLASS_EXT_BASE = (500);
        public const int IVISPECAN_VAL_MARKER_SEARCH_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for InstrumentSetting parameter of function -*/
        /*- IviSpecAn_SetInstrumentFromMarker -*/
        public const int IVISPECAN_VAL_INSTRUMENT_SETTING_FREQUENCY_CENTER = (1);
        public const int IVISPECAN_VAL_INSTRUMENT_SETTING_FREQUENCY_SPAN = (2);
        public const int IVISPECAN_VAL_INSTRUMENT_SETTING_FREQUENCY_START = (3);
        public const int IVISPECAN_VAL_INSTRUMENT_SETTING_FREQUENCY_STOP = (4);
        public const int IVISPECAN_VAL_INSTRUMENT_SETTING_REFERENCE_LEVEL = (5);
        public const int IVISPECAN_VAL_INSTRUMENT_SETTING_CLASS_EXT_BASE = (500);
        public const int IVISPECAN_VAL_INSTRUMENT_SETTING_SPECIFIC_EXT_BASE = (1000);
    }
}
 