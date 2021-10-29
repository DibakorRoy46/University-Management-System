using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Models.Models;

namespace UMS.Data.IRepository
{
    public interface IStudentRegisteationCourseRepository:IRepository<StudentRegisteationCourse>
    {
        Task UpdateAsync(StudentRegisteationCourse studentRegisteationCourse);
        Task<IEnumerable<StudentRegisteationCourse>> GetAllCourses(string userId);
        Task<int> CreditCompleted(string userId);
        Task<int> CreditAtempeted(string userId);
        Task<int> GetCourseBySemester(string userId, Guid semesterId);
        Task<IEnumerable<string>> GetSemesterList(string userId);
        Task<bool> GetPrerequisiteCourseChecker(string userId, Guid courseId);


    }
}
