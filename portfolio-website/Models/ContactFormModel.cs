// === ContactFormModel.cs ===
// Purpose: Strongly-typed contact form with validation rules shared by client and server.

using System.ComponentModel.DataAnnotations;

namespace PortfolioWebsite.Models
{
    /// <summary>
    /// Contact form model with client-side and server-side validation.
    /// Frontend-only; no persistence. Intent: validate user input before modal feedback.
    /// </summary>
    public class ContactFormModel
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100)]
        public required string Name { get; set; }
        
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [StringLength(200)]
        public required string Email { get; set; }
        
        [Required(ErrorMessage = "Message is required")]
        [StringLength(1000)]
        public required string Message { get; set; }
    }
}