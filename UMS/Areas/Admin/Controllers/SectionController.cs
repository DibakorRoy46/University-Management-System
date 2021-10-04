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
    [Authorize(Roles ="Admin ,Super Admin")]
    public class SectionController : Controller
    {
        private readonly IUnitOfWork _unitOfWrok;
        public SectionController(IUnitOfWork unitOfWork)
        {
            _unitOfWrok = unitOfWork;
        }
        #region Index
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<IActionResult> SectionTable(string searchValue,int pageNo,int pageSize)
        {
            pageNo = pageNo != 0 ? pageNo : 1;
            pageSize = 1;
            var numberOfSection = await _unitOfWrok.Section.CountAsync(searchValue);
            SectionVM sectionVM = new SectionVM()
            {
                SectionList = await _unitOfWrok.Section.SearchAsync(searchValue, pageNo, pageSize),
                Search = searchValue,
                Pager = new Pager(numberOfSection, pageNo, pageSize)
            };
            return PartialView("_SectionTable", sectionVM);
        }
        #endregion
        #region Upsert
        public async Task<IActionResult>Upsert(Guid id)
        {
            try
            {
                Section section = new Section();
                if(id.Equals(Guid.Empty))
                {
                    return View(section);
                }
                else
                {
                    section = await _unitOfWrok.Section.FirstOrDefaultAsync(x => x.Id.Equals(id));
                    if(section ==null)
                    {
                        return NotFound();
                    }
                    return View(section);
                }
            }
            catch(Exception ex)
            {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult>Upsert(Section section)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    if(section.Id.Equals(Guid.Empty))
                    {
                        await _unitOfWrok.Section.AddAsync(section);
                        TempData["message"] = "Successfully Created";
                    }
                    else
                    {
                        await _unitOfWrok.Section.UpdateAsync(section);
                        TempData["message"] = "Successfully Updated";
                    }
                    await _unitOfWrok.SaveAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(section);

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
                    var sectionObj = await _unitOfWrok.Section.FirstOrDefaultAsync(x => x.Id.Equals(id));
                    if(sectionObj==null)
                    {
                        return NotFound();
                    }
                    await _unitOfWrok.Section.RemoveAsync(sectionObj);
                    await _unitOfWrok.SaveAsync();
                    int  pageNo = 1;
                    int  pageSize = 10;
                    var numberOfSection = await _unitOfWrok.Section.CountAsync(null);
                    SectionVM sectionVM = new SectionVM()
                    {
                        SectionList = await _unitOfWrok.Section.SearchAsync(null, pageNo, pageSize),
                        Search = null,
                        Pager = new Pager(numberOfSection, pageNo, pageSize)
                    };
                    return PartialView("_SectionTable", sectionVM);

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
                    var exist = await _unitOfWrok.Section.Exists(x => x.Name.ToLower().Equals(name.ToLower()));
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
