using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using CategoriesService.Application.DTOs;
using CategoriesService.Application.Interfaces;
using CategoriesService.Persistence.Data;
using CategoriesService.Infrastructure.ExternalServices;
using Microsoft.EntityFrameworkCore;
using CategoriesService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CategoriesService.API.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly CategoriesDbContext _context; // Add this field

        public AuthController(ITokenService tokenService, CategoriesDbContext context) // Add context to constructor
        {
            _tokenService = tokenService;
            _context = context;

        }

        // for new login generate Hash
        [HttpGet("generatehash")]
        public IActionResult GenerateHash()
        {
            string newPassword = "Admin@123";
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(newPassword);
            return Ok(hashedPassword);
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login(LoginDto loginDto)
        {
            var user = _context.Users
                .FirstOrDefault(x => x.Name == loginDto.UserName);

            if (user == null)
                return Unauthorized("User not found");

            bool isValidPassword = BCrypt.Net.BCrypt.Verify(
                loginDto.Password,
                user.PasswordHash);

            if (!isValidPassword)
                return Unauthorized("Invalid password");

            var token = _tokenService.GenerateToken(
                user.Name,
                user.Role);

            return Ok(new
            {
                Message = "Login Successful",
                Token = token
            });
        }
    }
}
