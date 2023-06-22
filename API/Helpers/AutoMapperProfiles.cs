using API.DTOs;
using API.Entities;
using API.Extensions;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, MemberDto>()
                .ForMember(dest => dest.PhotoUrl, 
                    opt => opt.MapFrom(src => src.Photos.FirstOrDefault(x => x.IsMain).Url))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalcuateAge()));
            CreateMap<Photo, PhotoDto>();
            CreateMap<MemberUpdateDto, AppUser>();
            CreateMap<RegisterDto, AppUser>()
                .ForMember(x=> x.City, y => y.MapFrom(z=> z.City.Name))
                .ForMember(x=> x.Country, y => y.MapFrom(z=> z.Country.Name));
            CreateMap<DateTime, DateTime>().ConvertUsing(d => DateTime.SpecifyKind(d, DateTimeKind.Utc));
            CreateMap<DateTime?, DateTime?>().ConvertUsing(d => d.HasValue ? 
                DateTime.SpecifyKind(d.Value, DateTimeKind.Utc) : null);

            CreateMap<AtractieTuristica, AtractieTuristicaDto>()
                .ForMember(dest => dest.PhotoUrl, 
                    opt => opt.MapFrom(src => src.Photos.FirstOrDefault(x => x.IsMain).Url));
            CreateMap<AtractieTuristicaDto, AtractieTuristica>();
            CreateMap<Hotel, HotelDto>()
                .ForMember(dest => dest.PhotoUrl, 
                    opt => opt.MapFrom(src => src.Photos.FirstOrDefault(x => x.IsMain).Url));
            CreateMap<HotelDto, Hotel>();

            CreateMap<Restaurant, RestaurantDto>()
                .ForMember(dest => dest.PhotoUrl, 
                    opt => opt.MapFrom(src => src.Photos.FirstOrDefault(x => x.IsMain).Url));
            CreateMap<RestaurantDto, Restaurant>();

            CreateMap<Parc, ParcDto>()
                .ForMember(dest => dest.PhotoUrl, 
                    opt => opt.MapFrom(src => src.Photos.FirstOrDefault(x => x.IsMain).Url));
            CreateMap<ParcDto, Parc>();


            CreateMap<Review, ReviewDto>()
                .ForMember(dest => dest.CreatedByNume, 
                    opt => opt.MapFrom(src => src.CreatedByUser.LastName +" "+ src.CreatedByUser.FirstName));
            CreateMap<ReviewDto, Review>();
        }
    }
}