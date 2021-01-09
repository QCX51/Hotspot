using System;
using NETCONLib;
using System.Collections.Generic;
using NETWORKLIST;
using System.Diagnostics;
using Classes;

namespace Hotspot
{
    public static class ICSRouter
    {
        private static readonly INetSharingManager INetSharingMgr = new NetSharingManager();
        private static readonly INetworkListManager INetworkListMgr = new NetworkListManager();

        public static bool IsConnected           { get { return INetworkListMgr.IsConnected; } }
        public static bool IsConnectedToInternet { get { return INetworkListMgr.IsConnectedToInternet; } }
        public static bool IsSharingInstalled    { get { return INetSharingMgr.SharingInstalled; } }
        
        public static IEnumerable<NetworkInterfaceInfo> GetNetworkConnectionsInfo()
        {
            foreach (INetworkConnection INetConnection in INetworkListMgr.GetNetworkConnections())
            {
                NetworkInterfaceInfo NetITFInfo;
                NetITFInfo = Network.GetNetworkInterfaceInformation(INetConnection.GetAdapterId());
                NetITFInfo.InterfaceGuid = INetConnection.GetAdapterId();
                NetITFInfo.NetworkName = INetConnection.GetNetwork().GetName();
                NetITFInfo.NetworkDescription = INetConnection.GetNetwork().GetDescription();
                NetITFInfo.IsConnectedToInternet = INetConnection.IsConnectedToInternet;
                NetITFInfo.IsConnected = INetConnection.IsConnected;
                yield return NetITFInfo;
            }
        }

        public static bool IsNICConnectedToInternet(Guid InterfaceGuid)
        {
            foreach (INetworkConnection INetConnection in INetworkListMgr.GetNetworkConnections())
            {
                if (!INetConnection.GetAdapterId().Equals(InterfaceGuid)) continue;
                return INetConnection.IsConnectedToInternet;
            }
            return false;
        }

        public static IEnumerable<NetworkInterfaceInfo> GetNetworkInterfaces()
        {
            foreach (INetConnection NetConnection in INetSharingMgr.EnumEveryConnection)
            {
                NetworkInterfaceInfo NetITFInfo;
                INetConnectionProps INetConProps = INetSharingMgr.NetConnectionProps[NetConnection];
                Guid InterfaceGuid = Guid.Parse(INetConProps.Guid);
                NetITFInfo = Network.GetNetworkInterfaceInformation(InterfaceGuid);
                NetITFInfo.InterfaceGuid = InterfaceGuid;
                NetITFInfo.DeviceName = INetConProps.Name;
                NetITFInfo.DeviceDescription = INetConProps.DeviceName;
                NetITFInfo.IsConnectedToInternet = IsNICConnectedToInternet(InterfaceGuid);
                NetITFInfo.IsConnected = INetConProps.Status == tagNETCON_STATUS.NCS_CONNECTED;
                yield return NetITFInfo;

                switch (INetConProps.MediaType)
                {
                    case tagNETCON_MEDIATYPE.NCM_NONE:
                        // No media is present.
                        Debug.WriteLine("MediaType: NCM_NONE");
                        break;
                    case tagNETCON_MEDIATYPE.NCM_DIRECT:
                        // Direct serial connection through a serial port.
                        Debug.WriteLine("MediaType: NCM_DIRECT");
                        break;
                    case tagNETCON_MEDIATYPE.NCM_ISDN:
                        // Connection is through an integrated services digital network (ISDN) line.
                        Debug.WriteLine("MediaType: NCM_ISDN");
                        break;
                    case tagNETCON_MEDIATYPE.NCM_LAN:
                        // Connection is to a local area network (LAN).
                        Debug.WriteLine("MediaType: NCM_LAN");
                        break;
                    case tagNETCON_MEDIATYPE.NCM_PHONE:
                        // Dial-up connection over a conventional phone line.
                        Debug.WriteLine("MediaType: NCM_PHONE");
                        break;
                    case tagNETCON_MEDIATYPE.NCM_TUNNEL:
                        // Virtual private network (VPN) connection.
                        Debug.WriteLine("MediaType: NCM_TUNNEL");
                        break;
                    case tagNETCON_MEDIATYPE.NCM_PPPOE:
                        // Point-to-Point protocol (PPP) over Ethernet.
                        Debug.WriteLine("MediaType: NCM_PPPOE");
                        break;
                    case tagNETCON_MEDIATYPE.NCM_BRIDGE:
                        // Bridged connection.
                        Debug.WriteLine("MediaType: NCM_BRIDGE");
                        break;
                    case tagNETCON_MEDIATYPE.NCM_SHAREDACCESSHOST_LAN:
                        // Shared connection to a LAN.
                        Debug.WriteLine("MediaType: SHAREDACCESSHOST_LAN");
                        break;
                    case tagNETCON_MEDIATYPE.NCM_SHAREDACCESSHOST_RAS:
                        // Shared connection to a remote or wide area network (WAN).
                        Debug.WriteLine("MediaType: NCM_SHAREDACCESSHOST_RAS");
                        break;
                    default:
                        break;
                }
            }
        }

