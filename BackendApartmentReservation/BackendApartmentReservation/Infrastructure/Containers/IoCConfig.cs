using Autofac;

using BackendApartmentReservation.Infrastructure.Logging;

namespace BackendApartmentReservation.Infrastructure.Containers
{
    public class IoCConfig : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(GenericNLogLogger<>))
                .As(typeof(ICustomLogger<>));
        }
    }
}
