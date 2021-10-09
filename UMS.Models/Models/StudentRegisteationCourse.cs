using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS.Models.Models
{
    public class StudentRegisteationCourse
    {
        public string StudentId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public Guid AssignRegiCourseId { get; set; }
        public AssignRegistrationCourse AssignRegistrationCourse { get; set; }
    }
}
