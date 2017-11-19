using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Augustine.ScreenDimmer
{
    public partial class OsdWindow : Form
    {
        private Timer timer;

        private int intervalFadeIn;
        private int intervalDelay;
        private int intervalFadeOut;
        private byte targetOpacity; // 0 - 255
        private DateTime effectStartTime;
        private bool isDisplaying = false;

        public OsdWindow()
        {
            InitializeComponent();
            initialize();
    //        Display("Hello World", new Font("Segeo UI", 32), Color.Black, Color.White,
    //20, 20, 255, 1000, 1000, 2000);
        }

        public void Display(string text, Font font, Color backcolor, Color textcolor, int X, int Y, byte opacity,
            int fadeInInterval, int delayTime, int fadeOutInterval)
        {
            label1.Text = text;
            if (!isDisplaying)
            {
                Font = font;
                BackColor = backcolor;
                ForeColor = textcolor;
                Location = new Point(X, Y);
                targetOpacity = opacity;

                isDisplaying = true;
                intervalFadeIn = fadeInInterval;
                intervalDelay = delayTime;
                intervalFadeOut = fadeOutInterval;

                if (!Visible)
                {
                    Show();
                }

                effectStartTime = DateTime.Now;
                timer.Start();
            }
            else
            {
                ResetTimerNow();
            }
        }

        public void ResetTimerNow()
        {
            effectStartTime = DateTime.Now.AddMilliseconds(-intervalFadeIn);
        }


        private void initialize()
        {
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            ShowInTaskbar = false;
            //TopMost = true;
            NativeMethods.SetWindowLong(this.Handle, NativeMethods.GWL_EXSTYLE,
                // get current GWL_EXSTYLE
                NativeMethods.GetWindowLong(this.Handle, NativeMethods.GWL_EXSTYLE)
                // click-through. Need to be combined with see-through.
                // Need to recalled if opacity changed by setting Form.Cpacity.
                // Using SetLayeredWindowAttributes will not require to recall this.
                | (int)ExtendedWindowStyles.WS_EX_LAYERED
                // see-through
                | (int)ExtendedWindowStyles.WS_EX_TRANSPARENT
                // alt+tab invisible, need to set ShowInTaskbar to false.
                | (int)ExtendedWindowStyles.WS_EX_TOOLWINDOW
                | (int)ExtendedWindowStyles.WS_EX_NOACTIVATE);

            timer = new Timer();
            timer.Interval = (int)(1000/24); // 24 frames per second
            timer.Tick += timer_Tick;
        }

        void timer_Tick(object sender, EventArgs e)
        {
            //if (isResetTimer)
            //{
            //    effectStartTime = DateTime.Now.AddMilliseconds(intervalFadeIn);
            //    isResetTimer = false;
            //}
            int timeSpan = (int)((DateTime.Now - effectStartTime).TotalMilliseconds);
            if (timeSpan > (intervalFadeIn + intervalDelay + intervalFadeOut))
            {
                timer.Stop();
                timer.Enabled = false;
                isDisplaying = false;
                Hide();
            }
            else
            {
                byte opacity;
                if (timeSpan <= intervalFadeIn)
                {
                    opacity = (byte) (((float)timeSpan / intervalFadeIn) * targetOpacity);
                    //Console.WriteLine("{1} Fading in... {0}", opacity, timeSpan);
                }
                else if (timeSpan >= (intervalFadeIn + intervalDelay))
                {
                    int fadeOutTimespan = timeSpan - (intervalFadeIn + intervalDelay);
                    opacity = (byte)((1f - (float)fadeOutTimespan / intervalFadeOut) * targetOpacity);
                    //Console.WriteLine("{1} Fading out... {0}", opacity, timeSpan);
                }
                else
                {
                    opacity = targetOpacity;
                    //Console.WriteLine("{1} Delay... {0}", opacity, timeSpan);
                }
                TopMost = true;
                NativeMethods.SetLayeredWindowAttributes(Handle, 0, opacity,
                    (int)LayeredWindowAttributeFlags.LWA_ALPHA);
            }
        }

        protected override bool ShowWithoutActivation
        {
            get { return true; }
        }
    }
}