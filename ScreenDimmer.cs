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
        #region private properties
        /// <summary>
        /// Configuration file path.
        /// </summary>
        private readonly string confFile = "ScreenDimmer.conf";

        // child forms
        /// <summary>
        /// The overlay window.
        /// </summary>
        private Form overlayWindow;
        /// <summary>
        /// The about box.
        /// </summary>
        private AboutBox1 aboutBox;
        /// <summary>
        /// The help window ("show hotkeys" window).
        /// </summary>
        private HelpWindow helpWindow;
        /// <summary>
        /// The brightness OSD.
        /// </summary>
        private OsdWindow osdWindow;

        /// <summary>
        /// Contains all the settings.
        /// </summary>
        private Configuration configuration;

        /// <summary>
        /// Used for governing brightness increasing/decreasing speed.
        /// </summary>
        private int hotkeyRepeatCount;
        /// <summary>
        /// State variable to check if a hotkey is repeatedly pressed.
        /// </summary>
        private int lastHotkeyId = -1;
        /// <summary>
        /// Flag to check if closing to tray or exitting application.
        /// </summary>
        private bool isContextClose = false;
        /// <summary>
        /// State variable to decide to refresh the screen list or not.
        /// </summary>
        private int numberOfScreens = -1;
        
        // transition effect
        /// <summary>
        /// The total change in brightness track bar value.
        /// </summary>
        private int fadeDistance;
        /// <summary>
        /// The brightness track bar value when enabling the transition timer.
        /// </summary>
        private int fadeOrigin;
        /// <summary>
        /// The current time when enabling the transition timer.
        /// </summary>
        private DateTime fadeStartTime;
        #endregion
        
        #region static members
        /// <summary>
        /// Icon to use in system tray.
        /// </summary>
        public static readonly Icon IconMediumBright = TextIcon.CreateTextIcon("\uE286", Color.White);
        /// <summary>
        /// Main icon.
        /// </summary>
        public static readonly Icon IconMediumBright32x32 = TextIcon.CreateTextIcon("\uE286", Color.White, "", 32);
        #endregion

        public partial class Form1 : Form
        {
            public Form1()
            {
                //InitializeComponent();
            }

            const int WM_NCHITTEST = 0x0084;
            const int HTCLIENT = 1;
            const int HTCAPTION = 2;
            protected override void WndProc(ref Message m)
            {
                base.WndProc(ref m);
                switch (m.Msg)
                {
                    case WM_NCHITTEST:
                        if (m.Result == (IntPtr)HTCLIENT)
                        {
                            var p = this.PointToClient(new Point(m.LParam.ToInt32()));

                            m.Result =
                                (IntPtr)
                                (p.X <= 6
                                     ? p.Y <= 6 ? 13 : p.Y >= this.Height - 7 ? 16 : 10
                                     : p.X >= this.Width - 7
                                           ? p.Y <= 6 ? 14 : p.Y >= this.Height - 7 ? 17 : 11
                                           : p.Y <= 6 ? 12 : p.Y >= this.Height - 7 ? 15 : p.Y <= 24 ? 2 : 1);
                        }
                        break;
                }
            }
        }

        public ScreenDimmer()
        {
            InitializeComponent();
            initOverlayWindow();
            
            aboutBox = new AboutBox1();
            helpWindow = new HelpWindow();
            configuration = new Configuration();
            osdWindow = new OsdWindow();

            // has to be in this order!
            populateScreenList();
            loadConfiguration();
            hookKeys();

            notifyIcon1.Icon = IconMediumBright;
            Icon = IconMediumBright32x32;
            Text = string.Format("Screen Dimmer {0}", Assembly.GetExecutingAssembly().GetName().Version.ToString());
        }
        
        /// <summary>
        /// Initializes the overlay window (no border, click-through, see-through, invisible to Alt+Tab).
        /// </summary>
        private void initOverlayWindow()
        {
            overlayWindow = new Form1();
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

        #region hotkey management
        /// <summary>
        /// Tries to register all the global hotkeys.
        /// (!) Has to be called after loading configuration (when the hotkeys are defined).
        /// </summary>
        private void hookKeys()
        {
            StringBuilder sb = new StringBuilder();
            bool success = true;
            success &= tryHookKeyAppendError(configuration.HotKeyIncreaseBrightness, ref sb);
            success &= tryHookKeyAppendError(configuration.HotKeyDecreaseBrightness, ref sb);
            success &= tryHookKeyAppendError(configuration.HotKeyBrighten, ref sb);
            success &= tryHookKeyAppendError(configuration.HotKeyDim, ref sb);
            success &= tryHookKeyAppendError(configuration.HotKeyForceOnTop, ref sb);
            success &= tryHookKeyAppendError(configuration.HotKeyHalt, ref sb);
            success &= tryHookKeyAppendError(configuration.HotKeyResize, ref sb);
            if (!success)
            {
                notifyIcon1.ShowBalloonTip(0, "Cannot register one or more hotkeys.", sb.ToString(), ToolTipIcon.Warning);
            }
        }

        /// <summary>
        /// Pupulates the hotkeys on to the help window.
        /// </summary>
        private void populateHotkeys()
        {
            helpWindow.ResetHotkeyPanel();
            helpWindow.AddHotKey(configuration.HotKeyDim);
            helpWindow.AddHotKey(configuration.HotKeyBrighten);
            helpWindow.AddHotKey(configuration.HotKeyIncreaseBrightness);
            helpWindow.AddHotKey(configuration.HotKeyDecreaseBrightness);
            helpWindow.AddHotKey(configuration.HotKeyForceOnTop);
            helpWindow.AddHotKey(configuration.HotKeyHalt);
            helpWindow.AddHotKey(configuration.HotKeyResize);
        }

        /// <summary>
        /// Tries to register one hotkey and handle the error message if there are any.
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
        /// WndProc message listener.
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
                    } 
                    else
                    {
                        hotkeyRepeatCount = 0;
                        lastHotkeyId = hotkeyId;
                    }
                    //Console.WriteLine("Last {0} | Current {1} | Repeat {2}", lastHotkeyId, hotkeyId, hotkeyRepeatCount);
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
                        dim();
                    }
                    else if (hotkeyId == configuration.HotKeyBrighten.Id)
                    {
                        brighten();
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
                    else if (hotkeyId == configuration.HotKeyResize.Id)
                    {
                        NativeMethods.SetWindowLong(overlayWindow.Handle, NativeMethods.GWL_EXSTYLE,
                            NativeMethods.GetWindowLong(overlayWindow.Handle, NativeMethods.GWL_EXSTYLE)
                                ^ (int)ExtendedWindowStyles.WS_EX_TRANSPARENT);
                    }
                    break;
            }
            base.WndProc(ref m);
        }
        #endregion

        #region color management
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

        /// <summary>
        /// Gets the current color of the overlay window.
        /// </summary>
        /// <returns></returns>
        private Color getDimColor() {
            return overlayWindow.BackColor;
        }
        #endregion

        #region brightness management
        private void checkBoxZeroBrightness_CheckedChanged(object sender, EventArgs e)
        {
            setTrackBarBrightnessMinimum(checkBoxZeroBrightness.Checked ? 0 : 20);
        }
    
        /// <summary>
        /// Sets the minimum for the brightness track bar.
        /// If the current track bar value is smaller than the desired value, 
        /// it will be set to the desired value as well.
        /// Note that the case that the desired value is larger than maximum value is not handled here!
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
        /// Updates the ovelay opacity level according to current brightness track bar value.
        /// </summary>
        /// <param name="brightness"></param>
        private void setBrightness(int brightness)
        {
            toolTipHint.SetToolTip(trackBarBrightness, string.Format("{0}%", brightness));
            NativeMethods.SetLayeredWindowAttributes(overlayWindow.Handle, 0, (byte)(255 - (brightness / 100f) * 255),
                (int)LayeredWindowAttributeFlags.LWA_ALPHA);
        }

        /// <summary>
        /// Increases brightness track bar value.
        /// </summary>
        private void increseBrightness(int speed)
        {
            if (trackBarBrightness.Value == trackBarBrightness.Maximum)
            {
                return;
            }
            
            int modifiedSpeed = speed / 3 + 1;
            if (trackBarBrightness.Value+modifiedSpeed >= trackBarBrightness.Maximum)
            {
                trackBarBrightness.Value = trackBarBrightness.Maximum;
            } else {
                trackBarBrightness.Value += modifiedSpeed;
            }
        }

        /// <summary>
        /// Decreases brightness track bar value.
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

        /// <summary>
        /// Dims the screen to minimum brightness.
        /// </summary>
        private void dim()
        {
            if (checkBoxAllowTransition.Checked)
            {
                fade(trackBarBrightness.Minimum);
            }
            else
            {
                trackBarBrightness.Value = trackBarBrightness.Minimum;
            }
        }

        /// <summary>
        /// Brightens the sceen to maximum brightness.
        /// </summary>
        private void brighten()
        {
            if (checkBoxAllowTransition.Checked)
            {
                fade(trackBarBrightness.Maximum);
            }
            else
            {
                trackBarBrightness.Value = trackBarBrightness.Maximum;
            }
        }

        /// <summary>
        /// Initializes state variables for transition effect.
        /// </summary>
        /// <param name="target"></param>
        private void fade(int target)
        {

            fadeDistance = target - trackBarBrightness.Value;
            if (fadeDistance == 0)
            {
                return;
            }
            fadeOrigin = trackBarBrightness.Value;
            timerFade.Enabled = true;
            fadeStartTime = DateTime.Now;
        }

        /// <summary>
        /// Interpolates brightness in real time to ensure the transition happens in the period defined in configuration.FadeDuration.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerFade_Tick(object sender, EventArgs e)
        {
            TimeSpan currentTime = DateTime.Now - fadeStartTime;
            double target1 = (currentTime.TotalMilliseconds * fadeDistance / configuration.FadeDuration);
            int target = (int)(fadeOrigin + target1);
            if ((target >= trackBarBrightness.Maximum) && (fadeDistance > 0))
            {
                trackBarBrightness.Value = trackBarBrightness.Maximum;
                timerFade.Enabled = false;
            }
            else if ((target <= trackBarBrightness.Minimum) && (fadeDistance < 0))
            {
                trackBarBrightness.Value = trackBarBrightness.Minimum;
                timerFade.Enabled = false;
            }
            else
            {
                trackBarBrightness.Value = target;
            }
            //Console.WriteLine("{0} Current {1} | Different {3} | Target {2}:{5}| Total Distance {4}",
            //    currentTime.TotalMilliseconds, 0, target, target1, fadeDistance, 0 + target1);
        }

        private void buttonDim_Click(object sender, EventArgs e)
        {
            dim();
        }

        private void buttonBright_Click(object sender, EventArgs e)
        {
            brighten();
        }

        private void trackBarBrightness_ValueChanged(object sender, EventArgs e)
        {
            setBrightness(((TrackBar)sender).Value);
            if (!Visible)
            {
                osdWindow.Display(((TrackBar)sender).Value.ToString() + "%", new Font("Segeo UI", 32), Color.Black, Color.White, 20, 20, 255, 100, 1000, 1000);
            }
        }
        #endregion
   
        #region screen management
        /// <summary>
        /// Sets the size and position of the form to cover the desired screen.
        /// Does nothing if the desired screen index is invalid.
        /// </summary>
        /// <param name="form"></param>
        /// <param name="screenIndex"></param>
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

        /// <summary>
        /// Pupulates the sceen list into the combo box.
        /// </summary>
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
            catch (Exception)
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
            configuration.IsTransition = checkBoxAllowTransition.Checked;
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
            checkBoxEnforceOnTop.Checked = configuration.IsEnforceOnTop;
            checkBoxZeroBrightness.Checked = configuration.IsZeroBrightness;
            checkBoxDebug.Checked = configuration.IsDebug;
            checkBoxAllowTransition.Checked = configuration.IsTransition;
        }
        #endregion

        #region other UI callbacks
        private void labelExpandCollapse_Click(object sender, EventArgs e)
        {
            toggleAdvancedSettings();
        }

        private void toggleAdvancedSettings()
        {
            tableLayoutPanel3.Visible = !tableLayoutPanel3.Visible;
            if (tableLayoutPanel3.Visible)
            {
                labelExpandCollapse.Text = "◁";
                toolTipHint.SetToolTip(labelExpandCollapse, "Less");
            }
            else
            {
                labelExpandCollapse.Text = "▷";
                toolTipHint.SetToolTip(labelExpandCollapse, "More...");
            }
        }

        private void timerEnforceOnTop_Tick(object sender, EventArgs e)
        {
            enforceOnTop();
        }

        /// <summary>
        /// Forces the application to top most.
        /// </summary>
        private void enforceOnTop()
        {
            overlayWindow.TopMost = true; // overlay window is set to top most
            TopMost = true; // main form has to be on top of overlay window
        }

        /// <summary>
        /// Updates the interval of enforcing-on-top timer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            timerEnforceOnTop.Interval = (int)(((NumericUpDown)sender).Value * 1000);
        }

        /// <summary>
        /// Starts/stops enforcing-on-top timer.
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

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
        }

        private void controlPanelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
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
            Close();
        }
        #endregion
    }
}
