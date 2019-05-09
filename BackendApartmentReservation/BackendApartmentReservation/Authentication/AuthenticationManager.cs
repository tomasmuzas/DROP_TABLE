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

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JwtTokenSecret"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, authorisation.Employee.ExternalEmployeeId)
                }),

                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new EmployeeAuthenticationInfo
            {
                JwtToken = tokenHandler.WriteToken(token)
            };
        }
    }
}
