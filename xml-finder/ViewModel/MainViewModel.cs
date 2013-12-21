using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using xml_finder.Model;
using xml_finder.XmlParser;

namespace xml_finder.ViewModel
{
    class MainViewModel : ViewModelBase
    {
        private List<Track> _tracks;
        private XmlParserContext _xmlParserContext;

        public XmlParserContext XmlParserContext
        {
            get
            {
                return _xmlParserContext; 
            }
        }
        public List<Track> Tracks
        {
            get
            {
                return _tracks;
            }
            set
            {
                _tracks = value;
                RaisePropertyChanged("Tracks");
            }
        }

        public MainViewModel()
        {
            _xmlParserContext = new XmlParserContext();
            _xmlParserContext.ConcreteXmlParser.LoadDocument("res/data.xml");
            Tracks = _xmlParserContext.ConcreteXmlParser.ParseTracks();
        }
    }
}
