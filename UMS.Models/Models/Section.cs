using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS.Models.Models
{
    public class Section
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9- ]+$")]
        public string Name { get; set; }
        [Required]
        public int Initial { get; set; }
    }
}
