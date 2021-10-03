using IdentityServer4.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace UMS.Models.ViewModels
{
    public class UserClaimPartial
    {
        public IEnumerable<Claim> UserClaimList { get; set; }
        public string UserId { get; set; }
    }
}
