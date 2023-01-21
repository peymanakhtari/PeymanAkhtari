using Microsoft.AspNetCore.Identity;

namespace PeymanAkhtari.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Address { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
