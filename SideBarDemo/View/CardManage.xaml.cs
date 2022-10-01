using System.Windows.Controls;

namespace SideBarDemo.View
{
    /// <summary>
    /// CardManage.xaml 的交互逻辑
    /// </summary>
    public partial class CardManage : Page
    {
        public CardManage()
        {
            InitializeComponent();

            DataContext = new ViewModel.CardManage();
        }
    }
}
