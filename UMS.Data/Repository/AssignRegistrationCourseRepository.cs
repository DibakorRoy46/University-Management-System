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

        public async Task<int> CountAsync(string searchValue, Guid departmentId, Guid semesterId)
        {
            var courseList = await _db.AssignRegistrationCourses.Include(x => x.Semester).
                Include(x => x.ApplicationUser).Include(x => x.Courses).ThenInclude(x => x.CourseProtoType).
                ToListAsync();
            if(!String.IsNullOrEmpty(searchValue))
            {
                courseList = await _db.AssignRegistrationCourses.Include(x => x.Semester).
                Include(x => x.ApplicationUser).Include(x => x.Courses).ThenInclude(x => x.CourseProtoType).
                Where(x=>x.Courses.Name==searchValue || x.Courses.Initial.Contains(searchValue)).ToListAsync();
                if(departmentId!=Guid.Empty)
                {
                    courseList = courseList.Where(x => x.Courses.DepartmentId == departmentId).ToList();
                    if(semesterId!=Guid.Empty)
                    {
                        courseList = courseList.Where(x => x.SemesterId == semesterId).ToList();
                         
                    }
                    else
                    {
                        courseList = courseList.ToList();
                    }
                }
                else
                {
                    courseList = courseList.ToList();
                    if (semesterId != Guid.Empty)
                    {
                        courseList = courseList.Where(x => x.SemesterId == semesterId).ToList();
                       
                    }
                    else
                    {
                        courseList = courseList.ToList();
                       
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
                        
                    }
                    else
                    {
                        courseList = courseList.ToList();
                        
                    }
                }
                else
                {
                    courseList = courseList.ToList();
                    if (semesterId != Guid.Empty)
                    {
                        courseList = courseList.Where(x => x.SemesterId == semesterId).ToList();
                      
                    }
                    else
                    {
                        courseList = courseList.ToList();
                        
                    }
                }
            }
            return courseList.Count();
        }

        public async Task<IEnumerable<AssignRegistrationCourse>> SearchAsync(string searchValue, Guid departmentId, Guid semesterId, int pageNo, int pageSize)
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
                Where(x => x.Courses.Name.Contains(searchValue) || x.Courses.Initial.Contains(searchValue)).ToListAsync();
                if (departmentId != Guid.Empty)
                {
                    courseList = courseList.Where(x => x.Courses.DepartmentId == departmentId).ToList();
                    if (semesterId != Guid.Empty)
                    {
                        courseList = courseList.Where(x => x.SemesterId == semesterId).ToList();
                       
                    }
                    else
                    {
                        courseList = courseList.ToList();
                       
                    }
                }
                else
                {
                    courseList = courseList.ToList();
                    if (semesterId != Guid.Empty)
                    {
                        courseList = courseList.Where(x => x.SemesterId == semesterId).ToList();
                        
                    }
                    else
                    {
                        courseList = courseList.ToList();
                        
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
                       
                    }
                    else
                    {
                        courseList = courseList.ToList();
                       
                    }
                }
                else
                {
                    courseList = courseList.ToList();
                    if (semesterId != Guid.Empty)
                    {
                        courseList = courseList.Where(x => x.SemesterId == semesterId).ToList();
                       
                    }
                    else
                    {
                        courseList = courseList.ToList();
                       
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
                assignRegistrationCourseObj.FirstDate = assignRegistrationCourse.FirstDate;
                assignRegistrationCourseObj.SecondDate = assignRegistrationCourse.SecondDate;
                assignRegistrationCourseObj.StartTime = assignRegistrationCourse.StartTime;
                assignRegistrationCourseObj.EndTime = assignRegistrationCourse.EndTime;
            }
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllFaculty(Guid departmentId,Guid id)
        {
            if(id!=Guid.Empty)
            {
                var assignCourseObj = await _db.AssignRegistrationCourses.FirstOrDefaultAsync(x => x.Id == id);
                if(departmentId!=Guid.Empty)
                {
                     return await (from users in _db.ApplicationUsers join
                                        userDetails in _db.UserDetails on users.Id equals userDetails.UserId
                                         join aspnetUserRoles in _db.UserRoles on users.Id equals aspnetUserRoles.UserId
                                         join roles in _db.Roles on aspnetUserRoles.RoleId equals roles.Id where
                                         roles.Name == "Faculty" && userDetails.DepartmentId == departmentId 
                                         && users.Id!=assignCourseObj.TeacherId
                                         select users).ToListAsync();
                }
                else
                {
                     return await (from users in _db.ApplicationUsers join
                                        userDetails in _db.UserDetails on users.Id equals userDetails.UserId
                                         join aspnetUserRoles in _db.UserRoles on users.Id equals aspnetUserRoles.UserId
                                         join roles in _db.Roles on aspnetUserRoles.RoleId equals roles.Id where
                                         roles.Name == "Faculty" && users.Id != assignCourseObj.TeacherId
                                         select users).ToListAsync();
                }

            }
            else
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
            
           


           
        }

        public async Task<IEnumerable<Course>>GetAllCourse(Guid departmentId)
        {
            return await _db.Courses.Where(x => x.DepartmentId == departmentId).ToListAsync();
        }

        public async Task<bool>GetSectionValidity(Guid semesterId,Guid courseId,Guid sectionId)
        {
            var sectionList = await _db.AssignRegistrationCourses.Where(x => x.SemesterId == semesterId && x.CourseId == courseId).
                Select(x => x.SectionId).ToListAsync();
            
            if(sectionList.Contains(sectionId))
            {
                return true;
            }
            return false;
        }
        
        public async Task<bool>GetTeacherSlotValidity(string teacherId,Guid semesterId,string firstDate,string secondDate,DateTime startTime)
        {
            var courseList = await _db.AssignRegistrationCourses.Where(x => x.TeacherId == teacherId && x.SemesterId == semesterId
                          &&(x.FirstDate.ToLower().Equals(firstDate.ToLower()) || x.SecondDate.ToLower().Equals(firstDate.ToLower())
                          || x.SecondDate.ToLower().Equals(secondDate.ToLower()) || x.FirstDate.ToLower().Equals(secondDate.ToLower())) &&
                          x.StartTime.TimeOfDay<=startTime.TimeOfDay && x.EndTime.TimeOfDay>startTime.TimeOfDay).ToListAsync();
                        
            if(courseList.Count()>0)
            {
                return true;
            }
            return false;
        }
        public async Task<int>GetCourseType(Guid courseId)
        {
            return await _db.Courses.Where(x => x.Id == courseId).Select(x => x.CourseProtoType.Credit).FirstOrDefaultAsync();
            
        }
        
    }
}
