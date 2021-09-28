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
    public class ActivityRepository:Repository<Activity>,IActivityRepository
    {
        private readonly ApplicationDbContext _db;
        public ActivityRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public async Task UpdateAsync(Activity activity)
        {
            var activityObj = await _db.Activities.FirstOrDefaultAsync(x => x.Id.Equals(activity.Id));
            if(activityObj!=null)
            {
                activityObj.Name = activity.Name;
                activityObj.StartDate = activity.StartDate;
                activityObj.EndDate = activity.EndDate;
                activityObj.IsActive = activity.IsActive;
            }
        }

        public async Task<int> CountAsync(string search)
        {
            var activityList = await _db.Activities.ToListAsync();
            if(!String.IsNullOrEmpty(search))
            {
                activityList = await _db.Activities.Where(x => x.Name.ToLower().Contains(search.ToLower())).ToListAsync();
            }
            return activityList.Count();
        }

        public async Task<IEnumerable<Activity>> SearchAsync(string search, int pageNo, int pageSize)
        {
            var activityList = await _db.Activities.ToListAsync();
            if (!String.IsNullOrEmpty(search))
            {
                activityList = await _db.Activities.Where(x => x.Name.ToLower().Contains(search.ToLower())).ToListAsync();
            }
            return activityList.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
        }

     
    }
}
