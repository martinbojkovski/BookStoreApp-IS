using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreApp.Domain.Entities
{
    public class BookInOrder : BaseEntity
    {
        public Guid bookId { get; set; }
        public Book? orderedBook { get; set; }
        public Guid orderId { get; set; }
        public Order? order { get; set; }
        public int quantity { get; set; }
    }
}
