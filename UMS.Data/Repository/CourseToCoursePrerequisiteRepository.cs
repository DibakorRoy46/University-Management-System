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
    public class CourseToCoursePrerequisiteRepository:Repository<CourseToCoursePrerequisite>, ICourseToCoursePrerequisiteRepository
    {
        private readonly ApplicationDbContext _db;
        public CourseToCoursePrerequisiteRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public async Task<int> CountAsync(string searchValue,Guid? departmentId, Guid? courseId, Guid? coursePreId)
        {
            var courseList = await _db.CourseToCoursePrerequisites.
                Include(x => x.Course).Include(x => x.CoursePrerequisite).ToListAsync();
            if(!String.IsNullOrEmpty(searchValue))
            {
                courseList = await _db.CourseToCoursePrerequisites.Include(x => x.Course).Include(x => x.CoursePrerequisite).
                    Where(x => x.Course.Name.Contains(searchValue)).ToListAsync();

                if(!departmentId.Equals(Guid.Empty)&&departmentId!=null)
                {
                    courseList = courseList.Where(x => x.Course.DepartmentId.Equals(departmentId)).ToList();
                    if(!courseId.Equals(Guid.Empty) && courseId!=null)
                    {
                        courseList = courseList.Where(x => x.CourseId.Equals(courseId)).ToList();
                        if (!coursePreId.Equals(Guid.Empty))
                        {
                            courseList = courseList.Where(x => x.CoursePreId.Equals(coursePreId)).ToList();

                        }
                    }
                    else
                    {
                        courseList = courseList.ToList();
                        if (!courseId.Equals(Guid.Empty)&&courseId!=null)
                        {
                            courseList = courseList.Where(x => x.CourseId.Equals(courseId)).ToList();
                            if (!coursePreId.Equals(Guid.Empty) && coursePreId!=null)
                            {
                                courseList = courseList.Where(x => x.CoursePreId.Equals(coursePreId)).ToList();

                            }
                        }
                    }
                }
                else
                {
                    courseList = courseList.ToList();
                    if (!departmentId.Equals(Guid.Empty) && departmentId != null)
                    {
                        courseList = courseList.Where(x => x.Course.DepartmentId.Equals(departmentId)).ToList();
                        if (!courseId.Equals(Guid.Empty) && courseId != null)
                        {
                            courseList = courseList.Where(x => x.CourseId.Equals(courseId)).ToList();
                            if (!coursePreId.Equals(Guid.Empty))
                            {
                                courseList = courseList.Where(x => x.CoursePreId.Equals(coursePreId)).ToList();

                            }
                        }
                        else
                        {
                            courseList = courseList.ToList();
                            if (!courseId.Equals(Guid.Empty) && courseId != null)
                            {
                                courseList = courseList.Where(x => x.CourseId.Equals(courseId)).ToList();
                                if (!coursePreId.Equals(Guid.Empty) && coursePreId != null)
                                {
                                    courseList = courseList.Where(x => x.CoursePreId.Equals(coursePreId)).ToList();

                                }
                            }
                        }
                    }
                }
                
            }
            else
            {
                courseList = courseList.ToList();
                if (!departmentId.Equals(Guid.Empty) && departmentId != null)
                {
                    courseList = courseList.Where(x => x.Course.DepartmentId.Equals(departmentId)).ToList();
                    if (!courseId.Equals(Guid.Empty) && courseId != null)
                    {
                        courseList = courseList.Where(x => x.CourseId.Equals(courseId)).ToList();
                        if (!coursePreId.Equals(Guid.Empty))
                        {
                            courseList = courseList.Where(x => x.CoursePreId.Equals(coursePreId)).ToList();

                        }
                    }
                    else
                    {
                        courseList = courseList.ToList();
                        if (!courseId.Equals(Guid.Empty) && courseId != null)
                        {
                            courseList = courseList.Where(x => x.CourseId.Equals(courseId)).ToList();
                            if (!coursePreId.Equals(Guid.Empty) && coursePreId != null)
                            {
                                courseList = courseList.Where(x => x.CoursePreId.Equals(coursePreId)).ToList();

                            }
                        }
                    }
                }
                else
                {
                    courseList = courseList.ToList();
                    if (!departmentId.Equals(Guid.Empty) && departmentId != null)
                    {
                        courseList = courseList.Where(x => x.Course.DepartmentId.Equals(departmentId)).ToList();
                        if (!courseId.Equals(Guid.Empty) && courseId != null)
                        {
                            courseList = courseList.Where(x => x.CourseId.Equals(courseId)).ToList();
                            if (!coursePreId.Equals(Guid.Empty))
                            {
                                courseList = courseList.Where(x => x.CoursePreId.Equals(coursePreId)).ToList();

                            }
                        }
                        else
                        {
                            courseList = courseList.ToList();
                            if (!courseId.Equals(Guid.Empty) && courseId != null)
                            {
                                courseList = courseList.Where(x => x.CourseId.Equals(courseId)).ToList();
                                if (!coursePreId.Equals(Guid.Empty) && coursePreId != null)
                                {
                                    courseList = courseList.Where(x => x.CoursePreId.Equals(coursePreId)).ToList();

                                }
                            }
                        }
                    }
                }

            }
            return courseList.Count();
        }

        public async Task<IEnumerable<Guid>> GetCourseId(Guid coursePreId)
        {
            return await  _db.CourseToCoursePrerequisites.Where(x => x.CoursePreId.Equals(coursePreId)).Select(x => x.CourseId).ToListAsync();
        }

        public async Task<IEnumerable<Guid>> GetCoursePreId(Guid courseId)
        { 
            return await  _db.CourseToCoursePrerequisites.Where(x => x.CourseId.Equals(courseId)).Select(x => x.CoursePreId).ToListAsync();
        }
        public async Task<Guid> GetDepartmentId(Guid courseId)
        {
            return await _db.Courses.Where(x => x.Id.Equals(courseId)).Select(x => x.DepartmentId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<CourseToCoursePrerequisite>> SearchAsync(string searchValue,Guid? departmentId, Guid? courseId, Guid? coursePreId, int pageNo, int pageSize)
        {
            var courseList = await _db.CourseToCoursePrerequisites.
                Include(x => x.Course).Include(x => x.CoursePrerequisite).ToListAsync();
            if (!String.IsNullOrEmpty(searchValue))
            {
                courseList = await _db.CourseToCoursePrerequisites.Include(x => x.Course).Include(x => x.CoursePrerequisite).
                    Where(x => x.Course.Name.Contains(searchValue)).ToListAsync();

                if (!departmentId.Equals(Guid.Empty) && departmentId != null)
                {
                    courseList = courseList.Where(x => x.Course.DepartmentId.Equals(departmentId)).ToList();
                    if (!courseId.Equals(Guid.Empty) && courseId != null)
                    {
                        courseList = courseList.Where(x => x.CourseId.Equals(courseId)).ToList();
                        if (!coursePreId.Equals(Guid.Empty))
                        {
                            courseList = courseList.Where(x => x.CoursePreId.Equals(coursePreId)).ToList();

                        }
                    }
                    else
                    {
                        courseList = courseList.ToList();
                        if (!courseId.Equals(Guid.Empty) && courseId != null)
                        {
                            courseList = courseList.Where(x => x.CourseId.Equals(courseId)).ToList();
                            if (!coursePreId.Equals(Guid.Empty) && coursePreId != null)
                            {
                                courseList = courseList.Where(x => x.CoursePreId.Equals(coursePreId)).ToList();

                            }
                        }
                    }
                }
                else
                {
                    courseList = courseList.ToList();
                    if (!departmentId.Equals(Guid.Empty) && departmentId != null)
                    {
                        courseList = courseList.Where(x => x.Course.DepartmentId.Equals(departmentId)).ToList();
                        if (!courseId.Equals(Guid.Empty) && courseId != null)
                        {
                            courseList = courseList.Where(x => x.CourseId.Equals(courseId)).ToList();
                            if (!coursePreId.Equals(Guid.Empty))
                            {
                                courseList = courseList.Where(x => x.CoursePreId.Equals(coursePreId)).ToList();

                            }
                        }
                        else
                        {
                            courseList = courseList.ToList();
                            if (!courseId.Equals(Guid.Empty) && courseId != null)
                            {
                                courseList = courseList.Where(x => x.CourseId.Equals(courseId)).ToList();
                                if (!coursePreId.Equals(Guid.Empty) && coursePreId != null)
                                {
                                    courseList = courseList.Where(x => x.CoursePreId.Equals(coursePreId)).ToList();

                                }
                            }
                        }
                    }
                }

            }
            else
            {
                courseList = courseList.ToList();
                if (!departmentId.Equals(Guid.Empty) && departmentId != null)
                {
                    courseList = courseList.Where(x => x.Course.DepartmentId.Equals(departmentId)).ToList();
                    if (!courseId.Equals(Guid.Empty) && courseId != null)
                    {
                        courseList = courseList.Where(x => x.CourseId.Equals(courseId)).ToList();
                        if (!coursePreId.Equals(Guid.Empty))
                        {
                            courseList = courseList.Where(x => x.CoursePreId.Equals(coursePreId)).ToList();

                        }
                    }
                    else
                    {
                        courseList = courseList.ToList();
                        if (!courseId.Equals(Guid.Empty) && courseId != null)
                        {
                            courseList = courseList.Where(x => x.CourseId.Equals(courseId)).ToList();
                            if (!coursePreId.Equals(Guid.Empty) && coursePreId != null)
                            {
                                courseList = courseList.Where(x => x.CoursePreId.Equals(coursePreId)).ToList();

                            }
                        }
                    }
                }
                else
                {
                    courseList = courseList.ToList();
                    if (!departmentId.Equals(Guid.Empty) && departmentId != null)
                    {
                        courseList = courseList.Where(x => x.Course.DepartmentId.Equals(departmentId)).ToList();
                        if (!courseId.Equals(Guid.Empty) && courseId != null)
                        {
                            courseList = courseList.Where(x => x.CourseId.Equals(courseId)).ToList();
                            if (!coursePreId.Equals(Guid.Empty))
                            {
                                courseList = courseList.Where(x => x.CoursePreId.Equals(coursePreId)).ToList();

                            }
                        }
                        else
                        {
                            courseList = courseList.ToList();
                            if (!courseId.Equals(Guid.Empty) && courseId != null)
                            {
                                courseList = courseList.Where(x => x.CourseId.Equals(courseId)).ToList();
                                if (!coursePreId.Equals(Guid.Empty) && coursePreId != null)
                                {
                                    courseList = courseList.Where(x => x.CoursePreId.Equals(coursePreId)).ToList();

                                }
                            }
                        }
                    }
                }

            }
            return  courseList.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
        }
    }
}
