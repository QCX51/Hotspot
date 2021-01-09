
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Utils;

namespace Native
{
    public class WlanManager
    {
        //net stop wlansvc
        //reg delete hklm\system\currentcontrolset\services\wlansvc\parameters\hostednetworksettings /v hostednetworksettings
        //net start wlansvc

        private static IntPtr WlanHandle;
        private readonly wlanapi.WLAN_NOTIFICATION_CALLBACK NotificationCallback;
        private static Guid _HostedNetworkGuid = Guid.Empty;
        #region WLAN Manager

        public WlanManager()
        {
            NotificationCallback = new wlanapi.WLAN_NOTIFICATION_CALLBACK(OnWlanNotification);
            try { Initialize(); }
            catch { wlanapi.WlanCloseHandle(WlanHandle, IntPtr.Zero); }
        }

        private void Initialize()
        {
            uint result = wlanapi.WlanOpenHandle(wlanapi.WLAN_CLIENT_VERSION_VISTA, IntPtr.Zero, out uint ServerVersion, ref WlanHandle);
            if (result != 0) { throw new Exception("WlanOpenHandleEx"); }

            result = wlanapi.WlanRegisterVirtualStationNotification(WlanHandle, true, IntPtr.Zero);
            if (result != 0) { throw new Exception("WlanRegisterVirtualStationNotificationEx"); }

            result = wlanapi.WlanRegisterNotification(
                WlanHandle, WLAN_NOTIFICATION_SOURCE.WLAN_NOTIFICATION_SOURCE_ALL, true, NotificationCallback, IntPtr.Zero, IntPtr.Zero, out WLAN_NOTIFICATION_SOURCE notifSource);
            if (result != 0) { throw new Exception("WlanRegisterNotificationEx"); }

            WLAN_HOSTED_NETWORK_REASON failReason = InitSettings();
            if (failReason != WLAN_HOSTED_NETWORK_REASON.wlan_hosted_network_reason_success)
            { throw new Exception("WlanHostedNetworkInitSettingsEx: " + failReason.ToString()); }
        }
        #endregion

        #region Events
        public delegate void HostedNetworkStateHandler(WLAN_HOSTED_NETWORK_STATE WlanHostedNetworkState);
        public delegate void HostedNetworkPeerStateHandler(WLAN_HOSTED_NETWORK_PEER_STATE WlanHostedNetworkPeerState);
        public event HostedNetworkStateHandler HostedNetworkStateChange;
        public event HostedNetworkPeerStateHandler HostedNetworkPeerStateChanged;
        public event EventHandler<int> SignalQualityChanged;
        public event EventHandler<DOT11_RADIO_STATE> WlanSoftwareRadioStateChanged;
        public event EventHandler<DOT11_RADIO_STATE> WlanHardwareRadioStateChanged;
        #endregion

        #region OnNotification

        protected void OnHostedNetworkRadioStateChange(WLAN_HOSTED_NETWORK_RADIO_STATE state)
        {
            switch (state.dot11HardwareRadioState)
            {
                case DOT11_RADIO_STATE.dot11_radio_state_off:
                    break;
                case DOT11_RADIO_STATE.dot11_radio_state_on:
                    break;
                case DOT11_RADIO_STATE.dot11_radio_state_unknown:
                    break;
            }
        }

