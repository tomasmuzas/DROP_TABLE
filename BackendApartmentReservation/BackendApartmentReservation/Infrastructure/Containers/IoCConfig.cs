using Autofac;
using BackendApartmentReservation.Managers;

namespace BackendApartmentReservation.Infrastructure.Containers
{
    using Repositories;

    public class IoCConfig : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EmployeeManager>()
                .As<IEmployeeManager>();
            builder.RegisterType<EmployeeRepository>().As<IEmployeeRepository>();
        }
    }
}
