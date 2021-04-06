using BlogCore.Application.Advertisement;
using BlogCore.Application.UserInfo;
using BlogCore.Application.UserInfo.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogCore.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IAdvertisementService _advertisementServices;
        private readonly IUserInfoAppService _userInfoAppService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="advertisementServices"></param>
        public WeatherForecastController(ILogger<WeatherForecastController> logger, IAdvertisementService advertisementServices, IUserInfoAppService userInfoAppService)
        {
            _logger = logger;
            _advertisementServices = advertisementServices;
            _userInfoAppService = userInfoAppService;
        }

        /// <summary>
        /// Swagger测试Get请求
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        /// <summary>
        /// 基于策略的授权机制
        /// </summary>
        /// <returns></returns>
        [HttpGet("AuthorizeGet")]
        [Authorize(Policy = "SystemOrAdmin")]
        public ActionResult<IEnumerable<string>> AuthorizeGet()
        {
            return new string[] { "SystemOrAdmin", "true" };
        }
    }
}
