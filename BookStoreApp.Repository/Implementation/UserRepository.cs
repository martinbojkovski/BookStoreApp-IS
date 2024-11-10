using BookStoreApp.Data;
using BookStoreApp.Domain.Identity;
using BookStoreApp.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreApp.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<BookStoreUser> entities;
        string errorMessage = string.Empty;

        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<BookStoreUser>();
        }

        public void Delete(BookStoreUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
        }

        public BookStoreUser Get(string id)
        {
            var strGuid = id.ToString();
            return entities
                .Include(z => z.UserCart)
                .Include(z => z.UserCart.bookInShoppingCart)
                .Include("UserCart.bookInShoppingCart.book")
                .First(s => s.Id == strGuid);
        }

        public IEnumerable<BookStoreUser> GetAll()
        {
            return entities.AsEnumerable();
        }

        public void Insert(BookStoreUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(BookStoreUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            context.SaveChanges();
        }
    }
}
