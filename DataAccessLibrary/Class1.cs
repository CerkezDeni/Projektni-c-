using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Windows.Storage;
using Windows.UI.Xaml.Controls;

namespace DataAccessLibrary
{
    public class DataAccess
    {
        static string FileName = "sqliteDb";
        public async static void InitalizeDatabase()
        {
            await ApplicationData.Current.LocalFolder.CreateFileAsync(FileName, CreationCollisionOption.OpenIfExists);
            string dbPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, FileName);
            using (SqliteConnection conn = new SqliteConnection($"Filename={dbPath}"))
            {
                conn.Open();

                String tableCommand = "CREATE TABLE IF NOT EXISTS igraci (" +
                    "UserID INTEGER PRIMARY_KEY, " +
                    "Nickname VARCHAR(20) NOT NULL, " +
                    "Time VARCHAR(20) NOT NULL," +
                    "Uspjeh VARCHAR(20) NOT NULL); ";

                SqliteCommand createTable = new SqliteCommand(tableCommand, conn);

                createTable.ExecuteReader();

            }
        }

        public static void AddToTable(string Nickname, string Time, string Uspjeh)
        {
            string dbPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, FileName);
            using(SqliteConnection conn = new SqliteConnection($"Filename={dbPath}"))
            {
                conn.Open();

                SqliteCommand insert = new SqliteCommand();

                insert.Connection = conn;

                insert.CommandText ="INSERT INTO igraci VALUES (NULL, @Nickname, @Time, @Uspjeh);";
                insert.Parameters.AddWithValue("@Nickname", Nickname);
                insert.Parameters.AddWithValue("@Time", Time);
                insert.Parameters.AddWithValue("@Uspjeh", Uspjeh);

                insert.ExecuteReader();

                conn.Close();
            }    
        }
        public static List<User> GetUsers()
        {
            List<User> users = new List<User>();
            string dbPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, FileName);

            using (SqliteConnection conn = new SqliteConnection($"Filename={dbPath}"))
            {
                conn.Open();

                SqliteCommand selectCommand = new SqliteCommand("SELECT Nickname, Time, Uspjeh FROM igraci", conn);
                SqliteDataReader query = selectCommand.ExecuteReader();
                while (query.Read())
                {
                    users.Add(new User
                    {
                        Nickname = query.GetString(0),
                        Time = query.GetString(1),
                        Uspjeh = query.GetString(2)
                    });
                }


                conn.Close();
            }
            return users;
        }
    }
    public class User
    {
        public  string Nickname { get; set; }
        public  string Time { get; set; }

        public string Uspjeh { get;set; }

    }
    
}
