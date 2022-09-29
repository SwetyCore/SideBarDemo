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
            name = "≤‚ ‘ø®∆¨",
            description = "≤‚ ‘√Ë ˆ",
            mainView = typeof(View.TestCard)
        };

        public static cardInfo AISChedule = new cardInfo()
        {
            name = "–°∞ÆøŒ≥Ã±Ì",
            description = "≤‚ ‘√Ë ˆ",
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




        public string name => "ƒ¨»œ≤Âº˛";


        public Class1()
        {
        }
    }
}
