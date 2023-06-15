using System.Net.Http.Headers;
using API.Entities;
using API.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace API.Controllers
{
    public class CountriesController : BaseApiController
    {
        private readonly IUnitOfWork _uow;
        public CountriesController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<List<Country>>> GetAllCountries()
        {
             List<Country> listaTari = new List<Country>();

            listaTari = _uow.CountryRepository.GetAll();

            return Ok(listaTari);
        }
    }
}