using YandexCloud.BD;
using YandexCloud.BD.Postgres;

namespace YandexCloud.CORE.Repositories
{
    public interface IUoW : IDisposable
    {
        IOzonMainData OzonMainDataRepository { get; }
        IOzonStores OzonStoresRepository { get; }
        IOzonSecondDataRepository OzonSecondDataRepository { get; }
        IServiceNamesRepository OzonServiceNamesRepository { get; }

        Task CommitAsync();
        Task OpenTransactionAsync();
        Task RollbackAsync();
    }
}
