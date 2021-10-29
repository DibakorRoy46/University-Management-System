using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS.Models.Models
{
    public class AssignRegistrationCourse
    {
        [Key]
        public Guid Id { get; set; }
        public Guid CourseId { get; set; }
        [ForeignKey("CourseId")]
        public Course Courses { get; set; }
        public string TeacherId { get; set; }
        [ForeignKey("TeacherId")]
        public ApplicationUser ApplicationUser { get; set; }
        public Guid SemesterId { get; set; }
        [ForeignKey("SemesterId")]
        public Semester Semester { get; set; }
        public Guid SectionId { get; set; }
        [ForeignKey("SectionId")]
        public Section Section { get; set; }
         
        [Required]
        public string FirstDate { get; set; }
        public string SecondDate { get; set; }
        [DataType(DataType.Time)]
        public DateTime StartTime { get; set; }
        [DataType(DataType.Time)]
        public DateTime EndTime { get; set; }
        public IEnumerable<StudentRegisteationCourse> StudentRegisteationCourses { get; set; }
        [NotMapped]
        public Guid DepartmentId { get; set; }
        [NotMapped]
        public bool IsTaken { get; set; }
        [NotMapped]
        public int Count { get; set; }
        public IEnumerable<CourseContent> CourseContents { get; set; }
    
    }
}
