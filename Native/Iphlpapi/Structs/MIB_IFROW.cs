using System.Runtime.InteropServices;

namespace Native
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct MIB_IFROW
    {
        private const int MAX_INTERFACE_NAME_LEN = 256;
        private const int MAXLEN_PHYSADDR = 8;
        private const int MAXLEN_IFDESCR = 32;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_INTERFACE_NAME_LEN)]
        public char[] wszName;
        public uint dwIndex;
        public uint dwType;
        public uint dwMtu;
        public uint dwSpeed;
        public uint dwPhysAddrLen;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAXLEN_PHYSADDR)]
        public byte[] bPhysAddr;
        public uint dwAdminStatus;
        public uint dwOperStatus;
        public uint dwLastChange;
        public uint dwInOctets;
        public uint dwInUcastPkts;
        public uint dwInNUcastPkts;
        public uint dwInDiscards;
        public uint dwInErrors;
        public uint dwInUnknownProtos;
        public uint dwOutOctets;
        public uint dwOutUcastPkts;
        public uint dwOutNUcastPkts;
        public uint dwOutDiscards;
        public uint dwOutErrors;
        public uint dwOutQLen;
        public uint dwDescrLen;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAXLEN_IFDESCR)]
        public byte[] bDescr;
    }
    /*
    public struct NET_IF_NETWORK_GUID
    {
        public uint Data1;
        public uint Data2;
        public uint Data3;
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U1, SizeConst = 8)]
        public byte[] Data4;
    }
    */
}
