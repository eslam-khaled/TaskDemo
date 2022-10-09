using AutoMapper;
using DemoTask.Business.BusinessInterface;
using DemoTask.Business.Helpers;
using DemoTask.DAL.Models;
using DemoTask.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace DemoTask.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly JwtHandler _jwtHandler;
        private readonly ILoginTokenBusiness _loginTokenBusiness;
        public UserController(
            IMapper mapper,
            UserManager<User> userManager,
             JwtHandler jwtHandler,
             ILoginTokenBusiness loginTokenBusiness)
        {
            _mapper = mapper;
            _userManager = userManager;
            _jwtHandler = jwtHandler;
            _loginTokenBusiness = loginTokenBusiness;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserDto userForAuthentication)
        {
            var user = await _userManager.FindByNameAsync(userForAuthentication.Username);
            if (user == null || !await _userManager.CheckPasswordAsync(user, userForAuthentication.Password))
            {
                return Unauthorized(new UserDto { Message = "Invalid Authentication" });
            }
            var Role = await _userManager.GetRolesAsync(user);
            var signingCredentials = _jwtHandler.GetSigningCredentials();
            var claims = _jwtHandler.GetClaims(user);
            var tokenOptions = _jwtHandler.GenerateTokenOptions(signingCredentials, claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            var refreshToken = _jwtHandler.GenerateRefreshToken();

            LoginTokenDto loginTokenDto = new LoginTokenDto()
            {
                UserName = user.UserName,
                RefreshToken = refreshToken,
                Token = token
            };

            //TODO - check if first time, add , else update
            if (_loginTokenBusiness.CheckIfUserExist(loginTokenDto) == true)
            {
                _loginTokenBusiness.UpdateRefreshToken(loginTokenDto.RefreshToken, loginTokenDto.UserName);
            }
            else if (_loginTokenBusiness.CheckIfUserExist(loginTokenDto) == false)
            {
                _loginTokenBusiness.AddLoginTokenForUser(loginTokenDto);
            }
            

            return Ok(new UserDto
            {
                Success = true,
                Token = token,
                Username = userForAuthentication.Username,
                Role = Role.FirstOrDefault(),
                RefreshToken = refreshToken
            });
        }


        [HttpPost]
        [Route("RefreshToken")]
        public IActionResult RefreshToken([FromBody] LoginTokenDto loginToken)
        {
            var token = new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(loginToken.Token);
            var principal = _jwtHandler.GetPrincipalFromExpiredToken(token);
            var username = principal.Identity.Name;
            var user = _loginTokenBusiness.GetLoginTokenForUser(loginToken);

            //var user = userContext.LoginModels.SingleOrDefault(u => u.UserName == username);
            if (user == null || user.RefreshToken != loginToken.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return BadRequest("Invalid client request");
            }


            var newAccessToken = _jwtHandler.GenerateAccessToken(principal.Claims);
            var newRefreshToken = _jwtHandler.GenerateRefreshToken();



            var result = _loginTokenBusiness.UpdateRefreshToken(newRefreshToken, user.UserName);
            if (result == false)
            {
                return BadRequest("Couldn't update token");
            }

            return new ObjectResult(new
            {
                accessToken = newAccessToken,
                refreshToken = newRefreshToken
            });

        }


        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserDto userForRegistration)
        {
            if (userForRegistration == null || !ModelState.IsValid)
                return BadRequest();

            User user = new User()
            {
                UserName = userForRegistration.Username,

            };


            var result = await _userManager.CreateAsync(user, userForRegistration.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);

                return BadRequest(new UserDto { Errors = errors });
            }


            await AddUserToRole(user, userForRegistration.Role);

            return StatusCode(201);
        }

        private async Task AddUserToRole(User user, string role)
        {
            if (user == null || string.IsNullOrEmpty(role))
            {
                return;
            }
            var result = await _userManager.AddToRoleAsync(user, role);


        }

    }
}
