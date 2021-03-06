﻿using BlogCore.Application.Services;
using BlogCore.Application.UserInfo.Dtos;
using System.Threading.Tasks;

namespace BlogCore.Application.UserInfo
{
    public interface IUserInfoAppService : IAppService
    {
        Task<AdverUserInfoDto> GetUserInfo();

        Task<AdverUserInfoDto> AddUserInfo(AdverUserInfoDto input);

        Task<decimal> Sum(decimal i, decimal j);
    }
}
