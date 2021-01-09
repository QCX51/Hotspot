﻿
namespace Native
{
    public enum TUNNEL_TYPE : uint
    {
        TUNNEL_TYPE_NONE = 0,
        TUNNEL_TYPE_OTHER = 1,
        TUNNEL_TYPE_DIRECT = 2,
        TUNNEL_TYPE_6TO4 = 11,
        TUNNEL_TYPE_ISATAP = 13,
        TUNNEL_TYPE_TEREDO = 14,
        TUNNEL_TYPE_IPHTTPS = 15
    }
}
