using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Native
{
    public struct WLAN_BSS_ENTRY
    {
        DOT11_SSID dot11Ssid;
        ulong uPhyId;
        DOT11_MAC_ADDRESS dot11Bssid;
        DOT11_BSS_TYPE dot11BssType;
        DOT11_PHY_TYPE dot11BssPhyType;
        long lRssi;
        ulong uLinkQuality;
        bool bInRegDomain;
        ushort usBeaconPeriod;
        ulong ullTimestamp;
        ulong ullHostTimestamp;
        ushort usCapabilityInformation;
        ulong ulChCenterFrequency;
        WLAN_RATE_SET wlanRateSet;
        ulong ulIeOffset;
        ulong ulIeSize;
    }
}
