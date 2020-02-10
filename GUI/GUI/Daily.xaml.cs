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
using static WeatherAPIS.WeatherAPI;
using WeatherAPIS;


namespace GUI
{
    /// <summary>
    /// Interaction logic for Daily.xaml
    /// </summary>
    public partial class Daily : Window
    {

        Weather currently = null;


        public Daily()
        {
            InitializeComponent();
        }
        public void Button_6(object sender, RoutedEventArgs e)
        {
             Weather currently = WeatherData.GetWeather(TextBoxCity.Text);
            TextBoxSummary.Text = currently.daily.summary + Environment.NewLine + currently.daily.ToString();
        }
        public void Button_7(object sender, RoutedEventArgs e)
        {
            Database.Database.InsertWeatherData(currently, TextBoxCity.Text);
        }
    }
}
