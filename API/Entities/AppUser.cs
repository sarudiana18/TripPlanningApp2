using Microsoft.AspNetCore.Identity;

namespace API.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public DateOnly DateOfBirth { get; set; }
        public string FirstName { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime LastActive { get; set; } = DateTime.UtcNow;
        public string LastName { get; set; }
        public string CityNume { get; set; }
        public int CityId { get; set; }
        public City City {get; set;}
        public string CountryNume { get; set; }
        public List<Photo> Photos { get; set; } = new();
        public List<Review> Reviews { get; set; } = new();
        public ICollection<AppUserRole> UserRoles { get; set; }
    }
}