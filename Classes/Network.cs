
using System;
using System.Net;
using System.Net.NetworkInformation;

namespace Classes
{
    public static class Network
    {
        internal static IPAddress GetIPAddress(PhysicalAddress PhysicalAddress)
        {
            NetworkInterface[] NetInterfaces;
            try { NetInterfaces = NetworkInterface.GetAllNetworkInterfaces(); }
            catch { return IPAddress.None; }
            foreach (NetworkInterface NetInterface in NetInterfaces)
            {
                if (NetInterface.GetPhysicalAddress().Equals(PhysicalAddress))
                {
                    IPInterfaceProperties IPITFProps = NetInterface.GetIPProperties();
                    foreach (UnicastIPAddressInformation uIPAddrInfo in IPITFProps.UnicastAddresses)
                    {
                        if (!uIPAddrInfo.Address.IsIPv6LinkLocal && !uIPAddrInfo.Address.IsIPv6Teredo)
                        { return uIPAddrInfo.Address; }
                    }
                }
            }
            return IPAddress.None;
        }

        internal static IPAddress GetIPAddress(Guid InterfaceGuid)
        {
            NetworkInterface[] NetInterfaces;
            try { NetInterfaces = NetworkInterface.GetAllNetworkInterfaces(); }
            catch { return IPAddress.None; }
            foreach (NetworkInterface NetInterface in NetInterfaces)
            {
                if (InterfaceGuid.Equals(Guid.Parse(NetInterface.Id)))
                {
                    IPInterfaceProperties IPITFProps = NetInterface.GetIPProperties();
                    foreach (UnicastIPAddressInformation GatewayInfo in IPITFProps.UnicastAddresses)
                    {
                        if (!GatewayInfo.Address.IsIPv6LinkLocal && !GatewayInfo.Address.IsIPv6Teredo)
                        { return GatewayInfo.Address; }
                    }
                }
            }
            return IPAddress.None;
        }

        internal static PhysicalAddress GetPhysicalAddress(Guid InterfaceGuid)
        {
            NetworkInterface[] NetInterfaces;
            try { NetInterfaces = NetworkInterface.GetAllNetworkInterfaces(); }
            catch { return PhysicalAddress.None; }
            foreach (NetworkInterface NetInterface in NetInterfaces)
            {
                if (InterfaceGuid.Equals(Guid.Parse(NetInterface.Id)))
                { return NetInterface.GetPhysicalAddress(); }
            }
            return PhysicalAddress.None;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="InterfaceGuid">The GUID of the network adapter.</param>
        /// <exception cref="NetworkInformationException"/>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="FormatException"/>
        /// <returns>NetworkInterfaceInfo</returns>
        internal static NetworkInterfaceInfo GetNetworkInterfaceInformation(Guid InterfaceGuid)
        {
            NetworkInterfaceInfo NetITFInfo = new NetworkInterfaceInfo();
            foreach (NetworkInterface NetITF in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (!InterfaceGuid.Equals(Guid.Parse(NetITF.Id))) continue;
                IPInterfaceProperties IPITFProps = NetITF.GetIPProperties();
                foreach (UnicastIPAddressInformation uIPAddrInfo in IPITFProps.UnicastAddresses)
                {
                    if (!uIPAddrInfo.Address.IsIPv6LinkLocal && !uIPAddrInfo.Address.IsIPv6Teredo)
                    {
                        NetITFInfo.DeviceDescription = NetITF.Description;
                        NetITFInfo.DeviceName = NetITF.Name;
                        NetITFInfo.IPAddress = uIPAddrInfo.Address;
                        NetITFInfo.PhysicalAddress = NetITF.GetPhysicalAddress();
                        NetITFInfo.GatewayAddress = IPITFProps.GatewayAddresses.Count > 0 ?
                            IPITFProps.GatewayAddresses[0].Address : IPAddress.None;
                        return NetITFInfo;
                    }
                }
            }
            return NetITFInfo;
        }

        /// <exception cref="NetworkInformationException"/>
        /// <exception cref="ArgumentOutOfRangeException"/>
        internal static bool IsPortAvailable(int Port)
        {
            TcpConnectionInformation[] TCPCI; IPGlobalProperties IPGP;
            IPGP = IPGlobalProperties.GetIPGlobalProperties();
            TCPCI = IPGP.GetActiveTcpConnections();
            foreach (var TCPConInfo in TCPCI)
            { if (TCPConInfo.LocalEndPoint.Port.Equals(Port)) return false; }
            return true;
        }
    }
}

public class NetworkInterfaceInfo : INetworkInterfaceInfo
{
    public IPAddress IPAddress
    {
        get;
        set;
    } = IPAddress.None;
    public string DeviceName
    {
        get;
        set;
    } = string.Empty;
    public string DeviceDescription
    {
        get;
        set;
    } = string.Empty;
    public string NetworkName
    {
        get;
        set;
    } = string.Empty;
    public string NetworkDescription
    {
        get;
        set;
    } = string.Empty;
    public PhysicalAddress PhysicalAddress
    {
        get;
        set;
    } = PhysicalAddress.None;
    public IPAddress GatewayAddress
    {
        get;
        set;
    } = IPAddress.None;
    public Guid InterfaceGuid
    {
        get;
        set;
    } = Guid.Empty;
    public bool IsConnectedToInternet
    {
        get;
        set;
    } = false;
    public bool IsConnected
    {
        get;
        set;
    } = false;
}

public interface INetworkInterfaceInfo
{
    IPAddress IPAddress
    {
        get;
        set;
    }
    string DeviceName
    {
        get;
        set;
    }
    string DeviceDescription
    {
        get;
        set;
    }
    string NetworkName
    {
        get;
        set;
    }
    string NetworkDescription
    {
        get;
        set;
    }
    PhysicalAddress PhysicalAddress
    {
        get;
        set;
    }
    IPAddress GatewayAddress
    {
        get;
        set;
    }
    Guid InterfaceGuid
    {
        get;
        set;
    }
    bool IsConnectedToInternet
    {
        get;
        set;
    }
    bool IsConnected
    {
        get;
        set;
    }
}