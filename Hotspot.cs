using System;
using System.Reflection;
using System.Windows.Forms;
using System.Threading;
using Utils;
using System.Diagnostics;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Native;

namespace Hotspot
{
    /// <summary>
    /// 
    /// vWI-FI Hotspot
    /// Copyright (C) Alain Eus. Rivera Rios
    /// ....::: .*.*.* QCX51 *.*.*. :::....
    /// Date: 24/09/2019
    /// 
    /// </summary>
    public partial class Hotspot : Form
    {
        public static WlanManager WlanMgr;
        internal List<NetworkInterfaceInfo> NetInterfaceList = new List<NetworkInterfaceInfo>();
        internal static Classes.GridList DataGridList = new Classes.GridList();

        public Hotspot()
        {
            InitializeComponent();
            //CheckForIllegalCrossThreadCalls = false;
            Size = new System.Drawing.Size(272, 432);
            MaximumSize = new System.Drawing.Size(Size.Width + 800, Size.Height);
            MinimumSize = Size;

            Settings.Read();
            Settings.Save(Settings.SSID, Settings.Key);
            DataGridList.AddColumn("Hostname", true, false, true, true, 90);
            DataGridList.AddColumn("MAC", true, false, true, true, 80);
            DataGridList.AddColumn("IP", true, false, true, true, 80);
            DataGridList.AddColumn("Status", true, false, true, true, 60);
            DataGridList.AddColumn("Alias", false, false, true, true, 60);
            DataGridList.AddColumn("Time", true, false, true, false, 40);
            DataGridList.AddColumn("Action", true, false, false, false, 40);
            ListPanel.Controls.Add(DataGridList);
            FormClosing += OnMainFormClosing;
        }