        private void OnMediaSpecificModuleNotification(uint NotificationCode, IntPtr pData)
        {
            switch ((WLAN_NOTIFICATION_MSM)NotificationCode)
            {
                case WLAN_NOTIFICATION_MSM.wlan_notification_msm_start:
                    break;
                case WLAN_NOTIFICATION_MSM.wlan_notification_msm_associating:
                    break;
                case WLAN_NOTIFICATION_MSM.wlan_notification_msm_associated:
                    break;
                case WLAN_NOTIFICATION_MSM.wlan_notification_msm_authenticating:
                    break;
                case WLAN_NOTIFICATION_MSM.wlan_notification_msm_connected:

                    var data = (WLAN_MSM_NOTIFICATION_DATA)Marshal.PtrToStructure(
                        pData, typeof(WLAN_MSM_NOTIFICATION_DATA));
                    System.Diagnostics.Debug.WriteLine("Connected to profile name: "
                        + data.strProfileName.ConvertToString());

                    break;
                case WLAN_NOTIFICATION_MSM.wlan_notification_msm_roaming_start:
                    break;
                case WLAN_NOTIFICATION_MSM.wlan_notification_msm_roaming_end:
                    break;
                case WLAN_NOTIFICATION_MSM.wlan_notification_msm_radio_state_change:
                    var state = (WLAN_PHY_RADIO_STATE)Marshal.PtrToStructure(pData, typeof(WLAN_PHY_RADIO_STATE));
                    if (state.dwPhyIndex.Equals(0))
                    {
                        WlanSoftwareRadioStateChanged?.Invoke(this, state.dot11SoftwareRadioState);
                        WlanHardwareRadioStateChanged?.Invoke(this, state.dot11HardwareRadioState);
                    }
                    break;
                case WLAN_NOTIFICATION_MSM.wlan_notification_msm_signal_quality_change:
                    SignalQualityChanged?.Invoke(this, Marshal.ReadInt32(pData));
                    break;
                case WLAN_NOTIFICATION_MSM.wlan_notification_msm_disassociating:
                    break;
                case WLAN_NOTIFICATION_MSM.wlan_notification_msm_disconnected:

                    var Data2 = (WLAN_MSM_NOTIFICATION_DATA)Marshal.PtrToStructure(
                        pData, typeof(WLAN_MSM_NOTIFICATION_DATA));
                    System.Diagnostics.Debug.WriteLine("Disconnected from profile name: "
                        + Data2.strProfileName.ConvertToString());

                    break;
                case WLAN_NOTIFICATION_MSM.wlan_notification_msm_peer_join:
                    System.Diagnostics.Debug.WriteLine("msm_peer_join");
                    break;
                case WLAN_NOTIFICATION_MSM.wlan_notification_msm_peer_leave:
                    break;
                case WLAN_NOTIFICATION_MSM.wlan_notification_msm_adapter_removal:
                    break;
                case WLAN_NOTIFICATION_MSM.wlan_notification_msm_adapter_operation_mode_change:
                    break;
                case WLAN_NOTIFICATION_MSM.wlan_notification_msm_end:
                    break;
                default:
                    break;
            }
        }

        protected void OnHostedNetworkNotification(uint NotificationCode, IntPtr pData)
        {
            switch ((WLAN_HOSTED_NETWORK_NOTIFICATION_CODE)NotificationCode)
            {
                case WLAN_HOSTED_NETWORK_NOTIFICATION_CODE.wlan_hosted_network_peer_state_change:
                    var PeerState = ((WLAN_HOSTED_NETWORK_DATA_PEER_STATE_CHANGE)Marshal.PtrToStructure(
                        pData, typeof(WLAN_HOSTED_NETWORK_DATA_PEER_STATE_CHANGE))).NewState;
                    HostedNetworkPeerStateChanged?.Invoke(PeerState);
                    break;
                case WLAN_HOSTED_NETWORK_NOTIFICATION_CODE.wlan_hosted_network_radio_state_change:
                    OnHostedNetworkRadioStateChange((WLAN_HOSTED_NETWORK_RADIO_STATE)Marshal.PtrToStructure(
                        pData, typeof(WLAN_HOSTED_NETWORK_RADIO_STATE)));
                    break;
                case WLAN_HOSTED_NETWORK_NOTIFICATION_CODE.wlan_hosted_network_state_change:
                    var state = (WLAN_HOSTED_NETWORK_STATE_CHANGE)Marshal.PtrToStructure(
                        pData, typeof(WLAN_HOSTED_NETWORK_STATE_CHANGE));
                    HostedNetworkStateChange?.Invoke(state.NewState);
                    break;
            }
        }

