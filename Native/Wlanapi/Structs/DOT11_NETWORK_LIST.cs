using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Native
{
    public struct DOT11_NETWORK_LIST
    {
        uint dwNumberOfItems;
        uint dwIndex;
        DOT11_NETWORK[] Network;
    }
}
