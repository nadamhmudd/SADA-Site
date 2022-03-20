using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SADA.Core.Interfaces;
using SADA.Core.Models;
using SADA.Service;
using SADA.Service.Settings;

namespace SADA.DataAccess;
public class DbInitializer : IDbInitializer
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private ApplicationDbContext _db;
    private readonly MailSetting _mailSettings;

    public DbInitializer(
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager,
        ApplicationDbContext db,
        IOptions<MailSetting> mailSettings)
    {
        _db = db;
        _roleManager  = roleManager;
        _userManager  = userManager;
        _mailSettings = mailSettings.Value;
    }

    public void Initialize()
    {
        //check migrations if they are not applied, no need to updata-database command again
        try
        {
            if(_db.Database.GetPendingMigrations().Count() > 0)
            {
                _db.Database.Migrate();
            }
        }
        catch (Exception)
        {
        }

        //create roles if they are not created
        if (!_roleManager.RoleExistsAsync(SD.Role_Admin).GetAwaiter().GetResult())
        {
            _seedRoles();

            //if roles are not created, then we will create admin user as well
            _createAdmin();
        }

        return;
    }

    //----------------Helper Methods------------------------------------
    private void _seedRoles()
    {
        _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();
        _roleManager.CreateAsync(new IdentityRole(SD.Role_User)).GetAwaiter().GetResult();
    }

    private void _createAdmin()
    {
        _userManager.CreateAsync(new ApplicationUser
        {
            UserName = _mailSettings.Email,
            Email = _mailSettings.Email,
            Name = "SADA Admin",
        }, "Admin123*").GetAwaiter().GetResult();

        ApplicationUser user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == _mailSettings.Email);

        _userManager.AddToRoleAsync(user, SD.Role_Admin).GetAwaiter().GetResult();
    }
}
