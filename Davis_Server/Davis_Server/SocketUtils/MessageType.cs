using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Davis_Server.SocketUtils
{
    public enum MessageType
    {
        GenreList=0x000,
        SongList=0x001,
        CurrentSong=0x002
    }
}
