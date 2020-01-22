using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Ksiegarnia.Data;
using Ksiegarnia.Models;

namespace Ksiegarnia.Controllers
{
    public class BasketController : Controller
    {
        private readonly BookContext _context;
        List<BookModel> booksInBasket;

        public BasketController(BookContext context)
        {
            _context = context;
            booksInBasket = new List<BookModel>();
        }

        public IActionResult Index()
        {
            String basket = HttpContext.Session.GetString("basket");
            var books = from b in _context.Book select b;
            string idOfBook = "";

            try
            {
                for (int i = 0; i < basket.Length - 1; i++)
                {
                    if (basket[i] != ';')
                    {
                        idOfBook += basket[i];
                        if (basket[i + 1] == ';')
                        {
                            BookModel book = books.First(s => s.Id.ToString().Contains(idOfBook));
                            booksInBasket.Add(book);
                            idOfBook = "";
                        }   
                    }
                }
            }
            catch(NullReferenceException)
            {
                return View(null);
            }
            return View(booksInBasket);
        }

        public async Task<IActionResult> Delete(int id)
        {
            string basket = HttpContext.Session.GetString("basket");
            int len = id.ToString().Length;
            for (int i = 0; i < basket.Length - len; i++) 
            {
                if (basket.Substring(i, len) == id.ToString())
                {
                    basket = basket.Remove(i, len+1);
                    break;
                }
            }
            HttpContext.Session.SetString("basket", basket);

            var bookModel = booksInBasket.Find(b=>b.Id.Equals(id));
            booksInBasket.Remove(bookModel);

                return RedirectToAction(nameof(Index));
        }
    }
}