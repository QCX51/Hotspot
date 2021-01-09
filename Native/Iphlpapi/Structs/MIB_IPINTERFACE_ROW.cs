using System.Runtime.InteropServices;

namespace Native
{
    [StructLayout(LayoutKind.Sequential)]
    public struct MIB_IPINTERFACE_ROW
    {
        private const int ScopeLevelCount = 16;
        public ADDRESS_FAMILY Family;
        public NET_LUID InterfaceLuid;
        public uint InterfaceIndex;
        public uint MaxReassemblySize;
        public ulong InterfaceIdentifier;
        public uint MinRouterAdvertisementInterval;
        public uint MaxRouterAdvertisementInterval;
        public bool AdvertisingEnabled;
        public bool ForwardingEnabled;
        public bool WeakHostSend;
        public bool WeakHostReceive;
        public bool UseAutomaticMetric;
        public bool UseNeighborUnreachabilityDetection;
        public bool ManagedAddressConfigurationSupported;
        public bool OtherStatefulConfigurationSupported;
        public bool AdvertiseDefaultRoute;
        public NL_ROUTER_DISCOVERY_BEHAVIOR RouterDiscoveryBehavior;
        public uint DadTransmits;
        public uint BaseReachableTime;
        public uint RetransmitTime;
        public uint PathMtuDiscoveryTimeout;
        public NL_LINK_LOCAL_ADDRESS_BEHAVIOR LinkLocalAddressBehavior;
        public uint LinkLocalAddressTimeout;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = ScopeLevelCount)]
        public uint[] ZoneIndices;
        //public uint ZoneIndice0, ZoneIndice1, ZoneIndice2, ZoneIndice3, ZoneIndice4, ZoneIndice5, ZoneIndice6, ZoneIndice7,
        //     ZoneIndice8, ZoneIndice9, ZoneIndice10, ZoneIndice11, ZoneIndice12, ZoneIndice13, ZoneIndice14, ZoneIndice15;
        public uint SitePrefixLength;
        public uint Metric;
        public uint NlMtu;
        public bool Connected;
        public bool SupportsWakeUpPatterns;
        public bool SupportsNeighborDiscovery;
        public bool SupportsRouterDiscovery;
        public uint ReachableTime;
        public NL_INTERFACE_OFFLOAD_ROD TransmitOffload;
        public NL_INTERFACE_OFFLOAD_ROD ReceiveOffload;
        public bool DisableDefaultRoutes;
    }
}
