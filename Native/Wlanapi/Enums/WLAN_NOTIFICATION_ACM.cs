
namespace Native
{
    //https://msdn.microsoft.com/en-us/library/windows/desktop/dd815254(v=vs.85).aspx
    
    public enum WLAN_NOTIFICATION_ACM
 {
        wlan_notification_acm_start = 0,
        wlan_notification_acm_autoconf_enabled           = 0x00000001,
        wlan_notification_acm_autoconf_disabled          = 0x00000002,
        wlan_notification_acm_background_scan_enabled    = 0x00000003,
        wlan_notification_acm_background_scan_disabled   = 0x00000004,
        wlan_notification_acm_bss_type_change            = 0x00000005,
        wlan_notification_acm_power_setting_change       = 0x00000006,
        wlan_notification_acm_scan_complete              = 0x00000007,
        wlan_notification_acm_scan_fail                  = 0x00000008,
        wlan_notification_acm_connection_start           = 0x00000009,
        wlan_notification_acm_connection_complete        = 0x0000000a,
        wlan_notification_acm_connection_attempt_fail    = 0x0000000b,
        wlan_notification_acm_filter_list_change         = 0x0000000c,
        wlan_notification_acm_interface_arrival          = 0x0000000d,
        wlan_notification_acm_interface_removal          = 0x0000000e,
        wlan_notification_acm_profile_change             = 0x0000000f,
        wlan_notification_acm_profile_name_change        = 0x00000010,
        wlan_notification_acm_profiles_exhausted         = 0x00000011,
        wlan_notification_acm_network_not_available      = 0x00000012,
        wlan_notification_acm_network_available          = 0x00000013,
        wlan_notification_acm_disconnecting              = 0x00000014,
        wlan_notification_acm_disconnected               = 0x00000015,
        wlan_notification_acm_adhoc_network_state_change = 0x00000016,
        wlan_notification_acm_profile_unblocked          = 0x00000017,
        wlan_notification_acm_screen_power_change        = 0x00000018,
        wlan_notification_acm_profile_blocked            = 0x00000019,
        wlan_notification_acm_scan_list_refresh          = 0x0000001a,
        wlan_notification_acm_end                        = 0x0000001b
    }
}
