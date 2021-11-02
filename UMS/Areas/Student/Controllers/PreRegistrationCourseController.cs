using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using UMS.Data.IRepository;
using UMS.Models.Models;
using UMS.Models.ViewModels;

namespace UMS.Areas.Student.Controllers
{
    [Area("Student")]
    [Authorize]
    public class PreRegistrationCourseController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public PreRegistrationCourseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #region Index
        [Route("Preregistration")]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var activity = await _unitOfWork.Activity.FirstOrDefaultAsync(x => x.Name == "Preregistration");
            var semesterList = await _unitOfWork.Semester.GetAllAsync();
            if(User.IsInRole("Admin,Super Admin"))
            {
                semesterList = await _unitOfWork.Semester.GetStudentSemester(userId);
            }           
            var deparmentList = await _unitOfWork.Department.GetAllAsync();
            
            ViewBag.SemesterList = semesterList.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
            ViewBag.DepartmentList = deparmentList.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
            return View(activity);
        }
        public async Task<IActionResult> CourseTable(string searchValue,Guid departmentId,Guid semesterId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _unitOfWork.ApplicationUser.FirstOrDefaultAsync(x => x.Id == userId, includeProperties: "UserDetails");
            var userDetailsObj = await _unitOfWork.UserDetials.FirstOrDefaultAsync(x => x.UserId == userId);           
            var semesterObj = await _unitOfWork.Semester.FirstOrDefaultAsync(x => x.IsActive == true);
          
            semesterId = semesterId != Guid.Empty ? semesterId : semesterObj.Id;
            if (userDetailsObj != null)
            {
                PreRegistrationVM preRegistrationVM = new PreRegistrationVM()
                {
                    User = user,
                    CourseList = await _unitOfWork.PreRegistationCourses.GetAllCourses(searchValue, userDetailsObj.DepartmentId),
                };              
                if(preRegistrationVM.CourseList.Count()>0)
                {
                    var courseIdList = await _unitOfWork.PreRegistationCourses.SelectPreCourseId(userId, semesterId);
                    foreach (var course in preRegistrationVM.CourseList)
                    {
                        if (courseIdList.Contains(course.Id))
                        {
                            course.IsTaken = true;
                        }
                    }
                }            
                return PartialView("_CourseTable", preRegistrationVM);
            }
            else
            {
                if(departmentId!=Guid.Empty)
                {
                    PreRegistrationVM preRegistrationVM = new PreRegistrationVM()
                    {
                        User = user,
                        CourseList = await _unitOfWork.PreRegistationCourses.GetAllCourses(searchValue,departmentId),
                    };
                    var courseIdList = await _unitOfWork.PreRegistationCourses.SelectPreCourseId(null, semesterId);
                    foreach (var course in preRegistrationVM.CourseList)
                    {
                        if (courseIdList.Contains(course.Id))
                        {                           
                            course.Count = await _unitOfWork.PreRegistationCourses.CountStudent(course.Id, semesterId);
                        }
                    }

                    return PartialView("_CourseTable", preRegistrationVM);
                }
                else
                {
                    PreRegistrationVM preRegistrationVM = new PreRegistrationVM()
                    {
                        User = user,
                        CourseList = await _unitOfWork.PreRegistationCourses.GetAllCourses(searchValue),
                    };
                    var courseIdList = await _unitOfWork.PreRegistationCourses.
                        SelectPreCourseId(null, semesterId);
                    foreach (var course in preRegistrationVM.CourseList)
                    {
                        if (courseIdList.Contains(course.Id))
                        {                           
                            course.Count = await _unitOfWork.PreRegistationCourses.CountStudent(course.Id, semesterId);
                        }
                    }
                    return PartialView("_CourseTable", preRegistrationVM);
                }
                
            }
        }
        public async Task<IActionResult> SelectPreCourseTable(string userId,Guid semesterId)
        {
            userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var semesterObj = await _unitOfWork.Semester.FirstOrDefaultAsync(x => x.IsActive == true);
            var activityObj = await _unitOfWork.Activity.FirstOrDefaultAsync(x => x.Name == "Preregistration");
            
            if(activityObj.StartDate<=DateTime.Now.AddHours(6) &&activityObj.EndDate>=DateTime.Now.AddHours(6)&&activityObj.IsActive==true)
            {
                PreCourseVM preCourseVM = new PreCourseVM()
                {
                    CourseList = await _unitOfWork.PreRegistationCourses.
                         GetPreCourses(userId, semesterObj.Id),
                    StudentId = userId
                };
                return PartialView("_SelectPreCourseTable", preCourseVM);
            }
            else
            {
                semesterId = semesterId == Guid.Empty ? semesterObj.Id : semesterId;              
                PreCourseVM preCourseVM = new PreCourseVM()
                {
                    CourseList = await _unitOfWork.PreRegistationCourses.
                         GetPreCourses(userId, semesterId),
                    StudentId = userId
                };
                return PartialView("_SelectPreCourseTable", preCourseVM);
            }
          
        }
        [HttpPost]
        public async Task<IActionResult> SelectPreregistationCourse(string userId, Guid courseId)
        {
            try
            {
                var activity = await _unitOfWork.Activity.FirstOrDefaultAsync(x => x.Name == "Preregistration");
                var semesterObj = await _unitOfWork.Semester.FirstOrDefaultAsync(x => x.IsActive == true);
                if ((activity.StartDate <= DateTime.Now.AddHours(6) && DateTime.Now.AddHours(6) <= activity.EndDate))
                {
                    if (activity.IsActive == true)
                    {
                        if (!String.IsNullOrEmpty(userId) && courseId != Guid.Empty)
                        {                         
                            var assignPreCourseObj = await _unitOfWork.PreregiCourses
                                .SelectCourseId(courseId, semesterObj.Id);
                            if(!assignPreCourseObj.Contains(courseId))
                            {
                                PreregistrationCourses preCourseObj = new PreregistrationCourses();
                                preCourseObj.CourseId = courseId;
                                preCourseObj.SemesterId = semesterObj.Id;                              
                                await _unitOfWork.PreregiCourses.AddAsync(preCourseObj);
                                await _unitOfWork.SaveAsync();
                                AssignPreRegistrationCourse preCourse = new AssignPreRegistrationCourse();
                                preCourse.PreCourseId = preCourseObj.Id;
                                preCourse.StudentId = userId;
                                await _unitOfWork.PreRegistationCourses.AddAsync(preCourse);
                                await _unitOfWork.SaveAsync();
                                PreCourseVM preCourseVM = new PreCourseVM()
                                {
                                    CourseList = await _unitOfWork.PreRegistationCourses.
                                    GetPreCourses(userId, semesterObj.Id),
                                    StudentId = userId
                                };
                                return PartialView("_SelectPreCourseTable", preCourseVM);
                            }
                            else
                            {
                                var preCourseId = await _unitOfWork.PreregiCourses.
                                    FirstOrDefaultAsync(x => x.CourseId == courseId && x.SemesterId == semesterObj.Id);
                                AssignPreRegistrationCourse preCourse = new AssignPreRegistrationCourse();
                                preCourse.PreCourseId = preCourseId.Id;
                                preCourse.StudentId = userId;
                                await _unitOfWork.PreRegistationCourses.AddAsync(preCourse);
                                await _unitOfWork.SaveAsync();
                                PreCourseVM preCourseVM = new PreCourseVM()
                                {
                                    CourseList = await _unitOfWork.PreRegistationCourses.
                                    GetPreCourses(userId, semesterObj.Id),
                                    StudentId = userId
                                };
                                return PartialView("_SelectPreCourseTable", preCourseVM);
                            }

                        }
                        activity.IsActive = false;
                        return BadRequest();
                    }
                    return BadRequest();

                }
                return BadRequest();

            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public async Task<IActionResult> DeletePreregistationCourse(string userId, Guid courseId)
        {
            try
            {
                var activity = await _unitOfWork.Activity.FirstOrDefaultAsync(x => x.Name == "Preregistration");
                if ((activity.StartDate <= DateTime.Now.AddHours(6) && DateTime.Now.AddHours(6) <= activity.EndDate))
                {
                    if (activity.IsActive == true)
                    {
                        if (!String.IsNullOrEmpty(userId) && courseId != Guid.Empty)
                        {
                            var semesterObj = await _unitOfWork.Semester.FirstOrDefaultAsync(x => x.IsActive == true);

                            var preCourseObj = await _unitOfWork.PreregiCourses.
                                FirstOrDefaultAsync(x => x.CourseId == courseId && x.SemesterId == semesterObj.Id);
                            var selectPreCourseObj = await _unitOfWork.PreRegistationCourses.
                                FirstOrDefaultAsync(x => x.PreCourseId == preCourseObj.Id && x.StudentId == userId);
                            if (preCourseObj == null)
                            {
                                return NotFound();
                            }
                            await _unitOfWork.PreRegistationCourses.RemoveAsync(selectPreCourseObj);
                            await _unitOfWork.SaveAsync();
                            PreCourseVM preCourseVM = new PreCourseVM()
                            {
                                CourseList = await _unitOfWork.PreRegistationCourses.
                                GetPreCourses(userId,semesterObj.Id),
                                StudentId = userId
                            };
                            return PartialView("_SelectPreCourseTable", preCourseVM);
                        }
                    }
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        #endregion
    }
}
