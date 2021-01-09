using System.Runtime.InteropServices;

namespace Native
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct DOT11_MAC_ADDRESS
    {
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U1, SizeConst = 6)]
        public byte[] PhysicalAddress;
        public System.Net.NetworkInformation.PhysicalAddress HardwareID
        { get { return new System.Net.NetworkInformation.PhysicalAddress(PhysicalAddress); } }
        //public byte one;
        //public byte two;
        //public byte three;
        //public byte four;
        //public byte five;
        //public byte six;
    }
}
