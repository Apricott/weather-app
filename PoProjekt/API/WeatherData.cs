using System;
using static WeatherAPIS.GeocodingAPI;
using static WeatherAPIS.WeatherAPI;

namespace WeatherAPIS 
{
	public class WeatherData
	{
		public static Weather GetWeather(string location)
		{
			Coordinates coordinates = new Coordinates();
			Weather weather = new Weather();

			coordinates = GeocodingAPI.DecodeLocation(location);
			weather = WeatherAPI.GetWeather(coordinates.lat, coordinates.lon);

			coordinates = null;

			return weather;
		}
	}
}