using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using UMS.Data.IRepository;
using UMS.Models.Models;
using UMS.Models.ViewModels;

namespace UMS.Areas.Teacher.Controllers
{
    [Area("Teacher")]
    [Authorize(Roles ="Faculty")]
    public class TeacherCourseController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public TeacherCourseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        [Route("AdviseCourse")]
        public async Task<IActionResult>CourseList()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var semesterObj = await _unitOfWork.Semester.FirstOrDefaultAsync(x => x.IsActive == true);
            FacultyCourseVM facultyCourseVM = new FacultyCourseVM()
            {
                CourseList = await _unitOfWork.AssignRegistrationCourse.GetAllAsync(x => x.TeacherId == userId && x.SemesterId==semesterObj.Id,
                                includeProperties:"Courses,Section")
            };
            return View(facultyCourseVM);
        }
        [Route("PreviousAdviseCourse")]
        public async Task<IActionResult> PreviousCourseList()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var semesterObj = await _unitOfWork.Semester.FirstOrDefaultAsync(x => x.IsActive == true);
            FacultyCourseVM facultyCourseVM = new FacultyCourseVM()
            {
                PreviousCourseList = await _unitOfWork.AssignRegistrationCourse.GetAllAsync(x => x.TeacherId == userId && x.SemesterId!=semesterObj.Id,
                                includeProperties: "Courses,Section")
            };
            return View(facultyCourseVM);
        }
        [Route("AdviseCourse/StudentList")]
        public async Task<IActionResult>StudentList(Guid Id)
        {
            ViewBag.CouseId = Id;
            return View();
        }
        public async Task<IActionResult>StudentListPartial(string searchValue, Guid courseId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (courseId != Guid.Empty)
            {
                StudentListofCourseVM studentList = new StudentListofCourseVM()
                {
                    StudentList = await _unitOfWork.TeacherCourse.StudentList(searchValue,userId, courseId)
                };
                return PartialView("_StudentListPartial", studentList);
            }
            return NotFound();
        }
        [Route("PreviousAdviseCourse/StudentList")]
        public async Task<IActionResult> PreviousStudentList(Guid Id)
        {
            ViewBag.CouseId = Id;
            return View();
        }
        public async Task<IActionResult> PreviousStudentListPartial(string searchValue, Guid courseId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (courseId != Guid.Empty)
            {
                StudentListofCourseVM studentList = new StudentListofCourseVM()
                {
                    StudentList = await _unitOfWork.TeacherCourse.StudentList(searchValue, userId, courseId)
                };
                return PartialView("_PreviousStudentListPartial", studentList);
            }
            return NotFound();
        }

      
        public async Task<IActionResult> Detials(string studentId, Guid courseId)
        {
            var studentCourseObj = await _unitOfWork.RegistrationCourse.FirstOrDefaultAsync(x => x.AssignRegiCourseId == courseId &&
                                      x.StudentId == studentId, includeProperties: "AssignRegistrationCourse,ApplicationUser");
            return View(studentCourseObj);
            
        }
        [Authorize(Policy = "PriviousCourseEdit")]
        
        public async Task<IActionResult> PreviousDetials(string studentId, Guid courseId)
        {
            var studentCourseObj = await _unitOfWork.RegistrationCourse.FirstOrDefaultAsync(x => x.AssignRegiCourseId == courseId &&
                                      x.StudentId == studentId, includeProperties: "AssignRegistrationCourse,ApplicationUser");
            return View(studentCourseObj);

        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Detials(StudentRegisteationCourse studentRegisteationCourse)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    if(studentRegisteationCourse.AttendanceMark!=0&& studentRegisteationCourse.AssignmentMark !=0 &&
                        studentRegisteationCourse.MidTermMark!=0 && studentRegisteationCourse.FinalTermMark!=0)
                    {
                        var totalMark = studentRegisteationCourse.AttendanceMark + studentRegisteationCourse.AssignmentMark +
                            studentRegisteationCourse.MidTermMark + studentRegisteationCourse.FinalTermMark;
                        if(totalMark>=80)
                        {
                            studentRegisteationCourse.Grade = "A+";
                            studentRegisteationCourse.GPA = 4.00;
                        }
                        else if(totalMark<80&& totalMark>=75)
                        {
                            studentRegisteationCourse.Grade = "A";
                            studentRegisteationCourse.GPA = 3.75;
                        }
                        else if(totalMark<75 && totalMark>=70)
                        {
                            studentRegisteationCourse.Grade = "A-";
                            studentRegisteationCourse.GPA = 3.50;
                        }
                        else if (totalMark < 70 && totalMark >= 65)
                        {
                            studentRegisteationCourse.Grade = "B+";
                            studentRegisteationCourse.GPA = 3.25;
                        }
                        else if (totalMark < 65 && totalMark >= 60)
                        {
                            studentRegisteationCourse.Grade = "B";
                            studentRegisteationCourse.GPA = 3.00;
                        }
                        else if (totalMark < 60 && totalMark >= 55)
                        {
                            studentRegisteationCourse.Grade = "B-";
                            studentRegisteationCourse.GPA = 2.75;
                        }
                        else if (totalMark < 55 && totalMark >= 50)
                        {
                            studentRegisteationCourse.Grade = "C+";
                            studentRegisteationCourse.GPA = 2.50;
                        }
                        else if (totalMark < 50 && totalMark >= 45)
                        {
                            studentRegisteationCourse.Grade = "C";
                            studentRegisteationCourse.GPA = 2.25;
                        }
                        else if (totalMark < 45 && totalMark >= 40)
                        {
                            studentRegisteationCourse.Grade = "D";
                            studentRegisteationCourse.GPA = 2.00;
                        }
                        else
                        {
                            studentRegisteationCourse.Grade = "F";
                            studentRegisteationCourse.GPA = 0.00;
                        }
                    }
                    await _unitOfWork.StudentRegisteationCourse.UpdateAsync(studentRegisteationCourse);
                    await _unitOfWork.SaveAsync();
                    return RedirectToAction(nameof(StudentList),new {Id=studentRegisteationCourse.AssignRegiCourseId } );
                }
                return NotFound();
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }
        
        [Authorize(Policy = "PriviousCourseEdit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PreviousDetials(StudentRegisteationCourse studentRegisteationCourse)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (studentRegisteationCourse.AttendanceMark != 0 && studentRegisteationCourse.AssignmentMark != 0 &&
                        studentRegisteationCourse.MidTermMark != 0 && studentRegisteationCourse.FinalTermMark != 0)
                    {
                        var totalMark = studentRegisteationCourse.AttendanceMark + studentRegisteationCourse.AssignmentMark +
                            studentRegisteationCourse.MidTermMark + studentRegisteationCourse.FinalTermMark;
                        if (totalMark >= 80)
                        {
                            studentRegisteationCourse.Grade = "A+";
                            studentRegisteationCourse.GPA = 4.00;
                        }
                        else if (totalMark < 80 && totalMark >= 75)
                        {
                            studentRegisteationCourse.Grade = "A";
                            studentRegisteationCourse.GPA = 3.75;
                        }
                        else if (totalMark < 75 && totalMark >= 70)
                        {
                            studentRegisteationCourse.Grade = "A-";
                            studentRegisteationCourse.GPA = 3.50;
                        }
                        else if (totalMark < 70 && totalMark >= 65)
                        {
                            studentRegisteationCourse.Grade = "B+";
                            studentRegisteationCourse.GPA = 3.25;
                        }
                        else if (totalMark < 65 && totalMark >= 60)
                        {
                            studentRegisteationCourse.Grade = "B";
                            studentRegisteationCourse.GPA = 3.00;
                        }
                        else if (totalMark < 60 && totalMark >= 55)
                        {
                            studentRegisteationCourse.Grade = "B-";
                            studentRegisteationCourse.GPA = 2.75;
                        }
                        else if (totalMark < 55 && totalMark >= 50)
                        {
                            studentRegisteationCourse.Grade = "C+";
                            studentRegisteationCourse.GPA = 2.50;
                        }
                        else if (totalMark < 50 && totalMark >= 45)
                        {
                            studentRegisteationCourse.Grade = "C";
                            studentRegisteationCourse.GPA = 2.25;
                        }
                        else if (totalMark < 45 && totalMark >= 40)
                        {
                            studentRegisteationCourse.Grade = "D";
                            studentRegisteationCourse.GPA = 2.00;
                        }
                        else
                        {
                            studentRegisteationCourse.Grade = "F";
                            studentRegisteationCourse.GPA = 0.00;
                        }
                    }
                    await _unitOfWork.StudentRegisteationCourse.UpdateAsync(studentRegisteationCourse);
                    await _unitOfWork.SaveAsync();
                    return RedirectToAction(nameof(StudentList), new { Id = studentRegisteationCourse.AssignRegiCourseId });
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

    }
}
