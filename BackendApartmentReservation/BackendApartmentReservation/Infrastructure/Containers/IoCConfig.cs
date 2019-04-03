using Autofac;

using BackendApartmentReservation.Infrastructure.Logging;
using BackendApartmentReservation.Repositories;

namespace BackendApartmentReservation.Infrastructure.Containers
{
    public class IoCConfig : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ProfileRepository>().As<IProfileRepository>();
        }
    }
}
