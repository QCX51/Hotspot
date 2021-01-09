using System.Runtime.InteropServices;

namespace Native
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct WLAN_HOSTED_NETWORK_RADIO_STATE
    {
        public DOT11_RADIO_STATE dot11SoftwareRadioState;
        public DOT11_RADIO_STATE dot11HardwareRadioState;
    }
}
