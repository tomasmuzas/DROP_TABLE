using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BackendApartmentReservation.Authentication.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace BackendApartmentReservation.Authentication.AuthorizationRequirements.AdminOnly
{
    public class AdminOnlyHandler : 
        AuthorizationHandler<AdminOnlyRequirement>
    {
        private readonly IAuthenticationManager _authenticationManager;

        public AdminOnlyHandler(IAuthenticationManager authenticationManager)
        {
            _authenticationManager = authenticationManager;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminOnlyRequirement requirement)
        {
            var employeeId = context.User?.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

            if (string.IsNullOrEmpty(employeeId))
            {
                context.Fail();
                return;
            }

            try
            {
                await _authenticationManager.GetAndVerifyEmployee(employeeId);
            }
            catch (ArgumentException)
            {
                context.Fail();
                return;
            }
            
            context.Succeed(requirement);
        }
    }
}
