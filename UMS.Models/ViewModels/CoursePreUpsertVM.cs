using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Models.Models;

namespace UMS.Models.ViewModels
{
    public class CoursePreUpsertVM
    {
        public CoursePrerequisite CoursePrerequisite { get; set; }
        public IEnumerable<SelectListItem> CourseList { get; set; }
        public IEnumerable<SelectListItem> DepartmentList { get; set; }
    }
}