        private void OnONEXNotification(uint NotificationCode, IntPtr pData)
        {
            switch ((WLAN_NOTIFICATION_SOURCE_ONEX)NotificationCode)
            {
                case WLAN_NOTIFICATION_SOURCE_ONEX.OneXPublicNotificationBase:
                    break;
                case WLAN_NOTIFICATION_SOURCE_ONEX.OneXNotificationTypeResultUpdate:
                    var Result = (ONEX_RESULT_UPDATE_DATA)Marshal.PtrToStructure(pData, typeof(ONEX_RESULT_UPDATE_DATA));
                    break;
                case WLAN_NOTIFICATION_SOURCE_ONEX.OneXNotificationTypeAuthRestarted:
                    var Status = (ONEX_AUTH_STATUS)Marshal.ReadInt32(pData);
                    break;
                case WLAN_NOTIFICATION_SOURCE_ONEX.OneXNotificationTypeEventInvalid:
                    break;
                default:
                    break;
            }
        }

        private void OnACMNotification(uint NotificationCode, IntPtr pData)
        {
            switch ((WLAN_NOTIFICATION_ACM)NotificationCode)
            {
                case WLAN_NOTIFICATION_ACM.wlan_notification_acm_start:
                    break;
                case WLAN_NOTIFICATION_ACM.wlan_notification_acm_autoconf_enabled:
                    break;
                case WLAN_NOTIFICATION_ACM.wlan_notification_acm_autoconf_disabled:
                    break;
                case WLAN_NOTIFICATION_ACM.wlan_notification_acm_background_scan_enabled:
                    break;
                case WLAN_NOTIFICATION_ACM.wlan_notification_acm_background_scan_disabled:
                    break;
                case WLAN_NOTIFICATION_ACM.wlan_notification_acm_bss_type_change:
                    var BSSType = (DOT11_BSS_TYPE)Marshal.ReadInt32(pData);
                    switch (BSSType)
                    {
                        case DOT11_BSS_TYPE.dot11_BSS_type_infrastructure:
                            break;
                        case DOT11_BSS_TYPE.dot11_BSS_type_independent:
                            break;
                        case DOT11_BSS_TYPE.dot11_BSS_type_any:
                            break;
                        default:
                            break;
                    }
                    break;
                case WLAN_NOTIFICATION_ACM.wlan_notification_acm_power_setting_change:
                    var SettingChange = (WLAN_POWER_SETTING)Marshal.ReadInt32(pData);
                    switch (SettingChange)
                    {
                        case WLAN_POWER_SETTING.wlan_power_setting_no_saving:
                            break;
                        case WLAN_POWER_SETTING.wlan_power_setting_low_saving:
                            break;
                        case WLAN_POWER_SETTING.wlan_power_setting_medium_saving:
                            break;
                        case WLAN_POWER_SETTING.wlan_power_setting_maximum_saving:
                            break;
                        case WLAN_POWER_SETTING.wlan_power_setting_invalid:
                            break;
                        default:
                            break;
                    }
                    break;
                case WLAN_NOTIFICATION_ACM.wlan_notification_acm_scan_complete:
                    break;
                case WLAN_NOTIFICATION_ACM.wlan_notification_acm_scan_fail:
                    break;
                case WLAN_NOTIFICATION_ACM.wlan_notification_acm_connection_start:
                    break;
                case WLAN_NOTIFICATION_ACM.wlan_notification_acm_connection_complete:
                    break;
                case WLAN_NOTIFICATION_ACM.wlan_notification_acm_connection_attempt_fail:
                    break;
                case WLAN_NOTIFICATION_ACM.wlan_notification_acm_filter_list_change:
                    /*An application can call the WlanGetFilterList function to retrieve the new filter list. */
                    break;
                case WLAN_NOTIFICATION_ACM.wlan_notification_acm_interface_arrival:
                    break;
                case WLAN_NOTIFICATION_ACM.wlan_notification_acm_interface_removal:
                    break;
                case WLAN_NOTIFICATION_ACM.wlan_notification_acm_profile_change:
                    /* An application can call the WlanGetProfileList and WlanGetProfile functions
                     * to retrieve the updated profiles.
                     * The interface on which the profile list changes is identified by the InterfaceGuid member.*/
                    break;
                case WLAN_NOTIFICATION_ACM.wlan_notification_acm_profile_name_change:
                    break;
                case WLAN_NOTIFICATION_ACM.wlan_notification_acm_profiles_exhausted:
                    break;
                case WLAN_NOTIFICATION_ACM.wlan_notification_acm_network_not_available:
                    break;
                case WLAN_NOTIFICATION_ACM.wlan_notification_acm_network_available:
                    break;
                case WLAN_NOTIFICATION_ACM.wlan_notification_acm_disconnecting:
                    break;
                case WLAN_NOTIFICATION_ACM.wlan_notification_acm_disconnected:
                    break;
                case WLAN_NOTIFICATION_ACM.wlan_notification_acm_adhoc_network_state_change:
                    var NetState = (WLAN_ADHOC_NETWORK_STATE)Marshal.ReadInt32(pData);
                    switch (NetState)
                    {
                        case WLAN_ADHOC_NETWORK_STATE.wlan_adhoc_network_state_formed:
                            break;
                        case WLAN_ADHOC_NETWORK_STATE.wlan_adhoc_network_state_connected:
                            break;
                    }
                    break;
                case WLAN_NOTIFICATION_ACM.wlan_notification_acm_profile_unblocked:
                    break;
                case WLAN_NOTIFICATION_ACM.wlan_notification_acm_screen_power_change:
                    break;
                case WLAN_NOTIFICATION_ACM.wlan_notification_acm_profile_blocked:
                    break;
                case WLAN_NOTIFICATION_ACM.wlan_notification_acm_scan_list_refresh:
                    break;
                case WLAN_NOTIFICATION_ACM.wlan_notification_acm_end:
                    break;
                default:
                    break;
            }
        }

