using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;

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
