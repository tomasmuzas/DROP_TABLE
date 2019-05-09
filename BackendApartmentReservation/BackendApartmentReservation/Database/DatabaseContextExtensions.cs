using System.Linq;
using BackendApartmentReservation.Offices;

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
