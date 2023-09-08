using Dapper;
using Npgsql;
using YandexCloud.CORE.DTOs;

namespace YandexCloud.BD.Postgres
{
    public class PostgresOzonMainDataRepository : IOzonMainData
    {
        NpgsqlConnection _connection;

        public PostgresOzonMainDataRepository(NpgsqlConnection connection)
        {
            _connection = connection;
        }

        public async Task CreateAsync(IEnumerable<OzonDataDto> data)
        {
            var sql = "insert into first_table (date, sku, name, posting_number, accruals_for_sale, sale_comission) " +
                "values (@data)";
            await _connection.ExecuteAsync(sql, data);
        }
    }
}
