using System.Runtime.InteropServices;
namespace Native
{
    [StructLayout(LayoutKind.Sequential)]
    public struct WLAN_PHY_RADIO_STATE
    {
        public uint dwPhyIndex;
        public DOT11_RADIO_STATE dot11SoftwareRadioState;
        public DOT11_RADIO_STATE dot11HardwareRadioState;
    }
}
