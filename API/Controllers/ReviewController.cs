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
        public async Task<ActionResult<List<Review>>> GetAllReview()
        {
            List<Review> listaReview = new List<Review>();

            listaReview = _uow.ReviewsRepository.GetAll();

            return Ok(listaReview);
        }

        [HttpGet("{hotelId}")]
        public async Task<ActionResult<List<ReviewDto>>> GetAllReviewByHotelId(int hotelId)
        {
            List<ReviewDto> listaReviewDto = new List<ReviewDto>();

            var listaReview = _uow.ReviewsRepository.GetAllByHotelId(hotelId);
            listaReviewDto = _mapper.Map<List<ReviewDto>>(listaReview);

            return Ok(listaReviewDto);
        }
         
        [HttpPost("addReview")]
        public async Task<ActionResult> AddReview(ReviewDto reviewDto)
        {            
            var review = _mapper.Map<Review>(reviewDto);
            
            _uow.ReviewsRepository.AdaugaReview(review);

            if (await _uow.Complete()) 
            {
                return Ok();
            }
            else{
                return BadRequest("Problem adding atractie turistca");
            }
        }
    }
}