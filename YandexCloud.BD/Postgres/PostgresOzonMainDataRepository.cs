using Npgsql;

namespace YandexCloud.BD.Postgres
{
    public class PostgresOzonMainDataRepository : IOzonMainData
    {
        NpgsqlConnection _connection;

        public PostgresOzonMainDataRepository(NpgsqlConnection connection)
        {
            _connection = connection;
        }

        public async Task CreateAsync()
        {

        }
    }
}
