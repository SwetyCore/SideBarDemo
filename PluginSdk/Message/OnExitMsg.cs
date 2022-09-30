using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginSdk.Message
{
    public class OnExitMsg : RequestMessage<int>
    {
        public OnExitMsg(int i) 
        {

        }
    }

    public static class Messages
    {
        public static void SendOnExitMsg()
        {
            WeakReferenceMessenger.Default.Send(new OnExitMsg(1));
        }
    }
}
