using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace MonoMusicManager
{
    class Playlist
    {
        public string Name;
        public List<string> Songs { get; private set; }
        public bool ShortenPaths { get; private set; }

        public Playlist(bool shortenPaths)
        {
            Songs = new List<string>();
            ShortenPaths = shortenPaths;
            Name = DateTime.Now.ToString("yyyy-MM-dd");
        }

        public void AddSong(string path)
        {
            Songs.Add(path);
        }

        public string PrintSong(string song)
        {
            FileInfo file = new System.IO.FileInfo(song);
            song = file.FullName;

            if(ShortenPaths)
            {
                List<string> folders = MusicFolder.GetAllFolderNames();
                DirectoryInfo directory = file.Directory;

                bool searchingHierachy = true;

                while (searchingHierachy && directory.Parent != null)
                {
                    foreach (String folder in folders)
                    {
                        if (directory.Name.Equals(folder))
                        {
                            searchingHierachy = false;
                            break;
                        }
                    }

                    if (searchingHierachy)
                    {
                        directory = directory.Parent;
                    }
                    else
                    {
                        break;
                    }

                    //Console.WriteLine(directory.FullName);
                }

                //Console.WriteLine("Result " + directory.FullName);
                if (!searchingHierachy)
                {
                    song = ".." + file.FullName.Replace(directory.Parent.FullName, "");
                    song = song.Replace("\\", "/");
                }
            }
            
            //Console.WriteLine("Path " + song);

            return song;
        }

        public XElement CreateXML()
        {
            XElement smil = new XElement("smil");
            XElement head = new XElement("head");
            XElement body = new XElement("body");
            XElement generator = new XElement("meta");
            XElement itemCount = new XElement("meta");
            XElement title = new XElement("title", Name);
            XElement seq = new XElement("seq");

            generator.SetAttributeValue("name", "Generator");
            generator.SetAttributeValue("content", "MonoMusicManager");
            itemCount.SetAttributeValue("name", "ItemCount");
            itemCount.SetAttributeValue("content", Songs.Count);


            head.Add(generator);
            head.Add(itemCount);
            head.Add(title);
            smil.Add(head);


            foreach(string song in Songs)
            {
                XElement media = new XElement("media");
                media.SetAttributeValue("src", PrintSong(song));
                seq.Add(media);
            }

            body.Add(seq);
            smil.Add(body);

            return smil;
        }

        public string CreateXMLstring()
        {
            return "<?wpl version=\"1.0\"?>" + CreateXML().ToString();
        }
    }
}
