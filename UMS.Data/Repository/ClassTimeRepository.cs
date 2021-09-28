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
    public class ClassTimeRepository : Repository<ClassTime>, IClassTimeRepository
    {
        private readonly ApplicationDbContext _db;
        public ClassTimeRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public async Task<int> CountAsync(DateTime searchValue)
        {
            var classTimeList = await _db.ClassTimes.ToListAsync();
            if(searchValue!=DateTime.MinValue)
            {
                classTimeList = await _db.ClassTimes.Where(x => x.Time.TimeOfDay == searchValue.TimeOfDay).ToListAsync();

            }
            return classTimeList.Count();
        }

        public async Task<IEnumerable<ClassTime>> SearchAsync(DateTime searchValue, int pageNo, int pageSize)
        {
            var classTimeList = await _db.ClassTimes.ToListAsync();
            if (searchValue != DateTime.MinValue)
            {
                classTimeList = await _db.ClassTimes.Where(x => x.Time.TimeOfDay == searchValue.TimeOfDay).ToListAsync();

            }
            return classTimeList.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
        }

        public async Task UpdateAsync(ClassTime classTime)
        {
            var classTimeObj = await _db.ClassTimes.FirstOrDefaultAsync(x => x.Id.Equals(classTime.Id));
            if(classTimeObj!=null)
            {
                classTimeObj.Time = classTime.Time;
            }

        }
    }
}
