using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;
using YandexCloud.CORE.DTOs;
using YandexCloud.CORE.Repositories;

namespace YandexCloud.BD.Postgres
{
    public class UoW : IUoW
    {
        readonly IConfiguration _configuration;

        IOzonMainData _ozonMainData;
        IOzonStores _ozonStores;
        IOzonSecondDataRepository _ozonSecondDataRepository;
        IServiceNamesRepository _serviceNamesRepository;

        NpgsqlConnection _connection;
        NpgsqlTransaction _transaction;

        public UoW(IConfiguration configuration)
        {
            _configuration = configuration;
            var connString = _configuration["YandexDBConnectionString"];
            _connection = new NpgsqlConnection(connString);
        }

        public IOzonMainData OzonMainDataRepository => _ozonMainData ??= new PostgresOzonMainDataRepository(_connection);
        public IOzonStores OzonStoresRepository => _ozonStores ??= new PostgresOzonStoresRepository(_connection);
        public IOzonSecondDataRepository OzonSecondDataRepository => _ozonSecondDataRepository ??= new OzonSecondDataRepository(_connection);
        public IServiceNamesRepository OzonServiceNamesRepository => _serviceNamesRepository ??= new PostgresServiceNamesRepository(_connection);

        public async Task OpenTransactionAsync()
        {

            
            await _connection.OpenAsync();
            _transaction = await _connection.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            await _transaction.CommitAsync();
        }

        public async Task RollbackAsync()
        {
            await _transaction.RollbackAsync();
        }

        public void Dispose()
        {
            if (_connection?.State != ConnectionState.Open) 
                _connection?.Close();
            
            _transaction?.Dispose();
            _connection?.Dispose();
        }
    }
}
