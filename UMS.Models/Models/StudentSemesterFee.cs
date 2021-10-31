using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS.Models.Models
{
    public class StudentSemesterFee
    {
        public int Id { get; set; }
        public string StudentId { get; set; }
        public Guid SemesterId { get; set; }
        public int TotalPrice { get; set; }
        public IEnumerable<Semester> SemesterList { get; set; }
    }
}
