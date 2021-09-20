using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public CourseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
                pageSize = 1;
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
                var preCourseList = await _unitOfWork.CoursePrerequisite.GetAllAsync();
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
                    if(courseUpsertVM.Course==null)
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
                        //Course course = new Course();
                        //course.Name = courseUpsertVM.Course.Name;
                        //course.Initial = courseUpsertVM.Course.Initial;
                        //course.DepartmentId = courseUpsertVM.Course.DepartmentId;
                        //course.CourseProtoTypeId = courseUpsertVM.Course.CourseProtoTypeId;
                        //course.CourseType = courseUpsertVM.Course.CourseType;
                        await _unitOfWork.Course.AddAsync(courseUpsertVM.Course);
                        await _unitOfWork.SaveAsync();
                        CoursePrerequisite coursePre = new CoursePrerequisite();
                        coursePre.Name = courseUpsertVM.Course.Name;
                        coursePre.InitialName = courseUpsertVM.Course.Initial;
                        await _unitOfWork.CoursePrerequisite.AddAsync(coursePre);
                        await _unitOfWork.SaveAsync();
                        CourseToCoursePrerequisite courseToCoursePre = new CourseToCoursePrerequisite();
                        courseToCoursePre.CourseId = courseUpsertVM.Course.Id;
                        courseToCoursePre.CoursePreId = coursePre.Id;
                        await _unitOfWork.SaveAsync();
                        TempData["message"] = "Successfully Created";

                    }
                    else
                    {
                        await _unitOfWork.Course.UpdateAsync(courseUpsertVM.Course);
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
                int pageSize = 1;
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
    }
}
