
using System;
using System.Runtime.InteropServices;

namespace Native
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct IP_ADAPTER_INFO
    {
        public IntPtr Next;
        public int ComboIndex;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)IPTypes.MAX_ADAPTER_NAME_LENGTH + 4)]
        public string AdapterName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)IPTypes.MAX_ADAPTER_DESCRIPTION_LENGTH + 4)]
        public string AdapterDescription;
        public uint AddressLength;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)IPTypes.MAX_ADAPTER_ADDRESS_LENGTH)]
        public byte[] Address;
        public int Index;
        public uint Type;
        public uint DhcpEnabled;
        public IntPtr CurrentIpAddress;
        public IP_ADDR_STRING IpAddressList;
        public IP_ADDR_STRING GatewayList;
        public IP_ADDR_STRING DhcpServer;
        public bool HaveWins;
        public IP_ADDR_STRING PrimaryWinsServer;
        public IP_ADDR_STRING SecondaryWinsServer;
        public int LeaseObtained;
        public int LeaseExpires;
    }
}
