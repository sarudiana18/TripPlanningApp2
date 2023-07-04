using System.Net.Http.Headers;
using API.Entities;
using API.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using AutoMapper;
using Azure;
using API.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;

namespace API.Controllers
{
    public class ReviewController : BaseApiController
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public ReviewController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<Review>>> GetAllReviews()
        {
            List<Review> listaReview = new List<Review>();

            listaReview = _uow.ReviewsRepository.GetAll();

            return Ok(listaReview);
        }

        [HttpGet("getByHotelId/{hotelId}")]
        public async Task<ActionResult<List<ReviewDto>>> GetAllReviewByHotelId(int hotelId)
        {
            List<ReviewDto> listaReviewDto = new List<ReviewDto>();

            var listaReview = _uow.ReviewsRepository.GetAllByHotelId(hotelId);
            listaReviewDto = _mapper.Map<List<ReviewDto>>(listaReview);

            return Ok(listaReviewDto);
        }
        [HttpGet("getByParcId/{parcId}")]
        public async Task<ActionResult<List<ReviewDto>>> GetAllReviewByParcId(int parcId)
        {
            List<ReviewDto> listaReviewDto = new List<ReviewDto>();

            var listaReview = _uow.ReviewsRepository.GetAllByParcId(parcId);
            listaReviewDto = _mapper.Map<List<ReviewDto>>(listaReview);

            return Ok(listaReviewDto);
        }
        [HttpGet("getByRestaurantId/{restaurantId}")]
        public async Task<ActionResult<List<ReviewDto>>> GetAllReviewByRestaurantId(int restaurantId)
        {
            List<ReviewDto> listaReviewDto = new List<ReviewDto>();

            var listaReview = _uow.ReviewsRepository.GetAllByRestaurantId(restaurantId);
            listaReviewDto = _mapper.Map<List<ReviewDto>>(listaReview);

            return Ok(listaReviewDto);
        }
         
        [HttpPost("addReview")]
        public async Task<ActionResult> AddReview(ReviewDto reviewDto)
        {  
            var review = _mapper.Map<Review>(reviewDto);
            
            _uow.ReviewsRepository.AdaugaReview(review);

            //update nota hotel
            if(reviewDto.HotelId.HasValue && reviewDto.HotelId.Value > 0){

                var hotel = _uow.HotelRepository.GetHotel(reviewDto.HotelId.Value);
                if(hotel == null){
                    return BadRequest("Hotelul cu id-ul "+reviewDto.HotelId.ToString()+ ", trimis in request, nu a fost gasit in baza de date");
                }

                hotel.Rating = Queryable.Average((hotel.Reviews.Select(x=> x.Nota).ToList().AsQueryable()));
                _uow.HotelRepository.UpdateHotel(hotel);
          
            }
            //update nota restaurant
            if(reviewDto.RestaurantId.HasValue && reviewDto.RestaurantId.Value > 0){

                var restaurant = _uow.RestaurantRepository.GetRestaurant(reviewDto.RestaurantId.Value);
                if(restaurant == null){
                    return BadRequest("Restaurantul cu id-ul "+reviewDto.RestaurantId.ToString()+ ", trimis in request, nu a fost gasit in baza de date");
                }

                restaurant.Rating = Queryable.Average((restaurant.Reviews.Select(x=> x.Nota).ToList().AsQueryable()));
                _uow.RestaurantRepository.UpdateRestaurant(restaurant);
          
            }
            //update nota parc
            if(reviewDto.ParcId.HasValue && reviewDto.ParcId.Value > 0){

                var parc = _uow.ParcRepository.GetParc(reviewDto.ParcId.Value);
                if(parc == null){
                    return BadRequest("Parcul cu id-ul "+reviewDto.ParcId.ToString()+ ", trimis in request, nu a fost gasit in baza de date");
                }

                parc.Rating = Queryable.Average((parc.Reviews.Select(x=> x.Nota).ToList().AsQueryable()));
                _uow.ParcRepository.UpdateParc(parc);
          
            }
            

            if (await _uow.Complete()) 
            {
                return Ok();
            }
            else{
                return BadRequest("Eroare la adaugarea recenziei");
            }
        }
    }
}