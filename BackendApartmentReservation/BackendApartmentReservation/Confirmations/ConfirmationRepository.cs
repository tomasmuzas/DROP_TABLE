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

        public bool HasDeclinedTripParticipation(string employeeId, string tripId)
        {
            return HasDeclined(ConfirmationType.TripParticipation, employeeId, tripId);
        }

        public bool HasDeclinedTripMerge(string employeeId, string tripId)
        {
            return HasDeclined(ConfirmationType.TripMerge, employeeId, tripId);
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

        private bool HasDeclined(ConfirmationType type, string employeeId, string tripId)
        {
            return _db.Confirmations
                .Include(c => c.Employee)
                .Include(c => c.Trip)
                .Any(c => c.Type == type &&
                    c.Employee.ExternalEmployeeId == employeeId &&
                    c.Trip.ExternalTripId == tripId
                    && c.DeclinedAt != null);
        }

        public async Task AcceptConfirmation(DbConfirmation confirmation)
        {
            confirmation.AcceptedAt = DateTimeOffset.Now;
            _db.Update(confirmation);
            await _db.SaveChangesAsync();
        }

        public async Task DeclineConfirmation(DbConfirmation confirmation)
        {
            confirmation.DeclinedAt = DateTimeOffset.Now;
            _db.Update(confirmation);
            await _db.SaveChangesAsync();
        }

        public async Task<DbConfirmation> GetWaitingConfirmation(ConfirmationType type, string employeeId, string tripId)
        {
            return await _db.Confirmations
                .Include(c => c.Employee)
                .Include(c => c.Trip)
                .Where(c => c.Type == type &&
                    c.Employee.ExternalEmployeeId == employeeId &&
                    c.Trip.ExternalTripId == tripId &&
                    c.AcceptedAt == null &&
                    c.DeclinedAt == null)
                .SingleOrDefaultAsync();
        }
    }
}
