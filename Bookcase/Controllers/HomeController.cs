using Bookcase.Data;
using Bookcase.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal; // ToListAsync gibi işlemler için lazım olabilir

namespace Bookcase.Controllers
{
    public class HomeController : Controller
    {
        private readonly UygulamaDbContext _context;

        public HomeController(UygulamaDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.BookCount = _context.Kitaplar.Count();
            ViewBag.ThickBookCount = _context.Kitaplar.Count(k => k.BookPages > 500);
            return View();
        }

        // --- DİKKAT: İki Books metodu TEK bir metotta birleşti ---
        public IActionResult Books(string aramaDizesi)
        {
            // 1. Sorguyu hazırla
            var kitaplar = _context.Kitaplar.AsQueryable();

            // 2. Eğer arama kutusu boş değilse filtreyi uygula
            if (!string.IsNullOrEmpty(aramaDizesi))
            {
                kitaplar = kitaplar.Where(s => s.BookName.Contains(aramaDizesi) || s.Author.Contains(aramaDizesi));
            }

            // 3. Tek bir liste olarak döndür
            return View(kitaplar.ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Add() => View();
        [HttpPost]
        public IActionResult Add(BooksInfo book)
        {
            if (ModelState.IsValid)
            {
                string yeniKitapAdi = book.BookName.Trim().ToLower();
                string yeniYazarAdi = book.Author.Trim().ToLower();

                bool varMi = _context.Kitaplar.Any(k =>
                    k.BookName.Trim().ToLower() == yeniKitapAdi &&
                    k.Author.Trim().ToLower() == yeniYazarAdi);

                if (!varMi)
                {
                    _context.Kitaplar.Add(book);
                    _context.SaveChanges();
                    TempData["Success"] = $"«{book.BookName}» koleksiyonuna başarıyla eklendi.";
                    return RedirectToAction("Books");
                }
                else
                {
                    // DİKKAT: Burası "Success" değil "Error" olmalı!
                    TempData["Error"] = $"«{book.BookName}» zaten kitaplıkta mevcut.";
                    ModelState.AddModelError("", "Bu kitap zaten kayıtlı.");
                }
            }
            return View(book);
        }
        [HttpGet]
        public IActionResult Duzenle(int id)
        {
            var kitap = _context.Kitaplar.Find(id);
            if (kitap == null) return NotFound();
            return View(kitap);
        }

        [HttpPost]
        public IActionResult Duzenle(BooksInfo book)
        {
            if (ModelState.IsValid)
            {
                _context.Kitaplar.Update(book);
                _context.SaveChanges();
                return RedirectToAction("Books");
            }
            return View(book);
        }

        public IActionResult Sil(int id)
        {
            var kitap = _context.Kitaplar.Find(id);
            if (kitap == null) return NotFound();

            _context.Kitaplar.Remove(kitap);
            _context.SaveChanges();
            return RedirectToAction("Books");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}