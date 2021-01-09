using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Native
{
    public struct ONEX_EAP_ERROR
    {
        uint dwWinError;
        EAP_METHOD_TYPE type;
        uint dwReasonCode;
        Guid rootCauseGuid;
        Guid repairGuid;
        Guid helpLinkGuid;
        uint fRootCauseString;
        uint fRepairString;
        ONEX_VARIABLE_BLOB RootCauseString;
        ONEX_VARIABLE_BLOB RepairString;
    }
}
