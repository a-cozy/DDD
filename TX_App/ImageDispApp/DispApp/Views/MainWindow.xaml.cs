using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace DispApp.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        //using System.Runtime.InteropServices;
        const double fixedRate = (double)1024 / 737;
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            IntPtr handle = (new WindowInteropHelper(this)).Handle;
            HwndSource hwndSource =
                            (HwndSource)HwndSource.FromVisual(this);
            hwndSource.AddHook(WndHookProc);
        }

        const int WM_SIZING = 0x214;
        const int WMSZ_LEFT = 1;
        const int WMSZ_RIGHT = 2;
        const int WMSZ_TOP = 3;
        const int WMSZ_TOPLEFT = 4;
        const int WMSZ_TOPRIGHT = 5;
        const int WMSZ_BOTTOM = 6;
        const int WMSZ_BOTTOMLEFT = 7;
        const int WMSZ_BOTTOMRIGHT = 8;


        private IntPtr WndHookProc(
            IntPtr hwnd, int msg, IntPtr wParam,
            IntPtr lParam, ref bool handled)
        {
            if (msg == WM_SIZING)
            {
                RECT r = (RECT)Marshal.PtrToStructure(
                                          lParam, typeof(RECT));
                RECT recCopy = r;
                int w = r.right - r.left;
                int h = r.bottom - r.top;
                int dw;
                int dh;
                dw = (int)(h * fixedRate + 0.5) - w;
                dh = (int)(w / fixedRate + 0.5) - h;

                switch (wParam.ToInt32())
                {
                    case WMSZ_TOP:
                    case WMSZ_BOTTOM:
                        r.right += dw;
                        break;
                    case WMSZ_LEFT:
                    case WMSZ_RIGHT:
                        r.bottom += dh;
                        break;
                    case WMSZ_TOPLEFT:
                        if (dw > 0) r.left -= dw;
                        else r.top -= dh;
                        break;
                    case WMSZ_TOPRIGHT:
                        if (dw > 0) r.right += dw;
                        else r.top -= dh;
                        break;
                    case WMSZ_BOTTOMLEFT:
                        if (dw > 0) r.left -= dw;
                        else r.bottom += dh;
                        break;
                    case WMSZ_BOTTOMRIGHT:
                        if (dw > 0) r.right += dw;
                        else r.bottom += dh;
                        break;
                }
                Marshal.StructureToPtr(r, lParam, false);
            }
            return IntPtr.Zero;
        }
    }
}
