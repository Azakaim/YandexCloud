using YandexCloud.CORE.DTOs;
using YandexCloud.CORE.DTOs.RequestsDto;
using YandexCloud.CORE.Repositories;

namespace YandexCloud.CORE.BL.RequestHandlers
{
    public class HoldingForUndeliverableGoodsHandler : BaseRequestHandler
    {
        public HoldingForUndeliverableGoodsHandler(IOzonFullData ozonFullData, IUoW uow) : base(ozonFullData, uow)
        {
        }

        protected override string GetOperationType() => "OperationMarketplaceWithHoldingForUndeliverableGoods";

        protected override async Task<CommonRequestDto> HandleRequest(Operation data)
        {
            return await Task.Run(() => { 
                var holdingForUndeliverableGoodsData = new List<HoldingForUndeliverableGoodsModel>();
                
                holdingForUndeliverableGoodsData.Add(new HoldingForUndeliverableGoodsModel
                {
                    sku = data.items.FirstOrDefault().sku.ToString(),
                    name = data.items.FirstOrDefault().name,
                    amount = data.amount,
                    posting_number = data.posting.posting_number,
                    date = Convert.ToDateTime(data.operation_date),
                    operation_id = data.operation_id.ToString(),
                    clients_id = _requestDto.ClientId
                });

                return new CommonRequestDto { HoldingForUndeliverableGoodsModels = holdingForUndeliverableGoodsData };
            });
        }
    }
}
