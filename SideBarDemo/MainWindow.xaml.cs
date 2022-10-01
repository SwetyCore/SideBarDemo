﻿using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media.Animation;
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
            Height = SystemParameters.WorkArea.Height;

            IntPtr hWnd = new WindowInteropHelper(GetWindow(this)).EnsureHandle();
            SetWindowLong(hWnd, (-20), 0x80);

            vm = new ViewModel.SideBar();

            DataContext = vm;
        }

        private void root_MouseEnter(object sender, MouseEventArgs e)
        {
            Activate();
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
    }


}
