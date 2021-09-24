using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Models.Models;
using UMS.Utility;

namespace UMS.Models.ViewModels
{
    public class CourseToCourseVM
    {
        public IEnumerable<CourseToCoursePrerequisite> CourseToCourseList { get; set; }
        public string Search { get; set; }
        public Guid DepartmentId { get; set; }
        public Guid CourseId { get; set; }
        public Guid CoursePreId { get; set; }
        public Pager Pager { get; set; }
    }
}
