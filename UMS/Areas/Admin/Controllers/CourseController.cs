using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UMS.Data.Data;
using UMS.Data.IRepository;
using UMS.Models.Models;
using UMS.Models.ViewModels;
using UMS.Utility;

namespace UMS.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CourseController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _db;
        public CourseController(IUnitOfWork unitOfWork,ApplicationDbContext db)
        {
            _unitOfWork = unitOfWork;
            _db = db;
        }
        #region Index
        public async Task<IActionResult> Index()
        {
            ViewBag.DepartmentList = await _unitOfWork.Department.GetAllAsync();
            return View();
        }
        public async Task<IActionResult>CourseTable(string searchValue,Guid departmentId,int pageNo,int pageSize)
        {
            try
            {
                pageNo = pageNo != 0 ? pageNo : 1;
                pageSize = 10;
                var numberOfCourse = await _unitOfWork.Course.CountAsync(searchValue, departmentId);
                CourseVM courseVM = new CourseVM()
                {
                    CourseList = await _unitOfWork.Course.SearchAsync(searchValue, departmentId, pageNo, pageSize),
                    Search = searchValue,
                    DepartmentId = departmentId,
                    Pager = new Pager(numberOfCourse, pageNo, pageSize)
                };
                return PartialView("_CourseTable", courseVM);
            }
            catch(Exception ex)
            {
                return NotFound();
            }
            
        }
        #endregion

        #region Upsert
        public async Task<IActionResult>Upsert(Guid id)
        {
            try
            {         
                var courseTypeList = await _unitOfWork.CourseType.GetAllAsync();
                var courseProtoType = await _unitOfWork.CourseProtoType.GetAllAsync();
                var departmentList = await _unitOfWork.Department.GetAllAsync();
                var preCourseList = await _unitOfWork.CoursePrerequisite.GetAllAsync(x=>x.Id!=id);
                CourseUpsertVM courseUpsertVM = new CourseUpsertVM()
                {
                    Course = new Course(),
                    CourseTypeList = courseTypeList.Select(x => new SelectListItem()
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }),
                    CourseProtoTypeList = courseProtoType.Select(x => new SelectListItem()
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }),
                    DepartmentList = departmentList.Select(x => new SelectListItem()
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }),
                    CourseList=preCourseList.Select(x=>new SelectListItem()
                    {
                        Text=x.Name,
                        Value=x.Id.ToString()
                    })
                };

                if(id.Equals(Guid.Empty))
                {
                    return View(courseUpsertVM);
                }
                else
                {    
                    
                    courseUpsertVM.Course = await _unitOfWork.Course.FirstOrDefaultAsync(x => x.Id.Equals(id));
                    var courseList = await _unitOfWork.Course.GetAllAsync(x => x.DepartmentId.Equals(courseUpsertVM.Course.DepartmentId));
                    var finalCourseList = courseList.Where(x => !x.Id.Equals(id)).ToList();
                    courseUpsertVM.Course.CoursePreId = await _unitOfWork.CourseToCoursePrerequisite.GetCoursePreId(id);
                    courseUpsertVM.CourseList = finalCourseList.Select(x => new SelectListItem()
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }) ;
                    if (courseUpsertVM.Course==null)
                    {
                        return NotFound();
                    }
                    return View(courseUpsertVM);
                }
            }
            catch(Exception ex)
            {
                return NotFound();
            }
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Upsert(CourseUpsertVM courseUpsertVM)
        {
            try
            {
               if(ModelState.IsValid)
                {
                    if (courseUpsertVM.Course.Id.Equals(Guid.Empty))
                    {
                        await _unitOfWork.Course.AddAsync(courseUpsertVM.Course);
                        await _unitOfWork.SaveAsync();
                        CoursePrerequisite couresePreObj = new CoursePrerequisite();
                        couresePreObj.Id = courseUpsertVM.Course.Id;
                        couresePreObj.Name = courseUpsertVM.Course.Name;
                        couresePreObj.InitialName = courseUpsertVM.Course.Initial;
                        await _unitOfWork.CoursePrerequisite.AddAsync(couresePreObj);
                        await _unitOfWork.SaveAsync();
                        if (courseUpsertVM.Course.CoursePreId!=null)
                        {
                            foreach (var course in courseUpsertVM.Course.CoursePreId)
                            {
                                CourseToCoursePrerequisite courseToCoursePre = new CourseToCoursePrerequisite();
                                courseToCoursePre.CourseId = courseUpsertVM.Course.Id;
                                courseToCoursePre.CoursePreId = course;
                                await _unitOfWork.CourseToCoursePrerequisite.AddAsync(courseToCoursePre);
                                await _unitOfWork.SaveAsync();
                            }                         
                        }                                   
                        TempData["message"] = "Successfully Created";                       
                    }
                    else
                    {
                        await _unitOfWork.Course.UpdateAsync(courseUpsertVM.Course);
                        
                        if (!courseUpsertVM.Course.CoursePreId.Equals(Guid.Empty))
                        {
                            foreach (var course in courseUpsertVM.Course.CoursePreId)
                            {
                                CourseToCoursePrerequisite courseToCoursePre = new CourseToCoursePrerequisite();
                                courseToCoursePre.CourseId = courseUpsertVM.Course.Id;
                                courseToCoursePre.CoursePreId = course;
                                await _unitOfWork.CourseToCoursePrerequisite.AddAsync(courseToCoursePre);
                                await _unitOfWork.SaveAsync();
                            }
                        }
                        TempData["message"] = "Successfully Updated";
                        await _unitOfWork.SaveAsync();
                    }         
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    try
                    {
                        var courseTypeList = await _unitOfWork.CourseType.GetAllAsync();
                        var courseProtoType = await _unitOfWork.CourseProtoType.GetAllAsync();
                        var departmentList = await _unitOfWork.Department.GetAllAsync();

                        courseUpsertVM.CourseTypeList = courseTypeList.Select(x => new SelectListItem()
                        {
                            Text = x.Name,
                            Value = x.Id.ToString()
                        });
                        courseUpsertVM.CourseProtoTypeList = courseProtoType.Select(x => new SelectListItem()
                        {
                            Text = x.Name,
                            Value = x.Id.ToString()
                        });
                        courseUpsertVM.DepartmentList = departmentList.Select(x => new SelectListItem()
                        {
                            Text = x.Name,
                            Value = x.Id.ToString()
                        });


                        if (!courseUpsertVM.Course.Id.Equals(Guid.Empty))
                        {
                            courseUpsertVM.Course = await _unitOfWork.Course.
                                FirstOrDefaultAsync(x => x.Id.Equals(courseUpsertVM.Course.Id));
                        }
                        return View(courseUpsertVM);
                        
                    }
                    catch(Exception ex)
                    {
                        return NotFound();
                    }
                }
            }

            catch(Exception ex)
            {
                return NotFound();
            }
        }
        #endregion
        #region Cascade
        public async Task<IEnumerable<Course>> GetCourseList(Guid id,Guid courseId)
        {
            var couseList= await _unitOfWork.Course.GetCourseByDepartment(id,courseId);
            return couseList;
        }
        #endregion

        #region Delete
        [HttpPost]
        public async Task<IActionResult>Delete(Guid id)
        {
            try
            {
                Course courseObj = await _unitOfWork.Course.FirstOrDefaultAsync(x => x.Id.Equals(id));
                CoursePrerequisite coursePreObj = await _unitOfWork.CoursePrerequisite.
                    FirstOrDefaultAsync(x => x.Name.ToLower().Equals(courseObj.Name.ToLower()));
                if (courseObj==null || coursePreObj==null)
                {
                    return NotFound();
                }
                await _unitOfWork.Course.RemoveAsync(courseObj);
                
                await _unitOfWork.CoursePrerequisite.RemoveAsync(coursePreObj);
                await _unitOfWork.SaveAsync();
                int pageNo = 1;
                int pageSize = 10;
                var numberOfCourse = await _unitOfWork.Course.CountAsync(null,Guid.Empty);
                CourseVM courseVM = new CourseVM()
                {
                    CourseList = await _unitOfWork.Course.SearchAsync(null, Guid.Empty, pageNo, pageSize),
                    Search = null,
                    DepartmentId = Guid.Empty,
                    Pager = new Pager(numberOfCourse, pageNo, pageSize)
                };
                return PartialView("_CourseTable", courseVM);
            }
            catch(Exception ex)
            {
                return NotFound();
            }
        }
        #endregion

        #region Exists
        public async Task<IActionResult> ExistName (string name)
        {
            try
            {
                var exist = await _unitOfWork.Course.Exists(x => x.Name.ToLower().Equals(name.ToLower()));
                if(exist==true)
                {
                    return Json(new { success = false });
                }
                return Json(new { success = true });
            }
            catch(Exception ex)
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> ExistInitial(string Initial)
        {
            try
            {
                var exist = await _unitOfWork.Course.Exists(x => x.Initial.ToLower().Equals(Initial.ToLower()));
                if (exist == true)
                {
                    return Json(new { success = false });
                }
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        #endregion

        #region AssginPrerequisite
        public async Task<IActionResult> ManagePrerequisite(Guid id)
        {
            try
            {
                var departmentList = await _unitOfWork.Department.GetAllAsync();
                AssignCourseVM assignCourseVM = new AssignCourseVM()
                {
                    Course = await _unitOfWork.Course.FirstOrDefaultAsync(x => x.Id.Equals(id)),
                    CourseToCoursePrerequisite = new CourseToCoursePrerequisite()
                    {
                        CourseId = id
                    },
                    CourseToCoursePrerequisiteList = await _unitOfWork.CourseToCoursePrerequisite.
                    GetAllAsync(x => x.CourseId.Equals(id), includeProperties: "Course,CoursePrerequisite")
                };
                List<Guid> assignPrerequisite = assignCourseVM.CourseToCoursePrerequisiteList.
                    Select(x => x.CoursePreId).ToList();
                var tempPreList = await _unitOfWork.Course.
                    GetAllAsync(x => !assignPrerequisite.Contains(x.Id) && !x.Id.Equals(id) && x.DepartmentId.Equals(assignCourseVM.Course.DepartmentId));
                assignCourseVM.CoursePreList = tempPreList.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                });
                return View(assignCourseVM);
            }
            catch(Exception ex)
            {
                return NotFound();
            }
            
        }
        public async Task<IActionResult> PreCourseTable(Guid id)
        {
            PrerequisiteCourseVM prerequisiteCourse = new PrerequisiteCourseVM()
            {
                PrerequisiteCourseList = await _unitOfWork.CourseToCoursePrerequisite.GetAllAsync(x => x.CourseId.Equals(id),
                                        includeProperties: "Course,CoursePrerequisite"),
                CourseId=id
            };
            return PartialView("_PreCourseTable", prerequisiteCourse);

        }
        [HttpPost]
        public async Task<IActionResult> ManagePrerequisite(Guid courseId,Guid preCourseId )
        {
            try
            {
                if(courseId!=Guid.Empty && preCourseId!=Guid.Empty)
                {
                    //var exits = await _unitOfWork.CourseToCoursePrerequisite.
                    //    GetAllAsync(x => x.CourseId.Equals(courseId) && x.CoursePreId.Equals(preCourseId));
                    //if(exits.Count()>0)
                    //{
                    //    return Json(new { success = false });

                    //}
                    CourseToCoursePrerequisite courseObj = new CourseToCoursePrerequisite();
                    courseObj.CourseId = courseId;
                    courseObj.CoursePreId = preCourseId;
                    await _unitOfWork.CourseToCoursePrerequisite.AddAsync(courseObj);
                    await _unitOfWork.SaveAsync();
                    PrerequisiteCourseVM prerequisiteCourse = new PrerequisiteCourseVM()
                    {
                        PrerequisiteCourseList = await _unitOfWork.CourseToCoursePrerequisite.GetAllAsync(x => x.CourseId.Equals(courseId),
                                        includeProperties: "Course,CoursePrerequisite"),
                        CourseId=courseId
                    };
                    return PartialView("_PreCourseTable", prerequisiteCourse);
                }
                return View();
            }
            catch(Exception ex)
            {
                return NotFound();
            }
        }
        
        [HttpPost]
        public async Task<IActionResult>DeletePreCourse(Guid courseId,Guid coursePreId)
        {
            var preCourseObj = await _unitOfWork.CourseToCoursePrerequisite.
                FirstOrDefaultAsync(x => x.CoursePreId.Equals(coursePreId) && x.CourseId.Equals(courseId));
            if(preCourseObj==null)
            {
                return NotFound();
            }
            await _unitOfWork.CourseToCoursePrerequisite.RemoveAsync(preCourseObj);
            await _unitOfWork.SaveAsync();
            PrerequisiteCourseVM prerequisiteCourse = new PrerequisiteCourseVM()
            {
                PrerequisiteCourseList = await _unitOfWork.CourseToCoursePrerequisite.GetAllAsync(x => x.CourseId.Equals(courseId),
                                        includeProperties: "Course,CoursePrerequisite"),
                CourseId=courseId
            };
            return PartialView("_PreCourseTable", prerequisiteCourse);

        }
        #endregion
    }
}
