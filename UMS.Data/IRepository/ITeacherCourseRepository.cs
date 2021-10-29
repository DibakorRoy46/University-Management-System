using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Models.Models;

namespace UMS.Data.IRepository
{
    public interface ITeacherCourseRepository
    {
        Task<IEnumerable<StudentRegisteationCourse>> StudentList(string searchValue, string teacherId,Guid courseId);
    }
}
