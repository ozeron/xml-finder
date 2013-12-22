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
    }
}
