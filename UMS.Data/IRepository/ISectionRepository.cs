using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Models.Models;

namespace UMS.Data.IRepository
{
    public interface ISectionRepository:IRepository<Section>
    {
        Task UpdateAsync(Section section);
        Task<int> CountAsync(string search);
        Task<IEnumerable<Section>> SearchAsync(string search, int pageNo, int pageSize);

    }
}
