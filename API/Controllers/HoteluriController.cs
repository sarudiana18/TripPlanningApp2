using System.Net.Http.Headers;
using API.Entities;
using API.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Runtime.CompilerServices;
using API.DTOs;
using AutoMapper;
using API.Helpers;
using API.Extensions;

namespace API.Controllers
{
    public class HoteluriController : BaseApiController
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;
        public HoteluriController(IUnitOfWork uow, IMapper mapper, IPhotoService photoService)
        {
            _uow = uow;
            _mapper = mapper;
            _photoService = photoService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedList<HotelDto>>> GetHotels([FromQuery]HotelFilterDto hotelParams)
        {
            
            var hotels = await _uow.HotelRepository.GetHotelsAsync(hotelParams);

            Response.AddPaginationHeader(new PaginationHeader(hotels.CurrentPage, hotels.PageSize, 
                hotels.TotalCount, hotels.TotalPages));

            return Ok(hotels);
        }

        [HttpGet("getAllHotels")]
        public async Task<ActionResult<List<Hotel>>> GetAllHoteluri()
        {
            List<Hotel> listaHoteluri = new List<Hotel>();

            listaHoteluri = _uow.HotelRepository.GetAll();
        
            return Ok(listaHoteluri);
        }

        [HttpGet("getHotelByCityId/{CityId}")]
        public async Task<ActionResult<List<HotelDto>>> GetAllHoteluriByCityId(int CityId)
        {
            List<HotelDto> listaHoteluriDto = new List<HotelDto>();

            var listaHoteluri = _uow.HotelRepository.GetAllByCityId(CityId);
            
            listaHoteluriDto = _mapper.Map<List<HotelDto>>(listaHoteluri);

            return Ok(listaHoteluriDto);
        }
        [HttpGet("getHotelByCityIdAndBugetAndNrNoptiAndNrPersoane/{CityId}/{Buget}/{NrNopti}/{NrPersoane}")]
        public async Task<ActionResult<List<HotelDto>>> GetAllHoteluriByCityIdAndBugetAndNrNoptiAndNrPersoane(int CityId,
        int buget, int nrNopti, int nrPersoane)
        {
            List<HotelDto> listaHoteluriDto = new List<HotelDto>();

            var listaHoteluri = _uow.HotelRepository.GetAllByCityIdAndBugetAndNrNoptiAndNrPersoane(CityId, buget, nrNopti, nrPersoane);
            
            listaHoteluriDto = _mapper.Map<List<HotelDto>>(listaHoteluri);

            return Ok(listaHoteluriDto);
        }
        
        [HttpGet("getHotelById/{hotelId}")]
        public async Task<ActionResult<List<HotelDto>>> GetHotelById(int hotelId)
        {
            HotelDto hotelDto = new HotelDto();

            var hotel = _uow.HotelRepository.GetHotel(hotelId);
        
            hotelDto = _mapper.Map<HotelDto>(hotel);
            return Ok(hotelDto);
        }

        [HttpPost("addHotel")]
        public async Task<ActionResult> AddHotel(HotelDto hotelDto)
        {
            if (_uow.HotelRepository.VerificaExistentaHotel(hotelDto.Nume, hotelDto.CityId)) return BadRequest("Hotelul introdus exista deja pentru acest oras");
            
            var hotel = _mapper.Map<Hotel>(hotelDto);

            _uow.HotelRepository.AdaugaHotel(hotel);

            if (await _uow.Complete()) 
            {
                return Ok(hotel);
            }
            else{
                return BadRequest("Eroare la adaugarea hotelului");
            }
        }

        [HttpPost("add-photo/{hotelId}")]
        public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file, int hotelId)
        {
            var hotel = _uow.HotelRepository.GetHotel(hotelId);

            if (hotel == null) return NotFound();

            var result = await _photoService.AddPhotoAsync(file);

            if (result.Error != null) return BadRequest(result.Error.Message);

            var photo = new Photo
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId
            };

            if (hotel.Photos.Count == 0) photo.IsMain = true;

            hotel.Photos.Add(photo);

            if (await _uow.Complete()) 
            {
                return CreatedAtAction(nameof(AddPhoto), 
                    new {numeHotel = hotel.Nume}, _mapper.Map<PhotoDto>(photo));
            }

            return BadRequest("Eroare la adaugarea pozei");
        }
         [HttpPut("set-main-photo/{photoId}")]
        public async Task<ActionResult> SetMainPhoto(int photoId)
        {

            var hotel = _uow.HotelRepository.GetHotelByPhotoId(photoId);
            if (hotel == null) return NotFound();

            var photo = hotel.Photos.FirstOrDefault(x => x.Id == photoId);

            if (photo == null) return NotFound();

            if (photo.IsMain) return BadRequest("Aceasta poza este deja setata ca poza principala");

            var currentMain = hotel.Photos.FirstOrDefault(x => x.IsMain);
            if (currentMain != null) currentMain.IsMain = false;
            photo.IsMain = true;

            if (await _uow.Complete()) return NoContent();

            return BadRequest("Eroare la setarea pozei ca poza principala");
        }

         [HttpDelete("delete-photo/{photoId}")]
        public async Task<ActionResult> DeletePhoto(int photoId)
        {
            var hotel = _uow.HotelRepository.GetHotelByPhotoId(photoId);
            if (hotel == null) return NotFound();

            var photo = hotel.Photos.FirstOrDefault(x => x.Id == photoId);

            if (photo == null) return NotFound();

            if (photo.IsMain) return BadRequest("Nu puteti sterge poza principala!");

            if (photo.PublicId != null)
            {
                var result = await _photoService.DeletePhotoAsync(photo.PublicId);
                if (result.Error != null) return BadRequest(result.Error.Message);
            }

            hotel.Photos.Remove(photo);

            if (await _uow.Complete()) return Ok();

            return BadRequest("Eroare la stergerea pozei");
        }

        [HttpDelete("{objectId}")]
        public async Task<ActionResult> Delete(int objectId)
        {
            var hotel = _uow.HotelRepository.GetHotel(objectId);
            if (hotel == null) return NotFound();

            _uow.HotelRepository.Delete(hotel);

            if (await _uow.Complete()) return Ok();

            return BadRequest("Eroare la stergerea hotelului");
        }
        
    }
}