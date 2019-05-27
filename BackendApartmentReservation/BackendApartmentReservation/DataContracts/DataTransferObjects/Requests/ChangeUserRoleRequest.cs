using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BackendApartmentReservation.Employees;

namespace BackendApartmentReservation.DataContracts.DataTransferObjects.Requests
{
    public class ChangeUserRoleRequest
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public EmployeeRole Role { get; set; }
    }
}
