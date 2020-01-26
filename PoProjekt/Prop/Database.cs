using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SQLite;

namespace Database
{

    static class Database
    {
        static private SQLiteConnection databaseConnection; 
        static private SQLiteCommand databaseCommand;
        static private SQLiteDataAdapter databaseAdapter;
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
            int arrayLength = (queryResults.Count()/queryColumns);
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
    
    private bool CheckCityTable(string city)
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
        object cityID = ExecuteSelectQuery($"Select id_city from city where city='{location}'", 1);

        ExecuteInsertQuery($@"Insert into weather (time_recorded, summary, temperature_celsius, humidity, pressure_hPa, 
            wind_speed, id_city, type_of_forecast, insertion_date) values ('{entry.currently.time}', '{entry.currently.summary}',
            '{entry.currently.temperature}', '{entry.currently.humidity}', '{entry.currently.pressure}', '{entry.currently.windSpeed}', 
            {Convert.ToInt32(cityID)}, 'current', 'Convert.ToString(System.DateTime.Now)')");

        foreach (WeatherAPI.Data element in entry.hourly.data)
        {
            ExecuteInsertQuery($@"Insert into weather (time_recorded, summary, temperature_celsius, humidity, pressure_hPa, 
            wind_speed, id_city, type_of_forecast, insertion_date) values ('{element.time}', '{element.summary}',
            '{element.temperature}', '{element.humidity}', '{element.pressure}', '{element.windSpeed}', 
            {Convert.ToInt32(cityID)}, 'hourly', 'Convert.ToString(System.DateTime.Now)')");
        }

        foreach (WeatherAPI.Data element in entry.daily.data)
        {
           ExecuteInsertQuery($@"Insert into weather (time_recorded, summary, temperature_celsius, humidity, pressure_hPa, 
            wind_speed, id_city, type_of_forecast, insertion_date) values ('{element.time}', '{element.summary}',
            '{element.temperature}', '{element.humidity}', '{element.pressure}', '{element.windSpeed}', 
            {Convert.ToInt32(cityID)}, 'daily', 'Convert.ToString(System.DateTime.Now)')");
        }
    }

    static public WeatherAPI.Weather ExtractWeatherData(string entryTime)
    {
        
    }
}
