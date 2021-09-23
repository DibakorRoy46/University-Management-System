using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Models.Models;

namespace UMS.Data.IRepository
{
    public interface ICourseRepository:IRepository<Course>
    {
        Task<IEnumerable<Course>> GetCourseByDepartment(Guid id,Guid courseId);
        Task UpdateAsync(Course course);
        Task<int> CountAsync(string searchValue, Guid depaertmentId);
        Task<IEnumerable<Course>> SearchAsync(string searchValue, Guid depaertmentId,int pageNo,int pageSize);
    }
}
