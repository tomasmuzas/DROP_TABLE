using System;

namespace BackendApartmentReservation.Confirmations
{
    using System.Linq;
    using System.Threading.Tasks;
    using Database;
    using Database.Entities;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class ConfirmationRepository : IConfirmationRepository
    {
        private readonly DatabaseContext _db;

        public ConfirmationRepository(DatabaseContext db)
        {
            _db = db;
        }

        public bool HasAcceptedTripParticipation(string employeeId, string tripId)
        {
            return HasAccepted(ConfirmationType.TripParticipation, employeeId, tripId);
        }

        public bool HasAcceptedTripMerge(string employeeId, string tripId)
        {
            return HasAccepted(ConfirmationType.TripMerge, employeeId, tripId);
        }

        public async Task CreateConfirmation(DbEmployee employee, DbTrip trip, ConfirmationType type)
        {
            var confirmation = new DbConfirmation
            {
                Employee = employee,
                Trip = trip,
                ExternalConfirmationId = Guid.NewGuid().ToString(),
                Type = type
            };

            await _db.Confirmations.AddAsync(confirmation);
        }

        private bool HasAccepted(ConfirmationType type, string employeeId, string tripId)
        {
            return _db.Confirmations
                .Include(c => c.Employee)
                .Include(c => c.Trip)
                .Any(c => c.Type == type &&
                    c.Employee.ExternalEmployeeId == employeeId &&
                    c.Trip.ExternalTripId == tripId
                    && c.AcceptedAt != null);
        }
    }
}
