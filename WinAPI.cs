using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace WinClocks
{
    class MWinAPI
    {
        public const uint SHOWWINDOW = 0x0040;

        public const int  GWL_STYLE   = -16;
        public const int  GWL_EXSTYLE = -20;

        public const long WS_POPUP    = 0x80000000L;
        public const long WS_BORDER   = 0x00800000; //window with border
        public const long WS_DLGFRAME = 0x00400000; //window with double border but no title
        public const long WS_CAPTION  = WS_BORDER | WS_DLGFRAME; //window with a title bar 

        public const int  WM_KEYDOWN     = 0x0100;
        public const int  WM_ACTIVATE    = 0x0006;
        public const int  WM_TIMER       = 0x0113;

        public const int  WA_ACTIVE      = 1;
        public const int  WA_CLICKACTIVE = 2;
        public const int  WA_INACTIVE    = 0;


        public const int  VK_F7       = 0x76;
        public const int  VK_F8       = 0x77;

        [Flags]
        public enum ThreadAccess : int
        {
            TERMINATE            = (0x0001),
            SUSPEND_RESUME       = (0x0002),
            GET_CONTEXT          = (0x0008),
            SET_CONTEXT          = (0x0010),
            SET_INFORMATION      = (0x0020),
            QUERY_INFORMATION    = (0x0040),
            SET_THREAD_TOKEN     = (0x0080),
            IMPERSONATE          = (0x0100),
            DIRECT_IMPERSONATION = (0x0200)
        }

        [DllImport( "user32.dll" )]
        internal static extern bool MoveWindow( IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint );

        [DllImport("user32.dll")]
        public static extern int SetWindowLong( IntPtr hWnd, int nIndex, long dwNewLong );

        [DllImport("user32.dll")]
        public static extern long GetWindowLong( IntPtr hWnd, int nIndex );

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenThread( MWinAPI.ThreadAccess dwDesiredAccess, bool bInheritHandle, uint dwThreadId );

        [DllImport("kernel32.dll")]
        public static extern uint SuspendThread( IntPtr hThread );

        [DllImport("kernel32.dll")]
        public static extern int ResumeThread( IntPtr hThread );

        [DllImport("user32.dll")]
        public static extern void ClipCursor( IntPtr rect );

        [DllImport("user32.dll")]
        public static extern bool GetWindowRect( IntPtr hWnd, out IntPtr rect );

        //System.Drawing.Rectangle r = new System.Drawing.Rectangle(10, 10, 500, 500);
        //ClipCursor(ref r);
        //ClipCursor(IntPtr.Zero);\
        [DllImport("user32.dll")]
        public static extern uint SetTimer( IntPtr hWnd, uint iTimerId, uint timeout, IntPtr func );

    } // class MWinAPI

} // namespace WinClocks
