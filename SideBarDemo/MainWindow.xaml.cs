using HandyControl.Controls;
using PluginSdk;
using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using static SideBarDemo.Common.FullScreenChecker;
using Color = System.Drawing.Color;
using Window = System.Windows.Window;

namespace SideBarDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ViewModel.SideBar vm;

        DispatcherTimer t = new DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();
            t.Interval = new TimeSpan(0, 0, 1);
            t.Tick += (a, b) =>
            {
                vm.Time = DateTime.Now.ToString("HH:mm");
            };
            t.Start();
        }



        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            Height = SystemParameters.WorkArea.Height - 16;

            IntPtr hWnd = new WindowInteropHelper(GetWindow(this)).EnsureHandle();
            SetWindowLong(hWnd, (-20), 0x80);

            DockBar.Common.WindowAttribute.EnableRoundWindow(hWnd);
            new DockBar.Common.WindowAccentCompositor(this).Composite(Color.FromArgb(0xAA, 0xFF, 0xFF, 0xFF));
            vm = new ViewModel.SideBar();

            DataContext = vm;

            RegisterAppBar(false);
            var hs = PresentationSource.FromVisual(this) as HwndSource;
            hs.AddHook(new HwndSourceHook(WndProc));
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            RegisterAppBar(true);


            App.appConfig.instances = new System.Collections.Generic.Dictionary<Guid, string>();
            foreach (var item in vm.widgets)
            {
                App.appConfig.instances.Add(item.PluginGuid, item.ci.mainView.FullName);
            }

            base.OnClosing(e);
        }

        public void RegisterAppBar(bool registered)
        {
            APPBARDATA abd = new APPBARDATA();
            abd.cbSize = Marshal.SizeOf(abd);
            IntPtr hWnd = new WindowInteropHelper(GetWindow(this)).EnsureHandle();

            abd.hWnd = hWnd;
            if (!registered)
            {
                //register   
                uCallBackMsg = APIWrapper.RegisterWindowMessage("APPBARMSG_SC_HELPER");
                abd.uCallbackMessage = uCallBackMsg;
                uint ret = APIWrapper.SHAppBarMessage((int)ABMsg.ABM_NEW, ref abd);
            }
            else
            {
                APIWrapper.SHAppBarMessage((int)ABMsg.ABM_REMOVE, ref abd);
            }

        }

        bool RunningFullScreenApp = false;
        private IntPtr desktopHandle;
        private IntPtr shellHandle;
        int uCallBackMsg;
        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {

            if (msg == uCallBackMsg)
            {
                switch (wParam.ToInt32())
                {
                    case (int)ABNotify.ABN_FULLSCREENAPP:
                        {
                            IntPtr hWnd = APIWrapper.GetForegroundWindow();
                            //判断当前全屏的应用是否是桌面
                            if (hWnd.Equals(desktopHandle) || hWnd.Equals(shellHandle))
                            {
                                RunningFullScreenApp = false;
                                break;
                            }
                            //判断是否全屏
                            if ((int)lParam == 1)
                                this.RunningFullScreenApp = true;
                            else
                                this.RunningFullScreenApp = false;
                            break;
                        }
                    default:
                        break;
                }
            }


            return IntPtr.Zero;

        }

        private void root_MouseEnter(object sender, MouseEventArgs e)
        {
            //Topmost = true;
            //Activate();
            //Focus();

            if (RunningFullScreenApp)
            {
                return;
            }
            Storyboard? slide_in = FindResource("slide_in") as Storyboard;
            slide_in?.Begin();

            //SwitchToThisWindow(new WindowInteropHelper(this).EnsureHandle(),true);
            if (!IsFocused)
            {
                setForeground();
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PinWindow = !PinWindow;
            if (PinWindow)
            {
                pin_btn.Content = "\xF038";
            }
            else
            {
                pin_btn.Content = "\xF039";

            }
        }

        void setForeground()
        {
            int hForeWnd = GetForegroundWindow();
            int dwForeID = GetWindowThreadProcessId(hForeWnd, 0);
            int dwCurID = GetCurrentThreadId();
            AttachThreadInput(dwCurID, dwForeID, true);
            this.Activate();
            AttachThreadInput(dwCurID, dwForeID, false);
        }



        private bool PinWindow = false;
        private void root_Deactivated(object sender, EventArgs e)
        {
            if (PinWindow)
            {

            }
            else
            {
                Storyboard? slide_out = FindResource("slide_out") as Storyboard;
                slide_out?.Begin();

            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            trayIcon.Visibility = Visibility.Collapsed;
            App.Current.Shutdown();
        }




        #region Win32导入

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);
        [DllImport("user32.dll")]
        private static extern int GetForegroundWindow();
        [DllImport("user32.dll")]
        private static extern int GetWindowThreadProcessId(int hwnd, int lpdwProcessId);
        [DllImport("kernel32.dll")]
        private static extern int GetCurrentThreadId();
        [DllImport("user32.dll")]
        private static extern int AttachThreadInput(int idAttach, int idAttachTo, bool fAttach);


        [DllImport("user32.dll", EntryPoint = "SystemParametersInfo")]
        public static extern int SystemParametersInfo(int uAction, int uParam, IntPtr lpvParam, int fuWinIni);

        [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "SetForegroundWindow")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);//设置此窗体为活动窗体

        #endregion

        #region 拖拽排序
        internal static class Utils
        {
            //根据子元素查找父元素
            public static T FindVisualParent<T>(DependencyObject obj) where T : class
            {
                while (obj != null)
                {
                    T wc = obj as T;
                    if (wc != null)
                        return obj as T;

                    obj = VisualTreeHelper.GetParent(obj);
                }
                return null;
            }
        }
        private void LBoxSort_OnDrop(object sender, DragEventArgs e)
        {
            var pos = e.GetPosition(LBoxSort);
            var result = VisualTreeHelper.HitTest(LBoxSort, pos);
            if (result == null)
            {
                return;
            }
            //查找元数据
            var f = e.Data.GetFormats().FirstOrDefault();
            var sourcePerson = e.Data.GetData(f);
            if (sourcePerson == null)
            {
                return;
            }
            //查找目标数据
            var targetPerson = Utils.FindVisualParent<WidgetControl>(result.VisualHit);
            if (targetPerson == null)
            {
                return;
            }
            if (ReferenceEquals(targetPerson, sourcePerson))
            {
                return;
            }
            vm.widgets.Remove(sourcePerson as WidgetControl);
            vm.widgets.Insert(vm.widgets.IndexOf(targetPerson), sourcePerson as WidgetControl);


        }

        private void LBoxSort_OnPrevewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var n_pos = e.GetPosition(this);

                if (n_pos == old_Pos)
                {
                    e.Handled = false;
                    return;
                }

                var pos = e.GetPosition(LBoxSort);
                HitTestResult result = VisualTreeHelper.HitTest(LBoxSort, pos);
                //var tb=Utils.FindVisualParent<TextBox>(result.VisualHit);
                if (result == null)
                {
                    e.Handled = false;

                    return;
                }
                var listBoxItem = Utils.FindVisualParent<WidgetControl>(result.VisualHit);
                //if (listBoxItem == null || listBoxItem.Content != LBoxSort.SelectedItem)
                //{
                //    return;
                //}
                DataObject dataObj = new DataObject(listBoxItem);
                DragDrop.DoDragDrop(LBoxSort, dataObj, DragDropEffects.Move);
            }
            else
            {
                e.Handled = false;
            }
        }

        private System.Windows.Point old_Pos;
        private void LBoxSort_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            old_Pos = e.GetPosition(this);
        }

        #endregion
    }


}
