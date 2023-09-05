using YandexCloud.BD;
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
                var ozonData = await _ozonFullData.GetOzonDataAsync();
                await _uoW.OpenTransactionAsync();
                var id = await _uoW.OzonStoresRepository.CreateAsync();
                await _uoW.OzonMainDataRepository.CreateAsync();

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