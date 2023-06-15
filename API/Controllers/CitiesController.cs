using System.Net.Http.Headers;
using API.Entities;
using API.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace API.Controllers
{
    public class CitiesController : BaseApiController
    {
        private readonly IUnitOfWork _uow;
        public CitiesController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet]
        public ActionResult<List<City>> GetAllCities()
        {
            List<City> listaCities = new List<City>();

            listaCities = _uow.CityRepository.GetAll();

            return Ok(listaCities);
        }
        [HttpGet("getCitiesByCountryId/{countryId}")]
        public ActionResult<List<City>> GetAllCitiesByCountryId(int countryId)
        {
            List<City> listaCities = new List<City>();

            listaCities = _uow.CityRepository.GetAllByCountryId(countryId);

            return Ok(listaCities);
        }
        [HttpGet("getCitiesByStateId/{stateId}")]
        public ActionResult<List<City>> GetAllCitiesByStateId(int stateId)
        {
            List<City> listaCities = new List<City>();

            listaCities = _uow.CityRepository.GetAllByStateId(stateId);

            return Ok(listaCities);
        }
    }
}