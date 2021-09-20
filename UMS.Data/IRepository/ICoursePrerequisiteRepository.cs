using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Models.Models;

namespace UMS.Data.IRepository
{
    public interface ICoursePrerequisiteRepository:IRepository<CoursePrerequisite>
    {
        Task UpdateAsync(CoursePrerequisite coursePrerequisite);
        Task<int> CountAsync(string searchValue);
        Task<IEnumerable<CoursePrerequisite>> SearchAsync(string searchValue, int pageNo, int pageSize);
    }
}
