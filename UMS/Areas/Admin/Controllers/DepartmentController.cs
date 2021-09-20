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
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public DepartmentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #region Index
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<IActionResult>DepartmentTable(string searchValue,int pageNo,int pageSize)
        {
            pageNo = pageNo != 0 ? pageNo : 1;
            pageSize = 10;
            int numberOfDepartment = await _unitOfWork.Department.CountAsync(searchValue);
            DepartmentVM departmentVM = new DepartmentVM()
            {
                DepartmentList=await _unitOfWork.Department.SearchAsync(searchValue,pageNo,pageSize),
                Search=searchValue,
                Pager=new Pager(numberOfDepartment,pageNo,pageSize)
            };
            return PartialView("_DepartmentTable", departmentVM);

        }
        #endregion

        #region Upsert
        public async Task<IActionResult>Upsert(Guid id)
        {
            try
            {
                Department department = new Department();
                if (id.Equals(Guid.Empty))
                {
                    return View(department);
                }
                else
                {
                    department = await _unitOfWork.Department.FirstOrDefaultAsync(x => x.Id.Equals(id));
                    if(department==null)
                    {
                        return NotFound();
                    }
                    return View(department);
                }
            }
            catch(Exception ex)
            {
                return NotFound();
            }
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Upsert(Department department)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (department.Id.Equals(Guid.Empty))
                    {
                        await _unitOfWork.Department.AddAsync(department);
                        TempData["message"] = "Successfully Created";
                    }
                    else
                    {
                        await _unitOfWork.Department.UpdateAsync(department);
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
        public async Task<IActionResult>Delete(Guid id, int pageNo, int pageSize)
        {
            try
            {
                Department departmentObj = await _unitOfWork.Department.FirstOrDefaultAsync(x => x.Id.Equals(id));
                if(departmentObj==null)
                {
                    return NotFound();
                }
                await _unitOfWork.Department.RemoveAsync(departmentObj);
                await _unitOfWork.SaveAsync();
                pageNo = pageNo != 0 ? pageNo : 1;
                pageSize = 10;
                int numberOfDepartment = await _unitOfWork.Department.CountAsync(null);
                DepartmentVM departmentVM = new DepartmentVM()
                {
                    DepartmentList = await _unitOfWork.Department.SearchAsync(null, pageNo, pageSize),
                    Search = null,
                    Pager = new Pager(numberOfDepartment, pageNo, pageSize)
                };
                return PartialView("_DepartmentTable", departmentVM);
            }
            catch(Exception ex)
            {
                return NotFound();
            }
        }
        #endregion

        #region Exists
        public async Task<IActionResult>ExistName(string name)
        {
            try
            {
                var exist = await _unitOfWork.Department.Exists(x => x.Name.ToLower().Equals(name.ToLower()));
                if(exist==true)
                {
                    return Json(new { success = false });
                }
                return Json(new { success = true });
            }
            catch(Exception ex)
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> ExistInitial(string initial)
        {
            try
            {       
                var exist = await _unitOfWork.Department.Exists(x => x.Initial.ToLower().Equals(initial.ToLower()));
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
