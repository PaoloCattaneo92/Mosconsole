using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ChiudiQuellaPortaDai
{
    public class MoscoAudio
    {
        public FileInfo File { get; set; }
        public Uri Uri => new Uri(File.FullName, UriKind.Absolute);
        public string Name
        {
            get
            {
                var name = File.Name.Replace('_', ' ');
                name = name.Replace(".wav", "");
                name = name.First().ToString().ToUpper() + name.Substring(1);
                return name;
            }
        }

        public MoscoAudio(string file)
        {
            File = new FileInfo(file);
        }
    }
}
