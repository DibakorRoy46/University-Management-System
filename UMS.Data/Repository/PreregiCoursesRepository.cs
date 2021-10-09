using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UMS.Data.Data;
using UMS.Data.IRepository;
using UMS.Models.Models;

namespace UMS.Data.Repository
{
    public class PreregiCoursesRepository : Repository<PreregistrationCourses>, IPreregiCoursesRepository
    {
        private readonly ApplicationDbContext _db;
        public PreregiCoursesRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Guid>> SelectCourseId(Guid courseId, Guid semesterId, int year)
        {
            return  _db.CourseforPreregistration.
                Where(x => x.CourseId == courseId && x.SemesterId == semesterId && x.Year == year).Select(x => x.CourseId); 
        }
        public async Task<IEnumerable<int>> SelectYear(string userId=null)
        {
            if(!String.IsNullOrEmpty(userId))
            {
                return _db.PreRegistrationCourses.Include(x => x.ApplicationUser).Include(x => x.Courses).
                Where(x => x.ApplicationUser.Id == userId).Select(x => x.Courses.Year).Distinct();
            }
            return _db.PreRegistrationCourses.Select(x => x.Courses.Year).Distinct();


        }
    }
}
