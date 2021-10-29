using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Models.Models;

namespace UMS.Data.IRepository
{
    public interface ISemesterRepository:IRepository<Semester>
    {

        Task<int> CountAsync(string searchValue);
        Task<IEnumerable<Semester>> SearchAsync(string searchValue, int pageNo, int pageSize);
        Task UpdateAsync(Semester semester);
        Task<bool> IsActiveExist();
        Task<IEnumerable<Semester>> GetStudentSemester(string userId);
        Task<IEnumerable<Semester>> GetStudentRegisterSemester(string userId);
    }

}
