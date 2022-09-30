using PluginSdk;
using PluginSdk.Control;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Default.View
{
    /// <summary>
    /// Gallery.xaml 的交互逻辑
    /// </summary>
    public partial class Gallery : WidgetControl
    {
        public override cardInfo ci => Cards.Gallery;
        public Gallery(Guid g)
        {
            InitializeComponent();
            PluginGuid = g;
        }

        public override void OnEnabled()
        {
            //throw new NotImplementedException();
        }

        public override void OnDisabled()
        {
            //throw new NotImplementedException();
        }

        private void SetCardSize(object sender, RoutedEventArgs e)
        {
            var mi = sender as MenuItem;
            string size = mi.Header as string;
            var s = size.Split('×');
            if (s.Length==2)
            {
                ResizeCard(int.Parse(s[0]) * 2, int.Parse(s[1]) * 2);
            }


        }
    }
}
