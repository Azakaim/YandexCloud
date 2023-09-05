using YandexCloud.CORE.DTOs;
using YandexCloud.CORE.Repositories;

namespace YandexCloud.BD.Ozon
{
    public class WebOzonData : IOzonFullData
    {
        public async Task<OzonDataDto> GetOzonDataAsync()
        {
            return new OzonDataDto();
        }
    }
}
