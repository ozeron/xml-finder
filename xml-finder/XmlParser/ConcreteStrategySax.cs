using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using xml_finder.Model;

namespace xml_finder.XmlParser
{
    class ConcreteStrategySax : IXmlParserStrategy
    {
        private XmlReader _document;
        private String _filePath;
        private const String FilePathBase = "res/data.xml";
        private Dictionary<String, String> _dictionary = new Dictionary<string, string>();

        public ConcreteStrategySax() : this(FilePathBase)
        {     
        }
        public ConcreteStrategySax(String path)
        {
            _filePath = path;
            LoadDocument(path);
        }

        public void LoadDocument(string path)
        {
            _document = XmlReader.Create(path);
        }


        public List<Track> ParseTracks()
        {
            var library = new List<Track>();
            while (_document.Read())
            {
                if (_document.Name.Equals("TrackPath"))
                {
                    _document.Read();
                    if (_document.NodeType == XmlNodeType.Text)
                        library.Add(new Track(@_document.Value));
                }
            }
            return library;
        }

        public List<Track> Filter(string element, string value)
        {
            var library = new List<Track>();
            while (_document.Read())
            {
                if (_document.NodeType == XmlNodeType.Element && _document.Name.Equals("Track"))
                {
                    _document.Read();
                    while (!(_document.NodeType == XmlNodeType.EndElement && _document.Name.Equals("Track")))
                    {
                        if (_document.NodeType == XmlNodeType.Element)
                        {
                            string key = _document.Name;
                            _document.Read();
                            if (_document.NodeType == XmlNodeType.Text)
                                _dictionary.Add(key, @_document.Value);
                        }
                        _document.Read();
                    }
                    string path, innerText;
                    if (_dictionary.TryGetValue(element, out innerText) && innerText.ToLower().Contains(value.ToLower()) 
                        && _dictionary.TryGetValue("TrackPath", out path) && path != null)
                        library.Add(new Track(path));
                    _dictionary = new Dictionary<string, string>();
                }
                
            }
            LoadDocument(_filePath);
            return library;
        }
    }
}
