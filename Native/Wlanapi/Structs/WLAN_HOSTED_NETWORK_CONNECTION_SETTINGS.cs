using System;
using System.Runtime.InteropServices;

namespace Native
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)] //, CharSet =  CharSet.Unicode)]
    public struct WLAN_HOSTED_NETWORK_CONNECTION_SETTINGS
    {
        public DOT11_SSID hostedNetworkSSID;
        public uint dwMaxNumberOfPeers; // DWORD
    }
}
