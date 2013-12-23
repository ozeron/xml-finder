using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using xml_finder.Commands;
using xml_finder.Model;
using xml_finder.XmlParser;

namespace xml_finder.ViewModel
{
    class MainViewModel : ViewModelBase
    {
        private List<Track> _tracks;
        private List<Track> _showableTracks; 
        private readonly XmlParserContext _xmlParserContext;
        private String _inputQuery ="";
        private String _activeFilter = "";
        private TransformCommand _transform;
        
        public TransformCommand Transform
        { get { return _transform; }
          set
            {
                _transform = value;
                RaisePropertyChanged("Transform");
            } 
        }

        public String InputQuery
        {
            get
            {
                return _inputQuery;
            }
            set
            {
                Debug.WriteLine("Input Query changed");
                _inputQuery = value;
                Search();
                RaisePropertyChanged("InputQuery");
            }
        }
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
        public List<Track> ShowableTracks
        {
            get
            {
                return _showableTracks;
            }
            set
            {
                _showableTracks = value;
                RaisePropertyChanged("ShowableTracks");
            }
        }
        public String ActiveFilter
        {
            get { return _activeFilter; }
            set
            {
                var arr = value.Split(new []{": "}, StringSplitOptions.RemoveEmptyEntries);
                _activeFilter = arr[arr.Length - 1];
                Search();
                Debug.WriteLine("Filter chaged to " + _activeFilter);
                RaisePropertyChanged("ActiveFilter");
            }
        }


        public void Search()
        {
            if (_activeFilter.Equals("None"))
                return;
            var showableTracks = new List<Track>(_tracks);
            for (int i = 0; i < showableTracks.Count;)
            {
                string data = showableTracks[i].GetData(_activeFilter).ToLower();
                string query = _inputQuery.ToLower();
                if (! data.Contains(query))
                    showableTracks.RemoveAt(i);
                else
                    i++;
            }
            
            _showableTracks = showableTracks;
            RaisePropertyChanged("ShowableTracks");
            Transform = new TransformCommand(_showableTracks);
        }
        public MainViewModel()
        {
            
            _xmlParserContext = new XmlParserContext();
            _xmlParserContext.ConcreteXmlParser.LoadDocument("res/data.xml");
            Tracks = _xmlParserContext.ConcreteXmlParser.ParseTracks();
            _transform = new TransformCommand(_showableTracks);
        }


    }
}
