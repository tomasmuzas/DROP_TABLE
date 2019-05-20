using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BackendApartmentReservation.Authentication.AuthorizationRequirements.AdminOnly;
using BackendApartmentReservation.Authentication.Interfaces;
using BackendApartmentReservation.Employees;
using Microsoft.AspNetCore.Authorization;

namespace BackendApartmentReservation.Authentication.AuthorizationRequirements.OrganizerOnly
{
    public class OrganizerOnlyHandler :
        AuthorizationHandler<AdminOnlyRequirement>
    {
        private readonly IAuthenticationManager _authenticationManager;

        public OrganizerOnlyHandler(IAuthenticationManager authenticationManager)
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
                var employee = await _authenticationManager.GetAndVerifyEmployee(employeeId);
                if (employee.Role != EmployeeRole.Admin || employee.Role != EmployeeRole.Organizer)
                {
                    context.Fail();
                    return;
                }
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
