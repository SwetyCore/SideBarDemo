using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HandyControl.Controls;
using PluginSdk;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using SideBarDemo.View;

namespace SideBarDemo.ViewModel
{
    partial class SideBar : ObservableObject
    {

        public ObservableCollection<WidgetControl> widgets
        {
            get { return App.widgets; }
            set { SetProperty(ref App.widgets, value); }
        }

        public SideBar()
        {
            //widgets.Add(new View.TestCard(System.Guid.NewGuid()));
            //widgets.Add(new View.TestCard(System.Guid.NewGuid()));
            //widgets.Add(new View.TestCard(System.Guid.NewGuid()));

        }


        [RelayCommand]
        private void OpenSetting()
        {
            new SettingWindow().Show();
        }


    }
}
