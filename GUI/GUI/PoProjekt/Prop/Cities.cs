using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp6.Properties
{
    class Cities
    {
        string name { get; set; }
        public static int number_of_cities;
        List<Date> list_of_days = new List<Date>();

        Cities( string nazwa)
        {
            this.name = nazwa;
            number_of_cities++;
        }

        public void AddDay(Date f)
        {
            this.list_of_days.Add(f);
            this.list_of_days.Sort();
        }
    }
}
