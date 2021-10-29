using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Models.Models;

namespace UMS.Models.ViewModels
{
    public class FacultyCourseVM
    {
        public IEnumerable<AssignRegistrationCourse> CourseList { get; set; }
        public IEnumerable<AssignRegistrationCourse> PreviousCourseList { get; set; }
    }
}
