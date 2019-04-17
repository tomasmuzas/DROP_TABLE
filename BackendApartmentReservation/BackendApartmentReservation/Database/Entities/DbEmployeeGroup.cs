namespace BackendApartmentReservation.Database.Entities
{
    public class DbEmployeeGroup
    {
        public int EmployeeId { get; set; }

        public int GroupId { get; set; }

        public DbEmployee DbEmployee { get; set; }

        public DbGroup DbGroup { get; set; }
    }
}
