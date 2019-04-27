using System;
using System.Collections.Generic;
using System.Text;

namespace BackendApartmentReservation.Tests.DTO
{
    using System.ComponentModel.DataAnnotations;
    using DataContracts.DataTransferObjects.Requests;
    using Xunit;

    public class RegisterRequestModelStateTests
    {
        [Theory]
        [InlineData(null, false)]
        [InlineData("", false)]
        [InlineData("a", false)]
        [InlineData("A", false)]
        [InlineData("1", false)]
        [InlineData("aaaaa", false)]
        [InlineData("GoodPassword1", true)]
        [InlineData("StrongSecurePassword123", true)]
        [InlineData("StrongSecurePassword123With$pec1a;///{}Chars", true)]
        public void RegisterRequest_AllowsStandardPasswords(string password, bool shouldBeValid)
        {
            var registerRequest = new RegisterRequest
            {
                FirstName = "Name",
                LastName = "Surname",
                Email = "good@email.com",
                Office = "as",
                Password = password
            };

            var context = new ValidationContext(registerRequest);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(registerRequest, context, validationResults, true);
            Assert.Equal(shouldBeValid, isValid);
        }
    }
}
