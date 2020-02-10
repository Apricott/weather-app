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
using WeatherAPIS;

namespace GUI
{
    /// <summary>
    /// Interaction logic for Current.xaml
    /// </summary>
    public partial class Current : Window
    {
        public Current()
        {
            InitializeComponent();
            //Data Binding dla temperatury: nazwa: Temperature
          
        }
        public void Button_4(object sender, RoutedEventArgs e)
        {
            Weather current= GetWeather(TextBoxCity.Text);
            TextBoxTemp.Text= current.currently.ToString();
        }
        public void Button_5(object sender, RoutedEventArgs e)
        {
            InsertWeatherData(TextBoxCity.Text, current);
        }
    }
}
