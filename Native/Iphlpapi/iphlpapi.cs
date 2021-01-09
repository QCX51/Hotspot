using System;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;

/// <summary>
/// netsh interface ip delete neighbors "WI-FI" "192.168.100.1"
/// netsh interface ip add neighbors "WI-FI" "192.168.100.1" "00-13-f7-e5-33-77"
/// arp -s 192.168.100.1 00-13-f7-e5-33-77
/// </summary>
/// 
namespace Native
{
    public static partial class IPhlpapi
    {
        private const int MAX_ADAPTER_ADDRESS_LENGTH = 8;
        private const int MAX_ADAPTER_NAME_LENGTH    = 256;
        private const int MAX_DHCPV6_DUID_LENGTH     = 130;
        private const int NDIS_IF_MAX_STRING_SIZE    = 256;

        public delegate void PIPINTERFACE_CHANGE_CALLBACK(IntPtr CallerContext, IntPtr Row, MIB_NOTIFICATION_TYPE NotificationType);
        public static PIPINTERFACE_CHANGE_CALLBACK IPInterfaceChangeCallBack;
        public static IntPtr NotificationHandle;

        [DllImport("iphlpapi.dll", EntryPoint = "SendARP")]
        public static extern uint SendARP(long DestIP, long SrcIP, [Out] byte[] pMacAddr, ref int PhyAddrLen);

        [DllImport("iphlpapi.dll", EntryPoint = "GetAdaptersInfo")]
        private static extern uint GetAdaptersInfo(IntPtr AdapterInfo, [In] ref long SizePointer);

        [DllImport("iphlpapi.dll", EntryPoint = "FreeMibTable")]
        private static extern void FreeMibTable(IntPtr plpNetTable);

        /// <summary>
        /// The FlushIpNetTable2 function flushes the IP neighbor table on a local computer.
        /// </summary>
        /// <param name="Family">The address family to flush.
        /// <br />AF_INET The IPv4 address family. When this value is specified, this function flushes the neighbor IP address table that contains only IPv4 entries.
        /// <br />AF_INET6 The IPv6 address family. When this value is specified, this function flushes the neighbor IP address table that contains only IPv6 entries.
        /// <br />AF_UNSPEC The address family is unspecified. When this value is specified, this function flushes the neighbor IP address table that contains both IPv4 and IPv6 entries.
        /// </param>
        /// <param name="InterfaceIndex">The interface index. If the index is specified,
        /// the function flushes the neighbor IP address entries on a specific interface.
        /// Otherwise, the function flushes the neighbor IP address entries on all the interfaces.
        /// To ignore the interface, set this parameter to zero.</param>
        /// <returns>uint</returns>
        [DllImport("iphlpapi.dll", EntryPoint = "FlushIpNetTable2")]
        private static extern uint FlushIpNetTable2(ADDRESS_FAMILY Family, uint InterfaceIndex);

        /// <summary>
        /// The ResolveIpNetEntry2 function resolves the physical address for a neighbor IP address entry on the local computer.
        /// </summary>
        /// <param name="Row">A pointer to a <see cref="MIB_IPNET_ROW2"/> structure entry for a neighbor IP address entry.</param>
        /// <param name="SourceAddress">A pointer to a an optional source IP address used to select the interface to send the requests on for the neighbor IP address entry.</param>
        /// <returns></returns>
        [DllImport("iphlpapi.dll", EntryPoint = "ResolveIpNetEntry2")]
        private static extern uint ResolveIpNetEntry2([In, Out] ref MIB_IPNET_ROW2 Row, [In] IntPtr SourceAddress);

