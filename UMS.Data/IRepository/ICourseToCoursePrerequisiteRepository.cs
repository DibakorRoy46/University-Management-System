using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Models.Models;

namespace UMS.Data.IRepository
{
    public interface ICourseToCoursePrerequisiteRepository:IRepository<CourseToCoursePrerequisite>
    {
        Task<IEnumerable<Guid>> GetCourseId(Guid coursePreId);
        Task<IEnumerable<Guid>> GetCoursePreId(Guid courseId);
        Task<Guid> GetDepartmentId(Guid courseId);
        Task<int> CountAsync(string searchValue,Guid? departmentId,Guid? courseId, Guid? coursePreId);
        Task<IEnumerable<CourseToCoursePrerequisite>>SearchAsync(string searchValue,Guid? departmentId,Guid? courseId,Guid? coursePreId,int pageNo,int pageSize );
    }
}
