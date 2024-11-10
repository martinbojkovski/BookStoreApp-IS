using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreApp.Domain.Entities
{
    public class BookInShoppingCart : BaseEntity
    {
        public Guid bookId { get; set; }
        public Book? book { get; set; }
        public Guid shoppingCartId { get; set; }
        public ShoppingCart? shoppingCart { get; set; }
        public int quantity { get; set; }
    }
}
