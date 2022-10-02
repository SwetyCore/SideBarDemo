using PluginSdk;
using System;

namespace Default.View
{

    /// <summary>
    /// TestCard.xaml 的交互逻辑
    /// </summary>
    public partial class CountDown : WidgetControl
    {
        public override cardInfo ci => Cards.CountDown;

        public CountDown(Guid g)
        {
            InitializeComponent();

            PluginGuid = g;

            //DataContext = new ViewModel.CardTest(g);


        }




        public override void OnEnabled()
        {


        }

        public override void OnDisabled()
        {

        }
    }
}
