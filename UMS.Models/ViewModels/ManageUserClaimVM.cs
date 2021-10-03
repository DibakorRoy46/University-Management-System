using IdentityServer4.EntityFramework.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UMS.Models.Models;

namespace UMS.Models.ViewModels
{
    public class ManageUserClaimVM
    {
        public ApplicationUser User { get; set; }
        public IEnumerable<SelectListItem> ClaimList { get; set; }
        public string UserId { get; set; }
    }
}
