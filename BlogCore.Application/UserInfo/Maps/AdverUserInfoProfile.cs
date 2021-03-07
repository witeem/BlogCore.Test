using AutoMapper;
using BlogCore.Application.UserInfo.Dtos;
using BlogCore.Model.Models.UserInfo;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogCore.Application.UserInfo.Maps
{
    public class AdverUserInfoProfile:Profile
    {
        public AdverUserInfoProfile()
        {
            CreateMap<AdverUserInfo, AdverUserInfoDto>();
        }
    }
}
