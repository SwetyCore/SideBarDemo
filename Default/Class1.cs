using PluginSdk;
using System;
using System.Collections.Generic;

namespace Default
{
    public static class Cards
    {
        public static cardInfo Test = new cardInfo()
        {
            name = "���Կ�Ƭ",
            description = "��������",
            mainView = typeof(View.TestCard)
        };

        public static cardInfo AISChedule = new cardInfo()
        {
            name = "С���γ̱�",
            description = "�γ̱�Ƭ",
            mainView = typeof(View.AISchedule)
        };

        public static cardInfo BiliHelper = new cardInfo()
        {
            name = "bվ��Ƭ",
            description = "��ʾ�˺���Ϣ�Ŀ�Ƭ",
            mainView = typeof(View.BiliHelper)
        };

        public static cardInfo Gallery = new cardInfo()
        {
            name = "���",
            description = "�������",
            mainView = typeof(View.Gallery)
        };
        internal static cardInfo MsToDo = new cardInfo()
        {
            name = "Microsoft Todo",
            description = "΢����쿨Ƭ",
            mainView = typeof(View.MsToDo)
        };
        internal static cardInfo CountDown = new cardInfo()
        {
            name = "������",
            description = "�����տ�Ƭ",
            mainView = typeof(View.CountDown)
        };
        internal static cardInfo Memo = new cardInfo()
        {
            name = "���",
            description = "�����",
            mainView = typeof(View.Memo)
        };

        internal static cardInfo FolderView = new cardInfo()
        {
            name = "�ļ���",
            description = "�ļ���",
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




        public string name => "Ĭ�ϲ��";


    }
}
