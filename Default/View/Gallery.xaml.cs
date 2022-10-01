using PluginSdk;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Default.View
{
    /// <summary>
    /// Gallery.xaml 的交互逻辑
    /// </summary>
    public partial class Gallery : WidgetControl
    {

        public override cardInfo ci => Cards.Gallery;
        ViewModel.Gallery vm;
        public Gallery(Guid g)
        {
            InitializeComponent();
            PluginGuid = g;
        }

        public override void OnEnabled()
        {


            vm = new ViewModel.Gallery(this);
            vm.IsActive = true;
            DataContext = vm;

            ResizeCard(vm.cfg.size.Width, vm.cfg.size.Height);

            vm.InitFolder();



            //throw new NotImplementedException();
        }

        public override void OnDisabled()
        {
            vm.IsActive = false;

            //throw new NotImplementedException();
        }

        private void SetCardSize(object sender, RoutedEventArgs e)
        {
            var mi = sender as MenuItem;
            string size = mi.Header as string;
            var s = size.Split('×');
            if (s.Length == 2)
            {
                ResizeCard(int.Parse(s[0]) * 2, int.Parse(s[1]) * 2);
            }
            vm.cfg.size = new System.Drawing.Size(int.Parse(s[0]) * 2, int.Parse(s[1]) * 2);

        }
    }
}
