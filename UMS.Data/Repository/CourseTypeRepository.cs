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
    public class CourseTypeRepository : Repository<CourseType>, ICourseTypeRepository
    {
        private readonly ApplicationDbContext _db;
        public CourseTypeRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }
        public async Task<int> CountAsync(string searchValue)
        {
            IEnumerable<CourseType> courseTypeList = await _db.CourseTypes.ToListAsync();
            if(!String.IsNullOrEmpty(searchValue))
            {
                courseTypeList = await _db.CourseTypes.Where(x => x.Name.Contains(searchValue)).ToListAsync();
            }
            else
            {
                courseTypeList = courseTypeList.ToList();
            }
            return courseTypeList.Count();
        }

        public async Task<IEnumerable<CourseType>> SearchAsync(string searchValue, int pageNo, int pageSize)
        {
            IEnumerable<CourseType> courseTypeList = await _db.CourseTypes.ToListAsync();
            if (!String.IsNullOrEmpty(searchValue))
            {
                courseTypeList = await _db.CourseTypes.Where(x => x.Name.Contains(searchValue)).ToListAsync();
            }
            else
            {
                courseTypeList = courseTypeList.ToList();
            }
            return courseTypeList.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
        }

        public async Task UpdateAsync(CourseType courseType)
        {
            CourseType courseTypeObj = await _db.CourseTypes.FirstOrDefaultAsync(x => x.Id == courseType.Id);
            if(courseTypeObj!=null)
            {
                courseTypeObj.Name = courseType.Name;
            }
        }
    }
}
