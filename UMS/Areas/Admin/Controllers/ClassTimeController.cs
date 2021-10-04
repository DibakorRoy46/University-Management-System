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
    [Authorize(Roles ="Admin,Super Admin")]
    public class ClassTimeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ClassTimeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #region Index
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<IActionResult> ClassTimeTable(DateTime searchValue, int pageNo, int pageSize)
        {
            pageNo = pageNo != 0 ? pageNo : 1;
            pageSize = 10;
            var numberOfClassTime = await _unitOfWork.ClassTime.CountAsync(searchValue);
            ClassTImeVM classTimeVM = new ClassTImeVM()
            {
                ClassTimeList = await _unitOfWork.ClassTime.SearchAsync(searchValue, pageNo, pageSize),
                Search = searchValue,
                Pager = new Pager(numberOfClassTime, pageNo, pageSize)
            };
            return PartialView("_ClassTimeTable", classTimeVM);
        }
        #endregion

        #region Upsert
        public async Task<IActionResult>Upsert(Guid id)
        {
            try
            {
                ClassTime classTime = new ClassTime();
                if(id.Equals(Guid.Empty))
                {
                    return View(classTime);
                }
                else
                {
                    classTime = await _unitOfWork.ClassTime.FirstOrDefaultAsync(x => x.Id.Equals(id));
                    if(classTime ==null)
                    {
                        return NotFound();
                    }
                    return View(classTime);
                }
            }
            catch(Exception ex)
            {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult>Upsert(ClassTime classTime)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    if(classTime.Id.Equals(Guid.Empty))
                    {
                        await _unitOfWork.ClassTime.AddAsync(classTime);
                        TempData["message"] = "Successfully Created";
                    }
                    else
                    {
                        await _unitOfWork.ClassTime.UpdateAsync(classTime);
                        TempData["message"] = "Successfully Updated";
                    }
                    await _unitOfWork.SaveAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View();
            }
            catch(Exception ex)
            {
                return NotFound();
            }
        }

        #endregion
        #region Delete
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                if (!id.Equals(Guid.Empty))
                {
                    var classTimeObj = await _unitOfWork.ClassTime.FirstOrDefaultAsync(x => x.Id.Equals(id));
                    if (classTimeObj == null)
                    {
                        return NotFound();
                    }
                    await _unitOfWork.ClassTime.RemoveAsync(classTimeObj);
                    await _unitOfWork.SaveAsync();
                    int pageNo = 1;
                    int pageSize = 10;
                    var numberOfClassTime = await _unitOfWork.ClassTime.CountAsync(DateTime.MinValue);
                    ClassTImeVM classTime = new ClassTImeVM()
                    {
                        ClassTimeList = await _unitOfWork.ClassTime.SearchAsync(DateTime.MinValue, pageNo, pageSize),
                        Search = DateTime.MinValue,
                        Pager = new Pager(numberOfClassTime, pageNo, pageSize)
                    };
                    return PartialView("_ClassTimeTable", classTime);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        #endregion

        #region Exists
        public async Task<IActionResult> ExistsTime(DateTime time)
        {
            try
            {
                if (time!=DateTime.MinValue)
                {                   
                    var exist = await _unitOfWork.ClassTime.Exists(x => x.Time.TimeOfDay == time.TimeOfDay);
                    if (exist ==true)
                    {
                        return Json(new { success = false });
                    }
                    return Json(new { success = true });
                }

                return Json(new { success = false });
            }
            catch (Exception ex)
            {
                return NotFound();
            }

        }
        #endregion

    }
}
