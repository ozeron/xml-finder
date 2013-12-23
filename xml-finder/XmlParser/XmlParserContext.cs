using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xml_finder.XmlParser
{
    class XmlParserContext
    {
        private IXmlParserStrategy _concreteXmlParser;
        private XmlParserStrategy _strategy;

        public IXmlParserStrategy ConcreteXmlParser
        {
            get { return _concreteXmlParser; }
        }
        public XmlParserStrategy Strategy
        {
            get { return _strategy; }
            set
            {
                _strategy = value;
                Debug.WriteLine("Strategy changed to {0}",_strategy);
                ChooseParser();
            }
        }
        
        public XmlParserContext()
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
        DomParser =0,
        LinqParser,
        SaxParser
    };

}
