using YandexCloud.CORE.DTOs;
using YandexCloud.CORE.Repositories;

namespace YandexCloud.CORE.BL.RequestHandlers
{
    public class OperationItemReturnHandler : BaseRequestHandler
    {
        readonly List<OperationItemReturnModel> _operationItemReturnData;
        readonly List<PriceByOperationItemReturnModel> _priceByOperationItemReturnData;


        public OperationItemReturnHandler(IOzonFullData ozonFullData) : base(ozonFullData)
        {
            _operationItemReturnData = new List<OperationItemReturnModel>();
            _priceByOperationItemReturnData = new List<PriceByOperationItemReturnModel>();
        }

        protected override string GetOperationType() => "OperationItemReturn";

        protected override async Task HandleRequest(Operation data)
        {
            await Task.Run(() => {
                var operationReturnGoodsFBSofRMSDataGuid = Guid.NewGuid().ToString();

                var serviceDelivToCustomer = data.services.FirstOrDefault(s => s.name == "MarketplaceServiceItemReturnAfterDelivToCustomer");
                var serviceFlowLogistic = data.services.FirstOrDefault(s => s.name == "MarketplaceServiceItemReturnFlowLogistic");
                var serviceNotDelivToCustomer = data.services.FirstOrDefault(s => s.name == "MarketplaceServiceItemReturnNotDelivToCustomer");
                var serviceReturnPartGoodsCustomer = data.services.FirstOrDefault(s => s.name == "MarketplaceServiceItemReturnPartGoodsCustomer");

                if (serviceDelivToCustomer != null)
                    _priceByOperationItemReturnData.Add(new PriceByOperationItemReturnModel
                    {
                        price = serviceDelivToCustomer.price,
                        ozon_service_name_id = 3,
                        operation_item_return_id = operationReturnGoodsFBSofRMSDataGuid,
                    });

                if (serviceFlowLogistic != null)
                    _priceByOperationItemReturnData.Add(new PriceByOperationItemReturnModel
                    {
                        price = serviceFlowLogistic.price,
                        ozon_service_name_id = 4,
                        operation_item_return_id = operationReturnGoodsFBSofRMSDataGuid,
                    });

                if (serviceNotDelivToCustomer != null)
                    _priceByOperationItemReturnData.Add(new PriceByOperationItemReturnModel
                    {
                        price = serviceNotDelivToCustomer.price,
                        ozon_service_name_id = 5,
                        operation_item_return_id = operationReturnGoodsFBSofRMSDataGuid,
                    });

                if (serviceReturnPartGoodsCustomer != null)
                    _priceByOperationItemReturnData.Add(new PriceByOperationItemReturnModel
                    {
                        price = serviceReturnPartGoodsCustomer.price,
                        ozon_service_name_id = 6,
                        operation_item_return_id = operationReturnGoodsFBSofRMSDataGuid,
                    });

                _operationItemReturnData.Add(new OperationItemReturnModel
                {
                    id = operationReturnGoodsFBSofRMSDataGuid,
                    sku = data.items.FirstOrDefault().sku.ToString(),
                    name = data.items.FirstOrDefault().name,
                    amount = data.amount,
                    posting_number = data.posting.posting_number,
                    date = Convert.ToDateTime(data.operation_date),
                    operation_id = data.operation_id.ToString(),
                    clients_id = _requestDto.ClientId
                });

                _result.OperationItemReturnModels = _operationItemReturnData;
                _result.PriceByOperationItemReturnModels = _priceByOperationItemReturnData;
            });
        }
    }
}
