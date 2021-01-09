
using System.Runtime.InteropServices;
namespace Native
{
    [StructLayout(LayoutKind.Sequential)]
    public struct EAP_TYPE
    {
        public byte type;
        public uint dwVendorId;
        public uint dwVendorType;
    }
}
