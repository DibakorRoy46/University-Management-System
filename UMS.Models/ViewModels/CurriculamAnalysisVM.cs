using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Models.Models;

namespace UMS.Models.ViewModels
{
    public class CurriculamAnalysisVM
    {
        public IEnumerable<Course> courseList{ get; set;}
        public string UserId { get; set; }
        public int CreditCompletd { get; set; }   
        public double CompletedCGPA { get; set; }
        public int Batch { get; set; }
        public Department Department { get; set; }
        public TimeSpan AdmissionTIme { get; set; }
    }
}
