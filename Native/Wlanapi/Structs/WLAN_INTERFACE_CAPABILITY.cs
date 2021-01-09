using System.Runtime.InteropServices;
namespace Native
{
    public struct WLAN_INTERFACE_CAPABILITY
    {
        public WLAN_INTERFACE_TYPE interfaceType;
        public bool bDot11DSupported;
        public uint dwMaxDesiredSsidListSize;
        public uint dwMaxDesiredBssidListSize;
        public uint dwNumberOfSupportedPhys;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
        public DOT11_PHY_TYPE[] dot11PhyTypes;
    }
}
