﻿
namespace Native
{
    public enum IPTypes
    {
        DEFAULT_MINIMUM_ENTITIES = 32,
        MAX_ADAPTER_ADDRESS_LENGTH = 8,
        MAX_ADAPTER_DESCRIPTION_LENGTH = 128,
        MAX_ADAPTER_NAME_LENGTH = 256,
        MAX_DOMAIN_NAME_LEN = 128,
        MAX_HOSTNAME_LEN = 128,
        MAX_SCOPE_ID_LEN = 256,
        BROADCAST_NODETYPE = 1,
        PEER_TO_PEER_NODETYPE = 2,
        MIXED_NODETYPE = 4,
        HYBRID_NODETYPE = 8,
        IF_OTHER_ADAPTERTYPE = 0,
        IF_ETHERNET_ADAPTERTYPE = 1,
        IF_TOKEN_RING_ADAPTERTYPE = 2,
        IF_FDDI_ADAPTERTYPE = 3,
        IF_PPP_ADAPTERTYPE = 4,
        IF_LOOPBACK_ADAPTERTYPE = 5,
    }
}
