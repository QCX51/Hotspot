using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Native
{
    public struct ONEX_STATUS
    {
        public ONEX_AUTH_STATUS authStatus;
        public uint dwReason;
        public uint dwError;
    }
}
