using Newtonsoft.Json;
using PluginSdk;
using PluginSdk.Message;
using SideBarDemo.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using MessageBox = System.Windows.MessageBox;

namespace SideBarDemo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IEnumerable<IPlugin> Plugins = new ObservableCollection<IPlugin>();

        public static ObservableCollection<cardInfo> cis = new ObservableCollection<cardInfo>();


        public static ObservableCollection<WidgetControl> widgets = new ObservableCollection<WidgetControl>();


        public static Model.AppConfig? appConfig;

        const string CONFIG_FILE = "config.json";
        protected override void OnStartup(StartupEventArgs e)
        {

            if (File.Exists(CONFIG_FILE))
            {
                appConfig = JsonConvert.DeserializeObject<Model.AppConfig>(File.ReadAllText(CONFIG_FILE)) ?? new Model.AppConfig();
            }
            else
            {
                appConfig = new Model.AppConfig();
            }


            Application.Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;

            if (!Directory.Exists("Plugins"))
            {
                Directory.CreateDirectory("Plugins");
            }
            try
            {
                Plugins = new PluginLoader().Load();

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            widgets = new ObservableCollection<WidgetControl>();

            if (Plugins != null)
            {
                foreach (var item in Plugins)
                {

                    foreach (var c in item.cards)
                    {
                        //var a = Activator.CreateInstance(c.mainView);
                        cis.Add(c);
                    }
                }
            }

            foreach (var item in appConfig.instances)
            {
                foreach (var ci in cis)
                {
                    //插件标识
                    var wid = ci.mainView.FullName;


                    if (item.Value == wid)
                    {
                        var wc = Activator.CreateInstance(ci.mainView, item.Key) as WidgetControl;
                        widgets.Add(wc);
                        wc.OnEnabled();
                    }
                }
            }

            base.OnStartup(e);

        }

        protected override void OnExit(ExitEventArgs e)
        {
            Messages.SendOnExitMsg();

            base.OnExit(e);

            File.WriteAllText(CONFIG_FILE, JsonConvert.SerializeObject(appConfig));


        }
        void Current_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {

            MessageBox.Show("我们很抱歉，当前应用程序遇到一些问题，该操作已经终止，请进行重试，如果问题继续存在，请联系管理员.", "意外的操作", MessageBoxButton.OK, MessageBoxImage.Information);//这里通常需要给用户一些较为友好的提示，并且后续可能的操作

            File.WriteAllText("err.log", JsonConvert.SerializeObject(e.Exception));
            //Growl.Error(e.Exception.Message);
            //Environment.Exit(-1);
            e.Handled = true;
        }
    }
}