        /// <summary>
        /// The GetIpNetTable2 function retrieves the IP neighbor table on the local computer.
        /// </summary>
        /// <param name="Family">The address family to retrieve.
        /// The values currently supported are AF_INET, AF_INET6, and AF_UNSPEC.</param>
        /// <param name="Table">A pointer to a <see cref="MIB_IPNET_TABLE2"/> structure that contains a table of neighbor IP address entries on the local computer.</param>
        /// <returns></returns>
        [DllImport("iphlpapi.dll", EntryPoint = "GetIpNetTable2")]
        private static extern uint GetIpNetTable2(ADDRESS_FAMILY Family, [Out] out IntPtr Table);

        [DllImport("iphlpapi.dll", EntryPoint = "GetIpNetEntry2")]
        private static extern uint GetIpNetEntry2([In, Out] ref MIB_IPNET_ROW2 Row);

        [DllImport("iphlpapi.dll", EntryPoint = "ConvertInterfaceGuidToLuid")]
        public static extern uint ConvertInterfaceGuidToLuid([In, Out] ref Guid InterfaceGuid, [Out] out NET_LUID InterfaceLuid);

        [DllImport("iphlpapi.dll", EntryPoint = "ConvertInterfaceLuidToGuid")]
        public static extern uint ConvertInterfaceLuidToGuid([In, Out] ref NET_LUID InterfaceLuid, [Out] out Guid InterfaceGuid);

        [DllImport("iphlpapi.dll", EntryPoint = "ConvertInterfaceLuidToIndex")]
        public static extern uint ConvertInterfaceLuidToIndex([In, Out] ref NET_LUID InterfaceLuid, [Out] out uint InterfaceIndex);

        [DllImport("iphlpapi.dll", EntryPoint = "ConvertInterfaceIndexToLuid")]
        public static extern uint ConvertInterfaceIndexToLuid([In, Out] ref uint InterfaceIndex, [Out] out NET_LUID InterfaceLuid);

        [DllImport("iphlpapi.dll", EntryPoint = "ConvertInterfaceLuidToAlias")]
        private static extern uint ConvertInterfaceLuidToAlias([In, Out] ref NET_LUID InterfaceLuid, [In, Out] IntPtr InterfaceAlias, [In] int Length);

        [DllImport("iphlpapi.dll", EntryPoint = "ConvertInterfaceLuidToNameW")]
        private static extern uint ConvertInterfaceLuidToNameW(ref NET_LUID InterfaceLuid, [In, Out] IntPtr InterfaceName, [In] int Lenght);

        [DllImport("iphlpapi.dll", EntryPoint = "GetPerAdapterInfo")]
        private static extern uint GetPerAdapterInfo(int IfIndex, [In, Out] IntPtr pPerAdapterInfo, [In, Out] ref int pOutBufLen);

        /// <summary>
        /// The DeleteIpNetEntry2 function deletes a neighbor IP address entry on the local computer.
        /// </summary>
        /// <param name="Row">A pointer to a <see cref="MIB_IPNET_ROW2"/> structure entry for a neighbor IP address entry.
        /// On successful return, this entry will be deleted.</param>
        /// <returns></returns>
        [DllImport("iphlpapi.dll", EntryPoint = "DeleteIpNetEntry2")]
        private static extern uint DeleteIpNetEntry2([In, Out] ref MIB_IPNET_ROW2 Row);

        /// <summary>
        /// The SetIpNetEntry2 function sets the physical address of an existing neighbor IP address entry on the local computer.
        /// </summary>
        /// <param name="Row">A pointer to a <see cref="MIB_IPNET_ROW2"/> structure entry for a neighbor IP address entry.</param>
        /// <returns></returns>
        [DllImport("iphlpapi.dll", EntryPoint = "SetIpNetEntry2")]
        private static extern uint SetIpNetEntry2([In, Out] ref MIB_IPNET_ROW2 Row);

