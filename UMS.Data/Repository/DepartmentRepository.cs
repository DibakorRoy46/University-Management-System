using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UMS.Data.Data;
using UMS.Data.IRepository;
using UMS.Models.Models;

namespace UMS.Data.Repository
{
    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        private readonly ApplicationDbContext _db;
        public DepartmentRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public async Task<int> CountAsync(string searchValue)
        {
            IEnumerable<Department> departmentList = await _db.Departments.ToListAsync();
            if(!String.IsNullOrEmpty(searchValue))
            {
                departmentList = departmentList.
                    Where(x => (x.Name.Contains(searchValue) || x.Initial.Contains(searchValue))).ToList();
            }
            else
            {
                departmentList = departmentList.ToList();
            }
            return departmentList.Count();
        }

        public async Task<IEnumerable<Department>> SearchAsync(string searchValue, int pageNo = 1, int pageSize = 10)
        {
            IEnumerable<Department> departmentList = await _db.Departments.ToListAsync();
            if (!String.IsNullOrEmpty(searchValue))
            {
                departmentList = _db.Departments.
                    Where(x => (x.Name.Contains(searchValue) || x.Initial.Contains(searchValue))).ToList();
            }
            else
            {
                departmentList = departmentList.ToList();
            }
            return departmentList.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
        }

        public async Task UpdateAsync(Department department)
        {
            Department departmentObj = await _db.Departments.FirstOrDefaultAsync(x => x.Id == department.Id);
            if(departmentObj!=null)
            {
                departmentObj.Name = department.Name;
                departmentObj.Initial = department.Initial;
                departmentObj.RequiredCreditToComplete = department.RequiredCreditToComplete;
                departmentObj.RequireCourseToComplete = department.RequireCourseToComplete;
                departmentObj.PricePerCredit = department.PricePerCredit;
            }
        }
    }
}
