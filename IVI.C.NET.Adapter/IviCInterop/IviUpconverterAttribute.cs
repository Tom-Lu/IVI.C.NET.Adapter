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
    public class IviUpconverterAttribute
    {
        /*- IviUpconverter Fundamental Attributes -*/
        public const int IVIUPCONVERTER_ATTR_ACTIVE_IF_INPUT = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 0);    /* ViString */
        public const int IVIUPCONVERTER_ATTR_ACTIVE_RF_OUTPUT = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 1);    /* ViString */
        public const int IVIUPCONVERTER_ATTR_ALC_ENABLED = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 2);	 /* ViBoolean*/
        public const int IVIUPCONVERTER_ATTR_AUTO_CORRECTIONS_ENABLED = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 3);	 /* ViBoolean*/
        public const int IVIUPCONVERTER_ATTR_EXTERNAL_LO_ENABLED = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 4);	 /* ViBoolean*/
        public const int IVIUPCONVERTER_ATTR_EXTERNAL_LO_FREQUENCY = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 5);	 /* ViReal64 */
        public const int IVIUPCONVERTER_ATTR_IF_INPUT_ATTENUATION = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 6);	 /* ViReal64 */
        public const int IVIUPCONVERTER_ATTR_IF_INPUT_COUNT = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 7);	 /* ViInt32,  read-only */
        public const int IVIUPCONVERTER_ATTR_IF_INPUT_COUPLING = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 8);	 /* ViInt32  */
        public const int IVIUPCONVERTER_ATTR_IF_INPUT_FREQUENCY = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 9);	 /* ViReal64, read-only */
        public const int IVIUPCONVERTER_ATTR_IS_READY = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 10);	 /* ViBoolean,read-only */
        public const int IVIUPCONVERTER_ATTR_RF_OUTPUT_BANDWIDTH = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 11);	 /* ViReal64 */
        public const int IVIUPCONVERTER_ATTR_RF_OUTPUT_COUNT = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 12);	 /* ViInt32,  read-only */
        public const int IVIUPCONVERTER_ATTR_RF_OUTPUT_ENABLED = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 13);	 /* ViBoolean*/
        public const int IVIUPCONVERTER_ATTR_RF_OUTPUT_FREQUENCY = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 14);	 /* ViReal64 */

        /*- IviUpconverterOutputGain Attributes -*/
        public const int IVIUPCONVERTER_ATTR_RF_OUTPUT_GAIN = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 100);	 /* ViReal64 */

        /*- IviUpconverterOutputPowerLevel Attributes -*/
        public const int IVIUPCONVERTER_ATTR_RF_OUTPUT_LEVEL = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 110);	 /* ViReal64 */

        /*- IviUpconverterModulateAM Attributes -*/
        public const int IVIUPCONVERTER_ATTR_AM_DEPTH = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 120);	 /*	ViReal64 */
        public const int IVIUPCONVERTER_ATTR_AM_ENABLED = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 121);	 /* ViBoolean*/
        public const int IVIUPCONVERTER_ATTR_AM_EXTERNAL_COUPLING = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 122);	 /* ViInt32  */
        public const int IVIUPCONVERTER_ATTR_AM_NOMINAL_VOLTAGE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 123);	 /* ViReal64, read-only */
        public const int IVIUPCONVERTER_ATTR_AM_SCALING = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 124);	 /* ViInt32  */
        public const int IVIUPCONVERTER_ATTR_AM_SOURCE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 125);	 /* ViString */

        /*- IviUpconverterModulateFM Attributes -*/
        public const int IVIUPCONVERTER_ATTR_FM_DEVIATION = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 130);	 /* ViReal64 */
        public const int IVIUPCONVERTER_ATTR_FM_ENABLED = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 131);	 /* ViBoolean*/
        public const int IVIUPCONVERTER_ATTR_FM_EXTERNAL_COUPLING = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 132);	 /* ViInt32  */
        public const int IVIUPCONVERTER_ATTR_FM_NOMINAL_VOLTAGE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 133);	 /* ViReal64, read-only */
        public const int IVIUPCONVERTER_ATTR_FM_SOURCE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 134);	 /* ViString */

        /*- IviUpconverterModulatePM Attributes -*/
        public const int IVIUPCONVERTER_ATTR_PM_DEVIATION = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 140);	 /* ViReal64 */
        public const int IVIUPCONVERTER_ATTR_PM_ENABLED = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 141);	 /* ViBoolean*/
        public const int IVIUPCONVERTER_ATTR_PM_EXTERNAL_COUPLING = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 142);	 /* ViInt32  */
        public const int IVIUPCONVERTER_ATTR_PM_NOMINAL_VOLTAGE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 143);	 /* ViReal64, read-only */
        public const int IVIUPCONVERTER_ATTR_PM_SOURCE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 144);	 /* ViString */

        /*- IviUpconverterAnalogModulationSource Attributes -*/
        public const int IVIUPCONVERTER_ATTR_ANALOG_MODULATION_SOURCE_COUNT = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 150);  /* ViInt32,  read-only */

        /*- IviUpconverterModulatePulse Attributes -*/
        public const int IVIUPCONVERTER_ATTR_PULSE_MODULATION_ENABLED = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 160);	 /* ViBoolean*/
        public const int IVIUPCONVERTER_ATTR_PULSE_MODULATION_EXTERNAL_POLARITY = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 161);	 /* ViInt32  */

        /*- IviUpconverterBypass Attributes -*/
        public const int IVIUPCONVERTER_ATTR_BYPASS = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 170);	 /* ViBoolean*/

        /*- IviUpconverterOutputReadyTrigger Attributes -*/
        public const int IVIUPCONVERTER_ATTR_RF_OUTPUT_READY_TRIGGER = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 180);	 /* ViString */

        /*- IviUpconverterSweep Attributes -*/
        public const int IVIUPCONVERTER_ATTR_IS_SWEEPING = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 200);	 /* ViBoolean,read-only */
        public const int IVIUPCONVERTER_ATTR_SWEEP_MODE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 201);	 /* ViInt32  */
        public const int IVIUPCONVERTER_ATTR_SWEEP_TRIGGER_SOURCE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 202);  /* ViString */

        /*- IviUpconverterFrequencySweep Attributes -*/
        public const int IVIUPCONVERTER_ATTR_FREQUENCY_SWEEP_START = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 210);	 /* ViReal64 */
        public const int IVIUPCONVERTER_ATTR_FREQUENCY_SWEEP_STOP = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 211);	 /* ViReal64 */
        public const int IVIUPCONVERTER_ATTR_FREQUENCY_SWEEP_TIME = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 212);	 /* ViReal64 */

        /*- IviUpconverterPowerSweep Attributes -*/
        public const int IVIUPCONVERTER_ATTR_POWER_SWEEP_START = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 220);	 /* ViReal64 */
        public const int IVIUPCONVERTER_ATTR_POWER_SWEEP_STOP = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 221);  /* ViReal64 */
        public const int IVIUPCONVERTER_ATTR_POWER_SWEEP_TIME = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 222);	 /* ViReal64 */

        /*- IviUpconverterGainSweep Attributes -*/
        public const int IVIUPCONVERTER_ATTR_GAIN_SWEEP_START = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 230);	 /* ViReal64 */
        public const int IVIUPCONVERTER_ATTR_GAIN_SWEEP_STOP = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 231);  /* ViReal64 */
        public const int IVIUPCONVERTER_ATTR_GAIN_SWEEP_TIME = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 232);	 /* ViReal64 */

        /*- IviUpconverterFrequencyStep Attributes -*/
        public const int IVIUPCONVERTER_ATTR_FREQUENCY_STEP_DWELL = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 240);	 /* ViReal64 */
        public const int IVIUPCONVERTER_ATTR_FREQUENCY_STEP_SCALING = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 241);	 /* ViInt32  */
        public const int IVIUPCONVERTER_ATTR_FREQUENCY_STEP_SINGLE_STEP_ENABLED = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 242);	 /* ViBoolean*/
        public const int IVIUPCONVERTER_ATTR_FREQUENCY_STEP_SIZE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 243);	 /* ViReal64 */
        public const int IVIUPCONVERTER_ATTR_FREQUENCY_STEP_START = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 244);	 /* ViReal64 */
        public const int IVIUPCONVERTER_ATTR_FREQUENCY_STEP_STOP = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 245);	 /* ViReal64 */

        /*- IviUpconverterPowerStep Attributes -*/
        public const int IVIUPCONVERTER_ATTR_POWER_STEP_DWELL = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 250);	 /* ViReal64 */
        public const int IVIUPCONVERTER_ATTR_POWER_STEP_SINGLE_STEP_ENABLED = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 251);	 /* ViBoolean*/
        public const int IVIUPCONVERTER_ATTR_POWER_STEP_SIZE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 252);	 /* ViReal64 */
        public const int IVIUPCONVERTER_ATTR_POWER_STEP_START = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 253);	 /* ViReal64 */
        public const int IVIUPCONVERTER_ATTR_POWER_STEP_STOP = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 254);	 /* ViReal64 */

        /*- IviUpconverterGainStep Attributes -*/
        public const int IVIUPCONVERTER_ATTR_GAIN_STEP_DWELL = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 260);	 /* ViReal64 */
        public const int IVIUPCONVERTER_ATTR_GAIN_STEP_SINGLE_STEP_ENABLED = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 261);	 /* ViBoolean*/
        public const int IVIUPCONVERTER_ATTR_GAIN_STEP_SIZE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 262);	 /* ViReal64 */
        public const int IVIUPCONVERTER_ATTR_GAIN_STEP_START = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 263);	 /* ViReal64 */
        public const int IVIUPCONVERTER_ATTR_GAIN_STEP_STOP = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 264);	 /* ViReal64 */

        /*- IviUpconverterList Attributes -*/
        public const int IVIUPCONVERTER_ATTR_LIST_DWELL = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 270);	 /* ViReal64 */
        public const int IVIUPCONVERTER_ATTR_LIST_SELECTED_NAME = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 271);  /* ViString */
        public const int IVIUPCONVERTER_ATTR_LIST_SINGLE_STEP_ENABLED = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 272);	 /* ViBoolean*/

        /*- IviUpconverterALC Attributes -*/
        public const int IVIUPCONVERTER_ATTR_ALC_BANDWIDTH = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 300);	 /* ViReal64 */
        public const int IVIUPCONVERTER_ATTR_ALC_SOURCE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 301);	 /* ViInt32  */

        /*- IviUpconverterAttenuatorHold Attributes -*/
        public const int IVIUPCONVERTER_ATTR_ATTENUATOR_HOLD_ENABLED = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 310);	 /* ViBoolean*/

        /*- IviUpconverterReferenceOscillator Attributes -*/
        public const int IVIUPCONVERTER_ATTR_REFERENCE_OSCILLATOR_EXTERNAL_FREQUENCY = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 320);  /* ViReal64 */
        public const int IVIUPCONVERTER_ATTR_REFERENCE_OSCILLATOR_SOURCE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 321);  /* ViInt32  */
        public const int IVIUPCONVERTER_ATTR_REFERENCE_OSCILLATOR_OUTPUT_ENABLED = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 322);	 /* ViBoolean*/

        /*- IviUpconverterModulateIQ Attributes -*/
        public const int IVIUPCONVERTER_ATTR_IQ_ENABLED = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 330);	 /* ViBoolean*/
        public const int IVIUPCONVERTER_ATTR_IQ_NOMINAL_VOLTAGE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 331);	 /* ViReal64, read-only */
        public const int IVIUPCONVERTER_ATTR_IQ_SWAP_ENABLED = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 332);	 /* ViBoolean*/

        /*- IviUpconverterIQImpairment Attributes -*/
        public const int IVIUPCONVERTER_ATTR_IQ_IMPAIRMENT_ENABLED = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 340);	 /* ViBoolean*/
        public const int IVIUPCONVERTER_ATTR_IQ_IMPAIRMENT_I_OFFSET = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 341);	 /* ViReal64 */
        public const int IVIUPCONVERTER_ATTR_IQ_IMPAIRMENT_Q_OFFSET = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 342);	 /* ViReal64 */
        public const int IVIUPCONVERTER_ATTR_IQ_IMPAIRMENT_RATIO = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 343);	 /* ViReal64 */
        public const int IVIUPCONVERTER_ATTR_IQ_IMPAIRMENT_SKEW = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 344);	 /* ViReal64 */

        /****************************************************************************
         *--------------- IviUpconverter Class Attribute Value Defines -------------*
         ****************************************************************************/

        /*- Defined values for attribute IVIUPCONVERTER_ATTR_ALC_SOURCE -*/
        public const int IVIUPCONVERTER_VAL_ALC_SOURCE_INTERNAL = (0);
        public const int IVIUPCONVERTER_VAL_ALC_SOURCE_EXTERNAL = (1);

        public const int IVIUPCONVERTER_VAL_ALC_SOURCE_CLASS_EXT_BASE = (100);
        public const int IVIUPCONVERTER_VAL_ALC_SOURCE_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for attribute IVIUPCONVERTER_ATTR_AM_EXTERNAL_COUPLING -*/
        public const int IVIUPCONVERTER_VAL_AM_EXTERNAL_COUPLING_AC = (0);
        public const int IVIUPCONVERTER_VAL_AM_EXTERNAL_COUPLING_DC = (1);

        public const int IVIUPCONVERTER_VAL_AM_EXTERNAL_COUPLING_CLASS_EXT_BASE = (100);
        public const int IVIUPCONVERTER_VAL_AM_EXTERNAL_COUPLING_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for attribute IVIUPCONVERTER_ATTR_AM_SCALING -*/
        public const int IVIUPCONVERTER_VAL_AM_SCALING_LINEAR = (0);
        public const int IVIUPCONVERTER_VAL_AM_SCALING_LOGARITHMIC = (1);

        public const int IVIUPCONVERTER_VAL_AM_SCALING_CLASS_EXT_BASE = (100);
        public const int IVIUPCONVERTER_VAL_AM_SCALING_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for attribute IVIUPCONVERTER_ATTR_FM_EXTERNAL_COUPLING -*/
        public const int IVIUPCONVERTER_VAL_FM_EXTERNAL_COUPLING_AC = (0);
        public const int IVIUPCONVERTER_VAL_FM_EXTERNAL_COUPLING_DC = (1);

        public const int IVIUPCONVERTER_VAL_FM_EXTERNAL_COUPLING_CLASS_EXT_BASE = (100);
        public const int IVIUPCONVERTER_VAL_FM_EXTERNAL_COUPLING_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for attribute IVIUPCONVERTER_ATTR_FREQUENCY_STEP_SCALING -*/
        public const int IVIUPCONVERTER_VAL_FREQUENCY_STEP_SCALING_LINEAR = (0);
        public const int IVIUPCONVERTER_VAL_FREQUENCY_STEP_SCALING_LOGARITHMIC = (1);

        public const int IVIUPCONVERTER_VAL_FREQUENCY_STEP_SCALING_CLASS_EXT_BASE = (100);
        public const int IVIUPCONVERTER_VAL_FREQUENCY_STEP_SCALING_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for attribute IVIUPCONVERTER_ATTR_PM_EXTERNAL_COUPLING -*/
        public const int IVIUPCONVERTER_VAL_PM_EXTERNAL_COUPLING_AC = (0);
        public const int IVIUPCONVERTER_VAL_PM_EXTERNAL_COUPLING_DC = (1);

        public const int IVIUPCONVERTER_VAL_PM_EXTERNAL_COUPLING_CLASS_EXT_BASE = (100);
        public const int IVIUPCONVERTER_VAL_PM_EXTERNAL_COUPLING_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for attribute IVIUPCONVERTER_ATTR_PULSE_MODULATION_EXTERNAL_POLARITY -*/
        public const int IVIUPCONVERTER_VAL_PULSE_MODULATION_EXTERNAL_POLARITY_NORMAL = (0);
        public const int IVIUPCONVERTER_VAL_PULSE_MODULATION_EXTERNAL_POLARITY_INVERSE = (1);

        public const int IVIUPCONVERTER_VAL_PULSE_MODULATION_EXTERNAL_POLARITY_CLASS_EXT_BASE = (100);
        public const int IVIUPCONVERTER_VAL_PULSE_MODULATION_EXTERNAL_POLARITY_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for attribute IVIUPCONVERTER_ATTR_REFERENCE_OSCILLATOR_SOURCE -*/
        public const int IVIUPCONVERTER_VAL_REFERENCE_OSCILLATOR_SOURCE_INTERNAL = (0);
        public const int IVIUPCONVERTER_VAL_REFERENCE_OSCILLATOR_SOURCE_EXTERNAL = (1);

        public const int IVIUPCONVERTER_VAL_REFERENCE_OSCILLATOR_SOURCE_CLASS_EXT_BASE = (100);
        public const int IVIUPCONVERTER_VAL_REFERENCE_OSCILLATOR_SOURCE_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for attribute IVIUPCONVERTER_ATTR_SWEEP_MODE -*/
        public const int IVIUPCONVERTER_VAL_SWEEP_MODE_NONE = (0);
        public const int IVIUPCONVERTER_VAL_SWEEP_MODE_FREQUENCY_SWEEP = (1);
        public const int IVIUPCONVERTER_VAL_SWEEP_MODE_POWER_SWEEP = (2);
        public const int IVIUPCONVERTER_VAL_SWEEP_MODE_GAIN_SWEEP = (3);
        public const int IVIUPCONVERTER_VAL_SWEEP_MODE_FREQUENCY_STEP = (4);
        public const int IVIUPCONVERTER_VAL_SWEEP_MODE_POWER_STEP = (5);
        public const int IVIUPCONVERTER_VAL_SWEEP_MODE_GAIN_STEP = (6);
        public const int IVIUPCONVERTER_VAL_SWEEP_MODE_LIST = (7);

        public const int IVIUPCONVERTER_VAL_SWEEP_MODE_CLASS_EXT_BASE = (100);
        public const int IVIUPCONVERTER_VAL_SWEEP_MODE_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for attributes IVIUPCONVERTER_ATTR_RF_OUTPUT_READY_TRIGGER and IVIUPCONVERTER_ATTR_SWEEP_TRIGGER_SOURCE -*/
        public const string IVIUPCONVERTER_VAL_TRIGGER_SOURCE_NONE = "";
        public const string IVIUPCONVERTER_VAL_TRIGGER_SOURCE_IMMEDIATE = "Immediate";
        public const string IVIUPCONVERTER_VAL_TRIGGER_SOURCE_EXTERNAL = "External";
        public const string IVIUPCONVERTER_VAL_TRIGGER_SOURCE_stringERNAL = "stringernal";
        public const string IVIUPCONVERTER_VAL_TRIGGER_SOURCE_SOFTWARE = "Software";
        public const string IVIUPCONVERTER_VAL_TRIGGER_SOURCE_LAN0 = "LAN0";
        public const string IVIUPCONVERTER_VAL_TRIGGER_SOURCE_LAN1 = "LAN1";
        public const string IVIUPCONVERTER_VAL_TRIGGER_SOURCE_LAN2 = "LAN2";
        public const string IVIUPCONVERTER_VAL_TRIGGER_SOURCE_LAN3 = "LAN3";
        public const string IVIUPCONVERTER_VAL_TRIGGER_SOURCE_LAN4 = "LAN4";
        public const string IVIUPCONVERTER_VAL_TRIGGER_SOURCE_LAN5 = "LAN5";
        public const string IVIUPCONVERTER_VAL_TRIGGER_SOURCE_LAN6 = "LAN6";
        public const string IVIUPCONVERTER_VAL_TRIGGER_SOURCE_LAN7 = "LAN7";
        public const string IVIUPCONVERTER_VAL_TRIGGER_SOURCE_LXI0 = "LXI0";
        public const string IVIUPCONVERTER_VAL_TRIGGER_SOURCE_LXI1 = "LXI1";
        public const string IVIUPCONVERTER_VAL_TRIGGER_SOURCE_LXI2 = "LXI2";
        public const string IVIUPCONVERTER_VAL_TRIGGER_SOURCE_LXI3 = "LXI3";
        public const string IVIUPCONVERTER_VAL_TRIGGER_SOURCE_LXI4 = "LXI4";
        public const string IVIUPCONVERTER_VAL_TRIGGER_SOURCE_LXI5 = "LXI5";
        public const string IVIUPCONVERTER_VAL_TRIGGER_SOURCE_LXI6 = "LXI6";
        public const string IVIUPCONVERTER_VAL_TRIGGER_SOURCE_LXI7 = "LXI7";
        public const string IVIUPCONVERTER_VAL_TRIGGER_SOURCE_TTL0 = "TTL0";
        public const string IVIUPCONVERTER_VAL_TRIGGER_SOURCE_TTL1 = "TTL1";
        public const string IVIUPCONVERTER_VAL_TRIGGER_SOURCE_TTL2 = "TTL2";
        public const string IVIUPCONVERTER_VAL_TRIGGER_SOURCE_TTL3 = "TTL3";
        public const string IVIUPCONVERTER_VAL_TRIGGER_SOURCE_TTL4 = "TTL4";
        public const string IVIUPCONVERTER_VAL_TRIGGER_SOURCE_TTL5 = "TTL5";
        public const string IVIUPCONVERTER_VAL_TRIGGER_SOURCE_TTL6 = "TTL6";
        public const string IVIUPCONVERTER_VAL_TRIGGER_SOURCE_TTL7 = "TTL7";
        public const string IVIUPCONVERTER_VAL_TRIGGER_SOURCE_PXI_STAR = "PXI_STAR";
        public const string IVIUPCONVERTER_VAL_TRIGGER_SOURCE_PXI_TRIG0 = "PXI_TRIG0";
        public const string IVIUPCONVERTER_VAL_TRIGGER_SOURCE_PXI_TRIG1 = "PXI_TRIG1";
        public const string IVIUPCONVERTER_VAL_TRIGGER_SOURCE_PXI_TRIG2 = "PXI_TRIG2";
        public const string IVIUPCONVERTER_VAL_TRIGGER_SOURCE_PXI_TRIG3 = "PXI_TRIG3";
        public const string IVIUPCONVERTER_VAL_TRIGGER_SOURCE_PXI_TRIG4 = "PXI_TRIG4";
        public const string IVIUPCONVERTER_VAL_TRIGGER_SOURCE_PXI_TRIG5 = "PXI_TRIG5";
        public const string IVIUPCONVERTER_VAL_TRIGGER_SOURCE_PXI_TRIG6 = "PXI_TRIG6";
        public const string IVIUPCONVERTER_VAL_TRIGGER_SOURCE_PXI_TRIG7 = "PXI_TRIG7";
        public const string IVIUPCONVERTER_VAL_TRIGGER_SOURCE_PXIE_DSTARA = "PXIe_DSTARA";
        public const string IVIUPCONVERTER_VAL_TRIGGER_SOURCE_PXIE_DSTARB = "PXIe_DSTARB";
        public const string IVIUPCONVERTER_VAL_TRIGGER_SOURCE_PXIE_DSTARC = "PXIe_DSTARC";
        public const string IVIUPCONVERTER_VAL_TRIGGER_SOURCE_RTSI0 = "RTSI0";
        public const string IVIUPCONVERTER_VAL_TRIGGER_SOURCE_RTSI1 = "RTSI1";
        public const string IVIUPCONVERTER_VAL_TRIGGER_SOURCE_RTSI2 = "RTSI2";
        public const string IVIUPCONVERTER_VAL_TRIGGER_SOURCE_RTSI3 = "RTSI3";
        public const string IVIUPCONVERTER_VAL_TRIGGER_SOURCE_RTSI4 = "RTSI4";
        public const string IVIUPCONVERTER_VAL_TRIGGER_SOURCE_RTSI5 = "RTSI5";
        public const string IVIUPCONVERTER_VAL_TRIGGER_SOURCE_RTSI6 = "RTSI6";

        /*- Defined values for attribute IVIUPCONVERTER_ATTR_IF_INPUT_COUPLING -*/
        public const int IVIUPCONVERTER_VAL_IF_INPUT_COUPLING_AC = (0);
        public const int IVIUPCONVERTER_VAL_IF_INPUT_COUPLING_DC = (1);

        public const int IVIUPCONVERTER_VAL_INPUT_COUPLING_CLASS_EXT_BASE = (100);
        public const int IVIUPCONVERTER_VAL_INPUT_COUPLING_SPECIFIC_EXT_BASE = (1000);

        public const int IVIUPCONVERTER_VAL_CALIBRATION_COMPLETE = (0);
        public const int IVIUPCONVERTER_VAL_CALIBRATION_IN_PROGRESS = (1);
        public const int IVIUPCONVERTER_VAL_CALIBRATION_STATUS_UNKNOWN = (2);
        public const int IVIUPCONVERTER_VAL_CALIBRATION_FAILED = (3);

        public const int IVIUPCONVERTER_VAL_MAX_TIME_IMMEDIATE = (IviDriverAttribute.IVI_VAL_MAX_TIME_IMMEDIATE);
        public const int IVIUPCONVERTER_VAL_MAX_TIME_INFINITE = (IviDriverAttribute.IVI_VAL_MAX_TIME_INFINITE);
    }
}
 