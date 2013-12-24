using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using xml_finder.Model;

namespace xml_finder.XmlParser
{
    class ConcreteStrategyDom : IXmlParserStrategy
    {
        private XmlDocument _document;
        private String _filePath;
        private const String FilePathBase = "res/data.xml";
        private Dictionary<String, String> _dictionary = new Dictionary<string, string>(); 

        public ConcreteStrategyDom() : this(FilePathBase)
        {
            
        }
        public ConcreteStrategyDom(String path)
        {
            _filePath = path; 
            _document = new XmlDocument();
            LoadDocument(_filePath);
        }
        public void LoadDocument(string path)
        {
            _document.Load(path);
        }


        public List<Track> ParseTracks()
        {
            var library = new List<Track>();
            if (_document.FirstChild == null)
                throw new DataException("xml document root is null");
            var root = _document.FirstChild.NextSibling;
            foreach (XmlNode node in root.ChildNodes)
            {
                if (!node.Name.Equals("Track"))
                    continue;
                foreach (XmlNode child in node.ChildNodes)
                {
                    _dictionary.Add(child.Name, child.InnerText);
                }
                string path = "";
                if ( _dictionary.TryGetValue("TrackPath",out path) && path != null)
                    library.Add( new Track(path));
                _dictionary = new Dictionary<string, string>();
            }
            return library;
        }

        public List<Track> Filter(string element, string value)
        {
            var library = new List<Track>();
            if (_document.FirstChild == null && _document.FirstChild.NextSibling == null)
                throw new DataException("xml document root is null");
            var root = _document.FirstChild.NextSibling;
            foreach (XmlNode node in root.ChildNodes)
            {
                if (!node.Name.Equals("Track"))
                    continue;
                foreach (XmlNode child in node.ChildNodes)
                {
                    _dictionary.Add(child.Name, child.InnerText);
                }
                string path, innerText;
                if (_dictionary.TryGetValue(element, out innerText) && innerText.ToLower().Contains(value.ToLower()) 
                    && _dictionary.TryGetValue("TrackPath", out path) && path != null)
                    library.Add(new Track(path));
                _dictionary = new Dictionary<string, string>();
            }
            return library;
        }
    }
}
