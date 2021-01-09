using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Native
{
    public struct WLAN_CONNECTION_NOTIFICATION_DATA
    {
        public WLAN_CONNECTION_MODE wlanConnectionMode;
        public char[] strProfileName;
        public DOT11_SSID dot11Ssid;
        public DOT11_BSS_TYPE dot11BssType;
        public bool bSecurityEnabled;
        public uint wlanReasonCode;
        public uint dwFlags;
        public char[] strProfileXml;
    }
}
