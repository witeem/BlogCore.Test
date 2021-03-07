using AutoMapper;
using BlogCore.Application.UserInfo.Dtos;
using BlogCore.Model.Models.UserInfo;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.Application.UserInfo
{
    public class UserInfoAppService:IUserInfoAppService
    {
        private readonly ILogger<UserInfoAppService> _logger;
        private readonly IMapper _mapper;
        public UserInfoAppService(ILogger<UserInfoAppService> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<AdverUserInfoDto> GetUserInfo()
        {
            await Task.Delay(100);
            AdverUserInfo userInfo = new AdverUserInfo()
            {
                Id = 1,
                Name = "Witeem",
                RoleCodes = new List<string>() {"Amdin","Laoban" }
            };
            return _mapper.Map<AdverUserInfoDto>(userInfo);
        }
        
    }
}
