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
    [Authorize(Roles ="Student")]
    public class RegistrationController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public RegistrationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #region Index
        [Route("Registration")]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var activity = await _unitOfWork.Activity.FirstOrDefaultAsync(x => x.Name == "Registration");
            return View(activity);
        }
        public async Task<IActionResult> CourseTable(string searchValue, Guid departmentId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _unitOfWork.ApplicationUser.FirstOrDefaultAsync(x => x.Id == userId, includeProperties: "UserDetails");
            var userDetailsObj = await _unitOfWork.UserDetials.FirstOrDefaultAsync(x => x.UserId == userId);
            var semesterObj = await _unitOfWork.Semester.FirstOrDefaultAsync(x => x.IsActive == true);
            if (userDetailsObj != null)
            {
                RegistrationCourseVM registrationCourseVM = new RegistrationCourseVM()
                {
                    User = user,
                    CourseList = await _unitOfWork.RegistrationCourse.GetAllCourses(searchValue, userDetailsObj.DepartmentId,semesterObj.Id),
                };
                var courseIdList = await _unitOfWork.RegistrationCourse.SelectRegistrationCourseId(userId);
                foreach (var course in registrationCourseVM.CourseList)
                {
                    if (courseIdList.Contains(course.Id))
                    {
                        course.IsTaken = true;
                    }
                }
                return PartialView("_CourseTable", registrationCourseVM);
            }
            else
            {
                if (departmentId != Guid.Empty)
                {
                    RegistrationCourseVM registrationCourseVM = new RegistrationCourseVM()
                    {
                        User = user,
                        CourseList = await _unitOfWork.RegistrationCourse.GetAllCourses(searchValue, departmentId,semesterObj.Id),
                    };
                    return PartialView("_CourseTable", registrationCourseVM);
                }
                else
                {
                    RegistrationCourseVM registrationCourseVM = new RegistrationCourseVM()
                    {
                        User = user,
                        CourseList = await _unitOfWork.RegistrationCourse.GetAllCourses(searchValue, departmentId,semesterObj.Id),
                    };
                    return PartialView("_CourseTable", registrationCourseVM);
                }
            }
        }
        public async Task<IActionResult> SelectRegisCourseTable(string userId, Guid semesterId, int year)
        {
            userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var semesterObj = await _unitOfWork.Semester.FirstOrDefaultAsync(x => x.IsActive == true);
            var activityObj = await _unitOfWork.Activity.FirstOrDefaultAsync(x => x.Name == "Registration");

            if (activityObj.StartDate <= DateTime.Now && DateTime.Now <= activityObj.EndDate && activityObj.IsActive == true)
            {
                SelectRegistrationCourseVM regisCourseVM = new SelectRegistrationCourseVM()
                {
                    CourseList = await _unitOfWork.RegistrationCourse.
                         GetRegisteredCourses(userId, semesterObj.Id),
                    UserId = userId
                };
                return PartialView("_SelectRegisCourseTable", regisCourseVM);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> SelectRegistationCourse(string userId, Guid courseId)
        {
            try
            {
                var activity = await _unitOfWork.Activity.FirstOrDefaultAsync(x => x.Name == "Registration");
                var semesterObj = await _unitOfWork.Semester.FirstOrDefaultAsync(x => x.IsActive == true);
                if ((activity.StartDate <= DateTime.Now && DateTime.Now <= activity.EndDate))
                {
                    if (activity.IsActive == true)
                    {
                        if (!String.IsNullOrEmpty(userId) && courseId != Guid.Empty)
                        {
                            var courseIdList = await _unitOfWork.RegistrationCourse.TakenCourseId(userId);
                            var assignCourseList = await _unitOfWork.AssignRegistrationCourse.
                                FirstOrDefaultAsync(x => x.Id == courseId);                           
                            var timeValidity = await _unitOfWork.RegistrationCourse.
                                GetTimeAvilabity(userId, semesterObj.Id,assignCourseList.FirstDate,assignCourseList.SecondDate, assignCourseList.StartTime);
                            var takenActive = await _unitOfWork.StudentRegisteationCourse.GetPrerequisiteCourseChecker(userId,courseId);
                            if (!courseIdList.Contains(assignCourseList.CourseId)&& timeValidity==true && takenActive==true)
                            {
                                StudentRegisteationCourse regisCourse = new StudentRegisteationCourse();
                                regisCourse.AssignRegiCourseId = courseId;
                                regisCourse.StudentId = userId;
                                await _unitOfWork.RegistrationCourse.AddAsync(regisCourse);
                                await _unitOfWork.SaveAsync();
                                var semesterId = await _unitOfWork.StudentRegisteationCourse.GetSemester(courseId);
                                StudentSemesterFee studentSemesterFee = new StudentSemesterFee();
                                studentSemesterFee.StudentId = userId;
                                studentSemesterFee.SemesterId = semesterId;
                                var semesterPriceObj = await _unitOfWork.StudentSemeterFee.
                                    FirstOrDefaultAsync(x => x.StudentId == userId && x.SemesterId == semesterId);
                                var userDetailsObj = await _unitOfWork.UserDetials.FirstOrDefaultAsync(x => x.UserId==userId);
                                var departmentObj = await _unitOfWork.Department.FirstOrDefaultAsync(x => x.Id == userDetailsObj.DepartmentId);
                                var assignCourseObj = await _unitOfWork.AssignRegistrationCourse.FirstOrDefaultAsync(x => x.Id == courseId);
                                var courseObj = await _unitOfWork.Course.FirstOrDefaultAsync(x => x.Id == assignCourseObj.CourseId);
                                var courseProtoTypeObj = await _unitOfWork.CourseProtoType.FirstOrDefaultAsync(x => x.Id == courseObj.CourseProtoTypeId);
                                var totalPrice = 0;
                                if (semesterPriceObj!=null)
                                {
                                    totalPrice = semesterPriceObj.TotalPrice;
                                }
                                
                                studentSemesterFee.TotalPrice =totalPrice + (departmentObj.PricePerCredit * courseProtoTypeObj.Credit);
                                if(semesterPriceObj!=null)
                                {
                                    await _unitOfWork.StudentSemeterFee.UpdateAsync(studentSemesterFee);
                                }
                                else
                                {
                                    await _unitOfWork.StudentSemeterFee.AddAsync(studentSemesterFee);
                                }
                                await _unitOfWork.SaveAsync();
                                SelectRegistrationCourseVM regisCourseVM = new SelectRegistrationCourseVM()
                                {
                                    CourseList = await _unitOfWork.RegistrationCourse.
                                         GetRegisteredCourses(userId, semesterObj.Id),
                                    UserId = userId
                                };
                                return PartialView("_SelectRegisCourseTable", regisCourseVM);
                            }
                            else if(courseIdList.Contains(assignCourseList.CourseId))
                            {
                                return Json(new { success = false, message = "You are already taken this course" });
                            }
                            else if(timeValidity==false)
                            {
                                return Json(new { success = false, message = "You are already taken a course at this time" });
                            }
                            else if(takenActive==false)
                            {
                                return Json(new { success = false, message = "Prerequisites Course Incompleted" });
                            }
                            else
                            {
                                return Json(new { success = false, message = "Something Error" });
                            }                      
                        }
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
        public async Task<IActionResult> DeleteRegistationCourse(string userId, Guid courseId)
        {
            try
            {
                var activity = await _unitOfWork.Activity.FirstOrDefaultAsync(x => x.Name == "Registration");
                if ((activity.StartDate <= DateTime.Now && DateTime.Now <= activity.EndDate))
                {
                    if (activity.IsActive == true)
                    {
                        if (!String.IsNullOrEmpty(userId) && courseId != Guid.Empty)
                        {
                            var semesterObj = await _unitOfWork.Semester.FirstOrDefaultAsync(x => x.IsActive == true);


                            var selectCourseObj = await _unitOfWork.RegistrationCourse.
                                FirstOrDefaultAsync(x => x.AssignRegiCourseId == courseId && x.StudentId == userId);
                            if (selectCourseObj == null)
                            {
                                return NotFound();
                            }
                            await _unitOfWork.RegistrationCourse.RemoveAsync(selectCourseObj);
                            await _unitOfWork.SaveAsync();
                            SelectRegistrationCourseVM regisCourseVM = new SelectRegistrationCourseVM()
                            {
                                CourseList = await _unitOfWork.RegistrationCourse.
                                      GetRegisteredCourses(userId, semesterObj.Id),
                                UserId = userId
                            };
                            return PartialView("_SelectRegisCourseTable", regisCourseVM);
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

        #region RegistrationForm
        [Route("RegistrationForm")]
        public async Task<IActionResult>RegistrationForm()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var semesterObj = await _unitOfWork.Semester.FirstOrDefaultAsync(x => x.IsActive == true);
            var semesterList = await _unitOfWork.Semester.GetStudentRegisterSemester(userId);
            RegistrationFormVM registrationFormVM = new RegistrationFormVM()
            {
                StudentId = userId,
                SemesterId = semesterObj.Id,
                SemesterList = semesterList.Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                })
            };
            return View(registrationFormVM);
        }
        public async Task<IActionResult> RegistrationFormTable(string userId, Guid semesterId)
        {
            userId = userId == "" ? User.FindFirstValue(ClaimTypes.NameIdentifier) : userId;
            var semesterObj = await _unitOfWork.Semester.FirstOrDefaultAsync(x => x.IsActive == true);
            semesterId = semesterId == Guid.Empty ? semesterObj.Id : semesterId;
            var semesterFeeObj = await _unitOfWork.StudentSemeterFee.FirstOrDefaultAsync(x => x.StudentId == userId && x.SemesterId == semesterId);
            var totolrPrice = 0;
            if(semesterFeeObj!=null)
            {
                totolrPrice = semesterFeeObj.TotalPrice;
            }
            RegistrationFormTableVM registrationFormVM = new RegistrationFormTableVM()
            {
                RegistrationCourse = await _unitOfWork.StudentRegisteationCourse.GetCourseBySemester(userId, semesterId),
                Credits=await _unitOfWork.StudentRegisteationCourse.GetSemesterCredits(userId,semesterId),
                TotalFee=totolrPrice,
            };
            return PartialView("_RegistrationFormTable", registrationFormVM);
        }
        #endregion
    }
}
