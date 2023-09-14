using Dapper;
using Npgsql;
using YandexCloud.CORE.DTOs;

namespace YandexCloud.BD.Postgres
{
    public class PostgresMarketinActionsRepository : IOzonData<IEnumerable<OzonMarketingActionCostModel>>
    {
        NpgsqlConnection _connection;

        public PostgresMarketinActionsRepository(NpgsqlConnection connection) => _connection = connection;

        public async Task CreateAsync(IEnumerable<OzonMarketingActionCostModel> data)
        {
            var sql = "insert into marketing_actions (amount, operation_id, date) " +
                "values (@amount, @operation_id, @date)";

            await _connection.ExecuteAsync(sql, data);
        }
    }
}
