using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BackendApartmentReservation.Authentication.Interfaces;
using BackendApartmentReservation.DataContracts.DataTransferObjects.Requests;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BackendApartmentReservation.Authentication
{
    using Database.Entities;
    using Infrastructure.Utilities;

    public class AuthenticationManager : IAuthenticationManager
    {
        private readonly IAuthenticationRepository _authenticationRepository;

        private readonly IConfiguration _configuration;

        public AuthenticationManager(
            IConfiguration configuration,
            IAuthenticationRepository authenticationRepository)
        {
            _configuration = configuration;
            _authenticationRepository = authenticationRepository;
        }

        public async Task<EmployeeAuthenticationInfo> Authenticate(AuthenticationRequest request)
        {
            var hash = SecurityManager.GetPasswordHash(request.Password);

            var authorisation =
                await _authenticationRepository.Authorize(request.Email, hash);

            var key = Encoding.ASCII.GetBytes(_configuration["JwtTokenSecret"]);

            var token = new JwtSecurityToken(
                claims: new []
                {
                    new Claim(ClaimTypes.Name, authorisation.Employee.ExternalEmployeeId)
                },
                issuer: _configuration["JwtTokenIssuer"],
                audience: _configuration["JwtTokenIssuer"],
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                expires: DateTime.UtcNow.AddMinutes(60));

            return new EmployeeAuthenticationInfo
            {
                JwtToken = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }

        public async Task CreateAuthenticationInfo(DbEmployee dbEmployee, Password password)
        {
            var hashedPassword = SecurityManager.GetPasswordHash(password);
            await _authenticationRepository.CreateAuthenticationInfo(
                hashedPassword,
                dbEmployee);
        }
    }
}
