using API.Entities;

namespace API.DTOs
{
    public class MemberUpdateDto
    {
        public string CityNume { get; set; }
        public int CityId{get;set;}
        public City City{get; set;}
        public string CountryNume { get; set; }
        public int CountryId {get;set;}
        public Country Country {get;set;}
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}