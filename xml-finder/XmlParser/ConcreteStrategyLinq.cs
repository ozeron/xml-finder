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
            if (!File.Exists(_filePath))
            {
                CreateSampleData();
                Debug.WriteLine("");
            }
        }
        public ConcreteStrategyLinq(String filePath)
        {
            _filePath = filePath;
        }
        
        public void CreateSampleData()
        {
            _filePath = FilePathBase;
            //счетчик для номера композиции
            int trackId = 1;
            //Создание вложенными конструкторами.
            var doc = new XDocument();
            var library = new XElement("library");
            var files = Directory.GetFiles(@"D:\MUSIC\Eminem\The Marshall Mathers LP 2 [2013]\CD 1");
            foreach (var file in files)
            {
                var fileInfo = new FileInfo(file);
                var extension = fileInfo.Extension;
                if (extension.Equals(".mp3"))
                    library.Add( AddTrack(file)) ;
            }
            doc.Add(library);
            doc.Save(_filePath);
        }
        public XElement AddTrack(String path)
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
        public void SaveDocument()
        {
            _document.Save(_filePath);
            Debug.WriteLine("xml saved to '{0}': {1}", _filePath, _document);
        }
        public void SaveDocument(String path)
        {
            _document.Save(path);
            Debug.WriteLine("xml saved to '{0}': {1}", path, _document);
        }

        public List<Track> ParseTracks()
        {
            if (_document.Root == null)
                throw new DataException("xml document root is null");

            var tracks = _document.Root.Elements("Track");
            var library = (from track in tracks select track.Element("TrackPath") into trackPath
                           where trackPath != null select trackPath.Value into path
                           select new Track(path)).ToList();

            Debug.WriteLine("xml parsed {0} elements in {1}", library.Count, _document);
            return library;
        }
    }
}