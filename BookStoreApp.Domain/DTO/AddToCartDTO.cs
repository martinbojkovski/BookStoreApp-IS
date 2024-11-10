using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreApp.Domain.DTO
{
    public class AddToCartDTO
    {
        public Guid selectedBookId { get; set; }
        public string? selectedBookTitle { get; set; }
        public int quantity { get; set; }
    }
}
