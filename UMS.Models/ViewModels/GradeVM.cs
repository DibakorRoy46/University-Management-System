﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Models.Models;

namespace UMS.Models.ViewModels
{
    public class GradeVM
    {
        public IEnumerable<StudentRegisteationCourse> courseList { get; set; }
        public string UserId { get; set; }
        public int CreditCompletd { get; set; }
        public int CreditAttempted { get; set; }
        public IEnumerable<string> SemesterList { get; set; }
        public IEnumerable<int> YearList { get; set; }
        public List<int> CourseCount = new List<int>();
    }
}
