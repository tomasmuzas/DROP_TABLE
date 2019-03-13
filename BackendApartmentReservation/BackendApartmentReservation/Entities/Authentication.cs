using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendApartmentReservation.Entities
{
    public class Authentication
    {
        public int Id { get; set; }
        public int ProfileId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
