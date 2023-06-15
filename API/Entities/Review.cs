using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class Review
    {
        [Key][DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int Id {get; set;}
        public string DescriereReview{get; set;}
        public decimal Nota {get; set; }
        public string Titlu{get;set;}
        public DateTime? Created_At { get; set; } 
        public AppUser CreatedByUser {get; set;}
        public int HotelId{ get; set;}
        public int CreatedBy{get;set;}
        public Hotel Hotel {get; set;}
    }
}