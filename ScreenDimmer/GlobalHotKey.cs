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
    internal class GlobalHotKey
    {
        private KeyModifiers modifiers;
        private Keys key;
        public string Description { get; internal set; }
        public int Id { get; internal set; }
        
        private IntPtr hWnd;
        public bool IsRegistered { get; internal set; }

        public GlobalHotKey()
        {
        }

        /// <summary>
        /// Create new hotkey. A hotkey need to be registered before taking effect.
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="modifiers"></param>
        /// <param name="key"></param>
        /// <param name="doNotRegisterNow"></param>
        public GlobalHotKey(KeyModifiers modifiers, Keys key, string description)
        {
            this.modifiers = modifiers;
            this.key = key;
            this.Description = description;
            Id = GetHashCode();
            IsRegistered = false;
        }

        /// <summary>
        /// Set the key combination and re-register it if it is already registered.
        /// Unregistered (i.e. !IsRegistered) hotkey need to be registered manually.
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
            this.Id = GetHashCode();
        }

        public void SetDescription(string description)
        {
            Description = description;
        }

        public void Register(IntPtr hWnd)
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
            this.hWnd = hWnd; // only update the IntPtr if successfully regitered.
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

        public override int GetHashCode()
        {
            return (int) modifiers ^ (int)key; //^ hWnd.ToInt32();
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
