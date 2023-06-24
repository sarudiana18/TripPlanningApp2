using System.Runtime.CompilerServices;
using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    internal class AtractieTuristicaRepository : IAtractieTuristicaRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public AtractieTuristicaRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<AtractieTuristica> AdaugaAtractieTuristica(AtractieTuristica atractie)
        {
           _context.AtractiiTuristice.Add(atractie);
           await _context.SaveChangesAsync();
           return atractie;
        }

        public bool VerificaExistentaAtractieTuristica(string numeAtractie, int CityId)
        {
            return _context.AtractiiTuristice.Any(x=> x.Nume == numeAtractie && x.CityId == CityId);
        }

        public void AdaugaListaAtractiiTuristice(List<AtractieTuristica> listaAtractiiTuristice)
        {
            _context.AtractiiTuristice.AddRange(listaAtractiiTuristice);
        }

        public void UpdateAtractieTuristica(AtractieTuristica atractieTuristica)
        {
            _context.AtractiiTuristice.Update(atractieTuristica);
        }

        public void Delete(AtractieTuristica atractieTuristica)
        {
            _context.AtractiiTuristice.Remove(atractieTuristica);
        }

        public AtractieTuristica GetAtractieTuristica(int atractieTuristicaId)
        {
           return _context.AtractiiTuristice.Where(x=> x.Id == atractieTuristicaId).Include(p=> p.Photos).FirstOrDefault();
        }
        public async Task<List<AtractieTuristica>> GetAll()
        {
            return await _context.AtractiiTuristice.Include(p=> p.Photos).ToListAsync();
        }
        public List<AtractieTuristica> GetAllByCityId(int CityId)
        {
            return _context.AtractiiTuristice.Where(x=> x.CityId == CityId).Include(p=> p.Photos).ToList();
        }
        public AtractieTuristica GetAtractieByPhotoId(int photoId)
        {
           return _context.AtractiiTuristice.Where(x=> x.Photos.Any(y=> y.Id == photoId)).Include(p=> p.Photos).FirstOrDefault();
        }
        public async Task<PagedList<AtractieTuristicaDto>> GetAtractiiAsync(AtractieTuristicaFilterDto atractieParams)
        {

            var atractii = new List<AtractieTuristica>();
            if(!atractieParams.CityId.HasValue || atractieParams.CityId.Value == 0){
                atractii = await GetAll();
            }
            else{
                atractii = this.GetAllByCityId(atractieParams.CityId.Value);

            }
            var query = atractii.AsQueryable();

            if( !string.IsNullOrEmpty(atractieParams.Nume) && atractieParams.Nume != "null"){
                query = query.Where(u => u.Nume.Contains(atractieParams.Nume));
            }

            if( !string.IsNullOrEmpty(atractieParams.Adresa) && atractieParams.Adresa != "null"){
                query = query.Where(u => u.Adresa.Contains(atractieParams.Adresa));
            }
            query = atractieParams.SortField switch
            {
                "nume" => query.OrderBy(u => u.Nume),
                _ => query.OrderByDescending(u => u.Nume)
            };

            var atractiiTuristiceDto = _mapper.Map<IList<AtractieTuristicaDto>>(query);

            return await PagedList<AtractieTuristicaDto>.CreateAsync(
                atractiiTuristiceDto.AsQueryable(), 
                atractieParams.PageNumber, 
                atractieParams.PageSize);

        }
    }
}
