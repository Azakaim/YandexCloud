using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data.Common;
using System.Runtime.CompilerServices;

namespace YandexCloud.BD
{
    public class DB : IDB
    {
        NpgsqlConnection conn;
        readonly IConfiguration _configuration;

        public DB(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async void ConnectDB()
        {
            var connString = _configuration["YandexDBConnectionString"];

            await using var conn = new NpgsqlConnection(connString);
            await conn.OpenAsync();
        }

    }
}