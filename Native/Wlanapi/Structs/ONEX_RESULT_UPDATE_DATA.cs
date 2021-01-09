using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Native
{
    public struct ONEX_RESULT_UPDATE_DATA
    {
        public ONEX_STATUS oneXStatus;
        public ONEX_EAP_METHOD_BACKEND_SUPPORT BackendSupport;
        public bool fBackendEngaged;
        public uint fOneXAuthParams;
        public uint fEapError;
        public ONEX_VARIABLE_BLOB authParams;
        public ONEX_VARIABLE_BLOB eapError;
    }
}
