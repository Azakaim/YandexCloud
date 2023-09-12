using Dapper;
using Npgsql;
using YandexCloud.CORE.DTOs;

namespace YandexCloud.BD.Postgres
{
    public class PostgresOzonMainDataRepository : IOzonData<IEnumerable<OzonFirstDataDto>>
    {
        NpgsqlConnection _connection;

        public PostgresOzonMainDataRepository(NpgsqlConnection connection)
        {
            _connection = connection;
        }

        public async Task CreateAsync(IEnumerable<OzonFirstDataDto> data)
        {
            var sql = "insert into first_table " +
                "values (@id, @date, @sku, @name, @posting_number, @accruals_for_sale, @sale_comission)";
            await _connection.ExecuteAsync(sql, data);
        }
    }
}
