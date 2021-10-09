using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
    [Authorize(Roles ="Admin,Super Admin")]
    public class AssignRegistrationCourseController : Controller
    {
        private readonly IUnitOfWork _unitofWork;
       
        public AssignRegistrationCourseController(IUnitOfWork unitOfWork)
        {
            _unitofWork = unitOfWork;
           
        }
        #region Index
        public async Task<IActionResult> Index()
        {
            var semesterList = await _unitofWork.Semester.GetAllAsync();
            var departmentList = await _unitofWork.Department.GetAllAsync();
            var yearList = await _unitofWork.PreregiCourses.SelectYear();

            AssinRegistrationCourseVM assinRegistrationCourseVM = new AssinRegistrationCourseVM()
            {
                SemesterList = semesterList.Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }),
                DepartmentList = departmentList.Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }),
                YearList = yearList.Select(x => new SelectListItem()
                {
                    Text = x.ToString(),
                    Value = x.ToString()
                }),
            };
            return View(assinRegistrationCourseVM);
        }

        public async Task<IActionResult>AssignCourseTable(string searchValue,Guid departmentId,Guid semesterId,int year,int pageNo,int pageSize)
        {
            pageNo = pageNo != 0 ? pageNo : 1;
            pageSize = 10;
            var numberOfAssignCourse = await _unitofWork.
                AssignRegistrationCourse.CountAsync(searchValue, departmentId, semesterId, year);
            AssignCourseTableVM assignCourseTableVM = new AssignCourseTableVM()
            {
                AssignRegistrationCourseList = await _unitofWork.AssignRegistrationCourse.
                        SearchAsync(searchValue, departmentId, semesterId, year, pageNo, pageSize),
                Search = searchValue,
                DepartmentId = departmentId,
                SemesterId = semesterId,
                Year = year,
                Pager = new Pager(numberOfAssignCourse, pageNo, pageSize)
            };
            foreach (var course in assignCourseTableVM.AssignRegistrationCourseList)
            {
                course.Count = await _unitofWork.RegistrationCourse.CountRegistered(course.Id);
            }
            return PartialView("_assignCourseTable", assignCourseTableVM);
        }
        #endregion

        #region Upsert
        public async Task<IActionResult>Upsert(Guid id)
        {
            try
            {
                var semesterList = await _unitofWork.Semester.GetAllAsync();
                var courseList = await _unitofWork.Course.GetAllAsync();
                var sectionList = await _unitofWork.Section.GetAllAsync();
                var teacherList = await _unitofWork.AssignRegistrationCourse.GetAllFaculty(Guid.Empty);
                var departmentList = await _unitofWork.Department.GetAllAsync();
                var dayList = await _unitofWork.Day.GetAllAsync();
                AssginRegistrationCourseUpsertVM assginRegistrationCourseUpsertVM = new AssginRegistrationCourseUpsertVM()
                {
                    AssignRegistrationCourse = new AssignRegistrationCourse(),
                    SemesterList = semesterList.Select(x => new SelectListItem()
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }),
                    CourseList = courseList.Select(x => new SelectListItem()
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }),
                    SectionList = sectionList.Select(x => new SelectListItem()
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }),
                    TeacherList = teacherList.Select(x => new SelectListItem()
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }),
                    DepartemntList = departmentList.Select(x => new SelectListItem()
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }),
                    DayList=dayList.Select(x=>new SelectListItem()
                    { 
                        Text=x.Name,
                        Value=x.Name
                    })

                };
             
                if(id==Guid.Empty)
                {
                    return View(assginRegistrationCourseUpsertVM);
                }
                else
                {
                    assginRegistrationCourseUpsertVM.AssignRegistrationCourse = await _unitofWork.AssignRegistrationCourse.
                        FirstOrDefaultAsync(x => x.Id == id,includeProperties:"ApplicationUser");
                    var courseObj = await _unitofWork.Course.
                        FirstOrDefaultAsync(x => x.Id == assginRegistrationCourseUpsertVM.AssignRegistrationCourse.CourseId);
                    assginRegistrationCourseUpsertVM.AssignRegistrationCourse.DepartmentId = courseObj.DepartmentId;
                    if(assginRegistrationCourseUpsertVM.AssignRegistrationCourse==null)
                    {
                        return NotFound();
                    }
                    return View(assginRegistrationCourseUpsertVM);
                }
                
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(AssginRegistrationCourseUpsertVM assginRegistrationCourseUpsertVM)
        {
            try
            {
                if(assginRegistrationCourseUpsertVM.AssignRegistrationCourse.Id!=Guid.Empty)
                {

                    await _unitofWork.AssignRegistrationCourse.UpdateAsync(assginRegistrationCourseUpsertVM.AssignRegistrationCourse);                   
                    TempData["Success"] = "Successfully Updated";
                }
                else
                {
                    await _unitofWork.AssignRegistrationCourse.AddAsync(assginRegistrationCourseUpsertVM.AssignRegistrationCourse);
                    TempData["Success"] = "Successfully Created ";
                }
                await _unitofWork.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
           
        }
        #endregion
        #region Cascade
        public async Task<IEnumerable<ApplicationUser>>GetFacultyByDepartment(Guid departmentId)
        {
            return await _unitofWork.AssignRegistrationCourse.GetAllFaculty(departmentId);
        }
        public async Task<IEnumerable<Course>>GetCourseByDepartment(Guid departmentId)
        {
            return await _unitofWork.AssignRegistrationCourse.GetAllCourse(departmentId);
        }

        #endregion

        #region Delete
        [HttpPost]
        public async Task<IActionResult>Delete(Guid id,int pageNo,string searchValue,Guid departmentId,Guid semesterId,int year)
        {
            try
            {
                if(id!=Guid.Empty)
                {
                    var assignCourseObj = await _unitofWork.AssignRegistrationCourse.
                        FirstOrDefaultAsync(x => x.Id == id);
                    if(assignCourseObj==null)
                    {
                        return NotFound();
                    }
                    await _unitofWork.AssignRegistrationCourse.RemoveAsync(assignCourseObj);
                    await _unitofWork.SaveAsync();
                    pageNo = pageNo != 0 ? pageNo : 1;
                    int pageSize = 10;
                    var numberOfAssignCourse = await _unitofWork.
                        AssignRegistrationCourse.CountAsync(searchValue, departmentId, semesterId, year);
                    AssignCourseTableVM assignCourseTableVM = new AssignCourseTableVM()
                    {
                        AssignRegistrationCourseList = await _unitofWork.AssignRegistrationCourse.
                                SearchAsync(searchValue, departmentId, semesterId, year, pageNo, pageSize),
                        Search = searchValue,
                        DepartmentId = departmentId,
                        SemesterId = semesterId,
                        Year = year,
                        Pager = new Pager(numberOfAssignCourse, pageNo, pageSize)
                    };
                    return PartialView("_assignCourseTable", assignCourseTableVM);
                }
                return BadRequest();
            }
            catch(Exception ex)
            {
                return BadRequest();
            }

        }
        #endregion
    }
}
