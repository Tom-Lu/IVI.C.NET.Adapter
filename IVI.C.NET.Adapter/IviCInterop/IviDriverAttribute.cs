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
    public class IviDriverAttribute
    {
        public const int VI_SUCCESS = (0);
        public const string VI_NULL = null;
        public const int VI_TRUE = (1);
        public const int VI_FALSE = (0);

        public const int IVI_VAL_MAX_TIME_INFINITE = 0xFFFFFFF;
        public const int IVI_VAL_MAX_TIME_IMMEDIATE = 0;

        public const int IVI_ATTR_BASE = 1000000;
        public const int IVI_ENGINE_PRIVATE_ATTR_BASE = (IVI_ATTR_BASE + 00000);   /* base for private attributes of the IVI engine */
        public const int IVI_ENGINE_PUBLIC_ATTR_BASE = (IVI_ATTR_BASE + 50000);   /* base for public attributes of the IVI engine */
        public const int IVI_SPECIFIC_PUBLIC_ATTR_BASE = (IVI_ATTR_BASE + 150000);   /* base for public attributes of specific drivers */
        public const int IVI_SPECIFIC_PRIVATE_ATTR_BASE = (IVI_ATTR_BASE + 200000);   /* base for private attributes of specific drivers */
        /* This value was changed from IVI_ATTR_BASE + 10000 in the version of this file released in August 2013 (ICP 4.6). */
        /* A private attribute, by its very definition, should not be passed to another module; it should stay private to the compiled module. */

        public const int IVI_CLASS_PUBLIC_ATTR_BASE = (IVI_ATTR_BASE + 250000);   /* base for public attributes of class drivers */
        public const int IVI_CLASS_PRIVATE_ATTR_BASE = (IVI_ATTR_BASE + 300000);   /* base for private attributes of class drivers */
        /* This value was changed from IVI_ATTR_BASE + 20000 in the version of this file released in August 2013 (ICP 4.6). */
        /* A private attribute, by its very definition, should not be passed to another module; it should stay private to the compiled module. */


        /*****************************************************************************/
        /*= Public IVI engine attributes                                    ======== */
        /*= The data type of each attribute is listed, followed by the      ======== */
        /*= default value or a remark.                                      ======== */
        /*= Note:  "hidden" means not readable or writable by the end-user. ======== */
        /*****************************************************************************/
        public const uint IVI_ATTR_NONE =                  /* no attribute */              0xFFFFFFFF;

        public const int IVI_ATTR_RANGE_CHECK =          /* ViBoolean, VI_TRUE */        (IVI_ENGINE_PUBLIC_ATTR_BASE + 2);
        public const int IVI_ATTR_QUERY_INSTRUMENT_STATUS = /* ViBoolean, VI_FALSE */      (IVI_ENGINE_PUBLIC_ATTR_BASE + 3);   /* If VI_TRUE, the specific driver is allowed to query the instrument error status after each operation */
        public const int IVI_ATTR_CACHE =               /* ViBoolean, VI_TRUE */        (IVI_ENGINE_PUBLIC_ATTR_BASE + 4);
        public const int IVI_ATTR_SIMULATE =           /* ViBoolean, VI_FALSE */       (IVI_ENGINE_PUBLIC_ATTR_BASE + 5);
        public const int IVI_ATTR_RECORD_COERCIONS =   /* ViBoolean, VI_FALSE */       (IVI_ENGINE_PUBLIC_ATTR_BASE + 6);
        public const int IVI_ATTR_DRIVER_SETUP =       /* ViString,  "" */             (IVI_ENGINE_PUBLIC_ATTR_BASE + 7);

        public const int IVI_ATTR_INTERCHANGE_CHECK =     /* ViBoolean, VI_TRUE */        (IVI_ENGINE_PUBLIC_ATTR_BASE + 21);
        public const int IVI_ATTR_SPY =                 /* ViBoolean, VI_FALSE */       (IVI_ENGINE_PUBLIC_ATTR_BASE + 22);
        public const int IVI_ATTR_USE_SPECIFIC_SIMULATION = /* ViBoolean */               (IVI_ENGINE_PUBLIC_ATTR_BASE + 23);  /* simulate values in specific rather than class driver;  default is VI_TRUE if session opened through class driver, VI_FALSE otherwise */

        public const int IVI_ATTR_PRIMARY_ERROR =         /* ViInt32  */                  (IVI_ENGINE_PUBLIC_ATTR_BASE + 101);
        public const int IVI_ATTR_SECONDARY_ERROR =       /* VIInt32  */                  (IVI_ENGINE_PUBLIC_ATTR_BASE + 102);
        public const int IVI_ATTR_ERROR_ELABORATION =    /* ViString */                  (IVI_ENGINE_PUBLIC_ATTR_BASE + 103);

        public const int IVI_ATTR_CHANNEL_COUNT =         /* ViInt32,  not writable*/     (IVI_ENGINE_PUBLIC_ATTR_BASE + 203);   /* set by the specific-driver;  default: 1 */

        public const int IVI_ATTR_CLASS_DRIVER_PREFIX =  /* ViString, not writable*/     (IVI_ENGINE_PUBLIC_ATTR_BASE + 301);  /* instrument prefix for the class driver;  empty string if not using class driver */
        public const int IVI_ATTR_SPECIFIC_DRIVER_PREFIX = /* ViString, not writable*/     (IVI_ENGINE_PUBLIC_ATTR_BASE + 302);  /* instrument prefix for the specific driver */
        public const int IVI_ATTR_SPECIFIC_DRIVER_LOCATOR = /* ViString, not writable*/     (IVI_ENGINE_PUBLIC_ATTR_BASE + 303);  /* the pathnname of the specific driver code module; from the configuration file */
        public const int IVI_ATTR_IO_RESOURCE_DESCRIPTOR = /* ViString, not writable*/     (IVI_ENGINE_PUBLIC_ATTR_BASE + 304);  /* the string that describes how to find the physical instrument; from the configuration file */
        public const int IVI_ATTR_LOGICAL_NAME =     /* ViString, not writable*/     (IVI_ENGINE_PUBLIC_ATTR_BASE + 305);  /* for class drivers, the logical name used in the Init call to identify the specific instrument */
        public const int IVI_ATTR_VISA_RM_SESSION =     /* ViSession,hidden      */     (IVI_ENGINE_PUBLIC_ATTR_BASE + 321);
        public const int IVI_ATTR_SYSTEM_IO_SESSION =    /* ViSession,not user-writable*/(IVI_ENGINE_PUBLIC_ATTR_BASE + 322);
        public const int IVI_ATTR_IO_SESSION_TYPE =    /* ViString, not user-writable*/(IVI_ENGINE_PUBLIC_ATTR_BASE + 324);
        public const int IVI_ATTR_SYSTEM_IO_TIMEOUT =   /* ViInt32 */                   (IVI_ENGINE_PUBLIC_ATTR_BASE + 325);
        public const int IVI_ATTR_SUPPORTED_INSTRUMENT_MODELS =  /* ViString, not user-writable*/(IVI_ENGINE_PUBLIC_ATTR_BASE + 327);

        public const int IVI_ATTR_GROUP_CAPABILITIES =  /* ViString, not user-writable*/(IVI_ENGINE_PUBLIC_ATTR_BASE + 401);
        public const int IVI_ATTR_FUNCTION_CAPABILITIES = /* ViString, not user-writable*/(IVI_ENGINE_PUBLIC_ATTR_BASE + 402);

        public const int IVI_ATTR_ENGINE_MAJOR_VERSION =     /* ViInt32,  not user-writable*/(IVI_ENGINE_PUBLIC_ATTR_BASE + 501);
        public const int IVI_ATTR_ENGINE_MINOR_VERSION =     /* ViInt32,  not user-writable*/(IVI_ENGINE_PUBLIC_ATTR_BASE + 502);
        public const int IVI_ATTR_SPECIFIC_DRIVER_MAJOR_VERSION = /* ViInt32,  not user-writable*/(IVI_ENGINE_PUBLIC_ATTR_BASE + 503);
        public const int IVI_ATTR_SPECIFIC_DRIVER_MINOR_VERSION = /* ViInt32,  not user-writable*/(IVI_ENGINE_PUBLIC_ATTR_BASE + 504);
        public const int IVI_ATTR_CLASS_DRIVER_MAJOR_VERSION = /* ViInt32,  not user-writable*/(IVI_ENGINE_PUBLIC_ATTR_BASE + 505);
        public const int IVI_ATTR_CLASS_DRIVER_MINOR_VERSION = /* ViInt32,  not user-writable*/(IVI_ENGINE_PUBLIC_ATTR_BASE + 506);

        public const int IVI_ATTR_INSTRUMENT_FIRMWARE_REVISION = /* ViString, not user-writable*/(IVI_ENGINE_PUBLIC_ATTR_BASE + 510);
        public const int IVI_ATTR_INSTRUMENT_MANUFACTURER =   /* ViString, not user-writable*/(IVI_ENGINE_PUBLIC_ATTR_BASE + 511);
        public const int IVI_ATTR_INSTRUMENT_MODEL =         /* ViString, not user-writable*/(IVI_ENGINE_PUBLIC_ATTR_BASE + 512);
        public const int IVI_ATTR_SPECIFIC_DRIVER_VENDOR =    /* ViString, not user-writable*/(IVI_ENGINE_PUBLIC_ATTR_BASE + 513);
        public const int IVI_ATTR_SPECIFIC_DRIVER_DESCRIPTION = /* ViString, not user-writable*/(IVI_ENGINE_PUBLIC_ATTR_BASE + 514);
        public const int IVI_ATTR_SPECIFIC_DRIVER_CLASS_SPEC_MAJOR_VERSION = /* ViInt32, not user-writable*/(IVI_ENGINE_PUBLIC_ATTR_BASE + 515);
        public const int IVI_ATTR_SPECIFIC_DRIVER_CLASS_SPEC_MINOR_VERSION = /* ViInt32, not user-writable*/(IVI_ENGINE_PUBLIC_ATTR_BASE + 516);
        public const int IVI_ATTR_CLASS_DRIVER_VENDOR =      /* ViString, not user-writable*/(IVI_ENGINE_PUBLIC_ATTR_BASE + 517);
        public const int IVI_ATTR_CLASS_DRIVER_DESCRIPTION =  /* ViString, not user-writable*/(IVI_ENGINE_PUBLIC_ATTR_BASE + 518);
        public const int IVI_ATTR_CLASS_DRIVER_CLASS_SPEC_MAJOR_VERSION = /* ViInt32, not user-writable*/(IVI_ENGINE_PUBLIC_ATTR_BASE + 519);
        public const int IVI_ATTR_CLASS_DRIVER_CLASS_SPEC_MINOR_VERSION = /* ViInt32, not user-writable*/(IVI_ENGINE_PUBLIC_ATTR_BASE + 520);

        public const int IVI_ATTR_SPECIFIC_DRIVER_REVISION =  /* ViString, not user-writable*/(IVI_ENGINE_PUBLIC_ATTR_BASE + 551);
        public const int IVI_ATTR_CLASS_DRIVER_REVISION =  /* ViString, not user-writable*/(IVI_ENGINE_PUBLIC_ATTR_BASE + 552);
        public const int IVI_ATTR_ENGINE_REVISION =      /* ViString, not user-writable*/(IVI_ENGINE_PUBLIC_ATTR_BASE + 553);

        public const int IVI_ATTR_OPC_CALLBACK =       /* ViAddr,    hidden */     (IVI_ENGINE_PUBLIC_ATTR_BASE + 602);  /* see Ivi_OPCCallbackPtr  typedef */
        public const int IVI_ATTR_CHECK_STATUS_CALLBACK =   /* ViAddr,    hidden */     (IVI_ENGINE_PUBLIC_ATTR_BASE + 603);  /* see Ivi_CheckStatusCallbackPtr  typedef */

        public const int IVI_ATTR_USER_INTERCHANGE_CHECK_CALLBACK = /* ViAddr, hidden */   (IVI_ENGINE_PUBLIC_ATTR_BASE + 801);  /* see Ivi_InterchangeCheckCallbackPtr typedef */

    }
}
