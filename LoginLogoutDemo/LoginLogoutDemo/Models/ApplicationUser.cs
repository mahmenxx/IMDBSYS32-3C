using Microsoft.AspNetCore.Identity;

namespace LoginLogoutDemo.Models;

// Custom user - extends IdentityUser to add extra columns
public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; } = string.Empty;
}
