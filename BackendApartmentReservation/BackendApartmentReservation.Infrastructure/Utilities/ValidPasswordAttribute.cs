namespace BackendApartmentReservation.Infrastructure.Utilities
{
    using System.ComponentModel.DataAnnotations;

    public class ValidPasswordAttribute : RegularExpressionAttribute
    {
        private const string _pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$";

        public ValidPasswordAttribute() : base(_pattern)
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var password = value as Password;
            if (password == null)
            {
                return base.IsValid(value, validationContext);
            }

            var passwordString = (string)password;

            if (string.IsNullOrEmpty(passwordString))
            {
                return new ValidationResult("Password cannot be empty");
            }

            return base.IsValid(passwordString, validationContext);
        }
    }
}
