using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Interop;
using System.Runtime.InteropServices;
using System.Management;

namespace WinClocks
{
    public partial class MainWindow:Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.Loaded += OnLoad;
            this.Deactivated += OnDeactivate;

        } // MainWindow

        private void OnDeactivate(object o, EventArgs e)
        {
            this.Topmost = true;

        } // OnDeactivate

        private void OnLoad(object o, EventArgs e)
        {
            HwndSource source = HwndSource.FromHwnd( new WindowInteropHelper(this).Handle );
            source.AddHook( new HwndSourceHook(WndProc) );

            this.MouseLeftButtonDown += OnMouseDown;

            MWinAPI.SetTimer( new WindowInteropHelper(this).Handle, 10, 10, IntPtr.Zero );

            int offset = 0;
            this.Left  = 0;
            this.Top   = 0;

            for( int x = 0; x < System.Windows.Forms.Screen.AllScreens.Length; x++ )
            {
                System.Drawing.Rectangle rect = System.Windows.Forms.Screen.AllScreens[x].Bounds;
                offset += rect.Width;
            }

            this.Left = offset - this.Width;
            
        } // OnLoad

        private void OnMouseDown( object sender,MouseButtonEventArgs e )
        {
            this.DragMove();
            
        } // OnMouseDown

        private static IntPtr WndProc( IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled )
        {
            MainWindow mwin = (MainWindow)Application.Current.MainWindow;

            if( msg == MWinAPI.WM_ACTIVATE )
            {
                mwin.Topmost = true;
            }
            if( msg == MWinAPI.WM_TIMER )
            {
                string hours = DateTime.Now.TimeOfDay.Hours   < 10 ? "0" + DateTime.Now.TimeOfDay.Hours   : DateTime.Now.TimeOfDay.Hours   + "";
                string min   = DateTime.Now.TimeOfDay.Minutes < 10 ? "0" + DateTime.Now.TimeOfDay.Minutes : DateTime.Now.TimeOfDay.Minutes + "";
                string sec   = DateTime.Now.TimeOfDay.Seconds < 10 ? "0" + DateTime.Now.TimeOfDay.Seconds : DateTime.Now.TimeOfDay.Seconds + "";

                string mon = DateTime.Now.Month < 10 ? "0" + DateTime.Now.Month : DateTime.Now.Month + "";
                string day = DateTime.Now.Day   < 10 ? "0" + DateTime.Now.Day   : DateTime.Now.Day   + "";
                string yer = DateTime.Now.Year  < 10 ? "0" + DateTime.Now.Year  : DateTime.Now.Year  + "";

                mwin.lblTime.Content = string.Format( "{0}:{1}:{2}", hours, min, sec );
                mwin.lblDay .Content = DateTime.Now.DayOfWeek.ToString().ToUpper();
                mwin.lblDate.Content = string.Format("{0}.{1}.{2}", mon, day, yer);
            }
            
            return IntPtr.Zero;

        } // IntPtr WndProc

    } // class MainWindow

} // namespace WinClocks
