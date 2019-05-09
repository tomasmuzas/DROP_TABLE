using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendApartmentReservation.Database.Entities;
using BackendApartmentReservation.Offices;
using Microsoft.EntityFrameworkCore;

namespace BackendApartmentReservation.Database
{
    public static class DatabaseContextExtensions 
    {
        public static void Seed(this DatabaseContext db)
        {
            foreach (var office in OfficeSeeder.GetAllOffices())
            {
                var matchingOffice = db.Offices.SingleOrDefault(o => o.ExternalOfficeId == office.ExternalOfficeId);
                if (matchingOffice == null)
                {
                    db.Offices.Add(office);
                }
                else
                {
                    matchingOffice.Address = office.Address;
                    matchingOffice.OfficeApartment = office.OfficeApartment;
                }
            }

            db.SaveChanges();
        }
    }
}
