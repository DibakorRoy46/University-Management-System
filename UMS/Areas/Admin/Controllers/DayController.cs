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
    public class DayController : Controller
    {
        private readonly IUnitOfWork _unitOfWrok;
        public DayController(IUnitOfWork unitOfWork)
        {
            _unitOfWrok = unitOfWork;
        }
        #region Index
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<IActionResult>DayTable(string searchValue,int pageNo,int pageSize)
        {
            pageNo = pageNo != 0 ? pageNo : 1;
            pageSize = 10;
            var numberOfDay = await _unitOfWrok.Day.CountAsync(searchValue);
            DayVM dayVM = new DayVM()
            {
                DayList = await _unitOfWrok.Day.SearchAsync(searchValue, pageNo, pageSize),
                Search = searchValue,
                Pager = new Pager(numberOfDay, pageNo, pageSize)
            };
            return PartialView("_DayTable", dayVM);
        }
        #endregion
        #region Upsert
        public async Task<IActionResult>Upsert(Guid id)
        {
            try
            {
                Day day = new Day();
                if(id.Equals(Guid.Empty))
                {
                    return View(day);
                }
                else
                {
                    day = await _unitOfWrok.Day.FirstOrDefaultAsync(x => x.Id.Equals(id));
                    if(day==null)
                    {
                        return NotFound();
                    }
                    return View(day);
                }
            }
            catch(Exception ex)
            {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult>Upsert(Day day)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    if(day.Id.Equals(Guid.Empty))
                    {
                        await _unitOfWrok.Day.AddAsync(day);
                        TempData["message"] = "Successfully Created";
                    }
                    else
                    {
                        await _unitOfWrok.Day.UpdateAsync(day);
                        TempData["message"] = "Successfully Updated";
                    }
                    await _unitOfWrok.SaveAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(day);

            }
            catch(Exception ex)
            {
                return NotFound();
            }
        }
        #endregion
        #region Delete
        [HttpPost]
        public async Task<IActionResult>Delete(Guid id)
        {
            try
            {
                if(!id.Equals(Guid.Empty))
                {
                    var dayObj = await _unitOfWrok.Day.FirstOrDefaultAsync(x => x.Id.Equals(id));
                    if(dayObj==null)
                    {
                        return NotFound();
                    }
                    await _unitOfWrok.Day.RemoveAsync(dayObj);
                    await _unitOfWrok.SaveAsync();
                    int  pageNo = 1;
                    int  pageSize = 10;
                    var numberOfDay = await _unitOfWrok.Day.CountAsync(null);
                    DayVM dayVM = new DayVM()
                    {
                        DayList = await _unitOfWrok.Day.SearchAsync(null, pageNo, pageSize),
                        Search = null,
                        Pager = new Pager(numberOfDay, pageNo, pageSize)
                    };
                    return PartialView("_DayTable", dayVM);

                }
                return NotFound();
            }
            catch(Exception ex)
            {
                return NotFound();
            }
        }
        #endregion
        #region Exists
        public async Task<IActionResult> ExistsName(string name)
        {
            try
            {
                if(!String.IsNullOrEmpty(name))
                {
                    var exist = await _unitOfWrok.Day.Exists(x => x.Name.ToLower().Equals(name.ToLower()));
                    if (exist == true)
                    {
                        return Json(new { success = false });
                    }
                    return Json(new { success = true });
                }
                return Json(new { success = false });
            }
                
            catch(Exception ex)
            {
                return NotFound();
            }
        }
        #endregion
    }
}
