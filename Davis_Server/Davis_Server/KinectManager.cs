using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Kinect;
using Microsoft.Speech.Recognition;
using Microsoft.Speech.AudioFormat;
using System.Threading.Tasks;
using Fleck;
using Newtonsoft.Json;

namespace Davis_Server
{
    public class KinectManager
    {
        KinectSensor Kinect;
        private RecognizerInfo _ri;
        public RecognizerInfo Recognizer
        {
            get { return _ri; }
        }

        private SpeechRecognitionEngine engine;
        public SpeechRecognitionEngine Engine
        {
            get { return engine; }
        }
        List<IWebSocketConnection> sockets = null;
        public bool FindKinect()
        {
            bool foundKinect = false;

            // loop through all avaliable kinects
            foreach (var pkinect in KinectSensor.KinectSensors)
            {
                if (pkinect.Status == KinectStatus.Connected)
                {
                    this.Kinect = pkinect;
                    foundKinect = true;
                    break;
                }
            }

            if (foundKinect)
            {
                Kinect.Start();
                Kinect.ElevationAngle = 15;
            }
            return foundKinect;
        }

        public bool StartupRecognizer()
        {
            bool started = false;
            foreach(var rec in SpeechRecognitionEngine.InstalledRecognizers())
            {
                string value;
                rec.AdditionalInfo.TryGetValue("Kinect", out value);
                if ("True".Equals(value, StringComparison.OrdinalIgnoreCase))
                {
                    started = true;
                    _ri = rec;
                    break;
                }
            }
            return started;
        }

        public bool SetupGrammer()
        {
            if (this._ri == null)
            {
                return false;
            }

            this.engine = new SpeechRecognitionEngine(_ri.Id);
            this.engine.SpeechRecognized += this.SpeechRecognized;
            
            // Add grammer
            Choices choices = new Choices();
            List<string> dances = new List<string>() { "waltz", "swing", "club" };
            foreach(string dance in dances) 
            {
                StringBuilder strBuilder = new StringBuilder();
                strBuilder.Append("Davis play me something I can ");
                strBuilder.Append(dance);
                strBuilder.Append(" to");
                choices.Add(new SemanticResultValue(strBuilder.ToString(), dance));
            }
            choices.Add(new SemanticResultValue("Davis play something for elizabeth", "elizabeth"));
            choices.Add(new SemanticResultValue("Davis play something for mary", "mary"));
            var gb = new GrammarBuilder { Culture = _ri.Culture };
            gb.Append(choices);
            var g = new Grammar(gb);

            this.engine.LoadGrammar(g);
            try
            {
                engine.SetInputToAudioStream(
                    Kinect.AudioSource.Start(), 
                    new SpeechAudioFormatInfo(EncodingFormat.Pcm, 16000, 16, 1, 32000, 2, null)
                );

                this.engine.RecognizeAsync(RecognizeMode.Multiple);
            }
            catch (Exception e)
            {
                return false;
            }
            
            return true;
        }

        public void UpdateClients(List<IWebSocketConnection> _clients)
        {
            this.sockets = _clients;
        }
        public void SpeechRecognized(object sender, SpeechRecognizedEventArgs args)
        {
            const double ConfidenceThreshold = 0.3;
            string song = "", type = args.Result.Semantics.Value.ToString();
            Server.instance.amanager.ClearSongBuffer();
            switch (type)
            {
                case "waltz":
                    {
                        song = Server.instance.amanager.FindSong("waltz");
                        Console.WriteLine("You requested a waltz sir");
                        break;
                    }
                case "swing":
                    {
                        song = Server.instance.amanager.FindSong("swing");
                        Console.WriteLine("You requested to swing sir");
                        break;
                    }
                case "club":
                    {
                        song = Server.instance.amanager.FindSong("club");
                        Console.WriteLine("You requested to club sir");
                        break;
                    }
                case "mary":
                    {
                        song = Server.instance.amanager.FindSong("mary");
                        break;
                    }
                case "elizabeth" :
                    {
                        song = Server.instance.amanager.FindSong("elizabeth");
                        break;
                    }
            }
            Server.instance.amanager.SetAudioFileReaderPath(song);
            Server.instance.amanager.Play();

            // send message over websocket
            string message = JsonConvert.SerializeObject(new Message(song, type));
            if (this.sockets != null)
            {
                foreach (var socket in this.sockets)
                {
                    socket.Send(message);
                }
            }
        }
    }
}
