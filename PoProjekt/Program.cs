using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherAPIS;
using static WeatherAPIS.WeatherAPI;

namespace PoProjekt
{
    class Program
    {
        static void Main(string[] args)
        {

            Weather w1 = WeatherData.GetWeather("Kraków");
            

        }
    }
}
