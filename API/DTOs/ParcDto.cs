using System.ComponentModel.DataAnnotations;
using API.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;

namespace API.DTOs
{
    public class ParcDto
    {
        public int Id {get; set;}
        [Required]
        public string Nume {get; set;}
        [Required]
        public string Adresa {get; set;}
        public decimal Rating{get;set;}
        public int CityId {get; set;}
        public string PhotoUrl { get; set; }
         public List<PhotoDto> Photos { get; set; }
          public List<ReviewDto> Reviews { get; set; } 
    }
}