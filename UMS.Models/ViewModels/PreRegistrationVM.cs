using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Models.Models;

namespace UMS.Models.ViewModels
{
    public class PreRegistrationVM
    {
        public ApplicationUser User { get; set; }
        public IEnumerable<Course> CourseList { get; set; }
        
    }
}
