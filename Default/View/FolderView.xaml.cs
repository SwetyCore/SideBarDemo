using PluginSdk;
using System;

namespace Default.View
{

    /// <summary>
    /// TestCard.xaml 的交互逻辑
    /// </summary>
    public partial class FolderView : WidgetControl
    {
        public override cardInfo ci => Cards.FolderView;

        public FolderView(Guid g)
        {
            InitializeComponent();

            PluginGuid = g;

            DataContext = new ViewModel.FolderView(this);


        }




        public override void OnEnabled()
        {


        }

        public override void OnDisabled()
        {

        }
    }
}
