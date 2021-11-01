using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Data.Data;
using UMS.Models.Models;

namespace UMS.Data.Initializer
{
    public class DbInitializer:IDbInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(ApplicationDbContext db, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async void Initialize()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception ex)
            {

            }


            if (!_roleManager.RoleExistsAsync("Super Admin").GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole("Super Admin")).GetAwaiter().GetResult();
               
            }
            else
            {
                return;
            }

            _userManager.CreateAsync(new ApplicationUser
            {
                UserName = "dibakorroy56@gmail.com",
                Email = "dibakorroy56@gmail.com",
                EmailConfirmed = true,
                Name = "Dibakor Roy",
                PhoneNumber = "01784932050"
            }, "AdminDibakor_123@").GetAwaiter().GetResult();

            ApplicationUser user =await _db.ApplicationUsers.FirstOrDefaultAsync(u => u.Email == "dibakorroy56@gmail.com");
            _userManager.AddToRoleAsync(user, "Super Admin").GetAwaiter().GetResult();
        }
    }
}
