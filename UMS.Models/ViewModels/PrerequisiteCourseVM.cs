using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Models.Models;

namespace UMS.Models.ViewModels
{
    public class PrerequisiteCourseVM
    {
        public IEnumerable<CourseToCoursePrerequisite> PrerequisiteCourseList { get; set; }
        public Guid CourseId { get; set; }
    }
}
