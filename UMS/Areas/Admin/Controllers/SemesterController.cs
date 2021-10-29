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
    public class SemesterController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public SemesterController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #region Index
        [Route("Semester")]
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<IActionResult>SemesterTable(string searchValue,int pageNo,int pageSize)
        {
            pageNo = pageNo != 0 ? pageNo : 1;
            pageSize = 10;
            var numberOfSemester = await _unitOfWork.Semester.CountAsync(searchValue);
            SemesterVM semesterVM = new SemesterVM()
            {
                SemesterList = await _unitOfWork.Semester.SearchAsync(searchValue, pageNo, pageSize),
                Search = searchValue,
                Pager = new Pager(numberOfSemester, pageNo, pageSize)
            };
            return PartialView("_SemesterTable", semesterVM);
        }
        #endregion

        #region Upsert
        [Route("Semester/Upsert")]
        public async Task<IActionResult>Upsert(Guid id)
        {
            try
            {
                Semester semester = new Semester();
                if(id.Equals(Guid.Empty))
                {
                    return View(semester);
                }
                else
                {
                    semester = await _unitOfWork.Semester.FirstOrDefaultAsync(x => x.Id.Equals(id));
                    if(semester==null)
                    {
                        return NotFound();
                    }
                    return View(semester);
                }
            }
            catch(Exception ex)
            {
                return NotFound();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Semester/Upsert")]
        public async Task<IActionResult>Upsert(Semester semester)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    if(semester.Id.Equals(Guid.Empty))
                    {
                        await _unitOfWork.Semester.AddAsync(semester);
                        TempData["message"] = "Successfully Created";
                    }
                    else
                    {
                        await _unitOfWork.Semester.UpdateAsync(semester);
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
        public async Task<IActionResult>Delete(Guid id)
        {
            try
            {
                if(!id.Equals(Guid.Empty))
                {
                    var semesterObj = await _unitOfWork.Semester.FirstOrDefaultAsync(x => x.Id.Equals(id));
                    if(semesterObj==null)
                    {
                        return NotFound();
                    }
                    if(semesterObj.IsActive==false)
                    {
                        await _unitOfWork.Semester.RemoveAsync(semesterObj);
                        await _unitOfWork.SaveAsync();
                        int pageNo = 1;
                        int pageSize = 10;
                        var numberOfSemester = await _unitOfWork.Semester.CountAsync(null);
                        SemesterVM semesterVM = new SemesterVM()
                        {
                            SemesterList = await _unitOfWork.Semester.SearchAsync(null, pageNo, pageSize),
                            Search = null,
                            Pager = new Pager(numberOfSemester, pageNo, pageSize)
                        };
                        return PartialView("_SemesterTable", semesterVM);
                    }
                    return NotFound();

                    
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
        public async Task<IActionResult>ExistsName(string name)
        {
            try
            {
                if(!String.IsNullOrEmpty(name))
                {
                    var exist = await _unitOfWork.Semester.Exists(x => x.Name.ToLower().Equals(name.ToLower()));
                    if(exist==true)
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
        public async Task<IActionResult>IsActive()
        {
            var isActive = await _unitOfWork.Semester.IsActiveExist();
            if(isActive==true)
            {
                return Json( new { success=true} );
            }
            return Json(new { success = false });
        }
        #endregion

    }
}
