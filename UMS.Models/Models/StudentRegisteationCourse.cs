using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS.Models.Models
{
    public class StudentRegisteationCourse
    {
        public string StudentId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public Guid AssignRegiCourseId { get; set; }
        public AssignRegistrationCourse AssignRegistrationCourse { get; set; }
        
        public double? AttendanceMark { get; set; }
        public double? AssignmentMark { get; set; }
        public double? MidTermMark { get; set; }
        public double? FinalTermMark { get; set; }
        public string Grade { get; set; }
        public bool IsCompleted { get; set; }
        public double GPA { get; set; }
        [NotMapped]
        public int Count { get; set; }
       
    }
}
