using BookStoreApp.Domain.Entities;
using BookStoreApp.Repository.Interface;
using BookStoreApp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreApp.Service.Implementation
{
    public class BookService : IBookService
    {
        private readonly IRepository<Book> _bookRepository;

        public BookService(IRepository<Book> bookRepository)
        {
            _bookRepository = bookRepository;        }

        public Book CreateNewBook(Book book)
        {
            return _bookRepository.Insert(book);
        }

        public Book DeleteBook(Guid id)
        {
            Book bookToDelete = _bookRepository.Get(id);
            return _bookRepository.Delete(bookToDelete);
        }

        public Book GetBookById(Guid? id)
        {
            return _bookRepository.Get(id);
        }

        public List<Book> GetBooks()
        {
            return _bookRepository.GetAll().ToList();
        }

        public Book UpdateBook(Book book)
        {
            return _bookRepository.Update(book);
        }
    }
}