        /// <summary>
        /// The GetIfEntry2Ex function retrieves the specified level of information for the specified interface on the local computer.
        /// </summary>
        /// <param name="Level">The level of interface information to retrieve. This parameter can be one of the values from the MIB_IF_ENTRY_LEVEL enumeration type</param>
        /// <param name="Row">A pointer to a <see cref="MIB_IF_ROW2"/> structure that, on successful return, receives information for an interface on the local computer.
        /// On input, the InterfaceLuid or the InterfaceIndex member of the MIB_IF_ROW2 must be set to the interface for which to retrieve information.</param>
        /// <returns></returns>
        [DllImport("iphlpapi.dll", EntryPoint = "GetIfEntry2Ex")]
        private static extern uint GetIfEntry2Ex([In] MIB_IF_ENTRY_LEVEL Level, [In, Out] ref MIB_IF_ROW2 Row);

        [DllImport("iphlpapi.dll", EntryPoint = "GetIfEntry")]
        private static extern uint GetIfEntry([In, Out] ref MIB_IFROW Row);

        /// <summary>
        /// The GetIpNetworkConnectionBandwidthEstimates function retrieves historical bandwidth estimates for a network connection on the specified interface.
        /// </summary>
        /// <param name="InterfaceIndex">The local index value for the network interface.</param>
        /// <param name="AddressFamily">The address family.
        /// The values currently supported are AF_INET or AF_INET6, which are the Internet address family formats for IPv4 and IPv6.</param>
        /// <param name="BandwidthEstimates">A pointer to a buffer that returns the historical bandwidth estimates maintained for the point of attachment to which the interface is currently connected.</param>
        /// <returns></returns>
        /// Minimum supported client 	Windows 8 [desktop apps only]
        [DllImport("iphlpapi.dll", EntryPoint = "GetIpNetworkConnectionBandwidthEstimates")]
        private static extern uint GetIpNetworkConnectionBandwidthEstimates(uint InterfaceIndex, ADDRESS_FAMILY AddressFamily, [Out] out MIB_IP_NETWORK_CONNECTION_BANDWIDTH_ESTIMATES BandwidthEstimates);

        /// <summary>
        /// The SetIpInterfaceEntry function sets the properties of an IP interface on the local computer.
        /// </summary>
        /// <param name="Row">A pointer to a <see cref="MIB_IPINTERFACE_ROW"/> structure entry for an interface. On input,
        /// the Family member of the MIB_IPINTERFACE_ROW must be set to AF_INET6 or AF_INET and the InterfaceLuid or the InterfaceIndex member of the MIB_IPINTERFACE_ROW must be specified.</param>
        /// <returns></returns>
        [DllImport("iphlpapi.dll", EntryPoint = "SetIpInterfaceEntry")]
        private static extern uint SetIpInterfaceEntry([In, Out] ref MIB_IPINTERFACE_ROW Row);

        [DllImport("iphlpapi.dll", EntryPoint = "GetIpInterfaceEntry")]
        private static extern uint GetIpInterfaceEntry([In, Out] ref MIB_IPINTERFACE_ROW Row);

        [DllImport("iphlpapi.dll", EntryPoint = "GetIpInterfaceTable")]
        private static extern uint GetIpInterfaceTable([In] ADDRESS_FAMILY Family, [Out] out IntPtr Table);

        [DllImport("iphlpapi.dll", EntryPoint = "NotifyIpInterfaceChange")]
        private static extern uint NotifyIpInterfaceChange(ADDRESS_FAMILY Family, PIPINTERFACE_CHANGE_CALLBACK Callback, IntPtr CallerContext, bool InitialNotification, [Out] out IntPtr NotificationHandle);

        [DllImport("iphlpapi.dll", EntryPoint = "CancelMibChangeNotify2")]
        private static extern uint CancelMibChangeNotify2(IntPtr NotificationHandle);

        [DllImport("iphlpapi.dll", EntryPoint = "GetAdaptersAddresses")]
        private static extern uint GetAdaptersAddresses(ADDRESS_FAMILY Family, uint Flags, IntPtr Reserved, IntPtr pAdapterAddresses, ref uint pOutBufLen);

