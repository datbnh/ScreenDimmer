using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Augustine.ScreenDimmer
{
    internal class Configuration
    {
        private IntPtr hWnd;
        /// <summary>
        /// Range: 0-100
        /// </summary>
        internal byte CurrentBrightness;
        internal readonly byte MinimimNonZeroBrightness = 20;
        internal bool IsZeroBrightness;
        internal bool IsEnforceOnTop;
        internal bool IsDebug;
        internal bool IsTransition;
        internal int EnforcingPeriod;
        internal Color DimColor = Color.Black;
        internal GlobalHotKey HotKeyForceOnTop;
        internal GlobalHotKey HotKeyHalt;
        internal GlobalHotKey HotKeyDim;
        internal GlobalHotKey HotKeyBright;
        internal GlobalHotKey HotKeyIncreaseBrightness;
        internal GlobalHotKey HotKeyDecreaseBrightness;

        internal Configuration(IntPtr hWnd)
        {
            this.hWnd = hWnd;
            HotKeyForceOnTop = new GlobalHotKey(hWnd, KeyModifiers.MOD_WIN | KeyModifiers.MOD_CONTROL, System.Windows.Forms.Keys.T, "Re-enforce on Top of Taskbar", true);
            HotKeyHalt = new GlobalHotKey(hWnd, KeyModifiers.MOD_WIN | KeyModifiers.MOD_CONTROL, System.Windows.Forms.Keys.H, "Immediately Halt Program", true);
            HotKeyDim = new GlobalHotKey(hWnd, KeyModifiers.MOD_WIN | KeyModifiers.MOD_CONTROL, System.Windows.Forms.Keys.Left, "Dim (minimum brightness)", true);
            HotKeyBright = new GlobalHotKey(hWnd, KeyModifiers.MOD_WIN | KeyModifiers.MOD_CONTROL, System.Windows.Forms.Keys.Right, "Bright (maximum brightness)", true);
            HotKeyIncreaseBrightness = new GlobalHotKey(hWnd, KeyModifiers.MOD_WIN | KeyModifiers.MOD_CONTROL, System.Windows.Forms.Keys.Up, "Increase Brightness", true);
            HotKeyDecreaseBrightness = new GlobalHotKey(hWnd, KeyModifiers.MOD_WIN | KeyModifiers.MOD_CONTROL, System.Windows.Forms.Keys.Down, "Decrease Brightness", true);
            LoadDefault();
        }

        internal void LoadDefault()
        {
            CurrentBrightness = 70;
            IsZeroBrightness = true;
            IsEnforceOnTop = false;
            IsDebug = false;
            IsTransition = true;
            EnforcingPeriod = 30;
            HotKeyForceOnTop.SetKey(KeyModifiers.MOD_WIN | KeyModifiers.MOD_CONTROL, System.Windows.Forms.Keys.T);
            HotKeyHalt.SetKey(KeyModifiers.MOD_WIN | KeyModifiers.MOD_CONTROL, System.Windows.Forms.Keys.H);
            HotKeyDim.SetKey(KeyModifiers.MOD_WIN | KeyModifiers.MOD_CONTROL, System.Windows.Forms.Keys.Left);
            HotKeyBright.SetKey(KeyModifiers.MOD_WIN | KeyModifiers.MOD_CONTROL, System.Windows.Forms.Keys.Right);
            HotKeyIncreaseBrightness.SetKey(KeyModifiers.MOD_WIN | KeyModifiers.MOD_CONTROL, System.Windows.Forms.Keys.Up);
            HotKeyDecreaseBrightness.SetKey(KeyModifiers.MOD_WIN | KeyModifiers.MOD_CONTROL, System.Windows.Forms.Keys.Down);
        }
    }
}
