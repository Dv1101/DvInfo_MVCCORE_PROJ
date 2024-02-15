﻿using DvInfoWeb.Data;
using DvInfoWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace DvInfoWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            List<Category> objCategoryList = _db.Categories.ToList();

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
                _db.Categories.Add(obj);
                _db.SaveChanges();
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
            Category?  categoryFromDb2 = _db.Categories.Where(u => u.CategoryId == id).FirstOrDefault();
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
                _db.Categories.Update(obj);
                _db.SaveChanges();
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
            Category? categoryFromDb2 = _db.Categories.Where(u => u.CategoryId == id).FirstOrDefault();
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
            Category? obj = _db.Categories.Find(id);
            if(obj == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["SuccessMsg"] = "Category " + obj.Name + " Deleted.";
            return RedirectToAction("Index", "Category");
            

            //return View();



        }
    }
}
