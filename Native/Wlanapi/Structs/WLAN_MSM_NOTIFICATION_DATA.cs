using System.Runtime.InteropServices;

namespace Native
{
    public struct WLAN_MSM_NOTIFICATION_DATA
    {
        public WLAN_CONNECTION_MODE wlanConnectionMode;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256, ArraySubType = UnmanagedType.U1)]
        public byte[] strProfileName;
        public DOT11_SSID dot11Ssid;
        public DOT11_BSS_TYPE dot11BssType;
        public DOT11_MAC_ADDRESS dot11MacAddr;
        public bool bSecurityEnabled;
        public bool bFirstPeer;
        public bool bLastPeer;
        public uint wlanReasonCode;
    }
}
