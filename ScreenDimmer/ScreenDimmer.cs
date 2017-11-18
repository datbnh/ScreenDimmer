using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Augustine.ScreenDimmer
{
    internal partial class ScreenDimmer : Form
    {
        private Form overlayWindow;
        private AboutBox1 aboutBox;
        private HelpWindow helpWindow;

        private readonly int HEIGHT = 158;
        private readonly int WIDTH_EXPANDED = 405;
        private readonly int WIDTH_COLLAPSED = 190;

        /// <summary>
        /// configuration
        /// </summary>
        private Configuration configuration;

        /// <summary>
        /// State of main window.
        /// </summary>
        private bool isExpanded;
        /// <summary>
        /// Increase if a registered hotkey is pressed repeatedly. Use for governing brightness increase/decrease speed.
        /// </summary>
        private int hotkeyRepeatCount;
        /// <summary>
        /// Flag to check if a hotkey is repeatedly pressed.
        /// </summary>
        private int lastHotkeyId = -1;

        private bool isContextClose = false;

        private int numberOfScreens = -1;
        
        //private readonly Icon iconFullBright = TextIcon.CreateTextIcon("\uE284");
        //private readonly Icon iconZeroBright = TextIcon.CreateTextIcon("\uE285");
        public static readonly Icon IconMediumBright = TextIcon.CreateTextIcon("\uE286", Color.White);
        public static readonly Icon IconMediumBright32x32 = TextIcon.CreateTextIcon("\uE286", Color.White, "", 32);

        private readonly string confFile = "ScreenDimmer.conf";

        public ScreenDimmer()
        {
            InitializeComponent();
            initOverlayWindow();
            
            aboutBox = new AboutBox1();
            helpWindow = new HelpWindow();
            configuration = new Configuration();
            
            populateScreenList();
            collapse();
            loadConfiguration();
            hookKeys();

            notifyIcon1.Icon = IconMediumBright;
            
            Icon = IconMediumBright32x32;

            Text = string.Format("Screen Dimmer {0}", Assembly.GetExecutingAssembly().GetName().Version.ToString());
        }
        
        /// <summary>
        /// Initializes the overlay window.
        /// </summary>
        private void initOverlayWindow()
        {
            overlayWindow = new Form();
            overlayWindow.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            overlayWindow.ShowInTaskbar = false;
            overlayWindow.Show();
            overlayWindow.WindowState = FormWindowState.Maximized;
            enforceOnTop();
            NativeMethods.SetWindowLong(overlayWindow.Handle, NativeMethods.GWL_EXSTYLE,
                // get current GWL_EXSTYLE
                NativeMethods.GetWindowLong(overlayWindow.Handle, NativeMethods.GWL_EXSTYLE)
                // click-through. Need to be combined with see-through.
                // Need to recalled if opacity changed by setting Form.Cpacity.
                // Using SetLayeredWindowAttributes will not require to recall this.
                | (int)ExtendedWindowStyles.WS_EX_LAYERED
                // see-through
                | (int)ExtendedWindowStyles.WS_EX_TRANSPARENT
                // alt+tab invisible, need to set ShowInTaskbar to false.
                | (int)ExtendedWindowStyles.WS_EX_TOOLWINDOW);
            //NativeMethods.SetLayeredWindowAttributes(overlayWindow.Handle, 0, 192,
            //    (int)LayeredWindowAttributeFlags.LWA_ALPHA);
        }

        #region hotkeys
        /// <summary>
        /// (!) Has to be called after loading configuration.
        /// Tries to register all the global hotkeys.
        /// </summary>
        private void hookKeys()
        {
            StringBuilder sb = new StringBuilder();
            bool success = true;
            success &= tryHookKeyAppendError(configuration.HotKeyIncreaseBrightness, ref sb);
            success &= tryHookKeyAppendError(configuration.HotKeyDecreaseBrightness, ref sb);
            success &= tryHookKeyAppendError(configuration.HotKeyBright, ref sb);
            success &= tryHookKeyAppendError(configuration.HotKeyDim, ref sb);
            success &= tryHookKeyAppendError(configuration.HotKeyForceOnTop, ref sb);
            success &= tryHookKeyAppendError(configuration.HotKeyHalt, ref sb);
            if (!success)
            {
                notifyIcon1.ShowBalloonTip(0, "Cannot register one or more hotkeys.", sb.ToString(), ToolTipIcon.Warning);
            }
        }

        private void populateHotkeys()
        {
            helpWindow.ResetHotkeyPanel();
            helpWindow.AddHotKey(configuration.HotKeyDim);
            helpWindow.AddHotKey(configuration.HotKeyBright);
            helpWindow.AddHotKey(configuration.HotKeyIncreaseBrightness);
            helpWindow.AddHotKey(configuration.HotKeyDecreaseBrightness);
            helpWindow.AddHotKey(configuration.HotKeyForceOnTop);
            helpWindow.AddHotKey(configuration.HotKeyHalt);
        }

        /// <summary>
        /// Tries to register a hotkey and handle the exception if happens.
        /// </summary>
        /// <param name="hotkey"></param>
        /// <param name="sb"></param>
        /// <returns>true if successfully register the hotkey</returns>
        private bool tryHookKeyAppendError(GlobalHotKey hotkey, ref StringBuilder sb)
        {
            try
            {
                hotkey.Register(Handle);
                return true;
            }
            catch (Exception e)
            {
                sb.AppendLine(e.Message);
            }
            return false;
        }

        /// <summary>
        /// WndProc message listener
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            // Listen for operating system messages.
            switch (m.Msg)
            {
                case (int)WindowMessage.WM_HOTKEY:
                    int hotkeyId = (int)m.WParam;
                    if (hotkeyId == lastHotkeyId)
                    {
                        hotkeyRepeatCount++;
                    } else
                    {
                        hotkeyRepeatCount = 0;
                        lastHotkeyId = hotkeyId;
                    }
                    // Console.WriteLine(HotKeyId);
                    // Id can change, then cannot use switch case.
                    if (hotkeyId == configuration.HotKeyIncreaseBrightness.Id)
                    {
                        increseBrightness(hotkeyRepeatCount);
                    }
                    else if (hotkeyId == configuration.HotKeyDecreaseBrightness.Id)
                    {
                        decreaseBrightness(hotkeyRepeatCount);
                    }
                    else if (hotkeyId == configuration.HotKeyDim.Id)
                    {
                        setBrightness(trackBarBrightness.Maximum);
                    }
                    else if (hotkeyId == configuration.HotKeyBright.Id)
                    {
                        setBrightness(trackBarBrightness.Maximum);
                    }
                    else if (hotkeyId == configuration.HotKeyForceOnTop.Id)
                    {
                        enforceOnTop();
                    }
                    else if (hotkeyId == configuration.HotKeyHalt.Id)
                    {
                        isContextClose = true;
                        Close();
                    }
                    break;
            }
            base.WndProc(ref m);
        }
        #endregion

        /// <summary>
        /// Gets and updates the color from the color picker tool.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = overlayWindow.BackColor;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                setDimColor(colorDialog1.Color);
            }
        }

        /// <summary>
        /// Sets the backcolor of the overlay window and dim button to the desired color.
        /// </summary>
        /// <param name="color"></param>
        private void setDimColor(Color color)
        {
            buttonDim.BackColor = color;
            overlayWindow.BackColor = color;
        }

        private Color getDimColor() {
            return overlayWindow.BackColor;
        }


        private void labelExpandCollapse_Click(object sender, EventArgs e)
        {
            if (isExpanded)
            {
                collapse();
            }
            else
            {
                expand();
            }
        }

        private void expand()
        {
            this.Size = new Size(WIDTH_EXPANDED, HEIGHT);
            labelExpandCollapse.Text = "◁";
            toolTipHint.SetToolTip(labelExpandCollapse, "Less");
            isExpanded = true;
        }

        private void collapse()
        {
            this.Size = new Size(WIDTH_COLLAPSED, HEIGHT);
            labelExpandCollapse.Text = "▷";
            toolTipHint.SetToolTip(labelExpandCollapse, "More...");
            isExpanded = false;
        }


        private void toggleDebug(bool state)
        {
            // TODO
        }

        #region enforcing on top
        private void timerEnforceOnTop_Tick(object sender, EventArgs e)
        {
            enforceOnTop();
        }

        /// <summary>
        /// Enforce the program to the top most.
        /// </summary>
        private void enforceOnTop()
        {
            overlayWindow.TopMost = true;
            // bring the main form to top of the overlay window.
            TopMost = true;
            //overlayWindow.Validate();
            //Validate();
        }

        private void toggleEnforceOnTop(bool desiredState)
        {
            if (checkBoxEnforceOnTop.Checked == desiredState)
            {
                return;
            }
        }

        /// <summary>
        /// Update the interval of enforcing-on-top timer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            timerEnforceOnTop.Interval = (int)(((NumericUpDown)sender).Value * 1000);
        }

        /// <summary>
        /// Start/stop enforcing-on-top timer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxEnforceOnTop_CheckedChanged(object sender, EventArgs e)
        {
            bool isEnabled = ((CheckBox)sender).Checked;
            numericUpDown1.Enabled = isEnabled;
            label1.Enabled = isEnabled;
            if (isEnabled)
            {
                timerEnforceOnTop.Start();
            }
            else
            {
                timerEnforceOnTop.Stop();
            }
        }
        #endregion

        #region brightness control
        private void checkBoxZeroBrightness_CheckedChanged(object sender, EventArgs e)
        {
            setTrackBarBrightnessMinimum(checkBoxZeroBrightness.Checked ? 0 : 20);
        }

        private void toggleZeroBrighness(bool desiredState)
        {
            if (checkBoxZeroBrightness.Checked == desiredState)
            {
                return;
            }
            checkBoxZeroBrightness.Checked = desiredState;
        }
        
        /// <summary>
        /// Set the minimum for the brightness track bar.
        /// </summary>
        /// <param name="minimum"></param>
        private void setTrackBarBrightnessMinimum(int minimum)
        {
            if (trackBarBrightness.Value < minimum)
            {
                trackBarBrightness.Value = minimum;
            }
            trackBarBrightness.Minimum = minimum;
        }

        /// <summary>
        /// Update the ovelay opacity level according to current trackbar value.
        /// </summary>
        /// <param name="brightness"></param>
        private void setBrightness(int brightness)
        {
            toolTipHint.SetToolTip(trackBarBrightness, string.Format("{0} %", brightness));
            NativeMethods.SetLayeredWindowAttributes(overlayWindow.Handle, 0, (byte)(255 - (brightness / 100f) * 255),
                (int)LayeredWindowAttributeFlags.LWA_ALPHA);
        }

        /// <summary>
        /// Increase brightness track bar value.
        /// </summary>
        private void increseBrightness(int speed)
        {
            if (trackBarBrightness.Value == trackBarBrightness.Maximum) {
                return;
            }
            
            int modifiedSpeed = speed / 3;
            if (trackBarBrightness.Value+modifiedSpeed >= trackBarBrightness.Maximum)
            {
                trackBarBrightness.Value = trackBarBrightness.Maximum;
            } else {
                trackBarBrightness.Value += modifiedSpeed;
            }
        }

        /// <summary>
        /// Decrease brightness track bar value.
        /// </summary>
        private void decreaseBrightness(int speed)
        {
            if (trackBarBrightness.Value == trackBarBrightness.Minimum)
            {
                return;
            }

            int modifiedSpeed = speed / 3 + 1;
            if (trackBarBrightness.Value - modifiedSpeed <= trackBarBrightness.Minimum)
            {
                trackBarBrightness.Value = trackBarBrightness.Minimum;
            }
            else
            {
                trackBarBrightness.Value -= modifiedSpeed;
            }
        }

        private void trackBarBrightness_ValueChanged(object sender, EventArgs e)
        {
            setBrightness(((TrackBar)sender).Value);
        }
        #endregion

        private void labelAbout_Click(object sender, EventArgs e)
        {
            aboutBox.TopMost = true;
            aboutBox.ShowDialog();
        }

        private void labelHelp_Click(object sender, EventArgs e)
        {
            populateHotkeys();
            helpWindow.TopMost = true;
            helpWindow.ShowDialog();
        }

        private void labelBug_Click(object sender, System.EventArgs e)
        {
            //TODO
        }

        private void screenDimmer_FormClosing(object sender, FormClosingEventArgs e)
        {
            saveConfiguration();
            if (e.CloseReason == CloseReason.WindowsShutDown)
            {
                return;
            }
            if (!isContextClose)
            {
                this.Hide();
                e.Cancel = true;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isContextClose = true;
            notifyIcon1.Dispose();
            //overlayWindow.Close(); // not necessary, all children will be closed when main form closed.
            //helpWindow.Close();
            Close();
        }

        private void controlPanelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        #region screen management
        private void setScreen(Form form, int screenIndex)
        {
            if (Screen.AllScreens.Length - 1 < screenIndex)
            {
                return; // invalid index
            }
            FormWindowState currentState = form.WindowState;
            if (currentState != FormWindowState.Normal)
            {
                form.WindowState = FormWindowState.Normal;
            }
            form.Location = Screen.AllScreens[screenIndex].WorkingArea.Location;
            form.WindowState = currentState;
        }

        private void populateScreenList()
        {
            Screen[] screens = Screen.AllScreens;
            if (numberOfScreens != screens.Length)
            {
                numberOfScreens = screens.Length;
                List<string> screensNames = new List<string>();
                foreach (Screen item in screens)
                {
                    screensNames.Add(item.DeviceName);
                }
                int currentSelected = comboBoxScreens.SelectedIndex;
                comboBoxScreens.Items.Clear();
                comboBoxScreens.Items.AddRange(screensNames.ToArray());
                if (currentSelected < numberOfScreens)
                {
                    comboBoxScreens.SelectedIndex = currentSelected;
                } 
                else
                {
                    comboBoxScreens.SelectedIndex = 0;
                }
            }
        }

        private void comboBoxScreens_SelectedIndexChanged(object sender, EventArgs e)
        {
            setScreen(overlayWindow, ((ComboBox)sender).SelectedIndex);
        }

        private void comboBoxScreens_DropDown(object sender, EventArgs e)
        {
            populateScreenList();
        }
        #endregion

        #region configuration management
        private void saveConfiguration()
        {
            uiToConfig();
            configuration.SaveToFile(confFile);
        }

        /// <summary>
        /// (!) Has to be called after initializing all UI components.
        /// </summary>
        private void loadConfiguration()
        {
            try
            {
                configuration = Configuration.LoadFromFile(confFile);
            }
            catch (Exception ex)
            {
                configuration.LoadDefault();
            }
            configToUi();
        }

        private void uiToConfig()
        {
            configuration.CurrentBrightness = (byte) trackBarBrightness.Value;
            configuration.DimColor = getDimColor();
            configuration.EnforcingPeriod = (int) numericUpDown1.Value;
            configuration.IsDebug = checkBoxDebug.Checked;
            configuration.IsEnforceOnTop = checkBoxEnforceOnTop.Checked;
            // configuration.IsTransition = checkBoxTransition.Checked; //TODO
            configuration.IsZeroBrightness = checkBoxZeroBrightness.Checked;
            configuration.MonitorIndex = (byte) comboBoxScreens.SelectedIndex;
        }

        private void configToUi()
        {
            if (configuration.CurrentBrightness < trackBarBrightness.Minimum)
            {
                trackBarBrightness.Value = trackBarBrightness.Minimum;
            }
            else if (configuration.CurrentBrightness > trackBarBrightness.Maximum)
            {
                trackBarBrightness.Value = trackBarBrightness.Maximum;
            }
            else
            {
                trackBarBrightness.Value = configuration.CurrentBrightness;
            }
            
            if (configuration.EnforcingPeriod < numericUpDown1.Minimum)
            {
                numericUpDown1.Value = numericUpDown1.Minimum;
            }
            else if (configuration.CurrentBrightness > trackBarBrightness.Maximum)
            {
                numericUpDown1.Value = numericUpDown1.Maximum;
            }
            else
            {
                numericUpDown1.Value = configuration.EnforcingPeriod;
            }
            
            if (comboBoxScreens.Items.Count > configuration.MonitorIndex)
            {
                comboBoxScreens.SelectedIndex = configuration.MonitorIndex;
            }
            else
            {
                comboBoxScreens.SelectedIndex = 0;
            }

            setDimColor(configuration.DimColor);
            toggleEnforceOnTop(configuration.IsEnforceOnTop);
            toggleZeroBrighness(configuration.IsZeroBrightness);
            toggleDebug(configuration.IsDebug);
        }
        #endregion

        private void buttonDim_Click(object sender, EventArgs e)
        {
            setBrightness(trackBarBrightness.Minimum);
        }

        private void buttonBright_Click(object sender, EventArgs e)
        {
            setBrightness(trackBarBrightness.Maximum);
        }
    }
}
