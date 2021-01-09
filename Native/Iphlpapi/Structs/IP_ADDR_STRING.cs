
using System;
using System.Runtime.InteropServices;

namespace Native
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct IP_ADDR_STRING
    {
        public IntPtr Next;
        public IP_ADDRESS_STRING IpAddress;
        public IP_ADDRESS_STRING IpMask;
        public int Context;
    }
}
