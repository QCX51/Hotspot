using System;
using System.Runtime.InteropServices;

namespace Native
{
    /// <summary>
    /// Contains information provided when registering for notifications.
    /// </summary>
    /// <remarks>
    /// Corresponds to the native <c>WLAN_NOTIFICATION_DATA</c> type.
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    public struct WLAN_NOTIFICATION_DATA
    {
        public WLAN_NOTIFICATION_SOURCE NotificationSource;
        public uint NotificationCode;
        public Guid InterfaceGuid;
        public uint dwDataSize;
        public IntPtr pData;
    }
}
