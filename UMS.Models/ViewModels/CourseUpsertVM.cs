using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Models.Models;

namespace UMS.Models.ViewModels
{
    public class CourseUpsertVM
    {
        public Course Course { get; set; }
        public IEnumerable<SelectListItem> CourseTypeList { get; set; }
        public IEnumerable<SelectListItem> DepartmentList { get; set; }
        public IEnumerable<SelectListItem> CourseProtoTypeList { get; set; }
        public IEnumerable<SelectListItem> CourseList { get; set; }
    }
}
