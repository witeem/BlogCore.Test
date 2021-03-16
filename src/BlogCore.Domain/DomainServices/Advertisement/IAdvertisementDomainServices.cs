using BlogCore.Core.Serivces;
using System.Threading.Tasks;

namespace BlogCore.Domain.DomainServices.Advertisement
{
    public interface IAdvertisementDomainServices: IDomainService
    {
        Task<int> Sum(int i, int j);
    }
}
