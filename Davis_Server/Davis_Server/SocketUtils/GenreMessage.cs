using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Davis_Server.SocketUtils
{
    class GenreMessage : Message
    {
        List<string> genres;
        public GenreMessage(List<string> _genres)
            : base(MessageType.GenreList)
        {
            genres = _genres;
        }

        public object GetMessage()
        {
            return this.genres;
        }
    }
}
