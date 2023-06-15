using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class AtractieTuristicaDto
    {
        public int Id{ get; set; }
        [Required]
        public string Nume { get; set; }

        [Required] public string Descriere { get; set; }
        [Required] public string Adresa { get; set; }
        public int CityId { get; set; }
        public string PhotoUrl { get; set; }
        public List<PhotoDto> Photos { get; set; }
    }
}