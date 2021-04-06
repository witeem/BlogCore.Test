using AutoMapper;
using BlogCore.Application.UserInfo.Dtos;
using BlogCore.Core.UserInfo;
using BlogCore.Core.UserInfo.Respoitorys;
using BlogCore.Domain.DomainServices.Advertisement;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogCore.Application.UserInfo
{
    public class UserInfoAppService:IUserInfoAppService
    {
        private readonly ILogger<UserInfoAppService> _logger;
        private readonly IMapper _mapper;
        private readonly IUserInfoRepository _userInfoRepository;
        private readonly IAdvertisementDomainServices _advertisementDomainServices;
        public UserInfoAppService(ILogger<UserInfoAppService> logger, IMapper mapper, IUserInfoRepository userInfoRepository, IAdvertisementDomainServices advertisementDomainServices)
        {
            _logger = logger;
            _mapper = mapper;
            _userInfoRepository = userInfoRepository;
            _advertisementDomainServices = advertisementDomainServices;
        }

        public async Task<AdverUserInfoDto> GetUserInfo()
        {
            var userInfo = await _advertisementDomainServices.GetUserInfo();
            // AdverUserInfo userInfo = await _userInfoRepository.GetAsync(m => m.Id == 1, true);
            // await Task.Delay(100);
            //AdverUserInfo userInfo = new AdverUserInfo()
            //{
            //    Id = 1,
            //    Name = "Witeem",
            //    RoleCodes = "admin"
            //};
            return _mapper.Map<AdverUserInfoDto>(userInfo);
        }
        
    }
}
