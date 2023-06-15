using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public class State
    {
        [Key]        
        public int Id { get; set; }
        public string Name { get; set; }
        public int Country_Id { get; set; }
        public string Country_Code { get; set; }
        public string Fips_Code { get; set; }
        public string Iso2 { get; set; }
        public string Type { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public DateTime? Created_At { get; set; }
        public DateTime Updated_At { get; set; }
        public bool Flag { get; set; }
        public string WikiDataId { get; set; }
        public ICollection<City> Cities {get;set;}
        public Country Country {get;set;}
    }
}