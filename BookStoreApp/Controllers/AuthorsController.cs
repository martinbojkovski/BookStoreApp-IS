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

namespace BookStoreApp.Web.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        // GET: Authors
        public IActionResult Index()
        {
            return View(_authorService.GetAuthors());
        }

        // GET: Authors/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = _authorService.GetAuthorById(id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // GET: Authors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Authors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("name,surname,shortBiography,imageURL,id")] Author author)
        {
            if (ModelState.IsValid)
            {
                _authorService.CreateNewAuthor(author);
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }

        // GET: Authors/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = _authorService.GetAuthorById(id);
            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }

        // POST: Authors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("name,surname,shortBiography,imageURL,id")] Author author)
        {
            if (id != author.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _authorService.UpdateAuthor(author);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthorExists(author.Id))
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
            return View(author);
        }

        // GET: Authors/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = _authorService.GetAuthorById(id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _authorService.DeleteAuthor(id);
            return RedirectToAction(nameof(Index));
        }

        private bool AuthorExists(Guid id)
        {
            return _authorService.GetAuthorById(id) != null;
        }
    }
}
