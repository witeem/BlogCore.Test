using Autofac.Extras.DynamicProxy;
using BlogCore.Domain.DomainServices.Advertisement;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace BlogCore.Application.Advertisement
{
    [Intercept(typeof(ServiceInterceptor))]
    public class AdvertisementServices: IAdvertisementServices
    {
        private readonly ILogger<AdvertisementServices> _logger;
        private readonly IAdvertisementDomainServices _advertisementDomainServices;

        public AdvertisementServices(ILogger<AdvertisementServices> logger, IAdvertisementDomainServices advertisementDomainServices)
        {
            _logger = logger;
            _advertisementDomainServices = advertisementDomainServices;
        }

        public async Task<int> Sum(int i, int j)
        {
            return await _advertisementDomainServices.Sum(i, j);
        }
    }
}
