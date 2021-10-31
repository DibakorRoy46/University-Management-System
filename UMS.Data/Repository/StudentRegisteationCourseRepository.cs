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
    public class StudentRegisteationCourseRepository:Repository<StudentRegisteationCourse>, IStudentRegisteationCourseRepository
    {
        private readonly ApplicationDbContext _db;
        public StudentRegisteationCourseRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public async Task UpdateAsync(StudentRegisteationCourse studentRegisteationCourse)
        {
            var studentCourseObj = await _db.StudentRegisteationCourses.FirstOrDefaultAsync(x => x.StudentId == studentRegisteationCourse.StudentId
                                  && x.AssignRegiCourseId == studentRegisteationCourse.AssignRegiCourseId);
            if(studentCourseObj!=null)
            {
                studentCourseObj.AttendanceMark = studentRegisteationCourse.AttendanceMark;
                studentCourseObj.AssignmentMark = studentRegisteationCourse.AssignmentMark;
                studentCourseObj.MidTermMark = studentRegisteationCourse.MidTermMark;
                studentCourseObj.FinalTermMark = studentRegisteationCourse.FinalTermMark;
                studentCourseObj.Grade = studentRegisteationCourse.Grade;
                studentCourseObj.GPA = studentRegisteationCourse.GPA;
                studentCourseObj.IsCompleted = studentRegisteationCourse.IsCompleted;
            }
        }
        public async Task<IEnumerable<StudentRegisteationCourse>> GetAllCourses(string userId)
        {
            var courseList= await _db.StudentRegisteationCourses.Include(x => x.ApplicationUser).
                Include(x=>x.AssignRegistrationCourse).ThenInclude(x=>x.Semester).               
                Include(x => x.AssignRegistrationCourse).ThenInclude(x => x.Courses).ThenInclude(x => x.CourseProtoType).
                Include(x => x.AssignRegistrationCourse).ThenInclude(x => x.Courses).ThenInclude(x => x.CourseType).
                Where(x => x.StudentId == userId &&x.IsCompleted==true).Distinct().ToListAsync();
            return courseList;               
        }
        public async Task<double>GetCompletedCGPA(string userId)
        {
            var courseStudentList = await _db.StudentRegisteationCourses.Where(x => x.StudentId == userId && x.IsCompleted == true
                   && x.Grade != "F").ToListAsync();
            var creditCompleted = 0;
            var gainGPA = 0.0;
            var totalCGPA = 0.0;
            foreach (var credit in courseStudentList)
            {
                creditCompleted = creditCompleted + credit.AssignRegistrationCourse.Courses.CourseProtoType.Credit;
                gainGPA = credit.GPA * credit.AssignRegistrationCourse.Courses.CourseProtoType.Credit;
                totalCGPA = totalCGPA + gainGPA;
            }
            var completedCGPA = totalCGPA / creditCompleted;

            return Math.Round(completedCGPA,3); 
            
        }
        public async Task<double> GetAttempedCGPA(string userId)
        {
            var courseStudentList = await _db.StudentRegisteationCourses.Where(x => x.StudentId == userId && x.IsCompleted == true
                   ).ToListAsync();
            var creditCompleted = 0;
            var gainGPA = 0.0;
            var totalCGPA = 0.0;
            foreach (var credit in courseStudentList)
            {
                creditCompleted = creditCompleted + credit.AssignRegistrationCourse.Courses.CourseProtoType.Credit;
                gainGPA = credit.GPA * credit.AssignRegistrationCourse.Courses.CourseProtoType.Credit;
                totalCGPA = totalCGPA + gainGPA;
            }
            var completedCGPA = totalCGPA / creditCompleted;
            return Math.Round(completedCGPA,3);

        }
        public async Task<IEnumerable<StudentRegisteationCourse>> GetCourseBySemester(string userId,Guid semesterId)
        {
            var courseList = await _db.StudentRegisteationCourses.Include(x => x.ApplicationUser).
                Include(x => x.AssignRegistrationCourse).ThenInclude(x => x.Semester).
                Include(x => x.AssignRegistrationCourse).ThenInclude(x => x.Courses).ThenInclude(x => x.CourseProtoType).
                Include(x => x.AssignRegistrationCourse).ThenInclude(x => x.Courses).ThenInclude(x => x.CourseType).
                Include(x=>x.AssignRegistrationCourse).ThenInclude(x=>x.Courses).ThenInclude(x=>x.Department).
                Where(x => x.StudentId == userId && x.IsCompleted == true &&x.AssignRegistrationCourse.SemesterId==semesterId).
                Distinct().ToListAsync();
            return courseList;


        }
        public async Task<int>CreditCompleted(string userId)
        {
            var courseStudentList = await _db.StudentRegisteationCourses.Where(x => x.StudentId == userId && x.IsCompleted == true
                   && x.Grade != "F").ToListAsync();
            var creditCompleted = 0;
            foreach (var credit in courseStudentList)
            {
                creditCompleted = creditCompleted + credit.AssignRegistrationCourse.Courses.CourseProtoType.Credit;
            }
            return creditCompleted;
        }
        public async Task<int> CreditAtempeted(string userId)
        {
            var courseStudentList = await _db.StudentRegisteationCourses.Where(x => x.StudentId == userId && x.IsCompleted == true
                   ).ToListAsync();
            var creditAtempeted = 0;
            foreach (var credit in courseStudentList)
            {
                creditAtempeted = creditAtempeted + credit.AssignRegistrationCourse.Courses.CourseProtoType.Credit;
            }
            return creditAtempeted;
        }
        public async Task<IEnumerable<string>>GetSemesterList(string userId)
        {
            return await _db.StudentRegisteationCourses.Include(x => x.AssignRegistrationCourse).
                  Where(x => x.StudentId == userId).Select(x => x.AssignRegistrationCourse.Semester.Name)
                  .Distinct().ToListAsync();
        }
        public async Task<bool>GetPrerequisiteCourseChecker(string userId,Guid courseId)
        {
            var courseObj = await _db.AssignRegistrationCourses.FirstOrDefaultAsync(x => x.Id == courseId);
            var preCourseList = await _db.CourseToCoursePrerequisites.Where(x => x.CourseId == courseObj.CourseId).Select(x=>x.CoursePreId).
                ToListAsync();

            var courseStudentList = await _db.StudentRegisteationCourses.Where(x => x.StudentId == userId && x.IsCompleted == true
                   && x.Grade != "F").Select(x=>x.AssignRegistrationCourse.CourseId).ToListAsync();
            var isTrue = false;
            if(preCourseList.Count()>0)
            {              
                    foreach (var preCourse in preCourseList)
                    {
                        if (courseStudentList.Contains(preCourse))
                        {
                            isTrue = true;
                        }
                        else
                        {
                            isTrue = false;
                        }
                    }               
            }
            else
            {
                isTrue = true;
            }
            
            return isTrue;
        }
        public async Task<double>GetSemesterGPA(string userId,Guid semesterId)
        {
            var courseStudentList = await _db.StudentRegisteationCourses.Where(x => x.StudentId == userId && x.IsCompleted == true
                    && x.AssignRegistrationCourse.SemesterId==semesterId).ToListAsync();

            var creditCompleted = 0;
            var gainGPA = 0.0;
            var totalCGPA = 0.0;
            foreach (var credit in courseStudentList)
            {
                creditCompleted = creditCompleted + credit.AssignRegistrationCourse.Courses.CourseProtoType.Credit;
                gainGPA = credit.GPA * credit.AssignRegistrationCourse.Courses.CourseProtoType.Credit;
                totalCGPA = totalCGPA + gainGPA;
            }
            var completedCGPA = totalCGPA / creditCompleted;
            return Math.Round(completedCGPA, 3);
        }

        public async Task<int> GetSemesterCredits(string userId, Guid semesterId)
        {
            var courseStudentList = await _db.StudentRegisteationCourses.Where(x => x.StudentId == userId && x.IsCompleted == true
                    && x.AssignRegistrationCourse.SemesterId == semesterId).ToListAsync();

            var creditCompleted = 0;
            foreach (var credit in courseStudentList)
            {
                creditCompleted = creditCompleted + credit.AssignRegistrationCourse.Courses.CourseProtoType.Credit;

            }
            return creditCompleted;
        }

        public async Task<Guid>GetSemester(Guid courseId)
        {
            var semesterObj = await _db.AssignRegistrationCourses.Where(x => x.Id == courseId).Select(x => x.SemesterId).FirstOrDefaultAsync();
            return semesterObj;
        }
        
    }
}
