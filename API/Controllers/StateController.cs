using System.Net.Http.Headers;
using API.Entities;
using API.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace API.Controllers
{
    public class StatesController : BaseApiController
    {
        private readonly IUnitOfWork _uow;
        public StatesController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet]
        public ActionResult<List<State>> GetAllStates()
        {
            List<State> listaStates = new List<State>();

            listaStates = _uow.StateRepository.GetAll();

            return Ok(listaStates);
        }
        [HttpGet("getStatesByCountryId/{countryId}")]
        public ActionResult<List<State>> GetAllStatesByCountryId(int countryId)
        {
            List<State> listaStates = new List<State>();

            listaStates = _uow.StateRepository.GetAllByCountryId(countryId);

            return Ok(listaStates);
        }
    }
}