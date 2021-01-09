using System.Runtime.InteropServices;

namespace Native
{
    [StructLayout(LayoutKind.Sequential)]
    public struct SOCKADDR
    {
        public ADDRESS_FAMILY sa_family;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 14)]
        public char[] sa_data;
    }
}
