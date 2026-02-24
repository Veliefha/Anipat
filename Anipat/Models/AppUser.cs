using Microsoft.AspNetCore.Identity;

namespace Anipat.Models
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
