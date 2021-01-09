namespace Native
{
    /// https://msdn.microsoft.com/en-us/library/windows/desktop/ms706835(v=vs.85).aspx
    public enum WLAN_AUTOCONF_OPCODE
    {
        wlan_autoconf_opcode_start = 0,
        wlan_autoconf_opcode_show_denied_networks = 1,
        wlan_autoconf_opcode_power_setting = 2,
        wlan_autoconf_opcode_only_use_gp_profiles_for_allowed_networks = 3,
        wlan_autoconf_opcode_allow_explicit_creds = 4,
        wlan_autoconf_opcode_block_period = 5,
        wlan_autoconf_opcode_allow_virtual_station_extensibility = 6,
        wlan_autoconf_opcode_end = 7
    }
}
