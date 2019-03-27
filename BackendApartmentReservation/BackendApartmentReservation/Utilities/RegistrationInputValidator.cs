using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BackendApartmentReservation.DataContracts.DataTransferObjects.Requests;
using Castle.Core.Internal;

namespace BackendApartmentReservation.Utilities
{
    public static class RegistrationInputValidator
    {
        public static bool IsInputValid(RegisterRequest registerRequest)
        {
            var isValidEmail = Regex.IsMatch(registerRequest.Email, @"^[^@]+@[^@]+\.[^@]+$");
            var isValidOffice = Regex.IsMatch(registerRequest.Password, @" ^ (?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$"); //1+ lowercase, 1+ uppercase, 1+number
            return isValidOffice && isValidEmail;
        }
    }
}
