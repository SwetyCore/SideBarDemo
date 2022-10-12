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
            description = "øŒ≥Ã±Ìø®∆¨",
            mainView = typeof(View.AISchedule)
        };

        public static cardInfo BiliHelper = new cardInfo()
        {
            name = "b’æø®∆¨",
            description = "œ‘ æ’À∫≈–≈œ¢µƒø®∆¨",
            mainView = typeof(View.BiliHelper)
        };

        public static cardInfo Gallery = new cardInfo()
        {
            name = "œ‡≤·",
            description = " «œ‡≤·¿≤",
            mainView = typeof(View.Gallery)
        };
        internal static cardInfo MsToDo = new cardInfo()
        {
            name = "Microsoft Todo",
            description = "Œ¢»Ì¥˝∞Ïø®∆¨",
            mainView = typeof(View.MsToDo)
        };
        internal static cardInfo CountDown = new cardInfo()
        {
            name = "µπ ˝»’",
            description = "µπ ˝»’ø®∆¨",
            mainView = typeof(View.CountDown)
        };
        internal static cardInfo Memo = new cardInfo()
        {
            name = "±„º„",
            description = "±„º„¿≤",
            mainView = typeof(View.Memo)
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
        };




        public string name => "ƒ¨»œ≤Âº˛";


    }
}
