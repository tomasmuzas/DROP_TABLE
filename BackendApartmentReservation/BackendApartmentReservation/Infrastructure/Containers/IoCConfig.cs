using Autofac;

using BackendApartmentReservation.Infrastructure.Logging;
using BackendApartmentReservation.Repositories;
using BackendApartmentReservation.Managers;
using BackendApartmentReservation.Repositories;

namespace BackendApartmentReservation.Infrastructure.Containers
{
    public class IoCConfig : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ProfileRepository>().As<IProfileRepository>();
            builder.RegisterType<EmployeeManager>()
                .As<IEmployeeManager>();
            builder.RegisterType<EmployeeRepository>()
                .As<IEmployeeRepository>();
        }
    }
}
