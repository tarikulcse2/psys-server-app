using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Test.Entities.Models;
using Test.Service;
using Test.WebApi.ViewModel;

namespace Test.WebApi.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUserService _userService;
        private IConfiguration config;
        public AccountController(
            IUserService userService,
            IConfiguration config
            )
        {
            _userService = userService;
            this.config = config;
        }

        [HttpGet(nameof(GetAllUser))]
        public IActionResult GetAllUser(){
            return Ok(_userService.GetAll());
        }

        [AllowAnonymous]
        [HttpPost(nameof(Login))]
        public ActionResult Login([FromBody] LoginViewModel loginView)
        {
            User user = _userService.CheckUserLogin(new User() { Email = loginView.Email, Password = loginView.Password });
            if (user != null)
                return Ok(new { user.Email, user.Id, IsAuthorized = true, Token = GetToken(user) });
            return Ok(new { IsAuthorized = false, Data = (string)null, Message = "Invalid User" });
        }
        [AllowAnonymous]
        [HttpPost(nameof(Registration))]
        public IActionResult Registration([FromBody] User user)
        {
            if(_userService.Registration(user) > 0)
                return Ok(new { status = true, Data = user, Message = "Save Success!" });
            return Ok(new { status = false, Data = (string)null, Message = "Error Success!" });
        }

        private string GetToken(User userInfo)
        {
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            List<Claim> claims = new List<Claim> {
                new Claim("userId", userInfo.Id.ToString()),
                new Claim(ClaimTypes.Email, userInfo.Email),
                new Claim(ClaimTypes.Sid, Guid.NewGuid().ToString())
            };
            JwtSecurityToken token = new JwtSecurityToken(config["Jwt:Issuer"], config["Jwt:Issuer"],
                                            claims, notBefore: DateTime.UtcNow,
                                            expires: DateTime.Now.AddYears(1),
                                            signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
