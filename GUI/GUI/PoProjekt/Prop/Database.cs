using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SQLite;
using WeatherAPIS;

namespace Database
{

    static class Database
    {
        static private SQLiteConnection databaseConnection;
        static private SQLiteCommand databaseCommand;
        static private SQLiteDataReader databaseReader;

        //konieczna jest zmiana lokalizacji bazy danych

        private static void ExecuteInsertQuery(string commandText)
        {
            using (databaseConnection = new SQLiteConnection(
                @"Data Source=C:\Users\piotr\source\repos\ConsoleApp4\ConsoleApp4\bazaDanychProjekt.db"))
            {
                databaseConnection.Open();
                databaseCommand = databaseConnection.CreateCommand();
                databaseCommand.CommandText = commandText;
                databaseCommand.ExecuteNonQuery();
            }
        }
        //funkcja powyżej służy do wykonywania kwerend które nie zwracają wartości, jak na przykład INSERT albo DROP

        private static object[][] ExecuteSelectQuery(string commandText, int queryColumns)
        {
            using (databaseConnection = new SQLiteConnection(
                @"Data Source=C:\Users\piotr\source\repos\ConsoleApp4\ConsoleApp4\bazaDanychProjekt.db"))
            {
                databaseConnection.Open();
                databaseCommand = databaseConnection.CreateCommand();
                databaseCommand.CommandText = commandText;
                databaseReader = databaseCommand.ExecuteReader();
                LinkedList<object> queryResults = new LinkedList<object>();
                while (databaseReader.Read())
                {
                    for (int i = 0; i < queryColumns; i++)
                    {
                        queryResults.AddLast(databaseReader.GetValue(i));
                    }
                }
                int arrayLength = (queryResults.Count() / queryColumns);
                object[][] resultsArray = new object[(arrayLength)][];
                for (int i = 0; i < arrayLength; i++)
                {
                    resultsArray[i] = new object[queryColumns];
                    for (int j = 0; j < queryColumns; j++)
                    {
                        resultsArray[i][j] = queryResults.First.Value;
                        queryResults.RemoveFirst();
                    }
                }
                return resultsArray;
            }
        }

        static private bool CheckCityTable(string city)
        {
            object[][] result = ExecuteSelectQuery($"Select city from city where city='{city}'", 1);
            if (result.Length != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static public void InsertWeatherData(WeatherAPI.Weather entry, string location)
        {
            if (!CheckCityTable(location))
            {
                ExecuteInsertQuery($"Insert into city (city) values {location}");
            }
            int cityID = System.Convert.ToInt32(ExecuteSelectQuery($"Select id_city from city where city='{location}'", 1));

            ExecuteInsertQuery($"Insert into weather_data_object (entry_day, id_city) values ({System.DateTime.Now.DayOfYear}, {cityID}}};");

            int objectID = System.Convert.ToInt32(ExecuteSelectQuery($@"Select id_object from weather_data_object where id_city='{cityID}' 
            and entry_day ='{System.DateTime.Now.DayOfYear}'", 1));

            int order = 0;


        ExecuteInsertQuery($@"Insert into weather (time_recorded, summary, temperature_celsius, humidity, pressure_hPa, 
            wind_speed, type_of_forecast, id_object, list_order) values ('{entry.currently.time}', '{entry.currently.summary}',
            '{entry.currently.temperature}', '{entry.currently.humidity}', '{entry.currently.pressure}', '{entry.currently.windSpeed}', 
            'current', '{objectID}', '{++order}')");

            foreach (WeatherAPI.Data element in entry.hourly.data)
            {
                ExecuteInsertQuery($@"Insert into weather (time_recorded, summary, temperature_celsius, humidity, pressure_hPa, 
                wind_speed,  type_of_forecast, id_object) values ('{element.time}', '{element.summary}',
                '{element.temperature}', '{element.humidity}', '{element.pressure}', '{element.windSpeed}', 
                'hourly', '{objectID}', '{++order}')");
            }

            foreach (WeatherAPI.Data element in entry.daily.data)
            {
                ExecuteInsertQuery($@"Insert into weather (time_recorded, summary, temperature_celsius, humidity, pressure_hPa, 
                wind_speed, type_of_forecast, id_object) values ('{element.time}', '{element.summary}',
                '{element.temperature}', '{element.humidity}', '{element.pressure}', '{element.windSpeed}', 
                'daily', '{objectID}', '{++order}')");
            }
        }

        static public WeatherAPI.Weather ExtractWeatherData(int entryDate, string location)
        {
            if (!CheckCityTable(location))
            {
                throw new ArgumentException("Brak danych dla danego miasta");
            }
            int locationID = System.Convert.ToInt32(ExecuteSelectQuery($"Select id_city from city where city='{location}'", 1));
            object[][] result = ExecuteSelectQuery($@"Select id_entry from weather_data_object where entry_day='{entryDate}'
            and id_city='{locationID}'", 1);
            if (result.Length == 0)
            {
                throw new ArgumentException("Brak danych dla danego dnia");
            }
            int convertedResult = System.Convert.ToInt32(result);
            object[][] weatherData = ExecuteSelectQuery($@"Select time_recorded, summary, temperature_celsius, humidity, pressure_hPa,
            wind_speed, type_of_forecast, list_order from weather where entry_id='{convertedResult}' order by list_order", 8);

            WeatherAPI.Weather ExtractedWeatherData = new WeatherAPI.Weather();
            ExtractedWeatherData.currently = new WeatherAPI.Data();
            ExtractedWeatherData.currently.time = Convert.ToString(result[0][0]);
            ExtractedWeatherData.currently.summary = Convert.ToString(result[0][1]);
            ExtractedWeatherData.currently.temperature = Convert.ToString(result[0][2]);
            ExtractedWeatherData.currently.humidity = Convert.ToString(result[0][3]);
            ExtractedWeatherData.currently.pressure = Convert.ToString(result[0][4]);
            ExtractedWeatherData.currently.windSpeed = Convert.ToString(result[0][5]);
            ExtractedWeatherData.hourly = new WeatherAPI.Section();
            ExtractedWeatherData.daily = new WeatherAPI.Section();
            ExtractedWeatherData.hourly.summary = Convert.ToString(result[0][1]);
            ExtractedWeatherData.daily.summary = Convert.ToString(result[0][1]);
            ExtractedWeatherData.hourly.data = new List<WeatherAPI.Data>();
            ExtractedWeatherData.daily.data = new List<WeatherAPI.Data>();
            foreach (object[] data in weatherData)
            {
                WeatherAPI.Data ListElement = new WeatherAPI.Data();
                ListElement.time = Convert.ToString(data[0]);
                ListElement.summary = Convert.ToString(data[1]);
                ListElement.temperature = Convert.ToString(data[2]);
                ListElement.humidity = Convert.ToString(data[3]);
                ListElement.pressure = Convert.ToString(data[4]);
                ListElement.windSpeed = Convert.ToString(data[5]);
                if (Convert.ToString(data[6]) == "hourly")
                {
                    ExtractedWeatherData.hourly.data.Add(ListElement);
                } else
                {
                    ExtractedWeatherData.daily.data.Add(ListElement);
                }
            }
            return ExtractedWeatherData;
        }
    } }
