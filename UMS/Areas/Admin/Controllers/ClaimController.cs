using IdentityServer4.EntityFramework.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
    [Authorize]
    public class ClaimController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public ClaimController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _unitOfWork = unitOfWork;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        #region Index
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<IActionResult> ClaimTable(string searchValue, int pageNo, int pageSize)
        {
            pageNo = pageNo != 0 ? pageNo : 1;
            pageSize = 10;
            var numberOfClaims = await _unitOfWork.Claims.CountAsync(searchValue);
            ClaimVM claimVM = new ClaimVM()
            {
                ClaimList = await _unitOfWork.Claims.SearchAsync(searchValue, pageNo, pageSize),
                Search = searchValue,
                Pager = new Pager(numberOfClaims, pageNo, pageSize)
            };
            return PartialView("_ClaimTable", claimVM);
        }
        #endregion

        #region Upsert
        public async Task<IActionResult> Upsert(Guid id)
        {
            try
            {
                Claims claims = new Claims();
                if (id.Equals(Guid.Empty))
                {
                    return View(claims);
                }
                else
                {
                    claims = await _unitOfWork.Claims.FirstOrDefaultAsync(x => x.Id.Equals(id));
                    if (claims == null)
                    {
                        return NotFound();
                    }
                    return View(claims);
                }
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Upsert(Claims claims)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (claims.Id.Equals(Guid.Empty))
                    {
                        await _unitOfWork.Claims.AddAsync(claims);
                        TempData["message"] = "Successfully Created";
                    }
                    else
                    {
                        await _unitOfWork.Claims.UpdateAsync(claims);
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
        public async Task<IActionResult> Delete(Guid id,string searchValue,int pageNo)
        {
            try
            {
                if (!id.Equals(Guid.Empty))
                {
                    var claimsObj = await _unitOfWork.Claims.FirstOrDefaultAsync(x => x.Id.Equals(id));
                    if (claimsObj == null)
                    {
                        return NotFound();
                    }
                    await _unitOfWork.Claims.RemoveAsync(claimsObj);
                    await _unitOfWork.SaveAsync();
                    pageNo = pageNo != 0 ? pageNo : 1;
                    int pageSize = 10;
                    var numberOfClaims = await _unitOfWork.Claims.CountAsync(searchValue);
                    ClaimVM claimVM = new ClaimVM()
                    {
                        ClaimList = await _unitOfWork.Claims.SearchAsync(searchValue, pageNo, pageSize),
                        Search = searchValue,
                        Pager = new Pager(numberOfClaims, pageNo, pageSize)
                    };
                    return PartialView("_ClaimTable", claimVM);
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
        public async Task<IActionResult> ExistsClaimType(string claimType)
        {
            try
            {
                if (!String.IsNullOrEmpty(claimType))
                {
                    var exist = await _unitOfWork.Claims.Exists(x => x.ClaimType.ToLower().Equals(claimType.ToLower()));
                    if (exist == true)
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

        public async Task<IActionResult> ExistsClaimValue(string claimValue)
        {
            try
            {
                if (!String.IsNullOrEmpty(claimValue))
                {
                    var exist = await _unitOfWork.Claims.Exists(x => x.ClaimValue.ToLower().Equals(claimValue.ToLower()));
                    if (exist == true)
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
