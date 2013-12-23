using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using xml_finder.Model;
using xml_finder.XmlParser;

namespace xml_finder
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        void StartApp(object sender, StartupEventArgs e)
        {
            //var parser = new ConcreteStrategyLinq();
            //parser.CreateSampleData();
            var mwd = new MainWindow();
            mwd.Show();
            //this.Shutdown();
        }
    }
}
