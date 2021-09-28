using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Models.Models;

namespace UMS.Data.IRepository
{
    public interface IDayRepository:IRepository<Day>
    {
        Task UpdateAsync(Day day);
        Task<int> CountAsync(string searchValue);
        Task<IEnumerable<Day>> SearchAsync(string searchValue, int pageNo, int pageSize);
    }

}
