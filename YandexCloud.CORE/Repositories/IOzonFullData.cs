using YandexCloud.CORE.DTOs;

namespace YandexCloud.CORE.Repositories
{
    public interface IOzonFullData
    {
        Task<OzonResponseModel> GetDeliveryDataAsync();
    }
}
