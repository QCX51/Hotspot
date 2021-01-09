using System;
using System.Runtime.InteropServices;

namespace Native
{
    public static class wlanapi
    {
        /// <summary>Client version of Windows XP with SP3 and Wireless LAN API for Windows XP with SP2</summary>
        public const uint WLAN_CLIENT_VERSION_XP = 1;
        /// <summary>Client version for Windows Vista and Windows Server 2008</summary>
        public const uint WLAN_CLIENT_VERSION_VISTA = 2;

        public const uint WLAN_AVAILABLE_NETWORK_INCLUDE_ALL_ADHOC_PROFILES = 0x00000001;
        public const uint WLAN_AVAILABLE_NETWORK_INCLUDE_ALL_MANUAL_HIDDEN_PROFILES = 0x00000002;

        public const uint WLAN_CONNECTION_HIDDEN_NETWORK = 0x00000001;
        public const uint WLAN_CONNECTION_ADHOC_JOIN_ONLY = 0x00000002;
        public const uint WLAN_CONNECTION_IGNORE_PRIVACY_BIT = 0x00000004;
        public const uint WLAN_CONNECTION_EAPOL_PASSTHROUGH = 0x00000008;

        public delegate void WLAN_NOTIFICATION_CALLBACK(ref WLAN_NOTIFICATION_DATA notificationData, IntPtr context);

        [DllImport("Wlanapi.dll", SetLastError = true)]
        public static extern uint WlanConnect([In] IntPtr hClientHandle, ref Guid pInterfaceGuid, ref WLAN_CONNECTION_PARAMETERS pConnectionParameters, IntPtr pReserved);

        [DllImport("Wlanapi.dll", EntryPoint = "WlanCloseHandle")]
        public static extern uint WlanCloseHandle([In] IntPtr hClientHandle, IntPtr pReserved);

