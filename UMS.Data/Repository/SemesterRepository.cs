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
    public class SemesterRepository : Repository<Semester>, ISemesterRepository
    {
        private readonly ApplicationDbContext _db;
        public SemesterRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }
        public async Task<int> CountAsync(string searchValue)
        {
            var semesterList = await _db.Semesters.ToListAsync();
            if(!String.IsNullOrEmpty(searchValue))
            {
                semesterList = await _db.Semesters.Where(x => x.Name.Contains(searchValue)).ToListAsync();
            }
            
            return semesterList.Count();
        }
        public async Task<bool>IsActiveExist()
        {
            var semesterIsActive = await _db.Semesters.Select(x => x.IsActive).ToListAsync();
            
            if (semesterIsActive.Contains(true))
            {
                return false;
            }
            return true;

        }

        public async Task<IEnumerable<Semester>> SearchAsync(string searchValue, int pageNo, int pageSize)
        {
            var semesterList = await _db.Semesters.ToListAsync();
            if (!String.IsNullOrEmpty(searchValue))
            {
                semesterList = await _db.Semesters.Where(x => x.Name.Contains(searchValue)).ToListAsync();
            }
            return semesterList.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
        }

        public async Task UpdateAsync(Semester semester)
        {
            var semesterObj = await _db.Semesters.FirstOrDefaultAsync(x => x.Id.Equals(semester.Id));
            if(semesterObj!=null)
            {
                semesterObj.Name = semester.Name;
                semesterObj.IsActive = semester.IsActive;
            }
        }
        public async Task<IEnumerable<Semester>>GetStudentSemester(string userId)
        {
                      
             var semesterList=await (from semester in _db.Semesters join preCourse in _db.CourseforPreregistration on
                                     semester.Id equals preCourse.SemesterId join assignPreCourse in _db.PreRegistrationCourses
                                     on preCourse.Id equals assignPreCourse.PreCourseId where assignPreCourse.StudentId==userId 
                                     select semester).Distinct().ToListAsync();
            return semesterList;
        }
        public async Task<IEnumerable<Semester>>GetStudentRegisterSemester(string userId)
        {
            var semesterList=await (from semester in _db.Semesters join assignCourse in _db.AssignRegistrationCourses on
                                     semester.Id equals assignCourse.SemesterId join regisCourse in _db.StudentRegisteationCourses
                                     on assignCourse.Id equals regisCourse.AssignRegiCourseId where regisCourse.StudentId==userId 
                                     select semester).Distinct().ToListAsync();
             
            return semesterList;
        }
    }
}
