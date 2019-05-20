using Microsoft.AspNetCore.Authorization;

namespace BackendApartmentReservation.Authentication.AuthorizationRequirements.OrganizerOnly
{
    public class OrganizerOnlyAttribute : AuthorizeAttribute
    {
        public OrganizerOnlyAttribute() : base(PolicyNames.OrganizerOnly)
        {

        }
    }
}
