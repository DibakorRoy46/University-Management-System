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
    [Authorize(Roles ="Admin")]

    public class ActivityController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ActivityController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #region Index
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<IActionResult>ActivityTable(string searchValue,int pageNo,int pageSize)
        {
            pageNo = pageNo != 0 ? pageNo : 1;
            pageSize = 1;
            var numberOfActivity = await _unitOfWork.Activity.CountAsync(searchValue);
            ActivityVM activityVM = new ActivityVM()
            {
                ActivityList = await _unitOfWork.Activity.SearchAsync(searchValue, pageNo, pageSize),
                Search = searchValue,
                Pager = new Pager(numberOfActivity, pageNo, pageSize)
            };
            return PartialView("_ActivityTable", activityVM);

        }
        #endregion
        #region Upsert
        public async Task<IActionResult>Upsert(Guid id)
        {
            try
            {
                Activity activity = new Activity();
                if(id.Equals(Guid.Empty))
                {
                    return View(activity);
                }
                else
                {
                    activity = await _unitOfWork.Activity.FirstOrDefaultAsync(x => x.Id.Equals(id));
                    if(activity==null)
                    {
                        return NotFound();
                    }
                    return View(activity);
                }
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Upsert(Activity activity)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    if(activity.Id.Equals(Guid.Empty))
                    {
                        await _unitOfWork.Activity.AddAsync(activity);
                        TempData["message"] = "Successfully Created";
                    }
                    else
                    {
                        await _unitOfWork.Activity.UpdateAsync(activity);
                        TempData["message"] = "Successfully Updated";
                    }
                    await _unitOfWork.SaveAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(activity);
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }

        #endregion
        #region Delete
        [HttpPost]
        public async Task<IActionResult>Delete(Guid id)
        {
            try
            {
                var activityObj = await _unitOfWork.Activity.FirstOrDefaultAsync(x => x.Id.Equals(id));
                if (activityObj == null)
                {
                    return NotFound();
                }
                await _unitOfWork.Activity.RemoveAsync(activityObj);
                await _unitOfWork.SaveAsync();
                int  pageNo =  1;
                int  pageSize = 1;
                var numberOfActivity = await _unitOfWork.Activity.CountAsync(null);
                ActivityVM activityVM = new ActivityVM()
                {
                    ActivityList = await _unitOfWork.Activity.SearchAsync(null, pageNo, pageSize),
                    Search = null,
                    Pager = new Pager(numberOfActivity, pageNo, pageSize)
                };
                return PartialView("_ActivityTable", activityVM);


            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        #endregion
        #region Exists
        public async Task<IActionResult>ExistName(string name)
        {
            if(!String.IsNullOrEmpty(name))
            {
                var exist = await _unitOfWork.Activity.Exists(x => x.Name.ToLower().Equals(name.ToLower()));
                if(exist==true)
                {
                    return Json(new { success = false });
                }
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        #endregion

        #region Activity
        [HttpPost]
        public async Task<IActionResult>Activity(Guid id,int pageNo,string searchValue )
        {
            try
            {
                if(!id.Equals(Guid.Empty))
                {
                    var activityObj = await _unitOfWork.Activity.FirstOrDefaultAsync(x => x.Id.Equals(id));
                    if(activityObj.IsActive==true)
                    {
                        activityObj.IsActive = false;
                    }
                    else
                    {
                        activityObj.IsActive = true;
                    }
                    await _unitOfWork.SaveAsync();
                    int pageSize = 1;
                    var numberOfActivity = await _unitOfWork.Activity.CountAsync(searchValue);
                    ActivityVM activityVM = new ActivityVM()
                    {
                        ActivityList = await _unitOfWork.Activity.SearchAsync(searchValue, pageNo, pageSize),
                        Search = searchValue,
                        Pager = new Pager(numberOfActivity, pageNo, pageSize)
                    };
                    return PartialView("_ActivityTable", activityVM);
                }
                return NotFound();

            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }
        #endregion
    }

}
