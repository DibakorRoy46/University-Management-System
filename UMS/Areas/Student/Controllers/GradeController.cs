using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using UMS.Data.IRepository;
using UMS.Models.ViewModels;

namespace UMS.Areas.Student.Controllers
{
    [Area("Student")]
    [Authorize(Roles ="Student")]
    public class GradeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public GradeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public  async Task< IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            GradeVM gradeVM = new GradeVM()
            {
                courseList = await _unitOfWork.StudentRegisteationCourse.GetAllCourses(userId),
                UserId = userId,
                CreditAttempted=await _unitOfWork.StudentRegisteationCourse.CreditAtempeted(userId),
                CreditCompletd=await _unitOfWork.StudentRegisteationCourse.CreditCompleted(userId),
                SemesterList=await _unitOfWork.StudentRegisteationCourse.GetSemesterList(userId)               
            };
            var semsterList = await _unitOfWork.Semester.GetStudentRegisterSemester(userId);
                     
            foreach (var semeter in semsterList)
            {
                var courseListBysemester = await _unitOfWork.StudentRegisteationCourse.GetCourseBySemester(userId, semeter.Id);
                gradeVM.CourseCount.Add(courseListBysemester);
            }
          
            return View(gradeVM) ;
        }
    }
}
