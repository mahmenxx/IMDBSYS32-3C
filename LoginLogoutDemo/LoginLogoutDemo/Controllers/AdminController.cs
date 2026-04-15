using LoginLogoutDemo.Data;
using LoginLogoutDemo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LoginLogoutDemo.Controllers;

// Only Admin role can access this controller
[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public AdminController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    // ===== DEMONSTRATES: Querying the Role table (AspNetRoles) directly =====
    public async Task<IActionResult> Roles()
    {
        var roles = await _context.Roles.ToListAsync();
        return View(roles);
    }

    // ===== DEMONSTRATES: Getting users with roles via UserManager =====
    public async Task<IActionResult> Users()
    {
        var allUsers = await _context.Users.ToListAsync();
        var userRoleList = new List<UserWithRolesViewModel>();

        foreach (var user in allUsers)
        {
            // UserManager internally queries the FK junction table (AspNetUserRoles)
            var roles = await _userManager.GetRolesAsync(user);
            userRoleList.Add(new UserWithRolesViewModel
            {
                UserId = user.Id,
                FullName = user.FullName,
                Email = user.Email ?? "",
                Roles = roles.ToList()
            });
        }

        return View(userRoleList);
    }

    // ===== DEMONSTRATES: LINQ join across FK tables =====
    // Joins: AspNetUsers -> AspNetUserRoles (junction/FK) -> AspNetRoles
    public async Task<IActionResult> UserRoleDetails()
    {
        var details = await (
            from user in _context.Users
            join userRole in _context.UserRoles on user.Id equals userRole.UserId
            join role in _context.Roles on userRole.RoleId equals role.Id
            select new UserRoleDetailViewModel
            {
                UserId = user.Id,
                FullName = user.FullName,
                Email = user.Email ?? "",
                RoleId = role.Id,
                RoleName = role.Name ?? "",
                RoleDescription = role.Description ?? ""
            }
        ).ToListAsync();

        return View(details);
    }
}

// ViewModel: user with their role names
public class UserWithRolesViewModel
{
    public string UserId { get; set; } = "";
    public string FullName { get; set; } = "";
    public string Email { get; set; } = "";
    public List<string> Roles { get; set; } = new();
}

// ViewModel: explicit FK join result showing all columns
public class UserRoleDetailViewModel
{
    public string UserId { get; set; } = "";
    public string FullName { get; set; } = "";
    public string Email { get; set; } = "";
    public string RoleId { get; set; } = "";
    public string RoleName { get; set; } = "";
    public string RoleDescription { get; set; } = "";
}
