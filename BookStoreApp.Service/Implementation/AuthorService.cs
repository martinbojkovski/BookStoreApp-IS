using BookStoreApp.Domain.Entities;
using BookStoreApp.Repository.Interface;
using BookStoreApp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreApp.Service.Implementation
{
    public class AuthorService : IAuthorService
    {
        private readonly IRepository<Author> _authorRepository;

        public AuthorService(IRepository<Author> authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public Author CreateNewAuthor(Author author)
        {
            return _authorRepository.Insert(author);
        }

        public Author DeleteAuthor(Guid id)
        {
            var authorToDelete = _authorRepository.Get(id);
            return _authorRepository.Delete(authorToDelete);
        }

        public Author GetAuthorById(Guid? id)
        {
            return _authorRepository.Get(id);
        }

        public List<Author> GetAuthors()
        {
            return _authorRepository.GetAll().ToList();
        }

        public Author UpdateAuthor(Author author)
        {
            return _authorRepository.Update(author);
        }
    }
}
