using Microsoft.AspNetCore.Identity;

namespace LoginLogoutDemo.Models;

// Custom role - extends IdentityRole to add extra columns
// This becomes the AspNetRoles table with an additional "Description" column
public class ApplicationRole : IdentityRole
{
    public string? Description { get; set; }
}
