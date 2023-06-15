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
    public class AtractiiTuristiceController : BaseApiController
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;
        public AtractiiTuristiceController(IUnitOfWork uow, IMapper mapper, IPhotoService photoService)
        {
            _uow = uow;
            _mapper = mapper;
            _photoService = photoService;
        }

        [HttpGet]
        public async Task<ActionResult<List<AtractieTuristica>>> GetAllAtractiiTuristice()
        {
            List<AtractieTuristica> listaAtractiiTuristice = new List<AtractieTuristica>();

            listaAtractiiTuristice = await _uow.AtractieTuristicaRepository.GetAll();

            return Ok(listaAtractiiTuristice);
        }

        [HttpGet("getAtractieById/{atractieId}")]
        public async Task<ActionResult<AtractieTuristicaDto>> GetAtractiiTuristicaById(int atractieId)
        {
            AtractieTuristicaDto atractieTuristicaDto = new AtractieTuristicaDto();

            var atractieTuristica = _uow.AtractieTuristicaRepository.GetAtractieTuristica(atractieId);

            atractieTuristicaDto = _mapper.Map<AtractieTuristicaDto>(atractieTuristica);

            return Ok(atractieTuristicaDto);
        }

        [HttpGet("getAtractieByCityId/{CityId}")]
        public async Task<ActionResult<List<AtractieTuristicaDto>>> GetAllAtractiiTuristiceByCityId(int CityId)
        {
            List<AtractieTuristicaDto> listaAtractiiTuristiceDto = new List<AtractieTuristicaDto>();

            var listaAtractiiTuristice = _uow.AtractieTuristicaRepository.GetAllByCityId(CityId);

            listaAtractiiTuristiceDto = _mapper.Map<List<AtractieTuristicaDto>>(listaAtractiiTuristice);

            return Ok(listaAtractiiTuristiceDto);
        }
         
        [HttpPost("addAtractieTuristica")]
        public async Task<ActionResult> AddAtractieTuristica(AtractieTuristicaDto atractieDto,  IFormFile file)
        {
            if (_uow.AtractieTuristicaRepository.VerificaExistentaAtractieTuristica(atractieDto.Nume, atractieDto.CityId)) return BadRequest("Atractia turistica exista deja pentru acest oras");
            
            var result = await _photoService.AddPhotoAsync(file);

            if (result.Error != null) return BadRequest(result.Error.Message);

            var photo = new Photo
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId,
                IsMain = true
            };

            var atractie = _mapper.Map<AtractieTuristica>(atractieDto);
            
            atractie.Photos.Add(photo);

            _uow.AtractieTuristicaRepository.AdaugaAtractieTuristica(atractie);

            if (await _uow.Complete()) 
            {
                return CreatedAtAction(nameof(AddAtractieTuristica), 
                    new {numeAtractie = atractie.Nume}, _mapper.Map<PhotoDto>(photo));
            }
            else{
                return BadRequest("Problem adding atractie turistca");
            }
        }

        [HttpPost("add-photo/{atractieId}")]
        public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file, int atractieId)
        {
            var atractie = _uow.AtractieTuristicaRepository.GetAtractieTuristica(atractieId);

            if (atractie == null) return NotFound();

            var result = await _photoService.AddPhotoAsync(file);

            if (result.Error != null) return BadRequest(result.Error.Message);

            var photo = new Photo
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId
            };

            if (atractie.Photos.Count == 0) photo.IsMain = true;

            atractie.Photos.Add(photo);

            if (await _uow.Complete()) 
            {
                return CreatedAtAction(nameof(AddPhoto), 
                    new {numeAtractie = atractie.Nume}, _mapper.Map<PhotoDto>(photo));
            }

            return BadRequest("Problem adding photo");
        }
        
    }
}