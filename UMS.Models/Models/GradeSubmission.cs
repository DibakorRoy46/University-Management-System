using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS.Models.Models
{
    public class GradeSubmission
    {
        [Key]
        public Guid Id { get; set; }
        public Guid RegisterCourseId { get; set; }
        public AssignRegistrationCourse Courses { get; set; }
        public Guid StudentId { get; set; }
        public ApplicationUser Student { get; set; }
        public int? AttendanceMark { get; set; }
        
    }
}
