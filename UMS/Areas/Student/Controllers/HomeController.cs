using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using UMS.Data.IRepository;
using UMS.Data.Repository;
using UMS.Models;
using UMS.Models.ViewModels;

namespace UMS.Controllers
{
    [Area("Student")]
    [Authorize]

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;


        public HomeController(ILogger<HomeController> logger,IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var semesterObj = await _unitOfWork.Semester.FirstOrDefaultAsync(x => x.IsActive == true);
            if (User.IsInRole("Student"))
            {
                var preCourseList  = await _unitOfWork.PreRegistationCourses.
                    GetPreCourses(userId, semesterObj.Id, DateTime.Now.Year);
                var regisCourseList = await _unitOfWork.RegistrationCourse.
                   GetRegisteredCourses(userId, semesterObj.Id, DateTime.Now.Year);
                DashboardVM studentVM = new DashboardVM()
                {
                    RegistrationCourseList = regisCourseList.Count(),
                    PreregistrationCourseList = preCourseList.Count()                   
                };
                return View(studentVM);
            }
            if(User.IsInRole("Faculty"))
            {
                var courseList = await _unitOfWork.AssignRegistrationCourse.GetAllAsync(x => x.TeacherId == userId);
                DashboardVM facultyVM = new DashboardVM()
                {
                    AssignCourseList = courseList.Count()
                };
                return View(facultyVM);
            }
            if(User.IsInRole("Admin") || User.IsInRole("Super Admin"))
            {
                var studentList = await _unitOfWork.AdminDashboard.StudentList();
                var teacherList = await _unitOfWork.AdminDashboard.TeacherList();
                var employeeList = await _unitOfWork.AdminDashboard.EmployeeList();
                var departmentList = await _unitOfWork.Department.GetAllAsync();
                var roleList = await _unitOfWork.AdminDashboard.RoleList();
                DashboardVM adminVM = new DashboardVM()
                {
                    Student = studentList.Count(),
                    Teacher = teacherList.Count(),
                    Employee = employeeList.Count(),
                    Department = departmentList.Count(),
                    Role = roleList.Count(),
                };
                return View(adminVM);
            }
        
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
