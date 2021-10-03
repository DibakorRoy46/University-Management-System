using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using UMS.Data.Data;
using UMS.Data.IRepository;
using UMS.Models.Models;
using UMS.Models.ViewModels;
using UMS.Utility;

namespace UMS.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class UserController : Controller
    {
      
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _db;
      
        public UserController(
         ApplicationDbContext db,
         UserManager<IdentityUser> userManager,
            IUnitOfWork unitOfWork)
        {

            _db = db;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        #region Index
        public async Task<IActionResult> Index()
        {
            ViewBag.RoleList = await _db.Roles.ToListAsync();
            return View();
        }
        public async Task<IActionResult>UserTable(string searchValue,string roleId,int pageNo,int pageSize)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            pageNo = pageNo != 0 ? pageNo : 1;
            pageSize = 10;
            var numberOfUser = await _unitOfWork.User.CountAsync(searchValue,userId, roleId);
            UserVM userVM = new UserVM()
            {
                UserList = await _unitOfWork.User.SearchAsync(searchValue,userId, roleId, pageNo, pageSize),
                Search = searchValue,
                Role = roleId,
                Pager = new Pager(numberOfUser, pageNo, pageSize)
            };
            return PartialView("_UserTable", userVM);
        }
        #endregion

        #region Edit 
        [Route("User/Edit/")]
        public async Task<IActionResult> Edit(string Id)
        {
            try
            {
                ApplicationUser user = new ApplicationUser();
                if(!String.IsNullOrEmpty(Id))
                {
                    user = await _unitOfWork.User.FirstOrDefaultAsync(x => x.Id == Id);
                    if(user==null)
                    {
                        return NotFound();
                    }
                    return View(user);
                }
                return View(user);
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }
        [Route("User/Edit/")]
        [HttpPost]
        public async Task<IActionResult>Edit(ApplicationUser user)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    if(!String.IsNullOrEmpty(user.Id))
                    {
                        await _unitOfWork.User.UpdateAsync(user);
                        TempData["UserUpdate"] = "Successfully Updated";
                    }
                    await _unitOfWork.SaveAsync();
                    if(User.IsInRole("Admin"))
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ViewBag.Success = "Successfully Updated Profile";
                        return View(user);
                    }
                }
                return View(user);
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }

        #endregion

        #region Delete
        [HttpPost]
        public async Task<IActionResult> Delete(string id,string searchValue,string roleId,int pageNo)
        {
            try
            {
                if(!String.IsNullOrEmpty(id))
                {
                    var userObj = await _unitOfWork.User.FirstOrDefaultAsync(x => x.Id == id);
                    if(userObj==null)
                    {
                        return NotFound();
                    }
                    var userDetails = await _unitOfWork.UserDetials.FirstOrDefaultAsync(x => x.UserId == id);
                    if(userDetails!=null)
                    {
                        await _unitOfWork.UserDetials.RemoveAsync(userDetails);
                        await _unitOfWork.SaveAsync();
                    }
                    
                    await  _unitOfWork.User.RemoveAsync(userObj);
                    await _unitOfWork.SaveAsync();
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    pageNo = pageNo != 0 ? pageNo : 1;
                    int pageSize = 10;
                    var numberOfUser = await _unitOfWork.User.CountAsync(searchValue,userId, roleId);
                    UserVM userVM = new UserVM()
                    {
                        UserList = await _unitOfWork.User.SearchAsync(searchValue,userId, roleId, pageNo, pageSize),
                        Search = searchValue,
                        Role = roleId,
                        Pager = new Pager(numberOfUser, pageNo, pageSize)
                    };
                    return PartialView("_UserTable", userVM);

                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        #endregion

        #region LockUnLock

        public async Task<IActionResult>LockUnLock(string id,int pageNo,string searchValue,string roleId)
        {
            try
            {
                if(!String.IsNullOrEmpty(id))
                {
                    var user = await _unitOfWork.ApplicationUser.FirstOrDefaultAsync(x => x.Id == id);
                    if(user==null)
                    {
                        return NotFound();
                    }
                    if(user.LockoutEnd>DateTime.Now)
                    {
                        user.LockoutEnd = DateTime.Now;
                    }
                    else
                    {
                        user.LockoutEnd = DateTime.Now.AddYears(100);
                    }
                    await _unitOfWork.SaveAsync();
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    pageNo = pageNo != 0 ? pageNo : 1;
                    int pageSize = 10;
                    var numberOfUser = await _unitOfWork.User.CountAsync(searchValue,userId, roleId);
                    UserVM userVM = new UserVM()
                    {
                        UserList = await _unitOfWork.User.SearchAsync(searchValue,userId, roleId, pageNo, pageSize),
                        Search = searchValue,
                        Role = roleId,
                        Pager = new Pager(numberOfUser, pageNo, pageSize)
                    };
                    return PartialView("_UserTable", userVM);
                }
                else
                {
                    return NotFound();
                }
                

            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }
        #endregion

        #region AccessDenied
        [HttpGet]
        [Authorize]
        [Route("AccessDenied")]
        public IActionResult AccessDenied()
        {
            return View();
        }
        #endregion

        #region ManageRole
        public async Task<IActionResult> ManageRole(string id)
        {
            var roleList = await _db.Roles.ToListAsync();
            ManageRoleVM manageRole = new ManageRoleVM()
            {
                User = await _unitOfWork.User.FirstOrDefaultAsync(x => x.Id == id),
                UserRole = new IdentityUserRole<string>
                {
                    UserId = id
                }
            };
            var result = await(from userRole in _db.UserRoles
                               join role in _db.Roles on userRole.RoleId equals role.Id
                               join user in _db.ApplicationUsers on userRole.UserId equals user.Id
                               select new UserRole
                               {
                                   UserId = user.Id,
                                   UserName = user.UserName,
                                   RoleId = role.Id,
                                   RoleName = role.Name
                               }).Where(x => x.UserId == id).ToListAsync();
            manageRole.UserRoleList = result;
            List<string> assignRole = manageRole.UserRoleList.Select(x => x.RoleId).ToList();
            var tempRoleList = await _db.Roles.Where(x => !assignRole.Contains(x.Id)).ToListAsync();
            manageRole.RoleList = tempRoleList.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
          return View(manageRole);           
        }
        public async Task<IActionResult>ManageRolePartial(string id)
        {
            UserRolePartialVM userRolePartialVM = new UserRolePartialVM()
            {
                UserRoleList = await (from userRole in _db.UserRoles
                                      join role in _db.Roles on userRole.RoleId equals role.Id
                                      join user in _db.ApplicationUsers on userRole.UserId equals user.Id
                                      select new UserRole
                                      {
                                          UserId = user.Id,
                                          UserName = user.UserName,
                                          RoleId = role.Id,
                                          RoleName = role.Name
                                      }).Where(x => x.UserId == id).ToListAsync(),
                UserId = id
            };
            return PartialView("_ManageRolePartial",userRolePartialVM);
        }
        
        [HttpPost]
       
        public async Task<IActionResult> ManageRole(string userId,string roleId)
        {
            if(!String.IsNullOrEmpty(userId)&&!String.IsNullOrEmpty(roleId))
            {
                var user = await _db.ApplicationUsers.FirstOrDefaultAsync(x => x.Id == userId);             
                var roleName = await _db.Roles.FirstOrDefaultAsync(x => x.Id == roleId);
                var role = roleName.Name;
                var roleExists = await _db.UserRoles.Where(x=>x.UserId==userId).Select(x => x.RoleId).ToListAsync();
                if(roleExists.Contains(roleId))
                {
                    return BadRequest();
                }    
                await _userManager.AddToRoleAsync(user, role);
                await _db.SaveChangesAsync();

                UserRolePartialVM userRolePartialVM = new UserRolePartialVM()
                {
                    UserRoleList = await (from userRole in _db.UserRoles
                                          join roles in _db.Roles on userRole.RoleId equals roles.Id
                                          join users in _db.ApplicationUsers on userRole.UserId equals users.Id
                                          select new UserRole
                                          {
                                              UserId = users.Id,
                                              UserName = users.UserName,
                                              RoleId = roles.Id,
                                              RoleName = roles.Name
                                          }).Where(x => x.UserId == userId).ToListAsync(),
                    UserId = userId
                };
                return PartialView("_ManageRolePartial", userRolePartialVM);

            }
            else
            {
                return NotFound();
            }          
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole(string userId, string roleId)
        {
            var userRoleObj = await _db.UserRoles.FirstOrDefaultAsync(x => x.UserId == userId && x.RoleId == roleId);
           
            if (userRoleObj == null)
            {
                return NotFound();
            }
             _db.UserRoles.Remove(userRoleObj);         
            await _unitOfWork.SaveAsync();
            UserRolePartialVM userRolePartialVM = new UserRolePartialVM()
            {
                UserRoleList = await (from userRole in _db.UserRoles
                                      join roles in _db.Roles on userRole.RoleId equals roles.Id
                                      join users in _db.ApplicationUsers on userRole.UserId equals users.Id
                                      select new UserRole
                                      {
                                          UserId = users.Id,
                                          UserName = users.UserName,
                                          RoleId = roles.Id,
                                          RoleName = roles.Name
                                      }).Where(x => x.UserId == userId).ToListAsync(),
                UserId = userId
            };
            return PartialView("_ManageRolePartial", userRolePartialVM);

        }
        #endregion

        #region AssignClaimtoRole
        public async Task<IActionResult> AssignClaimToUser(string id)
        {
            var cliamList = await _unitOfWork.Claims.GetAllAsync();
            ManageUserClaimVM manageClaim = new ManageUserClaimVM()
            {
                User = await _unitOfWork.User.FirstOrDefaultAsync(x => x.Id == id),
                UserId = id,
            };
            var userClaimList = await _userManager.GetClaimsAsync(manageClaim.User);
            List<string> assignClaim = userClaimList.Select(x => x.Type).ToList();
            var dropClaimList = cliamList.Where(x => !assignClaim.Contains(x.ClaimType)).ToList();
            manageClaim.ClaimList = dropClaimList.Select(x => new SelectListItem
            {
                Text = x.ClaimType,
                Value = x.Id.ToString()
            });
            return View(manageClaim);
        }
        public async Task<IActionResult> ManageClaimPartial(string id)
        {
            var user =await _userManager.FindByIdAsync(id);
            UserClaimPartial userClaim = new UserClaimPartial()
            {
                UserClaimList= await _userManager.GetClaimsAsync(user),
                UserId = id
            };
            return PartialView("_ManageClaimPartial", userClaim);
        }
        [HttpPost]
        public async Task<IActionResult> AssignClaimToUser(string userId,Guid claimId)
        {
            if(!String.IsNullOrEmpty(userId)&& Guid.Empty!=claimId)
            {
                var user = await _userManager.FindByIdAsync(userId);
                if(user==null)
                {
                    return NotFound();
                }
                var claim = await _unitOfWork.Claims.FirstOrDefaultAsync(x => x.Id == claimId);
                if(claim==null)
                {
                    return NotFound();
                }
                var existsClaimType = await _db.UserClaims.Where(x=>x.UserId==user.Id).Select(x=>x.ClaimType).ToListAsync();
                var existsClaimValue = await _db.UserClaims.Where(x => x.UserId == user.Id).Select(x=>x.ClaimValue).ToListAsync();
                if(existsClaimType.Contains(claim.ClaimType)&&existsClaimValue.Contains(claim.ClaimValue))
                {
                    return NotFound();
                }
                
                await _userManager.AddClaimAsync(user,new Claim(claim.ClaimType, claim.ClaimValue));
                
                UserClaimPartial userClaim = new UserClaimPartial()
                {
                    UserClaimList = await _userManager.GetClaimsAsync(user),
                    UserId = userId
                };
                return PartialView("_ManageClaimPartial", userClaim);
            }
            return BadRequest();
        }
        [HttpPost]
        public async Task<IActionResult> DeleteClaimOfUser(string userId,string claimType,string claimValue)
        {
            if (!String.IsNullOrEmpty(userId) && !String.IsNullOrEmpty(claimType) &&!String.IsNullOrEmpty(claimValue))
            {
                var user = await _userManager.FindByIdAsync(userId);
                if(user==null)
                {
                    return NotFound();
                }
                await _userManager.RemoveClaimAsync(user, new Claim(claimType, claimValue));
              
                UserClaimPartial userClaim = new UserClaimPartial()
                {
                    UserClaimList = await _userManager.GetClaimsAsync(user),
                    UserId = userId
                };
                return PartialView("_ManageClaimPartial", userClaim);
            }
            return BadRequest();
        }
        #endregion
    }
}
