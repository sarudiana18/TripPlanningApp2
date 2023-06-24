using API.Entities;

namespace API.DTOs
{
    public class MemberDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PhotoUrl { get; set; }
        public int Age { get; set; }
        public string KnownAs { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public string CityNume { get; set; }
        public string CountryNume { get; set; }

        public City City{get;set;}
        public Country Country {get;set;}
        public State State {get;set;}
        public List<PhotoDto> Photos { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}