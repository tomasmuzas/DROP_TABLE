namespace BackendApartmentReservation.Infrastructure.Logging
{
    public interface ICustomLogger<T>
    {
        void Info(string message);

        void Warn(string message);

        void Error(string message);
    }
}