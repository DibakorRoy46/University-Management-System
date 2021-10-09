using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Models.Models;

namespace UMS.Models.ViewModels
{
    public class AssginRegistrationCourseUpsertVM
    {
        public AssignRegistrationCourse AssignRegistrationCourse { get; set; }
        public IEnumerable<SelectListItem> CourseList { get; set; }
        public IEnumerable<SelectListItem> SemesterList { get; set; }
        public IEnumerable<SelectListItem> TeacherList { get; set; }
        public IEnumerable<SelectListItem> SectionList { get; set; }
        public IEnumerable<SelectListItem> DepartemntList { get; set; }
        public IEnumerable<SelectListItem> DayList { get; set; }
    }
}
