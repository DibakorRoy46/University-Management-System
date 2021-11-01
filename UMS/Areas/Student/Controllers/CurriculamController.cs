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
    public class CurriculamController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CurriculamController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [Route("CurriculamAnalysis")]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userDetails= await _unitOfWork.UserDetials.FirstOrDefaultAsync(x => x.UserId == userId);
            var batchObj= await _unitOfWork.Semester.FirstOrDefaultAsync(x => x.Id == userDetails.SemesterId);
            var userObj = await _unitOfWork.ApplicationUser.FirstOrDefaultAsync(x => x.Id == userId);
            CurriculamAnalysisVM curriculamAnalysisVM = new CurriculamAnalysisVM()
            {
                UserId = userId,
                CreditCompletd = await _unitOfWork.StudentRegisteationCourse.CreditCompleted(userId),
                CompletedCGPA = await _unitOfWork.StudentRegisteationCourse.GetCompletedCGPA(userId),
                Department = await _unitOfWork.Department.FirstOrDefaultAsync(x => x.Id == userDetails.DepartmentId),
                Batch = batchObj.Batch,
                AdmissionTIme=DateTime.Now-userObj.RegisterDate
            };
            var completedCourseIdList = await _unitOfWork.StudentRegisteationCourse.GetCompleteCourseId(userId);
            var courseList = await _unitOfWork.Course.SearchAsync(null, userDetails.DepartmentId,0,0);
            foreach (var course in courseList)
            {
                if(completedCourseIdList.Contains(course.Id))
                {
                    var gradeObj = await _unitOfWork.StudentRegisteationCourse.GetCourseGrade(userId, course.Id);
                    course.Grade = gradeObj.Grade;
                    course.IsTaken = true;
                }
            }
            curriculamAnalysisVM.courseList = courseList;
            return View(curriculamAnalysisVM);
            
        }
    }
}
