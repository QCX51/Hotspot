using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Native
{
    public struct ONEX_AUTH_PARAMS
    {
        bool fUpdatePending;
        ONEX_VARIABLE_BLOB oneXConnProfile;
        ONEX_AUTH_IDENTITY authIdentity;
        uint dwQuarantineState;
        uint fSessionId;
        uint fhUserToken;
        uint fOnexUserProfile;
        uint fIdentity;
        uint fUserName;
        uint fDomain;
        uint dwSessionId;
        uint hUserToken;
        ONEX_VARIABLE_BLOB OneXUserProfile;
        ONEX_VARIABLE_BLOB Identity;
        ONEX_VARIABLE_BLOB UserName;
        ONEX_VARIABLE_BLOB Domain;
    }
}
