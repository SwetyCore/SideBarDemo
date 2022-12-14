using PluginSdk.Control;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace PluginSdk
{
    public abstract class WidgetControl : UserControl
    {
        public DispatcherTimer timer;
        public WidgetControl()
        {
            //ResourceDictionary resourceDictionary = new ResourceDictionary();
            //resourceDictionary.Source = new Uri("pack://application:,,,/Default;component/Style/index.xaml");
            //Resources.MergedDictionaries.Add(resourceDictionary);
        }

        public Guid PluginGuid { get; set; }

        public abstract void OnEnabled();

        public abstract void OnDisabled();

        public abstract cardInfo ci { get; }

        public void ResizeCard(int width_pix, int height_pix)
        {
            TilePanel.SetHeightPix(this, height_pix);
            TilePanel.SetWidthPix(this, width_pix);
        }

        public string GetPluginConfigFilePath()
        {
            var abl = Path.GetDirectoryName(System.Reflection.Assembly.GetCallingAssembly().Location);
            var ret = Path.Combine(abl, "Configs", $"{PluginGuid}.json");
            if (!Directory.Exists(Path.GetDirectoryName(ret)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(ret));
            }
            return ret;
        }


    }


    public class cardInfo
    {
        public ImageSource icon { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public Type settingPage { get; set; }

        public Type mainView { get; set; }
    }


    public interface IPlugin
    {
        public string name { get; }
        public Version version { get; }
        public string url { get; }
        public string author { get; }
        public List<cardInfo> cards { get; }

    }
}
