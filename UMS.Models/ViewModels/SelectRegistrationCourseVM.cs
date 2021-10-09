using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Models.Models;

namespace UMS.Models.ViewModels
{
    public class SelectRegistrationCourseVM
    {
        public IEnumerable<StudentRegisteationCourse> CourseList { get; set; }
        public string UserId { get; set; }
    }
}