        protected void OnWlanNotification(ref WLAN_NOTIFICATION_DATA NotifyData, IntPtr context)
        {
            if ((NotifyData.dwDataSize > 0) && (NotifyData.pData != IntPtr.Zero))
            {
                switch (NotifyData.NotificationSource)
                {
                    case WLAN_NOTIFICATION_SOURCE.WLAN_NOTIFICATION_SOURCE_HNWK:
                        try { OnHostedNetworkNotification(NotifyData.NotificationCode, NotifyData.pData); }
                        catch (Exception ex) { Trace($"{ex.Message}"); }
                        break;
                    case WLAN_NOTIFICATION_SOURCE.WLAN_NOTIFICATION_SOURCE_ACM:
                        try { OnACMNotification(NotifyData.NotificationCode, NotifyData.pData); }
                        catch (Exception ex) { Trace($"{ex.Message}"); }
                        break;
                    case WLAN_NOTIFICATION_SOURCE.WLAN_NOTIFICATION_SOURCE_IHV:
                        break;
                    case WLAN_NOTIFICATION_SOURCE.WLAN_NOTIFICATION_SOURCE_MSM:
                        try { OnMediaSpecificModuleNotification(NotifyData.NotificationCode, NotifyData.pData); }
                        catch (Exception ex) { Trace($"{ex.Message}"); }
                        break;
                    case WLAN_NOTIFICATION_SOURCE.WLAN_NOTIFICATION_SOURCE_ONEX:
                        try { OnONEXNotification(NotifyData.NotificationCode, NotifyData.pData); }
                        catch (Exception ex) { Trace($"{ex.Message}"); }
                        break;
                    case WLAN_NOTIFICATION_SOURCE.WLAN_NOTIFICATION_SOURCE_SECURITY:
                        break;
                    case WLAN_NOTIFICATION_SOURCE.WLAN_NOTIFICATION_SOURCE_ALL:
                        break;
                    case WLAN_NOTIFICATION_SOURCE.WLAN_NOTIFICATION_SOURCE_NONE:
                        break;
                    default:
                        break;
                }
            }
        }

        #endregion

        #region Public Methods

        public WLAN_HOSTED_NETWORK_REASON ForceStart()
        {
            _ = wlanapi.WlanHostedNetworkForceStart(
                WlanHandle, out WLAN_HOSTED_NETWORK_REASON failReason, IntPtr.Zero);
            return failReason;
        }

        public WLAN_HOSTED_NETWORK_REASON ForceStop()
        {
            _ = wlanapi.WlanHostedNetworkForceStop(
                WlanHandle, out WLAN_HOSTED_NETWORK_REASON failReason, IntPtr.Zero);
            return failReason;
        }

