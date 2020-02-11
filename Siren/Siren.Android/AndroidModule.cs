using Ninject.Modules;

namespace Siren.Droid
{
    public class AndroidModule : NinjectModule
    {
        public override void Load()
        {
            Bind<AppContextWrapper>().ToSelf().InSingletonScope();
        }
    }
}