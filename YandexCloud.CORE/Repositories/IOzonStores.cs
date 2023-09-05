using YandexCloud.CORE.DTOs;

namespace YandexCloud.CORE.Repositories
{
    public interface IOzonStores
    {
        Task<int> CreateAsync(OzonDataDto ozonDataDto);
    }
}
