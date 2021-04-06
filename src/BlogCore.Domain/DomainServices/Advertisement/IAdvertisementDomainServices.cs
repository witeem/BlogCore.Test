﻿using BlogCore.Core.Serivces;
using BlogCore.Core.UserInfo;
using BlogCore.Domain.DomainServices.Entitys;
using System.Threading.Tasks;

namespace BlogCore.Domain.DomainServices.Advertisement
{
    public interface IAdvertisementDomainServices: IDomainService
    {
        Task<int> Sum(int i, int j);

        Task<AdverUserInfo> GetUserInfo();
    }
}
