using YandexCloud.CORE.DTOs;
using YandexCloud.CORE.DTOs.RequestsDto;
using YandexCloud.CORE.Repositories;

namespace YandexCloud.CORE.BL.RequestHandlers
{
    public class MarketingActionCostHandler : BaseRequestHandler
    {
        public MarketingActionCostHandler(IOzonFullData ozonFullData, IUoW uow) : base(ozonFullData, uow) { }

        protected override string GetOperationType() => "MarketplaceMarketingActionCostOperation";

        protected override async Task<CommonRequestDto> HandleRequest(Operation data)
        {
            return await Task.Run(() =>
            {
                var ozonMarketingActionData = new List<OzonMarketingActionCostModel>();

                ozonMarketingActionData.Add(new OzonMarketingActionCostModel
                {
                    amount = data.amount,
                    date = Convert.ToDateTime(data.operation_date),
                    operation_id = data.operation_id.ToString(),
                    clients_id = _requestDto.ClientId
                });

                return new CommonRequestDto { OzonMarketingActionCostModels = ozonMarketingActionData };
            });
        }
    }
}
