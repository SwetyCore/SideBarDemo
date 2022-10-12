using HandyControl.Controls;
using PluginSdk;
using System;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
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
        public MainWindow()
        {
            InitializeComponent();


        }
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            Height = SystemParameters.WorkArea.Height-16;

            IntPtr hWnd = new WindowInteropHelper(GetWindow(this)).EnsureHandle();
            SetWindowLong(hWnd, (-20), 0x80);

            DockBar.Common.WindowAttribute.EnableRoundWindow(hWnd);
            new DockBar.Common.WindowAccentCompositor(this).Composite(Color.FromArgb(0xAA, 0xFF, 0xFF, 0xFF));
            vm = new ViewModel.SideBar();

            DataContext = vm;
        }

        private void root_MouseEnter(object sender, MouseEventArgs e)
        {
            //Topmost = true;
            //Activate();
            //Focus();
            Storyboard? slide_in = FindResource("slide_in") as Storyboard;
            slide_in?.Begin();

            //SwitchToThisWindow(new WindowInteropHelper(this).EnsureHandle(),true);

            Activate();
        }
        [DllImport("user32")]         
        static extern void SwitchToThisWindow(IntPtr hWnd, bool fAltTab);
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






        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);


        internal static class Utils
        {
            //根据子元素查找父元素
            public static T FindVisualParent<T>(DependencyObject obj) where T : class
            {
                while (obj != null)
                {
                    T wc = obj as T;
                    if (wc!=null)
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
            vm.widgets.Insert(LBoxSort.Items.IndexOf(targetPerson), sourcePerson as WidgetControl);
            
        
        }

        private void LBoxSort_OnPrevewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var n_pos = e.GetPosition(this);

                if (n_pos==old_Pos)
                {
                    e.Handled = false;
                    return;
                }

                var pos = e.GetPosition(LBoxSort);
                HitTestResult result = VisualTreeHelper.HitTest(LBoxSort, pos);
                if (result == null)
                {
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
    }


}
