using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Native
{
    public enum ONEX_AUTH_RESTART_REASON
    {
        OneXRestartReasonPeerInitiated,
        OneXRestartReasonMsmInitiated,
        OneXRestartReasonOneXHeldStateTimeout,
        OneXRestartReasonOneXAuthTimeout,
        OneXRestartReasonOneXConfigurationChanged,
        OneXRestartReasonOneXUserChanged,
        OneXRestartReasonQuarantineStateChanged,
        OneXRestartReasonAltCredsTrial,
        OneXRestartReasonInvalid
    }
}
