using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Native
{
    public struct WLAN_COUNTRY_OR_REGION_STRING_LIST
    {
        uint dwNumberOfItems;
        char[] pCountryOrRegionStringList;
    }
}
