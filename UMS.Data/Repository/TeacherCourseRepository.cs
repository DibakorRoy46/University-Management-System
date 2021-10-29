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
    public class TeacherCourseRepository: ITeacherCourseRepository
    {
        private readonly ApplicationDbContext _db;
        public TeacherCourseRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<StudentRegisteationCourse>> StudentList (string searchValue,string teacherId,Guid courseId)
        {

            if(String.IsNullOrEmpty(searchValue))
            {
                var studentList = await _db.StudentRegisteationCourses.Include(x => x.ApplicationUser).
                                       Include(x => x.AssignRegistrationCourse).Where(x => x.AssignRegistrationCourse.TeacherId == teacherId
                                       && x.AssignRegiCourseId == courseId).
                                       ToListAsync();
            
                return studentList;
            }
            else
            {
                var  studentList = await _db.StudentRegisteationCourses.Include(x => x.ApplicationUser).
                                    Include(x => x.AssignRegistrationCourse).Where(x => x.AssignRegistrationCourse.TeacherId == teacherId
                                    && x.AssignRegiCourseId == courseId && x.ApplicationUser.Name.Contains(searchValue)).
                                    ToListAsync();
                return studentList;
            }
                    
        }
    }
}
