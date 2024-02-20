using DvInfoWeb.DataAccess.Data;
using DvInfoWeb.DataAccess.Repository.IRepository;
using DvInfoWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DvInfoWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        /*private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }*/

        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll().ToList();
            
            return View(objProductList);
        }
        public IActionResult Create()
        {
            //projection in efcore
            IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.CategoryId.ToString()
            });
            ViewBag.CategoryList = CategoryList; //keyvalue pair passed.
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product obj, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\Products");   
                    using(var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    obj.ImageUrl = @"\images\Products\" + fileName;
                }

                
                _unitOfWork.Product.Add(obj);
                _unitOfWork.save();
                TempData["SuccessMsg"] = "Product " + obj.Title + " Created.";
                return RedirectToAction("Index", "Product");
            }

            return View();



        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //Product? categoryFromDb = _db.Categories.Find(id);
            //Product? categoryFromDb1 = _db.Categories.FirstOrDefault(u => u.ProductId == id);
            Product? categoryFromDb2 = _unitOfWork.Product.Get(u => u.Id == id);
            if (categoryFromDb2 == null)
            {
                return NotFound();
            }

            //projection in efcore
            IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.CategoryId.ToString()
            });
            ViewBag.CategoryList = CategoryList; //keyvalue pair passed.



            return View(categoryFromDb2);
        }
        [HttpPost]
        public IActionResult Edit(Product obj, IFormFile? file)
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
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\Products");

                    if(!string.IsNullOrEmpty(obj.ImageUrl))
                    {
                        //delete old image
                        var oldImgPath = Path.Combine(wwwRootPath, obj.ImageUrl.TrimStart('\\'));

                        if(System.IO.File.Exists(oldImgPath))
                        {
                            System.IO.File.Delete(oldImgPath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    obj.ImageUrl = @"\images\Products\" + fileName;
                }

                _unitOfWork.Product.update(obj);
                _unitOfWork.save();
                TempData["SuccessMsg"] = "Product " + obj.Title + " Updated.";
                return RedirectToAction("Index", "Product");
            }

            return View();



        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //Product? categoryFromDb = _db.Categories.Find(id);
            //Product? categoryFromDb1 = _db.Categories.FirstOrDefault(u => u.ProductId == id);
            Product? productFromDb2 = _unitOfWork.Product.Get(u => u.Id == id);
            if (productFromDb2 == null)
            {
                return NotFound();
            }

            return View(productFromDb2);
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
            Product? obj = _unitOfWork.Product.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            

            if (!string.IsNullOrEmpty(obj.ImageUrl))
            {
                //delete old image
                var oldImgPath = Path.Combine(wwwRootPath, obj.ImageUrl.TrimStart('\\'));

                if (System.IO.File.Exists(oldImgPath))
                {
                    System.IO.File.Delete(oldImgPath);
                }
            }
            _unitOfWork.Product.Remove(obj);
            _unitOfWork.save();
            TempData["SuccessMsg"] = "Product " + obj.Title + " Deleted.";
            return RedirectToAction("Index", "Product");


            //return View();



        }

        #region API CALLS

        public IActionResult GetAll()
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll().ToList();
            return Json(new {data = objProductList});
        }

        #endregion

    }
}
