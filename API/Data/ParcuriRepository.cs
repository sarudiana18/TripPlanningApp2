using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    internal class ParcRepository : IParcRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public ParcRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public void AdaugaParc(Parc Parc)
        {
            _context.Parcuri.Add(Parc);
        }

        public void AdaugaListaParcuri(List<Parc> listaParcuri)
        {
            _context.Parcuri.AddRange(listaParcuri);
        }

        public void UpdateParc(Parc parc)
        {
            _context.Parcuri.Update(parc);
        }

        public Parc GetParc(int parcId)
        {
           return _context.Parcuri.Where(x=> x.Id == parcId).Include(p=> p.Photos).Include(r=> r.Reviews).ThenInclude(y=> y.CreatedByUser).FirstOrDefault();
        }
        public Parc GetParcByPhotoId(int photoId)
        {
           return _context.Parcuri.Where(x=> x.Photos.Any(y=> y.Id == photoId)).Include(p=> p.Photos).Include(r=> r.Reviews).ThenInclude(y=> y.CreatedByUser).FirstOrDefault();
        }
        public List<Parc> GetAll()
        {
            return _context.Parcuri.Include(p=> p.Photos).Include(r=> r.Reviews).ThenInclude(y=> y.CreatedByUser).ToList();
        }
        public List<Parc> GetAllByCityId(int cityId)
        {
            return _context.Parcuri.Where(x=> x.CityId == cityId).Include(p=> p.Photos).Include(r=> r.Reviews).ThenInclude(y=> y.CreatedByUser).ToList();
        }
        public bool VerificaExistentaParc(string numeParc, int CityId)
        {
            return _context.Parcuri.Any(x=> x.Nume == numeParc && x.CityId == CityId);
        }
        public async Task<PagedList<ParcDto>> GetParcuriAsync(ParcFilterDto parcParams)
        {
            var parcuri = new List<Parc>();
            if(!parcParams.CityId.HasValue || parcParams.CityId.Value == 0){
                parcuri = this.GetAll();
            }
            else{
                parcuri = this.GetAllByCityId(parcParams.CityId.Value);

            }
            var query = parcuri.AsQueryable();

            if( !string.IsNullOrEmpty(parcParams.Nume) && parcParams.Nume != "null"){
                query = query.Where(u => u.Nume.ToLower().Contains(parcParams.Nume.ToLower()));
            }
            if( !string.IsNullOrEmpty(parcParams.Adresa) && parcParams.Adresa != "null"){
                query = query.Where(u => u.Adresa.ToLower().Contains(parcParams.Adresa.ToLower()));
            }
            if(parcParams.Rating.HasValue){
                query = query.Where(u => u.Rating >= parcParams.Rating);
            }
            query = parcParams.SortField switch
            {
                "nume" => query.OrderBy(u => u.Nume),
                "rating" => query.OrderByDescending(u => u.Rating),
                _ => query.OrderByDescending(u => u.Nume)
            };
            
            var parcuriDto = _mapper.Map<IList<ParcDto>>(query);

            return await PagedList<ParcDto>.CreateAsync(
                parcuriDto.AsQueryable(), 
                parcParams.PageNumber, 
                parcParams.PageSize);

        }
        public void Delete(Parc obiect)
        {
            _context.Parcuri.Remove(obiect);
        }
    }
}
