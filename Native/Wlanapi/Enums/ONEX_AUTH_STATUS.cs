using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Native
{
    public enum ONEX_AUTH_STATUS
    {
        OneXAuthNotStarted,
        OneXAuthInProgress,
        OneXAuthNoAuthenticatorFound,
        OneXAuthSuccess,
        OneXAuthFailure,
        OneXAuthInvalid
    }
}
