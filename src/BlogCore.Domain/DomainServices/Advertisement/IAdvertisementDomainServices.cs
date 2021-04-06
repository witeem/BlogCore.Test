using BlogCore.Core.Serivces;
using BlogCore.Core.UserInfo;
using BlogCore.Domain.DomainServices.Entitys;
using System.Threading.Tasks;

namespace BlogCore.Domain.DomainServices.Advertisement
{
    public interface IAdvertisementDomainServices: IDomainService
    {
        Task<decimal> Sum(decimal i, decimal j);

        Task<AdverUserInfo> GetUserInfo();

        Task<AdverUserInfo> AddUserInfo(AdverUserInfo input);
    }
}
