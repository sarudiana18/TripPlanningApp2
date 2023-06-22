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
            return _context.Hoteluri.Include(p=> p.Photos).Include(r=> r.Reviews).ThenInclude(y=> y.CreatedByUser).ToList();
        }
        public List<Hotel> GetAllByCityId(int cityId)
        {
            return _context.Hoteluri.Where(x=> x.CityId == cityId).Include(p=> p.Photos).Include(r=> r.Reviews).ThenInclude(y=> y.CreatedByUser).ToList();
        }
        public List<Hotel> GetAllByCityIdAndBugetAndNrNoptiAndNrPersoane(int cityId, int buget, int nrNopti, int nrPersoane)
        {
            //camere duble sau triple
            var nrCamereDuble = 0;
            var nrCamereTriple = 0;
            if(nrPersoane >0 && nrPersoane%2 ==0){
                nrCamereDuble = nrPersoane/2;
            }
            else if( nrPersoane >1){
                nrCamereTriple = 1;
                nrCamereDuble = (nrPersoane-3)/2; 
                return _context.Hoteluri.Where(x=> x.CityId == cityId && 
                (x.PricePerNight <= (!x.PricePerNightCameraTripla.HasValue || x.PricePerNightCameraTripla.Value==0? buget/nrNopti/nrCamereDuble+1:
                (buget - x.PricePerNightCameraTripla)/nrNopti/nrCamereDuble)))
                .Include(p=> p.Photos).Include(r=> r.Reviews).ThenInclude(y=> y.CreatedByUser).ToList();
            }
            else if(nrPersoane == 1){
                nrCamereDuble = 1;
            }

            return _context.Hoteluri.Where(x=> x.CityId == cityId && 
            (x.PricePerNight <= buget/nrNopti/nrCamereDuble)).Include(p=> p.Photos).Include(r=> r.Reviews).ThenInclude(y=> y.CreatedByUser).ToList();
        }
        public bool VerificaExistentaHotel(string numeHotel, int CityId)
        {
            return _context.Hoteluri.Any(x=> x.Nume == numeHotel && x.CityId == CityId);
        }
        public Hotel GetHotelByPhotoId(int photoId)
        {
           return _context.Hoteluri.Where(x=> x.Photos.Any(y=> y.Id == photoId)).Include(p=> p.Photos).Include(r=> r.Reviews).ThenInclude(y=> y.CreatedByUser).FirstOrDefault();
        }
    }
}
