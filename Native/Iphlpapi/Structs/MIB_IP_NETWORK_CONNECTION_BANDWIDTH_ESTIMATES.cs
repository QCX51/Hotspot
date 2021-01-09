using System.Runtime.InteropServices;

namespace Native
{
    [StructLayout(LayoutKind.Sequential)]
    public struct MIB_IP_NETWORK_CONNECTION_BANDWIDTH_ESTIMATES
    {
        public NL_BANDWIDTH_INFORMATION InboundBandwidthInformation;
        public NL_BANDWIDTH_INFORMATION OutboundBandwidthInformation;
    }
}
