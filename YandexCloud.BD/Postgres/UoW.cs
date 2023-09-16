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

        IOzonData<IEnumerable<OzonFirstDataDto>> _ozonMainData;
        IOzonData<IEnumerable<OzonAcquiringDataDto>> _ozonAcquiringData;
        IOzonData<IEnumerable<OzonMarketingActionCostModel>> _ozonMarketingActionData;
        IOzonData<IEnumerable<ClientReturnAgentOperationModel>> _clientReturnAgentOperationData;
        IOzonData<IEnumerable<ReturnAgentOperationRFBSModel>> _returnAgentOperationRFBSData;
        IOzonData<IEnumerable<OperationReturnGoodsFBSofRMSModel>> _operationReturnGoodsFBSofRMSData;
        IOzonData<IEnumerable<PriceByReturnGoodsFBSOfRMSModel>> _priceByReturnGoodsFBSOfRMSData;
        IOzonData<IEnumerable<OperationItemReturnModel>> _operationItemReturnModel;
        IOzonData<IEnumerable<PriceByOperationItemReturnModel>> _priceByOperationItemReturnData;
        IOzonData<IEnumerable<PremiumCashbackIndividualPointsModel>> _premiumCashbackIndividualPointsData;
        IOzonData<IEnumerable<HoldingForUndeliverableGoodsModel>> _holdingForUndeliverableGoodsData;
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

        public IOzonData<IEnumerable<OzonFirstDataDto>> OzonMainDataRepository => _ozonMainData ??= new PostgresOzonMainDataRepository(_connection);
        public IOzonStores OzonStoresRepository => _ozonStores ??= new PostgresOzonStoresRepository(_connection);
        public IOzonSecondDataRepository OzonSecondDataRepository => _ozonSecondDataRepository ??= new OzonSecondDataRepository(_connection);
        public IServiceNamesRepository OzonServiceNamesRepository => _serviceNamesRepository ??= new PostgresServiceNamesRepository(_connection);
        public IOzonData<IEnumerable<OzonAcquiringDataDto>> OzonAcquiringRepository => _ozonAcquiringData ??= new PostgresOzonAcquiringDataRepository(_connection);
        public IOzonData<IEnumerable<OzonMarketingActionCostModel>> OzonMarketingActionsRepository =>
            _ozonMarketingActionData ??= new PostgresMarketinActionsRepository(_connection);
        public IOzonData<IEnumerable<ClientReturnAgentOperationModel>> OzonClientReturnAgentRepository =>
            _clientReturnAgentOperationData ??= new PostgresClientReturnAgentRepository(_connection);
        public IOzonData<IEnumerable<ReturnAgentOperationRFBSModel>> OzonReturnAgentOperationRFBSRepository =>
            _returnAgentOperationRFBSData ??= new PostgresReturnAgentOperationRFBSRepository(_connection);
        public IOzonData<IEnumerable<OperationReturnGoodsFBSofRMSModel>> OzonOperationReturnGoodsFbsOfRmsRepository =>
            _operationReturnGoodsFBSofRMSData ??= new PostgresOperationReturnGoodsFbsOfRmsRepository(_connection);
        public IOzonData<IEnumerable<PriceByReturnGoodsFBSOfRMSModel>> OzonPriceByReturnGoodsFbsOfRmsRepository =>
            _priceByReturnGoodsFBSOfRMSData ??= new PostgresPriceByReturnGoodsFbsOfRmsRepository(_connection);
        public IOzonData<IEnumerable<OperationItemReturnModel>> OzonOperationItemReturnRepository =>
            _operationItemReturnModel ??= new PostgresOperationItemReturnRepository(_connection);
        public IOzonData<IEnumerable<PriceByOperationItemReturnModel>> OzonPriceByOperationItemReturnRepository =>
            _priceByOperationItemReturnData ??= new PostgresPriceByOperationItemReturnRepository(_connection);
        public IOzonData<IEnumerable<PremiumCashbackIndividualPointsModel>> OzonPremiumCashbackIndividualPointsRepository =>
            _premiumCashbackIndividualPointsData ??= new PostgresPremiumCashbackIndividualPointsRepository(_connection);
        public IOzonData<IEnumerable<HoldingForUndeliverableGoodsModel>> OzonHoldingForUndeliverableGoodsRepository =>
            _holdingForUndeliverableGoodsData ??= new PostgresHoldingForUndeliverableGoodsRepository(_connection);


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
