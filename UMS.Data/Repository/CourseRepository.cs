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
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        private readonly ApplicationDbContext _db;
        public CourseRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }
        public async Task<int> CountAsync(string searchValue, Guid depaertmentId)
        {
            IEnumerable<Course> couseList = await _db.Courses.Include(x => x.CourseType).
                Include(x => x.CourseProtoType).Include(x => x.Department).
                Include(x=>x.CourseToCoursePrerequisites).ThenInclude(x=>x.CoursePrerequisite)
                .ToListAsync();
            if(!String.IsNullOrEmpty(searchValue))
            {
                couseList = await _db.Courses.Include(X=>X.Department).
                    Include(x=>x.CourseType).Include(x=>x.CourseProtoType).
                    Include(x=>x.CourseToCoursePrerequisites).
                    ThenInclude(x=>x.CoursePrerequisite).
                    Where(x => ((x.Name.ToLower().Contains(searchValue.ToLower())))||
                    (x.Initial.ToLower().Contains(searchValue.ToLower()))||
                    (x.Department.Name.ToLower().Contains(searchValue.ToLower()))||
                    (x.Department.Initial.ToLower().Contains(searchValue.ToLower())) ||
                    (x.CourseProtoType.Name.ToLower().Contains(searchValue.ToLower()))||
                    (x.CourseType.Name.ToLower().Contains(searchValue.ToLower()))).ToListAsync();
                if(!depaertmentId.Equals(Guid.Empty))
                {
                    couseList = couseList.Where(x => x.DepartmentId.Equals(depaertmentId)).ToList();
                }                
            }
            else
            {
                couseList = couseList.ToList();
                if (!depaertmentId.Equals(Guid.Empty))
                {
                    couseList = couseList.Where(x => x.DepartmentId.Equals(depaertmentId)).ToList();
                }
            }
            return couseList.Count();
        }

        public async Task<IEnumerable<Course>> GetCourseByDepartment(Guid id, Guid courseId)
        {
            if (!id.Equals(Guid.Empty))
            {
                var courseList = await _db.Courses.Where(x => x.DepartmentId.Equals(id)).ToListAsync();

                if (!courseId.Equals(Guid.Empty))
                {
                    courseList = await _db.Courses.Where(x => x.DepartmentId.Equals(id) && x.Id != courseId).ToListAsync();
                }
                return (IEnumerable<Course>)courseList;
            }
            return null;
        }
            //var courseList = await (from p in _db.CoursePrerequisites
            //                        join cpc in _db.CourseToCoursePrerequisites on p.Id equals cpc.CoursePreId into mcpc
            //                        from cpct in mcpc.DefaultIfEmpty()
            //                        join c in _db.Courses on cpct.CourseId equals c.Id into cctp
            //                        from f in cctp.DefaultIfEmpty()    
            //                        select p).Where(x=>x.CourseToCoursePrerequisites.Any(x=>x.Course.DepartmentId==id)).ToListAsync();
        public async Task<IEnumerable<Course>> SearchAsync(string searchValue, Guid depaertmentId, int pageNo, int pageSize)
        {

            IEnumerable<Course> couseList = await _db.Courses.Include(x => x.CourseType).
                Include(x => x.CourseProtoType).Include(x => x.Department).
                Include(x=>x.CourseToCoursePrerequisites).ThenInclude(x=>x.CoursePrerequisite)
                .ToListAsync();
            if (!String.IsNullOrEmpty(searchValue))
            {
                couseList = await _db.Courses.Include(X => X.Department).
                    Include(x => x.CourseType).Include(x => x.CourseProtoType).
                    Include(x => x.CourseToCoursePrerequisites).
                    ThenInclude(x=>x.CoursePrerequisite).
                    Where(x => ((x.Name.ToLower().Equals(searchValue.ToLower()))) ||
                    (x.Initial.ToLower().Equals(searchValue.ToLower())) ||
                    (x.Department.Name.ToLower().Equals(searchValue.ToLower())) ||
                    (x.Department.Initial.ToLower().Equals(searchValue.ToLower())) ||
                    (x.CourseProtoType.Name.ToLower().Equals(searchValue.ToLower())) ||
                    (x.CourseType.Name.ToLower().Equals(searchValue.ToLower()))).ToListAsync();
                if (!depaertmentId.Equals(Guid.Empty))
                {
                    couseList = couseList.Where(x => x.DepartmentId.Equals(depaertmentId)).ToList();
                }
            }
            else
            {
                couseList = couseList.ToList();
                if (!depaertmentId.Equals(Guid.Empty))
                {
                    couseList = couseList.Where(x => x.DepartmentId.Equals(depaertmentId)).ToList();
                }
            }
            return  couseList.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
        }

        public async Task UpdateAsync(Course course)
        {
            Course courseObj = await _db.Courses.FirstOrDefaultAsync(x => x.Id.Equals(course.Id));
            if(courseObj!=null)
            {
                courseObj.Name = course.Name;
                courseObj.Initial = course.Initial;
                courseObj.DepartmentId = course.DepartmentId;
                courseObj.CourseProtoTypeId = course.CourseProtoTypeId;
                courseObj.CourseTypeId = course.CourseTypeId;
            }
        }
    }
}
