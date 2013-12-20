using System;
using System.Diagnostics;
using System.IO;
using System.Xml.Linq;

namespace xml_finder.XmlParser
{
    public class ConcreteStrategyLinq : IXmlParserStrategy
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
            var doc = new XDocument(
                new XElement("library",
                    new XElement("track",
                        new XAttribute("id", trackId++),
                        new XAttribute("genre", "Rap"),
                        new XAttribute("time", "3:24"),
                        new XElement("name", "Who We Be RMX (feat. 2Pac)"),
                        new XElement("artist", "DMX"),
                        new XElement("album", "The Dogz Mixtape: Who's Next?!")),
                    new XElement("track",
                        new XAttribute("id", trackId++),
                        new XAttribute("genre", "Rap"),
                        new XAttribute("time", "5:06"),
                        new XElement("name", "Angel (ft. Regina Bell)"),
                        new XElement("artist", "DMX"),
                        new XElement("album", "...And Then There Was X")),
                    new XElement("track",
                        new XAttribute("id", trackId++),
                        new XAttribute("genre", "Break Beat"),
                        new XAttribute("time", "6:16"),
                        new XElement("name", "Dreaming Your Dreams"),
                        new XElement("artist", "Hybrid"),
                        new XElement("album", "Wide Angle")),
                    new XElement("track",
                        new XAttribute("id", trackId++),
                        new XAttribute("genre", "Break Beat"),
                        new XAttribute("time", "9:38"),
                        new XElement("name", "Finished Symphony"),
                        new XElement("artist", "Hybrid"),
                        new XElement("album", "Wide Angle"))));
            //сохраняем наш документ
            doc.Save(_filePath);
        }

        public void LoadDocument()
        {
            LoadDocument(FilePathBase);
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
            Debug.WriteLine("xml loaded!!\n########\n{0}########",_document);
            return;
        }

        public void SaveDocument()
        {
            _document.Save(_filePath);
        }
    }
}