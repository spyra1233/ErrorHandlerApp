using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;


namespace ErrorHandlerApp.Models
{
    public class Bug
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string Status { get; set; } = "Open";

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required]
        public string Reporter { get; set; } = string.Empty;
    }
}