        public static void StartSharing(Guid IPublicGuid, Guid IPrivateGuid)
        {
            StopSharing();
            StartSharing(IPrivateGuid, tagSHARINGCONNECTIONTYPE.ICSSHARINGTYPE_PRIVATE);
            StartSharing(IPublicGuid, tagSHARINGCONNECTIONTYPE.ICSSHARINGTYPE_PUBLIC);
        }

        public static void StartSharing(Guid InterfaceGuid, tagSHARINGCONNECTIONTYPE ShConType)
        {
            foreach (INetConnection NetConnection in INetSharingMgr.EnumEveryConnection)
            {
                INetConnectionProps INetConProps = INetSharingMgr.NetConnectionProps[NetConnection];
                INetSharingConfiguration INetShConfig = INetSharingMgr.INetSharingConfigurationForINetConnection[NetConnection];

                if (!Guid.Parse(INetConProps.Guid).Equals(InterfaceGuid)) continue;
                switch (ShConType)
                {
                    case tagSHARINGCONNECTIONTYPE.ICSSHARINGTYPE_PUBLIC:
                        INetShConfig.EnableSharing(ShConType);
                        Trace.WriteLine(
                            $"\n" +
                            $"Start ICS Type: Public" +
                            $"\nName: {INetConProps.Name}" +
                            $"\nDeviceName: {INetConProps.DeviceName}" +
                            $"\nGuid: {INetConProps.Guid}" +
                            $"\n");
                        break;
                    case tagSHARINGCONNECTIONTYPE.ICSSHARINGTYPE_PRIVATE:
                        INetShConfig.EnableSharing(ShConType);
                        Trace.WriteLine(
                            $"\n" +
                            $"Start ICS Type: Private" +
                            $"\nName: {INetConProps.Name}" +
                            $"\nDeviceName: {INetConProps.DeviceName}" +
                            $"\nGuid: {INetConProps.Guid}" +
                            $"\n");
                        break;
                }
            }
        }

