using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
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

namespace AppBarWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IntPtr _oldWndProc;
        private IntPtr thisHandle;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CreateAppBar_Click(object sender, RoutedEventArgs e)
        {
            AppBarFunctions.SetAppBar(this, ABEdge.Top);
            thisHandle = new WindowInteropHelper(this).Handle;
            CheckAppBar();
        }

        private void CheckAppBar()
        {
            const int GWL_EXSTYLE = -20;
            const int WS_EX_APPWINDOW = 0x00040000;

            IntPtr exStyle = AppBarFunctions.GetWindowLongPtr(thisHandle, GWL_EXSTYLE);

            if ((exStyle.ToInt64() & WS_EX_APPWINDOW) != 0)
            {
                Debug.WriteLine("AppBar: true");
            }
            else
            {
                Debug.WriteLine("AppBar: false");
            }
        }

        private void DeregAppBar_Click(object sender, RoutedEventArgs e)
        {
            AppBarFunctions.SetAppBar(this, ABEdge.None);
            CheckAppBar();
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            HwndSource source = PresentationSource.FromVisual(this) as HwndSource;
            source.AddHook(WndProc);
        }

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            Debug.WriteLine("----------------");
            Debug.WriteLine((WindowMessage.WM_CODE)msg);
            Debug.WriteLine("wParam: " + wParam);
            Debug.WriteLine("lParam: " + lParam);

            return IntPtr.Zero;
        }
    }
}
