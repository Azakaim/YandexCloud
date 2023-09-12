using YandexCloud.BD;
using YandexCloud.BD.Postgres;
using YandexCloud.CORE.DTOs;

namespace YandexCloud.CORE.Repositories
{
    public interface IUoW : IDisposable
    {
        IOzonData<IEnumerable<OzonFirstDataDto>> OzonMainDataRepository { get; }
        IOzonStores OzonStoresRepository { get; }
        IOzonSecondDataRepository OzonSecondDataRepository { get; }
        IServiceNamesRepository OzonServiceNamesRepository { get; }
        IOzonData<IEnumerable<OzonAcquiringDataDto>> OzonAcquiringRepository { get; }

        Task CommitAsync();
        Task OpenTransactionAsync();
        Task RollbackAsync();
    }
}
