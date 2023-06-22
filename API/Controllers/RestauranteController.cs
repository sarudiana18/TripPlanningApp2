using System.Net.Http.Headers;
using API.Entities;
using API.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using AutoMapper;
using Azure;
using API.DTOs;

namespace API.Controllers
{
    public class RestauranteController : BaseApiController
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;
        public RestauranteController(IUnitOfWork uow, IMapper mapper, IPhotoService photoService)
        {
            _uow = uow;
            _mapper = mapper;
            _photoService = photoService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Restaurant>>> GetAllRestaurante()
        {
            List<Restaurant> listaRestaurante = new List<Restaurant>();

            listaRestaurante = _uow.RestaurantRepository.GetAll();

            return Ok(listaRestaurante);
        }

        [HttpGet("getRestaurantById/{restaurantId}")]
        public async Task<ActionResult<RestaurantDto>> GetAtractiiTuristicaById(int restaurantId)
        {
            RestaurantDto restaurantDto = new RestaurantDto();

            var restaurant = _uow.RestaurantRepository.GetRestaurant(restaurantId);

            restaurantDto = _mapper.Map<RestaurantDto>(restaurant);

            return Ok(restaurantDto);
        }

        [HttpGet("getRestaurantByCityId/{CityId}")]
        public async Task<ActionResult<List<RestaurantDto>>> GetAllRestauranteByCityId(int CityId)
        {
            List<RestaurantDto> listaRestauranteDto = new List<RestaurantDto>();

            var listaRestaurante = _uow.RestaurantRepository.GetAllByCityId(CityId);

            listaRestauranteDto = _mapper.Map<List<RestaurantDto>>(listaRestaurante);

            return Ok(listaRestauranteDto);
        }
         
        [HttpPost("addRestaurant")]
        public async Task<ActionResult> AddRestaurant(RestaurantDto restaurantDto)
        {
            if (_uow.RestaurantRepository.VerificaExistentaRestaurant(restaurantDto.Nume, restaurantDto.CityId)) return BadRequest("Atractia turistica exista deja pentru acest oras");

            var restaurant = _mapper.Map<Restaurant>(restaurantDto);
            
            _uow.RestaurantRepository.AdaugaRestaurant(restaurant);
             if (await _uow.Complete()) 
            {
                return Ok(restaurant);
            }
            else{
                return BadRequest("Problem adding restaurant");
            }
        }

        [HttpPost("add-photo/{restaurantId}")]
        public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file, int restaurantId)
        {
            var restaurant = _uow.RestaurantRepository.GetRestaurant(restaurantId);

            if (restaurant == null) return NotFound();

            var result = await _photoService.AddPhotoAsync(file);

            if (result.Error != null) return BadRequest(result.Error.Message);

            var photo = new Photo
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId
            };

            if (restaurant.Photos.Count == 0) photo.IsMain = true;

            restaurant.Photos.Add(photo);

            if (await _uow.Complete()) 
            {
                return CreatedAtAction(nameof(AddPhoto), 
                    new {numeRestaurant = restaurant.Nume}, _mapper.Map<PhotoDto>(photo));
            }

            return BadRequest("Problem adding photo");
        }
        
        
         [HttpPut("set-main-photo/{photoId}")]
        public async Task<ActionResult> SetMainPhoto(int photoId)
        {

            var restaurant = _uow.RestaurantRepository.GetRestaurantByPhotoId(photoId);
            if (restaurant == null) return NotFound();

            var photo = restaurant.Photos.FirstOrDefault(x => x.Id == photoId);

            if (photo == null) return NotFound();

            if (photo.IsMain) return BadRequest("Aceasta poza este deja setata ca poza principala");

            var currentMain = restaurant.Photos.FirstOrDefault(x => x.IsMain);
            if (currentMain != null) currentMain.IsMain = false;
            photo.IsMain = true;

            if (await _uow.Complete()) return NoContent();

            return BadRequest("Problem setting the main photo");
        }

         [HttpDelete("delete-photo/{photoId}")]
        public async Task<ActionResult> DeletePhoto(int photoId)
        {
            var restaurant = _uow.RestaurantRepository.GetRestaurantByPhotoId(photoId);
            if (restaurant == null) return NotFound();

            var photo = restaurant.Photos.FirstOrDefault(x => x.Id == photoId);

            if (photo == null) return NotFound();

            if (photo.IsMain) return BadRequest("Nu puteti sterge poza principala!");

            if (photo.PublicId != null)
            {
                var result = await _photoService.DeletePhotoAsync(photo.PublicId);
                if (result.Error != null) return BadRequest(result.Error.Message);
            }

            restaurant.Photos.Remove(photo);

            if (await _uow.Complete()) return Ok();

            return BadRequest("Problem deleting photo");
        }
    }
}