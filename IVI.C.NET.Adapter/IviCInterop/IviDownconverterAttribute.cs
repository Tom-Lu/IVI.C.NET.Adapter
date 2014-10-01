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
    public class IviDownconverterAttribute
    {
        /*- IviDownconverter Fundamental Attributes -*/
        public const int IVIDOWNCONVERTER_ATTR_IF_OUTPUT_COUNT = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 4);    /* ViInt32,  read-only */
        public const int IVIDOWNCONVERTER_ATTR_RF_INPUT_FREQUENCY = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 12);   /* ViReal64 */
        public const int IVIDOWNCONVERTER_ATTR_RF_INPUT_COUPLING = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 10);	 /* ViInt32  */
        public const int IVIDOWNCONVERTER_ATTR_RF_INPUT_COUNT = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 11);	 /* ViInt32,  read-only */
        public const int IVIDOWNCONVERTER_ATTR_ACTIVE_RF_INPUT = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 1);	 /* ViString */
        public const int IVIDOWNCONVERTER_ATTR_ACTIVE_IF_OUTPUT = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 0);	 /* ViString */
        public const int IVIDOWNCONVERTER_ATTR_IF_OUTPUT_FREQUENCY = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 6);	 /* ViReal64, read-only */
        public const int IVIDOWNCONVERTER_ATTR_IF_OUTPUT_ENABLED = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 5);	 /* ViBoolean*/
        public const int IVIDOWNCONVERTER_ATTR_IF_OUTPUT_GAIN = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 7);	 /* ViReal64 */
        public const int IVIDOWNCONVERTER_ATTR_RF_INPUT_ATTENUATION = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 9);	 /* ViReal64 */
        public const int IVIDOWNCONVERTER_ATTR_RF_INPUT_CORRECTIONS_ENABLED = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 340);	 /* ViBoolean*/
        public const int IVIDOWNCONVERTER_ATTR_IS_SETTLED = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 8);	 /* ViBoolean,read-only */
        public const int IVIDOWNCONVERTER_ATTR_EXTERNAL_LO_ENABLED = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 2);	 /* ViBoolean*/
        public const int IVIDOWNCONVERTER_ATTR_EXTERNAL_LO_FREQUENCY = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 3);	 /* ViReal64 */

        /*- IviDownconverterBypass Attributes -*/
        public const int IVIDOWNCONVERTER_ATTR_BYPASS = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 100);	 /* ViBoolean*/

        /*- IviDownconverterExternalMixer Attributes -*/
        public const int IVIDOWNCONVERTER_ATTR_EXTERNAL_MIXER_ENABLED = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 110);	 /*	ViBoolean*/
        public const int IVIDOWNCONVERTER_ATTR_EXTERNAL_MIXER_BIAS_LEVEL = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 111);	 /* ViReal64 */
        public const int IVIDOWNCONVERTER_ATTR_EXTERNAL_MIXER_BIAS_LIMIT = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 112);	 /* ViReal64 */
        public const int IVIDOWNCONVERTER_ATTR_EXTERNAL_MIXER_BIAS_ENABLED = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 113);	 /* ViBoolean*/
        public const int IVIDOWNCONVERTER_ATTR_EXTERNAL_MIXER_HARMONIC = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 114);	 /* ViInt32  */
        public const int IVIDOWNCONVERTER_ATTR_EXTERNAL_MIXER_NUMBER_OF_PORTS = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 115);	 /* ViInt32  */

        /*- IviDownconverterFrequencyStep Attributes -*/
        public const int IVIDOWNCONVERTER_ATTR_FREQUENCY_STEP_START = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 204);	 /* ViReal64 */
        public const int IVIDOWNCONVERTER_ATTR_FREQUENCY_STEP_STOP = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 205);	 /* ViReal64 */
        public const int IVIDOWNCONVERTER_ATTR_FREQUENCY_STEP_SIZE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 203);	 /* ViReal64 */
        public const int IVIDOWNCONVERTER_ATTR_FREQUENCY_STEP_DWELL = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 200);	 /* ViReal64 */
        public const int IVIDOWNCONVERTER_ATTR_FREQUENCY_STEP_SINGLE_STEP_ENABLED = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 202);	 /* ViBoolean*/
        public const int IVIDOWNCONVERTER_ATTR_FREQUENCY_STEP_SCALING = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 201);	 /* ViInt32  */

        /*- IviDownconverterFrequencySweep Attributes -*/
        public const int IVIDOWNCONVERTER_ATTR_FREQUENCY_SWEEP_START = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 211);  /* ViReal64 */
        public const int IVIDOWNCONVERTER_ATTR_FREQUENCY_SWEEP_STOP = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 212);	 /* ViReal64 */
        public const int IVIDOWNCONVERTER_ATTR_FREQUENCY_SWEEP_TIME = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 213);	 /* ViReal64 */
        public const int IVIDOWNCONVERTER_ATTR_FREQUENCY_SWEEP_TRIGGER_SOURCE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 214);	 /* ViString */
        public const int IVIDOWNCONVERTER_ATTR_FREQUENCY_SWEEP_MODE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 210);	 /* ViInt32  */
        public const int IVIDOWNCONVERTER_ATTR_IS_SWEEPING = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 215);	 /* ViBoolean,read-only */

        /*- IviDownconverterFrequencySweepList Attributes -*/
        public const int IVIDOWNCONVERTER_ATTR_FREQUENCY_SWEEP_LIST_SELECTED_NAME = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 221);	 /* ViString */
        public const int IVIDOWNCONVERTER_ATTR_FREQUENCY_SWEEP_LIST_DWELL = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 220);	 /* ViReal64 */
        public const int IVIDOWNCONVERTER_ATTR_FREQUENCY_SWEEP_LIST_SINGLE_STEP_ENABLED = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 222);	 /* ViBoolean*/

        /*- IviDownconverterBandCrossingInformation Attributes -*/
        public const int IVIDOWNCONVERTER_ATTR_NUM_BANDS = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 300);	 /* ViInt32,  read-only */

        /*- IviDownconverterIFFilter Attributes -*/
        public const int IVIDOWNCONVERTER_ATTR_IF_OUTPUT_FILTER_BANDWIDTH = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 310);	 /* ViReal64 */

        /*- IviDownconverterPreselector Attributes -*/
        public const int IVIDOWNCONVERTER_ATTR_PRESELECTOR_ENABLED = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 320);	 /* ViBoolean*/

        /*- IviDownconverterVideoDetectorBandwidth Attributes -*/
        public const int IVIDOWNCONVERTER_ATTR_IF_OUTPUT_VIDEO_DETECTOR_BANDWIDTH = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 330);	 /* ViReal64 */

        /*- IviDownconverterReferenceOscillator Attributes -*/
        public const int IVIDOWNCONVERTER_ATTR_REFERENCE_OSCILLATOR_EXTERNAL_FREQUENCY = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 341);  /* ViReal64 */
        public const int IVIDOWNCONVERTER_ATTR_REFERENCE_OSCILLATOR_SOURCE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 342);  /* ViInt32  */
        public const int IVIDOWNCONVERTER_ATTR_REFERENCE_OSCILLATOR_OUTPUT_ENABLED = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 343);	 /* ViBoolean*/

        /****************************************************************************
         *--------------- IviDownconverter Class Attribute Value Defines -----------*
         ****************************************************************************/

        /*- Defined values for attribute IVIDOWNCONVERTER_ATTR_FREQUENCY_STEP_SCALING -*/
        public const int IVIDOWNCONVERTER_VAL_FREQUENCY_STEP_SCALING_LINEAR = (0);
        public const int IVIDOWNCONVERTER_VAL_FREQUENCY_STEP_SCALING_LOGARITHMIC = (1);

        public const int IVIDOWNCONVERTER_VAL_FREQUENCY_STEP_SCALING_CLASS_EXT_BASE = (100);
        public const int IVIDOWNCONVERTER_VAL_FREQUENCY_STEP_SCALING_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for attribute IVIDOWNCONVERTER_ATTR_FREQUENCY_SWEEP_MODE -*/
        public const int IVIDOWNCONVERTER_VAL_FREQUENCY_SWEEP_MODE_NONE = (0);
        public const int IVIDOWNCONVERTER_VAL_FREQUENCY_SWEEP_MODE_SWEEP = (1);
        public const int IVIDOWNCONVERTER_VAL_FREQUENCY_SWEEP_MODE_STEP = (2);
        public const int IVIDOWNCONVERTER_VAL_FREQUENCY_SWEEP_MODE_LIST = (3);

        public const int IVIDOWNCONVERTER_VAL_FREQUENCY_SWEEP_MODE_CLASS_EXT_BASE = (100);
        public const int IVIDOWNCONVERTER_VAL_FREQUENCY_SWEEP_MODE_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for attribute IVIDOWNCONVERTER_ATTR_FREQUENCY_SWEEP_TRIGGER_SOURCE -*/
        public const string IVIDOWNCONVERTER_VAL_TRIGGER_SOURCE_NONE = "";
        public const string IVIDOWNCONVERTER_VAL_TRIGGER_SOURCE_IMMEDIATE = "Immediate";
        public const string IVIDOWNCONVERTER_VAL_TRIGGER_SOURCE_EXTERNAL = "External";
        public const string IVIDOWNCONVERTER_VAL_TRIGGER_SOURCE_stringERNAL = "stringernal";
        public const string IVIDOWNCONVERTER_VAL_TRIGGER_SOURCE_SOFTWARE = "Software";
        public const string IVIDOWNCONVERTER_VAL_TRIGGER_SOURCE_LAN0 = "LAN0";
        public const string IVIDOWNCONVERTER_VAL_TRIGGER_SOURCE_LAN1 = "LAN1";
        public const string IVIDOWNCONVERTER_VAL_TRIGGER_SOURCE_LAN2 = "LAN2";
        public const string IVIDOWNCONVERTER_VAL_TRIGGER_SOURCE_LAN3 = "LAN3";
        public const string IVIDOWNCONVERTER_VAL_TRIGGER_SOURCE_LAN4 = "LAN4";
        public const string IVIDOWNCONVERTER_VAL_TRIGGER_SOURCE_LAN5 = "LAN5";
        public const string IVIDOWNCONVERTER_VAL_TRIGGER_SOURCE_LAN6 = "LAN6";
        public const string IVIDOWNCONVERTER_VAL_TRIGGER_SOURCE_LAN7 = "LAN7";
        public const string IVIDOWNCONVERTER_VAL_TRIGGER_SOURCE_LXI0 = "LXI0";
        public const string IVIDOWNCONVERTER_VAL_TRIGGER_SOURCE_LXI1 = "LXI1";
        public const string IVIDOWNCONVERTER_VAL_TRIGGER_SOURCE_LXI2 = "LXI2";
        public const string IVIDOWNCONVERTER_VAL_TRIGGER_SOURCE_LXI3 = "LXI3";
        public const string IVIDOWNCONVERTER_VAL_TRIGGER_SOURCE_LXI4 = "LXI4";
        public const string IVIDOWNCONVERTER_VAL_TRIGGER_SOURCE_LXI5 = "LXI5";
        public const string IVIDOWNCONVERTER_VAL_TRIGGER_SOURCE_LXI6 = "LXI6";
        public const string IVIDOWNCONVERTER_VAL_TRIGGER_SOURCE_LXI7 = "LXI7";
        public const string IVIDOWNCONVERTER_VAL_TRIGGER_SOURCE_TTL0 = "TTL0";
        public const string IVIDOWNCONVERTER_VAL_TRIGGER_SOURCE_TTL1 = "TTL1";
        public const string IVIDOWNCONVERTER_VAL_TRIGGER_SOURCE_TTL2 = "TTL2";
        public const string IVIDOWNCONVERTER_VAL_TRIGGER_SOURCE_TTL3 = "TTL3";
        public const string IVIDOWNCONVERTER_VAL_TRIGGER_SOURCE_TTL4 = "TTL4";
        public const string IVIDOWNCONVERTER_VAL_TRIGGER_SOURCE_TTL5 = "TTL5";
        public const string IVIDOWNCONVERTER_VAL_TRIGGER_SOURCE_TTL6 = "TTL6";
        public const string IVIDOWNCONVERTER_VAL_TRIGGER_SOURCE_TTL7 = "TTL7";
        public const string IVIDOWNCONVERTER_VAL_TRIGGER_SOURCE_PXI_STAR = "PXI_STAR";
        public const string IVIDOWNCONVERTER_VAL_TRIGGER_SOURCE_PXI_TRIG0 = "PXI_TRIG0";
        public const string IVIDOWNCONVERTER_VAL_TRIGGER_SOURCE_PXI_TRIG1 = "PXI_TRIG1";
        public const string IVIDOWNCONVERTER_VAL_TRIGGER_SOURCE_PXI_TRIG2 = "PXI_TRIG2";
        public const string IVIDOWNCONVERTER_VAL_TRIGGER_SOURCE_PXI_TRIG3 = "PXI_TRIG3";
        public const string IVIDOWNCONVERTER_VAL_TRIGGER_SOURCE_PXI_TRIG4 = "PXI_TRIG4";
        public const string IVIDOWNCONVERTER_VAL_TRIGGER_SOURCE_PXI_TRIG5 = "PXI_TRIG5";
        public const string IVIDOWNCONVERTER_VAL_TRIGGER_SOURCE_PXI_TRIG6 = "PXI_TRIG6";
        public const string IVIDOWNCONVERTER_VAL_TRIGGER_SOURCE_PXI_TRIG7 = "PXI_TRIG7";
        public const string IVIDOWNCONVERTER_VAL_TRIGGER_SOURCE_PXIE_DSTARA = "PXIe_DSTARA";
        public const string IVIDOWNCONVERTER_VAL_TRIGGER_SOURCE_PXIE_DSTARB = "PXIe_DSTARB";
        public const string IVIDOWNCONVERTER_VAL_TRIGGER_SOURCE_PXIE_DSTARC = "PXIe_DSTARC";
        public const string IVIDOWNCONVERTER_VAL_TRIGGER_SOURCE_RTSI0 = "RTSI0";
        public const string IVIDOWNCONVERTER_VAL_TRIGGER_SOURCE_RTSI1 = "RTSI1";
        public const string IVIDOWNCONVERTER_VAL_TRIGGER_SOURCE_RTSI2 = "RTSI2";
        public const string IVIDOWNCONVERTER_VAL_TRIGGER_SOURCE_RTSI3 = "RTSI3";
        public const string IVIDOWNCONVERTER_VAL_TRIGGER_SOURCE_RTSI4 = "RTSI4";
        public const string IVIDOWNCONVERTER_VAL_TRIGGER_SOURCE_RTSI5 = "RTSI5";
        public const string IVIDOWNCONVERTER_VAL_TRIGGER_SOURCE_RTSI6 = "RTSI6";

        /*- Defined values for attribute IVIDOWNCONVERTER_ATTR_REFERENCE_OSCILLATOR_SOURCE -*/
        public const int IVIDOWNCONVERTER_VAL_REFERENCE_OSCILLATOR_SOURCE_INTERNAL = (0);
        public const int IVIDOWNCONVERTER_VAL_REFERENCE_OSCILLATOR_SOURCE_EXTERNAL = (1);

        public const int IVIDOWNCONVERTER_VAL_REFERENCE_OSCILLATOR_SOURCE_CLASS_EXT_BASE = (100);
        public const int IVIDOWNCONVERTER_VAL_REFERENCE_OSCILLATOR_SOURCE_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for attribute IVIDOWNCONVERTER_ATTR_RF_INPUT_COUPLING -*/
        public const int IVIDOWNCONVERTER_VAL_INPUT_COUPLING_AC = (0);
        public const int IVIDOWNCONVERTER_VAL_INPUT_COUPLING_DC = (1);

        public const int IVIDOWNCONVERTER_VAL_RF_INPUT_COUPLING_CLASS_EXT_BASE = (100);
        public const int IVIDOWNCONVERTER_VAL_RF_INPUT_COUPLING_SPECIFIC_EXT_BASE = (1000);

        public const int IVIDOWNCONVERTER_VAL_CALIBRATION_COMPLETE = (0);
        public const int IVIDOWNCONVERTER_VAL_CALIBRATION_IN_PROGRESS = (1);
        public const int IVIDOWNCONVERTER_VAL_CALIBRATION_STATUS_UNKNOWN = (2);
        public const int IVIDOWNCONVERTER_VAL_CALIBRATION_FAILED = (3);

        public const int IVIDOWNCONVERTER_VAL_CALIBRATED = (0);
        public const int IVIDOWNCONVERTER_VAL_UNCALIBRATED = (1);
        public const int IVIDOWNCONVERTER_VAL_CALIBRATED_STATUS_UNKNOWN = (2);

        public const int IVIDOWNCONVERTER_VAL_MAX_TIME_IMMEDIATE = (IviDriverAttribute.IVI_VAL_MAX_TIME_IMMEDIATE);
        public const int IVIDOWNCONVERTER_VAL_MAX_TIME_INFINITE = (IviDriverAttribute.IVI_VAL_MAX_TIME_INFINITE);

    }
}
