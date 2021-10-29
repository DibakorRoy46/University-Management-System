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

        public async Task<IEnumerable<AssignRegistrationCourse>> GetAllCourses(string searchValue, Guid departmentId,Guid semesterId)
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
                    if(semesterId!=Guid.Empty)
                    {
                        courseList = courseList.Where(x => x.SemesterId == semesterId).ToList();
                    }
                }
                else
                {
                    if (semesterId != Guid.Empty)
                    {
                        courseList = courseList.Where(x => x.SemesterId == semesterId).ToList();
                    }
                }

            }          
            else
            {
                if (!String.IsNullOrEmpty(searchValue))
                {
                    courseList = courseList.Where(x => x.Courses.DepartmentId == departmentId &&
                    x.Courses.Name.Contains(searchValue)).
                    ToList();
                    if (semesterId != Guid.Empty)
                    {
                        courseList = courseList.Where(x => x.SemesterId == semesterId).ToList();
                    }
                }
                else
                {
                    if (semesterId != Guid.Empty)
                    {
                        courseList = courseList.Where(x => x.SemesterId == semesterId).ToList();
                    }
                }
            }
            return courseList.ToList();          
        }


        public async Task<IEnumerable<StudentRegisteationCourse>> GetRegisteredCourses(string userId, Guid semesterId)
        {
            if(!String.IsNullOrEmpty(userId))
            {
                var courseList = await _db.StudentRegisteationCourses.Include(x => x.AssignRegistrationCourse).
                    ThenInclude(x => x.Courses).ThenInclude(x => x.CourseProtoType).Include(x => x.AssignRegistrationCourse).
                    ThenInclude(x => x.Courses).ThenInclude(x => x.CourseType).Include(x => x.AssignRegistrationCourse).
                    ThenInclude(x => x.Semester).Include(x => x.AssignRegistrationCourse).ThenInclude(x => x.Section).
                    Include(x => x.ApplicationUser).Where(x => x.StudentId == userId).ToListAsync();
                if(semesterId!=Guid.Empty)
                {
                    courseList = await _db.StudentRegisteationCourses.Include(x => x.AssignRegistrationCourse).
                    ThenInclude(x => x.Courses).ThenInclude(x => x.CourseProtoType).Include(x => x.AssignRegistrationCourse).
                    ThenInclude(x => x.Courses).ThenInclude(x => x.CourseType).Include(x => x.AssignRegistrationCourse).
                    ThenInclude(x => x.Semester).Include(x => x.AssignRegistrationCourse).ThenInclude(x => x.Section).
                    Include(x => x.ApplicationUser).Where(x => x.StudentId == userId && x.AssignRegistrationCourse.SemesterId==semesterId 
                    ).ToListAsync();
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

        public async Task UpdateAsync(StudentRegisteationCourse studentRegisteationCourse)
        {
            var studentRegistrationCourseObj = await _db.StudentRegisteationCourses.
                FirstOrDefaultAsync(x => x.StudentId == studentRegisteationCourse.StudentId &&
                x.AssignRegiCourseId == studentRegisteationCourse.AssignRegiCourseId);
            if(studentRegistrationCourseObj!=null)
            {
                studentRegistrationCourseObj.AttendanceMark = studentRegisteationCourse.AttendanceMark;
                studentRegistrationCourseObj.AssignmentMark = studentRegisteationCourse.AssignmentMark;
                studentRegistrationCourseObj.MidTermMark = studentRegisteationCourse.MidTermMark;
                studentRegistrationCourseObj.FinalTermMark = studentRegisteationCourse.FinalTermMark;
                studentRegistrationCourseObj.Grade = studentRegisteationCourse.Grade;
            }
        }
        public async Task<bool>GetTimeAvilabity(string userId,Guid semesterId,string firstDate,string secondDate,DateTime startTime)
        {
            var courseListTime = await _db.StudentRegisteationCourses.Where(x => x.StudentId == userId
                      && x.AssignRegistrationCourse.SemesterId == semesterId && x.AssignRegistrationCourse.FirstDate == firstDate
                       &&
                      x.AssignRegistrationCourse.StartTime.TimeOfDay <= startTime.TimeOfDay &&
                      x.AssignRegistrationCourse.EndTime.TimeOfDay > startTime.TimeOfDay).ToListAsync();
            if (secondDate!=null)
            {
                 courseListTime = await _db.StudentRegisteationCourses.Where(x => x.StudentId == userId
                                      && x.AssignRegistrationCourse.SemesterId == semesterId && (x.AssignRegistrationCourse.FirstDate == firstDate
                                      || x.AssignRegistrationCourse.SecondDate == secondDate || x.AssignRegistrationCourse.FirstDate == secondDate ||
                                      x.AssignRegistrationCourse.SecondDate == secondDate) &&
                                      x.AssignRegistrationCourse.StartTime.TimeOfDay <= startTime.TimeOfDay &&
                                      x.AssignRegistrationCourse.EndTime.TimeOfDay > startTime.TimeOfDay).ToListAsync();
            }
            
            if(courseListTime.Count()>0)
            {
                return false;
            }
            return true;
        }
    }
}
