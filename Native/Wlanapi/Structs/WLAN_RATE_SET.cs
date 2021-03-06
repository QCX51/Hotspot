﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Native
{
    public struct WLAN_RATE_SET
    {
        ulong uRateSetLength;
        /// <summary>
        /// An array of supported data transfer rates. DOT11_RATE_SET_MAX_LENGTH is defined in windot11.h to have a value of 126.
        /// Each supported data transfer rate is stored as a USHORT. The first bit of the USHORT specifies whether the rate is a basic rate.
        /// A basic rate is the data transfer rate that all stations in a basic service set (BSS) can use to receive frames from the wireless medium.
        /// If the rate is a basic rate, the first bit of the USHORT is set to 1.
        /// </summary>
        ushort[] usRateSet; // USHORT usRateSet[DOT11_RATE_SET_MAX_LENGTH];
    }
}
