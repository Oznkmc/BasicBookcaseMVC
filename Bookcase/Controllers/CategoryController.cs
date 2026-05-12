using Bookcase.Data;
using Bookcase.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bookcase.Controllers
{
    public class CategoryController : Controller
    {
        private readonly UygulamaDbContext _context;

        public CategoryController(UygulamaDbContext context)
        {
            _context = context;
        }

        // URL: /Category yazınca burası çalışır
        public IActionResult Index()
        {
            var kategoriler = _context.Kategoriler.ToList();
            // View adın CategoryAdd olduğu için ismi açıkça belirtiyoruz
            return View("CategoryAdd", kategoriler);
        }

        // View içindeki form bu isme (UpsertCategory) gönderiyor
        [HttpPost]
        public IActionResult UpsertCategory(int CategoryId, string CategoryName)
        {
            if (string.IsNullOrEmpty(CategoryName)) return RedirectToAction("Index");

            if (CategoryId == 0) // Yeni Kayıt
            {
                var yeni = new Category { CategoryName = CategoryName };
                _context.Kategoriler.Add(yeni);
                TempData["Success"] = "Kategori başarıyla eklendi.";
            }
            else // Güncelleme
            {
                var mevcut = _context.Kategoriler.Find(CategoryId);
                if (mevcut != null)
                {
                    mevcut.CategoryName = CategoryName;
                    _context.Kategoriler.Update(mevcut);
                    TempData["Success"] = "Kategori güncellendi.";
                }
            }

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteCategory(int id)
        {
            var kategori = _context.Kategoriler.Find(id);
            if (kategori != null)
            {
                _context.Kategoriler.Remove(kategori);
                _context.SaveChanges();
                TempData["Success"] = "Kategori silindi.";
            }
            return RedirectToAction("Index");
        }
    }
}