        /*
        public static void GetAdapterAddress()
        {
            uint outBufLen = 0; // IP_ADAPTER_ADDRESSES = 448
            IntPtr pAdapterAddresses = IntPtr.Zero;
            uint result = GetAdaptersAddresses(ADDRESS_FAMILY.AF_INET, 0, IntPtr.Zero, pAdapterAddresses, ref outBufLen);
            if (111 == result)
            {
                pAdapterAddresses = Marshal.AllocHGlobal((int)outBufLen);
                result = GetAdaptersAddresses(ADDRESS_FAMILY.AF_INET, 0, IntPtr.Zero, pAdapterAddresses, ref outBufLen);
            }
            while (result == 0 && IntPtr.Zero != pAdapterAddresses)
            {
                IP_ADAPTER_ADDRESSES addr = (IP_ADAPTER_ADDRESSES)Marshal.PtrToStructure(pAdapterAddresses, typeof(IP_ADAPTER_ADDRESSES));

                Debug.WriteLine(addr.FriendlyName);
                Debug.WriteLine(addr.Description);
                Debug.WriteLine(BitConverter.ToString(addr.PhysicalAddress, 0, 6));
                ConvertInterfaceLuidToGuid(ref addr.Luid, out Guid InterfaceGuid);
                Trace.WriteLine(InterfaceGuid.ToString());
                pAdapterAddresses = addr.Next;
            }
            Marshal.FreeHGlobal(pAdapterAddresses);
        }

        public static void GetAdapterInfo()
        {
            long StructSize = 0;
            IntPtr pAdapterInfo = IntPtr.Zero;
            uint result = GetAdaptersInfo(IntPtr.Zero, ref StructSize);
            if (result == 111)
            {
                pAdapterInfo = Marshal.AllocHGlobal(new IntPtr(StructSize));
                result = GetAdaptersInfo(pAdapterInfo, ref StructSize);
            }
            if (result != 0) { Marshal.FreeHGlobal(pAdapterInfo); return; }
            do
            {
                IP_ADAPTER_INFO infos = (IP_ADAPTER_INFO)Marshal.PtrToStructure(
                    pAdapterInfo, typeof(IP_ADAPTER_INFO));

                Trace.WriteLine(infos.AdapterName);
                Trace.WriteLine(infos.AdapterDescription);

                pAdapterInfo = infos.Next;
            }
            while (pAdapterInfo != IntPtr.Zero);
            Marshal.FreeHGlobal(pAdapterInfo);
        }

        public static bool RegisterNotifications(ADDRESS_FAMILY Family)
        {
            IPInterfaceChangeCallBack = new PIPINTERFACE_CHANGE_CALLBACK(OnNotifyIpInterfaceChange);
            uint result = NotifyIpInterfaceChange(Family, IPInterfaceChangeCallBack, IntPtr.Zero, false, out NotificationHandle);
            return result != 0 ? false : true;
        }

        public static bool DeregisterNotifications()
        {
            uint result = CancelMibChangeNotify2(NotificationHandle);
            return result != 0 ? false : true;
        }

        public static void OnNotifyIpInterfaceChange(IntPtr CallerContext, IntPtr pRow, MIB_NOTIFICATION_TYPE NotificationType)
        {
            if (pRow != IntPtr.Zero)
            {
                MIB_IPINTERFACE_ROW ipIRow = new MIB_IPINTERFACE_ROW();
                try { ipIRow = (MIB_IPINTERFACE_ROW)Marshal.PtrToStructure(pRow, typeof(MIB_IPINTERFACE_ROW)); }
                catch (Exception ex) { Trace.WriteLine(ex.Message); }
            }
        }
        */
        public static MIB_IP_NETWORK_CONNECTION_BANDWIDTH_ESTIMATES GetIpNetworkConnectionBandwidthEstimates(Guid InterfaceGuid)
        {
            ConvertInterfaceGuidToLuid(ref InterfaceGuid, out NET_LUID NetLUID);
            ConvertInterfaceLuidToIndex(ref NetLUID, out uint InterfaceIndex);
            uint result = GetIpNetworkConnectionBandwidthEstimates(InterfaceIndex, ADDRESS_FAMILY.AF_INET, out MIB_IP_NETWORK_CONNECTION_BANDWIDTH_ESTIMATES BandWidthEstimates);
            return BandWidthEstimates;
        }

