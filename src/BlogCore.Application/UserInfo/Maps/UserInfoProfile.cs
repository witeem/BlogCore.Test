using AutoMapper;
using BlogCore.Application.UserInfo.Dtos;
using BlogCore.Core.UserInfo;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogCore.Application.UserInfo.Maps
{
    public class UserInfoProfile:Profile
    {
        public UserInfoProfile()
        {
            CreateMap<AdverUserInfo, AdverUserInfoDto>().ReverseMap();
        }
    }
}
