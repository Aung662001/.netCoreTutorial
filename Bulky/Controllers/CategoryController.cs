using Bulky.Data;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bulky.Controllers
{
	public class CategoryController(ApplicationDbContext db) : Controller
	{
		public IActionResult Index()
		{
			List<Category> objCategoryList = [.. db.Categories.OrderBy(u=> u.DispleyOrder)];
			return View(objCategoryList);
		}
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(Category obj)
		{
			if (obj.Name == obj.DispleyOrder.ToString())
			{
				ModelState.AddModelError("name", "Category and Display Order can't be exctly the same!");
			}
			//if (obj.Name != null && obj.Name.ToLower() == "test")
			//{
			//	ModelState.AddModelError("", "test is not a valid Category!");
			//}
			if (ModelState.IsValid)
			{
				db.Categories.Add(obj);
				db.SaveChanges();
				TempData["success"] = "Category created successfully!";
				return RedirectToAction("Index");
			}
			else
			{
				return View();
			}
		}

		public IActionResult Edit(int? id)
		{
            if (id == null || id == 0)
			{
				return NotFound();
			}
			Category? category = db.Categories.Find(id);
			//Category? category1 = db.Categories.FirstOrDefault(u=>u.Id == id);
			//Category? category2 = db.Categories.Where(u=> u.Id == id).FirstOrDefault();
            if (category == null)
			{
				return NotFound();
			}
			return View(category);
		}

		[HttpPost]
		public IActionResult Edit(Category obj)
		{
			if (ModelState.IsValid)
			{
				db.Categories.Update(obj);
				db.SaveChanges();
				TempData["success"] = "Category updated successfully!";
				return RedirectToAction("Index");
			}
			return View();
		}

		public IActionResult Delete(int? id)
		{
			if(id == null || id == 0)
			{
				return NotFound();
			}
			Category? category = db.Categories.Find(id);
			if(category == null)
			{
				return NotFound();
			}
			return View(category);
		}
		[HttpPost,ActionName("Delete")]
		public IActionResult DeletePost(int? id)
		{
			Category? cat = db.Categories.Find(id);
			if(cat == null  )
			{
				return NotFound();
			}
			db.Categories.Remove(cat);
			db.SaveChanges();
			TempData["success"] = "Category deleted successfully!";
			return RedirectToAction("Index");
		}
	}
}
