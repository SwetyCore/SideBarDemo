using Default.CWindow;
using Flurl;
using Flurl.Http;
using HandyControl.Controls;
using PluginSdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using static Default.Model.BiliHelper;

namespace Default.View
{

    /// <summary>
    /// TestCard.xaml 的交互逻辑
    /// </summary>
    public partial class BiliHelper : WidgetControl
    {

        DispatcherTimer timer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 30, 0) };
        ViewModel.BiliHelper vm;
        public override cardInfo ci => Cards.BiliHelper;
        String Cookie = "";
        public BiliHelper(Guid g)
        {
            InitializeComponent();

            PluginGuid = g;

            vm = new ViewModel.BiliHelper(g);


        }




        public override void OnEnabled()
        {
            DataContext = vm;
            vm.Loading = true;
            timer.Start();

            DataUpdate(false);
            timer.Tick += (object? sender, EventArgs e) =>
            {
                DataUpdate();
            };

        }

        public override void OnDisabled()
        {

        }
        public static string GetMidFromCookie(string cookie)
        {
            List<string> DedeUserID = cookie.Split("; ").Where(x => x.IndexOf("DedeUserID=") != -1).ToList();
            if (DedeUserID.Count == 0)
            {
                return "";
            }
            else
            {
                return DedeUserID[0].Replace("DedeUserID=", "");
            }
        }
        /// <summary>
        /// http://api.bilibili.com/x/web-interface/card?mid=196435612&photo=true
        /// </summary>
        public async void DataUpdate(bool tip = true)
        {
            if (Cookie != null && Cookie != "" && GetMidFromCookie(Cookie) != "")
            {
                try
                {
                    string api = "http://api.bilibili.com/x/web-interface/card";
                    string url = api.SetQueryParams(new { mid = GetMidFromCookie(Cookie), photo = "true" });
                    vm.Card = await url.GetJsonAsync<web_interface_card.Root>();
                    vm.Loading = false;
                }
                catch (Exception ex)
                {
                    Growl.Error(ex.Message);
                }
            }
            else if (tip)
            {
                Growl.Error("哔哩哔哩:无效的Cookie!");
                vm.Loading = true;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var dl = new CookieGetter("https://passport.bilibili.com/login");
            dl.ShowDialog();
            Cookie = dl.Cookie;
            DataUpdate(true);
        }
    }
}
