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
    public class CoursePrerequisiteRepository : Repository<CoursePrerequisite>, ICoursePrerequisiteRepository
    {
        private readonly ApplicationDbContext _db;
        public CoursePrerequisiteRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }
        public async Task<int> CountAsync(string searchValue)
        {
            IEnumerable<CoursePrerequisite> coursePrerequisiteList = await _db.CoursePrerequisites.ToListAsync();
                
            if(!String.IsNullOrEmpty(searchValue))
            {
                coursePrerequisiteList = await _db.CoursePrerequisites.
                Where(x => (x.Name.ToLower().Contains(searchValue.ToLower())) ||
                (x.InitialName.ToLower().Equals(searchValue.ToLower()))).ToListAsync();
            }
            else
            {
                coursePrerequisiteList = coursePrerequisiteList.ToList();
            }
            return coursePrerequisiteList.Count();
        }

        public async Task<IEnumerable<CoursePrerequisite>> SearchAsync(string searchValue, int pageNo, int pageSize)
        {
            IEnumerable<CoursePrerequisite> coursePrerequisiteList = await _db.CoursePrerequisites.
             ToListAsync();
            if (!String.IsNullOrEmpty(searchValue))
            {
                coursePrerequisiteList = await _db.CoursePrerequisites.
                Where(x => (x.Name.ToLower().Contains(searchValue.ToLower())) ||
                (x.InitialName.ToLower().Contains(searchValue.ToLower()))).ToListAsync();
            }
            return  coursePrerequisiteList.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
        }

        public async Task UpdateAsync(CoursePrerequisite coursePrerequisite)
        {
            CoursePrerequisite coursePrerequisiteObj = await _db.CoursePrerequisites.
                FirstOrDefaultAsync(x => x.Id.Equals(coursePrerequisite.Id));
            if(coursePrerequisiteObj!=null)
            {
                coursePrerequisiteObj.Name = coursePrerequisite.Name;
                coursePrerequisiteObj.InitialName = coursePrerequisite.InitialName;
               
            }
        }
    }
}
