using Dapper;
using Npgsql;
using YandexCloud.CORE.DTOs;

namespace YandexCloud.BD.Postgres
{
    public class PostgresReturnAgentOperationRFBSRepository : IOzonData<IEnumerable<ReturnAgentOperationRFBSModel>>
    {
        NpgsqlConnection _connection;

        public PostgresReturnAgentOperationRFBSRepository(NpgsqlConnection connection) => _connection = connection;

        public async Task CreateAsync(IEnumerable<ReturnAgentOperationRFBSModel> data)
        {
            var sql = "insert into return_agent_operation_rfbs (sku, name, amount, posting_number, operation_id, date) " +
                "values (@sku, @name, @amount, @posting_number, @operation_id, @date)";

            await _connection.ExecuteAsync(sql, data);
        }
    }
}
