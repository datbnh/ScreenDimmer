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
            this.labelScreen = new System.Windows.Forms.Label();
            this.labelBrightness = new System.Windows.Forms.Label();
            this.labelExpandCollapse = new System.Windows.Forms.Label();
            this.labelHelp = new System.Windows.Forms.Label();
            this.labelBug = new System.Windows.Forms.Label();
            this.labelAbout = new System.Windows.Forms.Label();
            this.buttonBright = new System.Windows.Forms.Button();
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
            this.checkBoxAllowTransition = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.contextMenuStrip2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBrightness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolTipHint
            // 
            this.toolTipHint.ShowAlways = true;
            // 
            // buttonDim
            // 
            this.buttonDim.AutoSize = true;
            this.buttonDim.BackColor = System.Drawing.Color.Black;
            this.buttonDim.ContextMenuStrip = this.contextMenuStrip2;
            this.buttonDim.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonDim.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonDim.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.buttonDim.FlatAppearance.BorderSize = 0;
            this.buttonDim.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDim.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDim.ForeColor = System.Drawing.Color.Gray;
            this.buttonDim.Location = new System.Drawing.Point(3, 3);
            this.buttonDim.Name = "buttonDim";
            this.buttonDim.Size = new System.Drawing.Size(85, 31);
            this.buttonDim.TabIndex = 46;
            this.buttonDim.Text = "Dim ";
            this.toolTipHint.SetToolTip(this.buttonDim, "Apply minimum brightness.\r\nRight click to change color.");
            this.buttonDim.UseVisualStyleBackColor = false;
            this.buttonDim.Click += new System.EventHandler(this.buttonDim_Click);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
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
            // labelScreen
            // 
            this.labelScreen.AutoSize = true;
            this.labelScreen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelScreen.Location = new System.Drawing.Point(3, 32);
            this.labelScreen.Name = "labelScreen";
            this.labelScreen.Size = new System.Drawing.Size(22, 27);
            this.labelScreen.TabIndex = 44;
            this.labelScreen.Text = "📺";
            this.labelScreen.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTipHint.SetToolTip(this.labelScreen, "Screen");
            // 
            // labelBrightness
            // 
            this.labelBrightness.AutoSize = true;
            this.labelBrightness.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelBrightness.Font = new System.Drawing.Font("Segoe UI Symbol", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBrightness.Location = new System.Drawing.Point(3, 0);
            this.labelBrightness.Name = "labelBrightness";
            this.labelBrightness.Size = new System.Drawing.Size(22, 32);
            this.labelBrightness.TabIndex = 1;
            this.labelBrightness.Text = "";
            this.labelBrightness.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTipHint.SetToolTip(this.labelBrightness, "Brightness");
            // 
            // labelExpandCollapse
            // 
            this.labelExpandCollapse.AutoSize = true;
            this.labelExpandCollapse.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelExpandCollapse.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelExpandCollapse.Location = new System.Drawing.Point(166, 102);
            this.labelExpandCollapse.Margin = new System.Windows.Forms.Padding(0);
            this.labelExpandCollapse.Name = "labelExpandCollapse";
            this.labelExpandCollapse.Size = new System.Drawing.Size(22, 20);
            this.labelExpandCollapse.TabIndex = 54;
            this.labelExpandCollapse.Text = "▷";
            this.toolTipHint.SetToolTip(this.labelExpandCollapse, "More...");
            this.labelExpandCollapse.Click += new System.EventHandler(this.labelExpandCollapse_Click);
            // 
            // labelHelp
            // 
            this.labelHelp.AutoSize = true;
            this.labelHelp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelHelp.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHelp.Location = new System.Drawing.Point(142, 102);
            this.labelHelp.Margin = new System.Windows.Forms.Padding(0);
            this.labelHelp.Name = "labelHelp";
            this.labelHelp.Size = new System.Drawing.Size(24, 20);
            this.labelHelp.TabIndex = 55;
            this.labelHelp.Text = "❔";
            this.toolTipHint.SetToolTip(this.labelHelp, "Hotkeys...");
            this.labelHelp.Click += new System.EventHandler(this.labelHelp_Click);
            // 
            // labelBug
            // 
            this.labelBug.AutoSize = true;
            this.labelBug.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelBug.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBug.ForeColor = System.Drawing.Color.Maroon;
            this.labelBug.Location = new System.Drawing.Point(155, 97);
            this.labelBug.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.labelBug.Name = "labelBug";
            this.labelBug.Size = new System.Drawing.Size(24, 20);
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
            this.labelAbout.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAbout.Location = new System.Drawing.Point(0, 102);
            this.labelAbout.Margin = new System.Windows.Forms.Padding(0);
            this.labelAbout.Name = "labelAbout";
            this.labelAbout.Size = new System.Drawing.Size(22, 20);
            this.labelAbout.TabIndex = 42;
            this.labelAbout.Text = "ⓘ";
            this.labelAbout.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTipHint.SetToolTip(this.labelAbout, "About...");
            this.labelAbout.Click += new System.EventHandler(this.labelAbout_Click);
            // 
            // buttonBright
            // 
            this.buttonBright.AutoSize = true;
            this.buttonBright.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonBright.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonBright.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.buttonBright.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonBright.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonBright.ForeColor = System.Drawing.Color.Gray;
            this.buttonBright.Location = new System.Drawing.Point(94, 3);
            this.buttonBright.Name = "buttonBright";
            this.buttonBright.Size = new System.Drawing.Size(85, 31);
            this.buttonBright.TabIndex = 45;
            this.buttonBright.Text = "Brighten ";
            this.toolTipHint.SetToolTip(this.buttonBright, "Apply maximum brightness.");
            this.buttonBright.UseVisualStyleBackColor = true;
            this.buttonBright.Click += new System.EventHandler(this.buttonBright_Click);
            // 
            // checkBoxZeroBrightness
            // 
            this.checkBoxZeroBrightness.AutoSize = true;
            this.tableLayoutPanel3.SetColumnSpan(this.checkBoxZeroBrightness, 2);
            this.checkBoxZeroBrightness.Location = new System.Drawing.Point(3, 3);
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
            this.tableLayoutPanel1.SetColumnSpan(this.comboBoxScreens, 3);
            this.comboBoxScreens.Dock = System.Windows.Forms.DockStyle.Top;
            this.comboBoxScreens.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxScreens.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxScreens.Location = new System.Drawing.Point(31, 35);
            this.comboBoxScreens.Name = "comboBoxScreens";
            this.comboBoxScreens.Size = new System.Drawing.Size(154, 21);
            this.comboBoxScreens.TabIndex = 43;
            this.comboBoxScreens.DropDown += new System.EventHandler(this.comboBoxScreens_DropDown);
            this.comboBoxScreens.SelectedIndexChanged += new System.EventHandler(this.comboBoxScreens_SelectedIndexChanged);
            // 
            // checkBoxDebug
            // 
            this.checkBoxDebug.AutoSize = true;
            this.checkBoxDebug.Location = new System.Drawing.Point(3, 100);
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
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
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
            this.timerFade.Interval = 40;
            this.timerFade.Tick += new System.EventHandler(this.timerFade_Tick);
            // 
            // trackBarBrightness
            // 
            this.trackBarBrightness.AutoSize = false;
            this.tableLayoutPanel1.SetColumnSpan(this.trackBarBrightness, 3);
            this.trackBarBrightness.Dock = System.Windows.Forms.DockStyle.Top;
            this.trackBarBrightness.LargeChange = 20;
            this.trackBarBrightness.Location = new System.Drawing.Point(31, 3);
            this.trackBarBrightness.Maximum = 100;
            this.trackBarBrightness.Minimum = 20;
            this.trackBarBrightness.Name = "trackBarBrightness";
            this.trackBarBrightness.Size = new System.Drawing.Size(154, 26);
            this.trackBarBrightness.SmallChange = 10;
            this.trackBarBrightness.TabIndex = 30;
            this.trackBarBrightness.TickFrequency = 10;
            this.trackBarBrightness.Value = 45;
            this.trackBarBrightness.ValueChanged += new System.EventHandler(this.trackBarBrightness_ValueChanged);
            // 
            // checkBoxEnforceOnTop
            // 
            this.checkBoxEnforceOnTop.AutoSize = true;
            this.checkBoxEnforceOnTop.Location = new System.Drawing.Point(3, 26);
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
            this.label1.Dock = System.Windows.Forms.DockStyle.Right;
            this.label1.Enabled = false;
            this.label1.Location = new System.Drawing.Point(5, 46);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 0, 3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 22);
            this.label1.TabIndex = 51;
            this.label1.Text = "Enforcing Period [seconds]:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // colorDialog1
            // 
            this.colorDialog1.FullOpen = true;
            this.colorDialog1.SolidColorOnly = true;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.AutoSize = true;
            this.numericUpDown1.Enabled = false;
            this.numericUpDown1.Location = new System.Drawing.Point(158, 49);
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
            this.numericUpDown1.Size = new System.Drawing.Size(47, 22);
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
            // checkBoxAllowTransition
            // 
            this.checkBoxAllowTransition.AutoSize = true;
            this.tableLayoutPanel3.SetColumnSpan(this.checkBoxAllowTransition, 2);
            this.checkBoxAllowTransition.Location = new System.Drawing.Point(3, 77);
            this.checkBoxAllowTransition.Name = "checkBoxAllowTransition";
            this.checkBoxAllowTransition.Size = new System.Drawing.Size(115, 17);
            this.checkBoxAllowTransition.TabIndex = 57;
            this.checkBoxAllowTransition.Text = "Enable Transition";
            this.checkBoxAllowTransition.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.labelBrightness, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelScreen, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.trackBarBrightness, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.comboBoxScreens, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelExpandCollapse, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.labelHelp, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.labelAbout, 0, 3);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(188, 122);
            this.tableLayoutPanel1.TabIndex = 58;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel1.SetColumnSpan(this.tableLayoutPanel2, 4);
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.buttonDim, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.buttonBright, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 62);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(182, 37);
            this.tableLayoutPanel2.TabIndex = 59;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.AutoSize = true;
            this.tableLayoutPanel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.Controls.Add(this.checkBoxZeroBrightness, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.checkBoxEnforceOnTop, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.numericUpDown1, 1, 2);
            this.tableLayoutPanel3.Controls.Add(this.labelBug, 1, 4);
            this.tableLayoutPanel3.Controls.Add(this.checkBoxAllowTransition, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.checkBoxDebug, 0, 4);
            this.tableLayoutPanel3.Controls.Add(this.label1, 0, 2);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(197, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 5;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(208, 120);
            this.tableLayoutPanel3.TabIndex = 59;
            this.tableLayoutPanel3.Visible = false;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.AutoSize = true;
            this.tableLayoutPanel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel3, 1, 0);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(408, 128);
            this.tableLayoutPanel4.TabIndex = 60;
            // 
            // ScreenDimmer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(430, 153);
            this.Controls.Add(this.tableLayoutPanel4);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ScreenDimmer";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.screenDimmer_FormClosing);
            this.contextMenuStrip2.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBrightness)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTipHint;
        private System.Windows.Forms.CheckBox checkBoxZeroBrightness;
        private System.Windows.Forms.Button buttonDim;
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
        private System.Windows.Forms.CheckBox checkBoxAllowTransition;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button buttonBright;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
    }
}

