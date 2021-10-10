using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Models.Models;

namespace UMS.Data.IRepository
{
    public  interface IAdminDashboardRepository
    {
        Task<IEnumerable<ApplicationUser>> StudentList();
        Task<IEnumerable<ApplicationUser>> TeacherList();
        Task<IEnumerable<ApplicationUser>> EmployeeList();
        Task<IEnumerable<IdentityRole>> RoleList();
    }
}
