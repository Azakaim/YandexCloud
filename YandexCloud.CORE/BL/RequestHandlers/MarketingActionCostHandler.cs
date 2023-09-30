using YandexCloud.CORE.DTOs;
using YandexCloud.CORE.DTOs.RequestsDto;
using YandexCloud.CORE.Repositories;

namespace YandexCloud.CORE.BL.RequestHandlers
{
    public class MarketingActionCostHandler : BaseRequestHandler
    {
        readonly List<OzonMarketingActionCostModel> _ozonMarketingActionData;

        public MarketingActionCostHandler(IOzonFullData ozonFullData) : base(ozonFullData) 
        {
            _ozonMarketingActionData = new List<OzonMarketingActionCostModel>();
        }

        protected override string GetOperationType() => "MarketplaceMarketingActionCostOperation";

        protected override async Task HandleRequest(Operation data)
        {
            await Task.Run(() =>
            {
                _ozonMarketingActionData.Add(new OzonMarketingActionCostModel
                {
                    amount = data.amount,
                    date = Convert.ToDateTime(data.operation_date),
                    operation_id = data.operation_id.ToString(),
                    clients_id = _requestDto.ClientId
                });

                _result.OzonMarketingActionCostModels = _ozonMarketingActionData;
            });
        }
    }
}
