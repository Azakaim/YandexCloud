using YandexCloud.CORE.BL.Managers;
using YandexCloud.CORE.BL.RequestHandlers;
using YandexCloud.CORE.DTOs;
using YandexCloud.CORE.Repositories;

namespace YandexCloud.CORE.Services
{
    public class BlService : IBlService
    {
        readonly IUoW _uoW;
        readonly IEnumerable<IRequestHandler> _requestHandler;

        public event Action<string> OzonEventHandler;

        public BlService(IUoW uoW, IEnumerable<IRequestHandler> requestHandler)
        {
            _uoW = uoW;
            _requestHandler = requestHandler;
        }

        public async Task GetDataAsync(IEnumerable<RequestDataDto> requestDto)
        {
            var managerList = new List<OzonManager>();

            try
            {
                foreach (var dto in requestDto)
                {
                    var manager = new OzonManager(_uoW, _requestHandler);
                    var clientModel = new OzonClientModel { id = dto.ClientId, api_key = dto.ApiKey };

                    var clientModelFromDb = await _uoW.OzonClientRepository.ReadAsync();
                    if (!clientModelFromDb.Any(c => c.id == clientModel.id))
                        await _uoW.OzonClientRepository.CreateAsync(new List<OzonClientModel> { clientModel });

                    manager.OzonEventHandler += str => {
                        OzonEventHandler?.Invoke(str);
                    };
                    managerList.Add(manager);

                    await manager.HandleOzonData(dto);
                }

                foreach (var item in managerList)
                {
                    item.OzonEventHandler -= str => {
                        OzonEventHandler?.Invoke(str);
                    };
                }
            }
            catch (Exception ex)
            {
                OzonEventHandler?.Invoke(ex.Message);
                throw;
            }

        }
    }
}