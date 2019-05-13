using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BackendApartmentReservation.Authentication.AuthorizationRequirements.AdminOnly;
using BackendApartmentReservation.Authentication.Interfaces;
using BackendApartmentReservation.Employees;
using Microsoft.AspNetCore.Authorization;

namespace BackendApartmentReservation.Authentication.AuthorizationRequirements.EmployeeOnly
{
    public class EmployeeOnlyHandler : 
        AuthorizationHandler<EmployeeOnlyRequirement>
    {
        private readonly IAuthenticationManager _authenticationManager;

        public EmployeeOnlyHandler(IAuthenticationManager authenticationManager)
        {
            _authenticationManager = authenticationManager;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, EmployeeOnlyRequirement requirement)
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
                if (employee.Role != EmployeeRole.Admin)
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
