using System;
using KashkeshetCommon.MessageProtocol;

namespace SocketServer.Client
{
    public class OnMessage
    {
        event EventHandler OnMessageEvent;

        public OnMessage()
        {
        }
        public void AfterMessageRecieved(SocketMessageProtocol socketMessage)
        {
            OnMessageEvent.Invoke(this, new MsgEventArgs(socketMessage));
        }

        private class MsgEventArgs : EventArgs
        {
            public SocketMessageProtocol socketMessage { get;private set; }

            public MsgEventArgs(SocketMessageProtocol socketMessage)
            {
                this.socketMessage = socketMessage;
            }
        }
    }
    
}
