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
    public class CourseContentRepository : Repository<CourseContent>, ICourseContentRepository
    {
        private readonly ApplicationDbContext _db;
        public CourseContentRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public async Task<int> CountAsync(string searchValue)
        {
            var courseList = await _db.CourseContents.ToListAsync();
            if(!String.IsNullOrEmpty(searchValue))
            {
                courseList = await _db.CourseContents.Where(x => x.Name.Contains(searchValue)).ToListAsync();
            }
            return courseList.Count();
        }

        public async Task<IEnumerable<CourseContent>> SearchAsync(string searchValue, int pageNo, int pageSize)
        {
            var courseList = await _db.CourseContents.ToListAsync();
            if (!String.IsNullOrEmpty(searchValue))
            {
                courseList = await _db.CourseContents.Where(x => x.Name.Contains(searchValue)).ToListAsync();
            }
            return courseList.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
        }

        public async Task UpdateAsync(CourseContent courseContent)
        {
            var courseContentObj = await _db.CourseContents.FirstOrDefaultAsync(x => x.Id == courseContent.Id);
            if(courseContentObj!=null)
            {
                courseContentObj.Name = courseContent.Name;
                courseContentObj.Content  = courseContent.Content;
            }
        }
    }
}
