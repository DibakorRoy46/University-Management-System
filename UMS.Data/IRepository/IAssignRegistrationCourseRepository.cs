using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Models.Models;

namespace UMS.Data.IRepository
{
    public interface IAssignRegistrationCourseRepository:IRepository<AssignRegistrationCourse >
    {
        Task UpdateAsync(AssignRegistrationCourse assignRegistrationCourse);
        Task<int> CountAsync(string searchValue, Guid departmentId, Guid semesterId);
        Task<IEnumerable<AssignRegistrationCourse>> SearchAsync(string searchValue, Guid departmentId, Guid semesterId,int pageNo,int pageSize);
        Task<IEnumerable<ApplicationUser>> GetAllFaculty(Guid departmentId,Guid id);
        Task<IEnumerable<Course>> GetAllCourse(Guid departmentId);
        Task<int> GetCourseType(Guid courseId);
        Task<bool> GetSectionValidity(Guid semesterId, Guid courseId, Guid sectionId);
        Task<bool> GetTeacherSlotValidity(string teacherId, Guid semesterId, string firstDate, string secondDate, DateTime startTime);
    }
}
