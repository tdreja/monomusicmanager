using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TagLib;

namespace MonoMusicManager
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            string musicFolder = "C:\\Users\\thoma\\Downloads\\etc";
            string prefix = Path.Combine(musicFolder, "Apocalyptica - Worlds Collide");
            //string prefix = "F:\\Downloads\\Musik\\Solaris";

            MusicFile file = new MusicFile(Path.Combine(prefix, "101 - Worlds Collide.mp3"));
            //MusicFile file = new MusicFile("F:\\Musik\\Lieder\\311\\Love Song.mp3");

            List<MusicFile> sorted = MusicFolder.SortFiles("F:\\Musik\\", file, new MusicFile(Path.Combine(prefix, "105 - Helden.mp3")), new MusicFile(Path.Combine(prefix, "109 - Burn.mp3")));
            foreach(MusicFile tmp in sorted)
            {
                Console.WriteLine("Info " + tmp.Title + " " + tmp.DiscNr + " " + tmp.TrackNr);
                //Console.WriteLine("Parent " + tmp.AlbumParentFolder + " Various "+ tmp.HasVariousArtists + " Folder " + tmp.Folder);
                Console.WriteLine("New Target " + tmp.GetDestination("F:\\"));
            }
        }
    }
}
