﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xml_finder.Model;

namespace xml_finder.XmlParser
{
    class ConcreteStrategyDom : IXmlParserStrategy
    {
        public ConcreteStrategyDom()
        {
            
        }
        //TODO: Implement Dom Parser
        public void LoadDocument(string path)
        {
            throw new NotImplementedException();
        }

        public void SaveDocument()
        {
            throw new NotImplementedException();
        }

        public void SaveDocument(string path)
        {
            throw new NotImplementedException();
        }

        public List<Track> ParseTracks()
        {
            throw new NotImplementedException();
        }
    }
}
