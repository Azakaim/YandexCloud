using AutoMapper;
using YandexCloud.CORE.DTOs;
using YandexCloud.CORE.DTOs.RequestsDto;
using YandexCloud.CORE.Models;
using YandexCloud.CORE.Repositories;

namespace YandexCloud.CORE.BL.RequestHandlers
{
    public abstract class BaseRequestHandler : IRequestHandler
    {
        readonly IOzonFullData _ozonFullData;

        private protected RequestDataDto _requestDto;
        private protected CommonRequestDto _result;
        private protected IEnumerable<OzonServiceNamesDto> _ozonServiseNames;

        public BaseRequestHandler(IOzonFullData ozonFullData)
        {
            _ozonFullData = ozonFullData;
        }

        public async Task<CommonRequestDto> SendRequest(RequestDataDto requestDto, IEnumerable<OzonServiceNamesDto> ozonServiseNames)
        {
            _requestDto = requestDto;
            _result = new CommonRequestDto();
            _ozonServiseNames = ozonServiseNames;

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<RequestDataDto, RequestDataModel>()).CreateMapper();
            var requestModel = mapper.Map<RequestDataDto, RequestDataModel>(requestDto);
            requestModel.OperationType = GetOperationType();

            var ozonData = new List<OzonResponseModel>
            {
                await _ozonFullData.GetDeliveryDataAsync(requestModel)
            };

            var pageCount = ozonData.First().result.page_count;

            for (int i = 2; i <= pageCount; i++)
            {
                requestModel.Page = i;
                ozonData.Add(await _ozonFullData.GetDeliveryDataAsync(requestModel));
            }

            foreach (var item in ozonData)
            {
                foreach (var data in item.result.operations)
                {
                    await HandleRequest(data);
                }
            }

            return _result;
        }

        protected abstract Task HandleRequest(Operation data);
        protected abstract string GetOperationType();
    }
}