        private void UpdateInterfaceList(object sender, EventArgs e)
        {
            NetIList.Items.Clear();
            NetInterfaceList.Clear();
            NetIList.DropDownStyle = ComboBoxStyle.DropDownList;
            //foreach (NetworkInterfaceInfo NetITFInfo in ICSRouter.GetNetworkInterfaces())
            foreach (NetworkInterfaceInfo NetITFInfo in ICSRouter.GetNetworkConnectionsInfo())
            {
                if (NetITFInfo.InterfaceGuid.Equals(WlanMgr.HostedNetworkGuid)) continue;
                NetIList.DropDownWidth = 300;
                NetIList.Items.Add($"{NetITFInfo.DeviceName} ({NetITFInfo.DeviceDescription})");
                NetInterfaceList.Add(NetITFInfo);
            }
            if (NetIList.Items.Count <= 0)
            {
                Invoke(new Action(() =>
                { IPvXBox.ForeColor = System.Drawing.Color.Black; IPvXBox.Text = $"No NIC selected"; }));
                MessageBox.Show("There is no network interface connected to a network", Text,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                Invoke(new Action(() =>
                { IPvXBox.ForeColor = System.Drawing.Color.Black; IPvXBox.Text = $"No NIC selected"; }));
            }
        }

        private void OnInterfaceSelected(object sender, EventArgs e)
        {
            if (NetIList.SelectedIndex >= 0)
            {
                NetworkInterfaceInfo NetITFInfo;
                NetITFInfo = NetInterfaceList[NetIList.SelectedIndex];
                IPvXBox.ForeColor = NetITFInfo.IsConnectedToInternet ?
                System.Drawing.Color.Green : System.Drawing.Color.Red;
                IPvXBox.Text = NetITFInfo.IPAddress.ToString();
                IPvXBox.Text += NetITFInfo.IsConnectedToInternet ?
                "\nConnected to internet" : "\nNot connected to internet";
            }
        }

        private static readonly string[] SizeSuffixes = { "Bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };

        private static string SizeSuffix(long value, int decimalPlaces = 1)
        {
            if (value <= 0) { return string.Format("{0} bytes", 0); }

            // mag is 0 for bytes, 1 for KB, 2, for MB, etc.
            int mag = (int)Math.Log(value, 1024);

            // 1L << (mag * 10) == 2 ^ (10 * mag) 
            // [i.e. the number of bytes in the unit corresponding to mag]
            decimal adjustedSize = (decimal)value / (1L << (mag * 10));

            // make adjustment when the value is large enough that
            // it would round up to 1000 or more
            if (Math.Round(adjustedSize, decimalPlaces) >= 1024)
            {
                mag += 1;
                adjustedSize /= 1024;
            }

            return string.Format("{0:n" + (decimalPlaces < 1024 ? 0 : 1) + "} {1}", adjustedSize, SizeSuffixes[mag]);
        }

        private long LastRXBytes { get; set; }
        private long LastTXBytes { get; set; }

        private void UpdateHostedNetworkStatistic()
        {
            do
            {
                foreach (var item in NetworkInterface.GetAllNetworkInterfaces())
                {
                    if (Guid.Parse(item.Id).Equals(WlanMgr.HostedNetworkGuid))
                    {
                        long rx = item.GetIPStatistics().BytesReceived;
                        long tx = item.GetIPStatistics().BytesSent;

                        tx = tx >= 0 ? tx : 0;
                        rx = rx >= 0 ? rx : 0;
                        long rxx = (rx - LastRXBytes);
                        long txx = (tx - LastTXBytes);

                        ActivityLabel.Invoke(new Action(() =>
                        { ActivityLabel.Text = $"TX: {SizeSuffix(rxx)} RX: {SizeSuffix(txx)}"; }));

                        LastRXBytes = item.GetIPStatistics().BytesReceived;
                        LastTXBytes = item.GetIPStatistics().BytesSent;
                        Thread.Sleep(TimeSpan.FromMilliseconds(100));
                        break;
                    }
                }
            } while (true);
        }

        private void UpdatePeerListInfo()
        {
            foreach (WLAN_HOSTED_NETWORK_PEER_STATE PeerState in WlanMgr.HostedNetworkPeerList)
            {
                string PhysicalAddress = PeerState.PhysicalAddress.ConvertToString();
                UpdateStationStatus(string.Empty, PhysicalAddress, string.Empty, "Connected");
                OnHostedNetworkPeerStateChanged(PeerState);
            }
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            SSIDText.Text = Settings.SSID;
            KeyText.Text = Settings.Key;

            WlanMgr = new WlanManager();
            if (!WlanMgr.IsHostedNetworkSupported)
            {
                UnsupportedDevices();
                return;
            }

            if (WlanMgr.IsHostedNetworkActive)
            {
                IPhlpapi.FlushIpNetTable(WlanMgr.HostedNetworkGuid);
                WlanMgr.ForceStop();
            }

            NetIList.DropDown += UpdateInterfaceList;
            NetIList.SelectionChangeCommitted += OnInterfaceSelected;
            DataGridList.CellContentClick += OnCellContentClick;
            DataGridList.CellEndEdit += OnCellEndEdit;
            ShareButton.MouseClick += OnShareButtonClick;

            WlanMgr.HostedNetworkStateChange += OnHostedNetworkStateChange;
            WlanMgr.HostedNetworkPeerStateChanged += OnHostedNetworkPeerStateChanged;
            WlanMgr.WlanSoftwareRadioStateChanged += OnWlanSoftwareRadioStateChanged;
            OnHostedNetworkStateChange(WlanMgr.HostedNetworkState);

            System.Threading.Tasks.Task.Run(() => UpdateHostedNetworkStatistic());
            System.Threading.Tasks.Task.Run(() => UpdatePeerListInfo());
        }

        private string GetPeerAlias(string PhysicalAddress)
        {
            int Index = Properties.Settings.Default.AliasID.IndexOf(PhysicalAddress);
            if (Index >= 0) { return Properties.Settings.Default.Alias[Index]; }
            return string.Empty;
        }

        private void OnCellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex.Equals(4))
            {
                string id = DataGridList.Rows[e.RowIndex].Cells[1].Value as string ?? string.Empty;
                string alias = DataGridList.Rows[e.RowIndex].Cells[4].Value as string ?? string.Empty;

                int Index = Properties.Settings.Default.AliasID.IndexOf(id);
                alias = alias.Trim().Equals("") ? " " : alias.Trim();

                if (!Properties.Settings.Default.AliasID.Contains(id))
                {
                    Properties.Settings.Default.Alias.Add(alias);
                    Properties.Settings.Default.AliasID.Add(id);
                    Properties.Settings.Default.Save();
                }
                else if (Index >= 0)
                {
                    Properties.Settings.Default.AliasID[Index] = id;
                    Properties.Settings.Default.Alias[Index] = alias;
                    Properties.Settings.Default.Save();
                }
            }
        }

