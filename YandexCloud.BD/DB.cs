using Npgsql;
using System.Data.Common;
using System.Runtime.CompilerServices;

namespace YandexCloud.BD
{
    public class DB : IDB
    {
        string Host;
        string Port = "6432";
        string NameDB;
        string UserName;
        string Password;
        NpgsqlConnection conn;

        public DB(string host, string port, string name_DB, string user_Name, string password)
        {
            Host = host;
            Port = port;
            NameDB = name_DB;
            UserName = user_Name;
            Password = password;
            ConnectDB();
            ReaderDB().Wait();
        }

        public async void ConnectDB()
        {
            var connString = $"Host={Host};Port={Port};Database={NameDB};Username={UserName};Password={Password};Ssl Mode=VerifyFull;";

            await using var conn = new NpgsqlConnection(connString);
            await conn.OpenAsync();
        }

        public async Task ReaderDB()
        {
            await using (var cmd = new NpgsqlCommand("SELECT VERSION();", conn))
            await using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    Console.WriteLine(reader.GetInt32(0));
                }
            }
        }
    }
}