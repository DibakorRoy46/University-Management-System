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
    public class CoursePreController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        
        public CoursePreController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #region INDEX
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult>CoursePreTable(string searchValue,int pageNo,int pageSize)
        {
            pageNo = pageNo != 0 ? pageNo : 1;
            pageSize = 10;
            var numberOfCourePre = await _unitOfWork.CoursePrerequisite.CountAsync(searchValue);
            CoursePreVM coursePreVM = new CoursePreVM()
            {
                CoursePreList=await _unitOfWork.CoursePrerequisite.SearchAsync(searchValue,pageNo,pageSize),
                Search=searchValue,
                Pager=new Pager(numberOfCourePre,pageNo,pageSize)
            };
            return PartialView("_CoursePreTable", coursePreVM);
        }
        #endregion
        #region Delete
        [HttpPost]
        public async Task<IActionResult>Delete(Guid id)
        {
            try
            {
                var coursePreObj = await _unitOfWork.CoursePrerequisite.FirstOrDefaultAsync(x => x.Id == id);
                var courseObj = await _unitOfWork.Course.FirstOrDefaultAsync(x => x.Id == id);
                if(courseObj==null&&coursePreObj==null)
                {
                    return NotFound();
                }
                await _unitOfWork.Course.RemoveAsync(courseObj);
                await _unitOfWork.CoursePrerequisite.RemoveAsync(coursePreObj);
                await _unitOfWork.SaveAsync();
                int pageNo = 1;
                int pageSize = 10;
                var searchValue = String.Empty;
                var numberOfCourePre = await _unitOfWork.CoursePrerequisite.CountAsync(searchValue);
                CoursePreVM coursePreVM = new CoursePreVM()
                {
                    CoursePreList = await _unitOfWork.CoursePrerequisite.SearchAsync(searchValue, pageNo, pageSize),
                    Search = searchValue,
                    Pager = new Pager(numberOfCourePre, pageNo, pageSize)
                };
                return PartialView("_CoursePreTable", coursePreVM);

            }
            catch(Exception ex)
            {
                return NotFound();
            }
        }
        #endregion

        #region CASCADE
        public async Task<IEnumerable<Course>> GetCourse(Guid id,Guid courseId)
        {
            var courseList = await _unitOfWork.Course.GetCourseByDepartment(id,courseId);
            return courseList;
        }
        #endregion

    }
}
