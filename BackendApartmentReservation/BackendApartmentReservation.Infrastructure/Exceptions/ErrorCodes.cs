namespace BackendApartmentReservation.Infrastructure.Exceptions
{
    public static class ErrorCodes
    {
        public const string GenericInternalServerError = "generic.internal_error";
        public const string InvalidEntity = "generic.invalid_entity";
        public const string EmployeeNotFound = "employee.employee_not_found";

        // Trip
        public const string TripNotFound = "trip.trip_not_found";
        public const string TripOfficeNotFound = "trip.office_not_found";

        // Checklist
        public const string ChecklistAlreadyExists = "checklist.checklist_already_exists";
        public const string ChecklistFlightAlreadyExists = "checklist.flight_already_exists";
        public const string ChecklistCarAlreadyExists = "checklist.car_rent_already_exists";
        public const string ChecklistLivingPlaceAlreadyExists = "checklist.living_place_already_exists";
    }
}
