using Frankentime.Domain.Analytics;
using Ninject.Modules;

namespace Frankentime.WPF
{
    public class IocConfiguration : NinjectModule
    {
        public override void Load()
        {
            Bind<IAnalytics>().To<PreemptiveAnalytics>().InSingletonScope();
        }
    }
}
