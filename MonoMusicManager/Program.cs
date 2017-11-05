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

            string prefix = "F:\\Musik\\Album\\Hardstyle\\Hardstyle Vol 11";
            //string prefix = "F:\\Downloads\\Musik\\Solaris";

            MusicFile file = new MusicFile(Path.Combine(prefix, "103 - Fahrenheit.mp3"));
            //MusicFile file = new MusicFile("F:\\Musik\\Lieder\\311\\Love Song.mp3");
            Console.WriteLine("Title " + file.title + " Artist " + file.artist + " Album " + file.album + " Genre "+ file.genre + " Path " + file.path + " Track "+file.trackNr + " Disc "+file.discNr);

            List<MusicFile> sorted = MusicFolder.sortFiles("F:\\Musik\\", file, new MusicFile(Path.Combine(prefix, "110 - Back Again.mp3")), new MusicFile(Path.Combine(prefix, "114 - Magma.mp3")));
            foreach(MusicFile tmp in sorted)
            {
                Console.WriteLine("Parent " + tmp.albumParentFolder + " Various "+ tmp.variousArtists + " Folder " + tmp.folder);
            }
        }
    }
}
