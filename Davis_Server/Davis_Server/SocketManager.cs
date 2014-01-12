using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fleck;
namespace Davis_Server
{
    public class Message
    {
        string songName, songType;
        public Message(string sname, string stype)
        {
            songName = sname;
            songType = stype;
        }
    }
    class SocketManager
    {
        List<IWebSocketConnection> _clients = new List<IWebSocketConnection>();
        public void StartServer()
        {
            var server = new WebSocketServer("ws://localhost:8181");
            server.Start(socket =>
            {
                socket.OnOpen = () =>
                {
                    Console.WriteLine("Connected to " + socket.ConnectionInfo.ClientIpAddress);
                    _clients.Add(socket);
                };

                socket.OnClose = () =>
                {
                    Console.WriteLine("Disconnected from " + socket.ConnectionInfo.ClientIpAddress);
                };

                socket.OnMessage = message =>
                {
                    Console.WriteLine(message);
                };
            });
        }
    }
}
