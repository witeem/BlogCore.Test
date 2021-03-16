using AutoMapper;
using BlogCore.Application.UserInfo.Dtos;
using BlogCore.Core.UserInfo;

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
