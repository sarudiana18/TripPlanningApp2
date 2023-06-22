using System.ComponentModel.DataAnnotations;
using API.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;

namespace API.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string Username { get; set; }

        [Required] public string FirstName { get; set; }
        [Required] public string LastName { get; set; }
        [Required] public string Email { get; set; }
        [Required] public DateOnly? DateOfBirth { get; set; } // optional to make required work!
        [Required] public City City { get; set; }
        [Required] public Country Country { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 6)]
        public string Password { get; set; }
    }
}