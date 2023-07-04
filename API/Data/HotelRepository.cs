using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
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
            else if(nrPersoane == 1){
                nrCamereDuble = 1;
            }
            else if( nrPersoane >= 3){
                nrCamereTriple = 1;
                nrCamereDuble = (nrPersoane-3)/2;
                if(nrCamereDuble == 0 && nrCamereTriple == 1 && nrPersoane ==3){
                    return _context.Hoteluri.Where(x=> x.CityId == cityId && 
                    x.PricePerNightCameraTripla <= buget/nrNopti)
                    .Include(p=> p.Photos).Include(r=> r.Reviews).ThenInclude(y=> y.CreatedByUser).ToList();
                }
                return _context.Hoteluri.Where(x=> x.CityId == cityId && 
                (x.PricePerNight <= (!x.PricePerNightCameraTripla.HasValue || x.PricePerNightCameraTripla.Value==0? buget/nrNopti/(nrCamereDuble+2):
                (buget - x.PricePerNightCameraTripla)/nrNopti/nrCamereDuble)))
                .Include(p=> p.Photos).Include(r=> r.Reviews).ThenInclude(y=> y.CreatedByUser).ToList();
            }

            var hotels = new List<Hotel>();
            if(buget>0){
                hotels = _context.Hoteluri.Where(x=> x.CityId == cityId && 
            (x.PricePerNight <= buget/nrNopti/nrCamereDuble)).Include(p=> p.Photos).Include(r=> r.Reviews).ThenInclude(y=> y.CreatedByUser).ToList();
            }
            else{
                hotels = _context.Hoteluri.Where(x=> x.CityId == cityId).Include(p=> p.Photos).Include(r=> r.Reviews).ThenInclude(y=> y.CreatedByUser).ToList();
            }

            return hotels;
        }
        public bool VerificaExistentaHotel(string numeHotel, int CityId)
        {
            return _context.Hoteluri.Any(x=> x.Nume == numeHotel && x.CityId == CityId);
        }
        public Hotel GetHotelByPhotoId(int photoId)
        {
           return _context.Hoteluri.Where(x=> x.Photos.Any(y=> y.Id == photoId)).Include(p=> p.Photos).Include(r=> r.Reviews).ThenInclude(y=> y.CreatedByUser).FirstOrDefault();
        }

        public async Task<PagedList<HotelDto>> GetHotelsAsync(HotelFilterDto hotelParams)
        {
            var hoteluri = new List<Hotel>();
            if(!hotelParams.CityId.HasValue || hotelParams.CityId.Value == 0){
                hoteluri = this.GetAll();
            }
            else {
                hoteluri = this.GetAllByCityIdAndBugetAndNrNoptiAndNrPersoane(hotelParams.CityId.Value, hotelParams.Buget,
                            hotelParams.NrNopti, hotelParams.NrPersoane);
            }
           
            var query = hoteluri.AsQueryable();

            if( !string.IsNullOrEmpty(hotelParams.Nume) && hotelParams.Nume != "null"){
                query = query.Where(u => u.Nume.ToLower().Contains(hotelParams.Nume.ToLower()));
            }
            if( !string.IsNullOrEmpty(hotelParams.Adresa) && hotelParams.Adresa != "null"){
                query = query.Where(u => u.Adresa.ToLower().Contains(hotelParams.Adresa.ToLower()));
            }
            if(hotelParams.Rating.HasValue){
                query = query.Where(u => u.Rating >= hotelParams.Rating);
            }
            query = hotelParams.SortField switch
            {
                "nume" => query.OrderBy(u => u.Nume),
                "rating" => query.OrderByDescending(u => u.Rating),
                "pricePerNight" => query.OrderBy(u => u.PricePerNight),
                _ => query.OrderByDescending(u => u.Nume)
            };

            var hoteluriDto = _mapper.Map<IList<HotelDto>>(query);

            return await PagedList<HotelDto>.CreateAsync(
                hoteluriDto.AsQueryable(), 
                hotelParams.PageNumber, 
                hotelParams.PageSize);

        }
        public void Delete(Hotel obiect)
        {
            _context.Hoteluri.Remove(obiect);
        }
    }
}
