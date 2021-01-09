
using System.Runtime.InteropServices;

namespace Native
{
    [StructLayout(LayoutKind.Sequential)]
    public struct NL_BANDWIDTH_INFORMATION
    {
        public ulong Bandwidth;
        public ulong Instability;
        public bool BandwidthPeaked;
    }
}
