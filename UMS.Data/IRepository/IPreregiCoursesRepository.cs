using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Models.Models;

namespace UMS.Data.IRepository
{
    public interface IPreregiCoursesRepository:IRepository<PreregistrationCourses>
    {
        Task<IEnumerable<Guid>> SelectCourseId(Guid courseId, Guid semesterId, int year);
        Task<IEnumerable<int>> SelectYear(string userId=null);

    }
}
