using Dapper;
using Npgsql;
using YandexCloud.CORE.DTOs;

namespace YandexCloud.BD.Postgres
{
    public class PostgresOzonAcquiringDataRepository : IOzonData<IEnumerable<OzonAcquiringDataDto>>
    {
        NpgsqlConnection _connection;

        public PostgresOzonAcquiringDataRepository(NpgsqlConnection connection) => _connection = connection;

        public async Task CreateAsync(IEnumerable<OzonAcquiringDataDto> data)
        {
            var sql = "insert into acquiring_table (sku, name, amount, posting_number, operation_id, date) " +
                "values (@sku, @name, @amount, @posting_number, @operation_id, @date)";

            await _connection.ExecuteAsync(sql, data);
        }
    }
}
