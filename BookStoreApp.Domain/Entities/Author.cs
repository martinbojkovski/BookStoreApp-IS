using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreApp.Domain.Entities
{
    public class Author : BaseEntity
    {
        [Required]
        public string? name { get; set; }
        [Required]
        public string? surname { get; set; }
        public string? shortBiography { get; set; }
        public string? imageURL { get; set; }
        public virtual ICollection<Book>? books { get; set; }
    }
}
