using System.Runtime.InteropServices;

namespace Native
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct IP_ADDRESS_STRING
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string Address;
    }
}
