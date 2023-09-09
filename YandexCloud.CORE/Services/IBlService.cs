using YandexCloud.CORE.DTOs;

namespace YandexCloud.CORE.Services
{
    public interface IBlService
    {
        Task GetDataAsync(RequestDataDto requestModel);
    }
}