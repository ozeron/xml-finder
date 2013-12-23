using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using xml_finder.Annotations;

namespace xml_finder.ViewModel
{
    class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                Debug.WriteLine("Property changed in " + property);
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
        
    }
}
