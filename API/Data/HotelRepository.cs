using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    internal class HotelRepository : IHotelRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public HotelRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public void AdaugaHotel(Hotel Hotel)
        {
            _context.Hoteluri.Add(Hotel);
        }

        public void AdaugaListaHoteluri(List<Hotel> listaHoteluri)
        {
            _context.Hoteluri.AddRange(listaHoteluri);
        }

        public void UpdateHotel(Hotel hotel)
        {
            _context.Hoteluri.Update(hotel);
        }

        public Hotel GetHotel(int hotelId)
        {
           return _context.Hoteluri.Where(x=> x.Id == hotelId).Include(p=> p.Photos).Include(r=> r.Reviews).ThenInclude(y=> y.CreatedByUser).FirstOrDefault();
        }
        public List<Hotel> GetAll()
        {
            return _context.Hoteluri.Include(p=> p.Photos).Include(r=> r.Reviews).ToList();
        }
        public List<Hotel> GetAllByCityId(int cityId)
        {
            return _context.Hoteluri.Where(x=> x.CityId == cityId).Include(p=> p.Photos).Include(r=> r.Reviews).ToList();
        }
        public bool VerificaExistentaHotel(string numeHotel, int CityId)
        {
            return _context.Hoteluri.Any(x=> x.Nume == numeHotel && x.CityId == CityId);
        }
    }
}
