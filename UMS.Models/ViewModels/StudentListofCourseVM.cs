using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Models.Models;

namespace UMS.Models.ViewModels
{
    public class StudentListofCourseVM
    {
        public IEnumerable<StudentRegisteationCourse> StudentList { get; set; }
    }
}
