using AutoMapper;
using YandexCloud.CORE.DTOs;
using YandexCloud.CORE.Models;
using YandexCloud.CORE.Repositories;

namespace YandexCloud.CORE.Services
{
    public class BlService : IBlService
    {
        readonly IOzonFullData _ozonFullData;
        readonly IUoW _uoW;

        public BlService(IOzonFullData ozon, IUoW uoW)
        {
            _ozonFullData = ozon;
            _uoW = uoW;
        }

        public async Task GetDataAsync(RequestDataDto requestDto)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<RequestDataDto, RequestDataModel>()).CreateMapper();
            var requestModel = mapper.Map<RequestDataDto, RequestDataModel>(requestDto);

            try
            {
                var ozonData = new List<OzonResponseModel>();

                ozonData.Add(await _ozonFullData.GetDeliveryDataAsync(requestModel));

                var pageCount = ozonData.First().result.page_count;

                for (int i = 2; i <= pageCount; i++)
                {
                    requestModel.Page = i;
                    ozonData.Add(await _ozonFullData.GetDeliveryDataAsync(requestModel));
                }

                var ozonDataList = new List<OzonDataDto>();

                foreach (var item in ozonData)
                {
                    foreach (var data in item.result.operations)
                    {
                        ozonDataList.Add(new OzonDataDto
                        {
                            date = Convert.ToDateTime(data.operation_date),
                            sku = data.items[0].sku.ToString(),
                            name = data.items[0].name,
                            posting_number = data.posting.posting_number,
                            accruals_for_sale = data.accruals_for_sale.Value,
                            sale_comission = data.sale_commission
                        });
                    }
                }

                await _uoW.OpenTransactionAsync();
                await _uoW.OzonMainDataRepository.CreateAsync(ozonDataList);
                await _uoW.CommitAsync();

            }
            catch (Exception)
            {
                await _uoW.RollbackAsync();
                throw;
            }
        } 

    }
}