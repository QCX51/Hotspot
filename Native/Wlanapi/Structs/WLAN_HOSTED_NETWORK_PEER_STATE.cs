using System.Runtime.InteropServices;

namespace Native
{
    [StructLayout(LayoutKind.Sequential)]
    public struct WLAN_HOSTED_NETWORK_PEER_STATE
    {
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U1, SizeConst = 6)]
        private readonly byte[] PeerMacAddress;
        //public DOT11_MAC_ADDRESS PeerMacAddress;
        public WLAN_HOSTED_NETWORK_PEER_AUTH_STATE PeerAuthState;
        public System.Net.NetworkInformation.PhysicalAddress PhysicalAddress
        { get { return new System.Net.NetworkInformation.PhysicalAddress(PeerMacAddress); } }
    }
}
