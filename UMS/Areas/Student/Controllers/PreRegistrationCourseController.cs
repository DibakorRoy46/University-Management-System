using Microsoft.AspNetCore.Mvc;
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
            return View();
        }
        public async Task<IActionResult>CourseTable(string searchValue)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _unitOfWork.ApplicationUser.FirstOrDefaultAsync(x => x.Id == userId, includeProperties: "UserDetails");
            var userDetailsObj = await _unitOfWork.UserDetials.FirstOrDefaultAsync(x => x.UserId == userId);
            if (userDetailsObj != null)
            {
                              
                PreRegistrationVM preRegistrationVM = new PreRegistrationVM()
                {
                    User = user,
                    CourseList = await _unitOfWork.PreRegistationCourses.GetAllCourses(searchValue, userDetailsObj.DepartmentId),
                };
                var courseIdList = await _unitOfWork.PreRegistationCourses.SelectPreCourseId(userId);
                foreach (var course in preRegistrationVM.CourseList)
                {
                    if(courseIdList.Contains(course.Id))
                    {
                        course.IsTaken = true;
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
                return PartialView("_CourseTable", preRegistrationVM);
            }
        }
        public async Task<IActionResult>SelectPreCourseTable(string userId)
        {
            userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            PreCourseVM preCourseVM = new PreCourseVM()
            {
                CourseList = await _unitOfWork.PreRegistationCourses.GetPreCourses(userId),
                StudentId = userId
            };

            return PartialView("_SelectPreCourseTable",preCourseVM);
        }
        [HttpPost]
        public async Task<IActionResult>SelectPreregistationCourse(string userId,Guid courseId)
        {
            try
            {
                if(!String.IsNullOrEmpty(userId)&&courseId!=Guid.Empty)
                {
                    AssignPreRegistrationCourse preCourse = new AssignPreRegistrationCourse();
                    preCourse.CourseId = courseId;
                    preCourse.StudentId = userId;
                    await _unitOfWork.PreRegistationCourses.AddAsync(preCourse);
                    await _unitOfWork.SaveAsync();
                    PreCourseVM preCourseVM = new PreCourseVM()
                    {
                        CourseList = await _unitOfWork.PreRegistationCourses.GetPreCourses(userId),
                        StudentId = userId
                    };
                    return PartialView("_SelectPreCourseTable", preCourseVM);
                }
                return BadRequest();
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public async Task<IActionResult> DeletePreregistationCourse(string userId,Guid courseId)
        {
            try
            {
                if(!String.IsNullOrEmpty(userId)&&courseId!=Guid.Empty)
                {
                    var preCourseObj = await _unitOfWork.PreRegistationCourses.
                        FirstOrDefaultAsync(x => x.CourseId == courseId && x.StudentId == userId);
                    if(preCourseObj==null)
                    {
                        return NotFound();
                    }
                    await _unitOfWork.PreRegistationCourses.RemoveAsync(preCourseObj);
                    await _unitOfWork.SaveAsync();
                    PreCourseVM preCourseVM = new PreCourseVM()
                    {
                        CourseList = await _unitOfWork.PreRegistationCourses.GetPreCourses(userId),
                        StudentId = userId
                    };
                    return PartialView("_SelectPreCourseTable", preCourseVM);
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
