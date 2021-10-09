using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS.Models.Models
{
    public class AssignPreRegistrationCourse
    {
        
        public string StudentId { get; set; }   
        [ForeignKey("StudentId")]
        public ApplicationUser ApplicationUser { get; set; }
        public Guid PreCourseId { get; set; }
        public PreregistrationCourses Courses { get; set; } 
     
    }
}
