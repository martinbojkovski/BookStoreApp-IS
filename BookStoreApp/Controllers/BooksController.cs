using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookStoreApp.Data;
using BookStoreApp.Domain.Entities;
using BookStoreApp.Service.Interface;
using BookStoreApp.Domain.DTO;
using BookStoreApp.Service.Implementation;
using System.Security.Claims;

namespace BookStoreApp.Web.Controllers
{
    public class BooksController : Controller
    {
        private readonly IAuthorService _authorSerivce;
        private readonly IBookService _bookSerivce;
        private readonly IShoppingCartService _shoppingCartSerivce;

        public BooksController(IAuthorService authorService, IBookService bookService, IShoppingCartService shoppingCartSerivce)
        {
            _authorSerivce = authorService;
            _bookSerivce = bookService;
            _shoppingCartSerivce = shoppingCartSerivce;
        }

        // GET: Books
        public IActionResult Index()
        {
            return View(_bookSerivce.GetBooks());
        }

        // GET: Books/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = _bookSerivce.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }

            var author = _authorSerivce.GetAuthors().FirstOrDefault(a => a.Id == book.AuthorId);
            var authorFullName = author != null ? $"{author.name} {author.surname}" : "Unknown Author";

            ViewBag.AuthorName = authorFullName;

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            ViewBag.AuthorId = new SelectList(_authorSerivce.GetAuthors().Select(a => new { Id = a.Id, FullName = a.name + " " + a.surname }),"Id","FullName");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("isbn,title,description,imageURL,totalPages,rating,price,AuthorId,Id")] Book book)
        {
            if (ModelState.IsValid)
            {
                _bookSerivce.CreateNewBook(book);
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = _bookSerivce.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Id, isbn, title, description, imageURL, totalPages, rating, price, AuthorId")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _bookSerivce.UpdateBook(book);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
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
            return View(book);
        }

        // GET: Books/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = _bookSerivce.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _bookSerivce.DeleteBook(id);
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(Guid id)
        {
            return _bookSerivce.GetBookById(id) != null;
        }

        public IActionResult AddToCart(Guid Id)
        {
            var result = _shoppingCartSerivce.getBookInfo(Id);
            if (result != null)
            {
                return View(result);
            }
            return View();
        }

        [HttpPost]
        public IActionResult AddToCart(AddToCartDTO model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = _shoppingCartSerivce.AddBookToShoppingCart(userId, model);

            if (result != null)
            {
                return RedirectToAction("Index", "ShoppingCarts");
            }
            else { return View(model); }
        }
    }
}
