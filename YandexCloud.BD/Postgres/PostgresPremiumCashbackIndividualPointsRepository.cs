using Dapper;
using Npgsql;
using YandexCloud.CORE.DTOs;

namespace YandexCloud.BD.Postgres
{
    public class PostgresPremiumCashbackIndividualPointsRepository : IOzonData<IEnumerable<PremiumCashbackIndividualPointsModel>>
    {
        NpgsqlConnection _connection;

        public PostgresPremiumCashbackIndividualPointsRepository(NpgsqlConnection connection) => _connection = connection;

        public async Task CreateAsync(IEnumerable<PremiumCashbackIndividualPointsModel> data)
        {
            var sql = "insert into premium_cashback_individual_points (sku, name, amount, posting_number, operation_id, date) " +
                "values (@sku, @name, @amount, @posting_number, @operation_id, @date)";

            await _connection.ExecuteAsync(sql, data);
        }
    }
}
