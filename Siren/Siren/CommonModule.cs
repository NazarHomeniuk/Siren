using Ninject.Modules;
using Siren.Contracts.Services;
using Siren.Services;

namespace Siren
{
    public class CommonModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IHttpService>().To<HttpService>();
            Bind<IAuthorizationService>().To<AuthorizationService>();
            Bind<IProfileService>().To<ProfileService>();
        }
    }
}
