using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp6.Properties
{

    public class Date: IComparable<Date>
    {
        private DateTime date;
        List<Forecast> List_of_forecast = new List<Forecast>();
        public Date( DateTime date)
        {
            this.date = date;
        }
        void Add_to_list(Forecast f)
        {
            this.List_of_forecast.Add(f);
            this.List_of_forecast.Sort();
        }
        public int CompareTo(Date f)
        {
            if (f == null) return 0;
            else if (this.date > f.date) return (-1);
            else return (1);
        }
    }
}