using YandexCloud.BD;

namespace YandexCloud.CORE.Repositories
{
    public interface IUoW : IDisposable
    {
        IOzonMainData OzonMainDataRepository { get; }
        IOzonStores OzonStoresRepository { get; }

        Task CommitAsync();
        Task OpenTransactionAsync();
        Task RollbackAsync();
    }
}
