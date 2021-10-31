using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Models.Models;

namespace UMS.Models.ViewModels
{
    public class RegistrationFormTableVM
    {
        public int Credits { get; set; }
        public IEnumerable<StudentRegisteationCourse> RegistrationCourse { get; set; }
        public int TotalFee { get; set; }
        public int ActivityFee { get; set; } = 1000;
        public int LaboratoryFee { get; set; } = 2000;
    }
}
