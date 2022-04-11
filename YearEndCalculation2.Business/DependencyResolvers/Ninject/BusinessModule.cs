
using Ninject.Modules;
using YearEndCalculation2.Business.Abstract;
using YearEndCalculation2.Business.Concrete.Managers;

namespace YearEndCalculation2.Business.DependencyResolvers.Ninject
{
    public class BusinessModule:NinjectModule
    {
        public override void Load()
        {
            Bind<IYearEndService>().To<YearEndManager>().InSingletonScope();

        }
    }
}
