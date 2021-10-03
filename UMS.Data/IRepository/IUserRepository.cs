﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Models.Models;

namespace UMS.Data.IRepository
{
    public interface IUserRepository:IRepository<ApplicationUser>
    {
        Task UpdateAsync(ApplicationUser applicationUser);
        Task<int> CountAsync(string search,string userId, string roleId);
        Task<IEnumerable<ApplicationUser>> SearchAsync(string search,string userId, string roleId, int pageNo, int pageSize);

    }
}
