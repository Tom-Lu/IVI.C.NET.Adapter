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
    public class IviFgenAttribute
    {
        /*- IviFgen Fundamental Attributes -*/
        public const int IVIFGEN_ATTR_CHANNEL_COUNT = IviDriverAttribute.IVI_ATTR_CHANNEL_COUNT;              /* (ViInt32);, read-only */
        public const int IVIFGEN_ATTR_OUTPUT_MODE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 1);   /* (ViInt32); */
        public const int IVIFGEN_ATTR_REF_CLOCK_SOURCE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 2);   /* (ViInt32); */
        public const int IVIFGEN_ATTR_OUTPUT_ENABLED = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 3);   /* (ViBoolean,  Multi-Channe); */
        public const int IVIFGEN_ATTR_OUTPUT_IMPEDANCE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 4);   /* (ViReal64,   Multi-Channe); */
        public const int IVIFGEN_ATTR_OPERATION_MODE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 5);   /* (ViInt32,    Multi-Channe); */

        /*- IviFgenSampleClock Extended Attributes -*/
        public const int IVIFGEN_ATTR_SAMPLE_CLOCK_SOURCE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 21);  /* (ViInt32);   */
        public const int IVIFGEN_ATTR_SAMPLE_CLOCK_OUTPUT_ENABLED = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 22); /* (ViBoolean); */

        /*- IviFgenTerminalConfiguration Extended Attributes -*/
        public const int IVIFGEN_ATTR_TERMINAL_CONFIGURATION = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 31);  /* (ViInt32,    Multi-Channe); */

        /*- IviFgenStdFunc Extended Attributes -*/
        public const int IVIFGEN_ATTR_FUNC_WAVEFORM = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 101);  /* (ViInt32,  Multi-Channe);   */
        public const int IVIFGEN_ATTR_FUNC_AMPLITUDE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 102);  /* (ViReal64,  Multi-Channe);  */
        public const int IVIFGEN_ATTR_FUNC_DC_OFFSET = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 103);  /* (ViReal64,  Multi-Channe);  */
        public const int IVIFGEN_ATTR_FUNC_FREQUENCY = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 104);  /* (ViReal64,  Multi-Channe);  */
        public const int IVIFGEN_ATTR_FUNC_START_PHASE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 105);  /* (ViReal64,  Multi-Channe);  */
        public const int IVIFGEN_ATTR_FUNC_DUTY_CYCLE_HIGH = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 106);  /* (ViReal64,  Multi-Channe);  */

        /*- IviFgenArbWfm Extended Attributes -*/
        public const int IVIFGEN_ATTR_ARB_WAVEFORM_HANDLE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 201);  /* (ViInt32,  Multi-Channe);   */
        public const int IVIFGEN_ATTR_ARB_GAIN = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 202);  /* (ViReal64, Multi-Channe);  */
        public const int IVIFGEN_ATTR_ARB_OFFSET = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 203);  /* (ViReal64, Multi-Channe);  */
        public const int IVIFGEN_ATTR_ARB_SAMPLE_RATE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 204);  /* (ViReal64);  */

        public const int IVIFGEN_ATTR_MAX_NUM_WAVEFORMS = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 205);  /* (ViInt32, Read-only);   */
        public const int IVIFGEN_ATTR_WAVEFORM_QUANTUM = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 206);  /* (ViInt32, Read-only);   */
        public const int IVIFGEN_ATTR_MIN_WAVEFORM_SIZE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 207);  /* (ViInt32, Read-only);   */
        public const int IVIFGEN_ATTR_MAX_WAVEFORM_SIZE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 208);  /* (ViInt32, Read-only);   */

        /*- IviFgenArbFrequency Extended Attributes -*/
        public const int IVIFGEN_ATTR_ARB_FREQUENCY = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 209);  /* (ViReal64, Multi-Channe);  */

        /*- IviFgenArbSeq Extended Attributes -*/
        public const int IVIFGEN_ATTR_ARB_SEQUENCE_HANDLE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 211);  /* (ViInt32, Multi-Channe);   */
        public const int IVIFGEN_ATTR_MAX_NUM_SEQUENCES = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 212);  /* (ViInt32, Read-only);   */
        public const int IVIFGEN_ATTR_MIN_SEQUENCE_LENGTH = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 213);  /* (ViInt32, Read-only);   */
        public const int IVIFGEN_ATTR_MAX_SEQUENCE_LENGTH = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 214);  /* (ViInt32, Read-only);   */
        public const int IVIFGEN_ATTR_MAX_LOOP_COUNT = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 215);  /* (ViInt32, Read-only);   */

        /*- IviFgenArbWfmSize64 Extended Attributes -*/
        public const int IVIFGEN_ATTR_MIN_WAVEFORM_SIZE64 = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 221);  /* (ViInt64, Read-only);   */
        public const int IVIFGEN_ATTR_MAX_WAVEFORM_SIZE64 = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 222);  /* (ViInt64, Read-only);   */

        /*- IviFgenArbWfmBinary Extended Attributes -*/
        public const int IVIFGEN_ATTR_BINARY_ALIGNMENT = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 241);  /* (ViInt32, Read-only);   */
        public const int IVIFGEN_ATTR_SAMPLE_BIT_RESOLUTION = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 242);  /* (ViInt32, Read-only);   */

        /*- IviFgenArbDataMask Extended Attributes -*/
        public const int IVIFGEN_ATTR_OUTPUT_DATA_MASK = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 261);  /* (ViInt32); */

        /*- IviFgenArbSeqDepth Extended Attributes -*/
        public const int IVIFGEN_ATTR_SEQUENCE_DEPTH_MAX = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 281);  /* (ViInt32, Read-only);   */

        /*- IviFgenTrigger Extended Attributes -*/
        public const int IVIFGEN_ATTR_TRIGGER_SOURCE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 302);  /* (ViInt32, Multi-Channe);   */

        /*- IviFgenInternalTrigger Extended Attributes -*/
        public const int IVIFGEN_ATTR_INTERNAL_TRIGGER_RATE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 310);  /* (ViReal64);   */

        /*- IviFgenStartTrigger Extended Attributes -*/
        public const int IVIFGEN_ATTR_START_TRIGGER_DELAY = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 320);  /* (ViReal64, Multi-Channe);  */
        public const int IVIFGEN_ATTR_START_TRIGGER_SLOPE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 321);  /* (ViInt32,  Multi-Channe);  */
        public const int IVIFGEN_ATTR_START_TRIGGER_SOURCE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 322);  /* (ViString, Multi-Channe);  */
        public const int IVIFGEN_ATTR_START_TRIGGER_THRESHOLD = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 323);  /* (ViReal64, Multi-Channe);  */

        /*- IviFgenStopTrigger Extended Attributes -*/
        public const int IVIFGEN_ATTR_STOP_TRIGGER_DELAY = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 330);  /* (ViReal64, Multi-Channe);  */
        public const int IVIFGEN_ATTR_STOP_TRIGGER_SLOPE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 331);  /* (ViInt32,  Multi-Channe);  */
        public const int IVIFGEN_ATTR_STOP_TRIGGER_SOURCE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 332);  /* (ViString, Multi-Channe);  */
        public const int IVIFGEN_ATTR_STOP_TRIGGER_THRESHOLD = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 333);  /* (ViReal64, Multi-Channe);  */

        /*- IviFgenHoldTrigger Extended Attributes -*/
        public const int IVIFGEN_ATTR_HOLD_TRIGGER_DELAY = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 340);  /* (ViReal64, Multi-Channe);  */
        public const int IVIFGEN_ATTR_HOLD_TRIGGER_SLOPE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 341);  /* (ViInt32,  Multi-Channe);  */
        public const int IVIFGEN_ATTR_HOLD_TRIGGER_SOURCE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 342);  /* (ViString, Multi-Channe);  */
        public const int IVIFGEN_ATTR_HOLD_TRIGGER_THRESHOLD = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 343);  /* (ViReal64, Multi-Channe);  */

        /*- IviFgenBurst Extended Attributes -*/
        public const int IVIFGEN_ATTR_BURST_COUNT = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 350);  /* (ViInt32, Multi-Channe);   */

        /*- IviFgenResumeTrigger Extended Attributes -*/
        public const int IVIFGEN_ATTR_RESUME_TRIGGER_DELAY = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 360);  /* (ViReal64, Multi-Channe);  */
        public const int IVIFGEN_ATTR_RESUME_TRIGGER_SLOPE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 361);  /* (ViInt32,  Multi-Channe);  */
        public const int IVIFGEN_ATTR_RESUME_TRIGGER_SOURCE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 362);  /* (ViString, Multi-Channe);  */
        public const int IVIFGEN_ATTR_RESUME_TRIGGER_THRESHOLD = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 363);  /* (ViReal64, Multi-Channe);  */

        /*- IviFgenAdvanceTrigger Extended Attributes -*/
        public const int IVIFGEN_ATTR_ADVANCE_TRIGGER_DELAY = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 370);  /* (ViReal64, Multi-Channe);  */
        public const int IVIFGEN_ATTR_ADVANCE_TRIGGER_SLOPE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 371);  /* (ViInt32,  Multi-Channe);  */
        public const int IVIFGEN_ATTR_ADVANCE_TRIGGER_SOURCE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 372);  /* (ViString, Multi-Channe);  */
        public const int IVIFGEN_ATTR_ADVANCE_TRIGGER_THRESHOLD = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 373);  /* (ViReal64, Multi-Channe);  */

        /*- IviFgenModulateAM Extended Attributes -*/
        public const int IVIFGEN_ATTR_AM_ENABLED = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 401);  /* (ViBoolean, Multi-Channe); */
        public const int IVIFGEN_ATTR_AM_SOURCE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 402);  /* (ViInt32, Multi-Channe);   */
        public const int IVIFGEN_ATTR_AM_INTERNAL_DEPTH = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 403);  /* (ViReal64);  */
        public const int IVIFGEN_ATTR_AM_INTERNAL_WAVEFORM = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 404);  /* (ViInt32);   */
        public const int IVIFGEN_ATTR_AM_INTERNAL_FREQUENCY = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 405);  /* (ViReal64);  */

        /*- IviFgenModulateFM Extended Attributes -*/
        public const int IVIFGEN_ATTR_FM_ENABLED = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 501);  /* (ViBoolean, Multi-Channe); */
        public const int IVIFGEN_ATTR_FM_SOURCE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 502);  /* (ViInt32, Multi-Channe);   */
        public const int IVIFGEN_ATTR_FM_INTERNAL_DEVIATION = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 503);  /* (ViReal64);  */
        public const int IVIFGEN_ATTR_FM_INTERNAL_WAVEFORM = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 504);  /* (ViInt32);   */
        public const int IVIFGEN_ATTR_FM_INTERNAL_FREQUENCY = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 505);  /* (ViReal64);  */

        /*- IviFgenDataMarker Extended Attributes -*/
        public const int IVIFGEN_ATTR_DATAMARKER_AMPLITUDE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 601);  /* (ViReal64); */
        public const int IVIFGEN_ATTR_DATAMARKER_BIT_POSITION = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 602);  /* (ViInt32);  */
        public const int IVIFGEN_ATTR_DATAMARKER_COUNT = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 603);  /* (ViInt32, Read-only );      */
        public const int IVIFGEN_ATTR_DATAMARKER_DELAY = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 604);  /* (ViReal64); */
        public const int IVIFGEN_ATTR_DATAMARKER_DESTINATION = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 605);  /* (ViString); */
        public const int IVIFGEN_ATTR_DATAMARKER_POLARITY = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 606);  /* (ViInt32);  */
        public const int IVIFGEN_ATTR_DATAMARKER_SOURCE_CHANNEL = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 607);  /* (ViString); */

        /*- IviFgenSparseMarker Extended Attributes -*/
        public const int IVIFGEN_ATTR_SPARSEMARKER_AMPLITUDE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 701);  /* (ViReal64, Multi-Channe);  */
        public const int IVIFGEN_ATTR_SPARSEMARKER_COUNT = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 702);  /* (ViInt32,  Read-only);      */
        public const int IVIFGEN_ATTR_SPARSEMARKER_DELAY = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 703);  /* (ViReal64, Multi-Channe);  */
        public const int IVIFGEN_ATTR_SPARSEMARKER_DESTINATION = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 704);  /* (ViString, Multi-Channe);  */
        public const int IVIFGEN_ATTR_SPARSEMARKER_POLARITY = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 705);  /* (ViInt32,  Multi-Channe);  */
        public const int IVIFGEN_ATTR_SPARSEMARKER_WFMHANDLE = (IviDriverAttribute.IVI_CLASS_PUBLIC_ATTR_BASE + 706);  /* (ViInt32,  Multi-Channe);  */

        /******************************************************************************
         *------------------- IviFgen Class Attribute Value Defines ------------------*
         ******************************************************************************/

        /*- Defined valued for attribute IVIFGEN_ATTR_OUTPUT_MODE -*/
        public const int IVIFGEN_VAL_OUTPUT_FUNC = (0);
        public const int IVIFGEN_VAL_OUTPUT_ARB = (1);
        public const int IVIFGEN_VAL_OUTPUT_SEQ = (2);

        public const int IVIFGEN_VAL_OUT_MODE_CLASS_EXT_BASE = (500);
        public const int IVIFGEN_VAL_OUT_MODE_SPECIFIC_EXT_BASE = (1000);

        /*- Defined valued for attribute IVIFGEN_ATTR_OPERATION_MODE -*/
        public const int IVIFGEN_VAL_OPERATE_CONTINUOUS = (0);
        public const int IVIFGEN_VAL_OPERATE_BURST = (1);

        public const int IVIFGEN_VAL_OP_MODE_CLASS_EXT_BASE = (500);
        public const int IVIFGEN_VAL_OP_MODE_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for attribute IVIFGEN_ATTR_REF_CLOCK_SOURCE -*/
        public const int IVIFGEN_VAL_REF_CLOCK_INTERNAL = (0);
        public const int IVIFGEN_VAL_REF_CLOCK_EXTERNAL = (1);
        public const int IVIFGEN_VAL_REF_CLOCK_RTSI_CLOCK = (101);

        public const int IVIFGEN_VAL_CLK_SRC_CLASS_EXT_BASE = (500);
        public const int IVIFGEN_VAL_CLK_SRC_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for attribute IVIFGEN_ATTR_FUNC_WAVEFORM -*/
        public const int IVIFGEN_VAL_WFM_SINE = (1);
        public const int IVIFGEN_VAL_WFM_SQUARE = (2);
        public const int IVIFGEN_VAL_WFM_TRIANGLE = (3);
        public const int IVIFGEN_VAL_WFM_RAMP_UP = (4);
        public const int IVIFGEN_VAL_WFM_RAMP_DOWN = (5);
        public const int IVIFGEN_VAL_WFM_DC = (6);

        public const int IVIFGEN_VAL_WFM_CLASS_EXT_BASE = (500);
        public const int IVIFGEN_VAL_WFM_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for attribute IVIFGEN_ATTR_TRIGGER_SOURCE -*/
        public const int IVIFGEN_VAL_EXTERNAL = (1);
        public const int IVIFGEN_VAL_SOFTWARE_TRIG = (2);
        public const int IVIFGEN_VAL_INTERNAL_TRIGGER = (3);
        public const int IVIFGEN_VAL_TTL0 = (111);
        public const int IVIFGEN_VAL_TTL1 = (112);
        public const int IVIFGEN_VAL_TTL2 = (113);
        public const int IVIFGEN_VAL_TTL3 = (114);
        public const int IVIFGEN_VAL_TTL4 = (115);
        public const int IVIFGEN_VAL_TTL5 = (116);
        public const int IVIFGEN_VAL_TTL6 = (117);
        public const int IVIFGEN_VAL_TTL7 = (118);
        public const int IVIFGEN_VAL_ECL0 = (119);
        public const int IVIFGEN_VAL_ECL1 = (120);
        public const int IVIFGEN_VAL_PXI_STAR = (131);
        public const int IVIFGEN_VAL_RTSI_0 = (141);
        public const int IVIFGEN_VAL_RTSI_1 = (142);
        public const int IVIFGEN_VAL_RTSI_2 = (143);
        public const int IVIFGEN_VAL_RTSI_3 = (144);
        public const int IVIFGEN_VAL_RTSI_4 = (145);
        public const int IVIFGEN_VAL_RTSI_5 = (146);
        public const int IVIFGEN_VAL_RTSI_6 = (147);

        public const int IVIFGEN_VAL_TRIG_SRC_CLASS_EXT_BASE = (500);
        public const int IVIFGEN_VAL_TRIG_SRC_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for attribute IVIFGEN_ATTR_START_TRIGGER_SOURCE -*/
        /*- Defined values for attribute IVIFGEN_ATTR_STOP_TRIGGER_SOURCE -*/
        /*- Defined values for attribute IVIFGEN_ATTR_HOLD_TRIGGER_SOURCE -*/
        /*- Defined values for attribute IVIFGEN_ATTR_RESUME_TRIGGER_SOURCE -*/
        /*- Defined values for attribute IVIFGEN_ATTR_ADVANCE_TRIGGER_SOURCE -*/
        /*- Defined values for attribute IVIFGEN_ATTR_RESUME_TRIGGER_SOURCE -*/
        /*- Defined values for attribute IVIFGEN_ATTR_DATA_MARKER_DESTINATION -*/
        /*- Defined values for attribute IVIFGEN_ATTR_SPARSE_MARKER_DESTINATION -*/
        public const string IVIFGEN_VAL_TRIGGER_SOURCE_NONE = "";
        public const string IVIFGEN_VAL_TRIGGER_SOURCE_IMMEDIATE = "Immediate";
        public const string IVIFGEN_VAL_TRIGGER_SOURCE_EXTERNAL = "External";
        public const string IVIFGEN_VAL_TRIGGER_SOURCE_INTERNAL = "Internal";
        public const string IVIFGEN_VAL_TRIGGER_SOURCE_SOFTWARE = "Software";
        public const string IVIFGEN_VAL_TRIGGER_SOURCE_LAN0 = "LAN0";
        public const string IVIFGEN_VAL_TRIGGER_SOURCE_LAN1 = "LAN1";
        public const string IVIFGEN_VAL_TRIGGER_SOURCE_LAN2 = "LAN2";
        public const string IVIFGEN_VAL_TRIGGER_SOURCE_LAN3 = "LAN3";
        public const string IVIFGEN_VAL_TRIGGER_SOURCE_LAN4 = "LAN4";
        public const string IVIFGEN_VAL_TRIGGER_SOURCE_LAN5 = "LAN5";
        public const string IVIFGEN_VAL_TRIGGER_SOURCE_LAN6 = "LAN6";
        public const string IVIFGEN_VAL_TRIGGER_SOURCE_LAN7 = "LAN7";
        public const string IVIFGEN_VAL_TRIGGER_SOURCE_LXI0 = "LXI0";
        public const string IVIFGEN_VAL_TRIGGER_SOURCE_LXI1 = "LXI1";
        public const string IVIFGEN_VAL_TRIGGER_SOURCE_LXI2 = "LXI2";
        public const string IVIFGEN_VAL_TRIGGER_SOURCE_LXI3 = "LXI3";
        public const string IVIFGEN_VAL_TRIGGER_SOURCE_LXI4 = "LXI4";
        public const string IVIFGEN_VAL_TRIGGER_SOURCE_LXI5 = "LXI5";
        public const string IVIFGEN_VAL_TRIGGER_SOURCE_LXI6 = "LXI6";
        public const string IVIFGEN_VAL_TRIGGER_SOURCE_LXI7 = "LXI7";
        public const string IVIFGEN_VAL_TRIGGER_SOURCE_TTL0 = "TTL0";
        public const string IVIFGEN_VAL_TRIGGER_SOURCE_TTL1 = "TTL1";
        public const string IVIFGEN_VAL_TRIGGER_SOURCE_TTL2 = "TTL2";
        public const string IVIFGEN_VAL_TRIGGER_SOURCE_TTL3 = "TTL3";
        public const string IVIFGEN_VAL_TRIGGER_SOURCE_TTL4 = "TTL4";
        public const string IVIFGEN_VAL_TRIGGER_SOURCE_TTL5 = "TTL5";
        public const string IVIFGEN_VAL_TRIGGER_SOURCE_TTL6 = "TTL6";
        public const string IVIFGEN_VAL_TRIGGER_SOURCE_TTL7 = "TTL7";
        public const string IVIFGEN_VAL_TRIGGER_SOURCE_PXI_STAR = "PXI_STAR";
        public const string IVIFGEN_VAL_TRIGGER_SOURCE_PXI_TRIG0 = "PXI_TRIG0";
        public const string IVIFGEN_VAL_TRIGGER_SOURCE_PXI_TRIG1 = "PXI_TRIG1";
        public const string IVIFGEN_VAL_TRIGGER_SOURCE_PXI_TRIG2 = "PXI_TRIG2";
        public const string IVIFGEN_VAL_TRIGGER_SOURCE_PXI_TRIG3 = "PXI_TRIG3";
        public const string IVIFGEN_VAL_TRIGGER_SOURCE_PXI_TRIG4 = "PXI_TRIG4";
        public const string IVIFGEN_VAL_TRIGGER_SOURCE_PXI_TRIG5 = "PXI_TRIG5";
        public const string IVIFGEN_VAL_TRIGGER_SOURCE_PXI_TRIG6 = "PXI_TRIG6";
        public const string IVIFGEN_VAL_TRIGGER_SOURCE_PXI_TRIG7 = "PXI_TRIG7";
        public const string IVIFGEN_VAL_TRIGGER_SOURCE_PXIE_DSTARA = "PXIe_DSTARA";
        public const string IVIFGEN_VAL_TRIGGER_SOURCE_PXIE_DSTARB = "PXIe_DSTARB";
        public const string IVIFGEN_VAL_TRIGGER_SOURCE_PXIE_DSTARC = "PXIe_DSTARC";
        public const string IVIFGEN_VAL_TRIGGER_SOURCE_RTSI0 = "RTSI0";
        public const string IVIFGEN_VAL_TRIGGER_SOURCE_RTSI1 = "RTSI1";
        public const string IVIFGEN_VAL_TRIGGER_SOURCE_RTSI2 = "RTSI2";
        public const string IVIFGEN_VAL_TRIGGER_SOURCE_RTSI3 = "RTSI3";
        public const string IVIFGEN_VAL_TRIGGER_SOURCE_RTSI4 = "RTSI4";
        public const string IVIFGEN_VAL_TRIGGER_SOURCE_RTSI5 = "RTSI5";
        public const string IVIFGEN_VAL_TRIGGER_SOURCE_RTSI6 = "RTSI6";

        /*- Defined values for attribute IVIFGEN_ATTR_SAMPLE_CLOCK_SOURCE -*/
        public const int IVIFGEN_VAL_SAMPLE_CLOCK_SOURCE_INTERNAL = (0);
        public const int IVIFGEN_VAL_SAMPLE_CLOCK_SOURCE_EXTERNAL = (1);

        /*- Defined values for attribute IVIFGEN_ATTR_DATAMARKER_POLARITY -*/
        public const int IVIFGEN_VAL_MARKER_POLARITY_ACTIVE_HIGH = (0);
        public const int IVIFGEN_VAL_MARKER_POLARITY_ACTIVE_LOW = (1);

        /*- Defined values for attribute IVIFGEN_ATTR_AM_SOURCE -*/
        public const int IVIFGEN_VAL_AM_INTERNAL = (0);
        public const int IVIFGEN_VAL_AM_EXTERNAL = (1);

        public const int IVIFGEN_VAL_AM_SOURCE_CLASS_EXT_BASE = (500);
        public const int IVIFGEN_VAL_AM_SOURCE_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for attribute IVIFGEN_ATTR_AM_INTERNAL_WAVEFORM -*/
        public const int IVIFGEN_VAL_AM_INTERNAL_SINE = (1);
        public const int IVIFGEN_VAL_AM_INTERNAL_SQUARE = (2);
        public const int IVIFGEN_VAL_AM_INTERNAL_TRIANGLE = (3);
        public const int IVIFGEN_VAL_AM_INTERNAL_RAMP_UP = (4);
        public const int IVIFGEN_VAL_AM_INTERNAL_RAMP_DOWN = (5);

        public const int IVIFGEN_VAL_AM_INTERNAL_WFM_CLASS_EXT_BASE = (500);
        public const int IVIFGEN_VAL_AM_INTERNAL_WFM_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for attribute IVIFGEN_ATTR_FM_SOURCE -*/
        public const int IVIFGEN_VAL_FM_INTERNAL = (0);
        public const int IVIFGEN_VAL_FM_EXTERNAL = (1);

        public const int IVIFGEN_VAL_FM_SOURCE_CLASS_EXT_BASE = (500);
        public const int IVIFGEN_VAL_FM_SOURCE_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for attribute IVIFGEN_ATTR_FM_INTERNAL_WAVEFORM -*/
        public const int IVIFGEN_VAL_FM_INTERNAL_SINE = (1);
        public const int IVIFGEN_VAL_FM_INTERNAL_SQUARE = (2);
        public const int IVIFGEN_VAL_FM_INTERNAL_TRIANGLE = (3);
        public const int IVIFGEN_VAL_FM_INTERNAL_RAMP_UP = (4);
        public const int IVIFGEN_VAL_FM_INTERNAL_RAMP_DOWN = (5);

        public const int IVIFGEN_VAL_FM_INTERNAL_WFM_CLASS_EXT_BASE = (500);
        public const int IVIFGEN_VAL_FM_INTERNAL_WFM_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for attribute IVIFGEN_ATTR_BINARY_ALIGNMENT -*/
        public const int IVIFGEN_VAL_BINARY_ALIGNMENT_LEFT = (0);
        public const int IVIFGEN_VAL_BINARY_ALIGNMENT_RIGHT = (1);

        public const int IVIFGEN_VAL_BINARY_ALIGNMENT_CLASS_EXT_BASE = (500);
        public const int IVIFGEN_VAL_BINARY_ALIGNMENT_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for attribute IVIFGEN_ATTR_OUTPUT_TERMINAL_CONFIGURATION -*/
        public const int IVIFGEN_VAL_TERMINAL_CONFIGURATION_SINGLE_ENDED = (0);
        public const int IVIFGEN_VAL_TERMINAL_CONFIGURATION_DIFFERENTIAL = (1);

        public const int IVIFGEN_VAL_TERMINAL_CONFIGURATION_CLASS_EXT_BASE = (500);
        public const int IVIFGEN_VAL_TERMINAL_CONFIGURATION_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for attribute IVIFGEN_ATTR_START_TRIGGER_SLOPE -*/
        /*- Defined values for attribute IVIFGEN_ATTR_STOP_TRIGGER_SLOPE -*/
        /*- Defined values for attribute IVIFGEN_ATTR_HOLD_TRIGGER_SLOPE -*/
        /*- Defined values for attribute IVIFGEN_ATTR_RESUME_TRIGGER_SLOPE -*/
        /*- Defined values for attribute IVIFGEN_ATTR_ADVANCE_TRIGGER_SLOPE -*/
        public const int IVIFGEN_VAL_TRIGGER_POSITIVE = (0);
        public const int IVIFGEN_VAL_TRIGGER_NEGATIVE = (1);
        public const int IVIFGEN_VAL_TRIGGER_EITHER = (2);

        public const int IVIFGEN_VAL_TRIGGER_CLASS_EXT_BASE = (500);
        public const int IVIFGEN_VAL_TRIGGER_SPECIFIC_EXT_BASE = (1000);

        /*- Defined values for waveformHandle parameter for function IviFgen_ClearArbWaveform -*/
        public const int IVIFGEN_VAL_ALL_WAVEFORMS = (-1);

        /*- Defined values for sequenceHandle parameter for function IviFgen_ClearArbSequence -*/
        public const int IVIFGEN_VAL_ALL_SEQUENCES = (-1);


    }
}
