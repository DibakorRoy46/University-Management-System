using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Models.Models;

namespace UMS.Data.IRepository
{
    public interface IClaimRepository:IRepository<Claims>
    {
        Task UpdateAsync(Claims claims);
        Task<int> CountAsync(string searchValue);
        Task<IEnumerable<Claims>> SearchAsync(string searchValue, int pageNo, int pageSize);
    }
}
