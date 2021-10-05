using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Models.Models;

namespace UMS.Models.ViewModels
{
    public  class PreCourseVM
    {
        public string StudentId { get; set; }
        public IEnumerable<AssignPreRegistrationCourse> CourseList { get; set; }
        
    }

}
