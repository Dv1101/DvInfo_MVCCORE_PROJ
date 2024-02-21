using DvInfoWeb.DataAccess.Data;
using DvInfoWeb.DataAccess.Repository.IRepository;
using DvInfoWeb.Models;
using DvInfoWeb.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DvInfoWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CategoryController : Controller
    {
        /*private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }*/

        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Category> objCategoryList = _unitOfWork.Category.GetAll().ToList();

            return View(objCategoryList);
        }
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Name and Display order shall be different !!");
            }
            if (obj.Name != null && obj.Name.ToLower() == "test")
            {
                ModelState.AddModelError("", "Test is an invalid value");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(obj);
                _unitOfWork.save();
                TempData["SuccessMsg"] = "Category " + obj.Name + " Created.";
                return RedirectToAction("Index", "Category");
            }

            return View();



        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //Category? categoryFromDb = _db.Categories.Find(id);
            //Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u => u.CategoryId == id);
            Category? categoryFromDb2 = _unitOfWork.Category.Get(u => u.CategoryId == id);
            if (categoryFromDb2 == null)
            {
                return NotFound();
            }

            return View(categoryFromDb2);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            /*if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Name and Display order shall be different !!");
            }
            if (obj.Name != null && obj.Name.ToLower() == "test")
            {
                ModelState.AddModelError("", "Test is an invalid value");
            }*/
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.update(obj);
                _unitOfWork.save();
                TempData["SuccessMsg"] = "Category " + obj.Name + " Updated.";
                return RedirectToAction("Index", "Category");
            }

            return View();



        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //Category? categoryFromDb = _db.Categories.Find(id);
            //Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u => u.CategoryId == id);
            Category? categoryFromDb2 = _unitOfWork.Category.Get(u => u.CategoryId == id);
            if (categoryFromDb2 == null)
            {
                return NotFound();
            }

            return View(categoryFromDb2);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            /*if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Name and Display order shall be different !!");
            }
            if (obj.Name != null && obj.Name.ToLower() == "test")
            {
                ModelState.AddModelError("", "Test is an invalid value");
            }*/
            Category? obj = _unitOfWork.Category.Get(u => u.CategoryId == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Category.Remove(obj);
            _unitOfWork.save();
            TempData["SuccessMsg"] = "Category " + obj.Name + " Deleted.";
            return RedirectToAction("Index", "Category");


            //return View();



        }
    }
}
