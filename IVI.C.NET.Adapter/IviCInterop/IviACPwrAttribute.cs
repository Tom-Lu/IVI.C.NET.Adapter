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
    public class IviACPwrAttribute
    {

        /*- IviACPwr Fundamental Attributes -*/
        public const int IVIACPWR_ATTR_CURRENT_LIMIT = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 13);  /* ViReal64, applies to OutputPhase           */
        public const int IVIACPWR_ATTR_FREQUENCY = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 1);   /* ViReal64                                   */
        public const int IVIACPWR_ATTR_NUM_PHASES = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 2);   /* ViInt32, read-only                         */
        public const int IVIACPWR_ATTR_OUTPUT_ENABLED = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 14);  /* ViBoolean, applies to OutputPhase          */
        public const int IVIACPWR_ATTR_VOLTAGE_LEVEL = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 16);  /* ViReal64, applies to OutputPhase           */
        public const int IVIACPWR_ATTR_WAVEFORM = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 17);  /* ViString, applies to OutputPhase           */
        public const int IVIACPWR_ATTR_OUTPUT_PHASE_COUNT = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 36);  /* ViInt32, read-only                         */
        public const int IVIACPWR_ATTR_NUM_VOLTAGE_RANGES = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 15);  /* ViInt32, read-only, applies to OutputPhase */
        public const int IVIACPWR_ATTR_VOLTAGE_RANGE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 18);  /* ViReal64, applies to OutputPhase           */
        public const int IVIACPWR_ATTR_NUM_FREQUENCY_RANGES = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 3);   /* ViInt32, read-only                         */
        public const int IVIACPWR_ATTR_FREQUENCY_RANGE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 4);   /* ViReal64                                   */

        /* IviACPwrPhase Extended Attributes */
        public const int IVIACPWR_ATTR_PHASE_ANGLE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 19);  /* ViReal64, applies to OutputPhase */

        /* IviACPwrExternalSync Extended Attributes */
        public const int IVIACPWR_ATTR_EXTERNAL_SYNC_ENABLED = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 10);  /* ViBoolean            */
        public const int IVIACPWR_ATTR_EXTERNAL_SYNC_PHASE_OFFSET = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 11);  /* ViReal64             */
        public const int IVIACPWR_ATTR_EXTERNAL_SYNC_LOCKED = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 12);  /* ViBoolean, read-only */

        /* IviACPwrCurrentProtection Extended Attributes */
        public const int IVIACPWR_ATTR_CURRENT_PROTECTION_THRESHOLD = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 25);  /* ViReal64, applies to OutputPhase             */
        public const int IVIACPWR_ATTR_CURRENT_PROTECTION_DELAY = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 23);  /* ViReal64, applies to OutputPhase             */
        public const int IVIACPWR_ATTR_CURRENT_PROTECTION_ENABLED = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 24);  /* ViBoolean, applies to OutputPhase            */
        public const int IVIACPWR_ATTR_CURRENT_PROTECTION_TRIPPED = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 26);  /* ViBoolean, read-only, applies to OutputPhase */

        /* IviACPwrVoltageProtection Extended Attributes */
        public const int IVIACPWR_ATTR_UNDER_VOLTAGE_PROTECTION_LIMIT = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 30);  /* ViReal64, applies to OutputPhase             */
        public const int IVIACPWR_ATTR_OVER_VOLTAGE_PROTECTION_LIMIT = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 28);  /* ViReal64, applies to OutputPhase             */
        public const int IVIACPWR_ATTR_OVER_VOLTAGE_PROTECTION_ENABLED = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 27);  /* ViBoolean, applies to OutputPhase            */
        public const int IVIACPWR_ATTR_UNDER_VOLTAGE_PROTECTION_ENABLED = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 29);  /* ViBoolean, applies to OutputPhase            */
        public const int IVIACPWR_ATTR_VOLTAGE_PROTECTION_TRIPPED = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 31);  /* ViBoolean, read-only, applies to OutputPhase */

        /* IviACPwrArbWaveform Extended Attributes */
        public const int IVIACPWR_ATTR_NUM_WAVEFORMS_MAX = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 5);   /* ViInt32, read-only */
        public const int IVIACPWR_ATTR_NUM_OPTIMAL_DATA_POINTS = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 6);   /* ViInt32, read-only */
        public const int IVIACPWR_ATTR_FIXED_WAVEFORM_COUNT = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 7);   /* ViInt32, read-only */
        public const int IVIACPWR_ATTR_USER_WAVEFORM_COUNT = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 8);   /* ViInt32, read-only */
        public const int IVIACPWR_ATTR_AVAILABLE_WAVEFORM_COUNT = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 9);   /* ViInt32, read-only */

        /* IviACPwrImpedance Extended Attributes */
        public const int IVIACPWR_ATTR_OUTPUT_IMPEDANCE_ENABLED = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 21);  /* ViBoolean, applies to OutputPhase */
        public const int IVIACPWR_ATTR_OUTPUT_IMPEDANCE_RESISTIVE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 22);  /* ViReal64, applies to OutputPhase  */
        public const int IVIACPWR_ATTR_OUTPUT_IMPEDANCE_INDUCTIVE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 20);  /* ViReal64, applies to OutputPhase  */

        /* IviACPwrDCGeneration Extended Attributes */
        public const int IVIACPWR_ATTR_DC_MODE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 32);  /* ViInt32, applies to OutputPhase             */
        public const int IVIACPWR_ATTR_DC_VOLTAGE_LEVEL = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 33);  /* ViReal64, applies to OutputPhase            */
        public const int IVIACPWR_ATTR_DC_RANGE_MIN = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 40);  /* ViReal64, read-only, applies to OutputPhase */
        public const int IVIACPWR_ATTR_DC_RANGE_MAX = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 41);  /* ViReal64, read-only, applies to OutputPhase */
        public const int IVIACPWR_ATTR_DC_NUM_RANGES = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 35);  /* ViInt32, read-only, applies to OutputPhase  */

        /* IviACPwrVoltageRamp Extended Attributes */
        public const int IVIACPWR_ATTR_VOLTAGE_RAMP_BUSY = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 37);  /* ViBoolean, read-only, applies to OutputPhase */

        /* IviACPwrCurrentRamp Extended Attributes */
        public const int IVIACPWR_ATTR_CURRENT_RAMP_BUSY = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 38);  /* ViBoolean, read-only, applies to OutputPhase */

        /* IviACPwrFrequencyRamp Extended Attributes */
        public const int IVIACPWR_ATTR_FREQUENCY_RAMP_BUSY = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 39);  /* ViBoolean, read-only */

        /****************************************************************************
         *----------------- IviACPwr Class Attribute Value Defines -----------------*
         ****************************************************************************/
        /*- Values Defines for IVIACPWR_ATTR_DC_MODE -*/
        public const int IVIACPWR_VAL_MODE_AC_ONLY = (0);
        public const int IVIACPWR_VAL_MODE_DC_ONLY = (1);
        public const int IVIACPWR_VAL_MODE_AC_DC = (2);

        public const int IVIACPWR_VAL_DC_MODE_CLASS_EXT_BASE = (500);
        public const int IVIACPWR_VAL_DC_MODE_SPECIFIC_EXT_BASE = (1000);

        /****************************************************************************
         *----------------- IviACPwr Function Parameter Value Definitions ----------*
         ****************************************************************************/

        /*- Fetch Measurement -*/
        public const int IVIACPWR_VAL_MEASURE_VOLTAGE_RMS_LN = (0);
        public const int IVIACPWR_VAL_MEASURE_CURRENT_RMS = (1);
        public const int IVIACPWR_VAL_MEASURE_FREQUENCY = (2);
        public const int IVIACPWR_VAL_MEASURE_VOLTAGE_DC = (3);
        public const int IVIACPWR_VAL_MEASURE_CURRENT_DC = (4);
        public const int IVIACPWR_VAL_MEASURE_POWER_FACTOR = (5);
        public const int IVIACPWR_VAL_MEASURE_CREST_FACTOR = (6);
        public const int IVIACPWR_VAL_MEASURE_CURRENT_PEAK = (7);
        public const int IVIACPWR_VAL_MEASURE_POWER_VA = (8);
        public const int IVIACPWR_VAL_MEASURE_POWER_REAL = (9);
        public const int IVIACPWR_VAL_MEASURE_POWER_DC = (10);
        public const int IVIACPWR_VAL_MEASURE_PHASE_ANGLE = (11);
        public const int IVIACPWR_VAL_MEASURE_VOLTAGE_RMS_LL = (12);
        public const int IVIACPWR_VAL_MEASURE_CURRENT_OHD = (13);
        public const int IVIACPWR_VAL_MEASURE_CURRENT_EHD = (14);
        public const int IVIACPWR_VAL_MEASURE_CURRENT_THD = (15);
        public const int IVIACPWR_VAL_MEASURE_VOLTAGE_OHD = (16);
        public const int IVIACPWR_VAL_MEASURE_VOLTAGE_EHD = (17);
        public const int IVIACPWR_VAL_MEASURE_VOLTAGE_THD = (18);

        /*- Fetch Measurement Array -*/
        public const int IVIACPWR_VAL_MEASURE_ARRAY_CURRENT_HARMONIC_PHASE = (0);
        public const int IVIACPWR_VAL_MEASURE_ARRAY_CURRENT_HARMONIC_ABS = (1);
        public const int IVIACPWR_VAL_MEASURE_ARRAY_CURRENT_HARMONIC_PCT = (2);
        public const int IVIACPWR_VAL_MEASURE_ARRAY_VOLTAGE_HARMONIC_PHASE = (3);
        public const int IVIACPWR_VAL_MEASURE_ARRAY_VOLTAGE_HARMONIC_ABS = (4);
        public const int IVIACPWR_VAL_MEASURE_ARRAY_VOLTAGE_HARMONIC_PCT = (5);
        public const int IVIACPWR_VAL_MEASURE_ARRAY_CURRENT_CYCLE = (6);
        public const int IVIACPWR_VAL_MEASURE_ARRAY_VOLTAGE_CYCLE = (7);

        /*- Initiate Measurement -*/
        public const int IVIACPWR_VAL_MEASUREMENT_GROUP_BASE = (1);
        public const int IVIACPWR_VAL_MEASUREMENT_GROUP_HARMONIC = (2);
        public const int IVIACPWR_VAL_MEASUREMENT_GROUP_DISTORTION = (4);
        public const int IVIACPWR_VAL_MEASUREMENT_GROUP_WAVEFORM = (8);

        /*- Query Arbitrary Waveform Catalog -*/
        public const int IVIACPWR_VAL_WAVEFORM_CATALOG_FIXED = (0);
        public const int IVIACPWR_VAL_WAVEFORM_CATALOG_USER = (1);
        public const int IVIACPWR_VAL_WAVEFORM_CATALOG_ALL = (2);

    }
}
