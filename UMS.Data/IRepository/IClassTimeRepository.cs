using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Models.Models;

namespace UMS.Data.IRepository
{
    public interface IClassTimeRepository:IRepository<ClassTime>
    {

        Task<int> CountAsync(DateTime searchValue);
        Task<IEnumerable<ClassTime>> SearchAsync(DateTime searchValue, int pageNo, int pageSize);
        Task UpdateAsync(ClassTime classTime);
    }
}
