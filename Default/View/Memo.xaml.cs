using PluginSdk;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Default.View
{

    /// <summary>
    /// TestCard.xaml 的交互逻辑
    /// </summary>
    public partial class Memo : WidgetControl
    {
        public override cardInfo ci => Cards.Memo;

        private ViewModel.Memo vm;
        public Memo(Guid g)
        {
            InitializeComponent();

            PluginGuid = g;



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

        public override void OnEnabled()
        {
            vm = new ViewModel.Memo(this);
            DataContext = vm;
            ResizeCard(vm.cfg.size.Width, vm.cfg.size.Height);


        }

        public override void OnDisabled()
        {

        }
    }
}
