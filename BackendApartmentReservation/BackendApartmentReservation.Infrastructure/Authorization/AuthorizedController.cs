using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace BackendApartmentReservation.Infrastructure.Authorization
{
    public class AuthorizedController : ControllerBase
    {
        public string GetEmployeeId()
        {
            return User?.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
        }
    }
}
