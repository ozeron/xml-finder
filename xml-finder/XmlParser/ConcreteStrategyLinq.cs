using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Services;
using System.Xml.Linq;
using NUnit.Framework;
using xml_finder.Model;

namespace xml_finder.XmlParser
{
    class ConcreteStrategyLinq : IXmlParserStrategy
    {
        private XDocument _document;
        private String _filePath;
        private const String FilePathBase = "res/data.xml";


        public ConcreteStrategyLinq() : this(FilePathBase)
        {
        }
        public ConcreteStrategyLinq(String filePath)
        {
            _filePath = filePath;
            LoadDocument(_filePath);
        }
        
         

        public void CreateSampleData()
        {
            _filePath = FilePathBase;
            //счетчик для номера композиции
            int trackId = 1;
            //Создание вложенными конструкторами.
            var doc = new XDocument();
            var library = new XElement("library");
            RecursiveProcess(library, @"D:\MUSIC");
            doc.Add(library);
            doc.Save(_filePath);
        }
        private void RecursiveProcess( XElement el, string path)
        {
            var files = Directory.GetFiles(@path);
            var dir = Directory.GetDirectories(@path);
            foreach (var file in files)
            {
                var extension = (new FileInfo(file)).Extension;
                if (extension.Equals(".mp3"))
                    el.Add(AddTrack(file));
            }
            foreach (var d in dir)
            {
                RecursiveProcess(el,d);
            }
        }
        private  XElement AddTrack(String path)
        {
            var f = new FileInfo(path);
            
            if (!f.Exists)
                return null;
            var track = new Track(f.FullName);
            return new XElement("Track", new XElement("Title",track.Title),
                new XElement("Artist",track.FirstOfArtists), new XElement("Album",track.Album),
                new XElement("Year",track.Year), new XElement("TrackCount",track.TrackCount),
                new XElement("Genre", track.FirstOfGenres), new XElement("Duration",track.Duration),
                new XElement("BitRate",track.BitRate), new XElement("TrackPath",track.TrackFullPath));

        }
       
        public void LoadDocument(string path)
        {
            if ( !File.Exists(path) )
            {
                Debug.WriteLine("xmldoc not found: {0}", path);
                return;
            }
            //Setting File name
            _filePath = path;

            _document = XDocument.Load(path);
            if (_document != null)
                return;
            Debug.WriteLine("xml loaded: {0}",_document);
            
        }
        public List<Track> ParseTracks()
        {
            if (_document.Root == null)
                throw new DataException("xml document root is null");

            var tracks = _document.Root.Elements("Track");
            return (from track in tracks 
                    select track.Element("TrackPath") into trackPath
                    where trackPath != null 
                    select trackPath.Value into path 
                    select new Track(path)).ToList();
}

        public List<Track> Filter(string element, string value)
        {
            if (_document.Root == null)
                throw new DataException("xml document root is null");
            var tracks = _document.Root.Nodes();
            return (from XElement track in tracks
                    let el = track.Element(element)
                    let tp = track.Element("TrackPath")
                    where el != null && tp != null
                    && el.Value.ToLower().Contains(value.ToLower())
                    select new Track(tp.Value)).ToList();
        }
    }
}