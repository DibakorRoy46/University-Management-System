using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS.Models.Models
{
    public class ApplicationUser:IdentityUser
    {
        [Required]
        public string Name { get; set; }
        public DateTime RegisterDate { get; set; }
        [NotMapped]
        public IEnumerable<string> RoleId { get; set; }
        [NotMapped]
        public  IEnumerable<string> Role { get; set; }
        [NotMapped]
        public IEnumerable<SelectListItem> RoleList { get; set; }
        public IEnumerable<UserDetails> UserDetails { get; set; }
        


    }
}
