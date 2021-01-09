using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Native
{
    public struct WLAN_AUTH_CIPHER_PAIR_LIST
    {
        uint dwNumberOfItems;
        DOT11_AUTH_CIPHER_PAIR pAuthCipherPairList;
    }
}
