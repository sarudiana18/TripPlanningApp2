using System.Net.Http.Headers;
using API.Entities;
using API.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using AutoMapper;
using Azure;
using API.DTOs;
using API.Helpers;
using API.Extensions;

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
        public async Task<ActionResult<PagedList<AtractieTuristicaDto>>> GetAtractii([FromQuery]AtractieTuristicaFilterDto modelParams)
        {
            
            var modelsDto = await _uow.AtractieTuristicaRepository.GetAtractiiAsync(modelParams);

            Response.AddPaginationHeader(new PaginationHeader(modelsDto.CurrentPage, modelsDto.PageSize, 
                modelsDto.TotalCount, modelsDto.TotalPages));

            return Ok(modelsDto);
        }

        [HttpGet("getAllAtractiiTuristice")]
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
        public async Task<ActionResult> AddAtractieTuristica(AtractieTuristicaDto atractieDto)
        {
            if (_uow.AtractieTuristicaRepository.VerificaExistentaAtractieTuristica(atractieDto.Nume, atractieDto.CityId)) return BadRequest("Atractia turistica exista deja pentru acest oras");

            var atractie = _mapper.Map<AtractieTuristica>(atractieDto);
            
            try{
                
                atractie = await  _uow.AtractieTuristicaRepository.AdaugaAtractieTuristica(atractie);
                return Ok(atractie);
            }
            catch{
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
         [HttpPut("set-main-photo/{photoId}")]
        public async Task<ActionResult> SetMainPhoto(int photoId)
        {

            var atractie = _uow.AtractieTuristicaRepository.GetAtractieByPhotoId(photoId);
            if (atractie == null) return NotFound();

            var photo = atractie.Photos.FirstOrDefault(x => x.Id == photoId);

            if (photo == null) return NotFound();

            if (photo.IsMain) return BadRequest("Aceasta poza este deja setata ca poza principala");

            var currentMain = atractie.Photos.FirstOrDefault(x => x.IsMain);
            if (currentMain != null) currentMain.IsMain = false;
            photo.IsMain = true;

            if (await _uow.Complete()) return NoContent();

            return BadRequest("Problem setting the main photo");
        }

        [HttpDelete("delete-photo/{photoId}")]
        public async Task<ActionResult> DeletePhoto(int photoId)
        {
            var atractie = _uow.AtractieTuristicaRepository.GetAtractieByPhotoId(photoId);
            if (atractie == null) return NotFound();

            var photo = atractie.Photos.FirstOrDefault(x => x.Id == photoId);

            if (photo == null) return NotFound();

            if (photo.IsMain) return BadRequest("Nu puteti sterge poza principala!");

            if (photo.PublicId != null)
            {
                var result = await _photoService.DeletePhotoAsync(photo.PublicId);
                if (result.Error != null) return BadRequest(result.Error.Message);
            }

            atractie.Photos.Remove(photo);

            if (await _uow.Complete()) return Ok();

            return BadRequest("Problem deleting photo");
        }

        [HttpDelete("{objectId}")]
        public async Task<ActionResult> Delete(int objectId)
        {
            var atractie = _uow.AtractieTuristicaRepository.GetAtractieTuristica(objectId);
            if (atractie == null) return NotFound();

            _uow.AtractieTuristicaRepository.Delete(atractie);

            if (await _uow.Complete()) return Ok();

            return BadRequest("Problem deleting photo");
        }
        
    }
}