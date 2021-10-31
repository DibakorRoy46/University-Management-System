using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Models.Models;

namespace UMS.Data.IRepository
{
    public interface IEmployeeDetialsRepository:IRepository<EmployeeDetials>
    {
        Task UpdateAsync(EmployeeDetials employeeDetials);
    }
}