        [DllImport("Wlanapi.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern uint WlanDeleteProfile([In] IntPtr hClientHandle, ref Guid pInterfaceGuid, string strProfileName, IntPtr pReserved);

        [DllImport("Wlanapi.dll", SetLastError = true)]
        public static extern uint WlanDisconnect([In] IntPtr hClientHandle, ref Guid pInterfaceGuid, IntPtr pReserved);

        /// <summary>
        /// The WlanEnumInterfaces function enumerates all of the wireless LAN interfaces currently enabled on the local computer.
        /// </summary>
        /// <param name="hClientHandle">The client's session handle, obtained by a previous call to the WlanOpenHandle function.</param>
        /// <param name="pReserved">Reserved for future use. This parameter must be set to NULL.</param>
        /// <param name="ppInterfaceList">A pointer to storage for a pointer to receive the returned list of wireless LAN interfaces in a
        /// <see cref="WLAN_INTERFACE_INFO_LIST"/> structure.</param>
        /// <returns></returns>
        [DllImport("Wlanapi.dll", EntryPoint = "WlanEnumInterfaces")]
        public static extern uint WlanEnumInterfaces([In] IntPtr hClientHandle,[In] IntPtr pReserved, [Out] out IntPtr ppInterfaceList);

        [DllImport("Wlanapi.dll", EntryPoint = "WlanFreeMemory")]
        public static extern void WlanFreeMemory([In] IntPtr pMemory);

        [DllImport("Wlanapi.dll", EntryPoint = "WlanSetSecuritySettings")]
        public static extern void WlanSetSecuritySettings([In] IntPtr hClientHandle, [In] WLAN_SECURABLE_OBJECT SecurableObject, [In, MarshalAs(UnmanagedType.LPWStr)] string strModifiedSDDL);

        [DllImport("Wlanapi.dll", SetLastError = true)]
        public static extern uint WlanGetAvailableNetworkList([In] IntPtr hClientHandle, ref Guid pInterfaceGuid, uint dwFlags, IntPtr pReserved, ref IntPtr ppAvailableNetworkList);

        [DllImport("Wlanapi.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern uint WlanGetProfile([In] IntPtr hClientHandle, ref Guid pInterfaceGuid, string strProfileName, IntPtr pReserved, ref string pstrProfileXml, ref uint pdwFlags, ref uint pdwGrantedAccess);

        [DllImport("Wlanapi.dll", SetLastError = true)]
        public static extern uint WlanGetProfileList([In] IntPtr hClientHandle, ref Guid pInterfaceGuid, IntPtr pReserved, ref IntPtr ppProfileList);

        [DllImport("Wlanapi.dll", EntryPoint = "WlanHostedNetworkForceStart")]
        public static extern uint WlanHostedNetworkForceStart([In] IntPtr hClientHandle, [Out] out WLAN_HOSTED_NETWORK_REASON pFailReason, IntPtr pReserved);

        [DllImport("Wlanapi.dll", EntryPoint = "WlanHostedNetworkForceStop")]
        public static extern uint WlanHostedNetworkForceStop([In] IntPtr hClientHandle, [Out] out WLAN_HOSTED_NETWORK_REASON pFailReason, IntPtr pReserved);

        /// <summary>
        /// The WlanHostedNetworkInitSettings function configures and persists to storage the network connection settings
        /// (SSID and maximum number of peers, for example) on the wireless Hosted Network
        /// if these settings are not already configured.
        /// </summary>
        /// <param name="hClientHandle">The client's session handle, returned by a previous call to the WlanOpenHandle function.</param>
        /// <param name="pFailReason">An optional pointer to a value that receives the failure reason if the call to the WlanHostedNetworkInitSettings function fails.
        /// Possible values for the failure reason are from the WLAN_HOSTED_NETWORK_REASON enumeration</param>
        /// <param name="pReserved">Reserved for future use. This parameter must be NULL.</param>
        /// <returns></returns>
        [DllImport("Wlanapi.dll", EntryPoint = "WlanHostedNetworkInitSettings")]
        public static extern uint WlanHostedNetworkInitSettings([In] IntPtr hClientHandle, [Out] out WLAN_HOSTED_NETWORK_REASON pFailReason, IntPtr pReserved);

        [DllImport("Wlanapi.dll", EntryPoint = "WlanHostedNetworkQueryProperty")]
        public static extern uint WlanHostedNetworkQueryProperty([In] IntPtr hClientHandle, WLAN_HOSTED_NETWORK_OPCODE OpCode, [Out] out uint pDataSize,
            [Out] out IntPtr ppvData, [Out] out WLAN_OPCODE_VALUE_TYPE pWlanOpcodeValueType, IntPtr pReserved);

        [DllImport("Wlanapi.dll", EntryPoint = "WlanHostedNetworkQuerySecondaryKey")]
        public static extern uint WlanHostedNetworkQuerySecondaryKey([In] IntPtr hClientHandle, [Out] out uint pdwKeyLength,
            [Out, MarshalAs(UnmanagedType.LPStr)] out string ppucKeyData,
            [Out] out bool pbIsPassPhrase, [Out] out bool pbPersistent, [Out] out WLAN_HOSTED_NETWORK_REASON pFailReason, IntPtr pvReserved);

        [DllImport("Wlanapi.dll", SetLastError = true)]
        public static extern uint WlanHostedNetworkQueryStatus([In] IntPtr hClientHandle,
            [Out] out IntPtr ppWlanHostedNetworkStatus, [In, Out] IntPtr pvReserved);

        [DllImport("Wlanapi.dll", EntryPoint = "WlanHostedNetworkRefreshSecuritySettings")]
        public static extern uint WlanHostedNetworkRefreshSecuritySettings([In] IntPtr hClientHandle, [Out] out WLAN_HOSTED_NETWORK_REASON pFailReason, IntPtr pReserved);

        [DllImport("Wlanapi.dll", EntryPoint = "WlanHostedNetworkSetProperty")]
        public static extern uint WlanHostedNetworkSetProperty(IntPtr hClientHandle, WLAN_HOSTED_NETWORK_OPCODE OpCode,
            uint dwDataSize, IntPtr pvData, [Out] out WLAN_HOSTED_NETWORK_REASON pFailReason, IntPtr pvReserved);

        [DllImport("Wlanapi.dll", EntryPoint = "WlanHostedNetworkSetSecondaryKey")]
        public static extern uint WlanHostedNetworkSetSecondaryKey([In] IntPtr hClientHandle, uint dwKeyLength,
            [In, MarshalAs(UnmanagedType.LPStr)] string pucKeyData,
            bool bIsPassPhrase, bool bPersistent, [Out] out WLAN_HOSTED_NETWORK_REASON pFailReason, IntPtr pvReserved);

        [DllImport("Wlanapi.dll", EntryPoint = "WlanHostedNetworkStartUsing")]
        public static extern uint WlanHostedNetworkStartUsing([In] IntPtr hClientHandle, [Out] out WLAN_HOSTED_NETWORK_REASON pFailReason, IntPtr pReserved);

        [DllImport("Wlanapi.dll", EntryPoint = "WlanHostedNetworkStopUsing")]
        public static extern uint WlanHostedNetworkStopUsing([In] IntPtr hClientHandle, [Out] out WLAN_HOSTED_NETWORK_REASON pFailReason, IntPtr pReserved);

        /// <summary>
        /// The WlanOpenHandle function opens a connection to the server.
        /// </summary>
        /// <param name="dwClientVersion">The highest version of the WLAN API that the client supports.</param>
        /// <param name="pReserved">Reserved for future use. Must be set to NULL.</param>
        /// <param name="pdwNegotiatedVersion">The version of the WLAN API that will be used in this session.
        /// This value is usually the highest version supported by both the client and server.</param>
        /// <param name="ClientHandle">A handle for the client to use in this session.
        /// This handle is used by other functions throughout the session.</param>
        /// <returns></returns>
        [DllImport("Wlanapi.dll", EntryPoint = "WlanOpenHandle")]
        public static extern uint WlanOpenHandle(uint dwClientVersion, IntPtr pReserved, [Out] out uint pdwNegotiatedVersion, ref IntPtr ClientHandle);

        [DllImport("Wlanapi.dll", EntryPoint = "WlanQueryInterface")]
        public static extern uint WlanQueryInterface([In] IntPtr hClientHandle, [In] ref Guid pInterfaceGuid,[In] WLAN_INTF_OPCODE OpCode, IntPtr pReserved, [Out] out uint pdwDataSize, [Out] out IntPtr ppData, [In] WLAN_OPCODE_VALUE_TYPE pWlanOpcodeValueType);

        /// <summary>
        /// The WlanRegisterNotification function is used to register and unregister notifications on all wireless interfaces.
        /// </summary>
        /// <param name="hClientHandle">The client's session handle, obtained by a previous call to the WlanOpenHandle function.</param>
        /// <param name="dwNotifSource">The notification sources to be registered. These flags may be combined. When this parameter is set to WLAN_NOTIFICATION_SOURCE_NONE,
        /// WlanRegisterNotification unregisters notifications on all wireless interfaces.</param>
        /// <param name="bIgnoreDuplicate">Specifies whether duplicate notifications will be ignored. If set to TRUE, a notification will not be sent to the client if it is identical to the previous one.
        /// Windows XP with SP3 and Wireless LAN API for Windows XP with SP2:  This parameter is ignored.</param>
        /// <param name="funcCallback">A WLAN_NOTIFICATION_CALLBACK type that defines the type of notification callback function.
        /// This parameter can be NULL if the dwNotifSource parameter is set to WLAN_NOTIFICATION_SOURCE_NONE to unregister notifications on all wireless interfaces,</param>
        /// <param name="pCallbackContext">A pointer to the client context that will be passed to the callback function with the notification.</param>
        /// <param name="pReserved">Reserved for future use. Must be set to NULL.</param>
        /// <param name="pdwPrevNotifSource">A pointer to the previously registered notification sources.</param>
        /// <returns></returns>
        [DllImport("Wlanapi.dll", EntryPoint = "WlanRegisterNotification")]
        public static extern uint WlanRegisterNotification([In] IntPtr hClientHandle, WLAN_NOTIFICATION_SOURCE dwNotifSource, bool bIgnoreDuplicate,
            WLAN_NOTIFICATION_CALLBACK funcCallback, IntPtr pCallbackContext, IntPtr pReserved, [Out] out WLAN_NOTIFICATION_SOURCE pdwPrevNotifSource);

        [DllImport("Wlanapi.dll", SetLastError = true)]
        public static extern uint WlanScan([In] IntPtr hClientHandle, ref Guid pInterfaceGuid, IntPtr pDot11Ssid, IntPtr pIeData, IntPtr pReserved);

        [DllImport("Wlanapi.dll")]
        public static extern uint WlanSetInterface([In] IntPtr hClientHandle, ref Guid pInterfaceGuid, WLAN_INTF_OPCODE OpCode, uint dwDataSize, ref object obj, IntPtr pReserved);

        [DllImport("Wlanapi.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern uint WlanSetProfile([In] IntPtr hClientHandle, ref Guid pInterfaceGuid, uint dwFlags, string strProfileXml, string strAllUserProfileSecurity, bool bOverwrite, IntPtr pReserved, ref uint pdwReasonCode);

        [DllImport("Wlanapi.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern uint WlanSetProfileList([In] IntPtr hClientHandle, ref Guid pInterfaceGuid, uint dwItems, string[] strProfileNames, IntPtr pReserved);

        [DllImport("Wlanapi.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern uint WlanSetProfilePosition([In] IntPtr hClientHandle, ref Guid pInterfaceGuid, string strProfileName, uint dwPosition, IntPtr pReserved);

        [DllImport("wlanapi.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern uint WlanQueryAutoConfigParameter([In] IntPtr hClientHandle, [In] WLAN_AUTOCONF_OPCODE OpCode, [Out] out UIntPtr pdwDataSize, [Out] out bool ppvData, [Out] out WLAN_OPCODE_VALUE_TYPE pWlanOpcodeValueType, IntPtr pvReserved);
        //public static extern uint WlanQueryAutoConfigParameter([In] IntPtr hClientHandle, [In] WLAN_AUTOCONF_OPCODE OpCode, UIntPtr reserved, [Out] out UIntPtr pdwDataSize, [Out] out bool ppvData, [Out] out WLAN_OPCODE_VALUE_TYPE pWlanOpcodeValueType);

        [DllImport("wlanapi.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern uint WlanReasonCodeToString([In] uint dwReasonCode, [In] uint dwBufferSize, [In, Out] string pStringBuffer, [In] IntPtr pReserved);

        /// <summary>
        /// The WlanRegisterVirtualStationNotification function is used to register and unregister notifications on a virtual station.
        /// </summary>
        /// <param name="hClientHandle">The client's session handle, obtained by a previous call to the WlanOpenHandle function.</param>
        /// <param name="bRegister">A value that specifies whether to receive notifications on a virtual station.</param>
        /// <param name="pReserved">Reserved for future use. This parameter must be NULL.</param>
        /// <returns></returns>
        [DllImport("wlanapi.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern uint WlanRegisterVirtualStationNotification([In] IntPtr hClientHandle, [In] bool bRegister, [In] IntPtr pReserved);

        [DllImport("wlanapi.dll", EntryPoint = "WlanGetInterfaceCapability", CharSet = CharSet.Unicode)]
        public static extern uint WlanGetInterfaceCapability([In] IntPtr hClientHandle, [In, Out] ref Guid pInterfaceGuid, [In] IntPtr pReserved, [In, Out] ref IntPtr ppCapability);
    }
}
