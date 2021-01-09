using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Native
{
    public struct WLAN_RAW_DATA
    {
        uint dwDataSize;
        byte[] DataBlob;
    }
}
