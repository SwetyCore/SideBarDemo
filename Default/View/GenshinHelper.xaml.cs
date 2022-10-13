using Default.CWindow;
using DGP.Genshin.GamebarWidget.MiHoYoAPI;
using DGP.Genshin.GamebarWidget.Model;
using HandyControl.Controls;
using Microsoft.IdentityModel.Tokens;
using PluginSdk;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Default.View
{

    /// <summary>
    /// TestCard.xaml 的交互逻辑
    /// </summary>
    public partial class GenshinHelper : WidgetControl
    {
        public override cardInfo ci => Cards.GenshinHelper;
        internal Model.GenshinHelper.Config cfg;
        DispatcherTimer timer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 30, 0) };
        ViewModel.GenshinHelper vm;
        public GenshinHelper(Guid g)
        {
            InitializeComponent();

            PluginGuid = g;


        }

        public async void DataUpdate(bool tip = true)
        {
            if (string.IsNullOrEmpty(cfg.cookie))
            {
                return;
            }
            else
            {
                RefreshDailyNotePoolAsync(cfg.cookie);

            }
        }
        public async Task RefreshDailyNotePoolAsync(string mycookie)
        {

            if (mycookie == "" | mycookie == null)
            {
                Growl.Info("未设置cookie!");
                vm.Loading = true;

                return;
            }

            Cookie cookie = new Cookie(mycookie);


            List<UserGameRole> roles = await new UserGameRoleProvider(cookie.CookieValue).GetUserGameRolesAsync();
            //roleAndNotes.Clear();
            if (roles.Count < 1)
            {
                Growl.Error("无效的Cookie或者无网络!");
                vm.Loading = true;
                return;
            }
            foreach (UserGameRole role in roles)
            {
                DailyNote note = await new DailyNoteProvider(cookie.CookieValue).GetDailyNoteAsync(role.Region, role.GameUid);
                //roleAndNotes.Add(new RoleAndNote { Role = role, Note = note });
                vm.RoleAndNote = new RoleAndNote { Role = role, Note = note };
                vm.Loading = false;
            }


        }
        public override void OnEnabled()
        {
            vm = new ViewModel.GenshinHelper(this);
            DataContext = vm;


            vm.Loading = true;
            timer.Tick += (object? sender, EventArgs e) =>
            {
                DataUpdate();
            };


            DataUpdate();

            timer.Start();
        }

        public override void OnDisabled()
        {
            timer.Stop();



        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var dl = new CookieGetter("https://bbs.mihoyo.com/ys/");
            dl.ShowDialog();
            cfg.cookie = dl.Cookie;
            DataUpdate(true);
        }
    }
}
