using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Data.Data;
using UMS.Data.IRepository;

namespace UMS.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Department = new DepartmentRepository(db);
            CourseType = new CourseTypeRepository(db);
            CourseProtoType = new CourseProtoTypeRepository(db);
            Course = new CourseRepository(db);
            CoursePrerequisite = new CoursePrerequisiteRepository(db);
            CourseToCoursePrerequisite = new CourseToCoursePrerequisiteRepository(db);
            Semester = new SemesterRepository(db);
            ClassTime = new ClassTimeRepository(db);
            Day = new DayRepository(db);
            Section = new SectionRepository(db);
            Activity = new ActivityRepository(db);
            ApplicationUser = new ApplicationUserRepository(db);
            UserDetials = new UserDetialsRepository(db);
            User = new UserRepository(db);
            Claims = new ClaimRepository(db);
            PreRegistationCourses = new PreRegistrationCourseRepository(db);
            PreregiCourses = new PreregiCoursesRepository(db);
            RegistrationCourse = new RegistrationCourseRepository(db);
            AssignRegistrationCourse = new AssignRegistrationCourseRepository(db);
            CourseContent = new CourseContentRepository(db);
            AdminDashboard = new AdminDashboardRepository(db);
            TeacherCourse = new TeacherCourseRepository(db);
            StudentRegisteationCourse = new StudentRegisteationCourseRepository(db);

        }
        public IDepartmentRepository Department { get; private set; }

        public ICourseTypeRepository CourseType { get; private set; }

        public ICourseProtoTypeRepository CourseProtoType { get; private set; }
        public ICourseRepository Course { get; private set; }

        public ICoursePrerequisiteRepository CoursePrerequisite { get; private set; }

        public ICourseToCoursePrerequisiteRepository CourseToCoursePrerequisite { get; private set; }

        public ISemesterRepository Semester { get; private set; }
        public IClassTimeRepository ClassTime { get; private set; }

        public IDayRepository Day { get; private set; }

        public ISectionRepository Section { get; private set; }

        public IActivityRepository Activity { get; private set; }

        public IApplicationUserRepository ApplicationUser { get; private set; }
        public IUserDetialsRepository UserDetials { get; private set; }

        public IUserRepository User { get; private set; }

        public IClaimRepository Claims { get; private set; }

        public IPreRegistrationCourseRepository PreRegistationCourses { get; private set; }

        public IPreregiCoursesRepository PreregiCourses { get; private set; }

        public IAssignRegistrationCourseRepository AssignRegistrationCourse { get; private set; }

        public IRegistrationCourseRepository RegistrationCourse { get; private set; }

        public ICourseContentRepository CourseContent { get; private set; }

        public IAdminDashboardRepository AdminDashboard { get; private set; }

        public ITeacherCourseRepository TeacherCourse { get; private set; }

        public IStudentRegisteationCourseRepository StudentRegisteationCourse { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
