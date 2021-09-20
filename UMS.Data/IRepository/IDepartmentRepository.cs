using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Models.Models;

namespace UMS.Data.IRepository
{
    public interface IDepartmentRepository:IRepository<Department>
    {
        Task UpdateAsync(Department department);
        Task<IEnumerable<Department>> SearchAsync(string searchValue, int pageNo = 1, int pageSize = 10);
        Task<int> CountAsync(string searchValue);
    }
}
