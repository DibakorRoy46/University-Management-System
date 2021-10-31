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
    public class EmployeeDetialsRepository : Repository<EmployeeDetials>, IEmployeeDetialsRepository
    {
        private readonly ApplicationDbContext _db;
        public EmployeeDetialsRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }
        public async Task UpdateAsync(EmployeeDetials employeeDetials)
        {
            var employeeDetailsObj = await _db.EmployeeDetials.FirstOrDefaultAsync(x => x.Id == employeeDetials.Id);
            if(employeeDetailsObj!=null)
            {
                employeeDetailsObj.LeavingDate = employeeDetials.LeavingDate;
                employeeDetailsObj.Salary = employeeDetials.Salary;
            }
        }
    }
}
