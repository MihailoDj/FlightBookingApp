using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using api.Data;
using api.DTO;
using api.Entities;
using api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public AccountController(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        [Authorize]
        [HttpPost("register-user")]
        public async Task<ActionResult<UserDTO>> RegisterUser(RegisterDTO registerDTO)
        {
            if (await UserExists(registerDTO.Username)) return BadRequest("Username taken");

            using var hmac = new HMACSHA512();

            var user = new User
            {
                UserName = registerDTO.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password)),
                PasswordSalt = hmac.Key,
                Role = (Enums.UserRole)registerDTO.Role
            };

            _userRepository.AddUserAsync(user);
            await _userRepository.SaveAllAsync();

            return new UserDTO
            {
                Id = user.Id,
                UserName = user.UserName,
                Role = (int)user.Role,
                Token = _tokenService.CreateToken(user)
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        {
            var user = await _userRepository.GetUserByUsernameAsync(loginDTO.Username);

            if (user == null) return Unauthorized("Invalid username");

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));

            for (int i = 0; i < computedHash.Length; i++)
                if (computedHash[i] != user.PasswordHash[i])
                    return Unauthorized("Invalid password");

            return new UserDTO
            {
                Id = user.Id,
                UserName = user.UserName,
                Role = (int)user.Role,
                Token = _tokenService.CreateToken(user)
            };
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return Ok(await _userRepository.GetUsersAsync());
        }

        private async Task<bool> UserExists(string username)
        {
            return await _userRepository.UserExistsAsync(username);
        }
    }
}