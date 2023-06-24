using System.ComponentModel.DataAnnotations;
using API.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;

namespace API.DTOs
{
    public class HotelDto
    {
        public int Id {get; set;}
        [Required]
        public string Nume {get; set;}
        [Required]
        public string Adresa {get; set;}
        [Required]
        public decimal PricePerNight{get; set;}
        public decimal PricePerNightCameraTripla{get; set;}
        public decimal Rating{get;set;}
        public int CityId {get; set;}
        public string PhotoUrl { get; set; }
         public List<PhotoDto> Photos { get; set; }
          public List<ReviewDto> Reviews { get; set; } 
    }

    public class HotelFilterDto
    {
        public int? Id {get; set;}
        public string Nume {get; set;}
        public string Adresa {get; set;}
        public decimal? PricePerNight{get; set;}
        public decimal? PricePerNightCameraTripla{get; set;}
        public decimal? Rating{get;set;}
        public int? CityId {get; set;}
        public string PhotoUrl { get; set; }
        public int PageNumber{get; set;}
        public int PageSize{get;set;}
        public int Buget{get; set;}
        public int NrNopti{get;set;}
        public int NrPersoane{get; set;}
        public string SortField{get;set;}
        public List<PhotoDto> Photos { get; set; }
        public List<ReviewDto> Reviews { get; set; } 
    }
}