using BookStoreApp.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreApp.Domain.Entities
{
    public class ShoppingCart : BaseEntity
    {
        public string? ownerId { get; set; }
        public BookStoreUser? owner { get; set; }
        public virtual ICollection<BookInShoppingCart>? bookInShoppingCart { get; set; }
    }
}
