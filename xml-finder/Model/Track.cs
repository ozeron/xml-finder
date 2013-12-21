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
    public class Track : ViewModel.ViewModelBase
    {
        private String _title;
        private String[] _artists;
        private String _album;
        private String[] _genres;
        private uint _year;
        private uint _trackCount;

        private readonly Time _duration;
        private readonly Int32 _bitRate;

        private readonly String _trackFullPath;
        private readonly TagLib.File _file;

        public String Title
        {
            get { return _title; }
            set
            {
                _title = value;
                _file.Tag.Title = _title;
                RaisePropertyChanged("Title");
            }
        }
        public String Album
        {
            get { return _album; }
            set
            {
                _album = value;
                _file.Tag.Album = _album;
                RaisePropertyChanged("Album");
            }
        }
        public String[] Artists
        {
            get { return _artists; }
            set
            {
                _artists = value;
                _file.Tag.AlbumArtists = _artists;
                RaisePropertyChanged("Artists");
            }
        }
        public String[] Genres
        {
            get { return _genres; }
            set
            {
                _genres = value;
                _file.Tag.Genres= _genres;
                RaisePropertyChanged("Genres");
            }
        }
        public String FirstOfArtists
        {
            get { return (_artists != null) ? _artists[0] : null; }
            set
            {
                _artists = new[] { value };
                _file.Tag.AlbumArtists = _artists;
                RaisePropertyChanged("FirstOfArtists");
            }
        }
        public String FirstOfGenres
        {
            get { return (_genres == null) ? null : _genres[0]; }
            set
            {
                _genres = new[] { value };
                _file.Tag.Genres = _genres;
                RaisePropertyChanged("FirstOfGenres");
            }
        }
        
        public uint Year
        {
            get { return _year; }
            set
            {
                _year = value;
                _file.Tag.Year = _year;
                RaisePropertyChanged("Year");
            }
        }
        public uint TrackCount
        {
            get { return _trackCount; }
            set
            {
                _trackCount = value;
                _file.Tag.Track = _trackCount;
                RaisePropertyChanged("TrackCount");
            }
        }
        public Time Duration
        {
            get { return _duration; }
        }
        public Int32 BitRate
        {
            get { return _bitRate; }
        }
        public String TrackFullPath
        {
            get { return _trackFullPath; }
        }

        public Track()
            : this(@"res/1.mp3")
        {
        }

        public Track(string path)
        {
            var f = new FileInfo(@path);

            if (!f.Exists)
                throw new FileNotFoundException(@"path");

            _trackFullPath = f.FullName;

            _file = TagLib.File.Create(@path);

            _title = _file.Tag.Title;
            _artists = _file.Tag.AlbumArtists;
            _album = _file.Tag.Album;
            _genres = _file.Tag.Genres;
            _year = _file.Tag.Year;
            _trackCount = _file.Tag.Track;

            var t = _file.Properties.Duration;
            _duration = new Time(t.Hours,t.Minutes,t.Seconds);
            _bitRate = _file.Properties.AudioBitrate;
        }

        public override string ToString()
        {
            var str = new StringBuilder();
            str.Append("Title: " + _title + "\n");
            str.Append("Artists: " + _artists + "\n");
            str.Append("Album: " + _album + "\n");
            var st = String.Concat(
                "Title: ", _title, "\nArtist: ", _artists, "\nAlbum: ", _album, "\n#", _trackCount, "\nYear: ", _year, 
                "\nGenre: ", _genres[0], "\nDuration: ", _duration, "\nBitRate: ",
                _bitRate);


            return st;
        }

    }
}
