using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ksiegarnia.Data;
using Ksiegarnia.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Ksiegarnia.Controllers
{
    public class BookController : Controller
    {
        private readonly BookContext _context;
        private List<BookModel> basketList;
        private bool BookModelExists(int id)
        {
            return _context.Book.Any(e => e.Id == id);
        }

        public BookController(BookContext context)
        {
            _context = context;
            basketList = new List<BookModel>();
        }

        public async Task<IActionResult> Index(string searchString)
        {
            var books = from b in _context.Book select b;

            if (!String.IsNullOrEmpty(searchString))
            {
                books = books.Where(s => s.Title.Contains(searchString));
            }
            return View(await books.ToListAsync());
        }

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

        public IActionResult Create()
        {
            return View();
        }
        
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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookModel = await _context.Book.FindAsync(id);
            _context.Book.Remove(bookModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> AddToBasket(int id)
        {
            HttpContext.Session.SetString("basket", HttpContext.Session.GetString("basket") + id.ToString() + ";");

            return RedirectToAction(nameof(Index));
        }
    }
}