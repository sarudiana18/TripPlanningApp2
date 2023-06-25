using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUnitOfWork _uow;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService, IMapper mapper,
        IUnitOfWork uow)
        {
            _userManager = userManager;
            _mapper = mapper;
            _tokenService = tokenService;
            _uow = uow;
        }

        [HttpPost("register")] // POST: api/account/register?username=dave&password=pwd
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await UserExists(registerDto.Username)) return BadRequest("Numele de utilizator este luat");

            var user = _mapper.Map<AppUser>(registerDto);

            user.UserName = registerDto.Username.ToLower();
            user.CityId = registerDto.City.Id;

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded) return BadRequest(result.Errors);

            var roleResult = await _userManager.AddToRoleAsync(user, "Member");

            if (!roleResult.Succeeded) return BadRequest(result.Errors);

            return new UserDto
            {
                Id = user.Id,
                Username = user.UserName,
                Token = await _tokenService.CreateToken(user),
                FirstName = user.FirstName,
                LastName = user.LastName,
                Oras = registerDto.City,
                NumeOrasCurent = registerDto.City.Name,
                CountryNume = registerDto.Country.Name,
                Country = registerDto.Country
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.Users
                .Include(p => p.Photos)
                .Include(x => x.City).ThenInclude(y=> y.State)
                .SingleOrDefaultAsync(x => x.UserName == loginDto.Username);

            if (user == null) return Unauthorized("Nume de utilizator invalid");

            var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if (!result) return Unauthorized("Parola invalida");

            var tara = _uow.CountryRepository.GetCountryByNume(user.CountryNume);

            return new UserDto
            {
                Username = user.UserName,
                Token = await _tokenService.CreateToken(user),
                PhotoUrl = user.Photos.FirstOrDefault(x => x.IsMain)?.Url,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Oras = user.City,
                NumeOrasCurent = user.City.Name,
                CountryNume = user.CountryNume,
                Country = tara,
                Id = user.Id
            };
        }

        private async Task<bool> UserExists(string username)
        {
            return await _userManager.Users.AnyAsync(x => x.UserName == username.ToLower());
        }
    }
}