using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Davis_Server.SocketUtils
{
    abstract class Message : IMessage
    {
        MessageType type;
        public MessageType Type
        {
            get
            {
                return this.type;
            }
        }
        public Message(MessageType ms)
        {
            type = ms;
        }

        public object GetMessage()
        {
            return "Unknown";
        }
    }
}
