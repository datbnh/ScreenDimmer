namespace Augustine.ScreenDimmer
{
    internal partial class ScreenDimmer
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
            this.toolTipHint = new System.Windows.Forms.ToolTip(this.components);
            this.buttonDim = new System.Windows.Forms.Button();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.colorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonBright = new System.Windows.Forms.Button();
            this.labelScreen = new System.Windows.Forms.Label();
            this.labelBrightness = new System.Windows.Forms.Label();
            this.labelExpandCollapse = new System.Windows.Forms.Label();
            this.labelHelp = new System.Windows.Forms.Label();
            this.labelBug = new System.Windows.Forms.Label();
            this.labelAbout = new System.Windows.Forms.Label();
            this.checkBoxZeroBrightness = new System.Windows.Forms.CheckBox();
            this.comboBoxScreens = new System.Windows.Forms.ComboBox();
            this.checkBoxDebug = new System.Windows.Forms.CheckBox();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.controlPanelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timerEnforceOnTop = new System.Windows.Forms.Timer(this.components);
            this.timerFade = new System.Windows.Forms.Timer(this.components);
            this.trackBarBrightness = new System.Windows.Forms.TrackBar();
            this.checkBoxEnforceOnTop = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.toolTipWarning = new System.Windows.Forms.ToolTip(this.components);
            this.contextMenuStrip2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBrightness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolTipHint
            // 
            this.toolTipHint.ShowAlways = true;
            // 
            // buttonDim
            // 
            this.buttonDim.BackColor = System.Drawing.Color.Black;
            this.buttonDim.ContextMenuStrip = this.contextMenuStrip2;
            this.buttonDim.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonDim.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.buttonDim.FlatAppearance.BorderSize = 0;
            this.buttonDim.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDim.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDim.ForeColor = System.Drawing.Color.Gray;
            this.buttonDim.Location = new System.Drawing.Point(12, 71);
            this.buttonDim.Name = "buttonDim";
            this.buttonDim.Size = new System.Drawing.Size(77, 26);
            this.buttonDim.TabIndex = 46;
            this.buttonDim.Text = "Dim ";
            this.toolTipHint.SetToolTip(this.buttonDim, "[Enter]\r\nRight click to change color.");
            this.buttonDim.UseVisualStyleBackColor = false;
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.colorToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(113, 26);
            // 
            // colorToolStripMenuItem
            // 
            this.colorToolStripMenuItem.Name = "colorToolStripMenuItem";
            this.colorToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.colorToolStripMenuItem.Text = "Color...";
            this.colorToolStripMenuItem.Click += new System.EventHandler(this.colorToolStripMenuItem_Click);
            // 
            // buttonBright
            // 
            this.buttonBright.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonBright.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.buttonBright.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonBright.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonBright.ForeColor = System.Drawing.Color.Gray;
            this.buttonBright.Location = new System.Drawing.Point(95, 71);
            this.buttonBright.Name = "buttonBright";
            this.buttonBright.Size = new System.Drawing.Size(77, 26);
            this.buttonBright.TabIndex = 45;
            this.buttonBright.Text = "Bright ";
            this.toolTipHint.SetToolTip(this.buttonBright, "[ESC]");
            this.buttonBright.UseVisualStyleBackColor = true;
            // 
            // labelScreen
            // 
            this.labelScreen.AutoSize = true;
            this.labelScreen.Location = new System.Drawing.Point(12, 47);
            this.labelScreen.Name = "labelScreen";
            this.labelScreen.Size = new System.Drawing.Size(18, 13);
            this.labelScreen.TabIndex = 44;
            this.labelScreen.Text = "📺";
            this.labelScreen.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTipHint.SetToolTip(this.labelScreen, "Screen");
            // 
            // labelBrightness
            // 
            this.labelBrightness.AutoSize = true;
            this.labelBrightness.Location = new System.Drawing.Point(12, 15);
            this.labelBrightness.Name = "labelBrightness";
            this.labelBrightness.Size = new System.Drawing.Size(16, 13);
            this.labelBrightness.TabIndex = 1;
            this.labelBrightness.Text = "☀";
            this.labelBrightness.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTipHint.SetToolTip(this.labelBrightness, "Brightness");
            // 
            // labelExpandCollapse
            // 
            this.labelExpandCollapse.AutoSize = true;
            this.labelExpandCollapse.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelExpandCollapse.Location = new System.Drawing.Point(156, 103);
            this.labelExpandCollapse.Margin = new System.Windows.Forms.Padding(0);
            this.labelExpandCollapse.Name = "labelExpandCollapse";
            this.labelExpandCollapse.Size = new System.Drawing.Size(16, 13);
            this.labelExpandCollapse.TabIndex = 54;
            this.labelExpandCollapse.Text = "▷";
            this.toolTipHint.SetToolTip(this.labelExpandCollapse, "More...");
            this.labelExpandCollapse.Click += new System.EventHandler(this.labelExpandCollapse_Click);
            // 
            // labelHelp
            // 
            this.labelHelp.AutoSize = true;
            this.labelHelp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelHelp.Location = new System.Drawing.Point(138, 103);
            this.labelHelp.Margin = new System.Windows.Forms.Padding(0);
            this.labelHelp.Name = "labelHelp";
            this.labelHelp.Size = new System.Drawing.Size(18, 13);
            this.labelHelp.TabIndex = 55;
            this.labelHelp.Text = "❔";
            this.toolTipHint.SetToolTip(this.labelHelp, "Hotkeys...");
            this.labelHelp.Click += new System.EventHandler(this.labelHelp_Click);
            // 
            // labelBug
            // 
            this.labelBug.AutoSize = true;
            this.labelBug.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelBug.ForeColor = System.Drawing.Color.Maroon;
            this.labelBug.Location = new System.Drawing.Point(304, 103);
            this.labelBug.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.labelBug.Name = "labelBug";
            this.labelBug.Size = new System.Drawing.Size(18, 13);
            this.labelBug.TabIndex = 56;
            this.labelBug.Text = "🐞";
            this.toolTipHint.SetToolTip(this.labelBug, "View...");
            this.labelBug.Visible = false;
            this.labelBug.Click += new System.EventHandler(this.labelBug_Click);
            // 
            // labelAbout
            // 
            this.labelAbout.AutoSize = true;
            this.labelAbout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelAbout.Location = new System.Drawing.Point(9, 103);
            this.labelAbout.Margin = new System.Windows.Forms.Padding(0);
            this.labelAbout.Name = "labelAbout";
            this.labelAbout.Size = new System.Drawing.Size(16, 13);
            this.labelAbout.TabIndex = 42;
            this.labelAbout.Text = "ⓘ";
            this.labelAbout.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTipHint.SetToolTip(this.labelAbout, "About...");
            this.labelAbout.Click += new System.EventHandler(this.labelAbout_Click);
            // 
            // checkBoxZeroBrightness
            // 
            this.checkBoxZeroBrightness.AutoSize = true;
            this.checkBoxZeroBrightness.Location = new System.Drawing.Point(197, 14);
            this.checkBoxZeroBrightness.Name = "checkBoxZeroBrightness";
            this.checkBoxZeroBrightness.Size = new System.Drawing.Size(139, 17);
            this.checkBoxZeroBrightness.TabIndex = 47;
            this.checkBoxZeroBrightness.Text = "Allow Zero Brightness";
            this.toolTipWarning.SetToolTip(this.checkBoxZeroBrightness, "When setting brightness to zero, the screen will be completely dimmed with select" +
        "ed color!");
            this.checkBoxZeroBrightness.UseVisualStyleBackColor = true;
            this.checkBoxZeroBrightness.CheckedChanged += new System.EventHandler(this.checkBoxZeroBrightness_CheckedChanged);
            // 
            // comboBoxScreens
            // 
            this.comboBoxScreens.BackColor = System.Drawing.SystemColors.Menu;
            this.comboBoxScreens.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxScreens.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxScreens.Location = new System.Drawing.Point(33, 44);
            this.comboBoxScreens.Name = "comboBoxScreens";
            this.comboBoxScreens.Size = new System.Drawing.Size(139, 21);
            this.comboBoxScreens.TabIndex = 43;
            this.comboBoxScreens.DropDown += new System.EventHandler(this.comboBoxScreens_DropDown);
            this.comboBoxScreens.SelectedIndexChanged += new System.EventHandler(this.comboBoxScreens_SelectedIndexChanged);
            // 
            // checkBoxDebug
            // 
            this.checkBoxDebug.AutoSize = true;
            this.checkBoxDebug.Location = new System.Drawing.Point(197, 102);
            this.checkBoxDebug.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.checkBoxDebug.Name = "checkBoxDebug";
            this.checkBoxDebug.Size = new System.Drawing.Size(107, 17);
            this.checkBoxDebug.TabIndex = 41;
            this.checkBoxDebug.Text = "Log Debug Info";
            this.checkBoxDebug.UseVisualStyleBackColor = true;
            this.checkBoxDebug.Visible = false;
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon1.BalloonTipText = "Screen Dimmer is Running Here!";
            this.notifyIcon1.BalloonTipTitle = "Screen Dimmer";
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Text = "Screen Dimmer";
            this.notifyIcon1.Visible = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.controlPanelToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(147, 54);
            // 
            // controlPanelToolStripMenuItem
            // 
            this.controlPanelToolStripMenuItem.Name = "controlPanelToolStripMenuItem";
            this.controlPanelToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.controlPanelToolStripMenuItem.Text = "Control Panel";
            this.controlPanelToolStripMenuItem.Click += new System.EventHandler(this.controlPanelToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(143, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // timerEnforceOnTop
            // 
            this.timerEnforceOnTop.Interval = 30000;
            this.timerEnforceOnTop.Tick += new System.EventHandler(this.timerEnforceOnTop_Tick);
            // 
            // timerFade
            // 
            this.timerFade.Interval = 1;
            // 
            // trackBarBrightness
            // 
            this.trackBarBrightness.AutoSize = false;
            this.trackBarBrightness.LargeChange = 20;
            this.trackBarBrightness.Location = new System.Drawing.Point(34, 12);
            this.trackBarBrightness.Maximum = 100;
            this.trackBarBrightness.Minimum = 20;
            this.trackBarBrightness.Name = "trackBarBrightness";
            this.trackBarBrightness.Size = new System.Drawing.Size(138, 26);
            this.trackBarBrightness.SmallChange = 10;
            this.trackBarBrightness.TabIndex = 30;
            this.trackBarBrightness.TickFrequency = 10;
            this.trackBarBrightness.Value = 45;
            this.trackBarBrightness.ValueChanged += new System.EventHandler(this.trackBarBrightness_ValueChanged);
            // 
            // checkBoxEnforceOnTop
            // 
            this.checkBoxEnforceOnTop.AutoSize = true;
            this.checkBoxEnforceOnTop.Location = new System.Drawing.Point(197, 37);
            this.checkBoxEnforceOnTop.Name = "checkBoxEnforceOnTop";
            this.checkBoxEnforceOnTop.Size = new System.Drawing.Size(149, 17);
            this.checkBoxEnforceOnTop.TabIndex = 49;
            this.checkBoxEnforceOnTop.Text = "Force on Top of Taskbar";
            this.checkBoxEnforceOnTop.UseVisualStyleBackColor = true;
            this.checkBoxEnforceOnTop.CheckedChanged += new System.EventHandler(this.checkBoxEnforceOnTop_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Enabled = false;
            this.label1.Location = new System.Drawing.Point(194, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 13);
            this.label1.TabIndex = 51;
            this.label1.Text = "Enforcing Period [seconds]:";
            // 
            // colorDialog1
            // 
            this.colorDialog1.FullOpen = true;
            this.colorDialog1.SolidColorOnly = true;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Enabled = false;
            this.numericUpDown1.Location = new System.Drawing.Point(347, 55);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            1800,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(38, 22);
            this.numericUpDown1.TabIndex = 53;
            this.numericUpDown1.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // toolTipWarning
            // 
            this.toolTipWarning.ForeColor = System.Drawing.Color.Maroon;
            this.toolTipWarning.ShowAlways = true;
            this.toolTipWarning.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Warning;
            this.toolTipWarning.ToolTipTitle = "Caution";
            // 
            // ScreenDimmer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(399, 129);
            this.Controls.Add(this.labelBug);
            this.Controls.Add(this.labelHelp);
            this.Controls.Add(this.comboBoxScreens);
            this.Controls.Add(this.trackBarBrightness);
            this.Controls.Add(this.labelScreen);
            this.Controls.Add(this.labelBrightness);
            this.Controls.Add(this.labelExpandCollapse);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBoxEnforceOnTop);
            this.Controls.Add(this.checkBoxZeroBrightness);
            this.Controls.Add(this.buttonDim);
            this.Controls.Add(this.buttonBright);
            this.Controls.Add(this.labelAbout);
            this.Controls.Add(this.checkBoxDebug);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ScreenDimmer";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.screenDimmer_FormClosing);
            this.contextMenuStrip2.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBrightness)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTipHint;
        private System.Windows.Forms.CheckBox checkBoxZeroBrightness;
        private System.Windows.Forms.Button buttonDim;
        private System.Windows.Forms.Button buttonBright;
        private System.Windows.Forms.Label labelScreen;
        private System.Windows.Forms.ComboBox comboBoxScreens;
        private System.Windows.Forms.Label labelAbout;
        private System.Windows.Forms.CheckBox checkBoxDebug;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem controlPanelToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Timer timerEnforceOnTop;
        private System.Windows.Forms.Label labelBrightness;
        private System.Windows.Forms.Timer timerFade;
        private System.Windows.Forms.TrackBar trackBarBrightness;
        private System.Windows.Forms.CheckBox checkBoxEnforceOnTop;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label labelExpandCollapse;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem colorToolStripMenuItem;
        private System.Windows.Forms.Label labelHelp;
        private System.Windows.Forms.ToolTip toolTipWarning;
        private System.Windows.Forms.Label labelBug;
    }
}

