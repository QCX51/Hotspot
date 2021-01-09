using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Native
{
    public struct WLAN_RADIO_STATE
    {
        uint dwNumberOfPhys;
        WLAN_PHY_RADIO_STATE[] PhyRadioState;
    }
}
