using Microsoft.Win32;
using System;

namespace Hotspot
{
    internal static class Settings
    {
        internal static string SSID = "Hotspot-" + Environment.UserName.Split(new string[] { " " }, StringSplitOptions.None)[0];
        internal static string Key = Guid.NewGuid().ToString().Remove(8);
        public static RegistryKey RegKey
        { get { return Registry.LocalMachine.CreateSubKey(@"software\Hotspot", RegistryKeyPermissionCheck.ReadWriteSubTree); } }

        internal static void Save(string SSID, string Key)
        {
            using (RegistryKey RegKey = Settings.RegKey)
            {
                RegKey.SetValue("ssid", SSID, RegistryValueKind.String);
                RegKey.SetValue("key", Key, RegistryValueKind.String);
                Read();
            }
        }
        internal static void Read()
        {
            using (RegistryKey RegKey = Settings.RegKey)
            {
                SSID = Convert.ToString(RegKey.GetValue("ssid", SSID));
                Key = Convert.ToString(RegKey.GetValue("key", Key));
            }
        }
    }
}
