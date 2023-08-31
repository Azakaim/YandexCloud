namespace YandexCloud.BD
{
    public interface IDB
    {
        public void ConnectDB();
        public Task ReaderDB();
    }
}