using YandexCloud.CORE.DTOs;

namespace YandexCloud.BD
{
    public interface IOzonMainData
    {
        Task CreateAsync(IEnumerable<OzonDataDto> data);
    }
}