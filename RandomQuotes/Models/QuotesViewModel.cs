using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RandomQuotes.Models
{
    public class QuotesViewModel : BaseEntity
    {
        public int Id { get; set; }

        [Required, MinLength(4), MaxLength(200)]
        public string Quote { get; set; }

        [Required, MinLength(4), MaxLength(50)]
        public string Author { get; set; }

        public new DateTime CreatedAt { get; set; } = DateTime.Now;
        public new DateTime UpdatedAt { get; set; } = DateTime.Now;

    }
}
