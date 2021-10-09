using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Models.Models;

namespace UMS.Data.IRepository
{
    public interface ICourseContentRepository:IRepository<CourseContent>
    {
        Task<int> CountAsync(string searchValue);
        Task<IEnumerable<CourseContent>> SearchAsync(string searchValue, int pageNo, int pageSize);
        Task UpdateAsync(CourseContent courseContent);

    }
}
