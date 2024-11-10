using BookStoreApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreApp.Domain.DTO
{
    public class ShoppingCartDTO
    {
        public List<BookInShoppingCart>? allBooks { get; set; }
        public double totalPrice { get; set; }
    }
}
