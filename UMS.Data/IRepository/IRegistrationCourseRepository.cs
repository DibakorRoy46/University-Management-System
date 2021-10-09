using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Models.Models;

namespace UMS.Data.IRepository
{
    public interface IRegistrationCourseRepository:IRepository<StudentRegisteationCourse>
    {
         Task<IEnumerable<StudentRegisteationCourse>> GetRegisteredCourses(string userId, Guid semesterId, int year);
        Task<IEnumerable<AssignRegistrationCourse>> GetAllCourses(string searchValue, Guid departmentId);
      
        Task<IEnumerable<Guid>> SelectRegistrationCourseId(string userId);
        Task<IEnumerable<int>> SelectRegistrationYear(string userId);
        Task<IEnumerable<Guid>> TakenCourseId(string userId);
        Task<int> CountRegistered(Guid courseId);
    }
}
