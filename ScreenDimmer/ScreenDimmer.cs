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
        //private Form overlayWindow;
        private Dictionary<string, Form> overlayWindows;
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
        //private OsdWindow osdWindow;
        private Dictionary<string,OsdWindow> osdWindows;

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

        public ScreenDimmer()
        {
            InitializeComponent();
            initOverlayWindow();
            
            aboutBox = new AboutBox1();
            helpWindow = new HelpWindow();
            configuration = new Configuration();
            osdWindows = Screen.AllScreens.ToDictionary(k=>k.DeviceName, v=>new OsdWindow(v));

            // has to be in this order!
            initScreenOptions();
            loadConfiguration();
            hookKeys();

            notifyIcon1.Icon = IconMediumBright;
            Icon = IconMediumBright32x32;
            Text = string.Format("Screen Dimmer {0}", Assembly.GetExecutingAssembly().GetName().Version.ToString());
            setBrightness(trackBarBrightness.Value);
        }

        /// <summary>
        /// Creates checkboxes for each screen, allows users to only dim certain screens.
        /// </summary>
        private void initScreenOptions()
        {
            foreach (var screen in Screen.AllScreens)
            {
                var checkbox = new CheckBox()
                {
                    Text = screen.DeviceName,
                    Tag = screen
                };
                checkbox.CheckedChanged += (object sender, EventArgs e) =>
                {
                    var cb = sender as CheckBox;
                    var scr = cb.Tag as Screen;
                    //osdWindows[scr.DeviceName].Visible
                    var screens = getEnabledScreenList();
                    //cb.Visible = screens.Contains(scr.DeviceName);
                    overlayWindows[scr.DeviceName].Visible = screens.Contains(scr.DeviceName);
                    setBrightness(trackBarBrightness.Value);
                };
                tableLayoutPanel2.Controls.Add(checkbox);
            }
        }
        
        /// <summary>
        /// Initializes the overlay window (no border, click-through, see-through, invisible to Alt+Tab).
        /// </summary>
        private void initOverlayWindow()
        {
            overlayWindows = Screen.AllScreens.ToDictionary(k => k.DeviceName, 
                (v) => {

                    return new Form()
                    {
                        FormBorderStyle = System.Windows.Forms.FormBorderStyle.None,
                        ShowInTaskbar = false,
                        Visible = false,
                        Tag = v
                    };
                }
            );

            foreach (var window in overlayWindows)
            {
                window.Value.Load += (object sender, EventArgs e) => {
                    var overlay = (sender as Form);
                    var screen = overlay.Tag as Screen;
                    overlay.Top = screen.Bounds.Top;
                    overlay.Left = screen.Bounds.Left;
                    overlay.Width = screen.Bounds.Width;
                    overlay.Height = screen.Bounds.Height;
                    overlay.WindowState = FormWindowState.Maximized;
                    overlay.TopMost = true;
                };
                window.Value.Show();
                NativeMethods.SetWindowLong(window.Value.Handle, NativeMethods.GWL_EXSTYLE,
                    // get current GWL_EXSTYLE
                    NativeMethods.GetWindowLong(window.Value.Handle, NativeMethods.GWL_EXSTYLE)
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

            TopMost = true;
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
            foreach (var window in overlayWindows)
                colorDialog1.Color = window.Value.BackColor;
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
            foreach (var window in overlayWindows)
                window.Value.BackColor = color;
        }

        /// <summary>
        /// Gets the current color of the overlay window.
        /// </summary>
        /// <returns></returns>
        private Color getDimColor() 
        {
            foreach (var window in overlayWindows) //return first for now
                return window.Value.BackColor;
            return Color.White;
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

            var enabledScreenList = getEnabledScreenList();
            foreach (var window in overlayWindows)
            {
                var scr = window.Value.Tag as Screen;
                if (enabledScreenList.Contains(scr.DeviceName))
                {
                    NativeMethods.SetLayeredWindowAttributes(window.Value.Handle, 0, (byte)(255 - (brightness / 100f) * 255),
                        (int)LayeredWindowAttributeFlags.LWA_ALPHA);
                    window.Value.Name = "tng";
                }
                else
                {
                    window.Value.Visible = false;
                }
            }
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
            var enabledScreenList = getEnabledScreenList();

            foreach (var osdWindow in osdWindows)
            {
                if (enabledScreenList.Contains(osdWindow.Key))
                {
                    osdWindow.Value.Display($"({osdWindow.Key}) {((TrackBar)sender).Value}%", new Font("Segeo UI", 32), Color.Black, Color.White, 255, 100, 1000, 1000);
                }
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
        private void setScreen(Form form, Screen screen)
        {
            FormWindowState currentState = form.WindowState;
            if (currentState != FormWindowState.Normal)
            {
                form.WindowState = FormWindowState.Normal;
            }
            form.Location = screen.WorkingArea.Location;
            form.WindowState = currentState;
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
            configuration.EnabledScreens = getEnabledScreenList();
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

            setDimColor(configuration.DimColor);
            checkBoxEnforceOnTop.Checked = configuration.IsEnforceOnTop;
            checkBoxZeroBrightness.Checked = configuration.IsZeroBrightness;
            checkBoxDebug.Checked = configuration.IsDebug;
            checkBoxAllowTransition.Checked = configuration.IsTransition;


            var EnabledScreens = new List<string>();
            foreach (var control in tableLayoutPanel2.Controls)
            {
                var cb = control as CheckBox;
                if (cb != null && cb.Enabled)
                {
                    var scr = cb.Tag as Screen;
                    if (configuration.EnabledScreens?.Contains(scr.DeviceName)==true)
                    {
                        cb.Checked = true;
                    }
                }
            }
        }

        private List<string> getEnabledScreenList()
        {
            var EnabledScreens = new List<string>();
            foreach (var control in tableLayoutPanel2.Controls)
            {
                var cb = control as CheckBox;
                if (cb != null && cb.Checked)
                {
                    var scr = cb.Tag as Screen;
                    EnabledScreens.Add(scr.DeviceName);
                }
            }
            return EnabledScreens;
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
            //overlayWindow.TopMost = true; // overlay window is set to top most
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

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (var window in overlayWindows)
            {
                var screen = window.Value.Tag as Screen;
                window.Value.Top = screen.Bounds.Top;
                window.Value.Left = screen.Bounds.Left;
                window.Value.Width = screen.Bounds.Width;
                window.Value.Height = screen.Bounds.Height;
            }
        }
    }
}
