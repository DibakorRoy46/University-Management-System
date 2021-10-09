using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Models.Models;
using UMS.Utility;

namespace UMS.Models.ViewModels
{
    public class AssignCourseTableVM
    {
        public IEnumerable<AssignRegistrationCourse> AssignRegistrationCourseList { get; set; }
        public string Search { get; set; }
        public Guid DepartmentId { get; set; }
        public Guid SemesterId { get; set; }
        public int Year { get; set; }
        public Pager Pager { get; set; }
    }
}
