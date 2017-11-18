using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Augustine.ScreenDimmer
{
    /// <summary>
    /// Base on https://stackoverflow.com/questions/36379547/writing-text-to-the-system-tray-instead-of-an-icon
    /// </summary>
    class TextIcon
    {
        public static Icon CreateTextIcon(string str, Color color, string fontName = "", int size = 16)
        {
            float dpiXsf = 1;
            float dpiYsf = 1;
            using (Graphics graphics = Graphics.FromHwnd(IntPtr.Zero))
            {
                dpiXsf = graphics.DpiX / 96;
                dpiYsf = graphics.DpiY / 96;
            }
            Font fontToUse = fontName == "" ?
            new Font("Segoe UI Symbol", (float)size, FontStyle.Regular, GraphicsUnit.Pixel) :
            new Font(fontName, (float)(size*dpiXsf), FontStyle.Regular, GraphicsUnit.Pixel);
            Brush brushToUse = new SolidBrush(color);
            Bitmap bitmapText = new Bitmap((int)(size * dpiXsf), (int)(size * dpiYsf));
            Graphics g = System.Drawing.Graphics.FromImage(bitmapText);

            IntPtr hIcon;

            g.Clear(Color.Transparent);
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;
            g.DrawString(str, fontToUse, brushToUse, -4, -2);
            hIcon = (bitmapText.GetHicon());
            return System.Drawing.Icon.FromHandle(hIcon);
        }
    }
}
