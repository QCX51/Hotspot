﻿
namespace Native
{
    //https://msdn.microsoft.com/en-us/library/windows/desktop/dd815255(v=vs.85).aspx
    public enum WLAN_NOTIFICATION_MSM
 {
        wlan_notification_msm_start = 0,
        wlan_notification_msm_associating,
        wlan_notification_msm_associated,
        wlan_notification_msm_authenticating,
        wlan_notification_msm_connected,
        wlan_notification_msm_roaming_start,
        wlan_notification_msm_roaming_end,
        wlan_notification_msm_radio_state_change,
        wlan_notification_msm_signal_quality_change,
        wlan_notification_msm_disassociating,
        wlan_notification_msm_disconnected,
        wlan_notification_msm_peer_join,
        wlan_notification_msm_peer_leave,
        wlan_notification_msm_adapter_removal,
        wlan_notification_msm_adapter_operation_mode_change,
        wlan_notification_msm_end
    } 
}
