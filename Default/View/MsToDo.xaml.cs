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
    public partial class MsToDo : WidgetControl
    {
        public override cardInfo ci => Cards.MsToDo;

        ViewModel.MsToDo vm;
        public MsToDo(Guid g)
        {
            InitializeComponent();

            PluginGuid = g;


        }


        public override void OnEnabled()
        {

            vm = new ViewModel.MsToDo();

            DataContext = vm;
        }

        public override void OnDisabled()
        {

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            vm.GetTasksAsync(
            ((Microsoft.Graph.Entity)((object[])e.AddedItems)[0]).Id.ToString());
        }
    }
}
