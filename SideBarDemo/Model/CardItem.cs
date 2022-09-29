using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginSdk;
namespace SideBarDemo.Model
{
    public class CardItem
    {
        public WidgetControl card { get; set; }

        public Guid guid { get; set; }
    }
}
