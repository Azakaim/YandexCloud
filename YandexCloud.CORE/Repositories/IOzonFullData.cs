using YandexCloud.CORE.DTOs;
using YandexCloud.CORE.Models;

namespace YandexCloud.CORE.Repositories
{
    public interface IOzonFullData
    {
        Task<OzonResponseModel> GetDeliveryDataAsync(RequestDataModel requestModel);
    }
}
