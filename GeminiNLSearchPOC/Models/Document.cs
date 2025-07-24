// Models/Document.cs
using System.ComponentModel.DataAnnotations;

namespace GeminiNLSearchPOC.Models
{
    public class Document
    {
        public int Id { get; set; }
        
        [Required]
        public string Title { get; set; } = string.Empty;
        
        [Required]
        public string EscrowOfficer { get; set; } = string.Empty;
        
        public DateTime CreatedDate { get; set; }
        
        public string FileName { get; set; } = string.Empty;
    }
}