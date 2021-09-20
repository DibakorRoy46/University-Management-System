using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Models.Models;
using UMS.Utility;

namespace UMS.Models.ViewModels
{
    public class CourseVM
    {
        public IEnumerable<Course> CourseList { get; set; }
        public string Search { get; set; }
        public Guid DepartmentId { get; set; }
        public Pager Pager { get; set; }
    }
}
