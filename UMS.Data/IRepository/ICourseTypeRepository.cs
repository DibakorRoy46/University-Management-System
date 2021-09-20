using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Models.Models;

namespace UMS.Data.IRepository
{
    public interface ICourseTypeRepository:IRepository<CourseType>
    {
        Task UpdateAsync(CourseType course);
        Task<int> CountAsync(string searchValue);
        Task<IEnumerable<CourseType>> SearchAsync(string searchValue, int pageNo, int pageSize);
    }
}
