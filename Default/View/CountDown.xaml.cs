using PluginSdk;
using System;
using System.IO;

namespace Default.View
{

    /// <summary>
    /// TestCard.xaml 的交互逻辑
    /// </summary>
    public partial class CountDown : WidgetControl
    {
        public override cardInfo ci => Cards.CountDown;

        internal Model.CountDown.Config cfg;

        ViewModel.CountDown vm;

        public CountDown(Guid g)
        {
            InitializeComponent();

            PluginGuid = g;


        }


        public override void OnEnabled()
        {
            timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0,1);
            timer.Tick += Timer_Tick;
            timer.Start();
            vm = new ViewModel.CountDown(this);
            DataContext = vm;
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            vm.Update();
        }

        public override void OnDisabled()
        {
            timer.Stop();
            try
            {

                File.Delete(GetPluginConfigFilePath());
            }
            catch (Exception ex)
            {

            }
        }
    }
}
