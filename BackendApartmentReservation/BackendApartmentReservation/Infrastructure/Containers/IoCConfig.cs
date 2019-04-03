using Autofac;
<<<<<<< HEAD
using BackendApartmentReservation.Managers;
=======

using BackendApartmentReservation.Infrastructure.Logging;
>>>>>>> 84ed0fd37a8ec8214c0155573e5c9ac7d3e6421a
using BackendApartmentReservation.Repositories;

namespace BackendApartmentReservation.Infrastructure.Containers
{
    public class IoCConfig : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
<<<<<<< HEAD
            builder.RegisterType<EmployeeManager>()
                .As<IEmployeeManager>();
            builder.RegisterType<EmployeeRepository>()
                .As<IEmployeeRepository>();
=======
            builder.RegisterType<ProfileRepository>().As<IProfileRepository>();
>>>>>>> 84ed0fd37a8ec8214c0155573e5c9ac7d3e6421a
        }
    }
}
