using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS.Models.Models
{
    public class CourseContent
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CourseId { get; set; }
        [ForeignKey("CourseId")]
        public AssignRegistrationCourse Courses { get; set; }
        public string Content { get; set; }


        
    }
}
