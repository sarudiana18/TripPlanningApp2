using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public class Country
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Iso3 { get; set; }
        public string Numeric_Code { get; set; }
        public string Iso2 { get; set; }
        public string PhoneCode { get; set; }
        public string Capital { get; set; }
        public string Currency { get; set; }
        public string Currency_Name { get; set; }
        public string Currency_Symbol { get; set; }
        public string Tld { get; set; }
        public string Native { get; set; }
        public string Region { get; set; }
        public string Subregion { get; set; }
        public string Timezones { get; set; }
        public string Translations { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public string Emoji { get; set; }
        public string EmojiU { get; set; }
        public DateTime? Created_At { get; set; }
        public DateTime Updated_At { get; set; }
        public bool Flag { get; set; }
        public string WikiDataId { get; set; }
        public ICollection<City> Cities { get; set; }
        public ICollection<State> States { get; set; }

    
    }
}
