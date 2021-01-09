namespace Native
{
    public enum WLAN_NOTIFICATION_SOURCE_ONEX
    {
        OneXPublicNotificationBase = 0,
        //ndicates the beginning of the range that specifies the possible values for 802.1X notifications.
        OneXNotificationTypeResultUpdate,
        /*
         * Indicates that 802.1X authentication has a status change.
         * The pData member of the WLAN_NOTIFICATION_DATA structure points to a ONEX_RESULT_UPDATE_DATA structure that contains 802.1X update data.
         */
        OneXNotificationTypeAuthRestarted,
        /*
         * Indicates that 802.1X authentication restarted.
         * The pData member of the WLAN_NOTIFICATION_DATA structure points to an ONEX_AUTH_RESTART_REASON enumeration value that identifies the reason the authentication was restarted.
         */
        OneXNotificationTypeEventInvalid,
        //Indicates the end of the range that specifies the possible values for 802.1X notifications.
        OneXNumNotifications = OneXNotificationTypeEventInvalid
        //Indicates the end of the range that specifies the possible values for 802.1X notifications.
    }
}