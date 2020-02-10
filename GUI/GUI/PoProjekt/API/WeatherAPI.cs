using System;
using System.Text;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using static WeatherAPIS.GeocodingAPI;

// Powered by Dark Sky 
// https://darksky.net/poweredby/

namespace WeatherAPIS
{
    public class WeatherAPI
    {
        const string secretKey = "b059c81264d98dce5672736382ca5ccd";
        public static string SecretKey => secretKey;

        public class Weather
        {
            public Data currently { get; set; }
            public Section hourly { get; set; }
            public Section daily { get; set; }

        }

        public class Section
        {
            public string summary { get; set; }
            public string icon { get; set; }
            public List<Data> data { get; set; }
            public override string ToString()
            {
                StringBuilder x = new StringBuilder();
                foreach (Data y in data) x.AppendLine(y.ToString()); 
                return x.ToString();

            }
        }

        public class Data
        {
            public string time { get; set; }
            public string summary { get; set; }
            public string temperature { get; set; }
            public string humidity { get; set; }
            public string pressure { get; set; }
            public string windSpeed { get; set; }
            public string icon { get; set; }
            public override string ToString()
            {
                return (" pogoda na " + time +" to; temperatura " + temperature + " wilgotność " + humidity + " ciśnienie " 
                    + pressure + " prędkość wiatru " + windSpeed);
            }
        }


        public static Weather GetWeather(string latitude, string longitude)
        {
            string baseURL = "https://api.darksky.net/forecast/";
            string urlParameters = SecretKey + "/" + latitude + "," + longitude + "?" + "exclude = minutely,alerts,flags" + "&" + "lang=pl" + "&" + "units=si";
            string url = baseURL;

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(url);

            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync(urlParameters).Result;
            if (response.IsSuccessStatusCode)
            {
                var obj = response.Content.ReadAsAsync<Weather>().Result;
                return obj;

            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }

            client.Dispose();
            return null;
        }
    }
}