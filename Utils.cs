
using System;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Text;
using Native;

namespace Utils
{
    public static class WlanUtils
    {
        [DebuggerStepThrough]

        public static string ConvertToString(this DOT11_MAC_ADDRESS mac)
        {
            return BitConverter.ToString(mac.PhysicalAddress).Replace('-' , ':');
        }

        public static string ConvertToString(this PhysicalAddress mac)
        {
            return BitConverter.ToString(mac.GetAddressBytes()).Replace('-', ':');
        }

        public static PhysicalAddress Reverse(this PhysicalAddress PhysicalAddress)
        {
            byte[] macaddr = PhysicalAddress.GetAddressBytes();
            Array.Reverse(macaddr);
            return new PhysicalAddress(macaddr);
        }

        public static PhysicalAddress GetPhysicalAddress(this DOT11_MAC_ADDRESS mac)
        {
            return new PhysicalAddress(mac.PhysicalAddress);
        }

        public static string ConvertToString(this DOT11_SSID ssid)
        {
            return ssid.ucSSID;
        }

        public static string ConvertToString(this byte[] array)
        {
            return Encoding.Unicode.GetString(array).Trim('\u0000');
        }

        public static string ConvertToHexString(this byte value)
        {
            return Convert.ToString(value, 0x10).PadLeft(2, '0');
        }

        public static DOT11_SSID ConvertStringToDOT11_SSID(string ssid)
        {
            return new DOT11_SSID()
            {
                ucSSID = ssid,
                uSSIDLength = (uint)ssid.Length
            };
        }
    }
}
