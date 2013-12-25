﻿using System;
using System.Diagnostics;
using System.Globalization;
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
    class Track : ViewModel.ViewModelBase
    {
        private String _title = "";
        private String[] _artists = new []{""};
        private String _album = "";
        private String[] _genres = new []{""};
        private uint _year = 0;
        private uint _trackCount=0;

        private readonly Time _duration=new Time();
        private readonly Int32 _bitRate = 0;

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
            get { return (_artists.GetLength(0) > 0) ? _artists[0] : ""; }
            set
            {
                _artists = new[] { value };
                _file.Tag.AlbumArtists = _artists;
                RaisePropertyChanged("FirstOfArtists");
            }
        }
        public String FirstOfGenres
        {
            get { return (_genres.GetLength(0) > 0 ) ? _genres[0] : ""; }
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
        public int BitRate
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
            
            FixBadData();
        }

        public Track(String title, String album, String artist, String genre, uint year, uint trackCount, Time duration,
            int bitRate, String trackFullPath)
        {
            _trackFullPath = trackFullPath;
            _file = TagLib.File.Create(@_trackFullPath);

            _title = title;
            _artists = new [] {artist};
            _album = album;
            _genres = new [] {genre};
            _year = year;
            _trackCount = trackCount;

            _duration = duration;
            _bitRate = bitRate;

            FixBadData();
        }

        private void FixBadData()
        {
            if (_album == null) _album = "";
            if (_genres == null) _genres = new[]{""} ;
            if (_artists == null) _artists = new[] {""};

            if (!(FirstOfArtists.Equals("") ||  _title.Equals("")))
                return;

            var f = new FileInfo(_trackFullPath);
            string[] str = f.Name.Split(new []{'-','[',']'},StringSplitOptions.RemoveEmptyEntries);
            int i = 0;

            if (FirstOfArtists == null || FirstOfArtists.Equals(""))
                FirstOfArtists = str[0];
            if (Title == null || Title.Equals("") && str.Length >= 2)
                Title = str[1];
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

        public string GetData(string data)
        {
            switch (data)
            {
                case "Title":
                    return _title;
                case "Artist":
                    return (_artists.GetLength(0) > 0) ? _artists[0] : "";
                case "Album":
                    return _album;
                case "#":
                    return _trackCount.ToString();
                case "Year":
                    return _year.ToString();
                case "Genres":
                    return _genres[0];
                case "Duration":
                    return _duration.ToString();
                case "BitRate":
                    return _bitRate.ToString();
                case "FirstOfArtists":
                    return (_artists.GetLength(0) > 0) ? _artists[0] : "";
                case "FirstOfGenres":
                    return(_genres.GetLength(0) > 0) ? _genres[0] : "";
                default:
                    return _trackFullPath;
            }
        }
    }
}
