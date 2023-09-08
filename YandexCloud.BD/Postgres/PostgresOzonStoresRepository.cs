using Dapper;
using Npgsql;
using YandexCloud.CORE.DTOs;
using YandexCloud.CORE.Repositories;

namespace YandexCloud.BD.Postgres
{
    public class PostgresOzonStoresRepository : IOzonStores
    {
        NpgsqlConnection _connection;

        public PostgresOzonStoresRepository(NpgsqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<int> CreateAsync(OzonDataDto ozonDataDto)
        {
            var sql = "insert into ozon_stores (store) value (@store) returning 1";
            return await _connection.QueryFirstAsync<int>(sql, new { store = ozonDataDto });
        }
    }
}
