using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Models.Models;

namespace UMS.Data.IRepository
{
    public  interface IPreRegistrationCourseRepository:IRepository<AssignPreRegistrationCourse>
    {
        Task<IEnumerable<AssignPreRegistrationCourse>> GetPreCourses(string userId, Guid semesterId, int year);
        Task<IEnumerable<Course>> GetAllCourses(string searchValue, Guid departmentId);
        Task<IEnumerable<Course>> GetAllCourses(string searchValue);
        Task<IEnumerable<Guid>> SelectPreCourseId(string userId,Guid semesterId,int year);

        Task<int> CountStudent(Guid id, Guid semesterId, int year);
    }
}
