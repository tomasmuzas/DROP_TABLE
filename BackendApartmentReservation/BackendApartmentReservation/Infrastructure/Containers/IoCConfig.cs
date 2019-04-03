using Autofac;
using BackendApartmentReservation.Managers;
using BackendApartmentReservation.Repositories;

namespace BackendApartmentReservation.Infrastructure.Containers
{
    public class IoCConfig : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EmployeeManager>()
                .As<IEmployeeManager>();
            builder.RegisterType<EmployeeRepository>()
                .As<IEmployeeRepository>();
        }
    }
}
