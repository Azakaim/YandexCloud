using YandexCloud.CORE.DTOs;

namespace YandexCloud.BD
{
    public interface IOzonData<in T> where T : class
    {
        Task CreateAsync(T model);
    }
}