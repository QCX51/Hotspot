using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Native
{
    public enum WLAN_SECURABLE_OBJECT
    {
        wlan_secure_permit_list = 0,
        wlan_secure_deny_list = 1,
        wlan_secure_ac_enabled = 2,
        wlan_secure_bc_scan_enabled = 3,
        wlan_secure_bss_type = 4,
        wlan_secure_show_denied = 5,
        wlan_secure_interface_properties = 6,
        wlan_secure_ihv_control = 7,
        wlan_secure_all_user_profiles_order = 8,
        wlan_secure_add_new_all_user_profiles = 9,
        wlan_secure_add_new_per_user_profiles = 10,
        wlan_secure_media_streaming_mode_enabled = 11,
        wlan_secure_current_operation_mode = 12,
        wlan_secure_get_plaintext_key = 13,
        wlan_secure_hosted_network_elevated_access = 14,
        wlan_secure_virtual_station_extensibility = 15,
        wlan_secure_wfd_elevated_access = 16
    }
}
