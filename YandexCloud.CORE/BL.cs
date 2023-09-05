using YandexCloud.BD;

namespace YandexCloud.CORE
{
    public class BL : IBL
    {
        IDB _dataBase;

        public BL(IDB dataBase)
        {
            _dataBase = dataBase;
        }

        public void BasisLogik()
        {
            _dataBase.ConnectDB();
        }
    }
}