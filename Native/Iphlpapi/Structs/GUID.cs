
using System.Runtime.InteropServices;
namespace Native
{
    [StructLayout(LayoutKind.Sequential)]
    public struct GUID
    {
        public int Data1;
        public short Data2;
        public short Data3;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] Data4;
    }
}
