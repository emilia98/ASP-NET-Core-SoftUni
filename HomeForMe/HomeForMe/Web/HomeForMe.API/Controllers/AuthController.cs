using HomeForMe.Data;
using HomeForMe.Data.Models;
using HomeForMe.InputModels.Auth;
using HomeForMe.OutputModels.Auth;
using HomeForMe.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HomeForMe.API.Controllers
{
    public class AuthController : BaseAPIController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;

        public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterInputModel registerInputModel)
        {
            if (await UserWithUsernameExists(registerInputModel.Username))
            {
                return BadRequest(new
                {
                    Message = "Username is already taken!",
                    HasError = true
                });
            }

            if (await UserWithEmailExists(registerInputModel.Email))
            {
                return BadRequest(new
                {
                    Message = "Email is already taken!",
                    HasError = true
                });
            }

            /*
            var user = new AppUser
            {
                UserName = registerInputModel.Username,
                Email = registerInputModel.Email,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerInputModel.Password)),
                PasswordSalt = hmac.Key
            };

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            */

            return Ok(new
            {
                Message = "Successful registration!",
                HasSuccess = true
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginInputModel loginInputModel)
        {
            var user = await this.GetUserByUsername(loginInputModel.Username);

            if (user == null)
            {
                return Unauthorized(new
                {
                    Message = "Invalid username!",
                    HasError = true
                });
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginInputModel.Password, false);

            if (!result.Succeeded)
            {
                return Unauthorized(new
                {
                    Message = "Invalid password!",
                    HasError = true
                });
            }

            return Ok(new
            {
                Data = new LoginOutputModel
                {
                    Username = user.UserName,
                    Token = await _tokenService.GenerateToken(user)
                },
                Message = "Successfully logged in!",
                HasSuccess = true
            });
        }

        [NonAction]
        private async Task<bool> UserWithUsernameExists(string username)
        {
            return await _userManager.Users.AnyAsync(u => u.UserName == username);
        }

        [NonAction]
        private async Task<bool> UserWithEmailExists(string email)
        {
            return await _userManager.Users.AnyAsync(u => u.Email == email);
        }

        [NonAction]
        private async Task<AppUser> GetUserByUsername(string username)
        {
            return await _userManager.Users.SingleOrDefaultAsync(x => x.UserName == username);
        }
    }
}
