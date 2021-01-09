using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Native
{
    public struct WLAN_BSS_LIST
    {
        uint dwTotalSize;
        uint dwNumberOfItems;
        WLAN_BSS_ENTRY[] wlanBssEntries;
    }
}
