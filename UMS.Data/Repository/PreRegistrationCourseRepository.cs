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
    public class PreRegistrationCourseRepository : Repository<AssignPreRegistrationCourse>, IPreRegistrationCourseRepository
    {
        private readonly ApplicationDbContext _db;
        public PreRegistrationCourseRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }
        public async Task<IEnumerable<Course>> GetAllCourses(string searchValue, Guid departmentId)
        {
            var courseList = await _db.Courses.Where(x => x.DepartmentId == departmentId).
                    Include(x => x.CourseProtoType).Include(x => x.CourseType).Include(x => x.CourseToCoursePrerequisites).ToListAsync();
            if (!String.IsNullOrEmpty(searchValue))
            {
                courseList = await _db.Courses.Where(x => x.Name.Contains(searchValue) && x.DepartmentId == departmentId).
                  Include(x => x.CourseProtoType).Include(x => x.CourseType).Include(x => x.CourseToCoursePrerequisites).ToListAsync();
            }
            return courseList;
        }
        public async Task<IEnumerable<Course>> GetAllCourses(string searchValue)
        {
            var courseList = await _db.Courses.
                    Include(x => x.CourseProtoType).Include(x => x.CourseType).Include(x => x.CourseToCoursePrerequisites).ToListAsync();
            if (!String.IsNullOrEmpty(searchValue))
            {
                courseList = await _db.Courses.Where(x => x.Name.Contains(searchValue)).
                  Include(x => x.CourseProtoType).Include(x => x.CourseType).Include(x => x.CourseToCoursePrerequisites).ToListAsync();
            }
            return courseList;
        }

        public async Task<IEnumerable<AssignPreRegistrationCourse>> GetPreCourses(string userId)
        {         
            if(!String.IsNullOrEmpty(userId))
            {
                var  courseList = await _db.PreRegistrationCourses.Include(x=>x.Course).ThenInclude(x=>x.CourseProtoType)                  
                    .Where(x => x.StudentId==userId).ToListAsync();
                return courseList.ToList();
            }
            return null;            
        }

        public async Task<IEnumerable<Guid>> SelectPreCourseId(string userId)
        {
            return  _db.PreRegistrationCourses.Where(x => x.StudentId == userId).Select(x => x.CourseId);
        }
    }
}
