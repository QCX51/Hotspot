namespace Native
{
    public struct WLAN_STATISTICS
    {
        public ulong ullFourWayHandshakeFailures;
        public ulong ullTKIPCounterMeasuresInvoked;
        public ulong ullReserved;
        public WLAN_MAC_FRAME_STATISTICS MacUcastCounters;
        public WLAN_MAC_FRAME_STATISTICS MacMcastCounters;
        public uint dwNumberOfPhys;
        public WLAN_PHY_FRAME_STATISTICS PhyCounters;
    }
}
