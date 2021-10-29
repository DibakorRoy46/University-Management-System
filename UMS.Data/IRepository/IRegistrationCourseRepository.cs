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
        Task<IEnumerable<StudentRegisteationCourse>> GetRegisteredCourses(string userId, Guid semesterId);
        Task<IEnumerable<AssignRegistrationCourse>> GetAllCourses(string searchValue, Guid departmentId, Guid semesterId);     
        Task<IEnumerable<Guid>> SelectRegistrationCourseId(string userId);
        Task<IEnumerable<Guid>> TakenCourseId(string userId);
        Task<int> CountRegistered(Guid courseId);
        Task UpdateAsync(StudentRegisteationCourse studentRegisteationCourse);
        Task<bool> GetTimeAvilabity(string userId, Guid semesterId, string firstDate, string secondDate, DateTime startTime);
    }
}
