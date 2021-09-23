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
    public class CourseToCoursePrerequisiteRepository:Repository<CourseToCoursePrerequisite>, ICourseToCoursePrerequisiteRepository
    {
        private readonly ApplicationDbContext _db;
        public CourseToCoursePrerequisiteRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Guid>> GetCourseId(Guid coursePreId)
        {
            return  _db.CourseToCoursePrerequisites.Where(x => x.CoursePreId.Equals(coursePreId)).Select(x => x.CourseId).ToList();
        }

        public async Task<IEnumerable<Guid>> GetCoursePreId(Guid courseId)
        { 
            return  _db.CourseToCoursePrerequisites.Where(x => x.CourseId.Equals(courseId)).Select(x => x.CoursePreId).ToList();
        }
}
}
