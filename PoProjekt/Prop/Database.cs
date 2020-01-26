using System;
using System.Linq;
using System.Data.SQLite;

namespace Database
{

    static public class Database
    {
        static private SQLiteConnection databaseConnection; 
        static private SQLiteCommand databaseCommand;
        static private SQLiteDataAdapter databaseAdapter;
        static private SQLiteDataReader databaseReader;

        static Database()
        {
            databaseConnection = new SQLiteConnection(@"Data Source=C:\............\bazaDanychProjekt.db");
        }

    //konieczna jest zmiana lokalizacji bazy danych

        public static void ExecuteQuery(string commandText)
        {
            databaseConnection.Open();
            databaseCommand = databaseConnection.CreateCommand();
            databaseCommand.CommandText = commandText;
            databaseCommand.ExecuteNonQuery();
            databaseConnection.Close();
        }
    //funkcja powyżej służy do wykonywania kwerend które nie zwracają wartości, jak na przykład INSERT albo DROP

     }
    

}