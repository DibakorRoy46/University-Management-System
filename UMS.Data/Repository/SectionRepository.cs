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
    public class SectionRepository:Repository<Section>,ISectionRepository
    {
        private readonly ApplicationDbContext _db;
        public SectionRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public async Task<int> CountAsync(string search)
        {
            var sectionList = await _db.Sections.ToListAsync();
            if(!String.IsNullOrEmpty(search))
            {
                sectionList = await _db.Sections.Where(x => x.Name.ToLower().Contains(search.ToLower())).ToListAsync();
            }
            return sectionList.Count();
        }

        public async Task<IEnumerable<Section>> SearchAsync(string search, int pageNo, int pageSize)
        {
            var sectionList = await _db.Sections.ToListAsync();
            if (!String.IsNullOrEmpty(search))
            {
                sectionList = await _db.Sections.Where(x => x.Name.ToLower().Contains(search.ToLower())).ToListAsync();
            }
            return sectionList.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
        }

        public async Task UpdateAsync(Section section)
        {
            var sectionObj = await _db.Sections.FirstOrDefaultAsync(x => x.Id.Equals(section.Id));
            if(sectionObj!=null)
            {
                sectionObj.Name = section.Name;
                sectionObj.Initial = section.Initial;
            }
        }
    }
}
