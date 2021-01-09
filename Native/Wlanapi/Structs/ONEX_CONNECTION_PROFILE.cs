using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Native
{
    public struct ONEX_CONNECTION_PROFILE
    {
        uint dwVersion;
        uint dwTotalLen;
        uint fOneXSupplicantFlags;
        uint fsupplicantMode;
        uint fauthMode;
        uint fHeldPeriod;
        uint fAuthPeriod;
        uint fStartPeriod;
        uint fMaxStart;
        uint fMaxAuthFailures;
        uint fNetworkAuthTimeout;
        uint fAllowLogonDialogs;
        uint fNetworkAuthWithUITimeout;
        uint fUserBasedVLan;
        uint dwOneXSupplicantFlags;
        ONEX_SUPPLICANT_MODE supplicantMode;
        ONEX_AUTH_MODE authMode;
        uint dwHeldPeriod;
        uint dwAuthPeriod;
        uint dwStartPeriod;
        uint dwMaxStart;
        uint dwMaxAuthFailures;
        uint dwNetworkAuthTimeout;
        uint dwNetworkAuthWithUITimeout;
        bool bAllowLogonDialogs;
        bool bUserBasedVLan;
    }

    public enum ONEX_SUPPLICANT_MODE
    {
        OneXSupplicantModeInhibitTransmission, // 0 EAPOL-Start messages are not transmitted. Valid for wired LAN profiles only.

        OneXSupplicantModeLearn, // 1 The client determines when to send EAPOL-Start packets based on network capability. EAPOL-Start messages are only sent when required. Valid for wired LAN profiles only.

        OneXSupplicantModeCompliant, // 2 EAPOL-Start messages are transmitted as specified by 802.1X. Valid for both wired and wireless LAN profiles.

    }
    public enum ONEX_AUTH_MODE
    {

        OneXAuthModeMachineOrUser, // 0 Use machine or user credentials. When a user is logged on, the user's credentials are used for authentication. When no user is logged on, machine credentials are used.

        OneXAuthModeMachineOnly, // 1 Use machine credentials only.

        OneXAuthModeUserOnly, // 2 Use user credentials only.

        OneXAuthModeGuest, // 3 Use guest (empty) credentials only.

        OneXAuthModeUnspecified, // 4 Credentials to use are not specified.
    }
}
