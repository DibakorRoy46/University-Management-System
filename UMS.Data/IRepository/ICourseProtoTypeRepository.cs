using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Models.Models;

namespace UMS.Data.IRepository
{
    public interface ICourseProtoTypeRepository:IRepository<CourseProtoType>
    {
        Task UpdateAsync(CourseProtoType courseProtoType);
        Task<int> CountAsync(string searchValue);
        Task<IEnumerable<CourseProtoType>> SearchAsync(string searchValue, int pageNo, int pageSize);
    }
}
