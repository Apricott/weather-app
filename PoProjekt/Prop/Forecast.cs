using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace ConsoleApp6.Properties
{
    enum Cloudy { słonecznie, lekkie_zachmurzenie, duze_zachmurzenie };
    enum Rainfall { brak, przelotne_opady_deszczu, deszcz, deszcz_ze_sniegiem, snieg};
    enum Wind_direction { 
    północny, 
    północnoWschodni, 
    wschodni, 
    południowoWschodni, 
    południowy, 
    południowoZachodni, 
    zachodni, 
    północnoZachodni 
    };
    class Forecast : IComparable<Forecast>
    {
        private int temperature { get; set; }
        private Rainfall rainfall { get; set; }
        private Cloudy cloudy { get; set; }
        private int humidity { get; set; }
        private int wind_strength { get; set; }
        private Wind_direction wind_direction { get; set; }
        private int hours { get; set; }
        static public int number_of_forecasts;

        Forecast(int temp, Rainfall rain, Cloudy clou, int hum, int ws, Wind_direction wd, Cities city)
        {
            this.temperature = temp;
            this.rainfall = rain;
            this.cloudy = clou;
            this.humidity = hum;
            this.wind_strength = ws;
            this.wind_direction = wd;
            number_of_forecasts++;
        }
         public int CompareTo(Forecast f)
            {
            if (f == null) return 0;
            else if (this.hours > f.hours) return (-1);
            else return (1);
            }

        public override string ToString()
        {
            return ( " temperatura to " + temperature + ", przy wilgotnosci " + humidity + " zachmurzeniu " + cloudy + " opadach " + rainfall + " wietrze " +wind_direction + "o sile" +wind_strength);
        }

        public void ToXml(string file_name)
        {

        }
    }
}