        public WLAN_HOSTED_NETWORK_REASON StartUsing()
        {
            _ = wlanapi.WlanHostedNetworkStartUsing(
                WlanHandle, out WLAN_HOSTED_NETWORK_REASON failReason, IntPtr.Zero);
            return failReason;
        }

        public WLAN_HOSTED_NETWORK_REASON StopUsing()
        {
            _ = wlanapi.WlanHostedNetworkStopUsing(
                WlanHandle, out WLAN_HOSTED_NETWORK_REASON failReason, IntPtr.Zero);
            return failReason;
        }

        public WLAN_HOSTED_NETWORK_REASON InitSettings()
        {
            _ = wlanapi.WlanHostedNetworkInitSettings(
                WlanHandle, out WLAN_HOSTED_NETWORK_REASON failReason, IntPtr.Zero);
            return failReason;
        }

        public WLAN_HOSTED_NETWORK_REASON QuerySecondaryKey(out string KeyData, out bool PassPhrase, out bool Persistent)
        {
            _ = wlanapi.WlanHostedNetworkQuerySecondaryKey(
                WlanHandle, out uint keyLength, out KeyData, out PassPhrase, out Persistent, out WLAN_HOSTED_NETWORK_REASON failReason, IntPtr.Zero);
            return failReason;
        }

        public WLAN_HOSTED_NETWORK_REASON SetSecondaryKey(string KeyData, bool PassPhrase, bool Persistent)
        {
            _ = wlanapi.WlanHostedNetworkSetSecondaryKey(
                WlanHandle, (uint)(KeyData.Length + 1), KeyData, PassPhrase, Persistent, out WLAN_HOSTED_NETWORK_REASON failReason, IntPtr.Zero);
            return failReason;
        }

        public WLAN_HOSTED_NETWORK_REASON SetConnectionSettings(string HostedNetworkSSID, uint MaxNumberOfPeers)
        {
            WLAN_HOSTED_NETWORK_CONNECTION_SETTINGS settings = new WLAN_HOSTED_NETWORK_CONNECTION_SETTINGS()
            {
                hostedNetworkSSID = WlanUtils.ConvertStringToDOT11_SSID(HostedNetworkSSID),
                dwMaxNumberOfPeers = MaxNumberOfPeers
            };
            IntPtr pvData = Marshal.AllocHGlobal(Marshal.SizeOf(settings));
            Marshal.StructureToPtr(settings, pvData, true);

            uint result = wlanapi.WlanHostedNetworkSetProperty(WlanHandle,
                    WLAN_HOSTED_NETWORK_OPCODE.wlan_hosted_network_opcode_connection_settings,
                    (uint)Marshal.SizeOf(settings), pvData, out WLAN_HOSTED_NETWORK_REASON FailReason, IntPtr.Zero
                );
            Marshal.FreeHGlobal(pvData);
            return FailReason;
        }

        public WLAN_OPCODE_VALUE_TYPE QueryConnectionSettings(out string HostedNetworkSSID, out uint MaxNumberOfPeers)
        {
            uint result = wlanapi.WlanHostedNetworkQueryProperty(WlanHandle,
                    WLAN_HOSTED_NETWORK_OPCODE.wlan_hosted_network_opcode_connection_settings,
                    out uint dataSize, out IntPtr pvData, out WLAN_OPCODE_VALUE_TYPE opcode, IntPtr.Zero);

            var settings = (WLAN_HOSTED_NETWORK_CONNECTION_SETTINGS)Marshal.PtrToStructure(
                pvData, typeof(WLAN_HOSTED_NETWORK_CONNECTION_SETTINGS));

            wlanapi.WlanFreeMemory(pvData);

            HostedNetworkSSID = settings.hostedNetworkSSID.ConvertToString();
            MaxNumberOfPeers = settings.dwMaxNumberOfPeers;

            return opcode;
        }

