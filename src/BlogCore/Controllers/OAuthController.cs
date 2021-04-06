using BlogCore.Application.Advertisement;
using BlogCore.Application.UserInfo;
using BlogCore.Application.UserInfo.Dtos;
using BlogCore.Domain;
using IdentityModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OAuthController : ControllerBase
    {
        private IUserInfoAppService _userInfoAppService;

        public OAuthController(IUserInfoAppService userInfoAppService)
        {
            _userInfoAppService = userInfoAppService;
        }

        /// <summary>
        /// 生成令牌
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        [HttpPost("Authenticate")]
        public async Task<ActionResult> Authenticate([FromBody]UserDto userDto)
        {
            var user = await _userInfoAppService.GetUserInfo();
            if (user == null) return Unauthorized();
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(AppConsts.Secret);
            var authTime = DateTime.UtcNow;
            var expiresAt = authTime.AddDays(7);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(JwtClaimTypes.Audience,"api"),
                    new Claim(JwtClaimTypes.Issuer,"http://localhost:5200"),
                    new Claim(JwtClaimTypes.Id, user.Id.ToString()),
                    new Claim(JwtClaimTypes.Name, user.Name),
                    new Claim(JwtClaimTypes.Email, user.Email ?? "http://witeem@126.com"),
                    new Claim(JwtClaimTypes.PhoneNumber, user.PhoneNumber ?? "13570501792")
                }),
                Expires = expiresAt,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return Ok(new
            {
                access_token = tokenString,
                token_type = "Bearer",
                profile = new
                {
                    sid = user.Id,
                    name = user.Name,
                    auth_time = new DateTimeOffset(authTime).ToUnixTimeSeconds(),
                    expires_at = new DateTimeOffset(expiresAt).ToUnixTimeSeconds()
                }
            });
        }


        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetUserInfo")]
        public async Task<AdverUserInfoDto> GetUserInfo()
        {
            return await _userInfoAppService.GetUserInfo();
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetUserInfo")]
        public async Task<AdverUserInfoDto> Sum()
        {
            return await _userInfoAppService.Sum();
        }


    }


    public class UserDto
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}