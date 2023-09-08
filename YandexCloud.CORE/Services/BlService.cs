using YandexCloud.BD;
using YandexCloud.CORE.DTOs;
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

        public async Task GetDataAsync()
        {
            try
            {
                var ozonData = await _ozonFullData.GetDeliveryDataAsync();
                var ozonDataList = new List<OzonDataDto>();

                foreach (var item in ozonData.result.operations)
                {
                    ozonDataList.Add(new OzonDataDto
                    {
                        date = Convert.ToDateTime(item.operation_date),
                        sku = item.items[0].sku.ToString(),
                        name = item.items[0].name,
                        posting_number = item.posting.posting_number,
                        accruals_for_sale = item.accruals_for_sale,
                        sale_comission = item.sale_commission
                    });
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