using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Utility;

namespace UMS.Models.ViewModels
{
    public class RoleVM
    {
        public IEnumerable<IdentityRole> RoleList { get; set; }
        public String Search { get; set; }
        public Pager Pager { get; set; }
    }
}
