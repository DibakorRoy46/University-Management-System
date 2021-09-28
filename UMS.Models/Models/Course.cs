using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS.Models.Models
{
    public class Course
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage ="Please Enter Course Name")]
        [RegularExpression(@"^[a-zA-Z() ]+$")]
        public string Name { get; set; }
        [RegularExpression(@"^[a-zA-Z0-9 ]+$")]
        [Required(ErrorMessage ="Please Enter the Initail Name")]
        public string Initial { get; set; }
        public Guid DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }
        public Guid CourseTypeId { get; set; }
        [ForeignKey("CourseTypeId")]
        public CourseType CourseType { get; set; }
        public Guid CourseProtoTypeId { get; set; }
        [ForeignKey("CourseProtoTypeId")]
        public CourseProtoType CourseProtoType { get; set; }
        [NotMapped]      
        public IEnumerable<Guid> CoursePreId { get; set; }
        public IEnumerable<CourseToCoursePrerequisite> CourseToCoursePrerequisites { get; set; }
    }
}
