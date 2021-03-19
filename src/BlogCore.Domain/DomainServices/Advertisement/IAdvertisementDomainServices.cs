using BlogCore.Core.Serivces;
using BlogCore.Domain.DomainServices.Dto;
using System.Threading.Tasks;

namespace BlogCore.Domain.DomainServices.Advertisement
{
    public interface IAdvertisementDomainServices: IDomainService
    {
        Task<int> Sum(int i, int j);

        Task<User> FindUser(string userName, string password);
    }
}
