using System;
using System.ComponentModel.DataAnnotations;

namespace RustRemover.Api.Models
{
    public class Beer
    {
        public Guid Id { get; set; }

        [Required]
        public string Description { get; set; }
    }
}