
namespace Native
{
    public struct WLAN_PHY_FRAME_STATISTICS
    {
        public ulong ullTransmittedFrameCount;
        public ulong ullMulticastTransmittedFrameCount;
        public ulong ullFailedCount;
        public ulong ullRetryCount;
        public ulong ullMultipleRetryCount;
        public ulong ullMaxTXLifetimeExceededCount;
        public ulong ullTransmittedFragmentCount;
        public ulong ullRTSSuccessCount;
        public ulong ullRTSFailureCount;
        public ulong ullACKFailureCount;
        public ulong ullReceivedFrameCount;
        public ulong ullMulticastReceivedFrameCount;
        public ulong ullPromiscuousReceivedFrameCount;
        public ulong ullMaxRXLifetimeExceededCount;
        public ulong ullFrameDuplicateCount;
        public ulong ullReceivedFragmentCount;
        public ulong ullPromiscuousReceivedFragmentCount;
        public ulong ullFCSErrorCount;
    }
}
