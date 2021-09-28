using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS.Models.Models
{
    public class Activity
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z ]+$")]
        public string Name { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public string All { get; set; }
    }
}
