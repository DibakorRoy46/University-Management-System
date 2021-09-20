using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS.Models.Models
{
    public class CourseProtoType
    {
        [Key]
        public Guid Id { get; set; }
        [RegularExpression(@"^[A-Za-z]+$")]
        [Required(ErrorMessage = "Please Enter the CourseProto Type")]
        public string Name { get; set; }
        public int Credit { get; set; }
    }
}
