using Microsoft.AspNetCore.Authorization;

namespace BackendApartmentReservation.Authentication.AuthorizationRequirements.AdminOnly
{
    public class AdminOnlyAttribute : AuthorizeAttribute
    {
        public AdminOnlyAttribute() : base(PolicyNames.AdminOnly)
        {

        }
    }
}
