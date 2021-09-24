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
    public class SemesterRepository : Repository<Semester>, ISemesterRepository
    {
        private readonly ApplicationDbContext _db;
        public SemesterRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }
        public async Task<int> CountAsync(string searchValue)
        {
            var semesterList = await _db.Semesters.ToListAsync();
            if(!String.IsNullOrEmpty(searchValue))
            {
                semesterList = await _db.Semesters.Where(x => x.Name.Contains(searchValue)).ToListAsync();
            }
            
            return semesterList.Count();
        }

        public async Task<IEnumerable<Semester>> SearchAsync(string searchValue, int pageNo, int pageSize)
        {
            var semesterList = await _db.Semesters.ToListAsync();
            if (!String.IsNullOrEmpty(searchValue))
            {
                semesterList = await _db.Semesters.Where(x => x.Name.Contains(searchValue)).ToListAsync();
            }
            return semesterList.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
        }

        public async Task UpdateAsync(Semester semester)
        {
            var semesterObj = await _db.Semesters.FirstOrDefaultAsync(x => x.Id.Equals(semester.Id));
            if(semesterObj!=null)
            {
                semesterObj.Name = semester.Name;
            }
        }
    }
}
