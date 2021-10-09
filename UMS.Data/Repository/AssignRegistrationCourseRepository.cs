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
    public class AssignRegistrationCourseRepository:Repository<AssignRegistrationCourse>, IAssignRegistrationCourseRepository
    {
        private readonly ApplicationDbContext _db;
        public AssignRegistrationCourseRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public async Task<int> CountAsync(string searchValue, Guid departmentId, Guid semesterId, int year)
        {
            var courseList = await _db.AssignRegistrationCourses.Include(x => x.Semester).
                Include(x => x.ApplicationUser).Include(x => x.Courses).ThenInclude(x => x.CourseProtoType).
                ToListAsync();
            if(!String.IsNullOrEmpty(searchValue))
            {
                courseList = await _db.AssignRegistrationCourses.Include(x => x.Semester).
                Include(x => x.ApplicationUser).Include(x => x.Courses).ThenInclude(x => x.CourseProtoType).
                Where(x=>x.Courses.Name==searchValue).ToListAsync();
                if(departmentId!=Guid.Empty)
                {
                    courseList = courseList.Where(x => x.Courses.DepartmentId == departmentId).ToList();
                    if(semesterId!=Guid.Empty)
                    {
                        courseList = courseList.Where(x => x.SemesterId == semesterId).ToList();
                        if(year!=0)
                        {
                            courseList = courseList.Where(x => x.Year == year).ToList();
                        }    
                    }
                    else
                    {
                        courseList = courseList.ToList();
                        if (year != 0)
                        {
                            courseList = courseList.Where(x => x.Year == year).ToList();
                        }
                    }
                }
                else
                {
                    courseList = courseList.ToList();
                    if (semesterId != Guid.Empty)
                    {
                        courseList = courseList.Where(x => x.SemesterId == semesterId).ToList();
                        if (year != 0)
                        {
                            courseList = courseList.Where(x => x.Year == year).ToList();
                        }
                    }
                    else
                    {
                        courseList = courseList.ToList();
                        if (year != 0)
                        {
                            courseList = courseList.Where(x => x.Year == year).ToList();
                        }
                    }
                }
            }
            else
            {
                courseList = courseList.ToList();
                if (departmentId != Guid.Empty)
                {
                    courseList = courseList.Where(x => x.Courses.DepartmentId == departmentId).ToList();
                    if (semesterId != Guid.Empty)
                    {
                        courseList = courseList.Where(x => x.SemesterId == semesterId).ToList();
                        if (year != 0)
                        {
                            courseList = courseList.Where(x => x.Year == year).ToList();
                        }
                    }
                    else
                    {
                        courseList = courseList.ToList();
                        if (year != 0)
                        {
                            courseList = courseList.Where(x => x.Year == year).ToList();
                        }
                    }
                }
                else
                {
                    courseList = courseList.ToList();
                    if (semesterId != Guid.Empty)
                    {
                        courseList = courseList.Where(x => x.SemesterId == semesterId).ToList();
                        if (year != 0)
                        {
                            courseList = courseList.Where(x => x.Year == year).ToList();
                        }
                    }
                    else
                    {
                        courseList = courseList.ToList();
                        if (year != 0)
                        {
                            courseList = courseList.Where(x => x.Year == year).ToList();
                        }
                    }
                }
            }
            return courseList.Count();
        }

        public async Task<IEnumerable<AssignRegistrationCourse>> SearchAsync(string searchValue, Guid departmentId, Guid semesterId, int year, int pageNo, int pageSize)
        {
            var courseList = await _db.AssignRegistrationCourses.Include(x => x.Semester).Include(x=>x.Section).
                Include(x => x.ApplicationUser).Include(x => x.Courses).ThenInclude(x => x.CourseProtoType).
                Include(x=>x.Courses).ThenInclude(x=>x.Department).
                ToListAsync();
            if (!String.IsNullOrEmpty(searchValue))
            {
                courseList = await _db.AssignRegistrationCourses.Include(x => x.Semester).
                Include(x => x.ApplicationUser).Include(x => x.Courses).ThenInclude(x => x.CourseProtoType).
                Include(x => x.Courses).ThenInclude(x => x.Department).
                Where(x => x.Courses.Name.Contains(searchValue)).ToListAsync();
                if (departmentId != Guid.Empty)
                {
                    courseList = courseList.Where(x => x.Courses.DepartmentId == departmentId).ToList();
                    if (semesterId != Guid.Empty)
                    {
                        courseList = courseList.Where(x => x.SemesterId == semesterId).ToList();
                        if (year != 0)
                        {
                            courseList = courseList.Where(x => x.Year == year).ToList();
                        }
                    }
                    else
                    {
                        courseList = courseList.ToList();
                        if (year != 0)
                        {
                            courseList = courseList.Where(x => x.Year == year).ToList();
                        }
                    }
                }
                else
                {
                    courseList = courseList.ToList();
                    if (semesterId != Guid.Empty)
                    {
                        courseList = courseList.Where(x => x.SemesterId == semesterId).ToList();
                        if (year != 0)
                        {
                            courseList = courseList.Where(x => x.Year == year).ToList();
                        }
                    }
                    else
                    {
                        courseList = courseList.ToList();
                        if (year != 0)
                        {
                            courseList = courseList.Where(x => x.Year == year).ToList();
                        }
                    }
                }
            }
            else
            {
                courseList = courseList.ToList();
                if (departmentId != Guid.Empty)
                {
                    courseList = courseList.Where(x => x.Courses.DepartmentId == departmentId).ToList();
                    if (semesterId != Guid.Empty)
                    {
                        courseList = courseList.Where(x => x.SemesterId == semesterId).ToList();
                        if (year != 0)
                        {
                            courseList = courseList.Where(x => x.Year == year).ToList();
                        }
                    }
                    else
                    {
                        courseList = courseList.ToList();
                        if (year != 0)
                        {
                            courseList = courseList.Where(x => x.Year == year).ToList();
                        }
                    }
                }
                else
                {
                    courseList = courseList.ToList();
                    if (semesterId != Guid.Empty)
                    {
                        courseList = courseList.Where(x => x.SemesterId == semesterId).ToList();
                        if (year != 0)
                        {
                            courseList = courseList.Where(x => x.Year == year).ToList();
                        }
                    }
                    else
                    {
                        courseList = courseList.ToList();
                        if (year != 0)
                        {
                            courseList = courseList.Where(x => x.Year == year).ToList();
                        }
                    }
                }
            }
            return courseList.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
        }

        public async Task UpdateAsync(AssignRegistrationCourse assignRegistrationCourse)
        {
            var assignRegistrationCourseObj = await _db.AssignRegistrationCourses.
                FirstOrDefaultAsync(x => x.Id == assignRegistrationCourse.Id);
            if(assignRegistrationCourseObj!=null)
            {
                assignRegistrationCourseObj.TeacherId = assignRegistrationCourse.TeacherId;
                assignRegistrationCourseObj.SemesterId = assignRegistrationCourse.SemesterId;
                assignRegistrationCourseObj.SectionId = assignRegistrationCourse.SectionId;
                assignRegistrationCourseObj.Year = assignRegistrationCourse.Year;
                assignRegistrationCourseObj.FirstDate = assignRegistrationCourse.FirstDate;
                assignRegistrationCourseObj.SecondDate = assignRegistrationCourse.SecondDate;
                assignRegistrationCourseObj.StartTime = assignRegistrationCourse.StartTime;
                assignRegistrationCourseObj.EndTime = assignRegistrationCourse.EndTime;
            }
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllFaculty(Guid departmentId)
        {
            if(departmentId!=Guid.Empty)
            {
                 return await (from users in _db.ApplicationUsers join
                                    userDetails in _db.UserDetails on users.Id equals userDetails.UserId
                                     join aspnetUserRoles in _db.UserRoles on users.Id equals aspnetUserRoles.UserId
                                     join roles in _db.Roles on aspnetUserRoles.RoleId equals roles.Id where
                                     roles.Name == "Faculty" && userDetails.DepartmentId == departmentId 
                                     select users).ToListAsync();
            }
            else
            {
                 return await (from users in _db.ApplicationUsers join
                                    userDetails in _db.UserDetails on users.Id equals userDetails.UserId
                                     join aspnetUserRoles in _db.UserRoles on users.Id equals aspnetUserRoles.UserId
                                     join roles in _db.Roles on aspnetUserRoles.RoleId equals roles.Id where
                                     roles.Name == "Faculty"
                                     select users).ToListAsync();
            }
           


           
        }

        public async Task<IEnumerable<Course>>GetAllCourse(Guid departmentId)
        {
            return await _db.Courses.Where(x => x.DepartmentId == departmentId).ToListAsync();
        }
    }
}
