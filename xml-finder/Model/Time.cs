using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using NUnit.Framework;

namespace xml_finder.Model
{
    

    public class Time
    {
        private int _hours;
        private int _minutes;
        private int _seconds;

        public int Hours
        {
            get
            {
                return _hours;
            }
            set
            {
                if (value < 0)
                    throw new InvalidDataException("value < 0");
                _hours = value;
            }
        }
        public int Minutes
        {
            get
            {
                return _minutes;
            }
            set
            {
                if (value < 0)
                    throw new InvalidDataException("value < 0");
                    
                _minutes = (value < 60) ? value : value%60;
                Hours += value/60;
            }
        }
        public int Seconds
        {
            get { return _seconds; }
            set
            {
                if (value < 0)
                    throw new InvalidDataException("value < 0");
                _seconds = (value < 60) ? value : value % 60;
                Minutes += value / 60;
            } 
        }

        public Time() : this(0,0,0)
        {}
        public Time(int h, int m, int s)
        {
            Hours = h;
            Minutes = m;
            Seconds = s;
        }
   
        
        public override string ToString()
        {
            return Hours + ":" + Minutes + ":" + Seconds;
        }
    }

    [TestFixture]
    public class TimeTest
    {

        [Test]
        public void TestTime()
        {
            Time t = new Time();
            
            Assert.AreEqual(t.Hours,0);
            Assert.AreEqual(t.Minutes, 0);
            Assert.AreEqual(t.Seconds, 0);
            Assert.AreEqual(t.ToString(),"0:0:0");

            t = new Time(0,23,45);
            Assert.AreEqual(t.Hours, 0);
            Assert.AreEqual(t.Minutes, 23);
            Assert.AreEqual(t.Seconds, 45);
            Assert.AreEqual(t.ToString(), "0:23:45");

            t = new Time(0, 59, 61);
            Assert.AreEqual(t.Hours, 1);
            Assert.AreEqual(t.Minutes, 0);
            Assert.AreEqual(t.Seconds, 1);
            Assert.AreEqual(t.ToString(), "1:0:1");
            
            
        }

        [Test]
        [ExpectedException(typeof (InvalidDataException))]
        public void TestTimeMinus()
        {
            var t = new Time(-1, 61, -1);
            
        }
    }
}
