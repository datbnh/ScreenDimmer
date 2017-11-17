using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Augustine.ScreenDimmer
{
    /// <summary>
    /// Base on http://www.dreamincode.net/forums/topic/180436-global-hotkeys/
    /// </summary>
    [DataContract]
    internal class GlobalHotKey
    {
        private IntPtr hWnd;
        private KeyModifiers modifiers;
        private Keys key;
        public string Description { get; internal set; }
        public bool IsRegistered { get; internal set; }
        public int Id { get; internal set; }

        /// <summary>
        /// Create new hotkey and register it (except doNotRegisterNow is true).
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="modifiers"></param>
        /// <param name="key"></param>
        /// <param name="doNotRegisterNow"></param>
        public GlobalHotKey(IntPtr hWnd, KeyModifiers modifiers, Keys key, string description, bool doNotRegisterNow = false)
        {
            this.modifiers = modifiers;
            this.key = key;
            this.hWnd = hWnd;
            this.Description = description;
            Id = GetHashCode();
            IsRegistered = false;
            if (!doNotRegisterNow)
            {
                Register();
            }
        }

        /// <summary>
        /// Change the assigned hotkey and re-register it.
        /// </summary>
        /// <param name="modifiers"></param>
        /// <param name="key"></param>
        public void SetKey(KeyModifiers modifiers, Keys key)
        {
            if (modifiers == this.modifiers && (key == this.key))
            {
                return;
            }
            if (IsRegistered)
            {
                this.Unregister();
            }
            this.modifiers = modifiers;
            this.key = key;
        }

        public override int GetHashCode()
        {
            return (int) modifiers ^ (int)key ^ hWnd.ToInt32();
        }

        public void Register()
        {
            if (IsRegistered)
            {
                return;
            }
            if (!NativeMethods.RegisterHotKey(hWnd, Id, modifiers, key))
            {
                throw new Exception(string.Format("Cannot register hotkey [{0}].", this.ToString()), 
                    Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error()));
            }
            IsRegistered = true;
        }

        public void Unregister()
        {
            if (!IsRegistered)
            {
                return;
            }
            if (!NativeMethods.UnregisterHotKey(hWnd, Id))
            {
                throw Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error());
            }
            IsRegistered = false;
        }

        public static bool operator ==(GlobalHotKey a, GlobalHotKey b) {
            return a.GetHashCode() == b.GetHashCode();
        }
			
		public static bool operator !=(GlobalHotKey a, GlobalHotKey b) {
            return a.GetHashCode() != b.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (obj is GlobalHotKey)
            {
                return (GlobalHotKey)obj == this;
            }
            else
            {
                return false;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (modifiers.HasFlag(KeyModifiers.MOD_ALT))
            {
                sb.Append("Alt+");
            }
            if (modifiers.HasFlag(KeyModifiers.MOD_CONTROL))
            {
                sb.Append("Ctrl+");
            }
            if (modifiers.HasFlag(KeyModifiers.MOD_SHIFT))
            {
                sb.Append("Shift+");
            }
            if (modifiers.HasFlag(KeyModifiers.MOD_WIN))
            {
                sb.Append("Win+");
            }
            sb.Append(Enum.GetName(typeof(Keys), key) ?? ((int)key).ToString());
            return sb.ToString();
        }
    }
}
