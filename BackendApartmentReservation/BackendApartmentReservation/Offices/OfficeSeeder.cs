using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendApartmentReservation.Database.Entities;

namespace BackendApartmentReservation.Offices
{
    public static class OfficeSeeder
    {
        public static List<DbOffice> GetAllOffices() => new List<DbOffice>
            {
                new DbOffice
                {ExternalOfficeId = "dc2685f75514457785dc8270a01c7c42", Address = "343 W Erie St 600, Chicago", DbApartmentId = 1 },
                new DbOffice
                {ExternalOfficeId = "f693f77d159147668fa4ac92d2fd4e64", Address = "36 Toronto St Suite 260, Toronto", DbApartmentId = 1 },
                new DbOffice
                {ExternalOfficeId = "a4c2052cffb34574ac0c0a84802c122e", Address = "8 Devonshire Square, London", DbApartmentId = 1 },
                new DbOffice
                {ExternalOfficeId = "8fe8bf1bf35445f4b42b96b27ebb1dd1", Address = " A. Juozapavičiaus pr. 11 D, Kaunas", DbApartmentId = 1 },
                new DbOffice
                {ExternalOfficeId = "7d20f6037fe44bbf96f5c7490e72ed8d", Address = " 135 Žalgirio g., Vilnius", DbApartmentId = 1 }
            };
    }
}
