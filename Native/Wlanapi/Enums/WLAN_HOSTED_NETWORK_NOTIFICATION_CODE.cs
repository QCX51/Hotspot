
namespace Native
{
    /// <summary>
    /// The WLAN_HOSTED_NETWORK_NOTIFICATION_CODE enumerated type specifies the possible values of the NotificationCode parameter
    /// for received notifications on the wireless Hosted Network.
    /// <a href="https://msdn.microsoft.com/en-us/library/windows/desktop/dd439501(v=vs.85).aspx"></a>
    /// </summary>
    public enum WLAN_HOSTED_NETWORK_NOTIFICATION_CODE
    {
        /// <summary>
        /// The wireless Hosted Network state has changed.
        /// The <strong>pData</strong> member points to a <a href="https://msdn.microsoft.com/en-us/library/dd439509(v=vs.85)">
        /// <strong>WLAN_HOSTED_NETWORK_STATE_CHANGE</strong></a> structure that identifies the state change.
        /// </summary>
        wlan_hosted_network_state_change = 0x00001000,
        /// <summary>
        /// The wireless Hosted Network peer state has changed.
        /// The <strong>pData</strong> member points to a <a href="https://msdn.microsoft.com/en-us/library/dd439500(v=vs.85)">
        /// <strong>WLAN_HOSTED_NETWORK_DATA_PEER_STATE_CHANGE</strong></a> structure that identifies the peer state change.
        /// </summary>
        wlan_hosted_network_peer_state_change = 0x00001001,
        /// <summary>
        /// The wireless Hosted Network radio state has changed.
        /// The <strong>pData</strong> member points to a <a href="https://msdn.microsoft.com/en-us/library/dd439505(v=vs.85)">
        /// <strong>WLAN_HOSTED_NETWORK_RADIO_STATE</strong></a> structure that identifies the radio state change.
        /// </summary>
        wlan_hosted_network_radio_state_change = 0x00001002
    }
}
