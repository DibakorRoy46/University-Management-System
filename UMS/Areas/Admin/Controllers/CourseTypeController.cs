using Microsoft.AspNetCore.Mvc;
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
    public class CourseTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CourseTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #region Index
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<IActionResult> CourseTypeTable(string searchValue, int pageNo, int pageSize)
        {
            pageNo = pageNo != 0 ? pageNo : 1;
            pageSize = 10;
            int numberOfCourseType = await _unitOfWork.CourseType.CountAsync(searchValue);
            CourseTypeVM courseTypeVM = new CourseTypeVM()
            {
                CourseTypeList = await _unitOfWork.CourseType.SearchAsync(searchValue, pageNo, pageSize),
                Search = searchValue,
                Pager = new Pager(numberOfCourseType, pageNo, pageSize)
            };
            return PartialView("_CourseTypeTable", courseTypeVM);

        }
        #endregion

        #region Upsert
        public async Task<IActionResult> Upsert(Guid id)
        {
            try
            {
                CourseType courseType = new CourseType();
                if (id.Equals(Guid.Empty))
                {
                    return View(courseType);
                }
                else
                {
                    courseType = await _unitOfWork.CourseType.FirstOrDefaultAsync(x=>x.Id.Equals(id));
                    if (courseType == null)
                    {
                        return NotFound();
                    }
                    return View(courseType);
                }
            }
            catch (Exception ex)
            {
                return NotFound();
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(CourseType courseType)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (courseType.Id.Equals(Guid.Empty))
                    {
                        await _unitOfWork.CourseType.AddAsync(courseType);
                        TempData["message"] = "Successfully Created";
                    }
                    else
                    {
                        await _unitOfWork.CourseType.UpdateAsync(courseType);
                        TempData["message"] = "Successfully Updated";
                    }
                    await _unitOfWork.SaveAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View();
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        #endregion

        #region Delete
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id, int pageNo, int pageSize)
        {
            try
            {
                CourseType courseTypeObj = await _unitOfWork.CourseType.FirstOrDefaultAsync(x=>x.Id.Equals(id));
                if (courseTypeObj == null)
                {
                    return NotFound();
                }
                await _unitOfWork.CourseType.RemoveAsync(courseTypeObj);
                await _unitOfWork.SaveAsync();
                pageNo = pageNo != 0 ? pageNo : 1;
                pageSize = 10;
                int numberOfCourseType = await _unitOfWork.CourseType.CountAsync(null);
                CourseTypeVM courseTypeVM = new CourseTypeVM()
                {
                    CourseTypeList = await _unitOfWork.CourseType.SearchAsync(null, pageNo, pageSize),
                    Search = null,
                    Pager = new Pager(numberOfCourseType, pageNo, pageSize)
                };
                return PartialView("_CourseTypeTable", courseTypeVM);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        #endregion

        #region Exists
        public async Task<IActionResult> ExistName(string name)
        {
            try
            {
                var exist = await _unitOfWork.CourseType.Exists(x => x.Name.ToLower().Equals(name.ToLower()));
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
