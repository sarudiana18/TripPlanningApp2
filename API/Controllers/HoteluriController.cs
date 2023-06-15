using System.Net.Http.Headers;
using API.Entities;
using API.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Runtime.CompilerServices;
using API.DTOs;
using AutoMapper;

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
        [HttpGet("getHotelById/{hotelId}")]
        public async Task<ActionResult<List<HotelDto>>> GetHotelById(int hotelId)
        {
            HotelDto hotelDto = new HotelDto();

            var hotel = _uow.HotelRepository.GetHotel(hotelId);
        
            hotelDto = _mapper.Map<HotelDto>(hotel);
            return Ok(hotelDto);
        }

        [HttpPost("addHotel")]
        public async Task<ActionResult> AddHotel(HotelDto hotelDto,  IFormFile file)
        {
            if (_uow.HotelRepository.VerificaExistentaHotel(hotelDto.Nume, hotelDto.CityId)) return BadRequest("Atractia turistica exista deja pentru acest oras");
            
            var result = await _photoService.AddPhotoAsync(file);

            if (result.Error != null) return BadRequest(result.Error.Message);

            var photo = new Photo
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId,
                IsMain = true
            };

            var hotel = _mapper.Map<Hotel>(hotelDto);
            
            hotel.Photos.Add(photo);

            _uow.HotelRepository.AdaugaHotel(hotel);

            if (await _uow.Complete()) 
            {
                return CreatedAtAction(nameof(AddHotel), 
                    new {numeHotel = hotel.Nume}, _mapper.Map<PhotoDto>(photo));
            }
            else{
                return BadRequest("Problem adding hotel turistca");
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

            return BadRequest("Problem adding photo");
        }
    }
}