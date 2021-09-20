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
    public class CourseProtoTypeRepository : Repository<CourseProtoType>, ICourseProtoTypeRepository
    {
        private readonly ApplicationDbContext _db;
        public CourseProtoTypeRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }
        public async Task<int> CountAsync(string searchValue)
        {
            IEnumerable<CourseProtoType> courseProtoTypeList = await _db.CourseProtoTypes.ToListAsync();
            if(!String.IsNullOrEmpty(searchValue))
            {
                courseProtoTypeList = await _db.CourseProtoTypes.Where(x => x.Name.Contains(searchValue)).ToListAsync();
            }
            else
            {
                courseProtoTypeList = courseProtoTypeList.ToList();
            }
            return courseProtoTypeList.Count();
        }

        public async Task<IEnumerable<CourseProtoType>> SearchAsync(string searchValue, int pageNo, int pageSize)
        {
            IEnumerable<CourseProtoType> courseProtoTypeList = await _db.CourseProtoTypes.ToListAsync();
            if (!String.IsNullOrEmpty(searchValue))
            {
                courseProtoTypeList = await _db.CourseProtoTypes.Where(x => x.Name.Contains(searchValue)).ToListAsync();
            }
            else
            {
                courseProtoTypeList = courseProtoTypeList.ToList();
            }
            return courseProtoTypeList.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
        }

        public async Task UpdateAsync(CourseProtoType courseProtoType)
        {
            CourseProtoType courseProtoTypeObj = await _db.CourseProtoTypes.FirstOrDefaultAsync(x => x.Id == courseProtoType.Id);
            if(courseProtoTypeObj!=null)
            {
                courseProtoTypeObj.Name = courseProtoType.Name;
                courseProtoTypeObj.Credit = courseProtoType.Credit;
            }
        }
    }
}
