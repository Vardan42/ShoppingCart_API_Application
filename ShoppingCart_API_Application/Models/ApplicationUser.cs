using Microsoft.AspNetCore.Identity;

namespace ShoppingCart_API_Application.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string Name { get;set; }
    }
}
