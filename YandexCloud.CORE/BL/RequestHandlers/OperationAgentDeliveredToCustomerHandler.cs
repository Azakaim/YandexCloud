using YandexCloud.CORE.DTOs;
using YandexCloud.CORE.DTOs.RequestsDto;
using YandexCloud.CORE.Repositories;

namespace YandexCloud.CORE.BL.RequestHandlers
{
    public class OperationAgentDeliveredToCustomerHandler : BaseRequestHandler
    {
        List<OzonFirstTableModel> _ozonFirstDataList;
        List<OzonSecondDataDto> _ozonSecondDataList;

        public OperationAgentDeliveredToCustomerHandler(IOzonFullData ozonFullData, IUoW uow) : base(ozonFullData, uow)
        {
            _ozonFirstDataList = new List<OzonFirstTableModel>();
            _ozonSecondDataList = new List<OzonSecondDataDto>();
        }

        protected override string GetOperationType() => "OperationAgentDeliveredToCustomer";

        protected override async Task HandleRequest(Operation data)
        {
            var id = Guid.NewGuid();
            _ozonFirstDataList.Add(new OzonFirstTableModel
            {
                id = id.ToString(),
                date = Convert.ToDateTime(data.operation_date),
                sku = data.items[0].sku.ToString(),
                name = data.items[0].name,
                posting_number = data.posting.posting_number,
                accruals_for_sale = data.accruals_for_sale.Value,
                sale_comission = data.sale_commission,
                clients_id = _requestDto.ClientId
            });

            var ozonServiseNames = await _uow.OzonServiceNamesRepository.GetAsync();

            var service = ozonServiseNames.FirstOrDefault();
            var serviceFromResponse = data.services.FirstOrDefault(n => n.name == service.name);

            _ozonSecondDataList.Add(new OzonSecondDataDto
            {
                first_table_id = id.ToString(),
                price = serviceFromResponse?.price,
                ozon_service_names_id = 1
            });

            service = ozonServiseNames.FirstOrDefault(i => i.id == 2);
            serviceFromResponse = data.services.FirstOrDefault(n => n.name == service.name);

            _ozonSecondDataList.Add(new OzonSecondDataDto
            {
                first_table_id = id.ToString(),
                price = serviceFromResponse?.price,
                ozon_service_names_id = 2
            });

            _result.FirstSecondRequest = new FirstSecondRequestDto();
            _result.FirstSecondRequest.OzonFirstDataList = _ozonFirstDataList;
            _result.FirstSecondRequest.OzonSecondDataList = _ozonSecondDataList;
        }
    }
}