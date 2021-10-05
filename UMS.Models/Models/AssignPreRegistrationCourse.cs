﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS.Models.Models
{
    public class AssignPreRegistrationCourse
    {
        public string StudentId { get; set; }
        
        public ApplicationUser ApplicationUser { get; set; }
        public Guid CourseId { get; set; }
        public Course Course { get; set; }
        
    }
}
