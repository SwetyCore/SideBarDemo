using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Default.View;
using HandyControl.Controls;
using PluginSdk;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Default.ViewModel
{
    partial class CardTest:ObservableObject
    {


        public CardTest(Guid g)
        {

            Id = g;
        }

        [ObservableProperty]
        private Guid id;

        [RelayCommand]
        private void SendNotifyTest()
        {

            Growl.Success("测试通知！");
        }
    }
}
