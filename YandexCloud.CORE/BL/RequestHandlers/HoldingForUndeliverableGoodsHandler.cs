using YandexCloud.CORE.DTOs;
using YandexCloud.CORE.Repositories;

namespace YandexCloud.CORE.BL.RequestHandlers
{
    public class HoldingForUndeliverableGoodsHandler : BaseRequestHandler
    {
        readonly List<HoldingForUndeliverableGoodsModel> _holdingForUndeliverableGoodsData;

        public HoldingForUndeliverableGoodsHandler(IOzonFullData ozonFullData) : base(ozonFullData)
        {
            _holdingForUndeliverableGoodsData = new List<HoldingForUndeliverableGoodsModel>();
        }

        protected override string GetOperationType() => "OperationMarketplaceWithHoldingForUndeliverableGoods";

        protected override async Task HandleRequest(Operation data)
        {
            await Task.Run(() => { 
                _holdingForUndeliverableGoodsData.Add(new HoldingForUndeliverableGoodsModel
                {
                    sku = data.items.FirstOrDefault().sku.ToString(),
                    name = data.items.FirstOrDefault().name,
                    amount = data.amount,
                    posting_number = data.posting.posting_number,
                    date = Convert.ToDateTime(data.operation_date),
                    operation_id = data.operation_id.ToString(),
                    clients_id = _requestDto.ClientId
                });

                _result.HoldingForUndeliverableGoodsModels = _holdingForUndeliverableGoodsData;
            });
        }
    }
}
