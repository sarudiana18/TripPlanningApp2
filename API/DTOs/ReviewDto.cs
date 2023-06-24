using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class ReviewDto
    {
        public string DescriereReview{get; set;}
        public decimal Nota {get; set; }
        public DateTime Created_At { get; set; } 
        public int? HotelId{ get; set;}
        public int? RestaurantId{ get; set;}
        public int? ParcId{ get; set;}
        public int CreatedBy{get;set;}
        public string CreatedByNume{get;set;}
        public string Titlu {get;set;}
    }
}