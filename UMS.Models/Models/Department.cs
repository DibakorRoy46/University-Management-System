using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS.Models.Models
{
    public class Department
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage ="Please Enter the Department Name")]
        [RegularExpression(@"^[a-zA-Z ]+$")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Please Enter the Department Initial Name")]      
        [RegularExpression(@"^[a-zA-Z ]+$")]
        public string Initial { get; set; }
        public int RequiredCreditToComplete { get; set; }
        public int RequireCourseToComplete { get; set; }
        public IEnumerable<Course> Courses { get; set; }
    }
}
