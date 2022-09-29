using HandyControl.Controls;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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

            vm=new ViewModel.SideBar();
            
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

        private bool PinWindow=false;
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
    }


}
