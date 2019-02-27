using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace reactdotnet.Data.Entities
{
    public class Role : IdentityRole<int>
    {
        public List<User> Users { get; set; }
    }
}