using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS.Models.ViewModels
{
    public class AssinRegistrationCourseVM
    {
        public IEnumerable<SelectListItem> SemesterList { get; set; }
        public IEnumerable<SelectListItem> DepartmentList { get; set; }
        public IEnumerable<SelectListItem> YearList { get; set; }
        
    }

    
}
