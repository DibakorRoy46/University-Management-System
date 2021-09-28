﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Data.Data;
using UMS.Data.IRepository;
using UMS.Models.Models;

namespace UMS.Data.Repository
{
    public class ApplicationUserRepository:Repository<ApplicationUser>,IApplicationUserRepository
    {
        private readonly ApplicationDbContext _db;
        public ApplicationUserRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }
        public async Task<int> CountAsync(string search)
        {
            var applicationUserList = await _db.ApplicationUsers.ToListAsync();
            if(!String.IsNullOrEmpty(search))
            {
                applicationUserList = await _db.ApplicationUsers.
                    Where(x => x.Name.ToLower().Contains(search.ToLower())).ToListAsync();
            }
            return applicationUserList.Count();
        }

        public async Task<IEnumerable<ApplicationUser>> SearchAsync(string search, int pageNo, int pageSize)
        {
            var applicationUserList = await _db.ApplicationUsers.ToListAsync();
            if (!String.IsNullOrEmpty(search))
            {
                applicationUserList = await _db.ApplicationUsers.
                    Where(x => x.Name.ToLower().Contains(search.ToLower())).ToListAsync();
            }
            return applicationUserList.Skip((pageNo - 1) * pageSize).Take(pageNo).ToList();
        }

        public async Task UpdateAsync(ApplicationUser applicationUser)
        {
            throw new NotImplementedException();
        }
    }
}
