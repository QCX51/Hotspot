using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Native
{
    public struct WLAN_MAC_FRAME_STATISTICS
    {
        public ulong ullTransmittedFrameCount;
        public ulong ullReceivedFrameCount;
        public ulong ullWEPExcludedCount;
        public ulong ullTKIPLocalMICFailures;
        public ulong ullTKIPReplays;
        public ulong ullTKIPICVErrorCount;
        public ulong ullCCMPReplays;
        public ulong ullCCMPDecryptErrors;
        public ulong ullWEPUndecryptableCount;
        public ulong ullWEPICVErrorCount;
        public ulong ullDecryptSuccessCount;
        public ulong ullDecryptFailureCount;
    }
}
