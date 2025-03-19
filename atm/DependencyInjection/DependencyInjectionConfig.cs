using Ninject;
using Ninject.Modules;
using atm.Interfaces;
using atm.Services;

namespace atm.DependencyInjection
{
    public class DependencyInjectionConfig : NinjectModule
    {
        public override void Load()
        {
            Bind<IUserService>().To<UserService>().WithConstructorArgument("connectionString", "server=127.0.0.1;user=atm_user;database=midterm;password=password");
            Bind<ICustomerService>().To<CustomerService>().WithConstructorArgument("connectionString", "server=127.0.0.1;user=atm_user;database=midterm;password=password");
            Bind<IAdministratorService>().To<AdministratorService>().WithConstructorArgument("connectionString", "server=127.0.0.1;user=atm_user;database=midterm;password=password");
        }
    }
}
