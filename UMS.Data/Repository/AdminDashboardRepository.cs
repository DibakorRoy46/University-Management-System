using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Data.Data;
using UMS.Data.IRepository;
using UMS.Models.Models;

namespace UMS.Data.Repository
{
    public class AdminDashboardRepository : IAdminDashboardRepository
    {
        private readonly ApplicationDbContext _db;
        public AdminDashboardRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IEnumerable<ApplicationUser>> EmployeeList()
        {
            var employeeList=await (from users in _db.ApplicationUsers join userRoles in _db.UserRoles on
                                   users.Id equals userRoles.UserId join roles in _db.Roles on
                                   userRoles.RoleId equals roles.Id where roles.Name!="Student" && 
                                   roles.Name!="Faculty" select users).ToListAsync();
            return employeeList;
        }

        public async Task<IEnumerable<IdentityRole>> RoleList()
        {
            return await _db.Roles.ToListAsync();

        }

        public async Task<IEnumerable<ApplicationUser>> StudentList()
        {
           var studentList=await (from users in _db.ApplicationUsers join userRoles in _db.UserRoles on
                                   users.Id equals userRoles.UserId join roles in _db.Roles on
                                   userRoles.RoleId equals roles.Id where roles.Name=="Student" 
                                   select users).ToListAsync();
            return studentList;
        }

        public async Task<IEnumerable<ApplicationUser>> TeacherList()
        {
             var teacherList=await (from users in _db.ApplicationUsers join userRoles in _db.UserRoles on
                                   users.Id equals userRoles.UserId join roles in _db.Roles on
                                   userRoles.RoleId equals roles.Id where roles.Name=="Faculty" 
                                   select users).ToListAsync();
            return teacherList;
        }
    }
}
