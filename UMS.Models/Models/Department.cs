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
        [Required]
        [Range(100,200)]
        public int RequiredCreditToComplete { get; set; }
        [Required]
        [Range(1,100)]
        public int RequireCourseToComplete { get; set; }
        public int PricePerCredit { get; set; }
        public IEnumerable<Course> Courses { get; set; }
        public IEnumerable<StudentDetails> UserDetails { get; set; }
       
      
       
        
        
    }
}
