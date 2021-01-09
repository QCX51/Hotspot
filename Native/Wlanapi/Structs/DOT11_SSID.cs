using System.Runtime.InteropServices;

namespace Native
{
    [StructLayout(LayoutKind.Sequential)] //, CharSet = CharSet.Ansi)]
    public struct DOT11_SSID
    {
        private const int DOT11_SSID_MAX_LENGTH = 32;
        /// ULONG->unsigned int
        public uint uSSIDLength; //uint

        /// UCHAR[]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string ucSSID;
        //[MarshalAs(UnmanagedType.ByValArray, SizeConst = DOT11_SSID_MAX_LENGTH)]
        //public char[] ucSSID;
    }
}
