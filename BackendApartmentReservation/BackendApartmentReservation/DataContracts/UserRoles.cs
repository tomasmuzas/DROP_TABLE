using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendApartmentReservation.DataContracts
{
    public enum UserRoles
    {
        Admin,
        User
    }

    public static class UserRoleString
    {
        public const string Admin = "Admin";
        public const string User = "User";
    }
}
