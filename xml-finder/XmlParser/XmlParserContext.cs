﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xml_finder.XmlParser
{
    public class XmlParserContext
    {
        private IXmlParserStrategy _concreteXmlParser;
        private XmlParserStrategy _strategy;
        
        XmlParserContext()
        {
            _strategy = XmlParserStrategy.LinqParser;
            ChooseParser();
        }

        private void ChooseParser()
        {
            switch (_strategy)
            {
                case XmlParserStrategy.DomParser:
                    _concreteXmlParser = new ConcreteStrategyDom();
                    break;
                       
                case XmlParserStrategy.SaxParser:
                    _concreteXmlParser = new ConcreteStrategySax();
                    break;
                default:
                    _concreteXmlParser = new ConcreteStrategyLinq();
                    break; 
            }
        }

        public void SetStrategy(XmlParserStrategy strategy)
        {
            _strategy = strategy;
            ChooseParser();
        }
    }

    public enum XmlParserStrategy
    {
        DomParser,
        LinqParser,
        SaxParser
    };

}
