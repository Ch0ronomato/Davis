using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Davis_Server.SocketUtils
{
    class SongMessage : Message
    {
        string songName, songType;
        public SongMessage(string sname, string stype) 
            : base(MessageType.SongList)
        {
            songName = sname;
            songType = stype;
        }

        public string[] GetMessage()
        {
            return new string[] { songName, songType };
        }
    }
}
