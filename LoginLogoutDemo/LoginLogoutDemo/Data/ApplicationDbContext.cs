using LoginLogoutDemo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LoginLogoutDemo.Data;

// IdentityDbContext<TUser, TRole, TKey> creates these tables:
//   AspNetUsers        -> ApplicationUser  (our custom user)
//   AspNetRoles        -> ApplicationRole  (our custom role - the FK target)
//   AspNetUserRoles    -> Junction table   (FK: UserId -> AspNetUsers, RoleId -> AspNetRoles)
//   AspNetUserClaims, AspNetRoleClaims, AspNetUserLogins, AspNetUserTokens
public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Seed roles directly into the database so they exist on first migration
        builder.Entity<ApplicationRole>().HasData(
            new ApplicationRole
            {
                Id = "1",
                Name = "Admin",
                NormalizedName = "ADMIN",
                Description = "Full access to all features including user management"
            },
            new ApplicationRole
            {
                Id = "2",
                Name = "User",
                NormalizedName = "USER",
                Description = "Standard user with access to dashboard only"
            }
        );
    }
}
