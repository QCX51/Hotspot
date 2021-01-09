
using System;
using System.Runtime.InteropServices;

namespace Native
{
    [StructLayout(LayoutKind.Sequential)]
    public struct SOCKET_ADDRESS
    {
        public SOCKADDR lpSockAddr;
        public int iSockaddrLength;
    }
}
