using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace HtmlToPdfSample
{
    /// <summary>
    /// This is the only reliable way I've discovered to get the *actual* 
    /// scaling factor from windows. It's a shame it requires native interop
    /// but that's just how it is.
    /// </summary>
    public class NativeDeviceHelper
    {
        [DllImport("gdi32.dll")]
        static extern int GetDeviceCaps(IntPtr hdc, int nIndex);
        public enum DeviceCap
        {
            VERTRES = 10,
            DESKTOPVERTRES = 117,
            // http://pinvoke.net/default.aspx/gdi32/GetDeviceCaps.html
        }

        public static float GetScalingFactor()
        {
            var g = Graphics.FromHwnd(IntPtr.Zero);
            var desktop = g.GetHdc();
            var LogicalScreenHeight = GetDeviceCaps(desktop, (int)DeviceCap.VERTRES);
            var PhysicalScreenHeight = GetDeviceCaps(desktop, (int)DeviceCap.DESKTOPVERTRES);

            var ScreenScalingFactor = (float)PhysicalScreenHeight / (float)LogicalScreenHeight;

            return ScreenScalingFactor; // 1.25 = 125%
        }
    }
}