using PluginSdk;
using System;
using System.Collections.Generic;

namespace Default
{
    public static class Cards
    {
        public static cardInfo Test = new cardInfo()
        {
            name = "测试卡片",
            description = "测试描述",
            mainView = typeof(View.TestCard)
        };

        public static cardInfo AISChedule = new cardInfo()
        {
            name = "小爱课程表",
            description = "课程表卡片",
            mainView = typeof(View.AISchedule)
        };

        public static cardInfo BiliHelper = new cardInfo()
        {
            name = "b站卡片",
            description = "显示账号信息的卡片",
            mainView = typeof(View.BiliHelper)
        };

        public static cardInfo Gallery = new cardInfo()
        {
            name = "相册",
            description = "是相册啦",
            mainView = typeof(View.Gallery)
        };
        internal static cardInfo MsToDo = new cardInfo()
        {
            name = "Microsoft Todo",
            description = "微软待办卡片",
            mainView = typeof(View.MsToDo)
        };
        internal static cardInfo CountDown = new cardInfo()
        {
            name = "倒数日",
            description = "倒数日卡片",
            mainView = typeof(View.CountDown)
        };
        internal static cardInfo Memo = new cardInfo()
        {
            name = "便笺",
            description = "便笺啦",
            mainView = typeof(View.Memo)
        };

        internal static cardInfo FolderView = new cardInfo()
        {
            name = "文件夹",
            description = "文件夹",
            mainView = typeof(View.FolderView)
        };
    }





    public class Class1 : IPlugin
    {
        public Version version { get; } = new Version();
        public string url { get; } = "";
        public string author { get; } = "";

        public List<cardInfo> cards { get; } = new List<cardInfo>()
        {
            Cards.Test,
            Cards.AISChedule,
            Cards.BiliHelper,
            Cards.Gallery,
            Cards.MsToDo,
            Cards.CountDown,
            Cards.Memo,
            Cards.FolderView,
        };




        public string name => "默认插件";


    }
}
