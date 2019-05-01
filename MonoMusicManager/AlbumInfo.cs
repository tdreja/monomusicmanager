namespace MonoMusicManager
{
    /// <summary>
    /// This class contains all necessary info for an album, but not the music files itself!
    /// This class is used for the sorting of the music files.
    /// </summary>
    public class AlbumInfo
    {
        /// <summary>
        /// How many songs are within the album?
        /// </summary>
        public int SongCount { get; internal set; }

        /// <summary>
        /// How many discs are in the album?
        /// </summary>
        public uint MaxDisc { get; internal set; }

        /// <summary>
        /// What is the highest track number on one disc of the album?
        /// </summary>
        public uint MaxTrack { get; internal set; }

        /// <summary>
        /// Who is the artist of the album?
        /// </summary>
        public string Artist { get; internal set; }

        /// <summary>
        /// Was the album recored by various artists?
        /// </summary>
        public bool IsVarious { get; internal set; }

        /// <summary>
        /// Creates a new AlbumInfo
        /// </summary>
        /// <param name="artist">Artist of the album</param>
        /// <param name="discNr">Maximum disc number</param>
        /// <param name="trackNr">Maxmimum track number</param>
        public AlbumInfo(string artist, uint discNr, uint trackNr)
        {
            this.Artist = artist;
            IsVarious = false;
            SongCount = 1;
            MaxDisc = discNr;
            MaxTrack = trackNr;
        }
    }

}
