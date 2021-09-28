using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Models.Models;
using UMS.Utility;

namespace UMS.Models.ViewModels
{
    public class ClassTImeVM
    {
        public IEnumerable<ClassTime> ClassTimeList { get; set; }
        public DateTime Search { get; set; }
        public Pager Pager { get; set; }
    }
}
