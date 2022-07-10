using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Augustine.ScreenDimmer
{
    [DataContract]
    internal class Configuration
    {
        [DataMember(Name = "AllowZeroBrightness")]
        internal bool IsZeroBrightness;
        [DataMember(Name = "EnforceOnTop")]
        internal bool IsEnforceOnTop;
        [DataMember(Name = "DebuggingMode")]
        internal bool IsDebug;
        [DataMember(Name = "AllowTransition")]
        internal bool IsTransition;
        /// <summary>
        /// Range: 0-100
        /// </summary>
        [DataMember(Name = "CurrentBrightness")]
        internal byte CurrentBrightness;
        [DataMember(Name = "MinimumNonzeroBrightness")]
        internal readonly byte MinimumNonZeroBrightness = 20;
        [DataMember(Name = "EnforcingPeriod")]
        internal int EnforcingPeriod;
        [DataMember(Name = "TransitionDuration")]
        internal double FadeDuration;
        [DataMember(Name = "Color")]
        internal Color DimColor = Color.Black;

        [DataMember(Name = "HotkeyDim")]
        private string hotKeyDim {
            get { return HotKeyDim.ToString(); }
            set { HotKeyDim = GlobalHotkeyParser.Parse(value); } }
        internal GlobalHotKey HotKeyDim;
        
        [DataMember(Name = "HotkeyBrighten")]
        private string hotKeyBright {
            get { return HotKeyBrighten.ToString(); }
            set { HotKeyBrighten = GlobalHotkeyParser.Parse(value); } }
        internal GlobalHotKey HotKeyBrighten;

        [DataMember(Name = "HotkeyDecreaseBrightness")]
        private string hotKeyDecreaseBrightness {
            get { return HotKeyDecreaseBrightness.ToString(); }
            set { HotKeyDecreaseBrightness = GlobalHotkeyParser.Parse(value); } }
        internal GlobalHotKey HotKeyDecreaseBrightness;

        [DataMember(Name = "HotkeyIncreaseBrightness")]
        private string hotKeyIncreaseBrightness {
            get { return HotKeyIncreaseBrightness.ToString(); }
            set { HotKeyIncreaseBrightness = GlobalHotkeyParser.Parse(value); } }
        internal GlobalHotKey HotKeyIncreaseBrightness;

        [DataMember(Name = "HotkeyForceOnTop")]
        private string hotKeyForceOnTop {
            get { return HotKeyForceOnTop.ToString(); } 
            set { HotKeyForceOnTop = GlobalHotkeyParser.Parse(value); } }
        internal GlobalHotKey HotKeyForceOnTop;
        
        [DataMember(Name = "HotkeyHalt")]
        private string hotKeyHalt {
            get { return HotKeyHalt.ToString(); }
            set { HotKeyHalt = GlobalHotkeyParser.Parse(value); } }
        internal GlobalHotKey HotKeyHalt;

        [DataMember(Name = "EnabledScreens")]
        internal List<string> EnabledScreens;

        internal Configuration()
        {
            HotKeyDim = new GlobalHotKey();
            HotKeyBrighten = new GlobalHotKey();
            HotKeyIncreaseBrightness = new GlobalHotKey();
            HotKeyDecreaseBrightness = new GlobalHotKey();
            HotKeyForceOnTop = new GlobalHotKey();
            HotKeyHalt = new GlobalHotKey();
        }

        private void setHotkeyDescriptions()
        {
            HotKeyDim.SetDescription("Dim (minimum brightness)");
            HotKeyBrighten.SetDescription("Brighten (maximum brightness)");
            HotKeyIncreaseBrightness.SetDescription("Increase Brightness");
            HotKeyDecreaseBrightness.SetDescription("Decrease Brightness");
            HotKeyForceOnTop.SetDescription("Force on Top");
            HotKeyHalt.SetDescription("Immediately Halt Program");
        }

        internal void LoadDefault()
        {
            CurrentBrightness = 70;
            EnabledScreens = Screen.AllScreens.Select(o => o.DeviceName).ToList();
            IsZeroBrightness = true;
            IsEnforceOnTop = false;
            IsDebug = false;
            IsTransition = true;
            EnforcingPeriod = 30;
            FadeDuration = 250;
            HotKeyDim.SetKey(KeyModifiers.MOD_WIN | KeyModifiers.MOD_CONTROL, System.Windows.Forms.Keys.Left);
            HotKeyBrighten.SetKey(KeyModifiers.MOD_WIN | KeyModifiers.MOD_CONTROL, System.Windows.Forms.Keys.Right);
            HotKeyDecreaseBrightness.SetKey(KeyModifiers.MOD_WIN | KeyModifiers.MOD_CONTROL, System.Windows.Forms.Keys.Down);
            HotKeyIncreaseBrightness.SetKey(KeyModifiers.MOD_WIN | KeyModifiers.MOD_CONTROL, System.Windows.Forms.Keys.Up);
            HotKeyForceOnTop.SetKey(KeyModifiers.MOD_WIN | KeyModifiers.MOD_CONTROL, System.Windows.Forms.Keys.T);
            HotKeyHalt.SetKey(KeyModifiers.MOD_WIN | KeyModifiers.MOD_CONTROL, System.Windows.Forms.Keys.H);
            setHotkeyDescriptions();
        }

        internal static Configuration LoadFromFile(string fileName)
        {
            Configuration deserialized;
            try
            {
                deserialized = Serializer.DeSerializeObject<Configuration>(fileName);
            }
            catch (Exception ex)
            {
                throw new Exception("Cannot parse configuration from file.", ex);
            }
            deserialized.setHotkeyDescriptions();
            // For the purpose of robustness. Non-positive fade duration will cause trouble.
            if (deserialized.FadeDuration < 1)
            {
                deserialized.FadeDuration = 250;
            }
            return deserialized;
            //this.CurrentBrightness = deserialized.CurrentBrightness;
            //this.DimColor = deserialized.DimColor;
            //this.EnforcingPeriod = deserialized.EnforcingPeriod;
            //this.IsDebug = deserialized.IsDebug;
            //this.IsEnforceOnTop = deserialized.IsEnforceOnTop;
            //this.IsTransition = deserialized.IsTransition;
            //this.IsZeroBrightness = deserialized.IsZeroBrightness;
            ////this.MinimumNonZeroBrightness = deserialized.MinimumNonZeroBrightness;
            //this.MonitorIndex = deserialized.MonitorIndex;
            //this.HotKeyBright = deserialized.HotKeyBright;
            //this.HotKeyDecreaseBrightness = deserialized.HotKeyBright;
            //this.HotKeyDim = deserialized.HotKeyDim;
            //this.HotKeyForceOnTop = deserialized.HotKeyForceOnTop;
            //this.HotKeyHalt = deserialized.HotKeyHalt;
            //this.HotKeyIncreaseBrightness = deserialized.HotKeyIncreaseBrightness;
        }

        internal void SaveToFile(string fileName)
        {
            try
            {
                Serializer.SerializeObject<Configuration>(this, fileName);
            }
            catch (Exception ex)
            {
                throw new Exception("Cannot save current configuration to file.", ex);
            }
        }

    }
}