        public static void StopSharing()
        {
            foreach (INetConnection NetConnection in INetSharingMgr.EnumEveryConnection)
            {
                INetConnectionProps INetConProps = INetSharingMgr.NetConnectionProps[NetConnection];
                INetSharingConfiguration INetShConfig = INetSharingMgr.INetSharingConfigurationForINetConnection[NetConnection];

                if (!INetShConfig.SharingEnabled) continue;
                switch (INetShConfig.SharingConnectionType)
                {
                    case tagSHARINGCONNECTIONTYPE.ICSSHARINGTYPE_PUBLIC:
                        INetShConfig.DisableSharing();
                        Trace.WriteLine(
                            $"\n" +
                            $"Stop ICS Type: Public" +
                            $"\nName: {INetConProps.Name}" +
                            $"\nDeviceName: {INetConProps.DeviceName}" +
                            $"\nGuid: {INetConProps.Guid}" +
                            $"\n");
                        break;
                    case tagSHARINGCONNECTIONTYPE.ICSSHARINGTYPE_PRIVATE:
                        INetShConfig.DisableSharing();
                        Trace.WriteLine(
                            $"\n" +
                            $"Stop ICS Type: Private" +
                            $"\nName: {INetConProps.Name}" +
                            $"\nDeviceName: {INetConProps.DeviceName}" +
                            $"\nGuid: {INetConProps.Guid}" +
                            $"\n");
                        break;
                }
            }
        }
        public static void GetNetworkList(NLM_ENUM_NETWORK Flags)
        {
            foreach (INetwork Network in INetworkListMgr.GetNetworks(Flags))
            {
                System.Text.StringBuilder builder = new System.Text.StringBuilder();
                builder.AppendLine($"Name: {Network.GetName()}");
                builder.AppendLine($"Description: {Network.GetDescription()}");
                builder.AppendLine($"ID: {Network.GetNetworkId()}");
                builder.AppendLine($"Is connected: {Network.IsConnected}");
                builder.AppendLine($"Is connected to internet: {Network.IsConnectedToInternet}");
                
                switch (Network.GetCategory())
                {
                    case NLM_NETWORK_CATEGORY.NLM_NETWORK_CATEGORY_PUBLIC:
                        builder.AppendLine("Category: PUBLIC");
                        break;
                    case NLM_NETWORK_CATEGORY.NLM_NETWORK_CATEGORY_PRIVATE:
                        builder.AppendLine("Category: PRIVATE");
                        break;
                    case NLM_NETWORK_CATEGORY.NLM_NETWORK_CATEGORY_DOMAIN_AUTHENTICATED:
                        builder.AppendLine("Category: DOMAIN_AUTHENTICATED");
                        break;
                }
                switch (Network.GetConnectivity())
                {
                    case NLM_CONNECTIVITY.NLM_CONNECTIVITY_DISCONNECTED:
                        builder.AppendLine("Connectivity: DISCONNECTED");
                        break;
                    case NLM_CONNECTIVITY.NLM_CONNECTIVITY_IPV4_NOTRAFFIC:
                        builder.AppendLine("Connectivity: IPV4_NOTRAFFIC");
                        break;
                    case NLM_CONNECTIVITY.NLM_CONNECTIVITY_IPV6_NOTRAFFIC:
                        builder.AppendLine("Connectivity: IPV6_NOTRAFFIC");
                        break;
                    case NLM_CONNECTIVITY.NLM_CONNECTIVITY_IPV4_SUBNET:
                        builder.AppendLine("Connectivity: IPV4_SUBNET");
                        break;
                    case NLM_CONNECTIVITY.NLM_CONNECTIVITY_IPV4_LOCALNETWORK:
                        builder.AppendLine("Connectivity: IPV4_LOCALNETWORK");
                        break;
                    case NLM_CONNECTIVITY.NLM_CONNECTIVITY_IPV4_INTERNET:
                        builder.AppendLine("Connectivity: IPV4_INTERNET");
                        break;
                    case NLM_CONNECTIVITY.NLM_CONNECTIVITY_IPV6_SUBNET:
                        builder.AppendLine("Connectivity: IPV6_SUBNET");
                        break;
                    case NLM_CONNECTIVITY.NLM_CONNECTIVITY_IPV6_LOCALNETWORK:
                        builder.AppendLine("Connectivity: IPV6_LOCALNETWORK");
                        break;
                    case NLM_CONNECTIVITY.NLM_CONNECTIVITY_IPV6_INTERNET:
                        builder.AppendLine("Connectivity: IPV6_INTERNET");
                        break;
                    default:
                        builder.AppendLine("Connectivity: CONNECTED");
                        break;
                }
                switch (Network.GetDomainType())
                {
                    case NLM_DOMAIN_TYPE.NLM_DOMAIN_TYPE_NON_DOMAIN_NETWORK:
                        builder.AppendLine("DomainType: NON_DOMAIN_NETWORK");
                        break;
                    case NLM_DOMAIN_TYPE.NLM_DOMAIN_TYPE_DOMAIN_NETWORK:
                        builder.AppendLine("DomainType: DOMAIN_NETWORK");
                        break;
                    case NLM_DOMAIN_TYPE.NLM_DOMAIN_TYPE_DOMAIN_AUTHENTICATED:
                        builder.AppendLine("DomainType: DOMAIN_AUTHENTICATED");
                        break;
                }
                Trace.WriteLine(builder.ToString());
            }
        }
    }
}
