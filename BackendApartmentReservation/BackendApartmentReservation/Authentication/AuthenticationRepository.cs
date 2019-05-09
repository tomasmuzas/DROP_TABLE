using System.Threading.Tasks;
using BackendApartmentReservation.Authentication.Interfaces;
using BackendApartmentReservation.Database;
using BackendApartmentReservation.Database.Entities;
using BackendApartmentReservation.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace BackendApartmentReservation.Authentication
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly DatabaseContext _db;

        public AuthenticationRepository(DatabaseContext db)
        {
            _db = db;
        }

        public async Task<DbAuthorization> Authorize(string email, string hashedPassword)
        {
            var authorization = await 
                _db.Authorizations.
                    Include(a => a.Employee)
                    .SingleOrDefaultAsync(a => a.Email == email && a.Password == hashedPassword);

            if (authorization == null)
            {
                throw new ErrorCodeException(ErrorCodes.EmployeeNotFound);
            }

            return authorization;
        }

        public async Task CreateAuthenticationInfo(string hashedPassword, DbEmployee employee)
        {
            var authorization = new DbAuthorization
            {
                Email = employee.Email,
                Password = hashedPassword,
                Employee = employee
            };

            await _db.Authorizations.AddAsync(authorization);
        }
    }
}
