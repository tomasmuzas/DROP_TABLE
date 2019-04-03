using Autofac;

using BackendApartmentReservation.Infrastructure.Logging;
using BackendApartmentReservation.Repositories;
using BackendApartmentReservation.Managers;

namespace BackendApartmentReservation.Infrastructure.Containers
{
    public class IoCConfig : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EmployeeManager>()
                .As<IEmployeeManager>();
            builder.RegisterType<ProfileRepository>().As<IProfileRepository>();
        }
    }
}
