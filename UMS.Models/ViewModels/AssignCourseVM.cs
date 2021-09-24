using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Models.Models;

namespace UMS.Models.ViewModels
{
    public class AssignCourseVM
    {
        public Course Course { get; set; }
        public CourseToCoursePrerequisite CourseToCoursePrerequisite { get; set; }
        public IEnumerable<CourseToCoursePrerequisite> CourseToCoursePrerequisiteList { get; set; }
           
        public IEnumerable<SelectListItem> CoursePreList { get; set; }
    }
}
