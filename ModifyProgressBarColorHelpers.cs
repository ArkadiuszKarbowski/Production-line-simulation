using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Projekt_3
{
    internal static class ModifyProgressBarColorHelpers
    {
        public static void SetState(this ProgressBar pBar, int state)
        {
            SendMessage(pBar.Handle, 1040, (IntPtr)state, IntPtr.Zero);
        }
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr w, IntPtr l);
    }
}