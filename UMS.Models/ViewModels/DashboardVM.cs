using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Models.Models;

namespace UMS.Models.ViewModels
{
    public class DashboardVM
    {
        public int Student { get; set; }
        public int Teacher { get; set; }
        public int Employee { get; set; }
        public int Role { get; set; }
        public int Department { get; set; }
        public int RegistrationCourseList { get; set; }
        public int PreregistrationCourseList { get; set; }
        public int AssignCourseList { get; set; }
    }
}
