using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;


namespace ErrorHandlerApp.Models
{
    public class Report
    {
        [Key]
        public int Id { get; set; }

        public string Content { get; set; } = string.Empty;

        public int BugId { get; set; }
        public Bug Bug { get; set; } = null!;
    }
}
