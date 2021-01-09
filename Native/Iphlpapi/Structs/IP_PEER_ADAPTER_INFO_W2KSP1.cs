
using System;

namespace Native
{
    public struct IP_PER_ADAPTER_INFO_W2KSP1
    {
        public uint AutoconfigEnabled;
        public uint AutoconfigActive;
        public IntPtr CurrentDnsServer;
        public IP_ADDR_STRING DnsServerList;
    }
}
