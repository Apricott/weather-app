using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

// locationiq API

namespace WeatherAPIS
{
    public class GeocodingAPI
    {
        const string token = "e30907aa771a50";
        public static string Token => token;


        public class Coordinates
        {
            public string lon { get; set; }
            public string lat { get; set; }

        }

        public static Coordinates DecodeLocation(string searchString)
        {
            string baseURL = "https://eu1.locationiq.com/v1/search.php";
            string urlParameters = "?key=" + Token + "&" + "q=" + searchString + "&" + "format=json";
            string url = baseURL;

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(url);

            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync(urlParameters).Result;
            if (response.IsSuccessStatusCode)
            {
                var dataObjects = response.Content.ReadAsAsync<IEnumerable<Coordinates>>().Result;
                // API returns several coordinatest for similar locations, we want only the first one which is probably the searched for
                using (IEnumerator<Coordinates> iter = dataObjects.GetEnumerator())
                {
                    iter.MoveNext();
                    return iter.Current;
                }

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