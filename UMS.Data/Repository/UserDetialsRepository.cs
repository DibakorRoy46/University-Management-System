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
    public class UserDetialsRepository:Repository<UserDetails>,IUserDetialsRepository
    {
        private readonly ApplicationDbContext _db;
        public UserDetialsRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

    }
}
