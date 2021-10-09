using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UMS.Data.IRepository;

namespace UMS.Areas.Teacher.Controllers
{
    public class CourseContentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CourseContentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #region Index
        public IActionResult Index()
        {
            return View();
        }

        
        #endregion
    }
}
