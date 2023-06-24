using API.Entities;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace API.DTOs
{
    public class UserDto
    {
        public string Username { get; set; }
        public string Token { get; set; }
        public string PhotoUrl { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NumeOrasCurent { get; set; }
        public City  Oras {get; set;}
        public Country  Country {get; set;}
        public string CountryNume { get; set; }
        public int Id{get;set;}
    }
}