using YandexCloud.CORE.DTOs;
using YandexCloud.CORE.DTOs.RequestsDto;
using YandexCloud.CORE.Repositories;

namespace YandexCloud.CORE.BL.RequestHandlers
{
    public class ReturnGoodsFbsOfRmsHandler : BaseRequestHandler
    {
        readonly List<OperationReturnGoodsFBSofRMSModel> _operationReturnGoodsFBSofRMSData;
        readonly List<PriceByReturnGoodsFBSOfRMSModel> _priceByReturnGoodsFBSOfRMSData;


        public ReturnGoodsFbsOfRmsHandler(IOzonFullData ozonFullData) : base(ozonFullData)
        {
            _operationReturnGoodsFBSofRMSData = new List<OperationReturnGoodsFBSofRMSModel>();
            _priceByReturnGoodsFBSOfRMSData = new List<PriceByReturnGoodsFBSOfRMSModel>();
        }

        protected override string GetOperationType() => "OperationReturnGoodsFBSofRMS";

        protected override async Task HandleRequest(Operation data)
        {
            await Task.Run(() => {
                var operationReturnGoodsFBSofRMSDataGuid = Guid.NewGuid().ToString();

                var serviceDelivToCustomer = data.services.FirstOrDefault(s => s.name == "MarketplaceServiceItemReturnAfterDelivToCustomer");
                var serviceFlowLogistic = data.services.FirstOrDefault(s => s.name == "MarketplaceServiceItemReturnFlowLogistic");
                var serviceNotDelivToCustomer = data.services.FirstOrDefault(s => s.name == "MarketplaceServiceItemReturnNotDelivToCustomer");
                var serviceReturnPartGoodsCustomer = data.services.FirstOrDefault(s => s.name == "MarketplaceServiceItemReturnPartGoodsCustomer");

                if (serviceDelivToCustomer != null)
                    _priceByReturnGoodsFBSOfRMSData.Add(new PriceByReturnGoodsFBSOfRMSModel
                    {
                        price = serviceDelivToCustomer.price,
                        ozon_service_name_id = 3,
                        operation_return_goods_fbsof_rms_id = operationReturnGoodsFBSofRMSDataGuid,
                    });

                if (serviceFlowLogistic != null)
                    _priceByReturnGoodsFBSOfRMSData.Add(new PriceByReturnGoodsFBSOfRMSModel
                    {
                        price = serviceFlowLogistic.price,
                        ozon_service_name_id = 4,
                        operation_return_goods_fbsof_rms_id = operationReturnGoodsFBSofRMSDataGuid,
                    });

                if (serviceNotDelivToCustomer != null)
                    _priceByReturnGoodsFBSOfRMSData.Add(new PriceByReturnGoodsFBSOfRMSModel
                    {
                        price = serviceNotDelivToCustomer.price,
                        ozon_service_name_id = 5,
                        operation_return_goods_fbsof_rms_id = operationReturnGoodsFBSofRMSDataGuid,
                    });

                if (serviceReturnPartGoodsCustomer != null)
                    _priceByReturnGoodsFBSOfRMSData.Add(new PriceByReturnGoodsFBSOfRMSModel
                    {
                        price = serviceReturnPartGoodsCustomer.price,
                        ozon_service_name_id = 6,
                        operation_return_goods_fbsof_rms_id = operationReturnGoodsFBSofRMSDataGuid,
                    });

                _operationReturnGoodsFBSofRMSData.Add(new OperationReturnGoodsFBSofRMSModel
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

                _result.OperationReturnGoodsFBSofRMSModels = _operationReturnGoodsFBSofRMSData;
                _result.PriceByReturnGoodsFBSOfRMSModels = _priceByReturnGoodsFBSOfRMSData;
            });
        }
    }
}
