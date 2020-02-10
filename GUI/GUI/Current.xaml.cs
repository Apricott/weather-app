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
using static WeatherAPIS.WeatherAPI;
using WeatherAPIS;

namespace GUI
{
    /// <summary>
    /// Interaction logic for Current.xaml
    /// </summary>
    public partial class Current : Window
    {
        Weather current = null;

        public Current()
        {
            InitializeComponent();
        }
        public void Button_4(object sender, RoutedEventArgs e)
        {
            current = WeatherData.GetWeather(TextBoxCity.Text);
            TextBoxTemp.Text= current.currently.ToString();
        }
        public void Button_5(object sender, RoutedEventArgs e)
        {
            Database.Database.InsertWeatherData(current, TextBoxCity.Text);
        }
    }
}
