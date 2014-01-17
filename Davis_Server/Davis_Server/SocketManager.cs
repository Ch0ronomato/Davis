using Davis_Server.SocketUtils;
using Fleck;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Davis_Server
{

    class SocketManager
    {
        private class FinalMessage
        {
            object data;
            MessageType type;
            public FinalMessage(object d, MessageType t)
            {
                this.data = d;
                this.type = t;
            }
        }
        List<IWebSocketConnection> _clients = new List<IWebSocketConnection>();
        public void StartServer(GenreMessage genres)
        {
            var server = new WebSocketServer("ws://localhost:8181");
            server.Start(socket =>
            {
                socket.OnOpen = () =>
                {
                    Console.WriteLine("Connected to " + socket.ConnectionInfo.ClientIpAddress);
                    _clients.Add(socket);
                    this._sendMessageTo(genres, socket);
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

        public void SendMessage(Message message)
        {
            this._sendMessage(message);
        }

        private void _sendMessage(Message message)
        {
            foreach (var socket in this._clients)
            {
                this._sendMessageTo(message, socket);
            }
        }

        private void _sendMessageTo(Message message, IWebSocketConnection client)
        {
            Type trueType = message.GetType();
            MethodInfo method = trueType.GetMethods().Where(x => x.Name == "GetMessage").First();
            object data = method.Invoke(message, null);
            FinalMessage m = new FinalMessage(data, message.Type);
            JsonSerializer serializer = new JsonSerializer();
            
            client.Send(JsonConvert.SerializeObject(m));
        }
    }
}
