using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS.Models.Models
{
    public class CoursePrerequisite
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage ="Please Enter the Course Name")]
        [RegularExpression(@"^[a-zA-Z ]+$")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please Enter the Course Initial")]
        [RegularExpression(@"^[a-zA-Z0-9 ]+$")]
        public string InitialName { get; set; }
        public IEnumerable<CourseToCoursePrerequisite> CourseToCoursePrerequisites { get; set; }
    }
}
