using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using NAudio;
using NAudio.Wave;

namespace Davis_Server
{
    class AudioManager
    {
        private static AudioManager Instance;
        IWavePlayer player;
        AudioFileReader file;
        bool playing = false;
        List<string> songs = new List<string>();
        string rootPath = "C:\\Users\\Ian\\Music\\Davis_music_root\\";
        private AudioManager()
        {
            player = new WaveOut();
        }

        public void SetAudioFileReaderPath(string path)
        {
            file = new AudioFileReader(path);
        }

        public void Play()
        {
            if (!this.playing)
            {
                this.playing = true;
                this.player.Init(this.file);
                this.player.Play();
                this.player.PlaybackStopped += new EventHandler<StoppedEventArgs>(delegate(object sender, StoppedEventArgs args)
                {
                    this.playing = false;
                    if (this.songs.Count > 0)
                    {
                        // play another song
                        Random rand = new Random();
                        string song = this.songs.ElementAt(rand.Next(0, this.songs.Count));
                        SetAudioFileReaderPath(this.rootPath + song);
                        this.Play();
                    }
                });
            }
        }
        
        public static AudioManager GetInstance()
        {
            if (AudioManager.Instance == null)
            {
                AudioManager.Instance = new AudioManager();
            }

            return AudioManager.Instance;
        }

        public void ClearSongBuffer()
        {
            this.songs = new List<string>();
        }

        public string FindSong(string songType)
        {
            string songName = "";

            // open up the corresponding directory from our root path.
            foreach (string file in Directory.EnumerateFiles(this.rootPath + songType))
            {
                this.songs.Add(file);
            }

            // get a random song.
            Random rand = new Random();
            int index = rand.Next(0, songs.Count);
            songName = songs.ElementAt(index);
            songs.RemoveAt(index);

            return songName;
        }
    }
}
