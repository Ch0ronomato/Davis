using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Davis_Server.SocketUtils
{
    public interface IMessage
    {
        object GetMessage();
    }
}
