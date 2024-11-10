using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreApp.Domain.Entities
{
    public class Book : BaseEntity
    {
        [Required]
        public string? isbn { get; set; }
        [Required]
        public string? title { get; set; }
        public string? description { get; set; }
        public string? imageURL { get; set; }
        [Required]
        public int totalPages { get; set; }
        [Required]
        public double? rating { get; set; }
        public double? price { get; set; }
        public Guid AuthorId { get; set; }
        public  Author? Author { get; set; }
        public virtual ICollection<BookInShoppingCart>? bookInShoppingCart { get; set; }
        public virtual ICollection<BookInOrder>? bookInOrder { get; set; }

    }
}
