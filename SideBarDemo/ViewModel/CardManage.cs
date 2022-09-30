using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PluginSdk;
using SideBarDemo.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SideBarDemo.ViewModel
{
    partial class CardManage:ObservableObject
    {



		public ObservableCollection<WidgetControl> widgets
		{
			get { return App.widgets; }
			set { SetProperty(ref App.widgets , value); }
		}

		public ObservableCollection<cardInfo> CIS
		{
            get { return App.cis; }
            set { SetProperty(ref App.cis, value); }
        }

		[ObservableProperty]
		private cardInfo selected_CI;


		[RelayCommand]
		private void RemoveCard(object c)
		{
			var wc = c as WidgetControl;
			wc.OnDisabled();
			widgets.Remove(wc);
            App.appConfig.instances.Remove(wc.PluginGuid);

        }

        [RelayCommand]
        private void AddCard()
        {
			var w = Selected_CI;



            if (w != null)
			{
				var g = System.Guid.NewGuid();
                var wid = w.mainView.FullName;
				WidgetControl? wc = (WidgetControl)Activator.CreateInstance(w.mainView,g );
				wc?.OnEnabled();
                widgets.Add(wc);
				App.appConfig?.instances.Add(g,wid);
			}
        }

    }
}
