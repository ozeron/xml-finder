using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xml_finder.Model;

namespace xml_finder.XmlParser
{
    interface IXmlParserStrategy
    {
        void LoadDocument(String path);
        void SaveDocument();
        void SaveDocument(String path);

        List<Track> ParseDocument();
    }
}
