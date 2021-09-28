using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Models.Models;

namespace UMS.Data.IRepository
{
    public interface IActivityRepository:IRepository<Activity>
    {
        Task UpdateAsync(Activity activity);
        Task<int> CountAsync(string search);
        Task<IEnumerable<Activity>> SearchAsync(string search, int pageNo, int pageSize);
    }
}
