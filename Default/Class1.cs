using Default.View;
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
            description = "��������",
            mainView = typeof(View.AISchedule)
        };
    }





    public class Class1 : IPlugin
    {
        public Version version {get;} = new Version();
        public string url { get;  } = "";
        public string author { get; } = "";

        public List<cardInfo> cards { get; } = new List<cardInfo>()
        {
            Cards.Test,
            Cards.AISChedule,
        };




        public string name => "Ĭ�ϲ��";


        public Class1()
        {
        }
    }
}
