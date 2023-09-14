using Dapper;
using Npgsql;
using YandexCloud.CORE.DTOs;

namespace YandexCloud.BD.Postgres
{
    public class PostgresClientReturnAgentRepository : IOzonData<IEnumerable<ClientReturnAgentOperationModel>>
    {
        NpgsqlConnection _connection;

        public PostgresClientReturnAgentRepository(NpgsqlConnection connection) => _connection = connection;

        public async Task CreateAsync(IEnumerable<ClientReturnAgentOperationModel> data)
        {
            var sql = "insert into client_return_agent (sku, name, amount, posting_number, operation_id, date) " +
                "values (@sku, @name, @amount, @posting_number, @operation_id, @date)";

            await _connection.ExecuteAsync(sql, data);
        }
    }
}
