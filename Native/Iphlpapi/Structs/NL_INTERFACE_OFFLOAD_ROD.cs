
using System.Runtime.InteropServices;

namespace Native
{
    [StructLayout(LayoutKind.Sequential)]
    public struct NL_INTERFACE_OFFLOAD_ROD
    {
        public bool NlChecksumSupported;
        public bool NlOptionsSupported;
        public bool TlDatagramChecksumSupported;
        public bool TlStreamChecksumSupported;
        public bool TlStreamOptionsSupported;
        public bool FastPathCompatible;
        public bool TlLargeSendOffloadSupported;
        public bool TlGiantSendOffloadSupported;
    }
}
