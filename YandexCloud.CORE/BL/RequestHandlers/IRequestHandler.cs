using YandexCloud.CORE.DTOs;
using YandexCloud.CORE.DTOs.RequestsDto;

namespace YandexCloud.CORE.BL.RequestHandlers
{
    public interface IRequestHandler
    {
        Task<CommonRequestDto> SendRequest(RequestDataDto requestDto, IEnumerable<OzonServiceNamesDto> ozonServiseNames);
    }
}