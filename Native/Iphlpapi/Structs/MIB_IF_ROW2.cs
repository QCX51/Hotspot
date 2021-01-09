
using System.Runtime.InteropServices;
namespace Native
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct MIB_IF_ROW2
    {
        private const int IF_MAX_GUID_LENGTH = 16;
        private const int IF_MAX_STRING_SIZE = 256;
        private const int IF_MAX_PHYS_ADDRESS_LENGTH = 32;

        public NET_LUID InterfaceLuid;
        public uint InterfaceIndex;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IF_MAX_GUID_LENGTH)]
        public byte[] InterfaceGuid;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IF_MAX_STRING_SIZE + 1)]
        public char[] Alias;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IF_MAX_STRING_SIZE + 1)]
        public char[] Description;
        public uint PhysicalAddressLength;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IF_MAX_PHYS_ADDRESS_LENGTH)]
        public byte[] PhysicalAddress;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IF_MAX_PHYS_ADDRESS_LENGTH)]
        public byte[] PermanentPhysicalAddress;
        public uint Mtu;
        public IFTYPE Type;
        public TUNNEL_TYPE TunnelType;
        public NDIS_MEDIUM MediaType;
        public NDIS_PHYSICAL_MEDIUM PhysicalMediumType;
        public NET_IF_ACCESS_TYPE AccessType;
        public NET_IF_DIRECTION_TYPE DirectionType;
        public struct _InterfaceAndOperStatusFlags
        {
            public bool HardwareInterface;
            public bool FilterInterface;
            public bool ConnectorPresent;
            public bool NotAuthenticated;
            public bool NotMediaConnected;
            public bool Paused;
            public bool LowPower;
            public bool EndPointInterface;
        }
        public _InterfaceAndOperStatusFlags InterfaceAndOperStatusFlags;

        public IF_OPER_STATUS OperStatus;
        public NET_IF_ADMIN_STATUS AdminStatus;
        public NET_IF_MEDIA_CONNECT_STATE MediaConnectState;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IF_MAX_GUID_LENGTH)]
        public byte[] NetworkGuid;
        public NET_IF_CONNECTION_TYPE ConnectionType;
        public ulong TransmitLinkSpeed;
        public ulong ReceiveLinkSpeed;
        public ulong InOctets;
        public ulong InUcastPkts;
        public ulong InNUcastPkts;
        public ulong InDiscards;
        public ulong InErrors;
        public ulong InUnknownProtos;
        public ulong InUcastOctets;
        public ulong InMulticastOctets;
        public ulong InBroadcastOctets;
        public ulong OutOctets;
        public ulong OutUcastPkts;
        public ulong OutNUcastPkts;
        public ulong OutDiscards;
        public ulong OutErrors;
        public ulong OutUcastOctets;
        public ulong OutMulticastOctets;
        public ulong OutBroadcastOctets;
        public ulong OutQLen;
    }
}