        public IEnumerable<DOT11_PHY_TYPE> WlanGetInterfaceCapability(Guid InterfaceGuid)
        {
            IntPtr ppCapability = IntPtr.Zero;
            uint result = wlanapi.WlanGetInterfaceCapability(WlanHandle, ref InterfaceGuid, IntPtr.Zero, ref ppCapability);
            if (result != 0) yield return DOT11_PHY_TYPE.dot11_phy_type_unknown;
            var cap = (WLAN_INTERFACE_CAPABILITY)Marshal.PtrToStructure(ppCapability, typeof(WLAN_INTERFACE_CAPABILITY));
            foreach (var type in cap.dot11PhyTypes) { yield return type; }
            wlanapi.WlanFreeMemory(ppCapability);
        }

        public bool AllowHostedNetwork(bool pBool)
        {
            IntPtr pvData = Marshal.AllocHGlobal(Marshal.SizeOf(pBool));
            Marshal.StructureToPtr(pBool, pvData, true);
            _ = wlanapi.WlanHostedNetworkSetProperty(
                WlanHandle, WLAN_HOSTED_NETWORK_OPCODE.wlan_hosted_network_opcode_enable,
                (uint)Marshal.SizeOf(pBool), pvData, out WLAN_HOSTED_NETWORK_REASON failReason, IntPtr.Zero);
            Marshal.FreeHGlobal(pvData);
            return failReason == WLAN_HOSTED_NETWORK_REASON.wlan_hosted_network_reason_success;
        }

        public WLAN_STATISTICS GetWlanStatistic(Guid InterfaceGUID)
        {
            uint result = wlanapi.WlanQueryInterface(
                WlanHandle, ref InterfaceGUID, WLAN_INTF_OPCODE.wlan_intf_opcode_statistics, IntPtr.Zero, out uint dataSize, out IntPtr ppData, 0);
            if (result != 0) { return new WLAN_STATISTICS(); }

            WLAN_STATISTICS NICStatistics = (WLAN_STATISTICS)Marshal.PtrToStructure(ppData, typeof(WLAN_STATISTICS));
            IntPtr pPhyCounters = Marshal.OffsetOf(typeof(WLAN_STATISTICS), "PhyCounters");

            for (int i = 0; i < NICStatistics.dwNumberOfPhys; i++)
            {
                IntPtr pPHYFrames = new IntPtr(ppData.ToInt64() + pPhyCounters.ToInt64());
                var PHYFrames = (WLAN_PHY_FRAME_STATISTICS)Marshal.PtrToStructure(
                    pPHYFrames, typeof(WLAN_PHY_FRAME_STATISTICS));
                pPhyCounters += Marshal.SizeOf(PHYFrames);
            }
            wlanapi.WlanFreeMemory(ppData); return NICStatistics;
        }

        public bool HostedNetworkCapable(Guid InterfaceGuid)
        {
            _ = wlanapi.WlanQueryInterface(
                WlanHandle, ref InterfaceGuid, WLAN_INTF_OPCODE.wlan_intf_opcode_hosted_network_capable, IntPtr.Zero,
                out uint dataSize, out IntPtr pData, WLAN_OPCODE_VALUE_TYPE.wlan_opcode_value_type_query_only);
            bool Capable = pData != IntPtr.Zero ? (bool)Marshal.PtrToStructure(pData, typeof(bool)) : false;
            wlanapi.WlanFreeMemory(pData);
            return Capable;
        }

        #endregion

        #region Properties

        public WLAN_INTERFACE_INFO_LIST EnumInterfaces
        {
            get
            {
                wlanapi.WlanEnumInterfaces(WlanHandle, IntPtr.Zero, out IntPtr ppInterfaceList);
                return new WLAN_INTERFACE_INFO_LIST(ref ppInterfaceList);
            }
        }

        public WLAN_HOSTED_NETWORK_STATUS HostedNetworkStatus
        {
            get
            {
                if (wlanapi.WlanHostedNetworkQueryStatus(WlanHandle, out IntPtr pNetStatus, IntPtr.Zero) == 0)
                {
                    WLAN_HOSTED_NETWORK_STATUS NetStatus;
                    NetStatus = (WLAN_HOSTED_NETWORK_STATUS)Marshal.PtrToStructure(pNetStatus, typeof(WLAN_HOSTED_NETWORK_STATUS));
                    wlanapi.WlanFreeMemory(pNetStatus);
                    return NetStatus;
                }
                else return new WLAN_HOSTED_NETWORK_STATUS();
            }
        }

