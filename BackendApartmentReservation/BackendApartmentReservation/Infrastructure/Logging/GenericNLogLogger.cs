using NLog;

namespace BackendApartmentReservation.Infrastructure.Logging
{
    public class GenericNLogLogger<T> : ICustomLogger<T> where T : class
    {
        private readonly Logger _logger;

        public GenericNLogLogger()
        {
            _logger = LogManager.GetLogger(typeof(T).FullName);
        }

        public void Info(string message)
        {
            _logger.Info(message);
        }

        public void Warn(string message)
        {
            _logger.Warn(message);
        }

        public void Error(string message)
        {
            _logger.Error(message);
        }
    }
}
