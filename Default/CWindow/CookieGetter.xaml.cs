﻿using Microsoft.Web.WebView2.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Default.CWindow
{
    /// <summary>
    /// CookieGette.xaml 的交互逻辑
    /// </summary>
    public partial class CookieGetter : Window
    {
        string UserDataFolder;
        public CookieGetter(string url)
        {
            InitializeComponent();
            UserDataFolder = "C:\\MyAppUserDataFolder";
            var _task = CoreWebView2Environment.CreateAsync(null,
                                                        UserDataFolder);

            wv2.Source = new Uri(url);
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
        }
        public string Cookie { get; set; }
        private void GetCookieBtn_Click(object sender, RoutedEventArgs e)
        {
            GetCookie();
        }

        public async Task GetCookie()
        {
            Cookie = await wv2.ExecuteScriptAsync("document.cookie");
            Close();
        }

        private void wv2_Initialized(object sender, EventArgs e)
        {

        }
    }
}
