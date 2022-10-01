using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HandyControl.Controls;
using System;

namespace Default.ViewModel
{
    partial class CardTest : ObservableObject
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