        public bool IsHostedNetworkSupported
        {
            get
            {
                foreach (var InterfaceInfo in EnumInterfaces.InterfaceInfo)
                { if (HostedNetworkCapable(InterfaceInfo.InterfaceGuid)) { return true; } }
                return false;
            }
        }

        public IEnumerable<WLAN_HOSTED_NETWORK_PEER_STATE> HostedNetworkPeerList
        {
            get
            {
                _ = wlanapi.WlanHostedNetworkQueryStatus(WlanHandle, out IntPtr pNetStatus, IntPtr.Zero);
                var Status = (WLAN_HOSTED_NETWORK_STATUS)Marshal.PtrToStructure(pNetStatus, typeof(WLAN_HOSTED_NETWORK_STATUS));
                IntPtr pPeerList = Marshal.OffsetOf(typeof(WLAN_HOSTED_NETWORK_STATUS), "PeerList");

                for (int i = 0; i < Status.dwNumberOfPeers; i++)
                {
                    IntPtr intPtr = new IntPtr(pNetStatus.ToInt64() + pPeerList.ToInt64());
                    var peer = (WLAN_HOSTED_NETWORK_PEER_STATE)Marshal.PtrToStructure(
                        intPtr, typeof(WLAN_HOSTED_NETWORK_PEER_STATE));
                    yield return peer;
                    pPeerList += Marshal.SizeOf(peer);
                }
                wlanapi.WlanFreeMemory(pNetStatus);
            }
        }
        public Guid HostedNetworkGuid
        {
            get
            {
                if (!_HostedNetworkGuid.Equals(Guid.Empty))
                {
                    return _HostedNetworkGuid;
                }
                else
                {
                    _HostedNetworkGuid = HostedNetworkStatus.IPDeviceID;
                    return _HostedNetworkGuid;
                }
            }
        }

        public WLAN_HOSTED_NETWORK_STATE HostedNetworkState
        {
            get
            {
                return HostedNetworkStatus.HostedNetworkState;
            }
        }

        public bool IsHostedNetworkActive
        {
            get
            {
                return (HostedNetworkState == WLAN_HOSTED_NETWORK_STATE.wlan_hosted_network_active);
            }
        }

        public bool AllowVirtualStationExtensibility
        {
            get
            {
                uint result = wlanapi.WlanQueryAutoConfigParameter(
                    WlanHandle, WLAN_AUTOCONF_OPCODE.wlan_autoconf_opcode_allow_virtual_station_extensibility, out UIntPtr datasize, out bool pvData, out WLAN_OPCODE_VALUE_TYPE opcodeType, IntPtr.Zero);
                return pvData;
            }
        }

        public bool HostedNetworkEnabled
        {
            get
            {
                uint errorcode = wlanapi.WlanHostedNetworkQueryProperty(WlanHandle, WLAN_HOSTED_NETWORK_OPCODE.wlan_hosted_network_opcode_enable,
                    out uint dataSize, out IntPtr pvData, out WLAN_OPCODE_VALUE_TYPE opcode, IntPtr.Zero);
                if (errorcode == 87) System.Diagnostics.Debug.WriteLine("the WLAN AutoConfig Service is not running.\nThe service has not been started");
                if (errorcode != 0) return false;
                var Allowed = (bool)Marshal.PtrToStructure(pvData, typeof(bool));
                wlanapi.WlanFreeMemory(pvData);
                return Allowed;
            }
        }
        #endregion

        #region IDisposable Members
        ~WlanManager()
        {
            if (WlanHandle != IntPtr.Zero)
            { wlanapi.WlanCloseHandle(WlanHandle, IntPtr.Zero); }
        }
        #endregion
        private void Trace(string message)
        {
            foreach (System.Diagnostics.TraceListener listener in System.Diagnostics.Debug.Listeners)
            { listener.WriteLine(message); }
        }
    }
}
