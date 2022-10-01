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
            name = "Microsoft ToDo",
            description = "΢����쿨Ƭ",
            mainView = typeof(View.MsToDo)
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
        };




        public string name => "Ĭ�ϲ��";


    }
}
