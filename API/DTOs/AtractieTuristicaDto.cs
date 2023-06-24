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
    public class AtractieTuristicaFilterDto
    {
        public int? Id{ get; set; }
        public string Nume { get; set; }

        public string Descriere { get; set; }
        public string Adresa { get; set; }
        public int? CityId { get; set; }
        public string PhotoUrl { get; set; }
        public List<PhotoDto> Photos { get; set; }
        public int PageNumber{get; set;}
        public int PageSize{get;set;}
        public string SortField{get;set;}
    }
}