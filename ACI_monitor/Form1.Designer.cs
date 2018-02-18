namespace ACI_monitor
{
    partial class Main
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
            this.components = new System.ComponentModel.Container();
            this.Interface_101_clock = new System.Windows.Forms.Timer(this.components);
            this.Shutdown_101_ETH12 = new System.Windows.Forms.Button();
            this.Shutdown_102_ETH12 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusbar = new System.Windows.Forms.StatusStrip();
            this.Status_Connect = new System.Windows.Forms.ToolStripStatusLabel();
            this.LogBox_101 = new System.Windows.Forms.TextBox();
            this.Log_Tick_101 = new System.Windows.Forms.Timer(this.components);
            this.LogBox_102 = new System.Windows.Forms.TextBox();
            this.Log_Tick_102 = new System.Windows.Forms.Timer(this.components);
            this.adminState_101_eth12_led = new System.Windows.Forms.Button();
            this.operState_101_eth12_led = new System.Windows.Forms.Button();
            this.adminState_102_eth12_led = new System.Windows.Forms.Button();
            this.operState_102_eth12_led = new System.Windows.Forms.Button();
            this.adminState_101_eth12_label = new System.Windows.Forms.Label();
            this.adminState_102_eth12_label = new System.Windows.Forms.Label();
            this.operState_101_eth12_label = new System.Windows.Forms.Label();
            this.operState_102_eth12_label = new System.Windows.Forms.Label();
            this.Enable_101_ETH12 = new System.Windows.Forms.Button();
            this.Enable_102_ETH12 = new System.Windows.Forms.Button();
            this.Status_Box = new System.Windows.Forms.GroupBox();
            this.Log_Box = new System.Windows.Forms.GroupBox();
            this.Command_Box = new System.Windows.Forms.GroupBox();
            this.aaaRefresh = new System.Windows.Forms.Timer(this.components);
            this.Interface_102_clock = new System.Windows.Forms.Timer(this.components);
            this.Int_Norun_101 = new System.Timers.Timer();
            this.Int_Norun_102 = new System.Timers.Timer();
            this.menuStrip1.SuspendLayout();
            this.statusbar.SuspendLayout();
            this.Status_Box.SuspendLayout();
            this.Log_Box.SuspendLayout();
            this.Command_Box.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Int_Norun_101)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Int_Norun_102)).BeginInit();
            this.SuspendLayout();
            // 
            // Interface_101_clock
            // 
            this.Interface_101_clock.Interval = 500;
            this.Interface_101_clock.Tick += new System.EventHandler(this.Interface_101_clock_Tick);
            // 
            // Shutdown_101_ETH12
            // 
            this.Shutdown_101_ETH12.Enabled = false;
            this.Shutdown_101_ETH12.Location = new System.Drawing.Point(6, 19);
            this.Shutdown_101_ETH12.Name = "Shutdown_101_ETH12";
            this.Shutdown_101_ETH12.Size = new System.Drawing.Size(355, 23);
            this.Shutdown_101_ETH12.TabIndex = 3;
            this.Shutdown_101_ETH12.TabStop = false;
            this.Shutdown_101_ETH12.Text = "Shutdown 101 Eth1/2";
            this.Shutdown_101_ETH12.UseVisualStyleBackColor = true;
            this.Shutdown_101_ETH12.Click += new System.EventHandler(this.Shutdown_101_ETH12_Click);
            // 
            // Shutdown_102_ETH12
            // 
            this.Shutdown_102_ETH12.Enabled = false;
            this.Shutdown_102_ETH12.Location = new System.Drawing.Point(6, 87);
            this.Shutdown_102_ETH12.Name = "Shutdown_102_ETH12";
            this.Shutdown_102_ETH12.Size = new System.Drawing.Size(355, 23);
            this.Shutdown_102_ETH12.TabIndex = 4;
            this.Shutdown_102_ETH12.TabStop = false;
            this.Shutdown_102_ETH12.Text = "Shutdown 102 Eth1/2";
            this.Shutdown_102_ETH12.UseVisualStyleBackColor = true;
            this.Shutdown_102_ETH12.Click += new System.EventHandler(this.Shutdown_102_ETH12_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(744, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loginToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loginToolStripMenuItem
            // 
            this.loginToolStripMenuItem.Name = "loginToolStripMenuItem";
            this.loginToolStripMenuItem.Size = new System.Drawing.Size(104, 22);
            this.loginToolStripMenuItem.Text = "Login";
            this.loginToolStripMenuItem.Click += new System.EventHandler(this.LoginToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(104, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // statusbar
            // 
            this.statusbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Status_Connect});
            this.statusbar.Location = new System.Drawing.Point(0, 289);
            this.statusbar.Name = "statusbar";
            this.statusbar.Size = new System.Drawing.Size(744, 22);
            this.statusbar.SizingGrip = false;
            this.statusbar.Stretch = false;
            this.statusbar.TabIndex = 6;
            this.statusbar.Text = "statusStrip1";
            // 
            // Status_Connect
            // 
            this.Status_Connect.Name = "Status_Connect";
            this.Status_Connect.Size = new System.Drawing.Size(39, 17);
            this.Status_Connect.Text = "Status";
            // 
            // LogBox_101
            // 
            this.LogBox_101.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.LogBox_101.Location = new System.Drawing.Point(11, 25);
            this.LogBox_101.MaxLength = 327670000;
            this.LogBox_101.Multiline = true;
            this.LogBox_101.Name = "LogBox_101";
            this.LogBox_101.ReadOnly = true;
            this.LogBox_101.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.LogBox_101.Size = new System.Drawing.Size(319, 106);
            this.LogBox_101.TabIndex = 7;
            this.LogBox_101.TabStop = false;
            this.LogBox_101.TextChanged += new System.EventHandler(this.LogBox_101_TextChanged);
            // 
            // Log_Tick_101
            // 
            this.Log_Tick_101.Interval = 1000;
            this.Log_Tick_101.Tick += new System.EventHandler(this.Log_101_Tick);
            // 
            // LogBox_102
            // 
            this.LogBox_102.Cursor = System.Windows.Forms.Cursors.Default;
            this.LogBox_102.Location = new System.Drawing.Point(11, 137);
            this.LogBox_102.MaxLength = 327670000;
            this.LogBox_102.Multiline = true;
            this.LogBox_102.Name = "LogBox_102";
            this.LogBox_102.ReadOnly = true;
            this.LogBox_102.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.LogBox_102.Size = new System.Drawing.Size(319, 106);
            this.LogBox_102.TabIndex = 8;
            this.LogBox_102.TabStop = false;
            this.LogBox_102.TextChanged += new System.EventHandler(this.LogBox_102_TextChanged);
            // 
            // Log_Tick_102
            // 
            this.Log_Tick_102.Interval = 1000;
            this.Log_Tick_102.Tick += new System.EventHandler(this.Log_102_Tick);
            // 
            // adminState_101_eth12_led
            // 
            this.adminState_101_eth12_led.BackColor = System.Drawing.Color.Yellow;
            this.adminState_101_eth12_led.Enabled = false;
            this.adminState_101_eth12_led.Location = new System.Drawing.Point(15, 22);
            this.adminState_101_eth12_led.Name = "adminState_101_eth12_led";
            this.adminState_101_eth12_led.Size = new System.Drawing.Size(25, 25);
            this.adminState_101_eth12_led.TabIndex = 9;
            this.adminState_101_eth12_led.UseVisualStyleBackColor = false;
            // 
            // operState_101_eth12_led
            // 
            this.operState_101_eth12_led.BackColor = System.Drawing.Color.Yellow;
            this.operState_101_eth12_led.Enabled = false;
            this.operState_101_eth12_led.Location = new System.Drawing.Point(15, 50);
            this.operState_101_eth12_led.Name = "operState_101_eth12_led";
            this.operState_101_eth12_led.Size = new System.Drawing.Size(25, 25);
            this.operState_101_eth12_led.TabIndex = 10;
            this.operState_101_eth12_led.UseVisualStyleBackColor = false;
            // 
            // adminState_102_eth12_led
            // 
            this.adminState_102_eth12_led.BackColor = System.Drawing.Color.Yellow;
            this.adminState_102_eth12_led.Enabled = false;
            this.adminState_102_eth12_led.Location = new System.Drawing.Point(203, 22);
            this.adminState_102_eth12_led.Name = "adminState_102_eth12_led";
            this.adminState_102_eth12_led.Size = new System.Drawing.Size(25, 25);
            this.adminState_102_eth12_led.TabIndex = 11;
            this.adminState_102_eth12_led.UseVisualStyleBackColor = false;
            // 
            // operState_102_eth12_led
            // 
            this.operState_102_eth12_led.BackColor = System.Drawing.Color.Yellow;
            this.operState_102_eth12_led.Enabled = false;
            this.operState_102_eth12_led.Location = new System.Drawing.Point(203, 50);
            this.operState_102_eth12_led.Name = "operState_102_eth12_led";
            this.operState_102_eth12_led.Size = new System.Drawing.Size(25, 25);
            this.operState_102_eth12_led.TabIndex = 12;
            this.operState_102_eth12_led.UseVisualStyleBackColor = false;
            // 
            // adminState_101_eth12_label
            // 
            this.adminState_101_eth12_label.AutoSize = true;
            this.adminState_101_eth12_label.Location = new System.Drawing.Point(46, 28);
            this.adminState_101_eth12_label.Name = "adminState_101_eth12_label";
            this.adminState_101_eth12_label.Size = new System.Drawing.Size(118, 13);
            this.adminState_101_eth12_label.TabIndex = 13;
            this.adminState_101_eth12_label.Text = "AdminState 101 Eth1/2";
            // 
            // adminState_102_eth12_label
            // 
            this.adminState_102_eth12_label.AutoSize = true;
            this.adminState_102_eth12_label.Location = new System.Drawing.Point(234, 28);
            this.adminState_102_eth12_label.Name = "adminState_102_eth12_label";
            this.adminState_102_eth12_label.Size = new System.Drawing.Size(118, 13);
            this.adminState_102_eth12_label.TabIndex = 14;
            this.adminState_102_eth12_label.Text = "AdminState 102 Eth1/2";
            // 
            // operState_101_eth12_label
            // 
            this.operState_101_eth12_label.AutoSize = true;
            this.operState_101_eth12_label.Location = new System.Drawing.Point(46, 56);
            this.operState_101_eth12_label.Name = "operState_101_eth12_label";
            this.operState_101_eth12_label.Size = new System.Drawing.Size(112, 13);
            this.operState_101_eth12_label.TabIndex = 15;
            this.operState_101_eth12_label.Text = "OperState 101 Eth1/2";
            // 
            // operState_102_eth12_label
            // 
            this.operState_102_eth12_label.AutoSize = true;
            this.operState_102_eth12_label.Location = new System.Drawing.Point(234, 56);
            this.operState_102_eth12_label.Name = "operState_102_eth12_label";
            this.operState_102_eth12_label.Size = new System.Drawing.Size(112, 13);
            this.operState_102_eth12_label.TabIndex = 16;
            this.operState_102_eth12_label.Text = "OperState 101 Eth1/2";
            // 
            // Enable_101_ETH12
            // 
            this.Enable_101_ETH12.Enabled = false;
            this.Enable_101_ETH12.Location = new System.Drawing.Point(6, 48);
            this.Enable_101_ETH12.Name = "Enable_101_ETH12";
            this.Enable_101_ETH12.Size = new System.Drawing.Size(355, 23);
            this.Enable_101_ETH12.TabIndex = 17;
            this.Enable_101_ETH12.TabStop = false;
            this.Enable_101_ETH12.Text = "Enable 101 Eth1/2";
            this.Enable_101_ETH12.UseVisualStyleBackColor = true;
            this.Enable_101_ETH12.Click += new System.EventHandler(this.Enable_101_ETH12_Click);
            // 
            // Enable_102_ETH12
            // 
            this.Enable_102_ETH12.Enabled = false;
            this.Enable_102_ETH12.Location = new System.Drawing.Point(6, 116);
            this.Enable_102_ETH12.Name = "Enable_102_ETH12";
            this.Enable_102_ETH12.Size = new System.Drawing.Size(355, 23);
            this.Enable_102_ETH12.TabIndex = 18;
            this.Enable_102_ETH12.TabStop = false;
            this.Enable_102_ETH12.Text = "Enable 102 ETH1/2";
            this.Enable_102_ETH12.UseVisualStyleBackColor = true;
            this.Enable_102_ETH12.Click += new System.EventHandler(this.Enable_102_ETH12_Click);
            // 
            // Status_Box
            // 
            this.Status_Box.Controls.Add(this.operState_102_eth12_label);
            this.Status_Box.Controls.Add(this.adminState_101_eth12_led);
            this.Status_Box.Controls.Add(this.operState_101_eth12_led);
            this.Status_Box.Controls.Add(this.adminState_102_eth12_led);
            this.Status_Box.Controls.Add(this.operState_101_eth12_label);
            this.Status_Box.Controls.Add(this.operState_102_eth12_led);
            this.Status_Box.Controls.Add(this.adminState_102_eth12_label);
            this.Status_Box.Controls.Add(this.adminState_101_eth12_label);
            this.Status_Box.Location = new System.Drawing.Point(12, 27);
            this.Status_Box.Name = "Status_Box";
            this.Status_Box.Size = new System.Drawing.Size(367, 86);
            this.Status_Box.TabIndex = 19;
            this.Status_Box.TabStop = false;
            this.Status_Box.Text = "Status";
            // 
            // Log_Box
            // 
            this.Log_Box.Controls.Add(this.LogBox_101);
            this.Log_Box.Controls.Add(this.LogBox_102);
            this.Log_Box.Location = new System.Drawing.Point(385, 27);
            this.Log_Box.Name = "Log_Box";
            this.Log_Box.Size = new System.Drawing.Size(345, 253);
            this.Log_Box.TabIndex = 20;
            this.Log_Box.TabStop = false;
            this.Log_Box.Text = "Log Box";
            // 
            // Command_Box
            // 
            this.Command_Box.Controls.Add(this.Shutdown_101_ETH12);
            this.Command_Box.Controls.Add(this.Shutdown_102_ETH12);
            this.Command_Box.Controls.Add(this.Enable_101_ETH12);
            this.Command_Box.Controls.Add(this.Enable_102_ETH12);
            this.Command_Box.Location = new System.Drawing.Point(12, 119);
            this.Command_Box.Name = "Command_Box";
            this.Command_Box.Size = new System.Drawing.Size(367, 161);
            this.Command_Box.TabIndex = 21;
            this.Command_Box.TabStop = false;
            this.Command_Box.Text = "Command Box";
            // 
            // aaaRefresh
            // 
            this.aaaRefresh.Interval = 540000;
            this.aaaRefresh.Tick += new System.EventHandler(this.AaaRefresh_Tick);
            // 
            // Interface_102_clock
            // 
            this.Interface_102_clock.Interval = 500;
            this.Interface_102_clock.Tick += new System.EventHandler(this.Interface_102_clock_Tick);
            // 
            // Int_Norun_101
            // 
            this.Int_Norun_101.Interval = 25000D;
            this.Int_Norun_101.SynchronizingObject = this;
            this.Int_Norun_101.Elapsed += new System.Timers.ElapsedEventHandler(this.Int_Norun_101_Elapsed);
            // 
            // Int_Norun_102
            // 
            this.Int_Norun_102.Interval = 25000D;
            this.Int_Norun_102.SynchronizingObject = this;
            this.Int_Norun_102.Elapsed += new System.Timers.ElapsedEventHandler(this.Int_Norun_102_Elapsed);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(744, 311);
            this.Controls.Add(this.Command_Box);
            this.Controls.Add(this.Log_Box);
            this.Controls.Add(this.Status_Box);
            this.Controls.Add(this.statusbar);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Main";
            this.Text = "ACI Monitor@LAB";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusbar.ResumeLayout(false);
            this.statusbar.PerformLayout();
            this.Status_Box.ResumeLayout(false);
            this.Status_Box.PerformLayout();
            this.Log_Box.ResumeLayout(false);
            this.Log_Box.PerformLayout();
            this.Command_Box.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Int_Norun_101)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Int_Norun_102)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer Interface_101_clock;
        private System.Windows.Forms.Button Shutdown_101_ETH12;
        private System.Windows.Forms.Button Shutdown_102_ETH12;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusbar;
        private System.Windows.Forms.ToolStripStatusLabel Status_Connect;
        private System.Windows.Forms.TextBox LogBox_101;
        private System.Windows.Forms.Timer Log_Tick_101;
        private System.Windows.Forms.TextBox LogBox_102;
        private System.Windows.Forms.Timer Log_Tick_102;
        private System.Windows.Forms.ToolStripMenuItem loginToolStripMenuItem;
        private System.Windows.Forms.Button adminState_101_eth12_led;
        private System.Windows.Forms.Button operState_101_eth12_led;
        private System.Windows.Forms.Button adminState_102_eth12_led;
        private System.Windows.Forms.Button operState_102_eth12_led;
        private System.Windows.Forms.Label adminState_101_eth12_label;
        private System.Windows.Forms.Label adminState_102_eth12_label;
        private System.Windows.Forms.Label operState_101_eth12_label;
        private System.Windows.Forms.Label operState_102_eth12_label;
        private System.Windows.Forms.Button Enable_101_ETH12;
        private System.Windows.Forms.Button Enable_102_ETH12;
        private System.Windows.Forms.GroupBox Status_Box;
        private System.Windows.Forms.GroupBox Log_Box;
        private System.Windows.Forms.GroupBox Command_Box;
        private System.Windows.Forms.Timer aaaRefresh;
        private System.Windows.Forms.Timer Interface_102_clock;
        private System.Timers.Timer Int_Norun_101;
        private System.Timers.Timer Int_Norun_102;
    }
}

