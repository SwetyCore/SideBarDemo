using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media;

namespace PluginSdk
{
    public abstract class WidgetControl : UserControl
    {
        public float w_width { get; set; }
        public float w_height { get; set; }

        public Guid PluginGuid { get; set; }

        public abstract void OnEnabled();

        public abstract void OnDisabled();

        public abstract cardInfo ci { get; }

        public string GetPluginConfigFilePath()
        {
            var abl = Path.GetDirectoryName(System.Reflection.Assembly.GetCallingAssembly().Location);
            return Path.Combine(abl, $"{PluginGuid}.json");
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
        public string url { get;  }
        public string author { get;  }
        public List<cardInfo> cards { get; }

    }
}
