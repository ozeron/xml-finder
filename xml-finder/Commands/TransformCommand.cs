using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;
using System.Xml.Xsl;
using xml_finder.Model;

namespace xml_finder.Commands
{
    class TransformCommand : ICommand
    {
        private List<Track> _library;
        public TransformCommand(List<Track> library)
        {
            _library = library;
        }

        XslCompiledTransform _xsl = new XslCompiledTransform();

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            XDocument doc = new XDocument();
            XElement lib = new XElement("library");
            foreach (var track in _library)
            {
                lib.Add( AddTrack(track));
            }
            doc.Add(lib);
            doc.Save("res/out.xml");

            _xsl.Load("data.xsl");
            _xsl.Transform("res/out.xml","res/data.html");
        }

        private XElement AddTrack(Track track)
        {
            return new XElement("Track", new XElement("Title", track.Title),
                new XElement("Artist", track.FirstOfArtists), new XElement("Album", track.Album),
                new XElement("Year", track.Year), new XElement("TrackCount", track.TrackCount),
                new XElement("Genre", track.FirstOfGenres), new XElement("Duration", track.Duration),
                new XElement("BitRate", track.BitRate), new XElement("TrackPath", track.TrackFullPath));
        }
        public event EventHandler CanExecuteChanged;
    }
}
