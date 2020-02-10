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
    /// Interaction logic for Hourly.xaml
    /// </summary>
    public partial class Hourly : Window
    {

        Weather currently = null;

        public Hourly()
        {

            InitializeComponent();
        }
        public void Button_4(object sender, RoutedEventArgs e)
        {
            Weather currently = WeatherData.GetWeather(TextBoxCity.Text);

            StringBuilder s = new StringBuilder();

            foreach (Data y in currently.hourly.data)
            {
                s.AppendLine(y.ToString() + Environment.NewLine);
            }


            TextBoxSummary.Text = s + Environment.NewLine + currently.hourly.summary;
        }
        public void Button_5(object sender, RoutedEventArgs e)
        {
            Database.Database.InsertWeatherData(currently, TextBoxCity.Text);
        }
    }
    
}
