using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Database;
using WeatherAPIS;


namespace GUI
{
    /// <summary>
    /// Interaction logic for Daily.xaml
    /// </summary>
    public partial class Daily : Window
    {
        public Daily()
        {
            InitializeComponent();
        }
        public void Button_6()
        {
             Weather currently= GetWeather(TextBoxCity.Text);
        }
        public void Button_7()
        {
            InsertWeatherData(currently, TextBoxCity.Text);
        }
    }
}
