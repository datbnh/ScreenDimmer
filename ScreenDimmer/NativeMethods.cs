using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Augustine.ScreenDimmer
{
    internal static class NativeMethods
    {
        #region "User32.dll"
        /// <summary>
        /// Places the window above all non-topmost windows. The window maintains its topmost position even when it is deactivated.
        /// (SetWindowPos() parameter)
        /// </summary>
        public static IntPtr HWND_TOPMOST = new IntPtr(-1);

        /// <summary>
        /// Places the window at the top of the Z order.
        /// (SetWindowPos() parameter)
        /// </summary>
        public static IntPtr HWND_TOP = new IntPtr(0);

        /// <summary>
        /// Sets a new extended window style.
        /// (SetWindowLong() parameter)
        /// </summary>
        public const int GWL_EXSTYLE = -20;
        /// <summary>
        /// Sets a new window style.
        /// (SetWindowLong() parameter)
        /// </summary>
        public const int GWL_STYLE = -16;

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, int flags);

        [DllImport("user32.dll", EntryPoint = "GetWindowLong")]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", EntryPoint = "SetLayeredWindowAttributes")]
        public static extern bool SetLayeredWindowAttributes(IntPtr hWnd, int crKey, byte alpha, int dwFlags);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, KeyModifiers modifiers, Keys key);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        #endregion
    }
}
