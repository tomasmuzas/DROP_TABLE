using Autofac;
using BackendApartmentReservation.Managers;

namespace BackendApartmentReservation.Infrastructure.Containers
{
    public class IoCConfig : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EmployeeManager>()
                .As<IEmployeeManager>();
        }
    }
}
