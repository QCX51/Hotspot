using System;
using System.Runtime.InteropServices;

namespace Native
{
    public enum IP_DAD_STATE
    {
        IpDadStateInvalid = 0,
        IpDadStateTentative,
        IpDadStateDuplicate,
        IpDadStateDeprecated,
        IpDadStatePreferred
    }
    public enum IP_SUFFIX_ORIGIN
    {
        IpSuffixOriginOther = 0,
        IpSuffixOriginManual,
        IpSuffixOriginWellKnown,
        IpSuffixOriginDhcp,
        IpSuffixOriginLinkLayerAddress,
        IpSuffixOriginRandom
    }

    public enum IP_PREFIX_ORIGIN
    {
        IpPrefixOriginOther = 0,
        IpPrefixOriginManual,
        IpPrefixOriginWellKnown,
        IpPrefixOriginDhcp,
        IpPrefixOriginRouterAdvertisement
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct IP_ADAPTER_UNICAST_ADDRESS_LH
    {
        public ulong Alignment;
        public uint Length;
        public ushort Flags;
        public IntPtr Next;
        public SOCKET_ADDRESS Address;
        public IP_PREFIX_ORIGIN PrefixOrigin;
        public IP_SUFFIX_ORIGIN SuffixOrigin;
        public IP_DAD_STATE DadState;
        public uint ValidLifetime;
        public uint PreferredLifetime;
        public uint LeaseLifetime;
        public ushort OnLinkPrefixLength;
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct IP_ADAPTER_ADDRESSES
    {
        public ulong Alignment;
        public IntPtr Next;
        [MarshalAs(UnmanagedType.LPStr)]
        public string AdapterName;
        //public IntPtr AdapterName;
        public IntPtr FirstUnicastAddress;
        //public IP_ADAPTER_UNICAST_ADDRESS_LH FirstUnicastAddress;
        public IntPtr FirstAnycastAddress;
        public IntPtr FirstMulticastAddress;
        public IntPtr FirstDnsServerAddress;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string DnsSuffix;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string Description;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string FriendlyName;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)] //MAX_ADAPTER_ADDRESS_LENGTH = 8
        public byte[] PhysicalAddress;
        public uint PhysicalAddressLength;
        public uint Flags;
        public uint Mtu;
        public IFTYPE IfType;
        public IF_OPER_STATUS OperStatus;
        public uint Ipv6IfIndex;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public uint[] ZoneIndices;
        public IntPtr FirstPrefix;

        // Items added for Vista
        // May need to be removed on Windows versions below Vista to work properly (?)
        public ulong TrasmitLinkSpeed;
        public ulong ReceiveLinkSpeed;
        public IntPtr FirstWinsServerAddress;
        public IntPtr FirstGatewayAddress;
        public uint Ipv4Metric;
        public uint Ipv6Metric;
        public NET_LUID Luid;
        public SOCKET_ADDRESS Dhcpv4Server;
        public uint CompartmentId;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public byte[] NetworkGuid;
        public NET_IF_CONNECTION_TYPE ConnectionType;
        public TUNNEL_TYPE TunnelType;
        public SOCKET_ADDRESS Dhcpv6Server;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 130)] //MAX_DHCPV6_DUID_LENGTH = 130;
        public byte[] Dhcpv6ClientDuid;
        public uint Dhcpv6ClientDuidLength;
        public uint Dhcpv6Iaid;
        public uint FirstDnsSuffix;
    }
}
