using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ksiegarnia.Data;
using Ksiegarnia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace Ksiegarnia.Controllers
{
    [Authorize]
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _context;
        private bool BookModelExists(int id)
        {
            return _context.Book.Any(e => e.Id == id);
        }

        public BookController(ApplicationDbContext context)
        {
            _context = context;
        }

        [ResponseCache(Duration = 60)]
        [AllowAnonymous]
        public async Task<IActionResult> Index(string searchString)
        {
            var books = from b in _context.Book select b;

            if (!string.IsNullOrEmpty(searchString))
            {
                books = books.Where(s => s.Title.Contains(searchString));
            }
            return View(await books.ToListAsync());
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookModel = await _context.Book.FirstOrDefaultAsync(m => m.Id == id);

            if (bookModel == null)
            {
                return NotFound();
            }

            return View(bookModel);
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookModel bookModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bookModel);
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookModel = await _context.Book.FindAsync(id);

            if (bookModel == null)
            {
                return NotFound();
            }
            return View(bookModel);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BookModel bookModel)
        {
            if (id != bookModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookModelExists(bookModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(bookModel);
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookModel = await _context.Book.FirstOrDefaultAsync(m => m.Id == id);

            if (bookModel == null)
            {
                return NotFound();
            }

            return View(bookModel);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookModel = await _context.Book.FindAsync(id);
            _context.Book.Remove(bookModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [AllowAnonymous]
        public async Task<IActionResult> AddToBasket(int id)
        {
            var bookModel = await _context.Book.FirstOrDefaultAsync(m => m.Id == id);
            int amount;
            try
            {
                amount = (int)HttpContext.Session.GetInt32("amount");
            }
            catch (InvalidOperationException)
            {
                amount = 0;
            }
            HttpContext.Session.SetString("basket", HttpContext.Session.GetString("basket") + id.ToString() + ";");
            HttpContext.Session.SetInt32("amount", amount + Convert.ToInt32(bookModel.Price * 100));
            return RedirectToAction(nameof(Index));
        }
    }
}