using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    public class City
    {
        [Key] [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public int State_Id { get; set; }
        public string State_Code { get; set; }
        public int Country_Id { get; set; }
        public string Country_Code { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Updated_At { get; set; }
        public bool Flag { get; set; }
        public string WikiDataId { get; set; }
        public Country Country {get; set;}
        public State State {get; set;}
        public ICollection<Hotel> Hoteluri {get;set;}
        public ICollection<AtractieTuristica> AtractiiTuristice {get;set;}

    }
}
