using BlogCore.Core.UserInfo;
using BlogCore.Core.UserInfo.Respoitorys;
using BlogCore.Domain.DomainServices.Entitys;
using BlogCore.Extended;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogCore.Domain.DomainServices.Advertisement
{
    public class AdvertisementDomainServices : IAdvertisementDomainServices
    {
        private readonly ILogger<AdvertisementDomainServices> _logger;
        private readonly IUserInfoRepository _userInfoRepository;

        public AdvertisementDomainServices(ILogger<AdvertisementDomainServices> logger, IUserInfoRepository userInfoRepository)
        {
            _logger = logger;
            _userInfoRepository = userInfoRepository;
        }

        public async Task<decimal> Sum(decimal i, decimal j)
        {
            await Task.Delay(100);
            string secret = ConfigManagerConf.GetValue("Audience:Secret");
            Console.WriteLine($"Default:{secret}");
            return i * j;
        }


        public async Task<AdverUserInfo> GetUserInfo()
        {
            return await _userInfoRepository.GetAsync(m => m.Id == 1, true);
        }
    }
}
