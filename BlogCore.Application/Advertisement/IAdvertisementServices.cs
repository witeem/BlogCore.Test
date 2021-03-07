using BlogCore.Application.Services;
using System.Threading.Tasks;

namespace BlogCore.Application.Advertisement
{
    public interface IAdvertisementServices: IAppService
    {
        Task<int> Sum(int i, int j);
    }
}
