using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS.Models.Models
{
    public class CourseToCoursePrerequisite
    {
        public Guid CourseId { get; set; }
        public Course Course { get; set; }
        public Guid CoursePreId { get; set; }
        public CoursePrerequisite CoursePrerequisite { get; set; }
    }
}
