using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PluginSdk;
using SideBarDemo.View;
using System.Collections.ObjectModel;

namespace SideBarDemo.ViewModel
{
    partial class SideBar : ObservableObject
    {
        [ObservableProperty]
        private string time;
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