        public static MIB_IPNET_ROW2 GetIpNetEntry(Guid InterfaceGuid, IPAddress IpAddress)
        {
            MIB_IPNET_ROW2 Row = GetRow(InterfaceGuid, IpAddress);
            uint result = GetIpNetEntry2(ref Row);
            if (result != 0) { Trace.WriteLine("GetIpNetEntry"); }
            PrintErrorResult(result);
            return Row;
        }

        public static MIB_IPINTERFACE_ROW GetIpInterfaceEntry(Guid InterfaceGuid, ADDRESS_FAMILY Family)
        {
            MIB_IPINTERFACE_ROW pIpRow = new MIB_IPINTERFACE_ROW();
            ConvertInterfaceGuidToLuid(ref InterfaceGuid, out NET_LUID NetLUID);
            pIpRow.InterfaceLuid = NetLUID;
            pIpRow.Family = Family; // ADDRESS_FAMILY.AF_INET || ADDRESS_FAMILY.AF_INET6
            uint result = GetIpInterfaceEntry(ref pIpRow);
            if (result != 0) { Trace.WriteLine("GetIpInterfaceEntry"); }
            PrintErrorResult(result);
            return pIpRow;
        }

        public static MIB_IPINTERFACE_TABLE GetIpInterfaceTable(ADDRESS_FAMILY Family)
        {
            uint result = GetIpInterfaceTable(Family, out IntPtr pTable);
            if (result != 0) { Trace.WriteLine("GetIpInterfaceTable"); }
            PrintErrorResult(result);
            MIB_IPINTERFACE_TABLE ITable = new MIB_IPINTERFACE_TABLE(pTable);
            FreeMibTable(pTable);
            return ITable;
        }

        public static MIB_IF_ROW2 GetIfEntry2(Guid InterfaceGuid)
        {
            MIB_IF_ROW2 pIfRow = new MIB_IF_ROW2();
            ConvertInterfaceGuidToLuid(ref InterfaceGuid, out NET_LUID NetLUID);
            pIfRow.InterfaceLuid = NetLUID;
            uint result = GetIfEntry2Ex(MIB_IF_ENTRY_LEVEL.MibIfEntryNormal, ref pIfRow);
            if (result != 0) { Trace.WriteLine("GetIfEntry2"); }
            PrintErrorResult(result);
            return pIfRow;
        }
        
        public static PhysicalAddress ResolveIpNetEntry(Guid InterfaceGUID, IPAddress address)
        {
            MIB_IPNET_ROW2 row = GetRow(InterfaceGUID, address);
            uint result = ResolveIpNetEntry2(ref row, IntPtr.Zero);
            if (result != 0) { Trace.WriteLine("ResolveIpNetEntry"); }
            PrintErrorResult(result);
            return new PhysicalAddress(row.PhysicalAddress);
        }

        public static bool DeleteIPNetEntry(Guid InterfaceGuid, IPAddress address)
        {
            MIB_IPNET_ROW2 row = GetRow(InterfaceGuid, address);
            uint result = DeleteIpNetEntry2(ref row);
            if (result != 0) { Trace.WriteLine("DeleteIpNetEntry"); }
            PrintErrorResult(result);
            return !(result != 0);
        }

        public static MIB_IFROW GetIfEntry(Guid InterfaceGuid)
        {
            MIB_IFROW pIfRow = new MIB_IFROW();
            ConvertInterfaceGuidToLuid(ref InterfaceGuid, out NET_LUID NetLUID);
            ConvertInterfaceLuidToIndex(ref NetLUID, out uint InterfaceIndex);
            pIfRow.dwIndex = InterfaceIndex;
            uint result = GetIfEntry(ref pIfRow);
            if (result != 0) { Trace.WriteLine("GetIfEntry"); }
            PrintErrorResult(result);
            return pIfRow;
        }

