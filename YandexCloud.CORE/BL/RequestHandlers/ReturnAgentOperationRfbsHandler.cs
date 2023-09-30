using YandexCloud.CORE.DTOs;
using YandexCloud.CORE.Repositories;

namespace YandexCloud.CORE.BL.RequestHandlers
{
    public class ReturnAgentOperationRfbsHandler : BaseRequestHandler
    {
        readonly List<ReturnAgentOperationRFBSModel> _returnAgentOperationRFBSData;

        public ReturnAgentOperationRfbsHandler(IOzonFullData ozonFullData) : base(ozonFullData)
        {
            _returnAgentOperationRFBSData = new List<ReturnAgentOperationRFBSModel>();
        }

        protected override string GetOperationType() => "ReturnAgentOperationRFBS";

        protected override async Task HandleRequest(Operation data)
        {
            await Task.Run(() => {

                _returnAgentOperationRFBSData.Add(new ReturnAgentOperationRFBSModel
                {
                    sku = data.items.FirstOrDefault().sku.ToString(),
                    name = data.items.FirstOrDefault().name,
                    amount = data.amount,
                    posting_number = data.posting.posting_number,
                    date = Convert.ToDateTime(data.operation_date),
                    operation_id = data.operation_id.ToString(),
                    clients_id = _requestDto.ClientId
                });

                _result.ReturnAgentOperationRFBSModels = _returnAgentOperationRFBSData;
            });
        }
    }
}
