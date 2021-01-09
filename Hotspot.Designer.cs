namespace Hotspot
{
    partial class Hotspot
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Hotspot));
            this.SSIDLabel = new System.Windows.Forms.Label();
            this.KeyLabel = new System.Windows.Forms.Label();
            this.ShowKey = new System.Windows.Forms.CheckBox();
            this.ShareButton = new System.Windows.Forms.Button();
            this.ExitButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Logo = new System.Windows.Forms.Panel();
            this.ListPanel = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.NetIList = new System.Windows.Forms.ComboBox();
            this.StatusBox = new System.Windows.Forms.Panel();
            this.StatusLabel = new System.Windows.Forms.Label();
            this.ActivityLabel = new System.Windows.Forms.Label();
            this.ToggleListView = new System.Windows.Forms.Button();
            this.IPvXBox = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.SSIDText = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.KeyText = new System.Windows.Forms.TextBox();
            this.StatusBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // SSIDLabel
            // 
            this.SSIDLabel.AutoSize = true;
            this.SSIDLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SSIDLabel.Location = new System.Drawing.Point(101, 93);
            this.SSIDLabel.Name = "SSIDLabel";
            this.SSIDLabel.Size = new System.Drawing.Size(52, 20);
            this.SSIDLabel.TabIndex = 2;
            this.SSIDLabel.Text = "SSID";
            // 
            // KeyLabel
            // 
            this.KeyLabel.AutoSize = true;
            this.KeyLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KeyLabel.Location = new System.Drawing.Point(84, 148);
            this.KeyLabel.Name = "KeyLabel";
            this.KeyLabel.Size = new System.Drawing.Size(86, 20);
            this.KeyLabel.TabIndex = 3;
            this.KeyLabel.Text = "Password";
            // 
            // ShowKey
            // 
            this.ShowKey.AutoSize = true;
            this.ShowKey.Location = new System.Drawing.Point(75, 207);
            this.ShowKey.Name = "ShowKey";
            this.ShowKey.Size = new System.Drawing.Size(106, 17);
            this.ShowKey.TabIndex = 4;
            this.ShowKey.Text = "Show characters";
            this.ShowKey.UseVisualStyleBackColor = true;
            this.ShowKey.CheckedChanged += new System.EventHandler(this.ShowKey_CheckedChanged);
            // 
            // ShareButton
            // 
            this.ShareButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ShareButton.Location = new System.Drawing.Point(18, 314);
            this.ShareButton.Name = "ShareButton";
            this.ShareButton.Size = new System.Drawing.Size(99, 45);
            this.ShareButton.TabIndex = 5;
            this.ShareButton.Text = "Share";
            this.ShareButton.UseVisualStyleBackColor = true;
            // 
            // ExitButton
            // 
            this.ExitButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExitButton.Location = new System.Drawing.Point(140, 314);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(99, 45);
            this.ExitButton.TabIndex = 6;
            this.ExitButton.Text = "Exit";
            this.ExitButton.UseVisualStyleBackColor = true;
            this.ExitButton.Click += new System.EventHandler(this.OnExitButtonClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(64, 232);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 17);
            this.label1.TabIndex = 11;
            this.label1.Text = "Internet to share";
            // 
            // Logo
            // 
            this.Logo.BackgroundImage = global::Hotspot.Properties.Resources.WifiLogo;
            this.Logo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Logo.Location = new System.Drawing.Point(71, -2);
            this.Logo.Name = "Logo";
            this.Logo.Size = new System.Drawing.Size(115, 93);
            this.Logo.TabIndex = 12;
            // 
            // ListPanel
            // 
            this.ListPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListPanel.Location = new System.Drawing.Point(255, 33);
            this.ListPanel.Margin = new System.Windows.Forms.Padding(0);
            this.ListPanel.Name = "ListPanel";
            this.ListPanel.Padding = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.ListPanel.Size = new System.Drawing.Size(0, 334);
            this.ListPanel.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(253, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(164, 20);
            this.label2.TabIndex = 14;
            this.label2.Text = "Connected Devices";
            // 
            // NetIList
            // 
            this.NetIList.DropDownHeight = 130;
            this.NetIList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.NetIList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NetIList.FormattingEnabled = true;
            this.NetIList.IntegralHeight = false;
            this.NetIList.Location = new System.Drawing.Point(18, 256);
            this.NetIList.Name = "NetIList";
            this.NetIList.Size = new System.Drawing.Size(221, 21);
            this.NetIList.TabIndex = 15;
            // 
            // StatusBox
            // 
            this.StatusBox.BackColor = System.Drawing.SystemColors.HotTrack;
            this.StatusBox.Controls.Add(this.StatusLabel);
            this.StatusBox.Controls.Add(this.ActivityLabel);
            this.StatusBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.StatusBox.ForeColor = System.Drawing.SystemColors.Control;
            this.StatusBox.Location = new System.Drawing.Point(0, 373);
            this.StatusBox.Margin = new System.Windows.Forms.Padding(0);
            this.StatusBox.Name = "StatusBox";
            this.StatusBox.Padding = new System.Windows.Forms.Padding(4);
            this.StatusBox.Size = new System.Drawing.Size(256, 20);
            this.StatusBox.TabIndex = 16;
            // 
            // StatusLabel
            // 
            this.StatusLabel.AutoSize = true;
            this.StatusLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.StatusLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StatusLabel.Location = new System.Drawing.Point(4, 4);
            this.StatusLabel.Margin = new System.Windows.Forms.Padding(0);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(93, 14);
            this.StatusLabel.TabIndex = 10;
            this.StatusLabel.Text = "Status:  Unknown";
            this.StatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ActivityLabel
            // 
            this.ActivityLabel.AutoSize = true;
            this.ActivityLabel.Dock = System.Windows.Forms.DockStyle.Right;
            this.ActivityLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ActivityLabel.Location = new System.Drawing.Point(132, 4);
            this.ActivityLabel.Name = "ActivityLabel";
            this.ActivityLabel.Size = new System.Drawing.Size(120, 13);
            this.ActivityLabel.TabIndex = 1;
            this.ActivityLabel.Text = "RX: 0 bytes TX: 0 Bytes";
            this.ActivityLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ToggleListView
            // 
            this.ToggleListView.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ToggleListView.Location = new System.Drawing.Point(239, 152);
            this.ToggleListView.Name = "ToggleListView";
            this.ToggleListView.Size = new System.Drawing.Size(16, 40);
            this.ToggleListView.TabIndex = 17;
            this.ToggleListView.Text = ">";
            this.ToggleListView.UseVisualStyleBackColor = true;
            this.ToggleListView.Click += new System.EventHandler(this.OnToogleListView);
            // 
            // IPvXBox
            // 
            this.IPvXBox.Location = new System.Drawing.Point(34, 282);
            this.IPvXBox.Name = "IPvXBox";
            this.IPvXBox.Size = new System.Drawing.Size(189, 27);
            this.IPvXBox.TabIndex = 20;
            this.IPvXBox.Text = "No NIC selected";
            this.IPvXBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.SSIDText);
            this.groupBox1.Location = new System.Drawing.Point(46, 113);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(164, 35);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            // 
            // SSIDText
            // 
            this.SSIDText.BackColor = System.Drawing.SystemColors.Control;
            this.SSIDText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SSIDText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SSIDText.Location = new System.Drawing.Point(4, 9);
            this.SSIDText.Margin = new System.Windows.Forms.Padding(0);
            this.SSIDText.MaxLength = 16;
            this.SSIDText.Multiline = true;
            this.SSIDText.Name = "SSIDText";
            this.SSIDText.Size = new System.Drawing.Size(156, 20);
            this.SSIDText.TabIndex = 28;
            this.SSIDText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.SSIDText.WordWrap = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.KeyText);
            this.groupBox2.Location = new System.Drawing.Point(46, 166);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(164, 35);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            // 
            // KeyText
            // 
            this.KeyText.BackColor = System.Drawing.SystemColors.Control;
            this.KeyText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.KeyText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KeyText.Location = new System.Drawing.Point(4, 10);
            this.KeyText.Margin = new System.Windows.Forms.Padding(0);
            this.KeyText.MaxLength = 16;
            this.KeyText.Name = "KeyText";
            this.KeyText.Size = new System.Drawing.Size(156, 19);
            this.KeyText.TabIndex = 20;
            this.KeyText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.KeyText.UseSystemPasswordChar = true;
            this.KeyText.WordWrap = false;
            // 
            // Hotspot
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(256, 393);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.IPvXBox);
            this.Controls.Add(this.ToggleListView);
            this.Controls.Add(this.StatusBox);
            this.Controls.Add(this.NetIList);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ListPanel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.KeyLabel);
            this.Controls.Add(this.ExitButton);
            this.Controls.Add(this.ShareButton);
            this.Controls.Add(this.ShowKey);
            this.Controls.Add(this.SSIDLabel);
            this.Controls.Add(this.Logo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1000, 432);
            this.MinimumSize = new System.Drawing.Size(272, 432);
            this.Name = "Hotspot";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "vWi-Fi Hotspot";
            this.StatusBox.ResumeLayout(false);
            this.StatusBox.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label SSIDLabel;
        private System.Windows.Forms.Label KeyLabel;
        private System.Windows.Forms.CheckBox ShowKey;
        private System.Windows.Forms.Button ExitButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel Logo;
        private System.Windows.Forms.Panel ListPanel;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Button ShareButton;
        internal System.Windows.Forms.ComboBox NetIList;
        private System.Windows.Forms.Panel StatusBox;
        private System.Windows.Forms.Label ActivityLabel;
        private System.Windows.Forms.Button ToggleListView;
        public System.Windows.Forms.Label StatusLabel;
        private System.Windows.Forms.Label IPvXBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox SSIDText;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox KeyText;
    }
}

