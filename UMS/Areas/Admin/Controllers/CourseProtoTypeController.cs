using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles ="Admin, Super Admin")]
    public class CourseProtoTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CourseProtoTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #region Index
        [Route("CoursePrototype")]
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<IActionResult> CourseProtoTypeTable(string searchValue, int pageNo, int pageSize)
        {
            pageNo = pageNo != 0 ? pageNo : 1;
            pageSize = 10;
            int numberOfCourseProtoType = await _unitOfWork.CourseProtoType.CountAsync(searchValue);
            CourseProtoTypeVM courseProtoTypeVM = new CourseProtoTypeVM()
            {
                CourseProtoTypeList = await _unitOfWork.CourseProtoType.SearchAsync(searchValue, pageNo, pageSize),
                Search = searchValue,
                Pager = new Pager(numberOfCourseProtoType, pageNo, pageSize)
            };
            return PartialView("_CourseProtoTypeTable", courseProtoTypeVM);

        }
        #endregion

        #region Upsert
        [Route("CoursePrototype/Upsert")]
        public async Task<IActionResult> Upsert(Guid id)
        {
            try
            {
                CourseProtoType courseProtoType = new CourseProtoType();
                if (id.Equals(Guid.Empty))
                {
                    return View(courseProtoType);
                }
                else
                {
                    courseProtoType = await _unitOfWork.CourseProtoType.FirstOrDefaultAsync(x=>x.Id.Equals(id));
                    if (courseProtoType == null)
                    {
                        return NotFound();
                    }
                    return View(courseProtoType);
                }
            }
            catch (Exception ex)
            {
                return NotFound();
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("CoursePrototype/Upsert")]
        public async Task<IActionResult> Upsert(CourseProtoType courseProtoType)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!courseProtoType.Id.Equals(Guid.Empty))
                    {
                        await _unitOfWork.CourseProtoType.UpdateAsync(courseProtoType);
                        TempData["message"] = "Successfully Updated";
                    }
                    else
                    {
                        await _unitOfWork.CourseProtoType.AddAsync(courseProtoType);
                        TempData["message"] = "Successfully Created";
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
                CourseProtoType courseProtoTypeObj = await _unitOfWork.CourseProtoType.FirstOrDefaultAsync(x => x.Id.Equals(id));
                if (courseProtoTypeObj == null)
                {
                    return NotFound();
                }
                await _unitOfWork.CourseProtoType.RemoveAsync(courseProtoTypeObj);
                await _unitOfWork.SaveAsync();
                pageNo = pageNo != 0 ? pageNo : 1;
                pageSize = 10;
                int numberOfCourseProtoType = await _unitOfWork.CourseProtoType.CountAsync(null);
                CourseProtoTypeVM courseProtoTypeVM = new CourseProtoTypeVM()
                {
                    CourseProtoTypeList = await _unitOfWork.CourseProtoType.SearchAsync(null, pageNo, pageSize),
                    Search = null,
                    Pager = new Pager(numberOfCourseProtoType, pageNo, pageSize)
                };
                return PartialView("_CourseProtoTypeTable", courseProtoTypeVM);
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
                var exist = await _unitOfWork.CourseProtoType.Exists(x => x.Name.ToLower().Equals(name.ToLower()));
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
