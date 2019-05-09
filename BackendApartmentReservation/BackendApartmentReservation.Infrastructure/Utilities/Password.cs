namespace BackendApartmentReservation.Infrastructure.Utilities
{
    public class Password
    {
        private string _value { get; set; }

        public Password(string value)
        {
            _value = value;
        }

        public static implicit operator Password(string value)
        {
            return new Password(value);
        }

        // Implicit is also overriding explicit. More info: https://stackoverflow.com/a/10892855
        public static implicit operator string(Password password)
        {
            return password._value;
        }

        public override string ToString()
        {
            return "########";
        }
    }
}
