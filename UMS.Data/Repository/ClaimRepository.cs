using Microsoft.EntityFrameworkCore;
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
    public class ClaimRepository : Repository<Claims>, IClaimRepository
    {
        private readonly ApplicationDbContext _db;
        public ClaimRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }
        public async Task<int> CountAsync(string searchValue)
        {
            var claimList = await _db.Claims.ToListAsync();
            if(!String.IsNullOrEmpty(searchValue))
            {
                claimList = await _db.Claims.Where(x => x.ClaimType.Contains(searchValue)).ToListAsync();
            }
            return claimList.Count();
        }

        public async Task<IEnumerable<Claims>> SearchAsync(string searchValue, int pageNo, int pageSize)
        {
            var claimList = await _db.Claims.ToListAsync();
            if (!String.IsNullOrEmpty(searchValue))
            {
                claimList = await _db.Claims.Where(x => x.ClaimType.Contains(searchValue)).ToListAsync();
            }
            return claimList.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
        }

        public async Task UpdateAsync(Claims claims)
        {
            var claimObj = await _db.Claims.FirstOrDefaultAsync(x => x.Id == claims.Id);
            if(claimObj!=null)
            {
                claimObj.ClaimType = claims.ClaimType;
                claimObj.ClaimValue = claims.ClaimValue;
            }
        }
    }
}
