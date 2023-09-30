using YandexCloud.CORE.DTOs;
using YandexCloud.CORE.Repositories;

namespace YandexCloud.CORE.BL.RequestHandlers
{
    public class ClientReturnAgentHandler : BaseRequestHandler
    {
        readonly List<ClientReturnAgentOperationModel> _clientReturnAgentData;

        public ClientReturnAgentHandler(IOzonFullData ozonFullData) : base(ozonFullData)
        {
            _clientReturnAgentData = new List<ClientReturnAgentOperationModel>();
        }

        protected override string GetOperationType() => "ClientReturnAgentOperation";

        protected override async Task HandleRequest(Operation data)
        {
            await Task.Run(() => { 
                _clientReturnAgentData.Add(new ClientReturnAgentOperationModel
                {
                    sku = data.items.FirstOrDefault().sku.ToString(),
                    name = data.items.FirstOrDefault().name,
                    amount = data.amount,
                    posting_number = data.posting.posting_number,
                    date = Convert.ToDateTime(data.operation_date),
                    operation_id = data.operation_id.ToString(),
                    clients_id = _requestDto.ClientId
                });

                _result.ClientReturnAgentOperationModels = _clientReturnAgentData;
            });
        }
    }
}
