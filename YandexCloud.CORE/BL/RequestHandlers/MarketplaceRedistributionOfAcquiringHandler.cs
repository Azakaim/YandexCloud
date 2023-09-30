using YandexCloud.CORE.DTOs;
using YandexCloud.CORE.Repositories;

namespace YandexCloud.CORE.BL.RequestHandlers
{
    public class MarketplaceRedistributionOfAcquiringHandler : BaseRequestHandler
    {
        readonly List<OzonAcquiringDataDto> _ozonAcquiringData;

        public MarketplaceRedistributionOfAcquiringHandler(IOzonFullData ozonFullData) : base(ozonFullData)
        {
            _ozonAcquiringData = new List<OzonAcquiringDataDto>();
        }

        protected override string GetOperationType() => "MarketplaceRedistributionOfAcquiringOperation";

        protected override async Task HandleRequest(Operation data)
        {
            await Task.Run(() => {

                _ozonAcquiringData.Add(new OzonAcquiringDataDto
                {
                    sku = data.items.FirstOrDefault().sku.ToString(),
                    name = data.items.FirstOrDefault().name,
                    amount = data.amount,
                    posting_number = data.posting.posting_number,
                    date = Convert.ToDateTime(data.operation_date),
                    operation_id = data.operation_id.ToString(),
                    clients_id = _requestDto.ClientId
                });

                _result.OzonAcquiringDataDtos = _ozonAcquiringData;
            });
        }
    }
}
