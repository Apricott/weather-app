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

    private static void ExecuteQuery(string commandText)
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

    public static void InsertValues()
    {

    }
}
