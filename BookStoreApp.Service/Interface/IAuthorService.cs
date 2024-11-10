using BookStoreApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreApp.Service.Interface
{
    public interface IAuthorService
    {
        public List<Author> GetAuthors();
        public Author GetAuthorById(Guid? id);
        public Author CreateNewAuthor(Author author);
        public Author UpdateAuthor(Author author);
        public Author DeleteAuthor(Guid id);
    }
}
