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
    public class IviPwrMeterAttribute
    {

        /*- IviPwrMeter Fundamental Attributes -*/
        public const int IVIPWRMETER_ATTR_AVERAGING_AUTO_ENABLED = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 3);    /* ViBoolean */
        public const int IVIPWRMETER_ATTR_CORRECTION_FREQUENCY = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 4);    /* ViReal64 */
        public const int IVIPWRMETER_ATTR_OFFSET = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 5);    /* ViReal64 */
        public const int IVIPWRMETER_ATTR_RANGE_AUTO_ENABLED = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 2);    /* ViBoolean */
        public const int IVIPWRMETER_ATTR_UNITS = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 1);    /* ViInt32 */

        	/* Instrument Capabilities */
        public const int IVIPWRMETER_ATTR_CHANNEL_COUNT = (IviDriverAttribute.IVI_ATTR_CHANNEL_COUNT);                 /* ViInt32,  read-only  */

        /*- IviPwrMeter Extended Attributes -*/
        /*- IviPwrMeterChannelAcquisition Extension Group -*/
        public const int IVIPWRMETER_ATTR_CHANNEL_ENABLED = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 51);   /* ViBoolean */

        /*- IviPwrMeterManualRange Extension Group -*/
        public const int IVIPWRMETER_ATTR_RANGE_LOWER = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 101);  /* ViReal64 */
        public const int IVIPWRMETER_ATTR_RANGE_UPPER = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 102);  /* ViReal64 */

        /*- IviPwrMeterTriggerSource Extension Group -*/
        public const int IVIPWRMETER_ATTR_TRIGGER_SOURCE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 201);  /* ViInt32 */

        /*- IviPwrMeterInternalTrigger Extension Group -*/
        public const int IVIPWRMETER_ATTR_INTERNAL_TRIGGER_EVENT_SOURCE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 251);  /* ViString */
        public const int IVIPWRMETER_ATTR_INTERNAL_TRIGGER_LEVEL = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 252);  /* ViReal64 */
        public const int IVIPWRMETER_ATTR_INTERNAL_TRIGGER_SLOPE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 253);  /* ViInt32 */

        /*- IviPwrMeterAveragingCount Extension Group -*/
        public const int IVIPWRMETER_ATTR_AVERAGING_COUNT = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 301);  /* ViInt32 */

        /*- IviPwrMeterDutyCycleCorrection Extension Group -*/
        public const int IVIPWRMETER_ATTR_DUTY_CYCLE_CORRECTION = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 402);  /* ViReal64 */
        public const int IVIPWRMETER_ATTR_DUTY_CYCLE_CORRECTION_ENABLED = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 401);  /* ViBoolean */

        /*- IviPwrMeterReferenceOscillator Extension Group -*/
        public const int IVIPWRMETER_ATTR_REF_OSCILLATOR_ENABLED = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 501);  /* ViBoolean */
        public const int IVIPWRMETER_ATTR_REF_OSCILLATOR_FREQUENCY = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 502);  /* ViReal64 */
        public const int IVIPWRMETER_ATTR_REF_OSCILLATOR_LEVEL = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 503);  /* ViReal64 */


        /****************************************************************************
         *----------------- IviPwrMeter Class Attribute Value Defines -----------------*
         ****************************************************************************/
        /*- Defined values for attribute IVIPWRMETER_ATTR_UNITS -*/
        public const int IVIPWRMETER_VAL_DBM = (1);
        public const int IVIPWRMETER_VAL_DBMV = (2);
        public const int IVIPWRMETER_VAL_DBUV = (3);
        public const int IVIPWRMETER_VAL_WATTS = (4);
        public const int IVIPWRMETER_VAL_UNITS_CLASS_EXT_BASE = (500);
        public const int IVIPWRMETER_VAL_UNITS_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for attribute IVIPWRMETER_ATTR_TRIGGER_SOURCE -*/
        public const int IVIPWRMETER_VAL_IMMEDIATE = (1);
        public const int IVIPWRMETER_VAL_EXTERNAL = (2);
        public const int IVIPWRMETER_VAL_INTERNAL = (3);
        public const int IVIPWRMETER_VAL_SOFTWARE_TRIG = (4);
        public const int IVIPWRMETER_VAL_TTL0 = (100);
        public const int IVIPWRMETER_VAL_TTL1 = (101);
        public const int IVIPWRMETER_VAL_TTL2 = (102);
        public const int IVIPWRMETER_VAL_TTL3 = (103);
        public const int IVIPWRMETER_VAL_TTL4 = (104);
        public const int IVIPWRMETER_VAL_TTL5 = (105);
        public const int IVIPWRMETER_VAL_TTL6 = (106);
        public const int IVIPWRMETER_VAL_TTL7 = (107);
        public const int IVIPWRMETER_VAL_ECL0 = (200);
        public const int IVIPWRMETER_VAL_ECL1 = (201);
        public const int IVIPWRMETER_VAL_PXI_STAR = (300);
        public const int IVIPWRMETER_VAL_RTSI_0 = (400);
        public const int IVIPWRMETER_VAL_RTSI_1 = (401);
        public const int IVIPWRMETER_VAL_RTSI_2 = (402);
        public const int IVIPWRMETER_VAL_RTSI_3 = (403);
        public const int IVIPWRMETER_VAL_RTSI_4 = (404);
        public const int IVIPWRMETER_VAL_RTSI_5 = (405);
        public const int IVIPWRMETER_VAL_RTSI_6 = (406);
        public const int IVIPWRMETER_VAL_TRIGGER_SOURCE_CLASS_EXT_BASE = (500);
        public const int IVIPWRMETER_VAL_TRIGGER_SOURCE_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for attribute IVIPWRMETER_ATTR_INTERNAL_TRIGGER_SLOPE -*/
        public const int IVIPWRMETER_VAL_NEGATIVE = (0);
        public const int IVIPWRMETER_VAL_POSITIVE = (1);
        public const int IVIPWRMETER_VAL_TRIGGER_SLOPE_CLASS_EXT_BASE = (500);
        public const int IVIPWRMETER_VAL_TRIGGER_SLOPE_SPECIFIC_EXT_BASE = (1000);


        /*- Defined values for Operator parameter of function -*/
        /*- IviPwrMeter_ConfigureMeasurement -*/
        public const int IVIPWRMETER_VAL_NONE = (0);
        public const int IVIPWRMETER_VAL_DIFFERENCE = (1);
        public const int IVIPWRMETER_VAL_SUM = (2);
        public const int IVIPWRMETER_VAL_QUOTIENT = (3);

        /*- Defined values for MeasurementStatus parameter of function -*/
        /*- IviPwrMeter_IsMeasurementComplete -*/
        public const int IVIPWRMETER_VAL_MEAS_COMPLETE = (1);
        public const int IVIPWRMETER_VAL_MEAS_IN_PROGRESS = (0);
        public const int IVIPWRMETER_VAL_MEAS_STATUS_UNKNOWN = (-1);

        /*- Defined values for RangeType parameter of function -*/
        /*- IviPwrMeter_QueryResultRangeType -*/
        public const int IVIPWRMETER_VAL_IN_RANGE = (0);
        public const int IVIPWRMETER_VAL_UNDER_RANGE = (-1);
        public const int IVIPWRMETER_VAL_OVER_RANGE = (1);

        /*- Defined values for MaxTime parameter of function -*/
        /*- IviPwrMeter_Read -*/
        /*- Defined values for MaxTime parameter of function -*/
        /*- IviPwrMeter_ReadChannel -*/
        public const int IVIPWRMETER_VAL_MAX_TIME_IMMEDIATE = (0x0);
        public const uint IVIPWRMETER_VAL_MAX_TIME_INFINITE = (0xFFFFFFFFU);

        /*- Defined values for ZeroStatus parameter of function -*/
        /*- IviPwrMeter_IsZeroComplete -*/
        public const int IVIPWRMETER_VAL_ZERO_COMPLETE = (1);
        public const int IVIPWRMETER_VAL_ZERO_IN_PROGRESS = (0);
        public const int IVIPWRMETER_VAL_ZERO_STATUS_UNKNOWN = (-1);

        /*- Defined values for CalibrationStatus parameter of function -*/
        /*- IviPwrMeter_IsCalibrationComplete -*/
        public const int IVIPWRMETER_VAL_CALIBRATION_COMPLETE = (1);
        public const int IVIPWRMETER_VAL_CALIBRATION_IN_PROGRESS = (0);
        public const int IVIPWRMETER_VAL_CALIBRATION_STATUS_UNKNOWN = (-1);


    }
}