using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Models.Models;

namespace UMS.Models.ViewModels
{
    public class RegistrationFormVM
    {
        public string StudentId { get; set; }
        public Guid SemesterId { get; set; }
        public IEnumerable<SelectListItem> SemesterList { get; set; }
    }
}
