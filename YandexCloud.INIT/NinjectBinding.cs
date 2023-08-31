using YandexCloud.BD;

namespace YandexCloud.INIT
{
    public class NinjectBinding : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {
            Bind<IDB>().To<DB>();
        }
    }
}
