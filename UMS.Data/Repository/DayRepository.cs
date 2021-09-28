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
    public class DayRepository : Repository<Day>, IDayRepository
    {
        private readonly ApplicationDbContext _db;
        public DayRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public async Task<int> CountAsync(string searchValue)
        {
            var dayList = await _db.Days.ToListAsync();
            if(!String.IsNullOrEmpty(searchValue))
            {
                dayList = await _db.Days.Where(x => x.Name.ToLower().Contains(searchValue.ToLower())).ToListAsync();
            }
            return dayList.Count();
        }

        public async Task<IEnumerable<Day>> SearchAsync(string searchValue, int pageNo, int pageSize)
        {
            var dayList = await _db.Days.ToListAsync();
            if (!String.IsNullOrEmpty(searchValue))
            {
                dayList = await _db.Days.Where(x => x.Name.ToLower().Contains(searchValue.ToLower())).ToListAsync();
            }
            return dayList.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
        }

        public async Task UpdateAsync(Day day)
        {
            var dayObj = await _db.Days.FirstOrDefaultAsync(x => x.Id.Equals(day.Id));
            if(dayObj!=null)
            {
                dayObj.Name = day.Name;
            }
        }
    }
}
