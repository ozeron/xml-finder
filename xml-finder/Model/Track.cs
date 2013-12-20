using System;
using System.Diagnostics;
using System.IO;
using System.Security.Permissions;
using System.Windows.Media;
using TagLib;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagLib.Aac;
using File = System.IO.File;

namespace xml_finder.Model
{
    internal class Track
    {
        private String _title;
        private String _artist;
        private String _album;
        private String[] _genre;
        private uint _year;
        private uint _trackCount;

        private TimeSpan _duration;
        private Int32 _bitRate;

        private readonly String _trackPath;
        private readonly String _trackFullPath;
        private readonly TagLib.File _file;

        public Track()
        {
            _trackPath = @"res/1.mp3";
<<<<<<< HEAD
            var f = new FileInfo(_trackPath);
=======
            var f = new FileInfo("asddas");
>>>>>>> 97d2e2b62d6232214b35abeea0e1c9b68472a19c
            if (!f.Exists)
                throw new FileNotFoundException();

            _trackFullPath = f.FullName;

            _file = TagLib.File.Create(_trackPath);
            ReadData();
            OpenTrack();
        }

        private void ReadData()
        {
            _title = _file.Tag.Title;
            _artist = _file.Tag.FirstAlbumArtist;
            _album = _file.Tag.Album;
            _genre = _file.Tag.Genres;
            _year = _file.Tag.Year;
            _trackCount = (_file.Tag.TrackCount == 0) ? _file.Tag.Track : _file.Tag.TrackCount;

            _duration = _file.Properties.Duration;
            _bitRate = _file.Properties.AudioBitrate;
            Debug.WriteLine(
                "Title: {0}\nArtist: {1}\nAlbum: {2}\nYear: {3}\n#: {4}\nGenre: {5}\nDuration: {6}\nBitRate: {7}",
                _title, _artist, _album, _year, _trackCount, _genre[0], _duration, _bitRate);
        }

        private void OpenTrack()
        {
            
            var player = new MediaPlayer();
<<<<<<< HEAD
            
            var uri = new Uri(_trackFullPath);
            player.Open(uri);
            player.Play();
            
            //System.Diagnostics.Process.Start(_trackFullPath);
=======
            var uri = new Uri(_trackFullPath);
            player.Open(uri);
            player.Play();
>>>>>>> 97d2e2b62d6232214b35abeea0e1c9b68472a19c
        }
    }
}
