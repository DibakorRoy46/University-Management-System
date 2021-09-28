using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Models.Models;

namespace UMS.Data.IRepository
{
    public interface IApplicationUserRepository:IRepository<ApplicationUser>
    {
        Task UpdateAsync(ApplicationUser applicationUser);
        Task<int> CountAsync(string search);
        Task<IEnumerable<ApplicationUser>> SearchAsync(string search, int pageNo, int pageSize);
    }
}