        private void OnCellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex.Equals(6))
            {
                string mac = DataGridList.Rows[e.RowIndex].Cells[1].Value as string ?? string.Empty;
                string ip = DataGridList.Rows[e.RowIndex].Cells[2].Value as string ?? string.Empty;
                mac = mac.Replace(':', '-');

                if (System.Net.IPAddress.TryParse(ip, out System.Net.IPAddress address))
                {
                    PhysicalAddress macaddr = PhysicalAddress.Parse(mac);
                    switch ((int)DataGridList.Rows[e.RowIndex].Cells[6].Tag)
                    {
                        case 0:
                            DataGridList.Rows[e.RowIndex].Cells[6].Value = "Block";
                            DataGridList.Rows[e.RowIndex].Cells[6].Tag = 1;
                            IPhlpapi.SetIpNetEntry(WlanMgr.HostedNetworkGuid, macaddr, address);
                            Trace.WriteLine("Allowed: IP: " + ip + " MAC: " + macaddr.ToString());
                            do { Properties.Settings.Default.Blacklist.Remove(macaddr.ToString()); }
                            while (Properties.Settings.Default.Blacklist.Contains(macaddr.ToString()));
                            Properties.Settings.Default.Whitelist.Add(macaddr.ToString());
                            Properties.Settings.Default.Save();
                            break;
                        case 1:
                            PhysicalAddress bmacaddr = macaddr.Reverse();
                            DataGridList.Rows[e.RowIndex].Cells[6].Value = "Allow";
                            DataGridList.Rows[e.RowIndex].Cells[6].Tag = 0;
                            IPhlpapi.SetIpNetEntry(WlanMgr.HostedNetworkGuid, bmacaddr, address);
                            Trace.WriteLine("Blocked: IP: " + ip + " MAC: " + macaddr.ToString());
                            do { Properties.Settings.Default.Whitelist.Remove(macaddr.ToString()); }
                            while (Properties.Settings.Default.Whitelist.Contains(macaddr.ToString()));
                            Properties.Settings.Default.Blacklist.Add(macaddr.ToString());
                            Properties.Settings.Default.Save();
                            break;
                    }
                }
            }
        }

        private void OnWlanSoftwareRadioStateChanged(object sender, DOT11_RADIO_STATE state)
        {
            switch (state)
            {
                case DOT11_RADIO_STATE.dot11_radio_state_unknown:
                    Debug.WriteLine("dot11_radio_state_unknown");
                    break;
                case DOT11_RADIO_STATE.dot11_radio_state_on:
                    Debug.WriteLine("Software radio_state_on");
                    break;
                case DOT11_RADIO_STATE.dot11_radio_state_off:
                    Debug.WriteLine("Software radio_state_off");
                    break;
            }
        }

        private void OnHostedNetworkPeerStateChanged(WLAN_HOSTED_NETWORK_PEER_STATE PeerState)
        {
            switch (PeerState.PeerAuthState)
            {
                case WLAN_HOSTED_NETWORK_PEER_AUTH_STATE.wlan_hosted_network_peer_state_invalid:
                    UpdateStationStatus(string.Empty, PeerState.PhysicalAddress.ConvertToString(), string.Empty, "Disconnected");
                    IPhlpapi.DeleteIPNetEntry(WlanMgr.HostedNetworkGuid, PeerState.PhysicalAddress.Reverse());
                    IPhlpapi.DeleteIPNetEntry(WlanMgr.HostedNetworkGuid, PeerState.PhysicalAddress);
                    break;
                case WLAN_HOSTED_NETWORK_PEER_AUTH_STATE.wlan_hosted_network_peer_state_authenticated:
                    System.Threading.Tasks.Task.Run(() =>
                    {
                        int tries = 0;
                        System.Net.IPAddress PeerIPAddress = System.Net.IPAddress.None;
                        do
                        {
                            tries++;
                            PeerIPAddress = IPhlpapi.ResolveIPNetEntry(WlanMgr.HostedNetworkGuid, PeerState.PhysicalAddress);
                            Trace.WriteLine($"Try count: {tries}");
                            Thread.Sleep(TimeSpan.FromSeconds(1));
                        }
                        while (ICSRouter.IsConnected && (PeerIPAddress.Equals(System.Net.IPAddress.None) && tries < 60));
                        if (!PeerIPAddress.Equals(System.Net.IPAddress.None))
                        {
                            string HostName, Mac = PeerState.PhysicalAddress.ConvertToString();
                            try { HostName = System.Net.Dns.GetHostEntry(PeerIPAddress).HostName; }
                            catch { HostName = string.Empty; }
                            UpdateStationStatus(HostName, Mac, PeerIPAddress.ToString(), "Connected");
                            BlockConnection(PeerState.PhysicalAddress, PeerIPAddress);
                        }
                    });
                    break;
            }
        }

        private void BlockConnection(PhysicalAddress mac, System.Net.IPAddress addr)
        {
            Guid InterfaceGuid = WlanMgr.HostedNetworkGuid;
            if (!Properties.Settings.Default.Whitelist.Contains(mac.ToString()))
            {
                PhysicalAddress macaddr = mac.Reverse();
                IPhlpapi.SetIpNetEntry(InterfaceGuid, macaddr, addr);
                Invoke(new Action(() => UpdateBlacklistItems(mac)));
                Trace.WriteLine($"Blocked: IP: {addr} MAC: {mac}");
            }
            else
            {
                Invoke(new Action(() => UpdateWhitelistItems(mac)));
                Trace.WriteLine($"Allowed: IP: {addr} MAC: {mac}");
            }
        }

        private void UpdateBlacklistItems(PhysicalAddress wmac)
        {
            foreach (DataGridViewRow item in DataGridList.Rows)
            {
                if (item.Cells[1].Value.Equals(wmac.ConvertToString()))
                {
                    item.Cells[6].Value = "Allow";
                    item.Cells[6].Tag = 0;
                    item.Tag = wmac.ToString();
                }
            }
        }

        private void UpdateWhitelistItems(PhysicalAddress wmac)
        {
            foreach (DataGridViewRow item in DataGridList.Rows)
            {
                if (item.Cells[1].Value.Equals(wmac.ConvertToString()))
                {
                    item.Cells[6].Value = "Block";
                    item.Cells[6].Tag = 1;
                    item.Tag = wmac.ToString();
                }
            }
        }

        private void OnShareButtonClick(object sender, MouseEventArgs e)
        {
            if (SSIDText.Text.Length < 5)
            {
                MessageBox.Show("SSID Name must be at least 5 characters long."
                    , Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (KeyText.Text.Length < 6)
            {
                MessageBox.Show("Password must be at least 6 characters long."
                    , Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (NetIList.SelectedIndex < 0)
            {
                MessageBox.Show("Network interface not selected.\n\n" +
                    "Please select a network interface that you want to share its internet connection."
                    , Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                ((Button)sender).Enabled = false;
                NetworkInterfaceInfo NetITFInfo = NetInterfaceList[NetIList.SelectedIndex];
                Settings.Save(SSIDText.Text, KeyText.Text);

                new Thread(() =>
                {
                    Settings.Read();
                    StartHostedNetwork(Settings.SSID, Settings.Key, NetITFInfo.InterfaceGuid);
                    this?.Invoke(new Action(() => ((Button)sender).Enabled = true));
                })
                {
                    IsBackground = false,
                    Priority = ThreadPriority.AboveNormal
                }.Start();
            }
        }

        private void AddNewStation(string Hostname, string MAC, string IP, string status)
        {
            DataGridList.AddRow(new DataGridViewCell[]
            {
                new DataGridViewTextBoxCell() { Value = Hostname },
                new DataGridViewTextBoxCell() { Value = MAC },
                new DataGridViewTextBoxCell() { Value = IP },
                new DataGridViewTextBoxCell() { Value = status },
                new DataGridViewTextBoxCell() { Value = GetPeerAlias(MAC) },
                new DataGridViewTextBoxCell() { Value = "--:--:--" },
                new DataGridViewButtonCell() { Value = "Allow" },
            }, false, 40);
        }

        protected int IndexOf(string HardwareID)
        {
            foreach (DataGridViewRow row in DataGridList.Rows)
            {
                if (row.Cells[1].Value.ToString().ToLower().Equals(HardwareID.ToLower()))
                { return row.Index; }
            }
            return -1;
        }

        private void UpdateStationStatus(string Hostname, string MAC, string IP, string status)
        {
            if (DataGridList.InvokeRequired)
            {
                Invoke(new Action(() =>
                { UpdateStationStatus(Hostname, MAC, IP, status); }));
            }
            else
            {
                int Index = IndexOf(MAC);
                if (Index >= 0)
                {
                    DataGridList.Rows[Index].Cells[0].Value =
                        Hostname != string.Empty ? Hostname : DataGridList.Rows[Index].Cells[0].Value;
                    DataGridList.Rows[Index].Cells[1].Value =
                        MAC != string.Empty ? MAC : DataGridList.Rows[Index].Cells[1].Value;
                    DataGridList.Rows[Index].Cells[2].Value =
                        IP != string.Empty ? IP : DataGridList.Rows[Index].Cells[2].Value;
                    DataGridList.Rows[Index].Cells[3].Value =
                        status != string.Empty ? status : DataGridList.Rows[Index].Cells[3].Value;
                    DataGridList.Rows[Index].Cells[4].Value =
                        MAC != string.Empty ? GetPeerAlias(MAC) : DataGridList.Rows[Index].Cells[4].Value;
                }
                else { AddNewStation(Hostname, MAC, IP, status); }
            }
        }

        private void ShowKey_CheckedChanged(object sender, EventArgs e)
        {
            switch (((CheckBox)sender).Checked)
            {
                case true:
                    KeyText.UseSystemPasswordChar = false;
                    break;
                default:
                    KeyText.UseSystemPasswordChar = true;
                    break;
            }
        }

        private void OnExitButtonClick(object sender, EventArgs e)
        {
            AllowTransparency = true;
            for (double alpha = 1.00D; alpha >= 0.00D; alpha -= 0.01D)
            {
                this?.Invoke(new Action(() => Opacity = alpha));
                Thread.Sleep(TimeSpan.FromMilliseconds(5));
            }
            Application.Exit();
        }

        private void UnsupportedDevices()
        {
            NetIList.Items.Clear();
            NetIList.Items.Add("Unsupported WLAN interfaces");
            NetIList.SelectedIndex = 0;
        }

        private void OnHostedNetworkStateChange(WLAN_HOSTED_NETWORK_STATE WlanHnkState)
        {
            this?.Invoke(new Action(() =>
            {
                switch (WlanHnkState)
                {
                    case WLAN_HOSTED_NETWORK_STATE.wlan_hosted_network_unavailable:
                        StatusLabel.Text = $"Status: Unavailable";
                        ShareButton.Text = "Share";
                        NetIList.Enabled = true;
                        break;
                    case WLAN_HOSTED_NETWORK_STATE.wlan_hosted_network_idle:
                        StatusLabel.Text = $"Status: Idle";
                        ShareButton.Text = "Share";
                        NetIList.Enabled = true;
                        break;
                    case WLAN_HOSTED_NETWORK_STATE.wlan_hosted_network_active:
                        StatusLabel.Text = $"Status: Active";
                        ShareButton.Text = "Stop";
                        NetIList.Enabled = NetIList.SelectedIndex >= 0 ? false : true;
                        break;
                }
            }));
        }

        private string ShowHostedNetworkReason(WLAN_HOSTED_NETWORK_REASON Reason)
        {
            string Prefix = "The hosted network can not be started/stopped.\nReason: ";
            switch (Reason)
            {
                case WLAN_HOSTED_NETWORK_REASON.wlan_hosted_network_reason_success:
                    return string.Empty; //Prefix + "success";
                case WLAN_HOSTED_NETWORK_REASON.wlan_hosted_network_reason_unspecified:
                    return Prefix + "unspecified";
                case WLAN_HOSTED_NETWORK_REASON.wlan_hosted_network_reason_bad_parameters:
                    return Prefix + "bad parameters";
                case WLAN_HOSTED_NETWORK_REASON.wlan_hosted_network_reason_service_shutting_down:
                    return Prefix + "service shutting down";
                case WLAN_HOSTED_NETWORK_REASON.wlan_hosted_network_reason_insufficient_resources:
                    return Prefix + "insufficient resources";
                case WLAN_HOSTED_NETWORK_REASON.wlan_hosted_network_reason_elevation_required:
                    return Prefix + "elevation required";
                case WLAN_HOSTED_NETWORK_REASON.wlan_hosted_network_reason_read_only:
                    return Prefix + "read only";
                case WLAN_HOSTED_NETWORK_REASON.wlan_hosted_network_reason_persistence_failed:
                    return Prefix + "persistence failed";
                case WLAN_HOSTED_NETWORK_REASON.wlan_hosted_network_reason_crypt_error:
                    return Prefix + "crypt error";
                case WLAN_HOSTED_NETWORK_REASON.wlan_hosted_network_reason_impersonation:
                    return Prefix + "impersonation";
                case WLAN_HOSTED_NETWORK_REASON.wlan_hosted_network_reason_stop_before_start:
                    return Prefix + "stop before start";
                case WLAN_HOSTED_NETWORK_REASON.wlan_hosted_network_reason_interface_available:
                    return Prefix + "interface available";
                case WLAN_HOSTED_NETWORK_REASON.wlan_hosted_network_reason_interface_unavailable:
                    return Prefix + "interface unavailable";
                case WLAN_HOSTED_NETWORK_REASON.wlan_hosted_network_reason_miniport_stopped:
                    return Prefix + "miniport stopped";
                case WLAN_HOSTED_NETWORK_REASON.wlan_hosted_network_reason_miniport_started:
                    return Prefix + "miniport started";
                case WLAN_HOSTED_NETWORK_REASON.wlan_hosted_network_reason_incompatible_connection_started:
                    return Prefix + "incompatible connection started";
                case WLAN_HOSTED_NETWORK_REASON.wlan_hosted_network_reason_incompatible_connection_stopped:
                    return Prefix + "incompatible connection stopped";
                case WLAN_HOSTED_NETWORK_REASON.wlan_hosted_network_reason_user_action:
                    return Prefix + "user action";
                case WLAN_HOSTED_NETWORK_REASON.wlan_hosted_network_reason_client_abort:
                    return Prefix + "client abort";
                case WLAN_HOSTED_NETWORK_REASON.wlan_hosted_network_reason_ap_start_failed:
                    return Prefix + "AP start failed";
                case WLAN_HOSTED_NETWORK_REASON.wlan_hosted_network_reason_peer_arrived:
                    return Prefix + "peer arrived";
                case WLAN_HOSTED_NETWORK_REASON.wlan_hosted_network_reason_peer_departed:
                    return Prefix + "peer departed";
                case WLAN_HOSTED_NETWORK_REASON.wlan_hosted_network_reason_peer_timeout:
                    return Prefix + "peer timeout";
                case WLAN_HOSTED_NETWORK_REASON.wlan_hosted_network_reason_gp_denied:
                    return Prefix + "GP denied";
                case WLAN_HOSTED_NETWORK_REASON.wlan_hosted_network_reason_service_unavailable:
                    return Prefix + "service unavailable";
                case WLAN_HOSTED_NETWORK_REASON.wlan_hosted_network_reason_device_change:
                    return Prefix + "device change";
                case WLAN_HOSTED_NETWORK_REASON.wlan_hosted_network_reason_properties_change:
                    return Prefix + "properties change";
                case WLAN_HOSTED_NETWORK_REASON.wlan_hosted_network_reason_virtual_station_blocking_use:
                    return Prefix + "virtual station blocking used";
                case WLAN_HOSTED_NETWORK_REASON.wlan_hosted_network_reason_service_available_on_virtual_station:
                    return Prefix + "service available on virtual station";
                default:
                    return Prefix + "unknown";
            }
        }

        internal void StartHostedNetwork(string ssid, string key, Guid InterfaceGuid)
        {
            WLAN_HOSTED_NETWORK_REASON WlanHnkReason;
            WlanMgr.QueryConnectionSettings(out string SSID, out uint MaxNumberOfPeers);
            WlanMgr.SetConnectionSettings(ssid, MaxNumberOfPeers);
            WlanMgr.SetSecondaryKey(key, true, true);

            switch (WlanMgr.HostedNetworkState)
            {
                case WLAN_HOSTED_NETWORK_STATE.wlan_hosted_network_active:

                    try
                    {
                        IPhlpapi.FlushIpNetTable(WlanMgr.HostedNetworkGuid);
                        ICSRouter.StopSharing();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"{ex.Message}", Text,
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    finally
                    {
                        _ = WlanMgr.ForceStop();
                    }

                    break;
                case WLAN_HOSTED_NETWORK_STATE.wlan_hosted_network_unavailable:

                    if (!(Advapi32.GetPrivileges() && WlanMgr.AllowHostedNetwork(true)))
                    {
                        MessageBox.Show("The hosted network couldn't be started.",
                               Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    }
                    WlanHnkReason = WlanMgr.ForceStart();
                    if (WlanHnkReason != 0)
                    { MessageBox.Show(ShowHostedNetworkReason(WlanHnkReason), Text); break; }
                    while (WlanHnkReason == 0 && !WlanMgr.IsHostedNetworkActive)
                    {
                        Thread.Sleep(TimeSpan.FromSeconds(1));
                    }
                    try { ICSRouter.StartSharing(InterfaceGuid, WlanMgr.HostedNetworkGuid); }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"{ex.Message}", Text,
                              MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                    break;
                case WLAN_HOSTED_NETWORK_STATE.wlan_hosted_network_idle:

                    WlanHnkReason = WlanMgr.ForceStart();
                    if (WlanHnkReason != 0)
                    { MessageBox.Show(ShowHostedNetworkReason(WlanHnkReason), Text); break; }
                    while (WlanHnkReason == 0 && !WlanMgr.IsHostedNetworkActive)
                    {
                        Thread.Sleep(TimeSpan.FromSeconds(1));
                    }
                    try { ICSRouter.StartSharing(InterfaceGuid, WlanMgr.HostedNetworkGuid); }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"{ex.Message}", Text,
                              MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                    break;
            }
        }

        private readonly int Speed = 80;
        private bool ShowList = false;
        private void OnToogleListView(object sender, EventArgs e)
        {
            switch (ShowList)
            {
                case false:
                    ToggleListView.Text = "<";
                    for (int i = Width; i <= MaximumSize.Width; i += Speed)
                    {
                        int PosX = Location.X;
                        if ((Left + i) > Screen.PrimaryScreen.Bounds.Size.Width)
                        { Location = new System.Drawing.Point(PosX -= Speed, Location.Y); }
                        Width = i; Update();
                    }
                    ShowList = true;
                    break;
                case true:
                    ToggleListView.Text = ">";
                    for (int i = Width; i >= MinimumSize.Width; i -= Speed)
                    {
                        Width = i; Update();
                    }
                    ShowList = false;
                    break;
            }
        }

        private void OnMainFormClosing(object sender, FormClosingEventArgs e)
        {
            if (!(WlanMgr is null))
            {
                _ = IPhlpapi.FlushIpNetTable(WlanMgr.HostedNetworkGuid);
                ICSRouter.StopSharing();
                _ = WlanMgr.ForceStop();
            }
        }
    }
}


internal static class Program
{
    private static string AppGUID
    {
        get { return Assembly.GetExecutingAssembly().ManifestModule.ModuleVersionId.ToString(); }
    }
    [STAThread]
    private static void Main()
    {
        using (Mutex mutex = new Mutex(true, AppGUID, out bool IsNotRunning))
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(true);

            foreach (var item in Hotspot.ICSRouter.GetNetworkInterfaces())
            {
                System.Diagnostics.Debug.WriteLine(item.GatewayAddress.ToString());
            }

            if (IsNotRunning) { Application.Run(new Hotspot.Hotspot()); }
        }
    }
}
