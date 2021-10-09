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
    class RegistrationCourseRepository:Repository<StudentRegisteationCourse>, IRegistrationCourseRepository
    {
        private readonly ApplicationDbContext _db;
        public RegistrationCourseRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<IEnumerable<AssignRegistrationCourse>> GetAllCourses(string searchValue, Guid departmentId)
        {
            var courseList = await _db.AssignRegistrationCourses.Include(x => x.Courses).ThenInclude(x => x.CourseProtoType).
                Include(x => x.Courses).ThenInclude(x => x.CourseType).Include(x=>x.Section).Include(x=>x.ApplicationUser).
                ToListAsync();
            if (departmentId!=Guid.Empty)
            {
                courseList = await _db.AssignRegistrationCourses.Include(x => x.Courses).ThenInclude(x => x.CourseProtoType).
                Include(x => x.Courses).ThenInclude(x => x.CourseType).Include(x => x.Section).Include(x => x.ApplicationUser)
                .Where(x => x.Courses.DepartmentId == departmentId).
                ToListAsync();
                if (!String.IsNullOrEmpty(searchValue))
                {
                    courseList = courseList.Where(x => x.Courses.DepartmentId == departmentId &&
                    x.Courses.Name.Contains(searchValue)).
                    ToList();
                }
            }          
            else
            {
                if (!String.IsNullOrEmpty(searchValue))
                {
                    courseList = courseList.Where(x => x.Courses.DepartmentId == departmentId &&
                    x.Courses.Name.Contains(searchValue)).
                    ToList();
                }
            }
            return courseList.ToList();
           
        }


        public async Task<IEnumerable<StudentRegisteationCourse>> GetRegisteredCourses(string userId, Guid semesterId, int year)
        {
            if(!String.IsNullOrEmpty(userId))
            {
                var courseList = await _db.StudentRegisteationCourses.Include(x => x.AssignRegistrationCourse).
                    ThenInclude(x => x.Courses).ThenInclude(x => x.CourseProtoType).Include(x => x.AssignRegistrationCourse).
                    ThenInclude(x => x.Courses).ThenInclude(x => x.CourseType).Include(x => x.AssignRegistrationCourse).
                    ThenInclude(x => x.Semester).Include(x => x.AssignRegistrationCourse).ThenInclude(x => x.Section).
                    Include(x => x.ApplicationUser).Where(x => x.StudentId == userId).ToListAsync();
                if(semesterId!=Guid.Empty && year!=0)
                {
                    courseList = await _db.StudentRegisteationCourses.Include(x => x.AssignRegistrationCourse).
                    ThenInclude(x => x.Courses).ThenInclude(x => x.CourseProtoType).Include(x => x.AssignRegistrationCourse).
                    ThenInclude(x => x.Courses).ThenInclude(x => x.CourseType).Include(x => x.AssignRegistrationCourse).
                    ThenInclude(x => x.Semester).Include(x => x.AssignRegistrationCourse).ThenInclude(x => x.Section).
                    Include(x => x.ApplicationUser).Where(x => x.StudentId == userId && x.StudentId==userId 
                    &&x.AssignRegistrationCourse.Year==year).ToListAsync();
                }
                return courseList.ToList();
            }
            return null;
        }

        public async Task<IEnumerable<Guid>> SelectRegistrationCourseId(string userId)
        {
            return _db.StudentRegisteationCourses.Where(x => x.StudentId == userId).Select(x => x.AssignRegiCourseId);
            //var query=("select AssignRegistrationCourses.Id  from AssignRegistrationCourses inner join StudentRegisteationCourses on AssignRegistrationCourses.Id = StudentRegisteationCourses.AssignRegiCourseId where studentId =@userId ");
            //var courseId= (from assignCourse in _db.AssignRegistrationCourses join
            //                     studentCourse in _db.StudentRegisteationCourses  on
            //                     assignCourse.Id equals studentCourse.AssignRegiCourseId 
            //                     where(assignCourse.SemesterId==semesterId && assignCourse.Year==year
            //                     && studentCourse.StudentId==userId) select assignCourse).Select(x=>x.CourseId);
            //return courseId;
           

            
        }


        public async Task<IEnumerable<int>>SelectRegistrationYear(string userId)
        {
            return _db.StudentRegisteationCourses.Include(x => x.AssignRegistrationCourse).
                Where(x => x.StudentId == userId).Select(x => x.AssignRegistrationCourse.Year);
        }
        public async Task<IEnumerable<Guid>>TakenCourseId(string userId)
        {
            return _db.StudentRegisteationCourses.Include(x => x.AssignRegistrationCourse).
                Where(x => x.StudentId == userId).Select(x => x.AssignRegistrationCourse.CourseId);
        }
        public async Task<int>CountRegistered(Guid courseId)
        {
            var courseList= await _db.StudentRegisteationCourses.Where(x => x.AssignRegiCourseId == courseId).ToListAsync();
            return courseList.Count();
        }
    }
}
