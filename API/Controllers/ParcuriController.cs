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
    public class ParcuriController : BaseApiController
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;
        public ParcuriController(IUnitOfWork uow, IMapper mapper, IPhotoService photoService)
        {
            _uow = uow;
            _mapper = mapper;
            _photoService = photoService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedList<ParcDto>>> GetParcuri([FromQuery]ParcFilterDto modelParams)
        {
            
            var modelsDto = await _uow.ParcRepository.GetParcuriAsync(modelParams);

            Response.AddPaginationHeader(new PaginationHeader(modelsDto.CurrentPage, modelsDto.PageSize, 
                modelsDto.TotalCount, modelsDto.TotalPages));

            return Ok(modelsDto);
        }

        [HttpDelete("{objectId}")]
        public async Task<ActionResult> Delete(int objectId)
        {
            var parc = _uow.ParcRepository.GetParc(objectId);
            if (parc == null) return NotFound();

            _uow.ParcRepository.Delete(parc);

            if (await _uow.Complete()) return Ok();

            return BadRequest("Problem deleting photo");
        }

        [HttpGet("getAllParcuri")]
        public async Task<ActionResult<List<Parc>>> GetAllParcuri()
        {
            List<Parc> listaParcuri = new List<Parc>();

            listaParcuri = _uow.ParcRepository.GetAll();

            return Ok(listaParcuri);
        }

        [HttpGet("getParcById/{parcId}")]
        public async Task<ActionResult<ParcDto>> GetAtractiiTuristicaById(int parcId)
        {
            ParcDto parcDto = new ParcDto();

            var parc = _uow.ParcRepository.GetParc(parcId);

            parcDto = _mapper.Map<ParcDto>(parc);

            return Ok(parcDto);
        }

        [HttpGet("getParcByCityId/{CityId}")]
        public async Task<ActionResult<List<ParcDto>>> GetAllParcuriByCityId(int CityId)
        {
            List<ParcDto> listaParcuriDto = new List<ParcDto>();

            var listaParcuri = _uow.ParcRepository.GetAllByCityId(CityId);

            listaParcuriDto = _mapper.Map<List<ParcDto>>(listaParcuri);

            return Ok(listaParcuriDto);
        }
         
        [HttpPost("addParc")]
        public async Task<ActionResult> AddParc(ParcDto parcDto)
        {
            if (_uow.ParcRepository.VerificaExistentaParc(parcDto.Nume, parcDto.CityId)) return BadRequest("Atractia turistica exista deja pentru acest oras");

            var parc = _mapper.Map<Parc>(parcDto);
            
            _uow.ParcRepository.AdaugaParc(parc);
             if (await _uow.Complete()) 
            {
                return Ok(parc);
            }
            else{
                return BadRequest("Problem adding hotel turistca");
            }
        }

        [HttpPost("add-photo/{parcId}")]
        public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file, int parcId)
        {
            var parc = _uow.ParcRepository.GetParc(parcId);

            if (parc == null) return NotFound();

            var result = await _photoService.AddPhotoAsync(file);

            if (result.Error != null) return BadRequest(result.Error.Message);

            var photo = new Photo
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId
            };

            if (parc.Photos.Count == 0) photo.IsMain = true;

            parc.Photos.Add(photo);

            if (await _uow.Complete()) 
            {
                return CreatedAtAction(nameof(AddPhoto), 
                    new {numeParc = parc.Nume}, _mapper.Map<PhotoDto>(photo));
            }

            return BadRequest("Problem adding photo");
        }

        
        [HttpPut("set-main-photo/{photoId}")]
        public async Task<ActionResult> SetMainPhoto(int photoId)
        {

            var parc = _uow.ParcRepository.GetParcByPhotoId(photoId);
            if (parc == null) return NotFound();

            var photo = parc.Photos.FirstOrDefault(x => x.Id == photoId);

            if (photo == null) return NotFound();

            if (photo.IsMain) return BadRequest("Aceasta poza este deja setata ca poza principala");

            var currentMain = parc.Photos.FirstOrDefault(x => x.IsMain);
            if (currentMain != null) currentMain.IsMain = false;
            photo.IsMain = true;

            if (await _uow.Complete()) return NoContent();

            return BadRequest("Problem setting the main photo");
        }

         [HttpDelete("delete-photo/{photoId}")]
        public async Task<ActionResult> DeletePhoto(int photoId)
        {
            var parc = _uow.ParcRepository.GetParcByPhotoId(photoId);
            if (parc == null) return NotFound();

            var photo = parc.Photos.FirstOrDefault(x => x.Id == photoId);

            if (photo == null) return NotFound();

            if (photo.IsMain) return BadRequest("Nu puteti sterge poza principala!");

            if (photo.PublicId != null)
            {
                var result = await _photoService.DeletePhotoAsync(photo.PublicId);
                if (result.Error != null) return BadRequest(result.Error.Message);
            }

            parc.Photos.Remove(photo);

            if (await _uow.Complete()) return Ok();

            return BadRequest("Problem deleting photo");
        }
        
    }
}