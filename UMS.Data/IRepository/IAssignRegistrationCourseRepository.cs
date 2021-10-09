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
        Task<int> CountAsync(string searchValue, Guid departmentId, Guid semesterId, int year);
        Task<IEnumerable<AssignRegistrationCourse>> SearchAsync(string searchValue, Guid departmentId, Guid semesterId, int year,int pageNo,int pageSize);
        Task<IEnumerable<ApplicationUser>> GetAllFaculty(Guid departmentId);
        Task<IEnumerable<Course>> GetAllCourse(Guid departmentId);
    }
}
