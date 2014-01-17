using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fleck;
using kManager = Davis_Server.KinectManager;
using sManager = Davis_Server.SocketManager;
using aManager = Davis_Server.AudioManager;
using Davis_Server.SocketUtils;
namespace Davis_Server
{
    class Server
    {
        public kManager kmanager;
        public sManager smanager;
        public aManager amanager;
        public static Server instance;
        static void Main(string[] args)
        {
            // components
            Server server = new Server();
            Server.instance = server;
            server.kmanager = new kManager();
            server.smanager = new sManager();
            server.amanager = aManager.GetInstance();

            // kinect component
            if (server.kmanager.FindKinect()
                && server.kmanager.StartupRecognizer()
                && server.kmanager.SetupGrammer())
            {
                Console.WriteLine("Started everything");
            }

            // socket component
            server.smanager.StartServer(new GenreMessage(server.kmanager.Genres));

            Console.ReadLine();
        }
    }
}
