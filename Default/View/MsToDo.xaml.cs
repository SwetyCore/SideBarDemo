using HandyControl.Controls;
using PluginSdk;
using System;
using System.Windows.Controls;

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
            try
            {
                var id = ((Microsoft.Graph.Entity)((object[])e.AddedItems)[0]).Id.ToString();
                if (id==null)
                {
                    return;
                }
                vm.selectedTaskListId = id;

                vm.GetTasksAsync(vm.selectedTaskListId);
            }
            catch (Exception ex)
            {
                Growl.Error(ex.Message);
            }
        }
    }
}
