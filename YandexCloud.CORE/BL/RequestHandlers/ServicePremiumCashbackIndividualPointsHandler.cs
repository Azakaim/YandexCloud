using YandexCloud.CORE.DTOs;
using YandexCloud.CORE.DTOs.RequestsDto;
using YandexCloud.CORE.Repositories;

namespace YandexCloud.CORE.BL.RequestHandlers
{
    public class ServicePremiumCashbackIndividualPointsHandler : BaseRequestHandler
    {
        readonly List<PremiumCashbackIndividualPointsModel> _premiumCashbackIndividualPointsData;

        public ServicePremiumCashbackIndividualPointsHandler(IOzonFullData ozonFullData) : base(ozonFullData)
        {
            _premiumCashbackIndividualPointsData = new List<PremiumCashbackIndividualPointsModel>();
        }

        protected override string GetOperationType() => "OperationMarketplaceServicePremiumCashbackIndividualPoints";

        protected override async Task HandleRequest(Operation data)
        {
            await Task.Run(() => { 
                _premiumCashbackIndividualPointsData.Add(new PremiumCashbackIndividualPointsModel
                {
                    sku = data.items.FirstOrDefault().sku.ToString(),
                    name = data.items.FirstOrDefault().name,
                    amount = data.amount,
                    posting_number = data.posting.posting_number,
                    date = Convert.ToDateTime(data.operation_date),
                    operation_id = data.operation_id.ToString(),
                    clients_id = _requestDto.ClientId
                });

                _result.PremiumCashbackIndividualPointsModels = _premiumCashbackIndividualPointsData;
            });
        }
    }
}
