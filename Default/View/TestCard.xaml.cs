using PluginSdk;
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

namespace Default.View
{

    /// <summary>
    /// TestCard.xaml 的交互逻辑
    /// </summary>
    public partial class TestCard : WidgetControl
    {
        public override cardInfo ci => Cards.Test;

        public TestCard(Guid g)
        {
            InitializeComponent();

            PluginGuid = g;

            DataContext = new ViewModel.CardTest(g);


        }

        
        

        public override void OnEnabled()
        {


        }

        public override void OnDisabled()
        {

        }
    }
}
