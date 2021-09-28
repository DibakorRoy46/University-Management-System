using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using UMS.Data.IRepository;
using UMS.Models.Models;
using UMS.Models.ViewModels;

namespace UMS.Areas.Admin.Controllers
{
    
    [Area("Admin")]
    [Authorize]
    public class AccountController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UrlEncoder _urlEncoder;
        private readonly IUnitOfWork _unitOfWork;
        public AccountController(RoleManager<IdentityRole> roleManager,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            UrlEncoder urlEncoder,
            IUnitOfWork unitOfWork)
        {
            _urlEncoder = urlEncoder;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
        }
       
        #region Register
        
        public async Task<IActionResult> Register(string returnurl = null)
        {
            ViewData["ReturnUrl"] = returnurl;
            var roleList = await _roleManager.Roles.ToListAsync();
            var departemntList = await _unitOfWork.Department.GetAllAsync();

            RegisterVM registerVM = new RegisterVM()
            {
                RoleList = roleList.Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }),
                DepartmentList = departemntList.Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                })
            };
            return View(registerVM);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult>Register( RegisterVM model,string returnurl=null)
        {
            ViewData["ReturnUrl"] = returnurl;
            returnurl = returnurl ?? Url.Content("~");
            if(ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    Name = model.Name,
                    UserName = model.Email,
                    Email = model.Email,
                    RegisterDate = DateTime.Now
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if(result.Succeeded)
                {
                    if(model.RoleSelected!=null )
                    {
                        var role = await _roleManager.Roles.FirstOrDefaultAsync(x => x.Id == model.RoleSelected);
                        var department = await _unitOfWork.Department.FirstOrDefaultAsync(x => x.Id.Equals(model.DepartmentSelected));
                        await _userManager.AddToRoleAsync(user, role.Name);
                        UserDetails userDetails = new UserDetails();
                        userDetails.UserId = user.Id;
                        userDetails.DepartmentId = model.DepartmentSelected;
                        await _unitOfWork.UserDetials.AddAsync(userDetails);
                        await _unitOfWork.SaveAsync();
                    }
                    TempData["success"] = "Account Created Successfully";
                    return RedirectToAction("Index","User",new { area=""});
                }
                AddErrors(result);             
            }
            var roleList = await _roleManager.Roles.ToListAsync();
            var departemntList = await _unitOfWork.Department.GetAllAsync();

            model.RoleList = roleList.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
            model.DepartmentList = departemntList.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
            return View(model);
        }
        #endregion

        #region Login
        [AllowAnonymous]
        public async Task<IActionResult>Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult>Login(LoginVM model,string returnurl=null)
        {
            ViewData["ReturnUrl"] = returnurl;
            returnurl = returnurl ?? Url.Content("~");
            if(ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if(result.Succeeded)
                {
                    return RedirectToAction("Index", "Home", new { area = "Student" });
                }
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(model);
            }
            return View(model);
        }
        #endregion
        #region LogOut
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }

        #endregion


        #region Error
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        #endregion
    }
}
