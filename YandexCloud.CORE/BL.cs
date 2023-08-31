using YandexCloud.BD;

namespace YandexCloud.CORE
{
    public class BL
    {
        IDB _dataBase;

        public BL(IDB dataBase)
        {
            _dataBase = dataBase;
        }

        public void BasisLogik()
        {
            _dataBase.ConnectDB();
            _dataBase.ReaderDB();
        }
    }
}