        private static MIB_IPNET_ROW2 GetRow(Guid InterfaceGuid, IPAddress address)
        {
            ConvertInterfaceGuidToLuid(ref InterfaceGuid, out NET_LUID NetLUID);
            MIB_IPNET_ROW2 row = new MIB_IPNET_ROW2();

            if (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                byte[] IPv4 = address.GetAddressBytes();
                row.Address.Ipv4.sin_addr0 = IPv4[0];
                row.Address.Ipv4.sin_addr1 = IPv4[1];
                row.Address.Ipv4.sin_addr2 = IPv4[2];
                row.Address.Ipv4.sin_addr3 = IPv4[3];
                row.Address.Ipv4.sin_port = 0;
                row.Address.si_family = ADDRESS_FAMILY.AF_INET;
            }
            else if (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
            {
                byte[] IPv6 = address.GetAddressBytes();
                row.Address.Ipv6.sin6_addr0 = IPv6[0];
                row.Address.Ipv6.sin6_addr1 = IPv6[1];
                row.Address.Ipv6.sin6_addr2 = IPv6[2];
                row.Address.Ipv6.sin6_addr3 = IPv6[3];
                row.Address.Ipv6.sin6_addr4 = IPv6[4];
                row.Address.Ipv6.sin6_addr5 = IPv6[5];
                row.Address.Ipv6.sin6_addr6 = IPv6[6];
                row.Address.Ipv6.sin6_addr7 = IPv6[7];
                row.Address.Ipv6.sin6_addr8 = IPv6[8];
                row.Address.Ipv6.sin6_addr9 = IPv6[9];
                row.Address.Ipv6.sin6_addr10 = IPv6[10];
                row.Address.Ipv6.sin6_addr11 = IPv6[11];
                row.Address.Ipv6.sin6_addr12 = IPv6[12];
                row.Address.Ipv6.sin6_addr13 = IPv6[13];
                row.Address.Ipv6.sin6_addr14 = IPv6[14];
                row.Address.Ipv6.sin6_addr15 = IPv6[15];
                row.Address.Ipv6.sin6_port = 0;
                row.Address.si_family = ADDRESS_FAMILY.AF_INET6;
            }
            row.InterfaceLuid = NetLUID;
            return row;
        }

        public static bool SetIpNetEntry(Guid InterfaceGuid, PhysicalAddress macaddr, IPAddress ipaddress)
        {
            MIB_IPNET_ROW2 row = GetRow(InterfaceGuid, ipaddress);

            byte[] PhysicalAddr = macaddr.GetAddressBytes();
            row.PhysicalAddress = new byte[32];
            row.PhysicalAddressLength = 32;

            row.PhysicalAddress[0] = PhysicalAddr[0];
            row.PhysicalAddress[1] = PhysicalAddr[1];
            row.PhysicalAddress[2] = PhysicalAddr[2];
            row.PhysicalAddress[3] = PhysicalAddr[3];
            row.PhysicalAddress[4] = PhysicalAddr[4];
            row.PhysicalAddress[5] = PhysicalAddr[5];

            uint result = SetIpNetEntry2(ref row);
            if (result != 0) { Trace.WriteLine("SetIpNetEntry"); }
            PrintErrorResult(result);
            return !(result != 0);
        }

        private static MIB_IPNET_ROW2[] GetIpNetTable
        {
            get
            {
                uint result;
                result = GetIpNetTable2(ADDRESS_FAMILY.AF_UNSPEC, out IntPtr plpNetTable);
                if (result != 0) return new MIB_IPNET_ROW2[0];
                MIB_IPNET_ROW2[] IpNetRows = MIB_IPNET_TABLE2.GetTable(plpNetTable);
                FreeMibTable(plpNetTable);
                return IpNetRows;
            }
        }

        public static void SetIpNetEntry(Guid InterfaceGuid, PhysicalAddress oldPhysicalAddress, PhysicalAddress newPhysicalAddress)
        {
            IPAddress IPvX = IPAddress.None; uint result;
            result = GetIpNetTable2( ADDRESS_FAMILY.AF_UNSPEC, out IntPtr plpNetTable);

            foreach (MIB_IPNET_ROW2 row in GetIpNetTable)
            {
                IPvX = row.Address.si_family.Equals(ADDRESS_FAMILY.AF_INET) ? new IPAddress(row.Address.Ipv4.Address) : new IPAddress(row.Address.Ipv6.Address);

                if (PhysicalAddress.Parse(BitConverter.ToString(row.PhysicalAddress, 0, 6)).Equals(oldPhysicalAddress))
                {
                    MIB_IPNET_ROW2 row2 = row;
                    row2.PhysicalAddress = newPhysicalAddress.GetAddressBytes();
                    if (!(SetIpNetEntry2(ref row2) != 0))
                    { Trace.WriteLine($"OldMac:{oldPhysicalAddress} NewMac:{newPhysicalAddress}"); }
                }
            }
        }

        public static void DeleteIPNetEntry(Guid InterfaceGuid, PhysicalAddress PhysicalAddress)
        {
            foreach (MIB_IPNET_ROW2 Row in GetIpNetTable)
            {
                PhysicalAddress physicalAddress = PhysicalAddress.Parse(
                    BitConverter.ToString(Row.PhysicalAddress, 0, 6));

                if (physicalAddress.Equals(PhysicalAddress))
                {
                    IPAddress IPvX = Row.Address.si_family.Equals(ADDRESS_FAMILY.AF_INET)
                    ? new IPAddress(Row.Address.Ipv4.Address)
                    : new IPAddress(Row.Address.Ipv6.Address);
                    DeleteIPNetEntry(InterfaceGuid, IPvX);
                }
            }
        }

        public static IPAddress ResolveIPNetEntry(Guid InterfaceGuid, PhysicalAddress PhysicalAddress)
        {
            foreach (MIB_IPNET_ROW2 IPNetRow in GetIpNetTable)
            {
                string HardwareID = BitConverter.ToString(IPNetRow.PhysicalAddress, 0, 6);
                if (PhysicalAddress.Parse(HardwareID).Equals(PhysicalAddress))
                {
                    PingReply reply = null;
                    PingOptions PingOptns = new PingOptions(5, true);
                    IPAddress IPvX = IPNetRow.Address.si_family.Equals(ADDRESS_FAMILY.AF_INET) ?
                    new IPAddress(IPNetRow.Address.Ipv4.Address) :
                    new IPAddress(IPNetRow.Address.Ipv6.Address);
                    try { using (Ping ping = new Ping()) { reply = ping.Send(IPvX, 1, new byte[64], PingOptns); } }
                    catch { DeleteIPNetEntry(InterfaceGuid, PhysicalAddress); }
                    if (reply != null && reply.Status.Equals(IPStatus.Success)) return IPvX;
                }
            }
            return IPAddress.None;
        }

        public static string ConvertInterfaceGuidToAlias(Guid InterfaceGuid)
        {
            string InterfaceAlias; int Length = NDIS_IF_MAX_STRING_SIZE + 1;
            ConvertInterfaceGuidToLuid(ref InterfaceGuid, out NET_LUID InterfaceLuid);
            IntPtr pInterfaceAlias = Marshal.AllocHGlobal(new IntPtr(Length));
            ConvertInterfaceLuidToAlias(ref InterfaceLuid, pInterfaceAlias, Length);
            InterfaceAlias = Marshal.PtrToStringUni(pInterfaceAlias);
            Marshal.FreeHGlobal(pInterfaceAlias);
            return InterfaceAlias;
        }

        public static string ConvertInterfaceLuidToAlias(NET_LUID InterfaceLuid)
        {
            string InterfaceAlias; int Length = NDIS_IF_MAX_STRING_SIZE + 1;
            IntPtr pInterfaceAlias = Marshal.AllocHGlobal(new IntPtr(Length));
            ConvertInterfaceLuidToAlias(ref InterfaceLuid, pInterfaceAlias, Length);
            InterfaceAlias = Marshal.PtrToStringUni(pInterfaceAlias);
            Marshal.FreeHGlobal(pInterfaceAlias);
            return InterfaceAlias;
        }

        public static string ConvertInterfaceLuidToName(NET_LUID InterfaceLuid)
        {
            string InterfaceName; int Lenght = NDIS_IF_MAX_STRING_SIZE + 1;
            IntPtr pInterfaceName = Marshal.AllocHGlobal(new IntPtr(Lenght));
            ConvertInterfaceLuidToNameW(ref InterfaceLuid, pInterfaceName, Lenght);
            InterfaceName = Marshal.PtrToStringUni(pInterfaceName);
            Marshal.FreeHGlobal(pInterfaceName);
            return InterfaceName;
        }

        public static string ConvertInterfaceGuidToName(Guid InterfaceGuid)
        {
            string InterfaceName; int Lenght = NDIS_IF_MAX_STRING_SIZE + 1;
            ConvertInterfaceGuidToLuid(ref InterfaceGuid, out NET_LUID NetLUID);
            IntPtr pInterfaceName = Marshal.AllocHGlobal(new IntPtr(Lenght));
            ConvertInterfaceLuidToNameW(ref NetLUID, pInterfaceName, Lenght);
            InterfaceName = Marshal.PtrToStringUni(pInterfaceName);
            Marshal.FreeHGlobal(pInterfaceName);
            return InterfaceName;
        }

        public static bool FlushIpNetTable(Guid InterfaceGuid)
        {
            uint result = ConvertInterfaceGuidToLuid(ref InterfaceGuid, out NET_LUID NetLUID);
            if (result != 0) { PrintErrorResult(result); return false; }
            result = ConvertInterfaceLuidToIndex(ref NetLUID, out uint InterfaceIndex);
            if (result != 0) { PrintErrorResult(result); return false; }
            result = FlushIpNetTable2(ADDRESS_FAMILY.AF_UNSPEC, InterfaceIndex);
            PrintErrorResult(result);
            return result != 0 ? false : true;
        }

        private static void PrintErrorResult(uint result)
        {
            switch ((long)result)
            {
                case 0x00000008L: //ERROR_NOT_ENOUGH_MEMORY
                    Trace.WriteLine("ERROR_NOT_ENOUGH_MEMORY");
                    break;
                case 0x00000005L: //ERROR_ACCESS_DENIED
                    Trace.WriteLine("ERROR_ACCESS_DENIED");
                    break;
                case 0x00000002L: //ERROR_FILE_NOT_FOUND
                    Trace.WriteLine("ERROR_FILE_NOT_FOUND");
                    break;
                case 0x00000087L: //ERROR_INVALID_PARAMETER
                    Trace.WriteLine("ERROR_INVALID_PARAMETER");
                    break;
                case 0x00000050L: //ERROR_NOT_SUPPORTED
                    Trace.WriteLine("ERROR_NOT_SUPPORTED");
                    break;
                case 0x00005010L: //ERROR_OBJECT_ALREADY_EXISTS
                    Trace.WriteLine("ERROR_OBJECT_ALREADY_EXISTS");
                    break;
                case 0x00001168L: //ERROR_NOT_FOUND
                    Trace.WriteLine("ERROR_NOT_FOUND");
                    break;
                case 0xC000000D: //STATUS_INVALID_PARAMETER
                    Trace.WriteLine("STATUS_INVALID_PARAMETER");
                    break;
                default:
                    break;
            }
        }
    }
}
