
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    public class Hotel
    {
        [Key][DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int Id {get; set;}
        public string Nume {get; set;}
        public string Adresa {get; set;}
        public decimal PricePerNight{get; set;}
        public decimal Rating{get;set;}
        public int CityId {get; set;}
        public City City {get; set; }
        public List<Photo> Photos { get; set; } = new();
        public ICollection<Review> Reviews {get; set;}
    }
}