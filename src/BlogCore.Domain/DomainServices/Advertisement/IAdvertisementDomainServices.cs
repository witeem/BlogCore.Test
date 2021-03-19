using BlogCore.Core.Serivces;
using BlogCore.Domain.DomainServices.Entitys;
using System.Threading.Tasks;

namespace BlogCore.Domain.DomainServices.Advertisement
{
    public interface IAdvertisementDomainServices: IDomainService
    {
        Task<int> Sum(int i, int j);

        Task<AdverUserEntity> FindUser(string userName, string password);
    }
}
