using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Photos")]
    public class Photo
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public bool IsMain { get; set; }
        public string PublicId { get; set; }

        public int AppUserId { get; set; }
        public int AtractieTuristicaId { get; set; }
        public int HotelId { get; set; }

        public AppUser AppUser { get; set; }
        public AtractieTuristica AtractieTuristica{get; set;}
        public Hotel Hotel {get; set;}
    }
}